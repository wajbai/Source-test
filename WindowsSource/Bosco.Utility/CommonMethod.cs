using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Bosco.Utility;
using Bosco.Utility.CommonMemberSet;
using System.Globalization;
using Bosco.Utility.ConfigSetting;
using System.Threading;
using DevExpress.LookAndFeel;
using System.Security.Cryptography;
using System.IO;
using System.Xml.Serialization;
using System.Xml;
using System.Reflection;
using System.IO.Compression;
using System.Windows.Forms;
using Microsoft.Win32;
using System.Net;
using System.Text.RegularExpressions;
using DevExpress.XtraTreeList.Nodes.Operations;
using DevExpress.XtraTreeList.Nodes;
using System.Configuration;


namespace Bosco.Utility
{
    public class CommonMethod : IDisposable
    {
        #region Decelaration
        DefaultVoucherTypes enumVoucherType = new DefaultVoucherTypes();
        public static DataTable dtApplyUserRights = new DataTable();
        public static DataTable dtUseRights = new DataTable();
        ResultArgs resultArgs = new ResultArgs();
        public static int Flag = 0;
        private static double TDSTaxAmount = 0;
        private static double EdCessAmount = 0;
        private static double SurchargeAmount = 0;
        private static double SecEduCessAmount = 0;
        public static double SumofTaxAmount = 0;
        public static double NetTdsAmount = 0;
        private static string TaxDescription = string.Empty;
        public static string MultiDataBaseName = string.Empty;
        public static string HeadOfficeCode = string.Empty;
        public static string BranchOfficeCode = string.Empty;
        private string LICENSE_FILENAME = Path.Combine(Application.StartupPath.ToString(), "AcMEERPLicense.xml");

        public static string RegistryPath = @"Software\BoscoSoft\AcMEERP";
        public static double CalCuTaxRate = 0;
        public static double CalCuTaxAmount = 0;
        public static double NetAmout = 0;
        public static double CalCuDiscountAmount = 0;
        public static double CalCuDiscountPer = 0;

        #endregion

