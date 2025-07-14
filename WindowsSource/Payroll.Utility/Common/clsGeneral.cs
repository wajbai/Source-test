using System;
 
using System.Data;
using System.IO;
using System.Data.OleDb;
 
using System.Windows.Forms;
using System.Globalization;
using System.Collections;
using System.Drawing;
using System.ComponentModel;
using Microsoft.Office.Interop;
using Payroll.Utility.Validations;
using Payroll.Utility.CommonMemberSet;
//using Payroll.DAO.Data;
//using Payroll.DAO.MySQL;
//using Payroll.DAO.Schema;
 

namespace Payroll.Utility.Common
{
	public class clsGeneral  
	{
		public clsGeneral()
		{

		}
        
		public static IFormatProvider DATE_FORMAT		= new CultureInfo("en-GB", true);

		public const int DEPARTMENT_REGISTRATION	= 1;
		public const int DEPARTMENT_STORE			= 2;
		public const int DEPARTMENT_PHARMACY		= 3;
		public const int DEPARTMENT_WARD			= 4;
		public const int DEPARTMENT_BILLING			= 5;
		public const int DEPARTMENT_ADMIN			= 6;
		public const int DEPARTMENT_LAB				= 7;
		public const int DEPARTMENT_OTHER			= 8;
		public const int DEPARTMENT_RECORDING		= 9;
		

		/* -----------------------------------------------------
		 * Purpose	: Constants to retrieve the General Queries
		 * */
		public const int USERLIST					= 101;
		public const int DEPTLIST					= 102;
		public const int PHARMACYDAYCOLLECTION		= 103;
		public const int LABBILLDAYCOLLECTION		= 104;
		public const int CONCESSIONHEADS			= 105;
		public const int LAB_DEPTLIST				= 106;
		public const int GENERAL_CHRG_DAYCOLLECTION	= 107;
		public const int ADVANCE_DAYCOLLECTION		= 108;
		public const int IPBILL_DAYCOLLECTION		= 109;
		public const int GENERAL_CHRG_DEBTLIST		= 110;
		public const int PHARMACY_ALL_COLLECTION	= 111;
		public const int IPBILL_ADVANCE_PAYMENT		= 112;
		public const int IPBILL_ALLCOLLECTION		= 113;
		public const int DOCTORS_SHIFT				= 114;
		// -----------------------------------------------------

		//---------- USER LEVEL --------------------------------
		public const int SUPERVISOR	= 0;
		public const int OPERATOR	= 1;
		//------------------------------------------------------
		

		//-------Dues-------------------------------------------
		public const int REGISTRATION_DUE	= 1;
		public const int PHARMACY_DUE		= 2;
		public const int LABORATORY_DUE		= 3;
		public const int WARD_DUE			= 4;
		//------------------------------------------------------
		
		public static int USER_ID				= 0;
		public static int USER_LEVEL			= 0;
		public static string USER_NAME			= "";
		public static string RPT_USER_NAME		= "";
		public static string USER_DEPARTMENT    = "";
		public static string DB_SERVICE_NAME	= "";
		public static string DB_USER_NAME		= "";
		public static string DB_PASSWORD		= "";
		public static int SHOW_ADV_ADJ_IN_PAYMENT = 0;
		public static string STORE_NAME				= "";
		public static string PHARMACY_NAME			= "";
		public static string BILLINGSECTION_NAME	= "";
		public static string SAMPLE_NO_CAPTION		= "";

		public static string IS_REPORTS_DEPT_WISE       ="";

		// --- JG on 15-June-2006 --------------
		public const int NON_MEDICAL_ITEMS	= 0;
		public const int MEDICAL_ITEMS		= 1;
		public const int BOTH_ITEMS			= 2;
		// -------------------------------------
		//for payroll -GP on 20-Feb-2006
		public static string PAYROLL_MONTH="";
		public static long   PAYROLL_ID	  =0;
		public static string PAYROLL_RPT_COL_TOTAL_FIELDS = "";
		public static string PAYROLLDATE  ="";
		public static int PAYROLL_EDIT_COUNTER = 0;
		public static string PAYROLL_REPORT_INPUT_FILE= "Payroll_Report_Input.txt";
		public static bool   PAYROLL_CUSTOMIZED_REPORT = false;
				
		public const int LKP_DATE			= 100;
		public const int LKP_WARD			= 101;
		public const int LKP_ROOM			= 102;
		public const int LKP_VENDOR			= 103;
		public const int LKP_ITEMTYPE		= 104;
		public const int LKP_ITEMNAME		= 105;
		public const int LKP_EMP			= 106;
		public const int LKP_BATCH			= 107;
		public const int LKP_ROLE			= 108;
		public const int LKP_ACCOUNT_GROUP	= 109;
		public const int LKP_INVOICE		= 110;
		public const int LKP_TESTCODE		= 111;
		public const int LKP_LABCODE		= 112;
		public const int LKP_BATCHNUMBER	= 113;
		public const int LKP_ITEMDESC		= 114;
		public const int LKP_DEFAULTCHARGE	= 115;
		public const int LKP_RA_ITEMTYPE	= 116;		//Ward Drugs Reallocation Item Type
		public const int LKP_RA_ITEMNAME	= 117;
		public const int LKP_RA_BATCH		= 118;
		public const int LKP_RA_REGNO		= 119;
		public const int LKP_RD_ITEMTYPE	= 120;		//Lookup constants for Return Drug by Ward Patient
		public const int LKP_RD_ITEMNAME	= 121;
		public const int LKP_RD_BATCH		= 122;
		public const int LKP_ALL_ITEM_NAME	= 123;

		public const int LKP_ORDERVENDORITEM = 201;
		public const int LKP_DRUGISSUEITEM	 = 202;
		public const int LKP_VISITNUMBER	 = 203;

		public const int LKP_DEPT_TYPE		 = 204;
		public const int LKP_DEPT_ITEM		 = 205;
		public const int LKP_DEPT_BATCH		 = 206;
		public const int LKP_ORDER_NUMBER	 = 207;
		public const int LKP_GRN_NUMBER		 = 208;

		//Department _ Replacement		
		public const int LKP_ISSUE_BATCH	= 209;
		public const int LKP_ISSUE_TYPE		= 210;
		public const int LKP_ISSUE_ITEM		= 211;
		public const int LKP_ISSUE_ITEM_CREATE		= 212;
		
		public const int LKP_VISIT_DETAIL	 = 251;
		public const int LKP_REG_NUMBER		 = 252;
		public const int LKP_DEPARTMENT		 = 253;
		public const int LKP_CASE			 = 254;

		public const int LKP_ACCOUNTHEADLIST = 301;
		public const int LKP_ACCOUNTHEADS	 = 302;
		public const int LKP_ALLACCOUNTHEADS = 303;

		public const int LKP_RETURN_ITEM		=	400;
		public const int LKP_RETURN_BATCH		=	401;
		public const int LKP_RETURN_INVOICE		=	402;
		public const int LKP_DOCTOR_ACCOUNT_HEADS = 403;
		public const int LKP_LAB_REPORT_ACCOUNT_HEADS = 404;
		public const int LKP_LAB_DOCTOR			= 405;
		public const int LKP_BILL_DOCTOR		= 406;
		public const int LKP_OPER_DOCTOR		= 407;

		public const int LKP_LAB_RESULTS		= 451;
		public const int LKP_LAB_REMARK			= 452;

		public const int LKP_WRD_PAT_REGNO		= 601;
		public const int LKP_WRD_PAT_DRG_TYPE	= 602;
		public const int LKP_WRD_PAT_DRG_NAME	= 603;
		public const int LKP_WRD_PAT_BATCHNO	= 604;
		public const int LKP_PHAR_ITEMDESC		= 605;        //Jeya on 20-04-06
		public const int LKP_OPERATION			= 606;

		//Constans for General Procedures
		public const int LKP_GENERAL_CHARGES	= 651;

		// -- To position the drug in the Rack & Shelfs -------
		public const int LKP_DRUGTYPE			= 701;
		public const int LKP_DRUGNAME			= 702;

		// -- To Reposition the drugs in the Rack & Shelfs ----
		public const int LKP_REPOSITIONDRUG		= 703;
		public const int LKP_REPOS_PHA_BACH_NUM	= 704;
		public const int LKP_REPOS_STO_BACH_NUM	= 705;

		public const int LKP_DOCTOR_FEE_CATEGORY	= 706;

		public const int LKP_SPECIAL_PROCEDURE		= 707;
		public const int LKP_DOCTOR_VISIT_TIME		= 708;

		public const int LKP_WARD_ROOM_BED          = 800; //Included by Muthu on 14/04/2007
		
		public enum COSTCENTER
		{
			LKP_LAB_PAYMENT = 709,
			LKP_WRD_COST_CENTER = 710
		}
		
		public const int LKP_VISIT_DOCTOR	= 711;
		public const int LKP_VISIT_TYPE		= 712;
		public const int LKP_WARD_CHARGES	= 713;
		public const int LKP_OPR_CHARGES	= 714;
		public const int LKP_OPR_COST_CENT	= 715;

		public const Keys LKP_LOOKUP_KEY	=	Keys.F3;
		public const Keys LKP_SAVE_KEY		=	Keys.F8;

		// STATUS==========================================================================
		public const string sSTATUS_PREPARED			= "PREPARED";
		public const string sSTATUS_ORDERED				= "ORDERED";
		public const string sSTATUS_ISSUE_ACCEPTED		= "ISSUE_ACCEPTED";
		public const string sSTATUS_ISSUE_NOT_ACCEPTED	= "ISSUE_NOT_ACCEPTED";
		public const string sSTATUS_REJECTED			= "REJECTED";
		public const string sSTATUS_CANCELLED			= "CANCELLED";

		public const int nSTATUS_PREPARED			= 1;
		public const int nSTATUS_ISSUE_NOT_ACCEPTED	= 2;
		public const int nSTATUS_ORDERED			= 2;
		public const int nSTATUS_ISSUE_ACCEPTED		= 3;
		public const int nSTATUS_REJECTED			= 4;
		public const int nSTATUS_CANCELLED			= 5;
		//========================================================================================

		// RETURN DRUG STATUS=====================================================================
		public const string sRTN_STATUS_RETURNED	= "RETURNED";
		public const string sRTN_STATUS_ACCEPTED	= "ACCEPTED";
		public const string sRTN_STATUS_REJECTED	= "REJECTED";
		public const string sRTN_STATUS_CANCELED	= "CANCELED";

		public const int nRTN_STATUS_RETURNED	= 1;
		public const int nRTN_STATUS_ACCEPTED	= 2;
		public const int nRTN_STATUS_REJECTED	= 3;
		public const int nRTN_STATUS_CANCELED	= 4;
		// =======================================================================================

		// TRANS TYPE ============================================================================
		public const string sTRANS_REGISTRATION="REGISTRATION";
		public const string sTRANS_PHARMACY="PHARMACY";
		public const string sTRANS_LAB="LAB";
		public const string sTRANS_WARD="WARD";

		public const int nTRANS_REGISTRATION=1;
		public const int nTRANS_PHARMACY=3;
		public const int nTRANS_LAB=7;
		public const int nTRANS_WARD=4;
		//========================================================================================

		// CONSTANTS FOR UPDATING THE SELLING PRICE OF THE DRUGS =================================
		public const int STORE_PHARMACY_DEPT	= 1;
		public const int UPDATE_DRUGS_BATCH_SP	= 2;
		public const int DEPARTMENT_TYPE		= 3;
		public const int DRUG_TYPE				= 4;
		public const int DRUG_NAME				= 5;
		public const int STORE_BATCH_NUMBER		= 6;
		public const int PHARMACY_BATCH_NUMBER	= 7;
		public const int UPDATE_DRUGS_SP		= 8;
		public const int LKP_MULTIPLE_BATCH_NO	= 9;
		public const int STORE_DRUGS_BATCH_NO	= 10;
		public const int PHARMACY_DRUGS_BATCH_NO= 11;
		//========================================================================================
		
		public const string DAY_BEGINING_TIME = "12:00:00 AM";
		public const string DAY_ENDING_TIME   = "11:59:00 PM";

		public static int IP_BILL_PRINT_TYPE = 1;
		public static bool CHOOSE_PRINTER = false;
		public static string PRINTER_NAME = "";
        public static string EMP_No = string.Empty;

		public static long DATA_CLEANING_RECORD_BULK  = 50000;  //This is to overcome memory over flow error when filling dataset more than half(.5) GB of record
		public static long MAX_RECORD_IN_ACCESS_TABLE = 500000; //This is to overcome the memory over flow error in access because access table support only 2GB of record
		public static int  MAX_FIELD_SIZE_IN_ACCESS	  = 150;    //This defines the maximum field size if field size gets increased this should be modified

		public enum ISSUESTATUS
		{
			PREPARED			= 1,
			ISSUE_NOT_ACCEPTED	= 2,
			ISSUE_ACCEPTED		= 3,
			REJECTED			= 4,
			CANCELLED			= 5
		}
        public enum LeavingReason
        {
            Retired =1,
            Resigned=2,
            StudyLeave =3
        }
        public enum Gender
        {
            Male=1,
            Female=2
        }
        public enum Category
        {
            Medical=1,
            NonMedical=2
        }
        public enum IncrementMonth
        {
            January=1,
            February=2,
            March=3,
            April=4,
            May=5,
            June=6,
            July=7,
            August=8,
            September=9,
            October=10,
            November=11,
            December=12
        }
		public static string URINE_TEST_CAPTION = "Urine Test Fee";
		public const string MsgCaption="Payroll";
		public static string CurrentLab = "";

		//Included by Peter on 05-05-2007 (Modified on 06-11-2007)
		public static Excel.ApplicationClass objExcel;

		private int iNoOfIPRegistrationRep = 0;
		private int iNoOfOPRegistrationRep = 0;
		private int iNoOfPharmaciesRep = 0;
		private int iNoOfStoresRep = 0;
		private int iNoOfLabsRep = 0;
		private int iNoOfBillingRep = 0;
		private int iNoOfReocordingRep = 0;
		
		private string[,] strIPRegistrationReports;
		private string[,] strOPRegistrationReports;
		private string[,] strStoresReports;
		private string[,] strPharmaciesReports;
		private string[,] strLabsReports;
		private string[,] strBillingReports;
		private string[,] strRecordingReports;
		private string sLogFileName = "";
		private static string [] Ones = {"","One","Two","Three","Tour","Five","Six","Seven","Eight","Nine",
							 "Ten", "Eleven","Twelve","Thirteen","Fourteen","Fifteen","Sixteen",
							 "Seventeen","Eighteen","Nineteen"};
		private static string [] Tens = {"Twenty","Thirty","Fourty","Fifty","Sixty","Seventy","Eighty","Ninty"};

		//Added by LJ on 23-03-2009
		//To Encrypt the Password and Storing 
		private static int[] ENCRYPT_KEY = {35,11,22,33,44,55,66,77,88,99,
											   35,11,22,33,44,55,66,77,88,99,
											   35,11,22,33,44,55,66,77,88,99,
											   35,11,22,33,44,55,66,77,88,99,
											   35,11,22,33,44,55,66,77,88,99};
		public string Password;
		public string EncryptedWord;
		#region PasswordEncryptDecrypt

		public static string Encrypt(string word)
		{
			char[] word1 = new char[word.Length];
			char c;
			int d;

			for (int i = 0; i < word.Length; i++)
			{
				c = Convert.ToChar(word.Substring(i, 1));
				d = c - ENCRYPT_KEY[i];
				word1[i] = (Char)d;
			}
			string Eword = new string(word1);
			return Eword;
		}

		public static string Decrypt(string word)
		{

			char[] word1 = new char[word.Length];
			char c;
			int d;

			for (int i = 0; i < word.Length; i++)
			{
				c = Convert.ToChar(word.Substring(i, 1));
				d = c + ENCRYPT_KEY[i];
				word1[i] = (Char)d;
			}

			string Dword = new string(word1);
			return Dword;
		}
		#endregion


		//Added by PE on 05-08-08 
		//To add custom fonts size in reports

		public static string rptFont1 = "Century";//Default Century
		public static string rptFont2 = "Century";
		public static string rptFont3 = "Century";

		public static int rptFontSize1 = 15;//Default 10 
		public static int rptFontSize2 = 13;
		public static int rptFontSize3 = 10;

		public static string rptFontStyle1 = "Regular";
		public static string rptFontStyle2 = "Regular";
		public static string rptFontStyle3 = "Regular";

		public static string rptFontLine1 = "";
		public static string rptFontLine2 = "";
		public static string rptFontLine3 = "";

		//Peter - 06-11-2007
		//single pattern applied for the excel object becz to over come the problem
		//of multiple object creation at all expression evaluation times
		public static Excel.ApplicationClass ObjExcel
		{
			get 
			{
				if (objExcel == null) 
					objExcel = new Excel.ApplicationClass();
				return objExcel ;
			}
		}
		//======================================================
		public string LogFileName
		{
			set{this.sLogFileName=value;}
			get{return this.sLogFileName;}
		}
		
		public string record
		{
			set{this.recordLog(value);}
		}

		public String[,] getOPRegistrationReports()
		{
			return strOPRegistrationReports;
		}

		public int getNoOfOPRegistrationReports()
		{
			return iNoOfOPRegistrationRep;
		}

		public int getNoOfIPRegistrationReports()
		{
			return iNoOfIPRegistrationRep;
		}

		public int getNoOfStoreReports()
		{
			return iNoOfStoresRep;
		}

		public int getNoOfPharmacyReports()
		{
			return iNoOfPharmaciesRep;
		}

		public int getNoOfLabReports()
		{
			return iNoOfLabsRep;
		}
		public int getNoOfBillingReports()
		{
			return iNoOfBillingRep;
		}
		public int getNoOfRecordingReports()
		{
			return iNoOfReocordingRep;
		}

		public String[,] getIPRegistrationReports()
		{
			return strIPRegistrationReports;
		}

		public String[,] getStoresReports()
		{
			return strStoresReports;
		}

		public String[,] getPharmacyReports()
		{
			return strPharmaciesReports;
		}
		
		public String[,] getLabReports()
		{
			return strLabsReports;
		}

		public String[,] getBillingReports()
		{
			return strBillingReports;
		}

		public String[,] getRecordingReports()
		{
			return strRecordingReports;
		}

        //public static void fillList(ListBox lst, object strSql, string strDisplayMember, string strValueMember)
        //{
        //    using (DataManager dataMember = new DataManager(strSql, "list"))
        //    {
        //        ResultArgs resultArgs = dataMember.FetchData(DataSource.DataTable);
        //        if (resultArgs.Success && resultArgs.RowsAffected > 0)
        //        {
        //            // resultArgs.DataSource.Table.TableName = "list";
        //            lst.DisplayMember = strDisplayMember;
        //            lst.ValueMember = strValueMember;
        //            lst.DataSource = resultArgs.DataSource.Table;
        //        }
        //    }
        //}
	

        //public void loadShortCuts(ref string[] strShortCuts, ref string[] strDeptIds)
        //{
        //    try
        //    {
        //        string sSqlShortCuts = "SELECT HDEPT_ID, SHORT_CUT FROM HOSPITAL_DEPARTMENTS WHERE SHORT_CUT IS NOT NULL";
        //        DataHandling dh=new DataHandling(sSqlShortCuts,"ShortCuts");

        //        strShortCuts	= new string [dh.getRecordCount()];
        //        strDeptIds		= new string [dh.getRecordCount()];
        //        for (int i=0; i<dh.getRecordCount(); i++)
        //        {
        //            strDeptIds[i]	= dh.getData(i,"HDEPT_ID");
        //            strShortCuts[i]	= dh.getData(i,"SHORT_CUT");
        //        }
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.Message.ToString(),"Load Shortcur Error");
        //    }
        //}

        //public static void getListBoxSelected(ListBox lst, object strSql, string strValueColum)
        //{
        //    try
        //    {
        //        //DataHandling dh=new DataHandling(strSql,"list");

        //        //DataSet objDS = new DataSet();
        //        //DataView objDV = new DataView();		

        //        //lst.ClearSelected();					
        //        //for (int i=0; i<dh.getRecordCount(); i++)
        //        //    lst.SelectedValue=dh.getData(i,strValueColum);

