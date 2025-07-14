using System;
using System.Data;
using System.Windows.Forms;
using Bosco.Utility.Common;


namespace PAYROLL.UserControl
{
	public class frmHMSTemplate : System.Windows.Forms.Form
	{
		private System.ComponentModel.Container components = null;
		public delegate void LookupHandler(int nLookupIndex);
		public event LookupHandler ShowLookup;
		public event KeyEventHandler SaveRecord;
		
		private bool CallDirectly			= false;
		private int iLookupIndexId;
		private string currntcontrolName	= "";
		public string Lookup_Selected_Value	= "";
		public DataRow Lookup_Selected_Row;
		public string strFilter				= "";
		private int nPreviousLkpId			= -1;
		public int iPharmacyLookupId		= 0;
		public long lVisitNo				= 0;
		
		private int MouseX=0;
		private int MouseY=0;
		private Control PreControl;
		private int iDepartmentId			= 0;

		private bool isRegNumb				= false;
		public bool isStockReposition		= false;
		//UI.Search.frmAdvanceSearch objSearch;
	//	frmAdvanceSearch objSearch;

		public string GridColumnFilterTxt	= "";

		public frmHMSTemplate()
		{
			InitializeComponent();
		}

		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}
		
		public int LookupIndexId
		{
			set 
			{
				iLookupIndexId=value;
				if (ActiveControl != null)
					currntcontrolName= ActiveControl.Name.ToString();
			}
			get {return iLookupIndexId;}
		}