        #region Methods
        /// <summary>
        /// This is to get the list of Currency symbol for the all the country
        /// </summary>
        /// <returns></returns>
        public DataTable GetCurrencySymbolList()
        {
            DataTable dtCurrencySymbol = new DataTable();
            dtCurrencySymbol.Columns.Add(new DataColumn("Currency Symbol", typeof(string)));
            // dtCurrencySymbol.Columns.Add(new DataColumn("Name", typeof(string)));
            dtCurrencySymbol.Columns.Add(new DataColumn("Currency Code", typeof(string)));
            dtCurrencySymbol.Columns.Add(new DataColumn("Country Code", typeof(string)));
            dtCurrencySymbol.Columns.Add(new DataColumn("Country", typeof(string)));
            dtCurrencySymbol.Columns.Add(new DataColumn("Currency Name", typeof(string)));
            try
            {
                CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
                foreach (CultureInfo culture in cultures)
                {
                    var regionInfo = new RegionInfo(culture.Name);
                    dtCurrencySymbol.Rows.Add(regionInfo.CurrencySymbol, regionInfo.ISOCurrencySymbol, regionInfo.ThreeLetterISORegionName, regionInfo.DisplayName, regionInfo.CurrencyEnglishName);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            return dtCurrencySymbol;
        }

        /// <summary>
        /// Method to predict the next code
        /// </summary>
        /// <param name="parCode"></param>
        /// <returns></returns>
        public static string GetPredictedCode(string parCode, DataTable dtCode)
        {
            parCode = parCode != string.Empty ? parCode : "000";
            string finalCode = "";
            string tempstr = "";
            string Code = string.Empty;
            try
            {
                string prefixCode = Regex.Match(parCode, @"^[A-Z|a-z]+").Value;
                string digitCode = Regex.Match(parCode, @"\d+").Value;
                string suffixCode = Regex.Match(parCode, @"[A-Z|a-z]+$").Value;
                int tempCode = Convert.ToInt32(digitCode) + 1;
                if (prefixCode.Length != parCode.Length)
                {
                    if (digitCode.Length != parCode.Length)
                    {
                        // To check no of zero available in the code                     
                        if (ZeroCount(digitCode) != 0)
                            finalCode = prefixCode + tempCode.ToString(tempstr = AddZero(digitCode.Length)) + suffixCode;
                        else
                            finalCode = prefixCode + tempCode.ToString() + suffixCode;
                    }
                    else
                    {
                        // To check no of zero available in the code                     
                        if (ZeroCount(digitCode) != 0)
                            finalCode = prefixCode + tempCode.ToString(tempstr = AddZero(digitCode.Length)) + suffixCode;
                        else
                            finalCode = prefixCode + tempCode.ToString() + suffixCode;
                    }
                }
                // To check the generated code is present already or not
                for (int i = 0; i < dtCode.Rows.Count && dtCode != null && dtCode.Rows.Count > 0; i++)
                {
                    if (finalCode.Equals(dtCode.Rows[i][0].ToString())) { finalCode = GetPredictedCode(finalCode, dtCode); i = 0; }
                }
            }
            catch (Exception ex)
            {
                string exception = ex.ToString();
            }

            return finalCode;
        }

        public static string EscapeLikeValue(string value)
        {
            string rtn = value;
            string pattern = @"([-\]\[<>\?\*\\\""/\|\~\(\)\#/=><+\%&\^\'])";
            Regex expression = new Regex(pattern);

            if (expression.IsMatch(value))
            {
                StringBuilder sb = new StringBuilder(value.Length);
                for (int i = 0; i < value.Length; i++)
                {
                    char c = value[i];
                    switch (c)
                    {
                        case ']':
                        case '[':
                        case '%':
                        case '*':
                            sb.Append("[").Append(c).Append("]");
                            break;
                        case '\'':
                            sb.Append("''");
                            break;
                        default:
                            sb.Append(c);
                            break;
                    }
                }
                rtn = sb.ToString();
            }
            return rtn;
        }

        /// <summary>
        /// To count zeros present in the digitCode
        /// </summary>
        /// <param name="digit"></param>
        /// <returns></returns>
        private static int ZeroCount(string digit)
        {
            Regex reg = new Regex(@"0+");
            Match mat = reg.Match(digit);
            string tempstr = mat.Value;
            return tempstr.Length;
        }

        /// <summary>
        /// To add zero to string 
        /// </summary>
        /// <param name="length"></param>
        /// <returns></returns>
        private static string AddZero(int length)
        {
            string tempstr = "";
            for (int i = 1; i <= length; i++)
                tempstr = tempstr + "0";
            return tempstr;
        }

        /// <summary>
        /// This is to get the list of voucher types.
        /// </summary>
        /// <param name="cboVoucherType"></param>
        public void GetVoucherType(DevExpress.XtraEditors.ComboBoxEdit cboVoucherType)
        {
            EnumSetMember enumSetMember = new EnumSetMember();
            DataView dvVoucherType = enumSetMember.GetEnumDataSource(enumVoucherType, Sorting.None);
            if (dvVoucherType != null && dvVoucherType.Count != 0)
            {
                for (int i = 0; i < dvVoucherType.Count; i++)
                {
                    cboVoucherType.Properties.Items.Add(dvVoucherType.Table.Rows[i]["Name"].ToString());
                }
            }
        }

        public void ApplyTheme(string SkinName)
        {
            UserLookAndFeel.Default.SetSkinStyle(SkinName);
        }

        public DataTable AddHeaderColumn(DataTable dtSource, string ValueField, string DisplayField, string HeaderCaption = "<--All-->")
        {
            DataRow dr = null;
            if (dtSource.Columns.Contains(ValueField) && dtSource.Columns.Contains(DisplayField))
            {
                dr = dtSource.NewRow();
                dr[ValueField] = 0;
                dr[DisplayField] = HeaderCaption;
                dtSource.Rows.InsertAt(dr, 0);
            }
            return dtSource;
        }

        public static void EnableNavGroup(DevExpress.XtraNavBar.NavBarGroup navBarGroup, int GroupId)
        {
            try
            {
                if (dtApplyUserRights != null && dtApplyUserRights.Rows.Count != 0)
                {
                    DataView dvRights = dtApplyUserRights.DefaultView;
                    dvRights.RowFilter = "ACTIVITY_ID=" + GroupId + " OR PARENT_ID=" + GroupId + "";
                    if (dvRights == null || dvRights.Count == 0)
                    {
                        navBarGroup.Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        public static void ApplyRights(DevExpress.XtraNavBar.NavBarItem barItem, int ActivityId)
        {
            try
            {
                if (dtApplyUserRights != null && dtApplyUserRights.Rows.Count != 0)
                {
                    DataView dvRights = dtApplyUserRights.DefaultView;
                    dvRights.RowFilter = "ACTIVITY_ID=" + ActivityId + " OR PARENT_ID=" + ActivityId + "";
                    if (dvRights == null || dvRights.Count == 0)
                    {
                        barItem.Visible = false;
                    }
                    else
                    {
                        Flag = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        public static int ApplyRights(int ActivityId)
        {
            int AllowRights = 0;
            try
            {
                if (dtApplyUserRights != null && dtApplyUserRights.Rows.Count != 0)
                {
                    DataView dvRights = dtApplyUserRights.DefaultView;
                    dvRights.RowFilter = "ACTIVITY_ID=" + ActivityId + " OR PARENT_ID=" + ActivityId + "";
                    if (dvRights == null || dvRights.Count == 0)
                    {
                        AllowRights = 0;
                    }
                    else
                    {
                        AllowRights = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
            return AllowRights;
        }

        public static void EnableMenu(DevExpress.XtraNavBar.NavBarItem barItem, int ActivityId)
        {
            try
            {
                if (dtApplyUserRights != null && dtApplyUserRights.Rows.Count != 0)
                {
                    DataView dvRights = dtApplyUserRights.DefaultView;
                    dvRights.RowFilter = "ACTIVITY_ID=" + ActivityId + " OR PARENT_ID=" + ActivityId + "";
                    if (dvRights == null || dvRights.Count == 0)
                    {
                        barItem.Enabled = false;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        public static DataTable ApplyUserRightsForForms(int ActivityId)
        {
            DataTable dtForm = new DataTable();
            try
            {
                if (dtApplyUserRights != null && dtApplyUserRights.Rows.Count != 0)
                {
                    DataView dvRights = dtApplyUserRights.DefaultView;
                    dvRights.RowFilter = "ACTIVITY_ID=" + ActivityId + " OR PARENT_ID=" + ActivityId + "";
                    if (dvRights == null || dvRights.Count == 0)
                    {
                        dtForm = null;
                    }
                    else
                    {
                        dtForm = dvRights.ToTable();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
            return dtForm;
        }

        public static int ApplyUserRights(int ActivityId)
        {
            int AllowRights = 0;
            try
            {
                if (dtApplyUserRights != null && dtApplyUserRights.Rows.Count != 0)
                {
                    DataView dvRights = dtApplyUserRights.DefaultView;
                    dvRights.RowFilter = "ACTIVITY_ID=" + ActivityId + "";
                    if (dvRights == null || dvRights.Count == 0)
                    {
                        AllowRights = 0;
                    }
                    else
                    {
                        AllowRights = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
            return AllowRights;
        }

        public static int ApplyUserRightsForTransaction(int ActivityId)
        {
            int AllowRights = 0;
            try
            {
                if (dtApplyUserRights != null && dtApplyUserRights.Rows.Count != 0)
                {
                    DataView dvRights = dtApplyUserRights.DefaultView;
                    dvRights.RowFilter = "ACTIVITY_ID=" + ActivityId + " OR PARENT_ID=" + ActivityId + "";
                    if (dvRights == null || dvRights.Count == 0)
                    {
                        AllowRights = 0;
                    }
                    else
                    {
                        AllowRights = 1;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
            return AllowRights;
        }

        public static string Encrept(string EncreptString)
        {
            try
            {
                SimpleEncrypt.SimpleEncDec objDec = new SimpleEncrypt.SimpleEncDec();
                if (!string.IsNullOrEmpty(EncreptString))
                {
                    EncreptString = objDec.EncryptString(EncreptString);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally { }
            return EncreptString;
        }

        public static ResultArgs DecreptWithResultArg(string EncreptValue)
        {
            ResultArgs resultArg = new ResultArgs();
            string decreptValue = string.Empty;
            try
            {
                SimpleEncrypt.SimpleEncDec objDec = new SimpleEncrypt.SimpleEncDec();
                if (!string.IsNullOrEmpty(EncreptValue))
                {
                    decreptValue = objDec.DecryptString(EncreptValue);
                    resultArg.ReturnValue = decreptValue;
                    resultArg.Success = true;
                }
            }
            catch (Exception ex)
            {
                resultArg.Message = ex.Message;
            }
            return resultArg;
        }

        public static string Decrept(string EncreptValue)
        {
            ResultArgs resultarg = new ResultArgs();
            string decreptValue = string.Empty;
            try
            {
                resultarg = DecreptWithResultArg(EncreptValue);
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
            }
            finally
            {
                if (resultarg.Success)
                {
                    decreptValue = resultarg.ReturnValue.ToString();
                }
                else
                {
                    MessageRender.ShowMessage(resultarg.Message, true);
                }
            }
            return decreptValue;
        }


        /// <summary>
        /// On 25/08/2021, To check given text is encrypted or not
        /// </summary>
        /// <param name="EncreptValue"></param>
        /// <returns></returns>
        public static bool IsEncryptedText(string EncreptValue)
        {
            ResultArgs resultarg = new ResultArgs();
            bool rtn = false;
            string decreptValue = string.Empty;
            try
            {
                resultarg = DecreptWithResultArg(EncreptValue);
                rtn = resultarg.Success;
            }
            catch (Exception ex)
            {
                string msg = ex.Message;
                rtn = false;
            }
            return rtn;
        }
        
        private ResultArgs ValidateLicenseHeadOfficeCode(DataSet dsMasters)
        {
            string LicenseHeadOfficeCode = string.Empty;
            string MasterHeadOfficeCode = string.Empty;
            // resultArgs = GetLicenseHeadofficeCode();
            //if (resultArgs.Success)
            //{
            // if ((!string.IsNullOrEmpty(resultArgs.ReturnValue.ToString())) && resultArgs.ReturnValue != null)
            // {
            LicenseHeadOfficeCode = SettingProperty.headofficecode;  //resultArgs.ReturnValue.ToString();
            //Added by Carmel Raj
            HeadOfficeCode = LicenseHeadOfficeCode;
            resultArgs = GetHeadOfficeCode(dsMasters);
            if (resultArgs.Success)
            {
                if (resultArgs.ReturnValue != null)
                {
                    MasterHeadOfficeCode = resultArgs.ReturnValue.ToString();

                    if (!string.IsNullOrEmpty(MasterHeadOfficeCode))
                    {
                        bool isValidHeadCode = LicenseHeadOfficeCode.Equals(MasterHeadOfficeCode);
                        if (!isValidHeadCode)
                        {
                            resultArgs.Message = "License Head Office Code does not match with Master XML Head Code";
                        }
                    }
                }
                else
                {
                    resultArgs.Message = "Head Office Code is Empty in Master XML";
                }
            }
            //  }
            //  else
            //   {
            //       resultArgs.Message = "Head Office Code is empty in License XML";
            //   }
            // }
            return resultArgs;
        }

        public ResultArgs ValidateLicenseBranchCode(DataSet dsMasters)
        {
            string LicenseBranchCode = string.Empty;
            string MasterBranchCode = string.Empty;
            //resultArgs = GetLicenseBranchCode();
            //if (resultArgs.Success)
            // {
            //if (resultArgs.ReturnValue != null)
            //{
            LicenseBranchCode = SettingProperty.branachOfficeCode;    // resultArgs.ReturnValue.ToString();
            //Added by Carmel Raj
            BranchOfficeCode = LicenseBranchCode;

            resultArgs = GetBranchOfficeCode(dsMasters);
            if (resultArgs.Success)
            {
                if ((!string.IsNullOrEmpty(resultArgs.ReturnValue.ToString())) && resultArgs.ReturnValue != null)
                {
                    MasterBranchCode = resultArgs.ReturnValue.ToString();
                    if (!string.IsNullOrEmpty(MasterBranchCode))
                    {
                        bool isBranchCodeValid = LicenseBranchCode.Equals(MasterBranchCode);
                        if (!isBranchCodeValid)
                        {
                            resultArgs.Message = "License Branch Code does not match with the Master XML Branch Code";
                        }
                    }
                }
                else
                {
                    resultArgs.Message = "Branch Code is Empty in Master XML";
                }
            }
            // }
            // else
            //  {
            //      resultArgs.Message = "Branch Code is empty in License XML";
            //  }
            // }
            return resultArgs;
        }


        public ResultArgs SplitProjectValidateLicenseBranchCode(DataSet dsMasters)
        {
            string LicenseBranchCode = string.Empty;
            string MasterBranchCode = string.Empty;
            //resultArgs = GetLicenseBranchCode();
            //if (resultArgs.Success)
            // {
            //if (resultArgs.ReturnValue != null)
            //{
            LicenseBranchCode = SettingProperty.branachOfficeCode;    // resultArgs.ReturnValue.ToString();
            //Added by Carmel Raj
            BranchOfficeCode = LicenseBranchCode;

            resultArgs = GetBranchOfficeCode(dsMasters);
            if (resultArgs.Success)
            {
                if ((!string.IsNullOrEmpty(resultArgs.ReturnValue.ToString())) && resultArgs.ReturnValue != null)
                {
                    MasterBranchCode = resultArgs.ReturnValue.ToString();
                    if (!string.IsNullOrEmpty(MasterBranchCode))
                    {
                        bool isBranchCodeValid = LicenseBranchCode.Equals(MasterBranchCode);
                        //if (!isBranchCodeValid)
                        //{
                        //    resultArgs.Message = "License Branch Code does not match with the Master XML Branch Code";
                        //}
                    }
                }
                else
                {
                    resultArgs.Message = "Branch Code is Empty in Master XML";
                }
            }
            // }
            // else
            //  {
            //      resultArgs.Message = "Branch Code is empty in License XML";
            //  }
            // }
            return resultArgs;
        }

        public ResultArgs ValidateLicenseInformation(DataSet dsMasters)
        {
            try
            {
                resultArgs = ValidateLicenseBranchCode(dsMasters);
                if (resultArgs.Success)
                {
                    resultArgs = ValidateLicenseHeadOfficeCode(dsMasters);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Exception in Validating License Details. " + ex.ToString();
            }

            return resultArgs;
        }


        /// <summary>
        /// On 08/01/2022, To find given value in the datatable since specail characters may give problem in default Rowfilter
        /// it will return mapped row, otherwise will return null
        /// </summary>
        /// <returns></returns>
        public ResultArgs CheckValueCotainsInDataTable(DataTable dtSource, string sourcefldname, string svalue)
        {
            ResultArgs result = new ResultArgs();
            try
            {
                var varResult = (from matchedrow in dtSource.AsEnumerable()
                                 where matchedrow[sourcefldname].ToString() == svalue
                                 select matchedrow);
                if (varResult != null && varResult.Count() > 0)
                {
                    DataTable dtResult = varResult.CopyToDataTable();
                    result.DataSource.Data = dtResult;
                }
                else
                {
                    result.DataSource.Data = null;
                }
                result.Success = true;
            }
            catch (Exception err)
            {
                result.Message = err.Message;
            }

            return result;
        }

        /// <summary>
        /// This method is used to read brach code from given dataset (Master or Voucher)
        /// </summary>
        /// <param name="dsBranch"></param>
        /// <returns></returns>
        public ResultArgs GetBranchOfficeCode(DataSet dsBranch)
        {
            try
            {
                if (dsBranch != null && dsBranch.Tables.Count > 0)
                {
                    DataTable dtBranchCode = dsBranch.Tables["Header"];
                    if (dtBranchCode != null && dtBranchCode.Rows.Count > 0)
                    {
                        DataRow drBranchCode = dtBranchCode.Rows[0];
                        if (!string.IsNullOrEmpty(drBranchCode["BRANCH_OFFICE_CODE"].ToString()))
                        {
                            resultArgs = DecreptWithResultArg(drBranchCode["BRANCH_OFFICE_CODE"].ToString());
                        }
                        else
                        {
                            resultArgs.Message = "Branch Office Code is Empty";
                        }
                    }
                    else
                    {
                        resultArgs.Message = "Branch information (Branch Code, Head Office Code) is empty.";
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = "Problem in Getting Branch Office Code " + ex.ToString();
            }
            return resultArgs;
        }

        public ResultArgs GetHeadOfficeCode(DataSet dsBranch)
        {
            try
            {
                if (dsBranch != null && dsBranch.Tables.Count > 0)
                {
                    DataTable dtHeadOfficeCode = dsBranch.Tables["Header"];
                    if (dtHeadOfficeCode != null && dtHeadOfficeCode.Rows.Count > 0)
                    {
                        DataRow drBranchCode = dtHeadOfficeCode.Rows[0];
                        if (!string.IsNullOrEmpty(drBranchCode["HEAD_OFFICE_CODE"].ToString()))
                        {
                            resultArgs = DecreptWithResultArg(drBranchCode["HEAD_OFFICE_CODE"].ToString());
                        }
                        else
                        {
                            resultArgs.Message = "Branch Information ( Head Office Code) is Empty";
                        }
                    }
                    else
                    {
                        resultArgs.Message = "Branch Information ( Head Office Code) is Empty";
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        //public ResultArgs GetLicenseBranchCode()
        //{
        //    ResultArgs resultArgs = new ResultArgs();
        //    DataTable dtLicenseInfo = new DataTable();
        //    try
        //    {
        //        if (File.Exists(LICENSE_FILENAME))
        //        {
        //            DataSet dsLicenseInfo = XMLConverter.ConvertXMLToDataSet(LICENSE_FILENAME);
        //            if (dsLicenseInfo != null && dsLicenseInfo.Tables.Count > 0)
        //            {
        //                dtLicenseInfo = dsLicenseInfo.Tables["LicenseKey"];
        //                if (dtLicenseInfo != null && dtLicenseInfo.Rows.Count > 0)
        //                {
        //                    DataRow drBranchofficeCode = dtLicenseInfo.Rows[0];
        //                    if (!string.IsNullOrEmpty(drBranchofficeCode["BRANCH_OFFICE_CODE"].ToString()))
        //                    {
        //                        resultArgs = DecreptWithResultArg(drBranchofficeCode["BRANCH_OFFICE_CODE"].ToString());
        //                    }
        //                    else
        //                    {
        //                        resultArgs.Message = "Error in GetBranchOfficeCode: Branch Office Code is Empty in License File.";
        //                    }
        //                }
        //                else
        //                {
        //                    resultArgs.Message = "Error in GetBranchOfficeCode: Branch Office Code is not Found in the License File.";
        //                }
        //            }
        //        }
        //        else
        //        {
        //            resultArgs.Message = "License file not found in Branch.";
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        resultArgs.Message = ex.ToString();
        //    }
        //    finally { }
        //    return resultArgs;
        //}

        //public ResultArgs GetLicenseHeadofficeCode()
        //{
        //    ResultArgs resultArgs = new ResultArgs();
        //    DataTable dtLicenseInfo = new DataTable();
        //    try
        //    {
        //        if (File.Exists(LICENSE_FILENAME))
        //        {
        //            DataSet dsLicenseInfo = XMLConverter.ConvertXMLToDataSet(LICENSE_FILENAME);
        //            if (dsLicenseInfo != null && dsLicenseInfo.Tables.Count > 0)
        //            {
        //                dtLicenseInfo = dsLicenseInfo.Tables["LicenseKey"];
        //                DataRow drHeadofficeCode = dtLicenseInfo.Rows[0];
        //                if (!string.IsNullOrEmpty(drHeadofficeCode["HEAD_OFFICE_CODE"].ToString()))
        //                {
        //                    resultArgs = DecreptWithResultArg(drHeadofficeCode["HEAD_OFFICE_CODE"].ToString());
        //                }
        //                else
        //                {
        //                    resultArgs.Message = "Error in GetHeadOfficeCode: Head Office Code is Empty in License File.";
        //                }
        //            }
        //            else
        //            {
        //                resultArgs.Message = "Error in GetHeadOfficeCode: Head Office Code is not Found in the License File.";
        //            }
        //        }
        //        else
        //        {
        //            resultArgs.Message = "License file not found in Branch.";
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //        resultArgs.Message = ex.ToString();
        //    }
        //    finally { }
        //    return resultArgs;
        //}

        //private string GetLicBranchCode()
        //{
        //    DataTable dtLicenseInfo = new DataTable();
        //    string BranchOfficeCode = string.Empty;
        //    try
        //    {
        //        // this is to get the Path when run Solution all
        //        string fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), LICENSE_FILENAME);
        //        // this is to get the licence filepath from ACPP
        //        //string fileName = Path.GetDirectoryName(@"D:\\ACPERP\\Win\\AcMEERPLicense.xml");
        //        //fileName = fileName + "\\" + LICENSE_FILENAME;
        //        if (File.Exists(fileName))
        //        {
        //            DataSet dsLicenseInfo = XMLConverter.ConvertXMLToDataSet(fileName);
        //            if (dsLicenseInfo != null && dsLicenseInfo.Tables.Count > 0)
        //            {
        //                dtLicenseInfo = dsLicenseInfo.Tables["LicenseKey"];
        //                if (dtLicenseInfo != null)
        //                {
        //                    DataRow drLicense = dtLicenseInfo.Rows[0];
        //                    BranchOfficeCode = drLicense["BRANCH_OFFICE_CODE"].ToString();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        resultArgs.Message = ex.ToString();
        //    }
        //    finally { }
        //    return BranchOfficeCode;
        //}

        //private string GetLicHeadCode()
        //{
        //    DataTable dtLicenseInfo = new DataTable();
        //    string BranchOfficeHeadCode = string.Empty;
        //    try
        //    {
        //        // this is to get the Path when run Solution all
        //        string fileName = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), LICENSE_FILENAME);
        //        // this is to get the licence filepath from ACPP
        //        //string fileName = Path.GetDirectoryName(@"D:\\ACPERP\\Win\\AcMEERPLicense.xml");
        //        //fileName = fileName + "\\" + LICENSE_FILENAME;
        //        if (File.Exists(fileName))
        //        {
        //            DataSet dsLicenseInfo = XMLConverter.ConvertXMLToDataSet(fileName);
        //            if (dsLicenseInfo != null && dsLicenseInfo.Tables.Count > 0)
        //            {
        //                dtLicenseInfo = dsLicenseInfo.Tables["LicenseKey"];
        //                if (dtLicenseInfo != null)
        //                {
        //                    DataRow drLicense = dtLicenseInfo.Rows[0];
        //                    BranchOfficeHeadCode = drLicense["HEAD_OFFICE_CODE"].ToString();
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        resultArgs.Message = ex.ToString();
        //    }
        //    finally { }
        //    return BranchOfficeHeadCode;
        //}

        /// <summary>
        /// This method compress Dataset to byte data.
        /// </summary>
        /// <param name="ds"></param>
        /// <returns></returns>
        public static byte[] CompressData(DataSet ds)
        {
            byte[] data;
            try
            {
                using (MemoryStream memStream = new MemoryStream())
                {
                    GZipStream zipStream = new GZipStream(memStream, CompressionMode.Compress);
                    ds.WriteXml(zipStream, XmlWriteMode.WriteSchema);
                    zipStream.Close();
                    data = memStream.ToArray();
                    memStream.Close();
                }
            }
            catch (Exception ex)
            {
                data = null;
                MessageRender.ShowMessage(ex.ToString());
            }
            return data;
        }

        public static string CalculateTaxRate(DataTable dtTaxRate, double AssessAmout, TaxPolicyId tdsPolicy)
        {
            SumofTaxAmount = 0;
            string TaxAmount = string.Empty;
            try
            {
                if (dtTaxRate != null && dtTaxRate.Rows.Count > (int)YesNo.No)
                {
                    DataView dvTaxRate = dtTaxRate.DefaultView;
                    dvTaxRate.Sort = "TDS_TAX_TYPE_ID ASC";

                    if (tdsPolicy.Equals(TaxPolicyId.TDSWithoutPAN))
                    {
                        dvTaxRate.RowFilter = " TDS_TAX_TYPE_ID IN(2)";
                    }
                    foreach (DataRow dr in dvTaxRate.ToTable().Rows)
                    {
                        int TaxType = dr["TDS_TAX_TYPE_ID"] != DBNull.Value ? new NumberSetMember().ToInteger(dr["TDS_TAX_TYPE_ID"].ToString()) : 0;
                        double TaxRate = dr["TDS_RATE"] != DBNull.Value ? new NumberSetMember().ToDouble(dr["TDS_RATE"].ToString()) : 0;
                        double TDSExemptionLimit = dr["TDS_EXEMPTION_LIMIT"] != DBNull.Value ? new NumberSetMember().ToDouble(dr["TDS_EXEMPTION_LIMIT"].ToString()) : 0;
                        TaxAmount = CalTaxAmount(TaxType, AssessAmout, TaxRate, TDSExemptionLimit);
                    }
                    TaxAmount += Environment.NewLine;
                    TaxAmount += String.Format("Payable to TDS   = {0} {1}", new NumberSetMember().ToNumber(SumofTaxAmount), Environment.NewLine);
                    TaxAmount += String.Format("Payable to Party = {0} {1}", new NumberSetMember().ToNumber(AssessAmout - SumofTaxAmount), Environment.NewLine);
                    string NetPayAmount = new NumberSetMember().ToNumber(AssessAmout - SumofTaxAmount);
                    NetTdsAmount = new NumberSetMember().ToDouble(NetPayAmount);
                    dvTaxRate.RowFilter = "";
                }
                TaxDescription = string.Empty;
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
            return TaxAmount;
        }

        private static string CalTaxAmount(int TaxType, double AssessAmount, double TdsTaxRate, double TDSExemptionLimit)
        {
            try
            {
                switch (TaxType)
                {
                    case (int)TaxPolicyId.TDSWithPAN:
                        {
                            //TDSTaxAmount = AssessAmount >= TDSExemptionLimit ? AssessAmount / 100 * TdsTaxRate : 0;
                            TDSTaxAmount = AssessAmount / 100 * TdsTaxRate;
                            TaxDescription += String.Format("1.TDS Tax - {0} x {1}% ={2} {3}", new NumberSetMember().ToNumber(AssessAmount), TdsTaxRate, new NumberSetMember().ToNumber(TDSTaxAmount), Environment.NewLine);
                            SumofTaxAmount = SumofTaxAmount + TDSTaxAmount;
                            break;
                        }
                    case (int)TaxPolicyId.TDSWithoutPAN:
                        {
                            TDSTaxAmount = AssessAmount / 100 * TdsTaxRate;
                            TaxDescription += String.Format("1.TDS Tax - {0} x {1}% ={2} {3}", new NumberSetMember().ToNumber(AssessAmount), TdsTaxRate, new NumberSetMember().ToNumber(TDSTaxAmount), Environment.NewLine);
                            SumofTaxAmount = SumofTaxAmount + TDSTaxAmount;
                            break;
                        }
                    case (int)TaxPolicyId.Surcharge:
                        {
                            EdCessAmount = TDSTaxAmount > AssessAmount ? TDSTaxAmount / 100 * TdsTaxRate : 0;
                            TaxDescription += String.Format("2.Surcharge - {0} x {1}% ={2} {3}", new NumberSetMember().ToNumber(TDSTaxAmount), TdsTaxRate, new NumberSetMember().ToNumber(EdCessAmount), Environment.NewLine);
                            SumofTaxAmount = SumofTaxAmount + EdCessAmount;
                            break;
                        }
                    case (int)TaxPolicyId.EdCess:
                        {
                            SurchargeAmount = TDSTaxAmount > AssessAmount ? TDSTaxAmount / 100 * TdsTaxRate : 0;
                            TaxDescription += String.Format("3.EdCess - {0} x {1}% ={2} {3}", new NumberSetMember().ToNumber(TDSTaxAmount), TdsTaxRate, new NumberSetMember().ToNumber(SurchargeAmount), Environment.NewLine);
                            SumofTaxAmount = SumofTaxAmount + SurchargeAmount;
                            break;
                        }
                    case (int)TaxPolicyId.SecEdCess:
                        {
                            SecEduCessAmount = TDSTaxAmount > AssessAmount ? TDSTaxAmount / 100 * TdsTaxRate : 0;
                            TaxDescription += String.Format("4.Sec Ed Cess - {0} x {1}% ={2} {3}", new NumberSetMember().ToNumber(TDSTaxAmount), TdsTaxRate, new NumberSetMember().ToNumber(SecEduCessAmount), Environment.NewLine);
                            SumofTaxAmount = SumofTaxAmount + SecEduCessAmount;
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
            return TaxDescription;
        }

        /// <summary>
        /// This method is used to get acmeerp installed path if it installed in current system
        /// </summary>
        /// <returns>string</returns>
        public static string GetAcMeERPInstallPath()
        {
            string acmeerpInstallPath = string.Empty;
            try
            {
                //Find installed path in registry
                acmeerpInstallPath = GetRegistryValue(Registry.LocalMachine);
                if (string.IsNullOrEmpty(acmeerpInstallPath))
                {
                    acmeerpInstallPath = GetRegistryValue(Registry.CurrentUser);
                }
            }
            catch (Exception err)
            {
                //WriteLog("Error in GetAcMeERPInstallPath " + err.Message);
            }
            finally
            {
                //If not avilable, fix default path
                if (string.IsNullOrEmpty(acmeerpInstallPath))
                {
                    acmeerpInstallPath = @"C:\Program Files (x86)\BoscoSoft\AcMEERP\";
                    if (!File.Exists(Path.Combine(acmeerpInstallPath, "ACPP.exe")))
                        acmeerpInstallPath = @"C:\Program Files\BoscoSoft\AcMEERP\";

                    if (!Directory.Exists(acmeerpInstallPath))
                    {
                        acmeerpInstallPath = string.Empty;
                    }
                }
            }
            return acmeerpInstallPath;
        }

        private static string GetRegistryValue(RegistryKey regkey)
        {
            string Rtn = string.Empty;
            RegistryKey root = regkey.OpenSubKey(RegistryPath, false);
            try
            {
                if (root != null)
                {
                    if (root.GetValue("AcMEERPPath") != null)
                    {
                        Rtn = root.GetValue("AcMEERPPath").ToString();
                    }
                }
            }
            catch (Exception err)
            {
                Rtn = string.Empty;
                //WriteLog("Error in GetRegistryValue " + err.Message);
            }

            return Rtn;
        }

        /// <summary>
        /// To check the internet Connection 
        /// </summary>
        /// <returns></returns>
        public static bool CheckForInternetConnection()
        {
            try
            {
                using (var client = new WebClient())
                using (var stream = client.OpenRead("http://www.acmeerp.org"))
                {
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// This method is used to compress given file into given path
        /// Compress the sql file into gz file.
        /// </summary>
        /// <param name="directorySelected"></param>
        public static ResultArgs CompressFile(string OrginalFile, string CompressedFilePath)
        {
            ResultArgs result = new ResultArgs();
            try
            {

                if (File.Exists(OrginalFile))
                {
                    using (FileStream orignalFileStream = new FileStream(OrginalFile, FileMode.Open))
                    {
                        using (FileStream CompressedFileStream = File.Create(CompressedFilePath))
                        {
                            using (GZipStream compressionStream = new GZipStream(CompressedFileStream, CompressionMode.Compress))
                            {
                                orignalFileStream.CopyTo(compressionStream);
                                result.Success = true;
                            }
                        }
                    }
                }
                else
                {
                    result.Message=  "Could not compress the file, file is not found";
                }
            }
            catch (Exception ex)
            {
                result.Message = "Could not compress file " + ex.Message;
            }
            finally { }
            return result;
        }

        public static string RemoveSpecialCharacter(string RemoveCharacter)
        {
            try
            {
                if (!string.IsNullOrEmpty(RemoveCharacter))
                {
                    RemoveCharacter = Regex.Replace(RemoveCharacter, @"[^0-9a-zA-Z]+", " ");
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
            return RemoveCharacter;
        }

        #region Stock Net Amount Calculation


        public static double calculateStockNetPayment(double ActualAmount, double DiscountPer)
        {
            return calculateStockNetPayment(ActualAmount, DiscountPer, 0, 0, 0, 0);
        }

        public static double calculateStockNetPayment(double ActualAmount, double DiscountPer, double Discount)
        {
            return calculateStockNetPayment(ActualAmount, DiscountPer, Discount, 0, 0, 0);
        }

        public static double calculateStockNetPayment(double ActualAmount, double DiscountPer, double Discount, double OtherCharges)
        {
            return calculateStockNetPayment(ActualAmount, DiscountPer, Discount, OtherCharges, 0, 0);
        }

        public static double calculateStockNetPayment(double ActualAmount, double DiscountPer, double Discount, double OtherCharges, double TaxRate)
        {
            return calculateStockNetPayment(ActualAmount, DiscountPer, Discount, OtherCharges, TaxRate, 0);
        }

        public static double calculateStockNetPayment(double ActualAmount, double DiscountPer, double Discount, double OtherCharges, double TaxRate, double TaxAmount)
        {
            try
            {
                if (ActualAmount > 0)
                {
                    if (Discount > 0)
                    {
                        CalCuDiscountAmount = Discount;
                        CalCuDiscountPer = (CalCuDiscountAmount / ActualAmount) * 100;
                    }
                    else
                    {
                        CalCuDiscountAmount = ActualAmount * DiscountPer / 100;
                        CalCuDiscountPer = DiscountPer;
                    }

                    ActualAmount = ActualAmount - Discount;

                    ActualAmount = ActualAmount + OtherCharges;

                    if (TaxAmount > 0)
                    {
                        CalCuTaxAmount = TaxAmount;
                        CalCuTaxRate = (CalCuTaxAmount / ActualAmount) * 100;
                    }
                    else
                    {
                        CalCuTaxAmount = ActualAmount * TaxRate / 100;
                    }
                    NetAmout = ActualAmount + CalCuTaxAmount;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
            return NetAmout;
        }


        #endregion


        #region Asset NetAmount Calculation
        public static double calculateAssetNetPayment(double ActualAmount, double Discount)
        {
            return calculateAssetNetPayment(ActualAmount, Discount, 0, 0);
        }

        public static double calculateAssetNetPayment(double ActualAmount, double Discount, double TaxAmount)
        {
            return calculateAssetNetPayment(ActualAmount, Discount, TaxAmount, 0);
        }

        public static double calculateAssetNetPayment(double ActualAmount, double Discount, double TaxAmount, double OtherCharges)
        {
            try
            {
                NetAmout = 0;
                if (ActualAmount > 0)
                {
                    ActualAmount = ActualAmount - Discount;
                    ActualAmount = ActualAmount + OtherCharges;
                    NetAmout = ActualAmount + TaxAmount;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
            return NetAmout;
        }

        public static double CalculatePerCent(double actualAmount, double totalAmount)
        {
            double totalPercent = 0;
            if (actualAmount > 0)
            {
                totalPercent = (totalAmount / actualAmount) * 100;
            }
            return totalPercent;
        }

        public static double CalculateAmount(double actualAmount, double totalPercentage)
        {
            double totalAmount = 0;
            if (actualAmount > 0)
            {
                totalAmount = actualAmount * totalPercentage / 100;
            }
            return totalAmount;
        }

        #endregion

        #region Connectionstring Encryption/Decryption in Appconfig

        /// <summary>
        /// This method is used to encrypt connectionstring section in the application config
        /// </summary>
        /// <param name="configfile">config file path </param>
        /// <param name="section">Section to encrypt</param>
        public static ResultArgs EncryptConnectionSettings(string configfile, string section)
        {
            ResultArgs resultArg = new ResultArgs();
            try
            {
                ExeConfigurationFileMap configFile = new ExeConfigurationFileMap(configfile);
                configFile.ExeConfigFilename = configfile;
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFile, ConfigurationUserLevel.None);
                ConnectionStringsSection objconnectionstring = (ConnectionStringsSection)config.GetSection(section);
                if (!objconnectionstring.SectionInformation.IsProtected)
                {
                    objconnectionstring.SectionInformation.ProtectSection("RsaProtectedConfigurationProvider");
                    objconnectionstring.SectionInformation.ForceSave = true;
                    config.Save(ConfigurationSaveMode.Modified);
                    resultArg.Success = true;
                }
            }
            catch (Exception err)
            {
                resultArg.Message = "EncryptConnectionSettings " + err.Message;
            }
            return resultArg;
        }

        /// <summary>
        /// This method is used to decrypt connectionstring section of the application config
        /// </summary>
        /// <param name="configfile">Path of the config file</param>
        /// <param name="section">Section to encrypt</param>
        public static ResultArgs DecryptConnectionSettings(string configfile, string section)
        {
            ResultArgs resultArg = new ResultArgs();
            try
            {
                ExeConfigurationFileMap configFile = new ExeConfigurationFileMap(configfile);
                configFile.ExeConfigFilename = configfile;
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFile, ConfigurationUserLevel.None);
                ConnectionStringsSection objconnectionstring = (ConnectionStringsSection)config.GetSection(section);
                if (objconnectionstring.SectionInformation.IsProtected)
                {
                    objconnectionstring.SectionInformation.UnprotectSection();
                    objconnectionstring.SectionInformation.ForceSave = true;
                    config.Save(ConfigurationSaveMode.Modified);
                    resultArg.Success = true;
                }
            }
            catch (Exception err)
            {
                resultArg.Message = "DecryptConnectionSettings " + err.Message;
            }
            return resultArg;
        }

        /// <summary>
        /// This method is used to update the connectionstring value in config file
        /// </summary>
        /// <param name="keyname">name of the key</param>
        /// <param name="keyvalue">valye of the key</param>
        public static ResultArgs UpdateConnectionString(string configfile, string keyname, string keyvalue)
        {
            ResultArgs resultArg = new ResultArgs();
            try
            {
                ExeConfigurationFileMap configFile = new ExeConfigurationFileMap(configfile);
                configFile.ExeConfigFilename = configfile;
                Configuration config = ConfigurationManager.OpenMappedExeConfiguration(configFile, ConfigurationUserLevel.None);

                if (config != null)
                {
                    ConnectionStringsSection oSection = config.GetSection("connectionStrings") as ConnectionStringsSection;
                    if (oSection != null)
                    {
                        oSection.ConnectionStrings[keyname].ConnectionString = keyvalue;
                        config.Save(ConfigurationSaveMode.Modified);
                        resultArg.Success = true;
                    }
                }
            }
            catch (Exception err)
            {
                resultArg.Message = "UpdateConnectionString " + err.Message;
            }
            return resultArg;
        }

        #endregion

        #region ValidateLicensePeriod
        public static bool ValidateLicensePeriod(DateTime VoucherDate,string DateFrom,string DateTo)
        {
            bool Rtn = true;
            SimpleEncrypt.SimpleEncDec objDec = new SimpleEncrypt.SimpleEncDec();
            //License expiry will be validated when License Model is YES
            if (SettingProperty.Current.IsLicenseModel == 1)
            {
                //17/04/2017, Voucher Date should be less than or equal to Lincense To date, User can enter vouchers for prevuious years toooo
                if (SettingProperty.Current.BranchOfficeCode.ToUpper() == "BOSCOSBOSOFT") //for demo license, it will check only given license period
                {
                    if (VoucherDate >= new DateSetMember().ToDate(DateFrom, false) && VoucherDate <= new DateSetMember().ToDate(DateTo, false))
                    //if (VoucherDate <= new DateSetMember().ToDate(DateTo, false))
                    {
                        Rtn = true;
                    }
                    else
                    {
                        Rtn = false;
                    }
                }
                else
                {
                    //if (VoucherDate >= new DateSetMember().ToDate(DateFrom, false) && VoucherDate <= new DateSetMember().ToDate(DateTo, false))
                    if (VoucherDate <= new DateSetMember().ToDate(DateTo, false))
                    {
                        Rtn = true;
                    }
                    else
                    {
                        Rtn = false;
                    }
                }

                //On 21/10/2023, If posting from other module or third party, let us check voucher date should be with in first fy date from and last fy date to
                if (Rtn)
                {
                    if (!(VoucherDate >= SettingProperty.Current.FirstFYDateFrom && VoucherDate <= SettingProperty.Current.LastFYDateTo))
                    {
                        Rtn = false;
                    }
                }


            }
            return Rtn;
        }
        #endregion

        #region IDisposable Members

        public virtual void Dispose()
        {
            GC.Collect();
        }
        #endregion

        #region AddEmptyRowForMultiCahBankBookReport
        public DataTable AddEmptyHeaderColumn(DataTable dtSource, string ValueField, string DisplayField, string HeaderCaption = "")
        {
            DataRow dr = null;
            if (dtSource.Columns.Contains(ValueField) && dtSource.Columns.Contains(DisplayField))
            {
                dr = dtSource.NewRow();
                dr[ValueField] = 0;
                dr[DisplayField] = HeaderCaption;
                dtSource.Rows.InsertAt(dr, 0);
            }
            return dtSource;
        }
        #endregion
        
        public ResultArgs FetchLegalEntityProperties()
        {
            ResultArgs result = new ResultArgs();
            try
            {
                //DataTable dtLedgalEntityProperties = AppSchema.LegalEntity.DefaultView.Table;
                DataTable dtLegalEntityProperties = new DataTable("LegalEntityProperties");
                dtLegalEntityProperties.Columns.Add("LEGALENTITY_FIELD_NAME", typeof(System.String));
                dtLegalEntityProperties.Columns.Add("LEGALENTITY_DISPLAY_NAME", typeof(System.String));
                dtLegalEntityProperties.Rows.Add(new object[] { "REGNO", "Registration No" });
                dtLegalEntityProperties.Rows.Add(new object[] { "REGDATE", "Registration Date" });
                dtLegalEntityProperties.Rows.Add(new object[] { "PANNO", "PAN No" });
                dtLegalEntityProperties.Rows.Add(new object[] { "EIGHTYGNO", "80G No" });
                dtLegalEntityProperties.Rows.Add(new object[] { "EIGHTY_GNO_REG_DATE", "80G Date" });
                dtLegalEntityProperties.Rows.Add(new object[] { "FCRINO", "FCRA No" });
                dtLegalEntityProperties.Rows.Add(new object[] { "FCRIREGDATE", "FCRA Registration Date" });
                result.Success = true;
                result.DataSource.Data = dtLegalEntityProperties;
                //chkListShowLegalDetails.Properties.DataSource = dtLegalEntityProperties;
                //chkListShowLegalDetails.Properties.ValueMember = "LEGALENTITY_FIELD_NAME";
                //chkListShowLegalDetails.Properties.DisplayMember = "LEGALENTITY_DISPLAY_NAME";
            }
            catch (Exception err)
            {
                result.Message = err.Message;
            }
            return result;
        }

        #endregion


        void IDisposable.Dispose()
        {
            GC.Collect();
        }
    }

    class TreeListFindOperation : TreeListOperation
    {
        string val;
        public TreeListFindOperation(string value)
        {
            val = value;
        }

        public override void Execute(DevExpress.XtraTreeList.Nodes.TreeListNode node)
        {
            if (node.GetValue("ID").ToString() == val)
            {
                myNode = node;
            }
        }
        // Fields...
        private TreeListNode myNode;

        public TreeListNode MyNode
        {
            get { return myNode; }
            set { myNode = value; }
        }
    }
}