        //        using (DataManager dataMember = new DataManager(strSql, "list"))
        //        {
        //            lst.ClearSelected();
        //            ResultArgs resultArgs = dataMember.FetchData(DataSource.DataTable);
        //            if (resultArgs.Success && resultArgs.RowsAffected > 0)
        //            {
        //                DataTable dtListTable = resultArgs.DataSource.Table;
        //                for (int i = 0; i < dtListTable.Rows.Count; i++)
        //                {
        //                    lst.SelectedValue = dtListTable.Rows[i][strValueColum].ToString();
        //                }
        //            }
        //        }
        //    }
        //    catch(Exception e)
        //    {
        //        MessageBox.Show(e.Message.ToString(),"List Selecting Error");
        //    }
        //}
        public static string GetMySQLDateTime(string dateTime, DateDataType dateType)
        {
            string date = dateTime;

            if (date.Trim() == "") return null;
            try
            {
                if ((new CommonMemberSet.DateSetMember()).IsDate(date))
                {
                    //DateTimeFormatInfo dateTimeFormatInfo = new DateTimeFormatInfo();
                    //dateTimeFormatInfo.TimeSeparator = DateFormatInfo.TimeSeparator;
                    DateTime dt = DateTime.Parse(date);
                    switch (dateType)
                    {
                        case DateDataType.Date:
                            date = dt.ToString(DateFormatInfo.MySQLFormat.DateUpdate);
                            break;
                        case DateDataType.DateTime:
                            date = dt.ToString(DateFormatInfo.MySQLFormat.DateAndTimeUpdate);
                            break;
                        case DateDataType.TimeStamp:
                            date = dt.ToString(DateFormatInfo.MySQLFormat.TimeStampUpdate);
                            break;
                        case DateDataType.Time:
                            date = dt.ToString(DateFormatInfo.TimeFormat);
                            break;
                        case DateDataType.DateNoFormatBegin:
                            date = dt.ToString(DateFormatInfo.MySQLFormat.DateAndTimeNoformatBegin);
                            break;
                        case DateDataType.DateNoFormatEnd:
                            date = dt.ToString(DateFormatInfo.MySQLFormat.DateAndTimeNoformatEnd);
                        //case DateDataType.DateFormatYMD:
                        //    date = dt.ToString(DateFormatInfo.MySQLFormat.DateAndTimeUpdate);
                            break;
                    }
                }
            }
            catch (Exception) { }

            return date;
        }

        //public static string getServerDateTime()
        //{
        //    string strDate = "";
        //    //string sSql = "SELECT TO_CHAR(SYSDATE, 'DD/MM/YYYY HH:MI AM') AS \"Current Date\" FROM DUAL";

        //    try
        //    {
        //        DataHandling dh = new DataHandling(sSql, "System Date");
        //        DataSet objDS_SystemDate = new DataSet();
        //        objDS_SystemDate = dh.getDataSet();

        //        if (objDS_SystemDate.Tables["System Date"].Rows.Count > 0)
        //            strDate = objDS_SystemDate.Tables["System Date"].Rows[0]["Current Date"].ToString();
                
        //    }
        //    catch(Exception e)
        //    {
        //        MessageBox.Show(e.Message.ToString(),"Payroll", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    return strDate;
        //}

        //public static string getYesterdayDate()
        //{
        //    try
        //    {
        //        DataHandling dh = new DataHandling();
        //        return dh.ExecuteScalar("SELECT TO_CHAR(SYSDATE - 1, 'DD/MM/YYYY') FROM DUAL");
        //    }
        //    catch(Exception e)
        //    {
        //        MessageBox.Show(e.Message.ToString(),"Payroll", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //        return "";
        //    }
        //}

        //public static string getServerDate()
        //{
        //    string strDate = "";
        //    string sSql = "SELECT TO_CHAR(SYSDATE, 'DD/MM/YYYY') AS \"Current Date\" FROM DUAL";

        //    try
        //    {
        //        DataHandling dh=new DataHandling(sSql,"System Date");
        //        DataSet objDS_SystemDate = new DataSet();
        //        objDS_SystemDate=dh.getDataSet();

        //        if (objDS_SystemDate.Tables["System Date"].Rows.Count > 0)
        //            strDate = objDS_SystemDate.Tables["System Date"].Rows[0]["Current Date"].ToString();
        //    }
        //    catch(Exception e)
        //    {
        //        MessageBox.Show(e.Message.ToString(),"Payroll", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    return strDate;
        //}

        //public static string getServerDateMMDDFormat()
        //{
        //    string strDate = "";
        //    string sSql = "SELECT TO_CHAR(SYSDATE, 'MM/DD/YYYY') AS \"Current Date\" FROM DUAL";

        //    try
        //    {
        //        DataHandling dh = new DataHandling(sSql, "System Date");
        //        DataSet objDS_SystemDate = new DataSet();
        //        objDS_SystemDate = dh.getDataSet();

        //        if (objDS_SystemDate.Tables["System Date"].Rows.Count > 0)
        //            strDate = objDS_SystemDate.Tables["System Date"].Rows[0]["Current Date"].ToString();
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.Message.ToString(), "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    return strDate;
        //}
        //public static string getServerDateTimeMMDDFormat()
        //{
        //    string strDate = "";
        //    string sSql = "SELECT TO_CHAR(SYSDATE, 'MM/DD/YYYY HH:MI AM') AS \"Current Date\" FROM DUAL";

        //    try
        //    {
        //        DataHandling dh = new DataHandling(sSql, "System Date");
        //        DataSet objDS_SystemDate = new DataSet();
        //        objDS_SystemDate = dh.getDataSet();

        //        if (objDS_SystemDate.Tables["System Date"].Rows.Count > 0)
        //            strDate = objDS_SystemDate.Tables["System Date"].Rows[0]["Current Date"].ToString();
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.Message.ToString(), "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    return strDate;
        //}

        //public static string getServerDateyyFormat()
        //{
        //    string strDate = "";
        //    string sSql = "SELECT TO_CHAR(SYSDATE, 'DD/MM/YY') AS \"Current Date\" FROM DUAL";

        //    try
        //    {
        //        DataHandling dh=new DataHandling(sSql,"System Date");
        //        DataSet objDS_SystemDate = new DataSet();
        //        objDS_SystemDate=dh.getDataSet();

        //        if (objDS_SystemDate.Tables["System Date"].Rows.Count > 0)
        //            strDate = objDS_SystemDate.Tables["System Date"].Rows[0]["Current Date"].ToString();
        //    }
        //    catch(Exception e)
        //    {
        //        MessageBox.Show(e.Message.ToString(),"Payroll", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    return strDate;
        //}
        //public static string getServerDateyyyyFormat()
        //{
        //    string strDate = "";
        //    string sSql = "SELECT TO_CHAR(SYSDATE, 'DD/MM/YYYY') AS \"Current Date\" FROM DUAL";

        //    try
        //    {
        //        DataHandling dh = new DataHandling(sSql, "System Date");
        //        DataSet objDS_SystemDate = new DataSet();
        //        objDS_SystemDate = dh.getDataSet();

        //        if (objDS_SystemDate.Tables["System Date"].Rows.Count > 0)
        //            strDate = objDS_SystemDate.Tables["System Date"].Rows[0]["Current Date"].ToString();
        //    }
        //    catch (Exception e)
        //    {
        //        MessageBox.Show(e.Message.ToString(), "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    return strDate;
        //}

        //public static string getServerTime()
        //{
        //    string strTime = "";
        //    string sSql = "SELECT TO_CHAR(SYSDATE, 'HH:MI AM') AS \"Current Time\" FROM DUAL";

        //    try
        //    {
        //        DataHandling dh=new DataHandling();
        //        strTime = dh.ExecuteScalar(sSql);
        //    }
        //    catch(Exception e)
        //    {
        //        MessageBox.Show(e.Message.ToString(),"Payroll", MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    return strTime;
        //}
        //public static string getServerTimeWithoutPeriod()
        //{
        //    string strTime = "";
        //    string sSql = "SELECT TO_CHAR(SYSDATE, 'HH:MI') AS \"Current Time\" FROM DUAL";