		public int Department
		{
			set{iDepartmentId = value;}
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			// 
			// frmHMSTemplate
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.BackColor = System.Drawing.Color.FromArgb(((System.Byte)(188)), ((System.Byte)(201)), ((System.Byte)(203)));
			this.ClientSize = new System.Drawing.Size(520, 318);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
			this.KeyPreview = true;
			this.MaximizeBox = false;
			this.Name = "frmHMSTemplate";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "frmHMSTemplate";
			this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmHMSTemplate_KeyDown);
			this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.frmHMSTemplate_KeyPress);
			this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.frmHMSTemplate_MouseMove);

		}
		#endregion

        private void frmHMSTemplate_KeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
        {
            string sSql = "";
            //frmLookup frmlkp = null;			

            if (ActiveControl != null)
            {
                if (ActiveControl.GetType().ToString() == "System.Windows.Forms.ComboBox" && (e.KeyCode == Keys.Up || e.KeyCode == Keys.Down)) return;
                if (ActiveControl.GetType().Name == "TextBox")
                {
                    TextBox txt = (TextBox)ActiveControl;
                    if (txt.Multiline & txt.AcceptsReturn) return;
                }
            }

            switch (e.KeyCode)
            {
                case System.Windows.Forms.Keys.Return:
                    System.Windows.Forms.SendKeys.Send("{TAB}");
                    break;
                case System.Windows.Forms.Keys.Down:
                    System.Windows.Forms.SendKeys.Send("{TAB}");
                    break;
                case System.Windows.Forms.Keys.Up:
                    System.Windows.Forms.SendKeys.Send("+{TAB}");
                    break;
                case System.Windows.Forms.Keys.F1:
                    MessageBox.Show("Help", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
                //case System.Windows.Forms.Keys.F4:
                //    if (e.Alt) break;
                    //    if (iLookupIndexId == clsGeneral.LKP_REG_NUMBER)
                    //    {
                    //        bool bActivePatients = ((Utility.UserControl.ucRegNo)ActiveControl).ShowActivePatients;
                    //        string sText = ((Utility.UserControl.ucRegNo)ActiveControl).Text;
                    //        frmRegNoLookup objRegNo = new frmRegNoLookup(bActivePatients, sText);
                    //        objRegNo.ShowDialog();
                    //        if (objRegNo.getRegistrationNo() != "") Lookup_Selected_Value = objRegNo.getRegistrationNo();
                    //        if (ShowLookup!= null) ShowLookup(iLookupIndexId);
                    //        nPreviousLkpId = iLookupIndexId;
                    //    }
                    //    break;
                    //case clsGeneral.LKP_LOOKUP_KEY:
                    //    if (ActiveControl != null)
                    //        if (currntcontrolName!=ActiveControl.Name.ToString() && (!CallDirectly)) return;

                    //switch (iLookupIndexId)
                    //{
                        //case clsGeneral.LKP_WARD:
                        //    frmlkp=new frmLookup(clsAdminConstants.SUB_WARDS,1);					
                        //    break;
                        //case clsGeneral.LKP_ROOM: 
                        //    frmlkp=new frmLookup(clsAdminConstants.SUB_ROOMS,1);
                        //    break;
                        //case clsGeneral.LKP_OPERATION:
                        //    frmlkp=new frmLookup(clsAdminConstants.OPERATION,1);
                        //    break;
                        //case clsGeneral.LKP_VENDOR: 
                        //    frmlkp=new frmLookup("SELECT VEND_CODE AS Code, VEND_NAME AS Name FROM VENDOR WHERE STAT <> 2","CODE","Vendors","",GridColumnFilterTxt);
                        //    break;
                        //case clsGeneral.LKP_ITEMTYPE:
                        //    sSql =	"SELECT TYPE_ID, TYPE_DESC AS \"ITEM TYPE\" FROM ITEM_TYPE ORDER BY TYPE_DESC";
                        //    frmlkp = new frmLookup(sSql,"Item Type","Item Type","",GridColumnFilterTxt);
                        //    frmlkp.HideColumns="Type_Id";
                        //    break;
                        //case clsGeneral.LKP_ITEMNAME:
                        //    sSql =	"SELECT IT.ITEM_ID, IT.ITEM_CODE AS Code, TY.TYPE_DESC AS \"Item Type\", " +
                        //            "IT.ITEM_DESC AS \"Item Name\" " +
                        //            "FROM ITEM IT,ITEM_TYPE TY " +
                        //            "WHERE IT.ITEM_ID > 0 AND IT.TYPE_ID = TY.TYPE_ID AND IT.STAT = 0 " + strFilter;
                        //    frmlkp=new frmLookup(sSql,"Item Name","Items","Item Name",GridColumnFilterTxt);
                        //    frmlkp.HideColumns="ITEM_ID,Code";
                        //    break;
                        //case clsGeneral.LKP_ITEMDESC:
                        //    if (isStockReposition)
                        //        sSql =  "SELECT TY.TYPE_DESC AS \"Item Type\", IT.ITEM_DESC AS \"Item Name\" " +
                        //                "FROM ITEM IT, ITEM_TYPE TY " +
                        //                "WHERE IT.ITEM_ID>0 AND IT.TYPE_ID=TY.TYPE_ID " + strFilter + " " +
                        //                "ORDER BY IT.ITEM_DESC";
                        //    else
                        //        sSql =  "SELECT TY.TYPE_DESC AS \"Item Type\", IT.ITEM_DESC AS \"Item Name\" " +
                        //                "FROM ITEM IT, ITEM_TYPE TY " +
                        //                "WHERE IT.ITEM_ID>0 AND IT.TYPE_ID=TY.TYPE_ID" + strFilter;
                        //    frmlkp = new frmLookup(sSql,"Item Name","Items","Item Name",GridColumnFilterTxt);
                        //    break;
                        //case clsGeneral.LKP_PHAR_ITEMDESC:
                        //    sSql =  "SELECT IT.ITEM_DESC AS Item, IT.ITEM_CODE AS Code " +
                        //            "FROM ITEM IT, ITEM_TYPE TY " +
                        //            "WHERE IT.ITEM_ID>0 AND IT.TYPE_ID=TY.TYPE_ID" + strFilter;
                        //    frmlkp=new frmLookup(sSql,"ITEM","TEMS","Item",GridColumnFilterTxt);
                        //    break;
                        //    // -- To position the drug in the Rack & Shelfs -------
                        //case clsGeneral.LKP_DRUGTYPE:
                        //    frmlkp = new frmLookup("SELECT TYPE_ID, TYPE_DESC AS \"Item Type\" FROM ITEM_TYPE","Item Type","Item Type","","");
                        //    frmlkp.HideColumns = "TYPE_ID";
                        //    break;
                        //case clsGeneral.LKP_DRUGNAME:
                        //    sSql =	"SELECT IT.TYPE_DESC AS \"Item Type\", I.ITEM_ID, I.ITEM_DESC \"Item Name\" " +
                        //            "FROM ITEM I, ITEM_TYPE IT WHERE IT.TYPE_ID = I.TYPE_ID " + 
                        //            "AND I.ITEM_ID NOT IN (SELECT ITEM_ID FROM SHELF_ITEMS SI, SHELF S, " +
                        //            "DEPARTMENT_RACK DR WHERE SI.SHELF_ID = S.SHELF_ID AND S.RACK_ID = " +
                        //            "DR.RACK_ID AND DR.HDEPT_ID = " + iDepartmentId + ") " + strFilter;
                        //    frmlkp = new frmLookup(sSql,"Item Name","Items","Item Name",GridColumnFilterTxt);
                        //    frmlkp.HideColumns = "ITEM_ID";
                        //    break;
                        //    // ----------------------------------------------------

                        //    // -- To Reposition the drug in the Rack & Shelfs -----
                        //case clsGeneral.LKP_REPOSITIONDRUG:
                        //    sSql =	"SELECT IT.TYPE_DESC AS \"Item Type\", I.ITEM_DESC AS \"Item Name\", I.ITEM_ID " +
                        //        "FROM ITEM I, ITEM_TYPE IT WHERE IT.TYPE_ID = I.TYPE_ID " + strFilter;
                        //    frmlkp = new frmLookup(sSql,"Item Name","Items","Item Name",GridColumnFilterTxt);
                        //    frmlkp.HideColumns = "ITEM_ID";
                        //    break;
                        //case clsGeneral.LKP_REPOS_PHA_BACH_NUM:
                        //    sSql =	"select " +
                        //                "it.type_desc as \"Item Type\", i.item_desc as \"Item Name\", " +
                        //                "ds.bach_num as \"Batch No\" " +
                        //            "from item i, item_type it, dept_store_stock ds " +
                        //            "where ds.item_id = i.item_id and i.type_id = it.type_id " +
                        //            strFilter + " order by ds.item_id";
                        //    frmlkp = new frmLookup(sSql,"Batch No","Batch No", "", "");
                        //    break;
                        //case clsGeneral.LKP_REPOS_STO_BACH_NUM:
                        //    sSql =	"select " +
                        //                "it.type_desc as \"Item Type\", i.item_desc as \"Item Name\", " +
                        //                "s.bach_num as \"Batch No\" " +
                        //            "from item i, item_type it, store_stock s " +
                        //            "where s.item_id = i.item_id and i.type_id = it.type_id " +
                        //            strFilter + " order by s.item_id";
                        //    frmlkp = new frmLookup(sSql,"Batch No","Batch No","","");
                        //    break;
                        //    // ----------------------------------------------------

                        //    // -- To update the selling price of the drugs --------
                        //case clsGeneral.DRUG_NAME:
                        //    sSql =	"SELECT IT.TYPE_DESC AS \"Item Type\", I.ITEM_DESC AS \"Item Name\" " + 
                        //            "FROM ITEM I, ITEM_TYPE IT " +
                        //            "WHERE IT.TYPE_ID = I.TYPE_ID " + strFilter;
                        //    frmlkp = new frmLookup(sSql,"Item Name","Items","Item Name", GridColumnFilterTxt);
                        //    break;
                        //                        case clsGeneral.LKP_MULTIPLE_BATCH_NO:
                        //                            int iItemId		= 0;
                        //                            int iDeptId		= 0;
                        //                            int iDeptType	= 0;
                        //                            try
                        //                            {
                        //                                string[] strTemp = strFilter.Split(',');

                        //                                iItemId		= Convert.ToInt32(strTemp[0]);
                        //                                iDeptId		= Convert.ToInt32(strTemp[1]);
                        //                                iDeptType	= Convert.ToInt32(strTemp[2]);
                        //                            }
                        //                            catch
                        //                            {
                        //                                iItemId		= 0;
                        //                                iDeptId		= 0;
                        //                                iDeptType	= 0;
                        //                            }

                        //                            //frmDrugsBatchNo objDBN = new frmDrugsBatchNo(iItemId, iDeptId, iDeptType);
                        //                            //objDBN.ShowDialog();
                        //                            //if(objDBN.getSelectedBatchNo().ToString() !="" && ActiveControl.GetType().Name.ToString() == "TextBox" )
                        //                            //    ActiveControl.Text = objDBN.getSelectedBatchNo().ToString();
                        //                            //Lookup_Selected_Value  = objDBN.getSelectedBatchNo().ToString();
                        //                            break;
                        //                            // ----------------------------------------------------

                        //                        case clsGeneral.LKP_BATCHNUMBER:
                        //                            sSql =	"select distinct " +
                        //                                        "item_type.type_desc as \"Item Type\", item.item_desc as Item, " +
                        //                                        "store.bach_num as \"Batch No\", store.avil_qty as \"Avail Qty\", " +
                        //                                        "store.unt_pric as \"Unit Price\",store.exp_date as \"Expiry Date\" " +
                        //                                    "from " +
                        //                                        "item, item_type, store_stock store " +
                        //                                    "where " +
                        //                                        "item_type.type_id = item.type_id and " +
                        //                                        "store.item_id = item.item_id " + strFilter;
                        //                            frmlkp = new frmLookup(sSql, "Batch No", "Batch No","","");
                        //                            break;
                        //                        case clsGeneral.LKP_DATE:
                        //                            frmCalendar cal1 = new frmCalendar();
                        //                            cal1.ShowDialog();
                        //                            if(cal1.getSelectedDate().ToString() !="" && ActiveControl.GetType().Name.ToString() == "TextBox" )
                        //                                ActiveControl.Text=cal1.getSelectedDate().ToString();
                        //                            Lookup_Selected_Value=cal1.getSelectedDate().ToString();
                        //                            break;

                        //                        case clsGeneral.LKP_EMP:
                        //                            frmlkp=new frmLookup("SELECT USR_CODE AS \"USER CODE\", NAME FROM USERS WHERE STAT <> 2","NAME","Users","","");
                        //                            break;
                        //                        case clsGeneral.LKP_BATCH:
                        //                            frmlkp=new frmLookup("SELECT DISTINCT BACH_NUM AS \"Batch No\" FROM STORE,ITEM WHERE ITEM.ITEM_ID=STORE.ITEM_ID " + strFilter,"Batch No","Batch Nos","","");
                        //                            break;
                        //                        case clsGeneral.LKP_INVOICE:
                        //                            sSql =	"SELECT DISTINCT V.INV_NUM AS \"Invoice No\" " +
                        //                                "FROM STORE S, ORDER_DETAILS OD, INVOICE V, ITEM I, ITEM_TYPE IT " +
                        //                                "WHERE S.BACH_NUM=OD.BACT_NUM AND S.ITEM_ID=OD.ITEM_ID AND " +
                        //                                "OD.ORD_ID=V.ORD_ID AND I.ITEM_ID=S.ITEM_ID AND IT.TYPE_ID=I.TYPE_ID " + strFilter ;
                        //                            frmlkp=new frmLookup(sSql ,"Invoice No","Invoice Nos","","");
                        //                            break;
                        //                        case clsGeneral.LKP_ROLE:
                        //                            frmlkp=new frmLookup(clsAdminConstants.SUB_ROLE,1);							
                        //                            break;
                        //                        case clsGeneral.LKP_ACCOUNT_GROUP:
                        //                            frmlkp = new frmLookup(clsAdminQuery.getAccountQuery(clsAdminConstants.ACCOUNT_GROUP_LIST),"Group Code","Account Group","","");
                        //                            frmlkp.HideColumns = "Id";
                        //                            break;
                        //                        case clsGeneral.LKP_LABCODE:
                        //                            sSql = "select hdept_id, hdept_desc AS \"Lab\" from hospital_departments where hdept_type = 7";
                        //                            frmlkp=new frmLookup(sSql,"Lab","Lab","","");
                        //                            frmlkp.HideColumns = "hdept_id";
                        //                            break;
                        //                        case clsGeneral.LKP_TESTCODE:
                        //                            //if(clsGeneral.showMultipleLookup_LabTests())
                        //                            //    frmlkp = new frmLookup(new clsBill().getTestCodeQry(strFilter, lVisitNo),"Report Code","Report Code","Report Code",GridColumnFilterTxt,true,true);
                        //                            //else
                        //                            //    frmlkp = new frmLookup(new clsBill().getTestCodeQry(strFilter, lVisitNo),"Report Code","Report Code","Report Code",GridColumnFilterTxt);
                        //                            //frmlkp.HideColumns = "Report Id,Is Fixed";
                        //                            break;
                        //                        case clsGeneral.LKP_ORDERVENDORITEM:
                        //                            //BusinessLogic.Store.clsOrder objOrderVendor = new BusinessLogic.Store.clsOrder(iDepartmentId); 

                        //                            //if (clsGeneral.IsGeneralStore(iDepartmentId))
                        //                            //{
                        //                            //    frmlkp = new frmLookup(objOrderVendor.getOrderVendorSQL("ITEM NAME",0,iDepartmentId,"","","",strFilter),"Item Name","Item Name","Item Code",GridColumnFilterTxt);
                        //                            //    frmlkp.HideColumns = "Available Qty,Measurement,Measurement Id";
                        //                            //}
                        //                            //else
                        //                            //{
                        //                            //    frmlkp = new frmLookup(objOrderVendor.getOrderVendorSQL("ITEM NAME",0,iDepartmentId,"","","",strFilter),"Item Name","Item Name","Item Name",GridColumnFilterTxt);
                        //                            //    frmlkp.HideColumns = "Item Code,Available Qty,Measurement,Measurement Id";
                        //                            //}
                        //                            break;
                        //                        case clsGeneral.LKP_DRUGISSUEITEM:
                        //                            //BusinessLogic.Pharmacy.clsDepartmentStore objDrugIssue = new BusinessLogic.Pharmacy.clsDepartmentStore(); 
                        //                            //frmlkp = new frmLookup(objDrugIssue.getDrugIssuedSQL("BATCHSTOCK",iPharmacyLookupId,0,"",strFilter),"Drug Name","Drug Name","Drug Name",GridColumnFilterTxt);
                        //                            //frmlkp.HideColumns = "Drug Id,Drug Code,Unit Price,Vendor Price,Expiry Date,Is Free,Issue Measure,Issue Measure Id,Avail Measure Id,Remarks,Available Measure,Vendor Total,Actual Unit Price";
                        //                            break;
                        //                        //--- JG on 19-May-2006 ----------------------
                        //                        case clsGeneral.LKP_ISSUE_TYPE:
                        //                            sSql = new clsPharmacyQuery(iPharmacyLookupId).getSql(clsPharmacyConstant.ISSUEDRUGTOPATIENT.LKP_ITEMTYPE);
                        //                            frmlkp = new frmLookup(sSql,"Item Type","Item Type","","");
                        //                            break;
                        //                        case clsGeneral.LKP_ISSUE_ITEM:
                        //                            sSql = new clsPharmacyQuery(iPharmacyLookupId).getSql(clsPharmacyConstant.ISSUEDRUGTOPATIENT.LKP_ITEM);
                        //                            sSql += strFilter;
                        //                            frmlkp = new frmLookup(sSql,"Item Name","Items","Item Name",GridColumnFilterTxt);
                        //                            break;
                        //                        case clsGeneral.LKP_ISSUE_ITEM_CREATE:
                        //                            sSql = new clsPharmacyQuery(iPharmacyLookupId).getSql(clsPharmacyConstant.ISSUEDRUGTOPATIENT.LKP_ITEM_CREATE);
                        //                            sSql += strFilter;
                        //                            frmlkp = new frmLookup(sSql,"Item Name","Items","Item Name",GridColumnFilterTxt);
                        //                            break;
                        //                        case clsGeneral.LKP_ISSUE_BATCH:
                        //                            sSql = new clsPharmacyQuery(iPharmacyLookupId).getSql(clsPharmacyConstant.ISSUEDRUGTOPATIENT.LKP_BATCH);
                        //                            sSql += strFilter;
                        //                            frmlkp = new frmLookup(sSql,"Batch No","Batch No","","");

                        //                            if (clsGeneral.showPackSize() == 0)
                        //                                frmlkp.HideColumns = "Pack Size";
                        //                            break;
                        //                        //--------------------------------------------
                        //                        case clsGeneral.LKP_VISITNUMBER:
                        //                            frmlkp = new frmLookup(clsBillingQuery.getSql(clsBillingConstants.CONSOLIDATEDBILL.VISITNUMBER1) + " WHERE " + strFilter,"Visit Number","Visit Number","","");
                        //                            break;
                        //                        case clsGeneral.LKP_ACCOUNTHEADLIST:
                        //                            frmlkp = new frmLookup(clsBillingQuery.getSql(clsBillingConstants.CONSOLIDATEDBILL.ACCOUNTHEADLIST),"Details","Account Head","","");
                        //                            break;
                        //                        case (int) clsBillingConstants.CONSOLIDATEDBILL.WARDS_LOOKUP:
                        //                            frmlkp = new frmLookup(clsBillingQuery.getSql(clsBillingConstants.CONSOLIDATEDBILL.WARD_CHARGE_ACCHEAD), "Details", "Account Head", "Details", GridColumnFilterTxt);
                        //                            frmlkp.HideColumns = "ACCH_ID";
                        //                            frmlkp.isWardLookup = true;
                        //                            break;
                        //                        case (int) clsBillingConstants.CONSOLIDATEDBILL.WARDS_FINALISED_LOOKUP:
                        //                            frmlkp = new frmLookup(clsBillingQuery.getSql(clsBillingConstants.CONSOLIDATEDBILL.WARDS_FINALISED_ACCHEAD), "Details", "Account Head", "Details", GridColumnFilterTxt);
                        //                            frmlkp.HideColumns = "ACCH_ID";
                        //                            frmlkp.isWardLookup = true;
                        //                            break;
                        //                        case (int) clsBillingConstants.INTERMEDIATEBILL.WARDS_LOOKUP:
                        //                            frmlkp = new frmLookup(clsBillingQuery.getIMSql(clsBillingConstants.INTERMEDIATEBILL.WARD_CHARGE_ACCHEAD), "Details", "Account Head", "Details", GridColumnFilterTxt);
                        //                            frmlkp.HideColumns = "ACCH_ID";
                        //                            frmlkp.isWardLookup = true;
                        //                            break;
                        //                        case clsGeneral.LKP_ALLACCOUNTHEADS:
                        //                            frmlkp = new frmLookup(clsBillingQuery.getSql(clsBillingConstants.CONSOLIDATEDBILL.ACCOUNTHEADS)+strFilter,"Details","Accounts","","");
                        //                            break;
                        //                        case clsGeneral.LKP_DOCTOR_ACCOUNT_HEADS:
                        //                            frmlkp = new frmLookup(clsPeople.getPeopleQry(clsAdminConstants.DOCTOR_ACCOUNT_HEADS), "ACC CODE", "Doctor Account Heads", "", "");
                        //                            break;
                        //                        /* ----------------------------------------------------------------------------------------------
                        //                        * Name		: JG
                        //                        * Date		: 25-Jan-2005 08:45 AM
                        //                        * Purpose	: To increment the performance
                        //                        * */
                        //                        case clsGeneral.LKP_DEPT_ITEM:
                        //                            frmlkp = new frmLookup(new clsStoreQuery().getSql(clsStoreQuery.LKP_DEPT_ITEM) + strFilter,"ITEM NAME","ITEMS","ITEM NAME",GridColumnFilterTxt);
                        //                            frmlkp.HideColumns="ITEM_ID";
                        //                            break;
                        //                        case clsGeneral.LKP_DEPT_TYPE:
                        //                            frmlkp = new frmLookup(new clsStoreQuery().getSql(clsStoreQuery.LKP_DEPT_TYPE) + strFilter,"ITEM TYPE","ITEM TYPE","","");
                        //                            break;
                        //                        case clsGeneral.LKP_DEPT_BATCH:
                        //                            sSql = new clsStoreQuery().getSql(clsStoreQuery.LKP_DEPT_BATCH) + strFilter;
                        //                            sSql = sSql.Replace("<DEPTID>", iDepartmentId.ToString());
                        //                            frmlkp = new frmLookup(sSql,"BATCH","BATCH","","");						
                        //                            frmlkp.HideColumns="ITEM_ID,PHAR_MEAS_SYMB,PHAR_MEAS_ID,NET_QTY_MEAS_SYMB,NET_QTY_MEAS_ID";
                        //                            break;
                        //                        // ----------------------------------------------------------------------------------------------
                        //                        case clsGeneral.LKP_RETURN_BATCH:
                        //                            //sSql = new BusinessLogic.Store.clsVendorReturn().getLookupQry("BATCHNO")+ strFilter;
                        //                            //sSql = new BusinessLogic.Store.clsVendorReturn().getLookupQry("BATCHNO");
                        //                            //sSql += "and s.hdept_id = " + iDepartmentId;
                        //                            //sSql += strFilter;
                        //                            //frmlkp = new frmLookup(sSql, "Batch No", "Batch No","","");
                        //                            break;
                        //                        case clsGeneral.LKP_RETURN_ITEM:
                        //                            //sSql = new BusinessLogic.Store.clsVendorReturn().getLookupQry("ITEMS")+ strFilter;
                        //                            //frmlkp = new frmLookup(sSql,"Item Name","Items","Item Name",GridColumnFilterTxt);
                        //                            break;
                        //                        case clsGeneral.LKP_RETURN_INVOICE:
                        //                            //sSql = new BusinessLogic.Store.clsVendorReturn().getLookupQry("INVOICE")+ strFilter;
                        //                            //frmlkp = new frmLookup(sSql, "Invoice No", "Invoice No","","");
                        //                            break;
                        //                        case clsGeneral.LKP_ORDER_NUMBER:
                        //                            sSql =	"SELECT ORD_NUM AS \"ORDER NUMBER\", ORD_DATE AS \"DATE\", " +
                        //                                    "vendor.vend_name AS VENDOR FROM ORDERS, " +
                        //                                    "VENDOR WHERE ORDERS.VEND_CODE = VENDOR.VEND_CODE AND " +
                        //                                    "HDEPT_ID = " + clsStoreConstants.StoreId + " AND ORD_STAT IN(" + (int) clsStoreConstants.ORDERSTATUS.ORDERED + "," + 
                        //                                    (int) clsStoreConstants.ORDERSTATUS.PARTIALLYRECEIVED + "," + (int) clsStoreConstants.ORDERSTATUS.FULLYRECEIVED + ") ORDER BY ORD_ID DESC";
                        //                            frmlkp = new frmLookup(sSql,"ORDER NUMBER","ORDERS","","");
                        //                            break;
                        //                        case clsGeneral.LKP_GRN_NUMBER:
                        //                            sSql = "SELECT ORD_NUM AS \"ORDER NO\", GRN_NO AS \"GRN NO\" FROM INVOICES " + strFilter;
                        //                            frmlkp = new frmLookup(sSql,"GRN NO","GRN NO LIST","","");
                        //                            break;
                        //                        case clsGeneral.LKP_VISIT_DETAIL:
                        //                            if(strFilter == "")
                        //                            {
                        //                                MessageBox.Show("Please Enter a Registration No","Payroll");
                        //                                return;
                        //                            }
                        //                            sSql = "SELECT TO_CHAR(VIST_NUMB,'999999999999999') AS \"Visit No\", VIST_DATE AS \"Visit Date\" FROM VISIT_DETAILS" + strFilter;
                        //                            frmlkp = new frmLookup(sSql, "Visit No","","","");
                        //                            break;
                        //                        case clsGeneral.LKP_REG_NUMBER:
                        //                            objSearch = new frmAdvanceSearch();
                        //                            isRegNumb = true;
                        //                            break;

                        //                        case clsGeneral.LKP_WRD_PAT_DRG_TYPE:
                        //                            sSql =	"SELECT DISTINCT IT.TYPE_DESC AS \"DRUG TYPE\" FROM ITEM_TYPE IT, ITEM I, WARD_PHARMACY_DETAILS WPD, WARD_PHARMACY WP " +
                        //                                "WHERE WPD.HDEPT_ID = WP.HDEPT_ID AND WPD.ITEM_ID = I.ITEM_ID AND I.TYPE_ID = IT.TYPE_ID AND WP.WARD_PHAR_ID = WPD.WARD_PHAR_ID AND " + strFilter;
                        //                            frmlkp = new frmLookup(sSql,"DRUG TYPE","DRUG TYPE","","");
                        //                            break;
                        //                        case clsGeneral.LKP_WRD_PAT_DRG_NAME:
                        //                            sSql =	"SELECT DISTINCT I.ITEM_DESC AS \"DRUG NAME\" FROM ITEM_TYPE IT, ITEM I, WARD_PHARMACY_DETAILS WPD, WARD_PHARMACY WP " +
                        //                                "WHERE WPD.HDEPT_ID = WP.HDEPT_ID AND WPD.ITEM_ID = I.ITEM_ID AND WP.WARD_PHAR_ID = WPD.WARD_PHAR_ID AND I.TYPE_ID = IT.TYPE_ID AND " + strFilter;
                        //                            frmlkp = new frmLookup(sSql,"DRUG NAME","DRUG NAME","","");
                        //                            break;
                        //                        case clsGeneral.LKP_WRD_PAT_BATCHNO:
                        //                            sSql =	"SELECT " +
                        //                                "DISTINCT WPD.WARD_PHAR_DET_ID, WPD.QTY, WPD.QTY_MEAS_SYMB, " +
                        //                                "NVL(WPD.QTY_MEAS_ID,1) AS QTY_MEAS_ID, WPD.UNIT_PRIC, " +
                        //                                "NVL(WPD.RETUN_QTY,0) AS RETURNED, WPD.RETURN_QTY_MEAS_SYMB, " +
                        //                                "NVL(WPD.RETUN_QTY_MEAS_ID,NVL(WPD.QTY_MEAS_ID,1)) AS RETUN_QTY_MEAS_ID, " +
                        //                                "DS.EXP_DATE, WPD.BACH_NUMB AS \"BATCH NO\" " +
                        //                                "FROM " +
                        //                                "ITEM_TYPE IT, ITEM I, WARD_PHARMACY_DETAILS WPD, " +
                        //                                "WARD_PHARMACY WP, DEPARTMENT_STORE DS " +
                        //                                "WHERE " +
                        //                                "WPD.HDEPT_ID = WP.HDEPT_ID AND WPD.ITEM_ID = I.ITEM_ID AND " +
                        //                                "WP.WARD_PHAR_ID = WPD.WARD_PHAR_ID AND I.TYPE_ID = IT.TYPE_ID AND " + 
                        //                                "DS.ITEM_ID = I.ITEM_ID AND DS.BACH_NUM = WPD.BACH_NUMB AND " +
                        //                                "DS.HDEPT_ID = WP.HDEPT_ID AND " + strFilter;
                        //                            frmlkp = new frmLookup(sSql,"BATCH NO","BATCH NO","","");
                        //                            frmlkp.HideColumns = "WARD_PHAR_DET_ID,QTY,QTY_MEAS_SYMB,QTY_MEAS_ID,UNIT_PRIC,RETURNED,RETURN_QTY_MEAS_SYMB,RETUN_QTY_MEAS_ID,EXP_DATE";
                        //                            break;
                        //                        case clsGeneral.LKP_LAB_REPORT_ACCOUNT_HEADS:
                        //                            sSql = clsLabQuery.getLabQuery(clsLabConstants.LAB_CHARGE_ACCOUNTS);
                        //                            frmlkp = new frmLookup(sSql,"ACC CODE","ACC CODE","","");
                        //                            break;
                        //                        case clsGeneral.LKP_LAB_DOCTOR:
                        //                            sSql = clsLabQuery.getLabQuery(clsLabConstants.LKP_DOCTOR_DETAILS);
                        //                            frmlkp = new frmLookup(sSql,"DOCTOR","DOCTOR","DOCTOR CODE",GridColumnFilterTxt);
                        //                            break;
                        //                        case clsGeneral.LKP_DEPARTMENT:
                        //                            sSql = "SELECT DEPT_CODE AS \"DEPT CODE\", DEPT_DESC AS DEPARTMENT FROM DEPARTMENTS";
                        //                            frmlkp = new frmLookup(sSql,"DEPARTMENT","CLINICAL TYPE","","");
                        //                            break;
                        //                        case clsGeneral.LKP_CASE:
                        //                            sSql = "SELECT CASE_CODE AS \"CASE CODE\", CASE_DESC AS \"CASE\" FROM CASE_DETAILS";
                        //                            frmlkp = new frmLookup(sSql,"CASE","CASE DETAILS","","");
                        //                            break;
                        //                        case clsGeneral.LKP_DOCTOR_FEE_CATEGORY:
                        //                            sSql =	"SELECT FEE_CAT_ID, FEE_CAT_DESC " +
                        //                                "AS \"DOCTOR FEE CATEGORY\" FROM DOCTOR_FEE_CATEGORY";
                        //                            frmlkp = new frmLookup(sSql,"DOCTOR FEE CATEGORY","DOCTOR FEE CATEGORY","","");
                        //                            frmlkp.HideColumns = "FEE_CAT_ID";
                        //                            break;
                        //                        case clsGeneral.LKP_BILL_DOCTOR:
                        //                            sSql = clsLabQuery.getLabQuery(clsLabConstants.LKP_DOCTOR_DETAILS);
                        //                            frmlkp = new frmLookup(sSql,"DOCTOR CODE","DOCTOR CODE","","");
                        //                            break;
                        //                        case clsGeneral.LKP_OPER_DOCTOR:
                        //                            sSql = clsLabQuery.getLabQuery(clsLabConstants.LKP_DOCTOR_DETAILS);
                        //                            sSql += strFilter;
                        //                            frmlkp = new frmLookup(sSql,"DOCTOR CODE","DOCTOR CODE","","");
                        //                            break;
                        //                        case clsGeneral.LKP_GENERAL_CHARGES:
                        //                            sSql = clsBillingQuery.getGeneralProcQuery(clsBillingConstants.GENERALPROCEDURE.ACCOUNTHEADS);
                        //                            sSql += strFilter;
                        //                            frmlkp = new frmLookup(sSql,"Particulars","Particulars","","");
                        //                            frmlkp.HideColumns = "Code";
                        //                            break;
                        //                        case clsGeneral.LKP_DEFAULTCHARGE:
                        //                            sSql = clsAdminQuery.getAccountQuery(clsAdminConstants.WARD_ACCOUNT_HEADS);;
                        //                            frmlkp = new frmLookup(sSql,"ACC CODE","Default Charge Heads","","");
                        //                            frmlkp.HideColumns = "ACCID";
                        //                            break;
                        //                        case (int)clsGeneral.COSTCENTER.LKP_LAB_PAYMENT:
                        //                            sSql = clsGeneral.getPaymentCostCenterQuery(clsGeneral.COSTCENTER.LKP_LAB_PAYMENT);
                        //                            frmlkp = new frmLookup(sSql,"Doctor code","Cost Center","Doctor code",GridColumnFilterTxt);
                        //                            break;
                        //                        case (int)clsGeneral.COSTCENTER.LKP_WRD_COST_CENTER:
                        //                            sSql = clsGeneral.getPaymentCostCenterQuery(clsGeneral.COSTCENTER.LKP_WRD_COST_CENTER);
                        //                            frmlkp = new frmLookup(sSql,"Code","Cost Center","Code",GridColumnFilterTxt);
                        //                            break;
                        //                        case clsGeneral.LKP_VISIT_DOCTOR:
                        //                            sSql = "SELECT DISTINCT DVC.DOC_CODE AS \"DOCTOR CODE\", D.NAME AS DOC_NAME " +
                        //                                    "FROM DOCTOR_VISIT_CHARGES DVC, DOCTOR D, DOCT_DEPT DD " +
                        //                                    "WHERE DVC.DOC_CODE = D.DOCT_CODE AND DD.DOCT_CODE = D.DOCT_CODE AND DVC.AMOUNT>0";
                        //                            frmlkp = new frmLookup(sSql,"DOCTOR CODE","Doctor","DOCTOR CODE",GridColumnFilterTxt);
                        //                            break;
                        //                        case clsGeneral.LKP_VISIT_TYPE:
                        //                            sSql = "SELECT DVC.DOC_CODE, AH.ACC_DESC AS \"VISIT TYPE\", " +
                        //                                    "DVC.AMOUNT FROM DOCTOR_VISIT_CHARGES DVC, DOCTOR D, ACCOUNTHEADS AH " +
                        //                                    "WHERE DVC.DOC_CODE = D.DOCT_CODE AND DVC.ACC_ID = AH.ACCH_ID" + strFilter;
                        //                            frmlkp = new frmLookup(sSql,"VISIT TYPE","Visit Type","VISIT TYPE",GridColumnFilterTxt);
                        //                            frmlkp.HideColumns = "DOC_CODE";
                        //                            break;
                        //                        case clsGeneral.LKP_WARD_CHARGES:
                        //                            //sSql = clsAdminQuery.getAccountQuery(clsAdminConstants.WARD_ACCOUNT_HEADS);;
                        ////							sSql = "SELECT AH.ACC_CODE as \"Acc Code\", AH.ACC_DESC AS \"Description\", " +
                        ////								"MC.ACCH_AMOUNT AS \"Amount\", AH.ACCH_ID As \"AccId\" " +
                        ////								"FROM ACCOUNTHEADS AH, ACCOUNT_GROUP AG, MISCELL_CHRG_MAPPING MC WHERE " +
                        ////								"AH.ACC_GRP_CODE = AG.AGRP_CODE AND AG.MODU = 4 AND MC.ACCH_ID = AH.ACCH_ID " +
                        ////								"AND AH.ACC_TYPE = 1 AND AH.TRAN_TYPE = 2 " + strFilter + " UNION ALL " +
                        ////								"SELECT AH.ACC_CODE, AH.ACC_DESC, MC.ACCH_AMOUNT, AH.ACCH_ID " +
                        ////								"FROM ACCOUNTHEADS AH, ACCOUNT_GROUP AG, MISCELL_CHRG_MAPPING MC WHERE " +
                        ////								"AH.ACC_GRP_CODE = AG.AGRP_CODE AND AG.MODU = 7 AND MC.ACCH_ID = AH.ACCH_ID " +
                        //                            //								"AND AH.ACC_TYPE = 1 AND AH.TRAN_TYPE = 2 " + strFilter + " AND MC.ACCH_AMOUNT > 0";
                        //                            sSql = "SELECT AH.ACC_CODE as \"Acc Code\", AH.ACC_DESC AS \"Description\", " +
                        //                                "MC.ACCH_AMOUNT AS \"Amount\", AH.ACCH_ID As \"AccId\" " +
                        //                                "FROM ACCOUNTHEADS AH, ACCOUNT_GROUP AG, MISCELL_CHRG_MAPPING MC WHERE " +
                        //                                "AH.ACC_GRP_CODE = AG.AGRP_CODE AND AG.MODU = 4 AND MC.ACCH_ID = AH.ACCH_ID " +
                        //                                "AND AH.ACC_TYPE = 1 AND AH.TRAN_TYPE = 2 " +
                        //                                "AND NVL(MC.CLASS_ID,(SELECT OP_CLASS FROM OPD_CLASS_ID)) = GET_PATIENT_CLASS(" + lVisitNo + ") " +
                        //                                "UNION ALL " +
                        //                                "SELECT AH.ACC_CODE, AH.ACC_DESC, MC.ACCH_AMOUNT AS AMUN, AH.ACCH_ID " +
                        //                                "FROM ACCOUNTHEADS AH, ACCOUNT_GROUP AG, MISCELL_CHRG_MAPPING MC WHERE " +
                        //                                "AH.ACC_GRP_CODE = AG.AGRP_CODE AND AG.MODU = 7 AND MC.ACCH_ID = AH.ACCH_ID " +
                        //                                "AND AH.ACC_TYPE = 1 AND AH.TRAN_TYPE = 2 " +
                        //                                "AND NVL(MC.CLASS_ID,(SELECT OP_CLASS FROM OPD_CLASS_ID)) = GET_PATIENT_CLASS(" + lVisitNo + ") " +
                        //                                "AND MC.ACCH_AMOUNT > 0";
                        //                            frmlkp = new frmLookup(sSql,"Description","Ward Charges","Description",GridColumnFilterTxt);
                        //                            frmlkp.HideColumns = "ACCID";
                        //                            break;
                        //                        case clsGeneral.LKP_OPR_CHARGES:
                        ////							sSql = "SELECT DISTINCT AH.ACC_CODE AS CODE, AH.ACC_DESC AS DETAILS, " +
                        ////								"AH.AMUN AS \"UNIT PRICE\" FROM ACCOUNTHEADS AH, ACCOUNT_GROUP AG " +
                        ////								"WHERE AH.AGRP_ID = AG.AGRP_ID AND " +
                        ////								"AG.MODU = 4 AND AH.ACC_TYPE=1 AND AH.TRAN_TYPE=2";
                        //                            sSql = "SELECT AH.ACC_CODE AS \"CODE\", AH.ACC_DESC AS \"DETAILS\", WCM.ACC_AMNT " +
                        //                                "AS \"UNIT PRICE\" FROM WARD_CHARGE_CATE_MAPPING WCM, ACCOUNTHEADS AH WHERE " +
                        //                                "WCM.CATE_ID = " + strFilter + " AND AH.ACCH_ID = WCM.ACCH_ID AND NVL(WCM.CLASS_ID, " +
                        //                                "(SELECT OP_CLASS FROM OPD_CLASS_ID)) = GET_PATIENT_CLASS(" + lVisitNo + ") " +
                        //                                "UNION ALL SELECT AH.ACC_CODE, AH.ACC_DESC, MC.ACCH_AMOUNT FROM ACCOUNTHEADS AH, " +
                        //                                "MISCELL_CHRG_MAPPING MC WHERE MC.ACCH_ID = AH.ACCH_ID AND AH.ACC_TYPE = 1 AND " +
                        //                                "AH.TRAN_TYPE = 2 AND NVL(MC.CLASS_ID, (SELECT OP_CLASS FROM OPD_CLASS_ID)) = " +
                        //                                "GET_PATIENT_CLASS(" + lVisitNo + ") AND MC.ACCH_AMOUNT > 0";
                        //                            frmlkp = new frmLookup(sSql, "DETAILS","Ward Charges","DETAILS",GridColumnFilterTxt);
                        //                            break;
                        //                        case clsGeneral.LKP_OPR_COST_CENT:
                        //                            sSql = "SELECT CC.COST_CENT_CODE, CC.COST_CENT_DESC FROM COST_CENTRE CC";
                        //                            frmlkp = new frmLookup(sSql,"COST_CENT_CODE", "Doctor Codes","COST_CENT_CODE",GridColumnFilterTxt);
                        //                            break;
                        //                        case clsGeneral.LKP_WARD_ROOM_BED:
                        //                            sSql ="SELECT BOD.WARD_NUMB|| ', '||BOD.ROOM_NUMB||', '||BOD.BED_NUMB AS BED " +
                        //                                "FROM BED_OCCUPANCY_DETAILS BOD WHERE BOD.VIST_NUMB=" + lVisitNo + " AND BOD.OCCU_ID IN " +
                        //                                "(SELECT MIN(T.OCCU_ID) FROM BED_OCCUPANCY_DETAILS T WHERE T.VIST_NUMB = BOD.VIST_NUMB AND " +
                        //                                "TO_DATE(TO_CHAR(T.STRT_DATE, 'DD/MM/YYYY HH:MI AM'), 'DD/MM/YYYY HH:MI AM') >= " +
                        //                                "TO_DATE(TO_CHAR(BOD.STRT_DATE, 'DD/MM/YYYY HH:MI AM'), 'DD/MM/YYYY HH:MI AM') " +
                        //                                "AND TO_DATE(TO_CHAR(T.STRT_DATE, 'DD/MM/YYYY HH:MI AM'), 'DD/MM/YYYY HH:MI AM') < " +
                        //                                "TO_DATE(TO_CHAR(NVL(BOD.END_DATE, SYSDATE), 'DD/MM/YYYY HH:MI AM'), 'DD/MM/YYYY HH:MI AM'))";
                        //                            frmlkp = new frmLookup(sSql,"BED","BED DETAILS","BED",GridColumnFilterTxt);
                        //                            break;
                        //                        default:
                        //                            iLookupIndexId = 0;
                        //                            break;
                    }

                //        if (iLookupIndexId > 0 && isRegNumb == false) 
                //        {
                //            if (iLookupIndexId != clsGeneral.LKP_DATE && iLookupIndexId != clsGeneral.LKP_MULTIPLE_BATCH_NO)
                //            {
                //                frmlkp.ShowDialog();
                //                Lookup_Selected_Value=frmlkp.RtnValue;
                //                Lookup_Selected_Row=frmlkp.LookupRow;
                //            }
                //            if (ShowLookup!= null) ShowLookup(iLookupIndexId);

                //            if (nPreviousLkpId != iLookupIndexId && nPreviousLkpId>=0) 
                //            {
                //                strFilter="";
                //                GridColumnFilterTxt="";
                //            }
                //            nPreviousLkpId = iLookupIndexId;
                //        }
                //        else if (iLookupIndexId > 0 && isRegNumb == true)
                //        {
                //            //objSearch.ShowDialog();
                //            //if (objSearch.getRegistrationNo() != "") Lookup_Selected_Value = objSearch.getRegistrationNo();

                //            //if (ShowLookup!= null) ShowLookup(iLookupIndexId);

                //            //if (nPreviousLkpId != iLookupIndexId && nPreviousLkpId>=0) 
                //            //{
                //            //    strFilter="";
                //            //    GridColumnFilterTxt="";
                //            //}

                //            //nPreviousLkpId = iLookupIndexId;
                //            //isRegNumb = false;
                //        }
                //        break;
                //    //case clsGeneral.LKP_SAVE_KEY:
                //    //    if (SaveRecord != null) SaveRecord(sender,e);
                //    //    break;
                //    default:
                //        break;
                //}
                //CallDirectly=false;
            //}
        }
		
		public void CallKeyDown(object sender, System.Windows.Forms.KeyEventArgs e)
		{
			CallDirectly=true;
			frmHMSTemplate_KeyDown(sender,e);
		}

		private void frmHMSTemplate_MouseMove(object sender, System.Windows.Forms.MouseEventArgs e)
		{
			MouseX=e.X;
			MouseY=e.Y;
		}

		private void frmHMSTemplate_KeyPress(object sender, System.Windows.Forms.KeyPressEventArgs e)
		{
			string SpecialChars="'[]|";
			if (e.KeyChar!=(char)Keys.Return | e.KeyChar!=(char)Keys.Back)
				e.Handled = ((SpecialChars.IndexOf(e.KeyChar.ToString()))!=-1);
			if (e.Handled) MessageBox.Show("Invalid Character",clsGeneral.MsgCaption);

			if (ActiveControl.GetType().Name=="TextBox")
			{
				if (getControlBinding(ActiveControl)=="0")
				{
					e.Handled = true;
					if ((e.KeyChar <= 57) && (e.KeyChar >= 48) || (e.KeyChar == 13) || 
						(e.KeyChar == 8) || (e.KeyChar == 46))
						e.Handled = false;
				}
				PreControl=ActiveControl;
			}
		}

		private string getControlBinding(Control Ctl)
		{
			foreach( Binding  cl in Ctl.DataBindings)
			{
				return cl.DataSource.ToString();
			}
			return "";
		}
	}
}