        //    try
        //    {
        //        DataHandling dh=new DataHandling();
        //        strTime = dh.ExecuteScalar(sSql);
        //    }
        //    catch(Exception e)
        //    {
        //        MessageBox.Show(e.Message.ToString(),clsGeneral.MsgCaption, MessageBoxButtons.OK, MessageBoxIcon.Error);
        //    }
        //    return strTime;
        //}
        //public static int getServerHour()
        //{
        //    string sSql =	"select case when to_char(sysdate, 'HH') = '12' then '0' " +
        //                    "else to_char(sysdate, 'HH') end as \"current hour\" from dual";
        //    DataHandling dh = new DataHandling();
        //    return int.Parse(dh.ExecuteScalar(sSql));
        //}
        //public static int getServerMinute()
        //{
        //    string sSql =	"select case when substr((t.cur_min / 5),0,1) = '.' then '0' " +
        //                    "else substr((t.cur_min / 5),0,1) end as \"current minute\" from " +
        //                    "(select to_char(sysdate, 'MI') as cur_min from dual) t";
        //    DataHandling dh = new DataHandling();
        //    return int.Parse(dh.ExecuteScalar(sSql));
        //}
        //public static int getServerPeriod()
        //{
        //    string sSql =	"select case when to_char(sysdate, 'AM') = 'AM' then 0 " +
        //                    "else 1 end as \"current period\" from dual";
        //    DataHandling dh = new DataHandling();
        //    return int.Parse(dh.ExecuteScalar(sSql));
        //}

//        public static DataSet getCurrentStockPosition(int iDeptId, string sDept, int iRackId, int iShelfId)
//        {
//            DataSet objDS	= new DataSet();
//            string sSql		= "";
		
//            if (sDept.Trim().ToLower() == "store")
//            {
//                if (iRackId == 0 & iShelfId == 0)
//                    sSql =	"SELECT ROWNUM AS \"#\", IT.TYPE_DESC AS \"ITEM TYPE\", " +
//                            "I.ITEM_ID AS \"ITEM ID\", I.ITEM_CODE AS \"ITEM CODE\", " +
//                            "I.ITEM_DESC AS \"ITEM NAME\", S.BACH_NUM AS \"BATCH NUMBER\", " +
//                            "S.EXP_DATE AS \"EXP DATE\", NVL(S.NET_QTY, 0) AS \"AVAILABLE QTY\", S.NET_QTY_MEAS_SYMB AS \"MEASURE\", " +
//                            "TO_CHAR(S.UNT_PRIC, '9999999990.00') AS \"UNIT PRICE\", " +
//                            "TO_CHAR(S.VEN_PRIC, '9999999990.00') AS \"VENDOR PRICE\", " +
//                            "S.PACKSIZE AS \"PACK SIZE\", " +
//                            "(select max(sh.shelf_name) from  " +
//                            "department_rack dr, " +
//                            "shelf sh," +
//                            "shelf_items si " +
//                            "where dr.rack_id = sh.rack_id  and " +
//                               " sh.shelf_id = si.shelf_id and " +
//                                " si.item_id = s.item_id and dr.hdept_id = " + iDeptId + ") as shelf, " +
//                            "(select max(dr.rack_name) from " +
//                                " department_rack dr, " +
//                                "shelf sh," +
//                                "shelf_items si " +
//                                "where dr.rack_id = sh.rack_id  and " +
//                                "sh.shelf_id = si.shelf_id and " +
//                                "si.item_id = s.item_id and dr.hdept_id = " + iDeptId + " ) as Rack " +
//                            "FROM ITEM I, ITEM_TYPE IT, " +				//Modified By Jeya
//                            "STORE S WHERE S.HDEPT_ID = " + iDeptId + " AND " +
//                            "S.ITEM_ID = I.ITEM_ID AND IT.TYPE_ID = I.TYPE_ID AND " +
//                            "S.NET_QTY <> 0 AND S.TRAN_ID IN (SELECT MAX(TRAN_ID) FROM ITEM, " +
//                            "STORE WHERE STORE.HDEPT_ID = " + iDeptId + " AND STORE.ITEM_ID = ITEM.ITEM_ID " +
//                            "GROUP BY STORE.ITEM_ID, STORE.BACH_NUM)";
//                else if (iRackId != 0 & iShelfId != 0)
//                    sSql =	"SELECT ROWNUM AS \"#\", IT.TYPE_DESC AS \"ITEM TYPE\", " +
//                            "I.ITEM_ID AS \"ITEM ID\", I.ITEM_CODE AS \"ITEM CODE\", " +
//                            "I.ITEM_DESC AS \"ITEM NAME\", S.BACH_NUM AS \"BATCH NUMBER\", " +
//                            "S.EXP_DATE AS \"EXP DATE\", NVL(S.NET_QTY, 0) AS \"AVAILABLE QTY\", S.NET_QTY_MEAS_SYMB AS \"MEASURE\", " +
//                            "TO_CHAR(S.UNT_PRIC, '9999999990.00') AS \"UNIT PRICE\", " +
//                            "TO_CHAR(S.VEN_PRIC, '9999999990.00') AS \"VENDOR PRICE\", " +
//                            "S.PACKSIZE AS \"PACK SIZE\", " +
//                            "(select max(sh.shelf_name) from  " +
//                            "department_rack dr, " +
//                            "shelf sh," +
//                            "shelf_items si " +
//                            "where dr.rack_id = sh.rack_id  and " +
//                            " sh.shelf_id = si.shelf_id and " +
//                            " si.item_id = s.item_id and dr.hdept_id = " + iDeptId + " and sh.shelf_id=" + iShelfId + ") as shelf, " +
//                            "(select max(dr.rack_name) from " +
//                            " department_rack dr, " +
//                            "shelf sh," +
//                            "shelf_items si " +
//                            "where dr.rack_id = sh.rack_id  and " +
//                            "sh.shelf_id = si.shelf_id and " +
//                            "si.item_id = s.item_id and dr.hdept_id = " + iDeptId + " and dr.rack_id =  " + iRackId + " ) as Rack " +
//                            "FROM ITEM I, ITEM_TYPE IT,department_rack dr,shelf sh,STORE S WHERE S.HDEPT_ID = " + iDeptId + " AND " +
//                            "S.ITEM_ID = I.ITEM_ID AND I.ITEM_ID IN (SELECT ITEM_ID FROM SHELF_ITEMS " +
//                            "WHERE SHELF_ID = " + iShelfId + ") AND IT.TYPE_ID = I.TYPE_ID AND " +
//                            "dr.rack_id =  " + iRackId + "   AND " +										//Jeya
//                            "sh.shelf_id=" + iShelfId + "  AND " +                 //Jeya
//                            "sh.shelf_id in (SELECT SHELF_ID FROM SHELF WHERE RACK_ID =" + iRackId + ") and " +
//                            "S.NET_QTY <> 0 AND S.TRAN_ID IN (SELECT MAX(TRAN_ID) FROM ITEM, STORE " +
//                            "WHERE STORE.HDEPT_ID = " + iDeptId + " AND STORE.ITEM_ID = ITEM.ITEM_ID " +
//                            "AND ITEM.ITEM_ID IN (SELECT ITEM_ID FROM SHELF_ITEMS " +
//                            "WHERE SHELF_ID = " + iShelfId + ") " +
//                            "GROUP BY STORE.ITEM_ID, STORE.BACH_NUM)";
//                else if (iRackId != 0 & iShelfId == 0)
//                    sSql =	"SELECT ROWNUM AS \"#\", IT.TYPE_DESC AS \"ITEM TYPE\", " +
//                            "I.ITEM_ID AS \"ITEM ID\", I.ITEM_CODE AS \"ITEM CODE\", " +
//                            "I.ITEM_DESC AS \"ITEM NAME\", S.BACH_NUM AS \"BATCH NUMBER\", " +
//                            "S.EXP_DATE AS \"EXP DATE\", NVL(S.NET_QTY, 0) AS \"AVAILABLE QTY\", S.NET_QTY_MEAS_SYMB AS \"MEASURE\", " +
//                            "TO_CHAR(S.UNT_PRIC, '9999999990.00') AS \"UNIT PRICE\", " +
//                            "TO_CHAR(S.VEN_PRIC, '9999999990.00') AS \"VENDOR PRICE\", " +
//                            "S.PACKSIZE AS \"PACK SIZE\", " +
//                            "(select max(sh.shelf_name) from  " +
//                            "department_rack dr, " +
//                            "shelf sh," +
//                            "shelf_items si " +
//                            "where dr.rack_id = sh.rack_id  and " +
//                            " sh.shelf_id = si.shelf_id and " +
//                            " si.item_id = s.item_id and dr.hdept_id = " + iDeptId + ") as shelf, " +
//                            "(select max(dr.rack_name) from " +
//                            " department_rack dr, " +
//                            "shelf s," +
//                            "shelf_items si " +
//                            "where dr.rack_id = s.rack_id  and " +
//                            "s.shelf_id = si.shelf_id and " +
//                            "si.item_id = s.item_id and dr.hdept_id = " + iDeptId + " and dr.rack_id =  " + iRackId + ") as Rack " +
//                            "FROM ITEM I, ITEM_TYPE IT,department_rack dr, STORE S WHERE S.HDEPT_ID = " + iDeptId + " AND " +
//                            "S.ITEM_ID = I.ITEM_ID AND I.ITEM_ID IN (SELECT ITEM_ID FROM SHELF_ITEMS " +
//                            "WHERE SHELF_ID IN (SELECT SHELF_ID FROM SHELF WHERE RACK_ID = " + iRackId + "))AND " +
//                            "dr.rack_id =  " + iRackId + "  AND " +										//Jeya
//                            " IT.TYPE_ID = I.TYPE_ID AND S.NET_QTY <> 0 AND S.TRAN_ID IN " +
//                            "(SELECT MAX(TRAN_ID) FROM ITEM, STORE WHERE STORE.HDEPT_ID = " + iDeptId + 
//                            " AND STORE.ITEM_ID = ITEM.ITEM_ID AND ITEM.ITEM_ID IN (SELECT ITEM_ID FROM " +
//                            "SHELF_ITEMS WHERE SHELF_ID IN (SELECT SHELF_ID FROM SHELF WHERE " +
//                            "RACK_ID = " + iRackId + ")) GROUP BY STORE.ITEM_ID, STORE.BACH_NUM)";
//            }
//            else if (sDept.Trim().ToLower() == "pharmacy"
//                || sDept.Trim().ToLower() == "laboratory" || sDept.Trim().ToLower() == "ward")
//            {
//                if (iRackId == 0 & iShelfId == 0)
//                    sSql =	"SELECT ROWNUM AS \"#\", IT.TYPE_DESC AS \"ITEM TYPE\", " +
//                            "I.ITEM_ID AS \"ITEM ID\", I.ITEM_CODE AS \"ITEM CODE\", " +
//                            "I.ITEM_DESC AS \"ITEM NAME\", DS.BACH_NUM AS \"BATCH NUMBER\", " +
//                            "DS.EXP_DATE AS \"EXP DATE\", NVL(DS.NET_QTY, 0) AS \"AVAILABLE QTY\", DS.NET_QTY_MEAS_SYMB AS \"MEASURE\", " +
//                            "TO_CHAR(DS.UNT_PRIC, '9999999990.00') AS \"UNIT PRICE\", " +
//                            "TO_CHAR(DS.VEN_PRIC, '9999999990.00') AS \"VENDOR PRICE\", " +
//                            "DS.PACKSIZE AS \"PACK SIZE\", " +
//                            "(select max(sh.shelf_name) from  " +
//                            "department_rack dr, " +
//                            "shelf sh," +
//                            "shelf_items si " +
//                            "where dr.rack_id = sh.rack_id  and " +
//                            " sh.shelf_id = si.shelf_id and " +
//                            " si.item_id = ds.item_id and dr.hdept_id = " + iDeptId + " ) as shelf, " +
//                            "(select max(dr.rack_name) from " +
//                            " department_rack dr, " +
//                            "shelf s," +
//                            "shelf_items si " +
//                            "where dr.rack_id = s.rack_id  and " +
//                            "s.shelf_id = si.shelf_id and " +
//                            "si.item_id = ds.item_id and dr.hdept_id = " + iDeptId + " ) as Rack " +
//                            "FROM ITEM I, ITEM_TYPE IT, DEPARTMENT_STORE DS WHERE DS.HDEPT_ID = " + iDeptId + " AND " +
//                            "DS.ITEM_ID = I.ITEM_ID AND IT.TYPE_ID = I.TYPE_ID AND " +
//                            "DS.NET_QTY <> 0 AND TRAN_ID IN (SELECT MAX(TRAN_ID) FROM ITEM, DEPARTMENT_STORE " +
//                            "WHERE DEPARTMENT_STORE.HDEPT_ID = " + iDeptId + " AND DEPARTMENT_STORE.ITEM_ID = ITEM.ITEM_ID " +
//                            "GROUP BY DEPARTMENT_STORE.ITEM_ID, DEPARTMENT_STORE.BACH_NUM)";
//                else if (iRackId != 0 & iShelfId != 0)
//                    sSql=	"SELECT ROWNUM AS \"#\", IT.TYPE_DESC AS \"ITEM TYPE\", " +
//                            "I.ITEM_ID AS \"ITEM ID\", I.ITEM_CODE AS \"ITEM CODE\", " +
//                            "I.ITEM_DESC AS \"ITEM NAME\", DS.BACH_NUM AS \"BATCH NUMBER\", " +
//                            "DS.EXP_DATE AS \"EXP DATE\", NVL(DS.NET_QTY, 0) AS \"AVAILABLE QTY\", DS.NET_QTY_MEAS_SYMB AS \"MEASURE\", " +
//                            "TO_CHAR(DS.UNT_PRIC, '9999999990.00') AS \"UNIT PRICE\", " +
//                            "TO_CHAR(DS.VEN_PRIC, '9999999990.00') AS \"VENDOR PRICE\", " +
//                            "DS.PACKSIZE AS \"PACK SIZE\", " +
//                            "(select max(sh.shelf_name) from  " +
//                            "department_rack dr, " +
//                            "shelf sh," +
//                            "shelf_items si " +
//                            "where dr.rack_id = sh.rack_id  and " +
//                            " sh.shelf_id = si.shelf_id and " +
//                            " si.item_id = ds.item_id and dr.hdept_id = " + iDeptId + " and sh.shelf_id = " + iShelfId + " ) as shelf, " +
//                            "(select max(dr.rack_name) from " +
//                            " department_rack dr, " +
//                            "shelf s," +
//                            "shelf_items si " +
//                            "where dr.rack_id = s.rack_id  and " +
//                            "s.shelf_id = si.shelf_id and " +
//                            "si.item_id = ds.item_id and dr.hdept_id = " + iDeptId + " and dr.rack_id =" + iRackId + "  ) as Rack " +
//                            "FROM ITEM I, ITEM_TYPE IT,shelf sh,department_rack dr, DEPARTMENT_STORE DS " +
//                            "WHERE DS.HDEPT_ID = " + iDeptId + " AND DS.ITEM_ID = I.ITEM_ID AND " +
//                            "I.ITEM_ID IN (SELECT ITEM_ID FROM SHELF_ITEMS WHERE SHELF_ID = " + iShelfId + ") " +
//                            "AND IT.TYPE_ID = I.TYPE_ID AND DS.NET_QTY <> 0 AND " +
//                            "sh.shelf_id = " + iShelfId + " AND "+																								 //Jeya
//                            "dr.rack_id =  " + iRackId + "    AND " +										//Jeya
//                            "DS.TRAN_ID IN (SELECT MAX(TRAN_ID) FROM ITEM, DEPARTMENT_STORE " +
//                            "WHERE DEPARTMENT_STORE.HDEPT_ID = " + iDeptId + " AND DEPARTMENT_STORE.ITEM_ID = ITEM.ITEM_ID " +
//                            "AND ITEM.ITEM_ID IN (SELECT ITEM_ID FROM SHELF_ITEMS WHERE SHELF_ID = " + iShelfId + ") " +
//                            "GROUP BY DEPARTMENT_STORE.ITEM_ID, DEPARTMENT_STORE.BACH_NUM)";
//                else if (iRackId != 0 & iShelfId == 0)
//                    sSql=	"SELECT ROWNUM AS \"#\", IT.TYPE_DESC AS \"ITEM TYPE\", " +
//                            "I.ITEM_ID AS \"ITEM ID\", I.ITEM_CODE AS \"ITEM CODE\", " +
//                            "I.ITEM_DESC AS \"ITEM NAME\", DS.BACH_NUM AS \"BATCH NUMBER\", " +
//                            "DS.EXP_DATE AS \"EXP DATE\", NVL(DS.NET_QTY, 0) AS \"AVAILABLE QTY\", DS.NET_QTY_MEAS_SYMB AS \"MEASURE\", " +
//                            "TO_CHAR(DS.UNT_PRIC, '9999999990.00') AS \"UNIT PRICE\", " +
//                            "TO_CHAR(DS.VEN_PRIC, '9999999990.00') AS \"VENDOR PRICE\", " +
//                            "DS.PACKSIZE AS \"PACK SIZE\", " +
//                            "(select max(sh.shelf_name) from  " +
//                            "department_rack dr, " +
//                            "shelf sh," +
//                            "shelf_items si " +
//                            "where dr.rack_id = sh.rack_id  and " +
//                            " sh.shelf_id = si.shelf_id and " +
//                            " si.item_id = ds.item_id and dr.hdept_id = " + iDeptId + " ) as shelf, " +
//                            "(select max(dr.rack_name) from " +
//                            " department_rack dr, " +
//                            "shelf s," +
//                            "shelf_items si " +
//                            "where dr.rack_id = s.rack_id  and " +
//                            "s.shelf_id = si.shelf_id and " +
//                            "si.item_id = ds.item_id and dr.hdept_id = " + iDeptId + " and dr.rack_id =  " + iRackId + ") as Rack " +
//                            "FROM ITEM I, ITEM_TYPE IT,department_rack dr, DEPARTMENT_STORE DS " +
//                            "WHERE DS.HDEPT_ID = " + iDeptId + " AND DS.ITEM_ID = I.ITEM_ID AND " +
//                            "I.ITEM_ID IN (SELECT ITEM_ID FROM SHELF_ITEMS WHERE " +
//                            "SHELF_ID IN (SELECT SHELF_ID FROM SHELF WHERE RACK_ID = " + iRackId + ")) " +
//                            "AND IT.TYPE_ID = I.TYPE_ID AND DS.NET_QTY <> 0 AND " +
//                            "dr.rack_id =  " + iRackId + "  AND " +																                     //Jeya
//                            "DS.TRAN_ID IN (SELECT MAX(TRAN_ID) FROM ITEM, DEPARTMENT_STORE " +
//                            "WHERE DEPARTMENT_STORE.HDEPT_ID = " + iDeptId + " AND " +
//                            "DEPARTMENT_STORE.ITEM_ID = ITEM.ITEM_ID AND ITEM.ITEM_ID IN (SELECT ITEM_ID " +
//                            "FROM SHELF_ITEMS WHERE SHELF_ID IN (SELECT SHELF_ID FROM SHELF " +
//                            "WHERE RACK_ID = " + iRackId + ")) " +
//                            "GROUP BY DEPARTMENT_STORE.ITEM_ID, DEPARTMENT_STORE.BACH_NUM)";
//            }
//            try
//            {
//                DataHandling dh		= new DataHandling(sSql,"STOCK POSITION");
//                objDS	= dh.getDataSet();
//            }
//            catch(Exception ex)
//            {
//                MessageBox.Show(ex.Message, "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Error);
//            }

//            return objDS;
//        }
		
//        public static string getDepartmentName(int iHdeptId)
//        {
//            string strQuery = "SELECT HDEPT_DESC AS \"Hospital Department\" FROM HOSPITAL_DEPARTMENTS WHERE HDEPT_ID = " + iHdeptId;
//            DataHandling dhDept = new DataHandling(strQuery,"Hospital Department");
//            if(dhDept.getRecordCount()>0)
//            {
//                CurrentLab = dhDept.getData(0,"Hospital Department");
//            }
//            return CurrentLab;
//        }

//        public void getAvailableReports() //TODO can be changed to getAvailableStore
//        {
//            //To get available OP Registration reports
//            string strQuery="";
//            DataSet dsReport = new DataSet();
//            if(clsGeneral.RPT_USER_NAME.ToUpper()==clsGeneral.USER_DEPARTMENT.ToUpper())
//            {
//                strQuery = "SELECT R.RPT_CODE,R.RPT_NAME FROM REPORT_MAIN R "+
//                    "WHERE R.RPT_MODULE= 1 AND R.RPT_TYPE<>1";
//            }
//            else
//            {
//                strQuery = "SELECT R.RPT_CODE,R.RPT_NAME FROM REPORT_MAIN R,REPORT_MAPPING M "+
//                    "WHERE R.RPT_MODULE= 1 AND R.RPT_TYPE<>1 AND M.RPT_CODE=R.RPT_CODE AND UPPER(M.USR_CODE)='"+clsGeneral.USER_NAME+"'"; 	
//            }
//            if (createDataSet(strQuery,"Reports") == null)
//            {
//                dsReport = getDataSet();
//                iNoOfOPRegistrationRep = getRecordCount();
//                strOPRegistrationReports = new string[iNoOfOPRegistrationRep,2];
				
//                for(int i = 0;i<iNoOfOPRegistrationRep;i++)
//                {
//                    strOPRegistrationReports[i,0] = getData(i,"RPT_CODE");
//                    strOPRegistrationReports[i,1] = getData(i,"RPT_NAME");
//                }
//            }

//            //To get available IP Registration reports
//            dsReport.Dispose();
//            dsReport = new DataSet();
//            //strQuery = "SELECT RPT_CODE,RPT_NAME FROM REPORT_MAIN WHERE RPT_MODULE= -1 AND RPT_TYPE<>1"; 
//            if(clsGeneral.RPT_USER_NAME.ToUpper()==clsGeneral.USER_DEPARTMENT.ToUpper())
//            {
//                strQuery = "SELECT R.RPT_CODE,R.RPT_NAME FROM REPORT_MAIN R "+
//                    "WHERE R.RPT_MODULE= -1 AND R.RPT_TYPE<>1";
//            }
//            else
//            {
//                strQuery = "SELECT R.RPT_CODE,R.RPT_NAME FROM REPORT_MAIN R,REPORT_MAPPING M "+
//                    "WHERE R.RPT_MODULE= -1 AND R.RPT_TYPE<>1 AND M.RPT_CODE=R.RPT_CODE AND UPPER(M.USR_CODE)='"+clsGeneral.USER_NAME+"'";
//            }
//            if (createDataSet(strQuery,"Reports") == null)
//            {
//                dsReport = getDataSet();
//                iNoOfIPRegistrationRep = getRecordCount();
//                strIPRegistrationReports = new string[iNoOfIPRegistrationRep,2];
				
//                for(int i = 0;i<iNoOfIPRegistrationRep;i++)
//                {
//                    strIPRegistrationReports[i,0] = getData(i,"RPT_CODE");
//                    strIPRegistrationReports[i,1] = getData(i,"RPT_NAME");
//                }
//            }

//            //To get available Pharmacy reports
//            dsReport.Dispose();
//            dsReport = new DataSet();
//            //if(clsGeneral.RPT_USER_NAME.ToUpper()==clsGeneral.USER_DEPARTMENT.ToUpper())
//            if(sDepartmentTypeId.IndexOf(clsGeneral.DEPARTMENT_PHARMACY.ToString())>=0)
//            {
//                strQuery = "SELECT R.RPT_CODE,R.RPT_NAME FROM REPORT_MAIN R,REPORT_MAPPING M "+
//                    "WHERE R.RPT_MODULE= " + clsGeneral.DEPARTMENT_PHARMACY +" AND R.RPT_TYPE<>1 AND M.RPT_CODE=R.RPT_CODE AND UPPER(M.USR_CODE)='"+clsGeneral.USER_NAME+"'";
//            }
//            else
//            {
//                strQuery = "SELECT R.RPT_CODE,R.RPT_NAME FROM REPORT_MAIN R "+
//                    "WHERE R.RPT_MODULE= " + clsGeneral.DEPARTMENT_PHARMACY +" AND R.RPT_TYPE<>1";
//            }
//            if (createDataSet(strQuery,"Reports") == null)
//            {
//                dsReport = getDataSet();
//                iNoOfPharmaciesRep = getRecordCount();
//                strPharmaciesReports = new string[iNoOfPharmaciesRep,2];
				
//                for(int i = 0;i<iNoOfPharmaciesRep;i++)
//                {
//                    strPharmaciesReports[i,0] = getData(i,"RPT_CODE");
//                    strPharmaciesReports[i,1] = getData(i,"RPT_NAME");
//                }
//            }

//            //To get available Store reports
//            dsReport.Dispose();
//            dsReport = new DataSet();
//            //strQuery = "SELECT RPT_CODE,RPT_NAME FROM REPORT_MAIN WHERE RPT_MODULE=" + clsGeneral.DEPARTMENT_STORE +" AND RPT_TYPE<>1";
//            if(clsGeneral.RPT_USER_NAME.ToUpper()==clsGeneral.USER_DEPARTMENT.ToUpper())
//            {
//                strQuery = "SELECT R.RPT_CODE,R.RPT_NAME FROM REPORT_MAIN R "+
//                    "WHERE R.RPT_MODULE= " + clsGeneral.DEPARTMENT_STORE +" AND R.RPT_TYPE<>1";
						
//            }
//            else
//            {
//                strQuery = "SELECT R.RPT_CODE,R.RPT_NAME FROM REPORT_MAIN R,REPORT_MAPPING M "+
//                    "WHERE R.RPT_MODULE= " + clsGeneral.DEPARTMENT_STORE +" AND R.RPT_TYPE<>1 AND M.RPT_CODE=R.RPT_CODE AND UPPER(M.USR_CODE)='"+clsGeneral.USER_NAME+"'";
//            }
//            if (createDataSet(strQuery,"Reports") == null)
//            {
//                dsReport = getDataSet();
//                iNoOfStoresRep = getRecordCount();
//                strStoresReports = new string[iNoOfStoresRep,2];
				
//                for(int i = 0;i<iNoOfStoresRep;i++)
//                {
//                    strStoresReports[i,0] = getData(i,"RPT_CODE");
//                    strStoresReports[i,1] = getData(i,"RPT_NAME");
//                }
//            }

//            //To get available Lab reports
//            dsReport.Dispose();
//            dsReport = new DataSet();
//            //strQuery = "SELECT RPT_CODE,RPT_NAME FROM REPORT_MAIN WHERE RPT_MODULE=" + clsGeneral.DEPARTMENT_LAB +" AND RPT_TYPE<>1";
//            if(clsGeneral.RPT_USER_NAME.ToUpper()==clsGeneral.USER_DEPARTMENT.ToUpper())
//            {
//                strQuery = "SELECT R.RPT_CODE,R.RPT_NAME FROM REPORT_MAIN R "+
//                "WHERE R.RPT_MODULE= " + clsGeneral.DEPARTMENT_LAB +" AND R.RPT_TYPE<>1";
//            }
//            else
//            {
//                strQuery = "SELECT R.RPT_CODE,R.RPT_NAME FROM REPORT_MAIN R,REPORT_MAPPING M "+
//                    "WHERE R.RPT_MODULE= " + clsGeneral.DEPARTMENT_LAB +" AND R.RPT_TYPE<>1 AND M.RPT_CODE=R.RPT_CODE AND UPPER(M.USR_CODE)='"+clsGeneral.USER_NAME+"'";
//            }
//            if (createDataSet(strQuery,"Reports") == null)
//            {
//                dsReport = getDataSet();
//                iNoOfLabsRep= getRecordCount();
//                strLabsReports = new string[iNoOfLabsRep,2];
				
//                for(int i = 0;i<iNoOfLabsRep;i++)
//                {
//                    strLabsReports[i,0] = getData(i,"RPT_CODE");
//                    strLabsReports[i,1] = getData(i,"RPT_NAME");
//                }
//            }			

//            //To get available Billing reports
//            dsReport.Dispose();
//            dsReport = new DataSet();
//            //strQuery = "SELECT RPT_CODE,RPT_NAME FROM REPORT_MAIN WHERE RPT_MODULE=" + clsGeneral.DEPARTMENT_BILLING +" AND RPT_TYPE<>1";
//            if(clsGeneral.RPT_USER_NAME.ToUpper()==clsGeneral.USER_DEPARTMENT.ToUpper())
//            {
//                strQuery = "SELECT R.RPT_CODE,R.RPT_NAME FROM REPORT_MAIN R "+
//                    "WHERE R.RPT_MODULE= " + clsGeneral.DEPARTMENT_BILLING +" AND R.RPT_TYPE<>1";
//            }
//            else
//            {
//                strQuery = "SELECT R.RPT_CODE,R.RPT_NAME FROM REPORT_MAIN R,REPORT_MAPPING M "+
//                    "WHERE R.RPT_MODULE= " + clsGeneral.DEPARTMENT_BILLING +" AND R.RPT_TYPE<>1 AND M.RPT_CODE=R.RPT_CODE AND UPPER(M.USR_CODE)='"+ clsGeneral.USER_NAME+"'";
//            }
//            if (createDataSet(strQuery,"Reports") == null)
//            {
//                dsReport = getDataSet();
//                iNoOfBillingRep= getRecordCount();
//                strBillingReports = new string[iNoOfBillingRep,2];
				
//                for(int i = 0;i<iNoOfBillingRep;i++)
//                {
//                    strBillingReports[i,0] = getData(i,"RPT_CODE");
//                    strBillingReports[i,1] = getData(i,"RPT_NAME");
//                }
//            }

//            //To get available Recording reports
//            dsReport.Dispose();
//            dsReport = new DataSet();
//            //strQuery = "SELECT RPT_CODE,RPT_NAME FROM REPORT_MAIN WHERE RPT_MODULE=" + clsGeneral.DEPARTMENT_RECORDING +" AND RPT_TYPE<>1";
//            if(clsGeneral.RPT_USER_NAME.ToUpper()==clsGeneral.USER_DEPARTMENT.ToUpper())
//            {
//                strQuery = "SELECT R.RPT_CODE,R.RPT_NAME FROM REPORT_MAIN R "+
//                    "WHERE R.RPT_MODULE= " + clsGeneral.DEPARTMENT_BILLING +" AND R.RPT_TYPE<>1";
//            }
//            else
//            {
//                strQuery = "SELECT R.RPT_CODE,R.RPT_NAME FROM REPORT_MAIN R,REPORT_MAPPING M "+
//                    "WHERE R.RPT_MODULE= " + clsGeneral.DEPARTMENT_RECORDING +" AND R.RPT_TYPE<>1 AND M.RPT_CODE=R.RPT_CODE AND UPPER(M.USR_CODE)='"+clsGeneral.USER_NAME+"'";
//            }
//            if (createDataSet(strQuery,"Reports") == null)
//            {
//                dsReport = getDataSet();
//                iNoOfReocordingRep = getRecordCount();
//                strRecordingReports = new string [iNoOfReocordingRep,2];

//                for(int i = 0; i<iNoOfReocordingRep; i++)
//                {
//                    strRecordingReports[i,0] = getData(i,"RPT_CODE");
//                    strRecordingReports[i,1] = getData(i,"RPT_NAME");
//                }				
//            }
//        }
//        public void getRptsHdptIdwise(string sHid,int iModule)
//        {
//            string strQuery="";
//            DataSet dsReport = new DataSet();
//            if(sDepartmentTypeId.IndexOf(clsGeneral.DEPARTMENT_LAB.ToString())>=0)
//            {
//                strQuery ="SELECT R.RPT_CODE,R.RPT_NAME FROM REPORT_MAIN R,REPORT_MAPPING M "+
//                    "WHERE R.HDEPT_ID= " + sHid +" AND R.RPT_TYPE<>1 AND M.RPT_CODE=R.RPT_CODE AND UPPER(M.USR_CODE)='"+clsGeneral.USER_NAME+"'";
//            }
//            else
//            {
//                strQuery ="SELECT R.RPT_CODE,R.RPT_NAME FROM REPORT_MAIN R "+
//                    "WHERE R.HDEPT_ID= " + sHid +" AND R.RPT_TYPE<>1";
//            }
//            if (createDataSet(strQuery,"Reports") == null)
//            {
//                dsReport = getDataSet();
//                switch (iModule)
//                {
//                 case clsGeneral.DEPARTMENT_LAB:
//                        iNoOfLabsRep= getRecordCount();
//                        strLabsReports = new string[iNoOfLabsRep,2];
				
//                        for(int i = 0;i<iNoOfLabsRep;i++)
//                        {
//                            strLabsReports[i,0] = getData(i,"RPT_CODE");
//                            strLabsReports[i,1] = getData(i,"RPT_NAME");
//                        }
//                        break;
//                 case clsGeneral.DEPARTMENT_PHARMACY:
//                     iNoOfPharmaciesRep = getRecordCount();
//                     strPharmaciesReports = new string[iNoOfPharmaciesRep,2];
//                     for(int i = 0;i<iNoOfPharmaciesRep;i++)
//                     {
//                         strPharmaciesReports[i,0] = getData(i,"RPT_CODE");
//                         strPharmaciesReports[i,1] = getData(i,"RPT_NAME");
//                     }
//                        break;
//                case clsGeneral.DEPARTMENT_STORE:
//                    iNoOfStoresRep = getRecordCount();
//                    strStoresReports = new string[iNoOfStoresRep,2];
				
//                    for(int i = 0;i<iNoOfStoresRep;i++)
//                    {
//                        strStoresReports[i,0] = getData(i,"RPT_CODE");
//                        strStoresReports[i,1] = getData(i,"RPT_NAME");
//                    }
//                        break;
//                }
//            }			
//        }
//        public void getAvailReptsHdeptIdWise()
//        {
//            //To get available OP Registration reports
//            string strQuery="";
//            DataSet dsReport = new DataSet();
//            if(clsGeneral.RPT_USER_NAME.ToUpper()==clsGeneral.USER_DEPARTMENT.ToUpper())
//            {
//                strQuery = "SELECT R.RPT_CODE,R.RPT_NAME FROM REPORT_MAIN R "+
//                    "WHERE R.HDEPT_ID= 1 AND R.RPT_TYPE<>1";
//            }
//            else
//            {
//                strQuery = "SELECT R.RPT_CODE,R.RPT_NAME FROM REPORT_MAIN R,REPORT_MAPPING M "+
//                    "WHERE R.HDEPT_ID= 1 AND R.RPT_TYPE<>1 AND M.RPT_CODE=R.RPT_CODE AND UPPER(M.USR_CODE)='"+clsGeneral.USER_NAME+"'"; 	
//            }
//            if (createDataSet(strQuery,"Reports") == null)
//            {
//                dsReport = getDataSet();
//                iNoOfOPRegistrationRep = getRecordCount();
//                strOPRegistrationReports = new string[iNoOfOPRegistrationRep,2];
				
//                for(int i = 0;i<iNoOfOPRegistrationRep;i++)
//                {
//                    strOPRegistrationReports[i,0] = getData(i,"RPT_CODE");
//                    strOPRegistrationReports[i,1] = getData(i,"RPT_NAME");
//                }
//            }

//            //To get available IP Registration reports
//            dsReport.Dispose();
//            dsReport = new DataSet();
//            //strQuery = "SELECT RPT_CODE,RPT_NAME FROM REPORT_MAIN WHERE RPT_MODULE= -1 AND RPT_TYPE<>1"; 
//            if(clsGeneral.RPT_USER_NAME.ToUpper()==clsGeneral.USER_DEPARTMENT.ToUpper())
//            {
//                strQuery = "SELECT R.RPT_CODE,R.RPT_NAME FROM REPORT_MAIN R "+
//                    "WHERE R.HDEPT_ID= -1 AND R.RPT_TYPE<>1";
//            }
//            else
//            {
//                strQuery = "SELECT R.RPT_CODE,R.RPT_NAME FROM REPORT_MAIN R,REPORT_MAPPING M "+
//                    "WHERE R.HDEPT_ID= -1 AND R.RPT_TYPE<>1 AND M.RPT_CODE=R.RPT_CODE AND UPPER(M.USR_CODE)='"+clsGeneral.USER_NAME+"'";
//            }
//            if (createDataSet(strQuery,"Reports") == null)
//            {
//                dsReport = getDataSet();
//                iNoOfIPRegistrationRep = getRecordCount();
//                strIPRegistrationReports = new string[iNoOfIPRegistrationRep,2];
				
//                for(int i = 0;i<iNoOfIPRegistrationRep;i++)
//                {
//                    strIPRegistrationReports[i,0] = getData(i,"RPT_CODE");
//                    strIPRegistrationReports[i,1] = getData(i,"RPT_NAME");
//                }
//            }

//            //To get available Pharmacy reports
//            dsReport.Dispose();
//            dsReport = new DataSet();
//            //if(clsGeneral.RPT_USER_NAME.ToUpper()==clsGeneral.USER_DEPARTMENT.ToUpper())
//            if(sDepartmentTypeId.IndexOf(clsGeneral.DEPARTMENT_PHARMACY.ToString())>=0)
//            {
//                strQuery = "SELECT R.RPT_CODE,R.RPT_NAME FROM REPORT_MAIN R,REPORT_MAPPING M "+
//                    "WHERE R.RPT_MODULE= " + clsGeneral.DEPARTMENT_PHARMACY +" AND R.RPT_TYPE<>1 AND M.RPT_CODE=R.RPT_CODE AND UPPER(M.USR_CODE)='"+clsGeneral.USER_NAME+"'";
//            }
//            else
//            {
//                strQuery = "SELECT R.RPT_CODE,R.RPT_NAME FROM REPORT_MAIN R "+
//                    "WHERE R.RPT_MODULE= " + clsGeneral.DEPARTMENT_PHARMACY +" AND R.RPT_TYPE<>1";
//            }
//            if (createDataSet(strQuery,"Reports") == null)
//            {
//                dsReport = getDataSet();
//                iNoOfPharmaciesRep = getRecordCount();
//                strPharmaciesReports = new string[iNoOfPharmaciesRep,2];
				
//                for(int i = 0;i<iNoOfPharmaciesRep;i++)
//                {
//                    strPharmaciesReports[i,0] = getData(i,"RPT_CODE");
//                    strPharmaciesReports[i,1] = getData(i,"RPT_NAME");
//                }
//            }

//            //To get available Store reports
//            dsReport.Dispose();
//            dsReport = new DataSet();
//            //strQuery = "SELECT RPT_CODE,RPT_NAME FROM REPORT_MAIN WHERE RPT_MODULE=" + clsGeneral.DEPARTMENT_STORE +" AND RPT_TYPE<>1";
//            if(clsGeneral.RPT_USER_NAME.ToUpper()==clsGeneral.USER_DEPARTMENT.ToUpper())
//            {
//                strQuery = "SELECT R.RPT_CODE,R.RPT_NAME FROM REPORT_MAIN R "+
//                    "WHERE R.RPT_MODULE= " + clsGeneral.DEPARTMENT_STORE +" AND R.RPT_TYPE<>1";
						
//            }
//            else
//            {
//                strQuery = "SELECT R.RPT_CODE,R.RPT_NAME FROM REPORT_MAIN R,REPORT_MAPPING M "+
//                    "WHERE R.RPT_MODULE= " + clsGeneral.DEPARTMENT_STORE +" AND R.RPT_TYPE<>1 AND M.RPT_CODE=R.RPT_CODE AND UPPER(M.USR_CODE)='"+clsGeneral.USER_NAME+"'";
//            }
//            if (createDataSet(strQuery,"Reports") == null)
//            {
//                dsReport = getDataSet();
//                iNoOfStoresRep = getRecordCount();
//                strStoresReports = new string[iNoOfStoresRep,2];
				
//                for(int i = 0;i<iNoOfStoresRep;i++)
//                {
//                    strStoresReports[i,0] = getData(i,"RPT_CODE");
//                    strStoresReports[i,1] = getData(i,"RPT_NAME");
//                }
//            }

//            //To get available Lab reports
//            dsReport.Dispose();
//            dsReport = new DataSet();
//            //strQuery = "SELECT RPT_CODE,RPT_NAME FROM REPORT_MAIN WHERE RPT_MODULE=" + clsGeneral.DEPARTMENT_LAB +" AND RPT_TYPE<>1";
//            if(clsGeneral.RPT_USER_NAME.ToUpper()==clsGeneral.USER_DEPARTMENT.ToUpper())
//            {
//                strQuery = "SELECT R.RPT_CODE,R.RPT_NAME FROM REPORT_MAIN R "+
//                    "WHERE R.RPT_MODULE= " + clsGeneral.DEPARTMENT_LAB +" AND R.RPT_TYPE<>1";
//            }
//            else
//            {
//                strQuery = "SELECT R.RPT_CODE,R.RPT_NAME FROM REPORT_MAIN R,REPORT_MAPPING M "+
//                    "WHERE R.RPT_MODULE= " + clsGeneral.DEPARTMENT_LAB +" AND R.RPT_TYPE<>1 AND M.RPT_CODE=R.RPT_CODE AND UPPER(M.USR_CODE)='"+clsGeneral.USER_NAME+"'";
//            }
//            if (createDataSet(strQuery,"Reports") == null)
//            {
//                dsReport = getDataSet();
//                iNoOfLabsRep= getRecordCount();
//                strLabsReports = new string[iNoOfLabsRep,2];
				
//                for(int i = 0;i<iNoOfLabsRep;i++)
//                {
//                    strLabsReports[i,0] = getData(i,"RPT_CODE");
//                    strLabsReports[i,1] = getData(i,"RPT_NAME");
//                }
//            }			

//            //To get available Billing reports
//            dsReport.Dispose();
//            dsReport = new DataSet();
//            //strQuery = "SELECT RPT_CODE,RPT_NAME FROM REPORT_MAIN WHERE RPT_MODULE=" + clsGeneral.DEPARTMENT_BILLING +" AND RPT_TYPE<>1";
//            if(clsGeneral.RPT_USER_NAME.ToUpper()==clsGeneral.USER_DEPARTMENT.ToUpper())
//            {
//                strQuery = "SELECT R.RPT_CODE,R.RPT_NAME FROM REPORT_MAIN R "+
//                    "WHERE R.RPT_MODULE= " + clsGeneral.DEPARTMENT_BILLING +" AND R.RPT_TYPE<>1";
//            }
//            else
//            {
//                strQuery = "SELECT R.RPT_CODE,R.RPT_NAME FROM REPORT_MAIN R,REPORT_MAPPING M "+
//                    "WHERE R.RPT_MODULE= " + clsGeneral.DEPARTMENT_BILLING +" AND R.RPT_TYPE<>1 AND M.RPT_CODE=R.RPT_CODE AND UPPER(M.USR_CODE)='"+ clsGeneral.USER_NAME+"'";
//            }
//            if (createDataSet(strQuery,"Reports") == null)
//            {
//                dsReport = getDataSet();
//                iNoOfBillingRep= getRecordCount();
//                strBillingReports = new string[iNoOfBillingRep,2];
				
//                for(int i = 0;i<iNoOfBillingRep;i++)
//                {
//                    strBillingReports[i,0] = getData(i,"RPT_CODE");
//                    strBillingReports[i,1] = getData(i,"RPT_NAME");
//                }
//            }

//            //To get available Recording reports
//            dsReport.Dispose();
//            dsReport = new DataSet();
//            //strQuery = "SELECT RPT_CODE,RPT_NAME FROM REPORT_MAIN WHERE RPT_MODULE=" + clsGeneral.DEPARTMENT_RECORDING +" AND RPT_TYPE<>1";
//            if(clsGeneral.RPT_USER_NAME.ToUpper()==clsGeneral.USER_DEPARTMENT.ToUpper())
//            {
//                strQuery = "SELECT R.RPT_CODE,R.RPT_NAME FROM REPORT_MAIN R "+
//                    "WHERE R.RPT_MODULE= " + clsGeneral.DEPARTMENT_BILLING +" AND R.RPT_TYPE<>1";
//            }
//            else
//            {
//                strQuery = "SELECT R.RPT_CODE,R.RPT_NAME FROM REPORT_MAIN R,REPORT_MAPPING M "+
//                    "WHERE R.RPT_MODULE= " + clsGeneral.DEPARTMENT_RECORDING +" AND R.RPT_TYPE<>1 AND M.RPT_CODE=R.RPT_CODE AND UPPER(M.USR_CODE)='"+clsGeneral.USER_NAME+"'";
//            }
//            if (createDataSet(strQuery,"Reports") == null)
//            {
//                dsReport = getDataSet();
//                iNoOfReocordingRep = getRecordCount();
//                strRecordingReports = new string [iNoOfReocordingRep,2];

//                for(int i = 0; i<iNoOfReocordingRep; i++)
//                {
//                    strRecordingReports[i,0] = getData(i,"RPT_CODE");
//                    strRecordingReports[i,1] = getData(i,"RPT_NAME");
//                }				
//            }
//        }
		
//        public static int showMeasurement(int iDeptId)
//        {
//            string sql = "SELECT NVL(SHOW_MEAS, 0) AS SHOW_MEAS " +
//                         "FROM HOSPITAL_DEPARTMENTS WHERE HDEPT_ID = " + iDeptId;
//            DataHandling objDH = new DataHandling(sql, "SHOW MEASUREMENT");

//            if (objDH.getRecordCount() > 0)
//            {
//                try
//                {
//                    return Convert.ToInt32(objDH.getData(0,"SHOW_MEAS"));
//                }
//                catch
//                {
//                    return 0;
//                }
//            }else
//                return 0;
//        }

//        public static int getMedicalItemType(int iDeptId)
//        {
//            string sql = "SELECT NVL(STOR_TYPE, " + BOTH_ITEMS + ") AS STOR_TYPE " +
//                         "FROM HOSPITAL_DEPARTMENTS WHERE HDEPT_ID = " + iDeptId;

//            DataHandling objDH = new DataHandling(sql, "ISSUE MEDICAL ITEMS");

//            if (objDH.getRecordCount() > 0)
//            {
//                try
//                {
//                    return Convert.ToInt32(objDH.getData(0,"STOR_TYPE"));
//                }
//                catch
//                {
//                    return BOTH_ITEMS;
//                }
//            }
//            else
//                return BOTH_ITEMS;
//        }

//        public static string getGeneralQueries(int iConstant, string strCriteria)
//        {
//            string sSql = "";

//            switch(iConstant)
//            {
//                case USERLIST:
//                    sSql = 	"SELECT 0 AS USR_ID, '<ALL>' AS NAME FROM DUAL " +
//                            "UNION ALL "  +
//                            "SELECT USR_ID, NAME FROM USERS WHERE STAT <> 2";
//                    break;
//                case DEPTLIST:
//                    sSql =  "SELECT 0 AS HDEPT_ID, '' AS HDEPT_DESC FROM DUAL UNION " +
//                            "SELECT HDEPT_ID, HDEPT_DESC FROM HOSPITAL_DEPARTMENTS WHERE HDEPT_TYPE = 3"; 
//                    break;
//                case LAB_DEPTLIST:
//                    sSql =  "SELECT 0 AS HDEPT_ID, '' AS HDEPT_DESC FROM DUAL UNION " +
//                        "SELECT HDEPT_ID, HDEPT_DESC FROM HOSPITAL_DEPARTMENTS WHERE HDEPT_TYPE = " + DEPARTMENT_LAB; 
//                    break;
//                case GENERAL_CHRG_DEBTLIST:
//                    sSql = "SELECT BILL_TYPE_ID, BILL_TYPE_NAME FROM GENERAL_BILL_TYPE";
//                    break;
//            }
//            return sSql;
//        }
//        public static string getUserList(int iConstant,int ID)
//        {
//            string sSql="";
//            switch(iConstant)
//            {
//                case USERLIST:
//                    sSql ="SELECT  USR_ID, NAME FROM USERS  WHERE hdept_id = " + ID + " AND STAT <> 2 " ;
//                    break;
//            }
//            return sSql;
//        }
//        // --- To get the queries for updating the Selling Price of the Drugs ----

//        public static string getDrugSPQueries(int iConstant)
//        {
//            string sSql = "";

//            switch(iConstant)
//            {
//                case STORE_PHARMACY_DEPT:
//                    sSql =	"SELECT HDEPT_ID, HDEPT_DESC FROM HOSPITAL_DEPARTMENTS WHERE HDEPT_TYPE " +
//                            "IN (" + DEPARTMENT_PHARMACY + ", " + DEPARTMENT_STORE + ") " +
//                            "ORDER BY HDEPT_TYPE DESC";
//                    break;
//                case UPDATE_DRUGS_SP:
//                    sSql = 	"SELECT to_number(ROWNUM,9999999) AS \"#\", IT.TYPE_DESC AS \"ITEM TYPE\", " +
//                            "I.ITEM_DESC AS \"ITEM NAME\", 0 AS \"SELLING PRICE\", " +
//                            "I.ITEM_ID, '' AS \"REMARKS\" FROM ITEM I, ITEM_TYPE IT " +
//                            "WHERE IT.TYPE_ID = I.TYPE_ID AND I.ITEM_ID = -1 ORDER BY I.ITEM_ID";
//                    break;
//                case UPDATE_DRUGS_BATCH_SP:
//                    sSql = 	"SELECT TO_number(ROWNUM,9999999) AS \"#\", IT.TYPE_DESC AS \"ITEM TYPE\", " +
//                            "I.ITEM_DESC AS \"ITEM NAME\", DS.BACH_NUM AS \"BATCH NO\", " +
//                            "DS.UNT_PRIC AS \"SELLING PRICE\", DS.NET_QTY_MEAS_SYMB AS \"MEASUREMENT\", " +
//                            "DS.NET_QTY_MEAS_ID AS \"MEASURE ID\", I.ITEM_ID, '' AS \"REMARKS\" " +
//                            "FROM ITEM I, ITEM_TYPE IT, DEPARTMENT_STORE DS WHERE DS.HDEPT_ID = 0 " + 
//                            "AND DS.ITEM_ID = I.ITEM_ID AND IT.TYPE_ID = I.TYPE_ID AND DS.NET_QTY <> 0 " +
//                            "AND DS.TRAN_ID IN (SELECT MAX(TRAN_ID) FROM ITEM, DEPARTMENT_STORE WHERE " +
//                            "DEPARTMENT_STORE.HDEPT_ID = 0 AND DEPARTMENT_STORE.ITEM_ID = ITEM.ITEM_ID GROUP BY " +
//                            "DEPARTMENT_STORE.ITEM_ID, DEPARTMENT_STORE.BACH_NUM) ORDER BY DS.ITEM_ID";
//                    break;
//                case DEPARTMENT_TYPE:
//                    sSql =	"SELECT NVL(HDEPT_TYPE, 0) AS HDEPT_TYPE FROM HOSPITAL_DEPARTMENTS";
//                    break;
//                case DRUG_TYPE:
//                    sSql =	"SELECT TYPE_ID, TYPE_DESC FROM ITEM_TYPE";
//                    break;
//                case DRUG_NAME:
//                    sSql =	"SELECT IT.TYPE_DESC, I.ITEM_ID, I.ITEM_DESC " +
//                            "FROM ITEM I, ITEM_TYPE IT " +
//                            "WHERE IT.TYPE_ID = I.TYPE_ID";
//                    break;
//                case STORE_BATCH_NUMBER:
////					sSql =	"SELECT IT.TYPE_DESC, I.ITEM_DESC, S.BACH_NUM, NVL(S.UNT_PRIC, 0) " +
////							"AS UNT_PRIC, I.ITEM_ID, nvl(S.NET_QTY_MEAS_SYMB,'Num') AS MEASURE, " +
////							"nvl(S.NET_QTY_MEAS_ID,1) AS MEASURE_ID " +
////							"FROM ITEM I, ITEM_TYPE IT, STORE S WHERE S.HDEPT_ID = <DEPT> " +
////							"AND S.ITEM_ID = I.ITEM_ID AND IT.TYPE_ID = I.TYPE_ID " +
////							"AND S.TRAN_ID IN (SELECT MAX(TRAN_ID) FROM ITEM, STORE WHERE " +
////							"STORE.HDEPT_ID = <DEPT> AND STORE.ITEM_ID = ITEM.ITEM_ID GROUP BY " +
////							"STORE.ITEM_ID, STORE.BACH_NUM) ORDER BY S.ITEM_ID";
//                    sSql =	"select " +
//                                "it.type_desc, i.item_desc, s.bach_num, nvl(s.unt_pric,0) as unt_pric, " +
//                                "i.item_id, nvl(s.meas_symb,'Num') as measure, " +
//                                "nvl(s.meas_id,1) as measure_id " +
//                            "from item i, item_type it, store_stock s " +
//                            "where s.item_id = i.item_id and i.type_id = it.type_id and s.hdept_id = <DEPT> " +
//                            "order by s.item_id";
//                    break;
//                case PHARMACY_BATCH_NUMBER:
////					sSql =	"SELECT IT.TYPE_DESC, I.ITEM_DESC, DS.BACH_NUM, NVL(DS.UNT_PRIC, 0) " +
////							"AS UNT_PRIC, I.ITEM_ID, nvl(DS.NET_QTY_MEAS_SYMB,'Num') AS MEASURE, " +
////							"nvl(DS.NET_QTY_MEAS_ID,1) AS MEASURE_ID " +
////							"FROM ITEM I, ITEM_TYPE IT, DEPARTMENT_STORE DS WHERE DS.HDEPT_ID = <DEPT> " +
////							"AND DS.ITEM_ID = I.ITEM_ID AND IT.TYPE_ID = I.TYPE_ID " +
////							"AND DS.TRAN_ID IN (SELECT MAX(TRAN_ID) FROM ITEM, DEPARTMENT_STORE WHERE " +
////							"DEPARTMENT_STORE.HDEPT_ID = <DEPT> AND DEPARTMENT_STORE.ITEM_ID = ITEM.ITEM_ID GROUP BY " +
////							"DEPARTMENT_STORE.ITEM_ID, DEPARTMENT_STORE.BACH_NUM) ORDER BY DS.ITEM_ID";
//                    sSql =	"select " +
//                                "it.type_desc, i.item_desc, ds.bach_num, nvl(ds.unt_pric,0) as unt_pric, " +
//                                "i.item_id, nvl(ds.meas_symb,'Num') as measure, " +
//                                "nvl(ds.meas_id,1) as measure_id " +
//                            "from item i, item_type it, dept_store_stock ds " +
//                            "where ds.item_id = i.item_id and i.type_id = it.type_id and ds.hdept_id = <DEPT> " +
//                            "order by ds.item_id";
//                    break;
//                case STORE_DRUGS_BATCH_NO:
//                    sSql =	"SELECT to_number(ROWNUM,9999999) AS \"#\", S.BACH_NUM AS \"BATCH NO\", '0' AS \"SELECT\" " +
//                            "FROM STORE S " +
//                            "WHERE S.HDEPT_ID = <DEPT> AND S.ITEM_ID = <ITEM> AND S.NET_QTY <> 0 " +
//                            "AND S.TRAN_ID IN (SELECT MAX(TRAN_ID) FROM STORE WHERE " +
//                            "STORE.HDEPT_ID = <DEPT> AND STORE.ITEM_ID = <ITEM> GROUP BY " +
//                            "STORE.ITEM_ID, STORE.BACH_NUM)";
//                    break;
//                case PHARMACY_DRUGS_BATCH_NO:
//                    sSql =	"SELECT S.BACH_NUM AS \"BATCH NO\", DS.BACH_NUM AS \"BATCH NO\", '0' AS \"SELECT\" " +
//                            "FROM DEPARTMENT_STORE DS " +
//                            "WHERE DS.HDEPT_ID = <DEPT> AND DS.ITEM_ID = <ITEM> AND DS.NET_QTY <> 0 " +
//                            "AND DS.TRAN_ID IN (SELECT MAX(TRAN_ID) FROM DEPARTMENT_STORE WHERE " +
//                            "DEPARTMENT_STORE.HDEPT_ID = <DEPT> AND DEPARTMENT_STORE.ITEM_ID = <ITEM> GROUP BY " +
//                            "DEPARTMENT_STORE.ITEM_ID, DEPARTMENT_STORE.BACH_NUM)";
//                    break;
//            }
//            return sSql;
//        }

//        // -----------------------------------------------------------------------
//        public static string getDayCollectionQueries(string sReportDate, int nUserId,
//            int nHDepartmentId, int iConstant, int iModuleId, int nTDepartmentId)
//        {
//            string sSql = "";

//            switch(iConstant)
//            {
//                case PHARMACYDAYCOLLECTION:
//                    sSql =	"select " +
//                                "p.bill_numb as \"Bill No\", p.pat_name as \"Name\", " +
//                                "to_char(nvl(sum(p.charges),0), '9999999990.00') as \"Charges\", " +
//                                "to_char(nvl(sum(p.Payment),0), '9999999990.00') as \"Payment\", " +
//                                "to_char(nvl(sum(p.Discount),0), '9999999990.00') as \"Discount\", " +
//                                "to_char(nvl(max(p.due), '9999999990.00')) as \"Due\", " +
//                                "to_char(nvl(sum(p.DueCollection),0), '9999999990.00') as \"Due Collection\", " +
//                                "to_char(nvl(sum(p.Refunds),0), '9999999990.00') as \"Refunds\" " +
//                            "from " +
//                                "(select " +
//                                    "t.bill_numb, t.pat_name, t.due, " +
//                                    "case when t.trans in (" + clsAdminConstants.PHARMACY_ISSUE_MEDICINE + "," + clsAdminConstants.PHARMACY_SERVICE_CHARGE + ") then sum(abs(t.chrg_amnt)) else 0 end as Charges, " +
//                                    "case when t.trans in (" + clsAdminConstants.PHARMACY_MEDICINE_DISCOUNT + ") then sum(abs(t.chrg_amnt)) else 0 end as Discount, " +
//                                    "case when t.trans in (" + clsAdminConstants.PHARMACY_MEDICINE_PAYMENT + "," + clsAdminConstants.PHARMACY_PAY_FROM_DEPOSIT + ") then sum(abs(t.pay_amnt)) else 0 end as Payment, " +
//                                    "case when t.trans in (" + clsAdminConstants.PHARMACY_DUE_COLLECTION + ") then sum(abs(t.pay_amnt)) else 0 end as DueCollection, " +
//                                    "case when t.trans in (" + clsAdminConstants.PHARMACY_MEDICINE_REFUND + ") then sum(abs(t.pay_amnt)) else 0 end as Refunds " +
//                                "from " +
//                                    "(select " +
//                                        "pbd.chrg_amnt, pbd.pay_amnt, pb.bill_id, pbd.acc_code, " +
//                                        "am.trans, pb.bill_numb, pb.pat_name, pb.due " +
//                                    "from " +
//                                        "pharmacy_bill pb, pharmacy_bill_details pbd, account_mapping am " +
//                                    "where " +
//                                        "pbd.bill_id = pb.bill_id and pbd.acc_code = am.acc_code " +
//                                        "and am.modu = " + iModuleId + " and pbd.tran_usr_id = " + nUserId + 
//                                        " and pbd.hdept_id = " + nHDepartmentId + " and pbd.tdept_id = " + nHDepartmentId + 
//                                        " and pbd.coll_id is null) t " +
//                                "group by " +
//                                    "t.bill_numb, t.pat_name,t.trans, t.due) p " +
//                            "group by " +
//                                "p.bill_numb, p.pat_name";
//                    break;
//                case PHARMACY_ALL_COLLECTION:
//                    sSql =	"select " +
//                                "p.bill_numb as \"Bill No\", p.pat_name as \"Name\", " +
//                                "to_char(nvl(sum(p.charges),0), '9999999990.00') as \"Charges\", " +
//                                "to_char(nvl(sum(p.Payment),0), '9999999990.00') as \"Payment\", " +
//                                "to_char(nvl(sum(p.Discount),0), '9999999990.00') as \"Discount\", " +
//                                "to_char(nvl(max(p.due),0), '9999999990.00') as \"Due\", " +
//                                "to_char(nvl(sum(p.DueCollection),0), '9999999990.00') as \"Due Collection\", " +
//                                "to_char(nvl(sum(p.Refunds),0), '9999999990.00') as \"Refunds\" " +
//                            "from " +
//                                "(select " +
//                                    "t.bill_numb, t.pat_name, t.due, " +
//                                    "case when t.trans in (" + clsAdminConstants.PHARMACY_ISSUE_MEDICINE + "," + clsAdminConstants.PHARMACY_SERVICE_CHARGE + ") then sum(abs(t.chrg_amnt)) else 0 end as Charges, " +
//                                    "case when t.trans in (" + clsAdminConstants.PHARMACY_MEDICINE_DISCOUNT + ") then sum(abs(t.chrg_amnt)) else 0 end as Discount, " +
//                                    "case when t.trans in (" + clsAdminConstants.PHARMACY_MEDICINE_PAYMENT + "," + clsAdminConstants.PHARMACY_PAY_FROM_DEPOSIT + ") then sum(abs(t.pay_amnt)) else 0 end as Payment, " +
//                                    "case when t.trans in (" + clsAdminConstants.PHARMACY_DUE_COLLECTION + ") then sum(abs(t.pay_amnt)) else 0 end as DueCollection, " +
//                                    "case when t.trans in (" + clsAdminConstants.PHARMACY_MEDICINE_REFUND + ") then sum(abs(t.pay_amnt)) else 0 end as Refunds " +
//                                "from " +
//                                    "(select " +
//                                        "pbd.chrg_amnt, pbd.pay_amnt, pb.bill_id, pbd.acc_code, " +
//                                        "am.trans, pb.bill_numb, pb.pat_name, pb.due " +
//                                    "from " +
//                                        "pharmacy_bill pb, pharmacy_bill_details pbd, account_mapping am " +
//                                    "where " +
//                                        "pbd.bill_id = pb.bill_id and pbd.acc_code = am.acc_code " +
//                                        "and am.modu = " + iModuleId + 
//                                        " and pbd.hdept_id = " + nHDepartmentId + " and pbd.tdept_id = " + nHDepartmentId + 
//                                        " and pbd.coll_id is null) t " +
//                                "group by " +
//                                    "t.bill_numb, t.pat_name,t.trans, t.due) p " +
//                            "group by " +
//                                "p.bill_numb, p.pat_name";
//                    break;
//                case LABBILLDAYCOLLECTION:
//                    sSql = "SELECT DISTINCT LB.LAB_BILL_NUMB AS \"BILL NO\", LB.PAT_NAME AS \"Name\", " +
       
//                            "(SELECT TO_CHAR(NVL(SUM(abs(LBD.CHRG_AMNT)),0), '9999999990.00') " +
//                            "FROM LABORATORY_BILLS_DETAILS LBD, ACCOUNTHEADS AH " +
//                            "WHERE LBD.ACCH_ID = AH.ACCH_ID AND AH.ACC_TYPE = 1 " +
//                            "AND AH.TRAN_TYPE = 2 AND LBD.LAB_BILL_TRAN_ID = " +
//                            "LB.LAB_BILL_TRAN_ID AND LBD.TRAN_USR_ID = LBD.TRAN_USR_ID" + 
//                            //Commented by James on 15-Sep-2006 to show the charges for the bills generated in Laboratory
//                            //" AND LBD.HDEPT_ID = " + nHDepartmentId + " AND LBD.TDEPT_ID = " + nTDepartmentId +
//                            ") AS \"CHARGES\", " +
					       
//                            "(SELECT TO_CHAR(NVL(SUM(LBD.PAY_AMNT),0), '9999999990.00') " +
//                            "FROM LABORATORY_BILLS_DETAILS LBD, ACCOUNT_MAPPING AM " +
//                            "WHERE AM.ACCH_ID = LBD.ACCH_ID " +
//                            "AND AM.TRANS = " + clsAdminConstants.LAB_PAYMNET +
//                            " AND AM.MODU = " + iModuleId +
//                            " AND LBD.LAB_BILL_TRAN_ID = LB.LAB_BILL_TRAN_ID " +
//                            "AND LBD.TRAN_USR_ID = LBD.TRAN_USR_ID AND LBD.HDEPT_ID = " + nHDepartmentId + " AND LBD.TDEPT_ID = " + nTDepartmentId + ") AS \"PAYMENT\", " +

//                            "(SELECT TO_CHAR(NVL(SUM(abs(LBD.CHRG_AMNT)),0), '9999999990.00') " +
//                            "FROM LABORATORY_BILLS_DETAILS LBD, ACCOUNTHEADS AH " +
//                            "WHERE LBD.ACCH_ID = AH.ACCH_ID " +
//                            "AND AH.ACC_TYPE = 1 " +
//                            "AND AH.TRAN_TYPE = 1 " +
//                            "AND LBD.LAB_BILL_TRAN_ID = LB.LAB_BILL_TRAN_ID " +
//                            "AND LBD.TRAN_USR_ID = LBD.TRAN_USR_ID AND LBD.HDEPT_ID = " + nHDepartmentId + " AND LBD.TDEPT_ID = " + nTDepartmentId + ") AS \"DISCOUNT\", " +
					        
//                            "TO_CHAR((SELECT NVL(SUM(LBD.CHRG_AMNT),0) " +
//                            "FROM LABORATORY_BILLS_DETAILS LBD " +
//                            "WHERE LBD.LAB_BILL_TRAN_ID = LB.LAB_BILL_TRAN_ID " +
//                            "AND LBD.TRAN_USR_ID = LBD.TRAN_USR_ID " +
//                            //Commented by James on 15-Sep-2006 to show the due for the bills generated in Laboratory
//                            //"AND LBD.HDEPT_ID = " + nHDepartmentId + " AND LBD.TDEPT_ID = " + nTDepartmentId +
//                            ") - " +
//                            "(SELECT NVL(SUM(LBD.PAY_AMNT),0) " +
//                            "FROM LABORATORY_BILLS_DETAILS LBD, ACCOUNT_MAPPING AM " +
//                            "WHERE AM.ACCH_ID = LBD.ACCH_ID " +
//                            //By James on 22-09-2008, Added clsAdminConstants.LAB_ADVANCE in AM.TRANS
//                            "AND AM.TRANS IN (" + clsAdminConstants.LAB_PAYMNET + ", " + 
//                            clsAdminConstants.LAB_DUE_COLLECTION + ", " + clsAdminConstants.LAB_ADVANCE +") " +
//                            "AND AM.MODU = " + iModuleId + 
//                            " AND LBD.LAB_BILL_TRAN_ID = LB.LAB_BILL_TRAN_ID " +
//                            "AND LBD.TRAN_USR_ID = LBD.TRAN_USR_ID AND LBD.HDEPT_ID = " + nHDepartmentId + " AND LBD.TDEPT_ID = " + nTDepartmentId + "), '9999999990.00') AS \"DUE\", " +

//                            "(SELECT to_char(NVL(SUM(LBD.PAY_AMNT),0), '9999999990.00') " +
//                            "FROM LABORATORY_BILLS_DETAILS LBD,ACCOUNT_MAPPING AM " +
//                            "WHERE AM.ACCH_ID = LBD.ACCH_ID " +
//                            "AND AM.TRANS = " + clsAdminConstants.LAB_DUE_COLLECTION +
//                            " AND AM.MODU = " + iModuleId +
//                            " AND LBD.LAB_BILL_TRAN_ID = LB.LAB_BILL_TRAN_ID " +
//                            "AND LBD.TRAN_USR_ID = LBD.TRAN_USR_ID AND LBD.HDEPT_ID = " + nHDepartmentId + " AND LBD.TDEPT_ID = " + nTDepartmentId + ") AS \"DUE COLLECTION\", " +
					        
//                            "(SELECT to_char(NVL(SUM(LBD.PAY_AMNT)*-1,0), '9999999990.00') " +
//                            "FROM LABORATORY_BILLS_DETAILS LBD, ACCOUNTHEADS AH " +
//                            "WHERE LBD.ACCH_ID = AH.ACCH_ID " +
//                            "AND AH.ACC_TYPE = 2 " +
//                            "AND AH.TRAN_TYPE = 2 " +
//                            "AND LBD.LAB_BILL_TRAN_ID = LB.LAB_BILL_TRAN_ID " +
//                            "AND LBD.TRAN_USR_ID = LBD.TRAN_USR_ID AND LBD.HDEPT_ID = " + nHDepartmentId + " AND LBD.TDEPT_ID = " + nTDepartmentId + ") AS \"REFUNDS\" " +

//                            "FROM LABORATORY_BILLS LB, LABORATORY_BILLS_DETAILS LBD " +
//                            "WHERE LB.COLL_ID IS NULL AND LBD.COLL_ID IS NULL " +
//                            "AND LB.LAB_BILL_TRAN_ID = LBD.LAB_BILL_TRAN_ID " +							
//                            (nUserId == 0 ? "" : ("AND LBD.TRAN_USR_ID = " + nUserId)) +
//                            " AND LBD.HDEPT_ID = " + nHDepartmentId +
//                            " AND LBD.TDEPT_ID = " + nTDepartmentId + " AND LB.LAB_BILL_NUMB IS NOT NULL " +
//                            "ORDER BY LB.LAB_BILL_NUMB";
//                    break;
//                case GENERAL_CHRG_DAYCOLLECTION:
//                    //JI Problem of cancelling old bill showing Charges and Payments should be shown Refund only
//                    //Added " AND GBD.Coll_Id is null" in inner query of each field.
//                    sSql = "SELECT DISTINCT GB.BILL_NUMB AS \"BILL NO\", GB.PAT_NAME AS \"Name\", " +

//                        "(SELECT TO_CHAR(NVL(SUM(abs(GBD.CHRG_AMNT)),0), '9999999990.00') FROM " +
//                        "GENERAL_BILL_DETAILS GBD, ACCOUNTHEADS AH WHERE GBD.ACCH_ID = AH.ACCH_ID AND " +
//                        "AH.ACC_TYPE = 1 AND AH.TRAN_TYPE = 2 AND GBD.BILL_ID = GB.BILL_ID AND " +
//                        "GBD.TRAN_USR_ID = GBD.TRAN_USR_ID AND GBD.TDEPT_ID = " + nTDepartmentId +
//                        " AND GBD.Coll_Id is null ) AS \"CHARGES\", " +

//                        "(SELECT TO_CHAR(NVL(SUM(GBD.PAY_AMNT),0), '9999999990.00') FROM GENERAL_BILL_DETAILS GBD, " +
//                        "GENERAL_BILL_ACCOUNT_MAPPING GBAM WHERE GBAM.ACCH_ID = GBD.ACCH_ID AND GBAM.TRANS = " +
//                        clsAdminConstants.GENERAL_PAYMENT + " AND GBAM.GENR_BILL_TYPE_ID = " + nHDepartmentId +
//                        " AND GBD.BILL_ID = GB.BILL_ID AND GBD.TRAN_USR_ID = GBD.TRAN_USR_ID " +
//                        "AND GBAM.HDEPT_ID = " + nTDepartmentId + " AND GBD.Coll_Id is null) AS \"PAYMENT\", " +

//                        "(SELECT TO_CHAR(NVL(SUM(abs(GBD.CHRG_AMNT)),0), " +
//                        "'9999999990.00') FROM GENERAL_BILL_DETAILS GBD, ACCOUNTHEADS AH WHERE " +
//                        "GBD.ACCH_ID = AH.ACCH_ID AND AH.ACC_TYPE = 1 AND AH.TRAN_TYPE = 1 AND " +
//                        "GBD.BILL_ID = GB.BILL_ID AND GBD.TRAN_USR_ID = GBD.TRAN_USR_ID" +
//                        " AND GBD.TDEPT_ID = " + nTDepartmentId + " AND GBD.Coll_Id is null) AS \"DISCOUNT\", " +

//                        "TO_CHAR((SELECT NVL(SUM(GBD.CHRG_AMNT),0) FROM GENERAL_BILL_DETAILS GBD " +
//                        "WHERE GBD.BILL_ID = GB.BILL_ID AND GBD.TRAN_USR_ID = GBD.TRAN_USR_ID" +
//                        " AND GBD.TDEPT_ID = " + nTDepartmentId + " AND GBD.Coll_Id is null) - " +
//                        "(SELECT NVL(SUM(GBD.PAY_AMNT),0) FROM GENERAL_BILL_DETAILS GBD, GENERAL_BILL_ACCOUNT_MAPPING GBAM " +
//                        "WHERE GBAM.ACCH_ID = GBD.ACCH_ID AND (GBAM.TRANS = " + clsAdminConstants.GENERAL_PAYMENT +
//                        " OR GBAM.TRANS = " + clsAdminConstants.GENERAL_DUE_COLLECTION + ") AND GBAM.GENR_BILL_TYPE_ID = " +
//                        nHDepartmentId +  " AND GBD.BILL_ID = GB.BILL_ID AND GBD.TRAN_USR_ID = GBD.TRAN_USR_ID AND GBD.Coll_Id is null" +
//                        " AND GBAM.HDEPT_ID = " + nTDepartmentId + "), '9999999990.00') AS \"DUE\", " +

//                        "(SELECT to_char(NVL(SUM(GBD.PAY_AMNT),0), '9999999990.00') FROM " +
//                        "GENERAL_BILL_DETAILS GBD,GENERAL_BILL_ACCOUNT_MAPPING GBAM WHERE GBAM.ACCH_ID = GBD.ACCH_ID AND " +
//                        "GBAM.TRANS = " + clsAdminConstants.GENERAL_DUE_COLLECTION + " AND GBAM.GENR_BILL_TYPE_ID = " +
//                        nHDepartmentId +  " AND GBD.BILL_ID = GB.BILL_ID AND GBD.TRAN_USR_ID = GBD.TRAN_USR_ID AND " +
//                        "GBAM.HDEPT_ID = " + nTDepartmentId + "  AND  GBD.Coll_Id is null) AS \"DUE COLLECTION\", " +

//                        "(SELECT to_char(NVL(SUM(GBD.PAY_AMNT)*-1,0), " +
//                        "'9999999990.00') FROM GENERAL_BILL_DETAILS GBD, ACCOUNTHEADS AH WHERE " +
//                        "GBD.ACCH_ID = AH.ACCH_ID AND AH.ACC_TYPE = 2 AND AH.TRAN_TYPE = 2 AND " +
//                        "GBD.BILL_ID = GB.BILL_ID AND GBD.TRAN_USR_ID = GBD.TRAN_USR_ID" +
//                        " AND GBD.TDEPT_ID = " + nTDepartmentId + " AND  GBD.Coll_Id is null) AS \"REFUNDS\" " +

//                        "FROM GENERAL_BILL GB, GENERAL_BILL_DETAILS GBD " +
//                        "WHERE GB.COLL_ID IS NULL AND " +
//                        "GBD.COLL_ID IS NULL AND GB.BILL_ID = GBD.BILL_ID " +
//                        (nUserId==0 ? "" : "AND GBD.TRAN_USR_ID = " + nUserId) +
//                        " AND GBD.TDEPT_ID = " + nTDepartmentId +
//                        " AND GB.BILL_TYPE_ID = " + nHDepartmentId + " AND GB.BILL_NUMB IS NOT NULL " +
//                        "ORDER BY GB.BILL_NUMB";
//                    break;
//                case IPBILL_ALLCOLLECTION: case IPBILL_DAYCOLLECTION:
//                    clsGeneral.showAdvAdjused();
//                    sSql = "SELECT DISTINCT WB.WARD_BILL_NUMB AS \"BILL NO\", PT.NAME AS \"Name\", " +
//                        "(SELECT TO_CHAR(NVL(SUM(abs(WBD.CHRG_AMNT)),0), '9999999990.00') " +
//                        "FROM WARD_BILL_DETAILS WBD, ACCOUNTHEADS AH " +
//                        "WHERE WBD.ACCH_ID = AH.ACCH_ID AND AH.ACC_TYPE = 1 AND AH.TRAN_TYPE = 2 AND " +
//                        "WBD.WARD_TRAN_ID = WB.WARD_TRAN_ID) AS \"CHARGES\", " +
//                        "(SELECT TO_CHAR(NVL(SUM(WBD.PAY_AMNT),0), '9999999990.00') " +
//                        "FROM WARD_BILL_DETAILS WBD, ACCOUNT_MAPPING AM " +
//                        "WHERE AM.ACCH_ID = WBD.ACCH_ID AND AM.TRANS IN (" + clsAdminConstants.WARD_PAYMENT +
//                        (SHOW_ADV_ADJ_IN_PAYMENT == 1 ? ", " + clsAdminConstants.WARD_TRANS_FROM_ADV : "") +  
//                        ") AND AM.MODU = " + iModuleId + " AND " +
//                        "WBD.WARD_TRAN_ID = WB.WARD_TRAN_ID" +
//                        (iConstant==IPBILL_DAYCOLLECTION ? " AND WBD.RECE_NUMB IS NULL" : "") + ") AS \"PAYMENT\", " +
//                        "(SELECT TO_CHAR(NVL(SUM(WBD.PAY_AMNT),0), '9999999990.00') " +
//                        "FROM WARD_BILL_DETAILS WBD, ACCOUNT_MAPPING AM " +
//                        "WHERE AM.ACCH_ID = WBD.ACCH_ID AND AM.TRANS =" + clsAdminConstants.WARD_TRANS_FROM_ADV +  
//                        " AND AM.MODU = " + iModuleId + " AND " +
//                        "WBD.WARD_TRAN_ID = WB.WARD_TRAN_ID" +
//                        (iConstant==IPBILL_DAYCOLLECTION ? " AND WBD.RECE_NUMB IS NULL" : "") + ") AS \"ADV ADJ\", " +
//                        "(SELECT TO_CHAR(NVL(SUM(abs(WBD.CHRG_AMNT)),0), '9999999990.00') " +
//                        "FROM WARD_BILL_DETAILS WBD, ACCOUNTHEADS AH " +
//                        "WHERE WBD.ACCH_ID = AH.ACCH_ID AND AH.ACC_TYPE = 1 AND AH.TRAN_TYPE = 1 AND " +
//                        "WBD.WARD_TRAN_ID = WB.WARD_TRAN_ID) AS \"DISCOUNT\", " +
//                        "TO_CHAR((SELECT NVL(SUM(WBD.CHRG_AMNT),0) FROM WARD_BILL_DETAILS WBD " +
//                        "WHERE WBD.WARD_TRAN_ID = WB.WARD_TRAN_ID) - " +
//                        "(SELECT NVL(SUM(WBD.PAY_AMNT),0) " +
//                        "FROM WARD_BILL_DETAILS WBD, ACCOUNT_MAPPING AM " +
//                        "WHERE AM.ACCH_ID = WBD.ACCH_ID AND AM.TRANS IN (" + clsAdminConstants.WARD_PAYMENT +
//                        ", " + clsAdminConstants.WARD_DUE_COLLECTION + ", " + clsAdminConstants.WARD_TRANS_FROM_ADV + ") " + 
//                        "AND AM.MODU = " + iModuleId + " AND " +
//                        "WBD.WARD_TRAN_ID = WB.WARD_TRAN_ID), '9999999990.00') AS \"DUE\", " +
//                        "(SELECT to_char(NVL(SUM(WBD.PAY_AMNT),0), '9999999990.00') " +
//                        "FROM WARD_BILL_DETAILS WBD,ACCOUNT_MAPPING AM " +
//                        "WHERE AM.ACCH_ID = WBD.ACCH_ID AND AM.TRANS = " +
//                        clsAdminConstants.WARD_DUE_COLLECTION + " AND AM.MODU = " + iModuleId + " AND " +
//                        "WBD.WARD_TRAN_ID = WB.WARD_TRAN_ID) AS \"DUE COLLECTION\", " +
//                        "(SELECT to_char(NVL(SUM(WBD.PAY_AMNT)*-1,0), '9999999990.00') " +
//                        "FROM WARD_BILL_DETAILS WBD, ACCOUNTHEADS AH " +
//                        "WHERE WBD.ACCH_ID = AH.ACCH_ID AND AH.ACC_TYPE = 2 AND AH.TRAN_TYPE = 2 AND " +
//                        "WBD.WARD_TRAN_ID = WB.WARD_TRAN_ID) AS \"REFUNDS\" " +
//                        "FROM WARD_BILL WB, WARD_BILL_DETAILS WBD, PATIENT PT, VISIT_DETAILS VD " +
//                        "WHERE WB.VIST_NUMB = VD.VIST_NUMB AND VD.REG_NUMB = PT.REG_NUMB " +
//                        "AND WBD.COLL_ID IS NULL AND WB.WARD_TRAN_ID = WBD.WARD_TRAN_ID " +
//                        (nUserId==0 ? "" : "AND WBD.TRAN_USR_ID = " + nUserId) +
//                        " AND WB.WARD_BILL_NUMB IS NOT NULL AND NVL(WB.IS_FINA,1) = 1 " +
//                        " AND WBD.HDEPT_ID = " + nTDepartmentId +
//                        " AND WBD.TDEPT_ID = " + nTDepartmentId +
//                        " ORDER BY WB.WARD_BILL_NUMB";
//                    break;
//                case IPBILL_ADVANCE_PAYMENT:
////					sSql = "SELECT " +
////							"DISTINCT PT.REG_NUMB AS \"REG NO\", PT.NAME AS \"Name\", WBD.RECE_NUMB AS \"RECEIPT NO\", " +
////							"TO_CHAR(CASE WHEN AM.TRANS=" + clsAdminConstants.WARD_PAYMENT +
////							" THEN NVL(SUM(WBD.PAY_AMNT),0) ELSE 0 END, '9999999990.00') AS PAYMENT, " +
////							"TO_CHAR(CASE WHEN AM.TRANS=" + clsAdminConstants.WARD_REFUNDS +
////							" THEN ABS(NVL(SUM(WBD.PAY_AMNT),0)) ELSE 0 END, '9999999990.00') AS REFUNDS " +
////						"FROM " +
////							"WARD_BILL WB, WARD_BILL_DETAILS WBD, PATIENT PT, VISIT_DETAILS VD, ACCOUNT_MAPPING AM " +
////						"WHERE " +
////							"WB.VIST_NUMB = VD.VIST_NUMB AND VD.REG_NUMB = PT.REG_NUMB " +
////							"AND WBD.COLL_ID IS NULL AND WB.WARD_TRAN_ID = WBD.WARD_TRAN_ID " +
////							"AND WBD.HDEPT_ID = " + nTDepartmentId +
////							" AND WBD.TDEPT_ID = " + nTDepartmentId + " " +
////							(nUserId==0 ? "" : "AND WBD.TRAN_USR_ID = " + nUserId) +
////							"AND AM.ACCH_ID = WBD.ACCH_ID AND AM.MODU = " + iModuleId +
////							" AND (WBD.RECE_NUMB IS NOT NULL OR AM.TRANS=" + clsAdminConstants.WARD_REFUNDS + ") " +
////							"AND AM.TRANS IN (" + clsAdminConstants.WARD_PAYMENT + ", "
////							+ clsAdminConstants.WARD_REFUNDS + ") " +
////							"GROUP BY PT.REG_NUMB, PT.NAME, WBD.RECE_NUMB, AM.TRANS";

//                    sSql = "SELECT DISTINCT PT.REG_NUMB AS \"REG NO\", PT.NAME AS \"Name\", " +
//                        "WBD.RECE_NUMB AS \"RECEIPT NO\", " +
//                        " case when WBD.Pay_Amnt > 0 then WBD.Pay_Amnt else 0             end as PAYMENT," +
//                        " case when WBD.Pay_Amnt > 0 then  0           else (WBD.Pay_Amnt) * -1  end as REFUNDS " +
//                        "FROM WARD_BILL WB, WARD_BILL_DETAILS WBD, PATIENT PT, VISIT_DETAILS VD, ACCOUNT_MAPPING AM " +
//                        "WHERE WB.VIST_NUMB = VD.VIST_NUMB AND VD.REG_NUMB = PT.REG_NUMB " +
//                        "AND WBD.COLL_ID IS NULL AND WB.WARD_TRAN_ID = WBD.WARD_TRAN_ID " +
//                        (nUserId==0 ? "" : "AND WBD.TRAN_USR_ID = " + nUserId) +
//                        " AND WBD.HDEPT_ID = " + nTDepartmentId +
//                        " AND WBD.TDEPT_ID = " + nTDepartmentId +
//                        " AND AM.ACCH_ID = WBD.ACCH_ID"+
//                        " and WBD.RECE_NUMB IS NOT NULL";
//                    break;
//                case ADVANCE_DAYCOLLECTION:
//                    sSql = "SELECT PT.REG_NUMB AS \"REG NO\", PT.NAME AS \"NAME\", WA.RECE_NUMB AS \"RECEIPT NO\", " +
//                        "TO_CHAR(WA.AMNT_PAID, '9999999990.00') AS \"PAYMENT\", " +
//                        "TO_CHAR(0, '9999999990.00') AS \"REFUNDS\" FROM " +
//                        "WARD_ADVANCE WA, VISIT_DETAILS VD, PATIENT PT, ACCOUNT_MAPPING AM " +
//                        "WHERE WA.COLL_ID IS NULL AND WA.VIST_NUMB = VD.VIST_NUMB " +
//                        (nUserId==0 ? "" : "AND WA.TRAN_USR_ID = " + nUserId ) +
//                        "AND VD.REG_NUMB = PT.REG_NUMB AND AM.TRANS = " + clsAdminConstants.WARD_ADVANCE +
//                        " AND AM.MODU = " + iModuleId +	" AND WA.ACCH_ID = AM.ACCH_ID" +
//                        " UNION SELECT PT.REG_NUMB AS \"REG NO\", PT.NAME AS \"NAME\",  WA.RECE_NUMB AS \"RECEIPT NO\", " +
//                        "TO_CHAR(0, '9999999990.00') AS \"PAYMENT\", TO_CHAR(SUM(ABS(WA.AMNT_PAID)), '9999999990.00') AS \"REFUNDS\" FROM " +
//                        "WARD_ADVANCE WA, VISIT_DETAILS VD, PATIENT PT, ACCOUNT_MAPPING AM " +
//                        "WHERE WA.COLL_ID IS NULL AND WA.VIST_NUMB = VD.VIST_NUMB " +
//                        "AND VD.REG_NUMB = PT.REG_NUMB AND AM.TRANS = " + clsAdminConstants.ADVANCE_REFUND +
//                        (nUserId==0 ? "" : " AND WA.TRAN_USR_ID = " + nUserId ) +
//                        " AND AM.MODU = " + iModuleId + " AND WA.ACCH_ID = AM.ACCH_ID " +
//                        "GROUP BY PT.REG_NUMB, WA.RECE_NUMB, PT.NAME";
//                    break;
//            }
//            return sSql;
//        }
//        public static string getSql(int iConstant)
//        {
//            string sSql = "";
//            switch(iConstant)
//            {
//                case CONCESSIONHEADS:
//                    sSql =	"SELECT ' ' AS \"CONCESSION CODE\", '' AS \"CONCESSION HEAD\" FROM DUAL " +
//                            "UNION ALL SELECT CH.CONS_CODE, CH.CONS_DESC FROM CONCESSION_HEAD CH";
//                    break;
//                case DOCTORS_SHIFT:
//                    sSql =	"select ds.shift_id as \"Shift Id\", ds.shift_no as \"Shift No\" from doctor_shifts ds " +
//                            "where ds.doct_code = '<DOCTORCODE>' " +
//                            "and lower(ds.day) = trim(lower(to_char(sysdate, 'Day')))";
//                    break;
//            }
//            return sSql;
//        }

        public static string GetHospitalName()
        {
            string sHospName = "";
            if (File.Exists(Application.StartupPath + "\\HMSCONFIG.dat"))
            {
                StreamReader TempPath = new StreamReader(Application.StartupPath + "\\HMSCONFIG.dat");
                sHospName = TempPath.ReadToEnd();
            }
            return sHospName;
        }

//        public static void getUrineTestCaption()
//        {
//            string sSql = "SELECT PS.NEW_CAPTION AS CAPTION FROM PARAMETER_SETTINGS PS WHERE " +
//                          "PS.MODU = 1 AND PS.ID = 26";
			
//            DataHandling objDH = new DataHandling(sSql, "URINETESTCAPTION");

//            if (objDH.getRecordCount() > 0)
//            {
//                clsGeneral.URINE_TEST_CAPTION = objDH.getData(0,"CAPTION");
//            }
//        }

//        #region Register Menu Properties
		
//        public DataTable getRegisters()
//        {
//            string sSQL = "SELECT RM.RPT_MODULE, RM.RPT_CODE, RM.RPT_NAME FROM REPORT_MAIN RM WHERE RM.RPT_TYPE = 1 " +
//                          "AND RM.RPT_MODULE IS NOT NULL ORDER BY RM.RPT_MODULE";
//            if(createDataSet(sSQL,"Registers") == null)
//                return getDataSet().Tables["Registers"];
//            return null;
//        }

//        public DataTable getJobs()
//        {
//            string sSQL = "SELECT J.TYPE, J.CODE, J.TITLE FROM JOBS J";
//            if(createDataSet(sSQL,"Jobs") == null)
//                return getDataSet().Tables["Jobs"];
//            return null;
//        }

//        public DataTable getRegistersModules()
//        {
//            string sSQL = "SELECT DISTINCT RM.RPT_MODULE FROM REPORT_MAIN RM WHERE RM.RPT_TYPE = 1 " +
//                "AND RM.RPT_MODULE IS NOT NULL ORDER BY RM.RPT_MODULE";
//            if(createDataSet(sSQL,"RegisterModules") == null)
//                return getDataSet().Tables["RegisterModules"];
//            return null;
//        }
		
//        #endregion

        //#region UserDetails

		public static Int32 iUserLevel				= 0;
		public static Int32 iUserId				= 0;
		public static string sUsrCode		    = "";
		public static Int32 iDepartmentId		= 0;
		public static Int32 iDepartmentTypeId	= 0;
		public static string sDepartmentTypeId	= "";
		
		public static Int32 UserLevel
		{
			get {return iUserLevel;}
		}
		public static Int32 UserId
		{
			get {return iUserId;}
		}
		public Int32 DepartmentId
		{
			get {return iDepartmentId;}
		}
		public string DepartmentType
		{
			get {return sDepartmentTypeId;}
		}

        //public static void getUserDetails(string strUserName)
        //{
        //    //int iUserid = 0;
        //    string strQuery =	"SELECT USR_ID,USR_CODE, NVL(HDEPT_ID,0) AS HDEPT_ID, NVL(USERLEVEL,0) AS USERLEVEL " +
        //                        "FROM USERS WHERE UPPER(USR_CODE) = '" + strUserName + "'";
        //    DataHandling dh = new DataHandling(strQuery,"User Details");
        //    if(dh.getRecordCount()>0)
        //    {
        //        iUserId			= Convert.ToInt32(dh.getData(0,"USR_ID"));
        //        sUsrCode       =dh.getData(0,"USR_CODE");
        //        iDepartmentId	= Convert.ToInt32(dh.getData(0,"HDEPT_ID"));
        //        iUserLevel		= Convert.ToInt32(dh.getData(0,"USERLEVEL"));
        //    }
        //    if(iDepartmentId != 0)
        //    {
        //        strQuery = "SELECT HDEPT_TYPE FROM HOSPITAL_DEPARTMENTS WHERE HDEPT_ID = " + iDepartmentId;
        //        DataHandling dhDept = new DataHandling(strQuery,"Dept Details");
        //        if(dhDept.getRecordCount()>0)
        //        {
        //            iDepartmentTypeId =Convert.ToInt32(dhDept.getData(0,"HDEPT_TYPE"));
        //            sDepartmentTypeId=dhDept.getData(0,"HDEPT_TYPE");
        //        }
        //    }
        //    else
        //    {
        //        strQuery = "SELECT get_userModules('"+sUsrCode+"') as HDEPT_TYPE FROM dual";
        //        DataHandling dhDept = new DataHandling(strQuery,"Dept Details");
        //        if(dhDept.getRecordCount()>0)
        //        {
        //            sDepartmentTypeId = dhDept.getData(0,"HDEPT_TYPE");
        //        }
        //    }
			
        //}
		
        //public static DataView getUserDepartments(string strUserName)
        //{
        //    string strQuery = "SELECT UD.HDEPT_ID, HD.HDEPT_DESC, HD.HDEPT_TYPE FROM USERS_DEPARTMENT UD, " +
        //                      "HOSPITAL_DEPARTMENTS HD WHERE UD.HDEPT_ID = HD.HDEPT_ID AND " +
        //                      "UPPER(UD.USER_CODE) = '" + strUserName + "'";
        //    DataHandling dh = new DataHandling(strQuery,"User Details");
        //    return new DataView(dh.getDataSet().Tables["User Details"]);
        //}

        //public static int getDeptTypeId(int iDeptId)
        //{
        //    DataHandling objDH = new DataHandling();
        //    string strQuery = getDrugSPQueries(DEPARTMENT_TYPE);
        //    strQuery += " WHERE HDEPT_ID = " + iDeptId;
        //    objDH.createDataSet(strQuery, "DEPT TYPE");
        //    if(objDH.getRecordCount() > 0)
        //        return Convert.ToInt32(objDH.getData(0,"HDEPT_TYPE"));
        //    else 
        //        return 0;
        //}
        //#endregion
		

		/*
		 * Purpose	   : To Replace the Tags in a query with the parameter Passed 
		 * Parameter 1 : Query with Tags
		 * Parameter 2 : Two Dimensions string array with 
		 *				 Tag in first Dimension and Value in Second Dimension
		 */
		public static string replaceTag(string sSQL, string[,] strValue)
		{
			for(int i=0; i<=strValue.GetUpperBound(0); i++)
			{
				sSQL = sSQL.Replace(strValue[i,0],strValue[i,1]);
			}
			return sSQL;
		}

        //public static string getPaymentCostCenterQuery(COSTCENTER enCostCenter)
        //{
        //    string sSql = "";
        //    switch(enCostCenter)
        //    {
        //        case COSTCENTER.LKP_LAB_PAYMENT:
        //            sSql = "SELECT DOCT_CODE AS \"Doctor code\", NAME AS \"Doctor Name\" FROM DOCTOR " +
        //                   "WHERE STAT <> 2";
        //            break;
        //        case COSTCENTER.LKP_WRD_COST_CENTER:
        //            sSql = "SELECT COST_CENT_CODE AS Code, COST_CENT_DESC AS Description FROM COST_CENTRE";
        //            break;
        //    }
        //    return sSql;
        //}
        //public static void getIpBillPrintSetting()
        //{
        //    string sSql= "SELECT NVL(BC.IP_BILL_PRINT_TYPE,1) AS IP_BILL_PRINT_TYPE FROM BILLING_CONFIG BC";

        //    DataHandling objBillSet = new DataHandling(sSql, "Billing Settings");
        //    if(objBillSet.getRecordCount()>0)
        //        IP_BILL_PRINT_TYPE		= int.Parse(objBillSet.getData(0, "IP_BILL_PRINT_TYPE"));
        //    else
        //        IP_BILL_PRINT_TYPE		= 1;
        //}
		
        //public static int getIpBillFooterSetting()
        //{
        //    string sSql= "SELECT NVL(BC.IPBILL_SUMMERY_PRINT_TYPE,0) FROM BILLING_CONFIG BC";

        //    DataHandling dh = new DataHandling();//(sSql, "Billing Settings");
        //    return int.Parse(dh.ExecuteScalar(sSql));
        //}

        //public static string getVocabulary(int iDeptId)
        //{
        //    DataHandling objDH = new DataHandling();
        //    string sSql = "select nvl(stor_type,3) as stor_type from hospital_departments where hdept_id = " + iDeptId;
        //    objDH.createDataSet(sSql, "Store Type");
        //    if (objDH.getRecordCount() > 0)
        //    {
        //        if (clsGeneral.getDeptTypeId(iDeptId) == clsGeneral.DEPARTMENT_STORE)
        //        {
        //            if (Convert.ToInt32(objDH.getData(0,"stor_type")) == clsGeneral.MEDICAL_ITEMS)
        //                return "Drug";
        //            else
        //                return "Item";
        //        }
        //        else if (clsGeneral.getDeptTypeId(iDeptId) == clsGeneral.DEPARTMENT_PHARMACY)
        //        {
        //            if (Convert.ToInt32(objDH.getData(0,"stor_type")) == clsGeneral.MEDICAL_ITEMS)
        //                return "Drug";
        //            else
        //                return "Item";
        //        }
        //    }

        //    return "Drugs";
        //}

        //public static void showAdvAdjused()
        //{
        //    string sSql= "SELECT NVL(BC.SHOW_ADV_ADJ_IN_PAYMENT,0) AS SHOWADVADJ FROM BILLING_CONFIG BC";

        //    DataHandling objDH = new DataHandling(sSql, "Show Adv Adj");
        //    if(objDH.getRecordCount()>0)
        //        SHOW_ADV_ADJ_IN_PAYMENT		= int.Parse(objDH.getData(0, "SHOWADVADJ"));
        //}

		public static void recordLog(string sFileName, string sRecord)
		{
			if(sFileName=="")
			{
				MessageBox.Show("Log File name does not exist.","Payroll",MessageBoxButtons.OK,MessageBoxIcon.Information);
				return;
			}
			string sPa =  Application.StartupPath + "\\" + sFileName;
			
			if (!File.Exists(sPa))
			{
				File.Create(sPa).Close();
				StreamWriter SW = new StreamWriter(sPa);
				SW.WriteLine(sRecord);
				SW.Close();
			}
			else
			{
				StreamWriter SW = File.AppendText(sPa);
				SW.WriteLine(sRecord);
				SW.Close();
			}
		}

		private void recordLog(string sRecord)
		{	
			if(this.sLogFileName=="")
			{
				MessageBox.Show("Log File name does not exist.","Payroll",MessageBoxButtons.OK,MessageBoxIcon.Information);
				return;
			}
			string sPa = Application.StartupPath + "\\" + this.sLogFileName;
			
			if (!File.Exists(sPa))
			{
				File.Create(sPa).Close();
				StreamWriter SW = new StreamWriter(sPa);
				SW.WriteLine(sRecord);
				SW.Close();
			}
			else
			{
				StreamWriter SW = File.AppendText(sPa);
				SW.WriteLine(sRecord);
				SW.Close();
			}
		}

        //public static double getDepositAmount(string sRegNo)
        //{
        //    DataHandling dh = new DataHandling();
        //    string sSql = "select nvl(d.deposit_amnt,0) as deposit_amnt " +
        //                  "from deposit d where d.reg_numb = '" + sRegNo + "' " +
        //                  "and d.tran_id = (select max(tran_id) from deposit where reg_numb = '" + sRegNo + "')";
        //    try
        //    {
        //        dh.createDataSet(sSql,"Deposit");
        //        if (dh.getRecordCount() > 0)
        //            return Convert.ToDouble(dh.getData(0,"deposit_amnt"));
        //        else
        //            return 0;
        //    }
        //    catch
        //    {
        //        return 0;
        //    }
        //}

        //public static bool isValidRegNo(string sRegNo)
        //{
        //    DataHandling dh = new DataHandling();
        //    string sSql = "select count(*) from patient where lower(reg_numb) = lower('" + sRegNo + "')";
        //    return int.Parse(dh.ExecuteScalar(sSql)) > 0;
        //}

        //public static long getLastVisitNo(string sRegNo)
        //{
        //    DataHandling dh = new DataHandling();
        //    string sSql = "select max(v.vist_numb) as vist_numb from visit_details v " +
        //                  "where v.reg_numb = '" + sRegNo + "'";

        //    try
        //    {
        //        dh.createDataSet(sSql,"Last Visit No");
        //        if (dh.getRecordCount() > 0)
        //            return Convert.ToInt64(dh.getData(0, "vist_numb"));
        //        else
        //            return 0;
        //    }
        //    catch
        //    {
        //        return 0;
        //    }
        //}

        //public static string getSelectedItems(int iDeptId)
        //{
        //    DataHandling dh = new DataHandling();
        //    string sSql = "select itemid from dept_drugs_mapping where deptid = " + iDeptId;
        //    string sTemp = dh.ExecuteScalar(sSql);
        //    if(sTemp != "")
        //        sTemp = sTemp.Replace("ê",",");
        //    else
        //        sTemp = "0";
        //    return sTemp;
        //}

        //public static string getSelectedItem(int iDeptId)
        //{
        //    string sSql = "select itemid from mapping_dept_with_drugs where deptid = " + iDeptId;
        //    return sSql;
        //}

        //public static string processJobs(string sSPName, string [,] sSPValues)
        //{
        //    DataHandling dh = new DataHandling();
        //    OracleCommand objCmd = new OracleCommand();
        //    OracleConnection objConn = dh.createConnection();
        //    objConn.Open();
        //    objCmd.Connection=objConn;
        //    objCmd.Parameters.Clear();
        //    objCmd.CommandText = sSPName;
        //    objCmd.CommandType = CommandType.StoredProcedure;

        //    for(int i=0;i<sSPValues.GetLength(1);i++)
        //    {
        //        switch(sSPValues[2,i])
        //        {
        //            case "int":
        //                objCmd.Parameters.Add(sSPValues[0,i], OracleType.Number).Value = int.Parse(sSPValues[1,i]);
        //                break;
        //            case "double":
        //                objCmd.Parameters.Add(sSPValues[0,i], OracleType.Number).Value = double.Parse(sSPValues[1,i]);
        //                break;
        //            case "date": case "text": case "string":
        //                objCmd.Parameters.Add(sSPValues[0,i], OracleType.VarChar).Value = sSPValues[1,i];
        //                break;
        //        }
        //    }

        //    try
        //    {
        //        objCmd.ExecuteNonQuery();
        //        objCmd.Dispose();
        //        objConn.Close();
        //    }
        //    catch(Exception ex)
        //    {
        //        objCmd.Dispose();
        //        objConn.Close();
        //        return ex.Message;
        //    }
        //    return "";
        //}

        //public static DataSet getFilteredLKPDataSet(string sTableName, string sFilter)
        //{
        //    DataHandling dh  = new DataHandling();
        //    DataSet dsLookup = new DataSet();
        //    string sSql		 = "";

        //    switch(sTableName)
        //    {
        //        case "DOCTOR":
        //            //--- Doctor Status should be Active [1 -- Active,   2 -- InActive] ---
        //            sSql =	"select dt.doct_code as \"Doctor Code\", dt.name as Doctor " +
        //                    "from doctor dt, departments d, doctor_department dd " +
        //                    "where dt.doct_code = dd.doct_code and dd.dept_id = d.dept_id " +
        //                    "and lower(d.dept_code) = lower('" + sFilter + "') and dt.stat in (1, 0)";
        //            dh.createDataSet(sSql, sTableName);
        //            dsLookup = dh.getDataSet();
        //            break;
        //    }

        //    return dsLookup;
        //}

        //public static DataSet getLookupDataSet(string sTableName)
        //{
        //    DataHandling dh = new DataHandling();
        //    DataSet dsLookup = new DataSet();

        //    switch(sTableName)
        //    {
        //        case "SUBURB":
        //            dh.createDataSet("select suburb, city, pincode from suburb", sTableName);
        //            dsLookup = dh.getDataSet();
        //            break;
        //        case "CLINICAL TYPE":
        //            dh.createDataSet("select dept_code as \"Clinical Type Code\", dept_desc as \"Clinical Type\" from departments", sTableName);
        //            dsLookup = dh.getDataSet();
        //            break;
        //        case "CASE":
        //            dh.createDataSet("select case_code as \"Case Code\", case_desc as \"Case\" from case_details", sTableName);
        //            dsLookup = dh.getDataSet();
        //            break;
        //        case "DOCTOR":
        //            //--- Doctor Status should be Active [1 -- Active,   2 -- InActive] ---
        //            dh.createDataSet("select doct_code as \"Doctor Code\", name as Doctor from doctor where stat in (1, 0)", sTableName);
        //            dsLookup = dh.getDataSet();
        //            break;
        //        case "GENERICITEM":
        //            dh.createDataSet("select gen_item_id, gen_code, gen_name from generic_item", sTableName);
        //            dsLookup = dh.getDataSet();
        //            break;
        //    }
			
        //    return dsLookup;
        //}

        //public static string getAttenderName(string sRegNo)
        //{
        //    DataHandling dh = new DataHandling();
        //    string sSql = "select name from patient where reg_numb = '" + sRegNo + "'";
        //    string sTemp = dh.ExecuteScalar(sSql);
        //    return sTemp;
        //}

        //public static bool IsHoliday()
        //{
        //    string sSql =	"SELECT " +
        //                        "CASE WHEN TO_CHAR(SYSDATE,'D') IN (SELECT AC.WEEK_HOLIDAY FROM ADMIN_CONFIG AC) " +
        //                        "OR (SELECT COUNT(*) FROM HOLIDAYS H WHERE TO_DATE(TO_CHAR(SYSDATE,'DD-Mon-YYYY')) BETWEEN " +
        //                        "TO_DATE(TO_CHAR(H.DATE_FROM,'DD-Mon-YYYY')) AND TO_DATE(TO_CHAR(H.DATE_TO,'DD-Mon-YYYY')))>=1 " +
        //                        "THEN '1' ELSE '0' END AS HOLIDAY " +
        //                    "FROM " +
        //                        "DUAL";
        //    if(new DataHandling().ExecuteScalar(sSql) == "1")
        //        return true;
        //    else
        //        return false;
        //}

        //public static bool IsHoliday(string sDate)
        //{
        //    string sSql =	"SELECT " +
        //        "CASE WHEN TO_CHAR(TO_DATE('" + sDate + "','dd/MM/yyyy'),'D') IN (SELECT AC.WEEK_HOLIDAY FROM ADMIN_CONFIG AC) " +
        //        "OR (SELECT COUNT(*) FROM HOLIDAYS H WHERE TO_DATE(TO_CHAR(TO_DATE('" + sDate + "','dd/MM/yyyy'),'DD-Mon-YYYY')) BETWEEN " +
        //        "TO_DATE(TO_CHAR(H.DATE_FROM,'DD-Mon-YYYY')) AND TO_DATE(TO_CHAR(H.DATE_TO,'DD-Mon-YYYY')))>=1 " +
        //        "THEN '1' ELSE '0' END AS HOLIDAY " +
        //        "FROM " +
        //        "DUAL";
        //    return new DataHandling().ExecuteScalar(sSql) == "1";
        //}

        //public static bool IsEmergencyPeriod()
        //{
        //    string sSql =	"select count(*) from admin_config " +
        //                    "where " +
        //                        "(to_date(to_char(sysdate,'HH:MI AM'),'HH:MI AM') > " +
        //                        "to_date(to_char(t.emer_from_time,'HH:MI AM'),'HH:MI AM') and " +
        //                        "to_date(to_char(sysdate,'HH:MI AM'),'HH:MI AM') <= " +
        //                        "to_date('11:59 PM','HH:MI AM')) " +
        //                    "or " +
        //                        "(to_date(to_char(sysdate,'HH:MI AM'),'HH:MI AM') >= " +
        //                        "to_date('12:00 AM','HH:MI AM') and " +
        //                        "to_date(to_char(sysdate,'HH:MI AM'),'HH:MI AM') < " +
        //                        "to_date(to_char(t.emer_to_time,'HH:MI AM'),'HH:MI AM'))";
        //    if(new DataHandling().ExecuteScalar(sSql) == "1")
        //        return true;
        //    else
        //        return false;
        //}

        //public static double getAccHead_Amount(string strAccCode)
        //{
        //    string sSql =	"select nvl(amun, 0) as Amount from accountheads " +
        //                    "where lower(acc_code) = lower('" + strAccCode + "')";
        //    try
        //    {
        //        return double.Parse(new DataHandling().ExecuteScalar(sSql));
        //    }
        //    catch
        //    {
        //        return 0;
        //    }
        //}

        //public static string getDoctorName(string sDoctCode)
        //{
        //    string sSql = "select name as \"Doctor\" from doctor where doct_code = '" + sDoctCode + "'";

        //    try
        //    {
        //        return new DataHandling().ExecuteScalar(sSql);
        //    }
        //    catch
        //    {
        //        return "";
        //    }
        //}

        //public static bool collectSpecialFee(string sDoctCode)
        //{
        //    string sSql =	"select count(*) from doctor_shifts ds where ds.doct_code = '" + sDoctCode + "' " +
        //                    "and to_char(ds.day) = to_char(sysdate, 'Day')";
        //    DataHandling dh = new DataHandling();

        //    if (int.Parse(dh.ExecuteScalar(sSql)) == 0)
        //        return false;
        //    else
        //        return true;
        //}

        //public static int getShiftNo(string strDoctorCode)
        //{
        //    DataHandling dh = new DataHandling();
        //    DataTable dt	= new DataTable();
        //    string sSql		= "select to_char(sysdate, 'HH24') * 60 + to_char(sysdate, 'MI') as SysTime from dual";
        //    int iSysTime	= 0;
        //    int iShiftId	= 0;

        //    try
        //    {
        //        iSysTime = int.Parse(dh.ExecuteScalar(sSql));
        //    }
        //    catch
        //    {
        //        iSysTime = 0;
        //    }

        //     sSql =	"select " +
        //                "ds.shift_id, ds.shift_no, " +
        //                "to_char(ds.shift_from_time, 'HH24') * 60 + to_char(ds.shift_from_time, 'MI') as from_time, " +
        //                "to_char(ds.buffer_to_time, 'HH24') * 60 + to_char(ds.buffer_to_time, 'MI') as to_time " +
        //            "from " +
        //                "doctor_shifts ds " +
        //            "where " +
        //                "lower(doct_code) = lower('" + strDoctorCode + "') " +
        //                "and lower(ds.day) = trim(lower(to_char(sysdate, 'Day'))) " +
        //            "order by " +
        //                "ds.shift_from_time";

        //    dh.createDataSet(sSql, "Doctors Shift Time");
        //    dt = dh.getDataSet().Tables["Doctors Shift Time"];

        //    try
        //    {
        //        int iLowerLimit = 0;

        //        for (int i=0; i<dt.Rows.Count; i++)
        //        {
        //            int iUpperLimit = int.Parse(dt.Rows[i]["to_time"].ToString());

        //            if (iSysTime >= iLowerLimit & iSysTime <= iUpperLimit)
        //                iShiftId = int.Parse(dt.Rows[i]["shift_id"].ToString());

        //            iLowerLimit = int.Parse(dt.Rows[i]["to_time"].ToString());
        //        }

        //        if (dt.Rows.Count > 0 & iShiftId == 0)
        //            iShiftId = int.Parse(dt.Rows[0]["shift_id"].ToString());
        //    }
        //    catch
        //    {
        //        iShiftId = 0;
        //    }

        //    return iShiftId;
        //}

		public static string AmountToText(double dAmount)
		{
			if(dAmount <= 0)
				return "";
			string sReturn = "";
			string sAmount = dAmount.ToString("###############0.00");
			
			string sRuppees = sAmount.Substring(0, sAmount.IndexOf("."));
			string sPaise	= sAmount.Substring(sAmount.IndexOf(".") + 1);
			long dRuppees	= long.Parse(sRuppees);
			long dPaise		= long.Parse(sPaise);
			sRuppees		= getRuppees(dRuppees);
			sPaise			= getPaise(dPaise);
			if(dRuppees > 0)
				sReturn			= sRuppees + " Ruppees";
			if(dPaise > 0)
				sReturn += (dRuppees > 0 ? " And " : "") + sPaise + " Paise";
			sReturn += " Only";
			
			return sReturn;
		}

		private static string getRuppees(long iRuppees)
		{
			string sReturn = "";
			string sRuppees = iRuppees.ToString();
			if(sRuppees.Length > 15)
				return "";
			while(sRuppees.Length > 0 & long.Parse(sRuppees) > 0)
			{
				int iLen = sRuppees.Length;
				switch(sRuppees.Length)
				{
					case 1: case 2:
						sReturn = sReturn == "" ? getPaise(long.Parse(sRuppees)) : sReturn + " And " + getPaise(long.Parse(sRuppees)); 
						sRuppees = "0";
						break;
					case 3: //Hundreds
                        sReturn = sReturn == "" ? 
							getPaise(long.Parse(sRuppees.Substring(0,1))) + (long.Parse(sRuppees.Substring(0,1))>0? " Hundred" : "") :
							sReturn + (long.Parse(sRuppees.Substring(0,1))>0 ? " " + getPaise(long.Parse(sRuppees.Substring(0,1))) + " Hundred" : ""); 
						sRuppees = sRuppees.Substring(1);
						break;
					case 4: case 5: case 6: case 7: case 8: case 9:
					case 10: case 11: case 12: case 13: case 14: case 15:
						sReturn = sReturn == "" ?
							getPaise(long.Parse(sRuppees.Substring(0,(iLen%2) + 1))) + (long.Parse(sRuppees.Substring(0,(iLen%2) + 1))>0 ? getDigits(iLen): "") :
							sReturn + (long.Parse(sRuppees.Substring(0,(iLen%2) + 1))>0 ? " " + getPaise(long.Parse(sRuppees.Substring(0,(iLen%2) + 1))) + getDigits(iLen):""); 
						sRuppees = sRuppees.Substring((iLen%2) + 1);
						break;
				}
			}
			return sReturn;
		}

		private static string getDigits(int iLen)
		{
			string sReturn = "";
			switch(iLen)
			{
				case 4: case 5://Thousands
					sReturn = " Thousand";
					break;
				case 6: case 7://Lakhs
					sReturn = " Lakh";
					break;
				case 8: case 9://Crores
					sReturn = " Crore";
					break;
				case 10: case 11://Millions
					sReturn = " Million";
					break;
				case 12: case 13://Billions
					sReturn = " Billion";
					break;
				case 14: case 15://Trillions
					sReturn = " Trillion";
					break;
			}
			return sReturn;
		}

		private static string getPaise(long iPaise)
		{
			string sReturn = "";
			if(iPaise < 20)
				sReturn = Ones[iPaise];
			else
				sReturn = Tens[(iPaise/10)-2] + " " + Ones[iPaise%10];

			return sReturn;
		}

        //public static bool isEmployeeNoExists(int iCorporateId, string sEmpNo)
        //{
        //    string sSql = "select count(*) from patient where lower(emp_no) = lower('" + sEmpNo + "') " +
        //                  "and corporate_id = " + iCorporateId;
        //    DataHandling dh = new DataHandling();

        //    if (int.Parse(dh.ExecuteScalar(sSql)) == 0)
        //        return false;
        //    else
        //        return true;
        //}

        //public static bool isMemoNoExists(int iCorporateId, string sMemoNo)
        //{
        //    string sSql = "select count(*) from memo_details where lower(memo_no) = lower('" + sMemoNo + "') " +
        //                  "and corporate_id = " + iCorporateId;
        //    DataHandling dh = new DataHandling();

        //    if (int.Parse(dh.ExecuteScalar(sSql)) == 0)
        //        return false;
        //    else
        //        return true;
        //}

        //public static bool isMemoNoExists(string sRegNo, string sMemoNo)
        //{
        //    string sSql = "select count(*) from memo_details md, visit_details vd " +
        //                  "where vd.memo_id = md.memo_id and vd.reg_numb = md.reg_no " +
        //                  "and vd.memo_no in ('" + sMemoNo + "') and vd.reg_numb = '" + sRegNo + "'";
        //    DataHandling dh = new DataHandling();

        //    if (int.Parse(dh.ExecuteScalar(sSql)) == 1)
        //        return false;
        //    else
        //        return true;
        //}

        //public static bool isMemoValid(string sFromDate, string sToDate)
        //{
        //    string sSql =	"select count(*) from dual " +
        //                    "where to_date(to_char(sysdate, 'DD/MM/YYYY'), 'DD/MM/YYYY') " +
        //                    "between to_date('" + sFromDate + "', 'DD/MM/YYYY') " +
        //                    "and to_date('" + sToDate + "', 'DD/MM/YYYY')";
        //    DataHandling dh = new DataHandling();

        //    if (int.Parse(dh.ExecuteScalar(sSql)) == 1)
        //        return true;
        //    else
        //        return false;
        //}

        //public static bool AllowPositionDrugs(string sUserCode, bool bStore)
        //{
        //    string sSql = "";

        //    if (bStore)
        //        sSql =	"select nvl(pos_store_drugs, 0) as position_drugs from users " +
        //                "where lower(usr_code) = lower('" + sUserCode + "')";
        //    else
        //        sSql =	"select nvl(pos_pha_drugs, 0) as position_drugs from users " +
        //                "where lower(usr_code) = lower('" + sUserCode + "')";

        //    DataHandling dh = new DataHandling();

        //    if (int.Parse(dh.ExecuteScalar(sSql)) == 1)
        //        return true;
        //    else
        //        return false;
        //}

        //public static bool AllowRepositionDrugs(string sUserCode, bool bStore)
        //{
        //    string sSql = "";

        //    if (bStore)
        //        sSql =	"select nvl(repos_store_drugs, 0) as reposition_drugs from users " +
        //                "where lower(usr_code) = lower('" + sUserCode + "')";
        //    else
        //        sSql =	"select nvl(repos_pha_drugs, 0) as reposition_drugs from users " +
        //                "where lower(usr_code) = lower('" + sUserCode + "')";

        //    DataHandling dh = new DataHandling();

        //    if (int.Parse(dh.ExecuteScalar(sSql)) == 1)
        //        return true;
        //    else
        //        return false;
        //}
		
        //public static bool ShowReportMenu(string sUserCode)
        //{
        //    string sSql = "";
        //    sSql =	"SELECT NVL(SHOW_REPORT_MENU, 1) AS SHOW_REPORT_MENU FROM USERS " +
        //            "WHERE LOWER(USR_CODE) = LOWER('" + sUserCode + "')";
        //    DataHandling dh = new DataHandling();
        //    try
        //    {
        //        if (int.Parse(dh.ExecuteScalar(sSql)) == 1)
        //            return true;
        //        else
        //            return false;
        //    }
        //    catch
        //    {
        //        return true;
        //    }
        //}

        //public static bool ShowRegisterMenu(string sUserCode)
        //{
        //    string sSql = "";
        //    sSql =	"SELECT NVL(SHOW_REGISTER_MENU, 1) AS SHOW_REGISTER_MENU FROM USERS " +
        //        "WHERE LOWER(USR_CODE) = LOWER('" + sUserCode + "')";
        //    DataHandling dh = new DataHandling();
        //    try
        //    {
        //        if (int.Parse(dh.ExecuteScalar(sSql)) == 1)
        //            return true;
        //        else
        //            return false;
        //    }
        //    catch
        //    {
        //        return true;
        //    }
        //}

        //public static double getPharmDiscPercentage(string sRegNo)
        //{
        //    try
        //    {
        //        string sSql =	"select pat_type_id, nvl(corporate_id,0) as corporate_id " +
        //                        "from patient where reg_numb = '" + sRegNo + "'";
			
        //        DataHandling dh = new DataHandling();
        //        dh.createDataSet(sSql, "Pharmacy Discount");

        //        if (dh.getRecordCount() > 0)
        //        {
        //            if (int.Parse(dh.getData(0, "corporate_id")) == 0)
        //            {
        //                if (AllowPharmacyDiscount(sRegNo))
        //                {
        //                    sSql =	"select nvl(pharm_discount_perc, 0) as pharm_discount_perc " +
        //                            "from patient_type where pat_type_id = " + int.Parse(dh.getData(0, "pat_type_id"));
        //                    string sTemp = dh.ExecuteScalar(sSql);
        //                    sTemp = (sTemp.Trim() == "") ? "0" : sTemp.Trim();
        //                    return double.Parse(sTemp);
        //                }
        //            }
        //        }
        //    }
        //    catch
        //    {
        //        return 0;
        //    }

        //    return 0;
        //}

        //private static bool AllowPharmacyDiscount(string sRegNo)
        //{
        //    DataHandling dh = new DataHandling();
        //    try
        //    {
        //        long lVisitNo = getLastVisitNo(sRegNo);

        //        if (lVisitNo > 0)
        //        {
        //            string sSql =	"select nvl(allow_pharm_disc, 1) as allow_pharm_disc from fee_classification " +
        //                            "where class_id = (select get_patient_class(vist_numb) as classid " +
        //                            "from visit_details where vist_numb = " + lVisitNo + ")";
        //            return int.Parse(dh.ExecuteScalar(sSql)) == 1;
        //        }
        //    }
        //    catch
        //    {
        //        return true;
        //    }

        //    return true;
        //}

        //public static bool isCorporatePatient(string sRegNo)
        //{
        //    return new DataHandling().ExecuteScalar("select nvl(corporate_id,0) as corporate_id from patient where reg_numb = '" + sRegNo + "'") == "1";
        //}

        //public static bool makeConsumeEntry()
        //{
        //    string sSql = "SELECT NVL(LC.AUTOMATIC_CONSUME,0) AS \"INSTANT CONSUME\" FROM LAB_CONFIG LC";
        //    DataHandling dh = new DataHandling();

        //    if (int.Parse(dh.ExecuteScalar(sSql)) == 1)
        //        return true;
        //    else
        //        return false;
        //}
        //public static bool IsClassExists()
        //{
        //    string sSql = "SELECT COUNT(*) AS NOOFCLASS FROM FEE_CLASSIFICATION";
        //    DataHandling dh = new DataHandling();

        //    return int.Parse(dh.ExecuteScalar(sSql)) > 1;
        //}
        //public static string getClassName(int iClassId)
        //{
        //    string sSql = "select class_name from fee_classification where class_id = " + iClassId;
        //    DataHandling dh = new DataHandling();
			
        //    try
        //    {
        //        return dh.ExecuteScalar(sSql);
        //    }
        //    catch
        //    {
        //        return "";
        //    }
        //}
        //public static bool IsSpecialVisitTime(string sTime)
        //{
        //    string sSql = "";

        //    sSql =	"select count(*) from billing_config " +
        //        "where " +
        //        "(to_date(to_char(to_date('" + sTime + "','HH:MI AM'),'HH:MI AM'),'HH:MI AM') >= " +
        //        "to_date(to_char(spec_visit_from_time,'HH:MI AM'),'HH:MI AM') and " +
        //        "to_date(to_char(to_date('" + sTime + "','HH:MI AM'),'HH:MI AM'),'HH:MI AM') <= " +
        //        "to_date('11:59 PM','HH:MI AM')) " +
        //        "or " +
        //        "(to_date(to_char(to_date('" + sTime + "','HH:MI AM'),'HH:MI AM'),'HH:MI AM') >= " +
        //        "to_date('12:00 AM','HH:MI AM') and " +
        //        "to_date(to_char(to_date('" + sTime + "','HH:MI AM'),'HH:MI AM'),'HH:MI AM') <= " +
        //        "to_date(to_char(spec_visit_to_time,'HH:MI AM'),'HH:MI AM'))";

        //    if(new DataHandling().ExecuteScalar(sSql) == "1")
        //        return true;
        //    else
        //        return false;
        //}

        //public static bool IsEmergencyPeriod(string sDateTime)
        //{
        //    string sDate = "";
        //    string sTime = "";
        //    if(sDateTime == "")
        //    {
        //        sDate = clsGeneral.getServerDate();
        //        sTime = clsGeneral.getServerTime();
        //    }
        //    else
        //    {
        //        sDate = clsValidation.getFormattedDate(sDateTime.Substring(0, sDateTime.IndexOf(" ") + 1));
        //        sTime = sDateTime.Substring(sDateTime.IndexOf(" "));
        //    }
        //    if(IsHoliday(sDate))
        //        return true;
        //    if(IsSpecialVisitTime(sTime))
        //        return true;
        //    return false;
        //}

        //public static string getVisitType(int iTypeId)
        //{
        //    string sSql = "SELECT AH.ACC_DESC FROM ACCOUNT_MAPPING AM, ACCOUNTHEADS AH WHERE " +
        //                  "AM.ACCH_ID = AH.ACCH_ID AND AM.MODU = 4";
        //    if(iTypeId == 1)
        //    {
        //        sSql += " AND AM.TRANS = " + clsAdminConstants.FIRST_VISIT;
        //    }
        //    else if (iTypeId == 2)
        //    {
        //        sSql += " AND AM.TRANS = " + clsAdminConstants.ROUTINE_VISIT;
        //    }
        //    else if (iTypeId == 3)
        //    {
        //        sSql += " AND AM.TRANS = " + clsAdminConstants.SPECIAL_VISIT_DAY;
        //    }
        //    else if (iTypeId == 4)
        //    {
        //        sSql += " AND AM.TRANS = " + clsAdminConstants.SPECIAL_VISIT_NIGHT;
        //    }
        //    return new DataHandling().ExecuteScalar(sSql);
        //}

        //public static int showCorporateDetails()
        //{
        //    string sql = "select nvl(show_corporate_details, 0) AS show_corporate_details from registration_settings";
        //    try
        //    {
        //        DataHandling dh = new DataHandling(sql, "SHOW CORPORATE DETAILS");
        //        if (dh.getRecordCount() < 0) return 0;
        //        else return Convert.ToInt32(dh.getData(0, "show_corporate_details"));
        //    }
        //    catch
        //    {
        //        return 0;
        //    }
        //}
        //public static int seperateRegNo()
        //{
        //    string sql = "select nvl(seperate_regno, 0) as seperate_regno from registration_settings";
        //    try
        //    {
        //        DataHandling dh = new DataHandling(sql, "SEPERATE REG NO");
        //        if (dh.getRecordCount() < 0) return 0;
        //        else return Convert.ToInt32(dh.getData(0, "seperate_regno"));
        //    }
        //    catch
        //    {
        //        return 0;
        //    }
        //}

        //public static int showPackSize()
        //{
        //    string sql = "select nvl(show_packsize, 0) as show_packsize from pharmacy_config";
        //    try
        //    {
        //        DataHandling dh = new DataHandling(sql, "SHOW PACK SIZE");
        //        if (dh.getRecordCount() < 0) return 0;
        //        else return Convert.ToInt32(dh.getData(0, "show_packsize"));
        //    }
        //    catch
        //    {
        //        return 0;
        //    }
        //}
        //public static string getDefaultMiscellQty()
        //{
        //    string sSql = "SELECT NVL(BC.MISCEL_CHRG_DEF_QTY,0) FROM BILLING_CONFIG BC";
        //    return new DataHandling().ExecuteScalar(sSql);
        //}
        //public static string getDefaultServiceBillQty()
        //{
        //    string sSql = "SELECT NVL(BC.SB_TO_WP_DEFAULT_QTY,0) FROM BILLING_CONFIG BC";
        //    return new DataHandling().ExecuteScalar(sSql);
        //}
        //public static bool showAllDeptTest()
        //{
        //    string sSql = "SELECT NVL(SHOW_ALL_DEPT_TEST,0) FROM LAB_CONFIG";
        //    return new DataHandling().ExecuteScalar(sSql) == "1";
        //}
        //public static bool hideQtyAndServId()
        //{
        //    string sSql = "SELECT NVL(HIDE_QTY_SERV_ID,0) FROM LAB_CONFIG";
        //    return new DataHandling().ExecuteScalar(sSql) == "1";
        //}

        //public static int getTitlesGender(int iTitleId)
        //{
        //    string sSql = "select nvl(gender,0) as gender from title where title_id = " + iTitleId;
        //    DataHandling dh = new DataHandling();
        //    return int.Parse(dh.ExecuteScalar(sSql));
        //}

        //public static string getDoctorShifts(string sRegNo)
        //{
        //    DataHandling dh = new DataHandling();
        //    string sTemp = "";

        //    try
        //    {
        //        string sSql =	"select doct_code from visit_details " +
        //            "where vist_numb = (select max(vist_numb) as vist_numb from visit_details " +
        //            "where reg_numb = '" + sRegNo + "')";
        //        string sDoctCode = dh.ExecuteScalar(sSql);

        //        sSql =	"select " +
        //                    "upper(substr(t.day, 0, 3)) || ' - ' || " +
        //                    "to_char(t.shift_from_time, 'HH:MI AM') || ' TO ' || " +
        //                    "to_char(t.shift_to_time, 'HH:MI AM') as DoctorShifts " +
        //                "from " +
        //                    "doctor_shifts t " +
        //                "where " +
        //                    "lower(t.doct_code) = lower('" + sDoctCode + "')";

        //        dh.createDataSet(sSql, "Doctor Shifts");

        //        foreach(DataRow dr in dh.getDataSet().Tables[0].Rows)
        //            sTemp = sTemp + " " + dr["DoctorShifts"].ToString();
        //    }
        //    catch
        //    {
        //        sTemp = "";
        //    }

        //    return sTemp.Trim();
        //}
        //public static bool addPayrollMenu()
        //{
        //    string sSql = "SELECT NVL(U.PREPARE_PAYROLL,0) FROM USERS U WHERE upper(U.USR_CODE) = upper('" + USER_NAME +"')";
        //    DataHandling dh = new DataHandling();
        //    return dh.ExecuteScalar(sSql) == "1";
        //}
        //public static bool showMultipleLookup_LabTests()
        //{
        //    try
        //    {
        //        string sSql = "select nvl(show_mlookup_tests,0) as show_mlookup_tests from lab_config";
        //        DataHandling dh = new DataHandling();
        //        return int.Parse(dh.ExecuteScalar(sSql)) == 1;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}

        //public static bool IsPatientExpired(string sRegNo)
        //{
        //    try
        //    {
        //        long lVisitNo = getLastVisitNo(sRegNo);
        //        string sSql	=	"select nvl(rlse_type, 0) as rlse_type from visit_details " +
        //                        "where vist_numb = " + lVisitNo;
        //        DataHandling dh = new DataHandling();
        //        return int.Parse(dh.ExecuteScalar(sSql)) == 4; //-- 4 is the EXPIRED Status --
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        //public static bool ShowWardMenu()
        //{
        //    try
        //    {
        //        string sSql	= "SELECT NVL(HIDE_WARD_MENU,0) FROM ADMIN_CONFIG";
        //        DataHandling dh = new DataHandling();
        //        return int.Parse(dh.ExecuteScalar(sSql)) == 0;
        //    }
        //    catch
        //    {
        //        return true;
        //    }
        //}
        //public static bool ShowPayrollMenu()
        //{
        //    try
        //    {
        //        string sSql	= "SELECT NVL(HIDE_PAYROLL_MENU,0) FROM ADMIN_CONFIG";
        //        DataHandling dh = new DataHandling();
        //        return int.Parse(dh.ExecuteScalar(sSql)) == 0;
        //    }
        //    catch
        //    {
        //        return true;
        //    }
        //}
        //public static bool IsGeneralStore(int iHDeptId)
        //{
        //    try
        //    {
        //        string sSql	= "select nvl(is_gstore,0) from hospital_departments where hdept_id = " + iHDeptId;
        //        DataHandling dh = new DataHandling();
        //        return int.Parse(dh.ExecuteScalar(sSql)) == 1;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
		public static double Calculate(string sExpression)
		{
            Excel.ApplicationClass objExcel = new Excel.ApplicationClass();
			try
			{
				object objCheck=objExcel.Evaluate(sExpression);
				if(objCheck.ToString()== "-2146826273" | objCheck.ToString() == "-2146826281" ) return 0;
				return double.Parse((objCheck.ToString()));
			}
			catch{}
			return 0;
		}

		public static bool IsValidFormula(string sFormula)
		{
			Excel.ApplicationClass objExcel = new Excel.ApplicationClass();
			try
			{
				object objCheck=objExcel.Evaluate(sFormula);
				if(objCheck.ToString()!="-2146826273" & objCheck.ToString() != "-2146826259" & objCheck.ToString() != "-2146826281")
					return true;
				else
					return false;
			}
			catch{}
			return false;
		}

        public static bool PrintFooterAtBottom()
        {
            //try
            //{
                //string sSql = "SELECT NVL(LC.PRINT_FOOTER_AT_BOTTOM,0) FROM LAB_CONFIG LC";
                //DataHandling dh = new DataHandling();
                //return int.Parse(dh.ExecuteScalar(sSql)) == 1;
            //}
            //catch
            //{
                return false;
            //}
        }
        //public static bool PrintReportCategory()
        //{
        //    try
        //    {
        //        string sSql	= "SELECT NVL(LC.PRINT_REPT_CATEGORY,1) FROM LAB_CONFIG LC";
        //        DataHandling dh = new DataHandling();
        //        return int.Parse(dh.ExecuteScalar(sSql)) == 1;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        //public static bool SetUserFontSize(int iUserId, string sFormName)
        //{
        //    try
        //    {
        //        string sSql = "select count(*) from user_font where user_id = " + iUserId + " and lower(form_name) = lower('" + sFormName + "')";
        //        DataHandling dh = new DataHandling();
        //        return int.Parse(dh.ExecuteScalar(sSql)) == 1;
        //    }
        //    catch
        //    {
        //        return false;
        //    }
        //}
        //public static int GetLabelFontSize(int iUserId, string sFormName)
        //{
        //    try
        //    {
        //        string sSql =	"select nvl(label_font_size,0) as label_font_size from user_font " +
        //                        "where user_id = " + iUserId + " and lower(form_name) = lower('" + sFormName + "')";
        //        DataHandling dh = new DataHandling();
        //        return int.Parse(dh.ExecuteScalar(sSql));
        //    }
        //    catch
        //    {
        //        return 0;
        //    }
        //}
        //public static int GetTextBoxFontSize(int iUserId, string sFormName)
        //{
        //    try
        //    {
        //        string sSql =	"select nvl(textbox_font_size,0) as textbox_font_size from user_font " +
        //                        "where user_id = " + iUserId + " and lower(form_name) = lower('" + sFormName + "')";
        //        DataHandling dh = new DataHandling();
        //        return int.Parse(dh.ExecuteScalar(sSql));
        //    }
        //    catch
        //    {
        //        return 0;
        //    }
        //}
        //public static int GetComboBoxFontSize(int iUserId, string sFormName)
        //{
        //    try
        //    {
        //        string sSql =	"select nvl(combobox_font_size,0) as combobox_font_size from user_font " +
        //                        "where user_id = " + iUserId + " and lower(form_name) = lower('" + sFormName + "')";
        //        DataHandling dh = new DataHandling();
        //        return int.Parse(dh.ExecuteScalar(sSql));
        //    }
        //    catch
        //    {
        //        return 0;
        //    }
        //}
        //public static void SetUserFontSize(Control.ControlCollection ccl, string sFormName)
        //{
        //    foreach(Control ctl in ccl)
        //    {
        //        switch (ctl.GetType().Name)
        //        {
        //            case "Label":
        //                try
        //                {
        //                    if (((Label)ctl).Tag.ToString().Trim() != "")
        //                        ((Label)ctl).Font = new System.Drawing.Font(((Label)ctl).Font.Name, GetLabelFontSize(USER_ID, sFormName), ((Label)ctl).Font.Style);
        //                }
        //                catch{}
        //                break;
        //            case "TextBox":
        //                ((TextBox)ctl).Font = new System.Drawing.Font(((TextBox)ctl).Font.Name, GetTextBoxFontSize(USER_ID, sFormName), ((TextBox)ctl).Font.Style);
        //                break;
        //            case "ComboBox":
        //                ((ComboBox)ctl).Font = new System.Drawing.Font(((ComboBox)ctl).Font.Name, GetComboBoxFontSize(USER_ID, sFormName), ((ComboBox)ctl).Font.Style);
        //                break;
        //            case "Panel":
        //            case "GroupBox":
        //                SetUserFontSize(ctl.Controls, sFormName);
        //                break;
        //        }
        //    }
        //}

        //public static void setSampleNoCaption()
        //{
        //    string sSql =	"select nvl(sample_no_caption,'Specimen No') from lab_config";
        //    DataHandling dh = new DataHandling();
        //    SAMPLE_NO_CAPTION = dh.ExecuteScalar(sSql);
        //}

        //public static bool generateSampleNo()
        //{
        //    string sSql =	"SELECT NVL(GENERATE_SAMPLE_NO,0) FROM LAB_CONFIG";
        //    DataHandling dh = new DataHandling();
        //    return int.Parse(dh.ExecuteScalar(sSql)) == 1;
        //}

        //public static bool showTestStatus()
        //{
        //    string sSql =	"SELECT NVL(SHOW_TEST_STATUS,0) FROM LAB_CONFIG";
        //    DataHandling dh = new DataHandling();
        //    return int.Parse(dh.ExecuteScalar(sSql)) == 1;
        //}
        //public static int getSnoLength()
        //{
        //    try
        //    {
        //        string sSql = "select nvl(sno_len_for_printing,0) as sno_len_for_printing from admin_config";
        //        return int.Parse(new DataHandling().ExecuteScalar(sSql));
        //    }
        //    catch
        //    {
        //        return 0;
        //    }
        //}
        //public static bool showAllInGroupPrinting()
        //{
        //    string sSql = "SELECT NVL(GROUP_PRINT_SHOW_ALL,0) FROM LAB_CONFIG";
        //    return int.Parse(new DataHandling().ExecuteScalar(sSql)) == 1;
        //}
	}
}