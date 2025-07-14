using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Utility.Common;
using Payroll.DAO.Schema;
using Bosco.DAO.Data;
using Bosco.Utility;
using Bosco.Utility.ConfigSetting;

namespace Payroll.Model.UIModel
{
    public class clsPrReport : SystemBase
    {
        ApplicationSchema.PRCOMPMONTHDataTable dtCompMonth = null;
        public clsPrReport()
        {
            dtCompMonth = this.AppSchema.PRCOMPMONTH;
            sStaffName = "CONCAT(stfpersonal.firstname,CONCAT(' ', IFNULL(stfpersonal.MIDDLE_NAME,'')),CONCAT(' ',stfpersonal.lastname))"; //" CONCAT(stfpersonal.firstname , ' ' , stfpersonal.lastname) ";
        }

        private long mvarPayRollId = 0;
        private long mvarGroupId = 0;
        private long mvarCompType = 0;
        private string sStaffName = "";
        private string sReportField = "";
        private string sFieldType = "#";                     //Fields and Types of the report table
        private string sFinalReportField = "";               //Finalized fields for using it in the query
        private string sColumnTotalFields = "@";              //Fields of the report table that have to be summed up in the report. Only numerical fields
        private string sReportTable = "PRReportTable";  //Report Table name
        private string[] sSumFields;				  //To maintain sum fields for entire report
        private string[] sGroupByFields;				  //To maintain group by fields for entire report
        private string[] sSumFields1;				  //To maintain sum by fields for each report
        private string[] sGroupByFields1;                 // To maintain group by fields for each report
        private string sTextFields = "";
        private string sNumericFields = "";


        private clsPrLoan objPrLoan = new clsPrLoan();
        private clsModPay objmodPay = new clsModPay();
        private clsPRPSReport objPRPSReport = new clsPRPSReport();
        private DataView dvReportTableView;
        ResultArgs resultArgs = null;

        public long CompType
        {
            get { return mvarCompType; }
            set { mvarCompType = value; }
        }
        public long PayRollId
        {
            get { return mvarPayRollId; }
            set { mvarPayRollId = value; }
        }
        public long GroupId
        {
            get { return mvarGroupId; }
            set { mvarGroupId = value; }
        }
        public string ColumnTotalFields
        {
            get { return sColumnTotalFields; }
        }
        /**
		*Purpose    : This method is used to Create the Table for All Components
		*ReturnType : As Boolean. If the Table is Created or Not.
		**/
        public bool CreateReportTable()
        {
            string sSql = "";
            string sCreateSql = "";
            string sCheckField = "";
            DataTable rsComp = new DataTable();

            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchComponentReport, "Component"))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
                if (resultArgs.Success)
                    rsComp = resultArgs.DataSource.Table;
                //dh.createDataSet(sSql, "Component");
                //rsComp = dh.getDataSet();
                DataTable dtPRComp = rsComp;
                sCreateSql = "create table " + sReportTable + "(staffid " + "INT(10)" +
                              ",groupid " + "INT(10)";
                sSumFields = new string[rsComp.Rows.Count];
                sGroupByFields = new string[rsComp.Rows.Count];

                for (int i = 0; i < rsComp.Rows.Count; i++)
                {
                    if (sCheckField.IndexOf("!" + rsComp.Rows[i]["Component"].ToString() + "!", 0) == -1)
                    {
                        string sTempField = rsComp.Rows[i]["Component"].ToString();

                        if (int.Parse(rsComp.Rows[i]["Type"].ToString()) <= 1 & int.Parse(rsComp.Rows[i]["CompRound"].ToString()) == 0)
                        {
                            sFieldType += sTempField.ToUpper() + "@" + typeof(double).ToString() + "#";

                            sCreateSql += ",`" + sTempField + "`  " + "varchar(200)";
                            sReportField += sReportTable + ".\"" + sTempField + "\",";
                            sFinalReportField += "IFNULL(truncate(" + sReportTable + ".\"" + sTempField + "\",2),'0')" + " as \"" + sTempField + "\",";
                            sColumnTotalFields += sTempField + "@";
                            sSumFields[i] = "IFNULL(truncate(SUM(" + sReportTable + ".\"" + sTempField + "\"),2),'0') as \"" + sTempField + "\"";
                            sGroupByFields[i] = "";
                            sNumericFields += "IFNULL(truncate(" + sReportTable + ".\"" + sTempField + "\",2),'0')" + " as \"" + sTempField + "\",";
                        }
                        else if (int.Parse(rsComp.Rows[i]["Type"].ToString()) <= 1 & int.Parse(rsComp.Rows[i]["CompRound"].ToString()) > 0)
                        {
                            sFieldType += sTempField.ToUpper() + "@" + typeof(double).ToString() + "#";

                            sCreateSql += ",`" + sTempField + "`  " + "varchar(200)";
                            sReportField += sReportTable + ".\"" + sTempField + "\",";
                            sFinalReportField += "IFNULL(truncate(" + sReportTable + ".\"" + sTempField + "\",2),'0.00')" + " as \"" + sTempField + "\",";
                            sColumnTotalFields += sTempField + "@";
                            sSumFields[i] = "IFNULL(truncate(SUM(" + sReportTable + ".\"" + sTempField + "\"),2),'0.00') as \"" + sTempField + "\"";
                            sGroupByFields[i] = "";
                            sNumericFields += "IFNULL(truncate(" + sReportTable + ".\"" + sTempField + "\",2),'0.00')" + " as \"" + sTempField + "\",";
                        }

                        else
                        {
                            sFieldType += sTempField.ToUpper() + "@" + typeof(string).ToString() + "#";

                            sCreateSql += ",`" + sTempField + "`  " + "varchar(200)";
                            sReportField += sReportTable + ".\"" + sTempField + "\",";
                            sFinalReportField += sReportTable + ".\"" + sTempField + "\",";
                            sSumFields[i] = "" + sReportTable + ".\"" + sTempField + "\"  as \"" + sTempField + "\"";
                            sGroupByFields[i] = sReportTable + ".\"" + sTempField + "\"";
                            sTextFields += sReportTable + ".\"" + sTempField + "\",";
                        }
                    }
                    sCheckField = sCheckField + "!" + rsComp.Rows[i]["component"].ToString() + "!";


                }
                sCreateSql = sCreateSql + ")";
                if (sReportField != null)
                {
                    sReportField = sReportField.Trim();
                    sFinalReportField = sFinalReportField.Trim();
                    sNumericFields = sNumericFields.Trim();
                    sTextFields = sTextFields.Trim();
                }
                if (sReportField != null)
                {
                    sReportField = sReportField.TrimEnd(',');
                    sFinalReportField = sFinalReportField.TrimEnd(',');
                    sNumericFields = sNumericFields.TrimEnd(',');
                    sTextFields = sTextFields.TrimEnd(',');
                }
                //dh.createDataSet(sCreateSql,"PrReport")
                if (DropReportTable()) //On 29/05/2019, before create report able, drop it if exists
                {
                    using (DataManager dataManager = new DataManager())
                    {
                        //dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                        resultArgs = dataManager.UpdateData(sCreateSql);
                    }
                }
                if (!resultArgs.Success)
                {
                    XtraMessageBox.Show("Unable to create report", "Payroll", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return false;
                }

                FillReport();

                rsComp.Dispose();
                return resultArgs.RowsAffected > 0;

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message, "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }
        /*	Purpose    : This method is used to get the Staff SQL  for the Selected Department
		*	Arguments  : nDeptId -> For the Selected Department Id
		*	ReturnType : As String (SQL)
		*/
        /**
      *Purpose    : This method is used to Create the Table for All Components
      *ReturnType : As Boolean. If the Table is Created or Not.
      **/
        public bool CreateReportTable(string date1, string date2)
        {
            string sSql = "";
            string sCreateSql = "";
            string sCheckField = "";
            DataTable rsComp = new DataTable();

            //sSql = "select " +
            //    " distinct prcomponent.component, prcomponent.Type," +
            //    " prcompmonth.comp_order,prcomponent.CompRound " +
            //    " from prcomponent,prcompmonth " +
            //    " where prcomponent.componentid=prcompmonth.componentid and " +
            //    " prcompmonth.payrollid = " + clsGeneral.PAYROLL_ID +
            //    " and prcompmonth.comp_order > 0 " +
            //    " order by prcompmonth.comp_order";
            try
            {

                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchComponentReport, "Component"))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
                if (resultArgs.Success)
                    rsComp = resultArgs.DataSource.Table;
                //dh.createDataSet(sSql, "Component");
                //rsComp = dh.getDataSet();
                DataTable dtPRComp = rsComp;
                sCreateSql = "create table " + sReportTable + "(staffid " + "INT(10)" +
                              ",groupid " + "INT(10)";
                sSumFields = new string[rsComp.Rows.Count];
                sGroupByFields = new string[rsComp.Rows.Count];

                for (int i = 0; i < rsComp.Rows.Count; i++)
                {

                    if (sCheckField.IndexOf("!" + rsComp.Rows[i]["Component"].ToString() + "!", 0) == -1)
                    {
                        string sTempField = rsComp.Rows[i]["Component"].ToString();

                        if (int.Parse(rsComp.Rows[i]["Type"].ToString()) <= 1 & int.Parse(rsComp.Rows[i]["CompRound"].ToString()) == 0)
                        {
                            sFieldType += sTempField.ToUpper() + "@" + typeof(int).ToString() + "#";

                            sCreateSql += ",`" + sTempField + "`  " + "varchar(200)";
                            sReportField += sReportTable + ".\"" + sTempField + "\",";
                            sFinalReportField += "IFNULL(truncate(" + sReportTable + ".\"" + sTempField + "\",2),'0')" + " as \"" + sTempField + "\",";
                            sColumnTotalFields += sTempField + "@";
                            sSumFields[i] = "IFNULL(truncate(SUM(" + sReportTable + ".\"" + sTempField + "\"),2),'0') as \"" + sTempField + "\"";
                            sGroupByFields[i] = "";
                            sNumericFields += "IFNULL(truncate(" + sReportTable + ".\"" + sTempField + "\",2),'0')" + " as \"" + sTempField + "\",";
                        }
                        else if (int.Parse(rsComp.Rows[i]["Type"].ToString()) <= 1 & int.Parse(rsComp.Rows[i]["CompRound"].ToString()) > 0)
                        {
                            sFieldType += sTempField.ToUpper() + "@" + typeof(double).ToString() + "#";

                            sCreateSql += ",`" + sTempField + "`  " + "varchar(200)";
                            sReportField += sReportTable + ".\"" + sTempField + "\",";
                            sFinalReportField += "IFNULL(truncate(" + sReportTable + ".\"" + sTempField + "\",2),'0.00')" + " as \"" + sTempField + "\",";
                            sColumnTotalFields += sTempField + "@";
                            sSumFields[i] = "IFNULL(truncate(SUM(" + sReportTable + ".\"" + sTempField + "\"),2),'0.00') as \"" + sTempField + "\"";
                            sGroupByFields[i] = "";
                            sNumericFields += "IFNULL(truncate(" + sReportTable + ".\"" + sTempField + "\",2),'0.00')" + " as \"" + sTempField + "\",";
                        }

                        else
                        {
                            sFieldType += sTempField.ToUpper() + "@" + typeof(string).ToString() + "#";

                            sCreateSql += ",`" + sTempField + "`  " + "varchar(200)";
                            sReportField += sReportTable + ".\"" + sTempField + "\",";
                            sFinalReportField += sReportTable + ".\"" + sTempField + "\",";
                            sSumFields[i] = "" + sReportTable + ".\"" + sTempField + "\"  as \"" + sTempField + "\"";
                            sGroupByFields[i] = sReportTable + ".\"" + sTempField + "\"";
                            sTextFields += sReportTable + ".\"" + sTempField + "\",";
                        }
                    }
                    sCheckField = sCheckField + "!" + rsComp.Rows[i]["component"].ToString() + "!";


                }
                sCreateSql = sCreateSql + ")";
                if (sReportField != null)
                {
                    sReportField = sReportField.Trim();
                    sFinalReportField = sFinalReportField.Trim();
                    sNumericFields = sNumericFields.Trim();
                    sTextFields = sTextFields.Trim();
                }
                if (sReportField != null)
                {
                    sReportField = sReportField.TrimEnd(',');
                    sFinalReportField = sFinalReportField.TrimEnd(',');
                    sNumericFields = sNumericFields.TrimEnd(',');
                    sTextFields = sTextFields.TrimEnd(',');
                }
                //dh.createDataSet(sCreateSql,"PrReport")

                using (DataManager dataManager = new DataManager())
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    resultArgs = dataManager.UpdateData(sCreateSql);
                }
                if (!resultArgs.Success)
                {
                    XtraMessageBox.Show("Unable to create report", "Payroll", System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Warning);
                    return false;
                }
                FillReport(date1, date2);
                rsComp.Dispose();
                return resultArgs.RowsAffected > 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }
        public object GetDMLSQL(string sRptId)
        {
            object sSql = "";
            sRptId = sRptId.ToUpper();
            switch (sRptId)
            {
                case "COMPONENT-01":
                    sSql = SQLCommand.Payroll.FetchComponent01;
                    break;
                case "DEPT-01":
                    sSql = SQLCommand.Payroll.FetchDept01;
                    break;
                case "GROUP-01":
                    sSql = SQLCommand.Payroll.FetchGroup01;
                    break;
                case "PAYROLL":
                    sSql = SQLCommand.Payroll.FetchPayroll;
                    break;
                default:
                    sSql = "";
                    break;
            }
            return sSql;
        }
        public DataTable FetchComponentForReport(object sql)
        {
            DataTable dtSource = null;
            using (DataManager dataManager = new DataManager(sql))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                    dtSource = resultArgs.DataSource.Table;
            }
            return dtSource;
        }
        /*		Purpose    : This method is used to Get the Query for the Selected Component
        *		Argument   : nCompId for the Selected Component
        *		ReturnType : As String For the Query
        */

        //public string GetReportQuery(int nReportId, long nCompId, long nDeptId)
        //{
        //    string sRptSQL = "";

        //    if (nReportId == 0 || nReportId == 6) //Pay Register
        //    {

        //        if (nDeptId == 0)
        //        {
        //            if (nDeptId == 0)
        //            {
        //                string sSelSQL = this.GetFieldName(nCompId, 0, false, true);
        //                if (sSelSQL != "" && nReportId == 0)
        //                {
        //                    string condition = string.Empty;
        //                    if (nCompId != 0)
        //                    {
        //                        condition = " where " + sReportTable + ".groupid = " + nCompId;
        //                    }
        //                    sRptSQL = "select @s:=@s+1  'S.No'," + sSelSQL + " from " + sReportTable + ",(SELECT @s:= 0) as s,prstaffgroup " + condition;

        //                }
        //                else if (sSelSQL != "" && nReportId == 6)
        //                {
        //                    sSelSQL = this.GetFieldNameForWages(nCompId, 0, false, true);
        //                    string sWhere = string.Empty;
        //                    if (nCompId != 0)
        //                    {
        //                        sWhere = sReportTable + ".groupid = " + nCompId + " and ";
        //                    }
        //                    sRptSQL = "select @s:=@s+1  'S.No'" +
        //                        ",concat(stfPersonal.FirstName , ' ' ,stfPersonal.LastName) as Name ,MAXWAGESBASIC as 'Min Wages Basic', MAXWAGESHRA as 'Min Wages DA',( MAXWAGESBASIC+ MAXWAGESHRA ) as 'Min Wages Total', "
        //                        + sSelSQL + ",' '  as Signature from " + sReportTable + ",(SELECT @s:= 0) as s,prstaffgroup,stfservice,stfpersonal  " +
        //                      " where " + sWhere + " stfservice.staffid = prstaffgroup.staffid and stfpersonal.staffid = stfservice.staffid and stfpersonal.staffid = prreporttable.staffid ";
        //                }
        //                else
        //                {
        //                    string sWhere = string.Empty;
        //                    if (nCompId != 0)
        //                    {
        //                        sWhere = " where " + sReportTable + ".groupid = " + nCompId;
        //                    }
        //                    if (sNumericFields != "")
        //                    {
        //                        sRptSQL = "select @s:=@s+1  'S.No'," + sTextFields + "," + sNumericFields + " from " + sReportTable + ",(SELECT @s:= 0) as s,prstaffgroup " + sWhere;
        //                        // " where " + sReportTable + ".groupid = " + nCompId;
        //                    }
        //                    else
        //                    {
        //                        sRptSQL = "select @s:=@s+1  'S.No'," + sTextFields + sNumericFields + " from " + sReportTable + ",(SELECT @s:= 0) as s,prstaffgroup " + sWhere;
        //                        //   " where " + sReportTable + ".groupid = " + nCompId;
        //                    }
        //                }
        //            }
        //        }
        //        else
        //        {
        //            if (sNumericFields != "")
        //            {
        //                sRptSQL = "select @s:=@s+1  'S.No'," + sTextFields + "," + sNumericFields + " from stfpersonal," + sReportTable +
        //                          ",(SELECT @s:= 0) as s,prstaffgroup where stfpersonal.staffid = " + sReportTable +
        //                          ".staffid AND stfpersonal.deptid = " + nDeptId;
        //            }
        //            else
        //            {
        //                if (sTextFields == "")
        //                    sTextFields = "*";
        //                sRptSQL = "select @s:=@s+1  'S.No'," + sTextFields + sNumericFields + " from stfpersonal," + sReportTable +
        //                          ",(SELECT @s:= 0) as s,prstaffgroup where stfpersonal.staffid = " + sReportTable +
        //                          ".staffid AND stfpersonal.deptid = " + nDeptId;
        //            }
        //        }
        //        if (nReportId == 0 && nCompId == 0)
        //        {
        //            sRptSQL += " where " + sReportTable + ".staffid = prstaffgroup.staffid " +
        //                          " AND prstaffgroup.payrollid = " + clsGeneral.PAYROLL_ID +
        //                          " order by prstaffgroup.stafforder,prstaffgroup.groupid";
        //        }
        //        else if (nReportId == 6)
        //        {
        //            sRptSQL += " AND " + sReportTable + ".staffid = prstaffgroup.staffid " +
        //                        " AND prstaffgroup.payrollid = " + clsGeneral.PAYROLL_ID;
        //        }
        //        else
        //        {
        //            sRptSQL += " AND " + sReportTable + ".staffid = prstaffgroup.staffid " +
        //                        " AND prstaffgroup.payrollid = " + clsGeneral.PAYROLL_ID +
        //                        " order by prstaffgroup.stafforder,prstaffgroup.groupid";
        //        }
        //    }
        //    else if (nReportId == 1) //Abstract Payroll Departmentwise
        //    {
        //        string sWhere = string.Empty;
        //        if (nCompId != 0)
        //        {
        //            sWhere = " and " + sReportTable + ".groupid = " + nCompId;
        //        }

        //        string sSelSQL = this.GetFieldName(nCompId, 0, false, true);
        //        string sRptField = "";

        //        for (int j = 0; j < sSumFields1.Length; j++)
        //            sRptField += sSumFields1[j].ToString() + ",";
        //        sRptField = sRptField.TrimEnd(',');

        //        string sGroupFields = " ";
        //        for (int j = 0; j < sGroupByFields1.Length; j++)
        //            if (sGroupByFields1[j].ToString() != "")
        //                sGroupFields += sGroupByFields1[j].ToString() + ",";
        //        sGroupFields = sGroupFields.TrimEnd(',');

        //        if (sRptField == "") //FetchReportFieldsWithoutGroup
        //        {
        //            sRptSQL = "select " +
        //                "hospital_departments.hdept_desc as \"Department\"" +
        //                " from stfpersonal," + sReportTable +//hospital_departments,
        //                " where " + sReportTable + ".staffid = stfpersonal.staffid " + sWhere;
        //            //   " and stfpersonal.deptid = hospital_departments.hdept_id ";
        //            //  " group by hospital_departments.hdept_desc";
        //        }
        //        else //FetchReportFieldsWithGroup
        //        {
        //            sRptSQL = "select @s:=@s+1 'S.No', " + sRptField +
        //                " from stfpersonal," + sReportTable + //hospital_departments,
        //                ",(SELECT @s:= 0) as s  where  "
        //                + sReportTable + ".staffid = stfpersonal.staffid" + sWhere +
        //                //" and stfpersonal.deptid = hospital_departments.hdept_id " +
        //                " group by " + sGroupFields + "ORDER BY stfpersonal.staffid";
        //        }
        //    }
        //    else if (nReportId == 4)
        //    {
        //        string sSelSQL = this.GetFieldName(nCompId, 0, false, true);
        //        string sRptField = "";

        //        for (int j = 0; j < sSumFields1.Length; j++)
        //            sRptField += sSumFields1[j].ToString() + ",";
        //        sRptField = sRptField.TrimEnd(',');

        //        string sGroupFields = " ";
        //        for (int j = 0; j < sGroupByFields1.Length; j++)
        //            if (sGroupByFields1[j].ToString() != "")
        //                sGroupFields += sGroupByFields1[j].ToString() + ",";
        //        sGroupFields = sGroupFields.TrimEnd(',');


        //        //sRptSQL = "select " +
        //        //    "hospital_departments.hdept_desc as \"Department\"" +
        //        //    " from stfpersonal," + sReportTable +//hospital_departments,
        //        //    " where " + sReportTable + ".groupid = " + nCompId +
        //        //    " and " + sReportTable + ".staffid = stfpersonal.staffid ";
        //        ////   " and stfpersonal.deptid = hospital_departments.hdept_id ";
        //        ////  " group by hospital_departments.hdept_desc";
        //        sRptSQL = "SELECT " +
        //                "PRT.NAME AS 'Name', " +
        //                "SUM(PRT.`BASIC PAY`+PRT.DA) AS 'Wages', " +
        //                "PRT.PF AS 'Employee /s Share 12%(PF)', " +
        //                "ROUND((PRT.PF*(3.67/12)),2) AS 'Employer /s Share 3.67%(EPF)', " +
        //                "ROUND((PRT.PF*(8.33/12)),2) AS 'Employer /s Share 8.33%(EPS)' " +
        //                "FROM STFPERSONAL ST, " + sReportTable + " PRT WHERE PRT.GROUPID = " + nCompId +
        //                " AND PRT.STAFFID = ST.STAFFID GROUP BY PRT.Name";
        //    }
        //    else if (nReportId == 2) //Abstract Component -  FetchAbstractComponent
        //    {
        //        sRptSQL = "select stfpersonal.empno as \"Id\"," + sStaffName +
        //                  " as \"Name\",' 'as \"Department\",truncate(prstaff.compvalue" +
        //                  " ,2)as \"Amount\" " +
        //                  " from prstaff,stfpersonal " +//hospital_departments
        //                  " where prstaff.payrollid = " + mvarPayRollId +
        //                  " and prstaff.componentid = " + nCompId +
        //                  " and prstaff.staffid = stfpersonal.staffid ";
        //        // " stfpersonal.deptid = hospital_departments.hdept_id ";
        //    }
        //    //Included By : Jules
        //    //Purpose     : Employee PF Report
        //    else if (nReportId == 5)
        //    {

        //        sRptSQL = "SELECT PFNUMBER AS \"PF ACCOUNT NO\",RT. NAME AS \"NAME\",\n" +
        //                    "       ROUND(SUM(RT. `BASIC PAY` + RT. DA), 2) AS 'WAGES',\n" +
        //                    "       ROUND(RT.PF, 2) AS \"EMPLOYEE'S SHARE 12%(PF)\",\n" +
        //                    "       ROUND((RT. PF) * (3.67 / 12), 2) AS \"EMPLOYER'S SHARE 3.67%(EPF)\",\n" +
        //                    "       ROUND((RT. PF) * (8.33 / 12), 2) AS \"EMPLOYER'S SHARE 8.33%(EPS)\"\n" +
        //                    "  FROM STFPERSONAL SP, PRREPORTTABLE RT, PRSTAFF PS,STFSERVICE SS\n" +
        //                    " WHERE RT.STAFFID = SP.STAFFID\n" +
        //                    "   AND PS.STAFFID = RT.STAFFID\n" +
        //                    "   AND SS.STAFFID = SP.STAFFID\n" +
        //                    "   AND PS.PAYROLLID =" + mvarPayRollId + "\n" +
        //                    " GROUP BY RT.NAME;";
        //    }
        //    if (sRptSQL.IndexOf("SUM") < 0)
        //    {
        //        sRptSQL = sRptSQL.Replace("group by", "order by");
        //    }
        //    return sRptSQL;
        //}


        /*		Purpose    : This method is used to Get the Query for the Selected Component
       *		Argument   : nCompId for the Selected Component
       *		ReturnType : As String For the Query
       */
        public string GetReportQuery(int nReportId, string nCompId, long nDeptId)
        {
            string sRptSQL = "";
            string isPayEnable = "";
            if (nReportId == 0 || nReportId == 6) //Pay Register
            {

                if (nDeptId == 0)
                {
                    if (nDeptId == 0)
                    {
                        string sSelSQL = this.GetFieldName(nCompId, 0, false, true);
                        if (sSelSQL != "" && nReportId == 0)
                        {
                            string condition = string.Empty;
                            if (nCompId != "0")
                            {
                                condition = " where " + sReportTable + ".groupid in( " + nCompId + ")";
                            }
                            isPayEnable = (SettingProperty.PayrollFinanceEnabled) ? ",Prproject_staff" : "";
                            sRptSQL = "select @s:=@s+1  'S.No'," + sSelSQL + " from " + sReportTable + isPayEnable + ",(SELECT @s:= 0) as s,prstaffgroup " + condition;

                        }
                        else if (sSelSQL != "" && nReportId == 6)
                        {
                            sSelSQL = this.GetFieldNameForWages(nCompId, 0, false, true);
                            string sWhere = string.Empty;
                            if (nCompId != "0")
                            {
                                sWhere = sReportTable + ".groupid IN ( " + nCompId + ") and ";
                            }
                            isPayEnable = (SettingProperty.PayrollFinanceEnabled) ? ",Prproject_staff" : "";
                            sRptSQL = "select @s:=@s+1  'S.No'" +
                                ", CONCAT(stfPersonal.firstname,CONCAT(' ', IFNULL(stfPersonal.MIDDLE_NAME,'')),CONCAT(' ',stfPersonal.lastname)) as Name ,"
                                + sSelSQL + ",' '  as Signature from " + sReportTable + isPayEnable + ",(SELECT @s:= 0) as s,prstaffgroup,stfservice,stfpersonal  " + // MAXWAGESBASIC as 'Min Wages Basic', MAXWAGESHRA as 'Min Wages DA',( MAXWAGESBASIC+ MAXWAGESHRA ) as 'Min Wages Total', 
                              " where " + sWhere + " stfservice.staffid = prstaffgroup.staffid and stfpersonal.staffid = stfservice.staffid and stfpersonal.staffid = prreporttable.staffid ";
                        }
                        else
                        {
                            string sWhere = string.Empty;
                            if (nCompId != "0")
                            {
                                sWhere = " where " + sReportTable + ".groupid in( " + nCompId + ")";
                            }
                            isPayEnable = (SettingProperty.PayrollFinanceEnabled) ? ",Prproject_staff" : "";
                            if (sNumericFields != "")
                            {
                                sRptSQL = "select @s:=@s+1  'S.No'," + sTextFields + "," + sNumericFields + " from " + sReportTable + isPayEnable + ",(SELECT @s:= 0) as s,prstaffgroup " + sWhere;
                                // " where " + sReportTable + ".groupid = " + nCompId;
                            }
                            else
                            {
                                sRptSQL = "select @s:=@s+1  'S.No'," + sTextFields + sNumericFields + " from " + sReportTable + isPayEnable + ",(SELECT @s:= 0) as s,prstaffgroup " + sWhere;
                                //   " where " + sReportTable + ".groupid = " + nCompId;
                            }
                        }
                    }
                }
                else
                {
                    if (sNumericFields != "")
                    {
                        isPayEnable = (SettingProperty.PayrollFinanceEnabled) ? ",Prproject_staff" : "";
                        sRptSQL = "select @s:=@s+1  'S.No'," + sTextFields + "," + sNumericFields + " from stfpersonal," + sReportTable + isPayEnable +
                                  ",(SELECT @s:= 0) as s,prstaffgroup where stfpersonal.staffid = " + sReportTable +
                                  ".staffid AND stfpersonal.deptid = " + nDeptId;
                    }
                    else
                    {
                        if (sTextFields == "")
                            sTextFields = "*";
                        isPayEnable = (SettingProperty.PayrollFinanceEnabled) ? ",Prproject_staff" : "";
                        sRptSQL = "select @s:=@s+1  'S.No'," + sTextFields + sNumericFields + " from stfpersonal," + sReportTable + isPayEnable +
                                  ",(SELECT @s:= 0) as s,prstaffgroup where stfpersonal.staffid = " + sReportTable +
                                  ".staffid AND stfpersonal.deptid = " + nDeptId;
                    }
                }
                if (nReportId == 0 && nCompId == "0")
                {
                    isPayEnable = (SettingProperty.PayrollFinanceEnabled) ? " AND PRReportTable.staffid = prproject_staff.staffid " : "";
                    sRptSQL += " where " + sReportTable + ".staffid = prstaffgroup.staffid " + isPayEnable +
                                  " AND prstaffgroup.payrollid = " + clsGeneral.PAYROLL_ID + "{ AND prproject_staff.project_id IN(?PROJECT_ID)}" +
                                  " order by prstaffgroup.stafforder,prstaffgroup.groupid";
                }
                else if (nReportId == 6)
                {
                    isPayEnable = (SettingProperty.PayrollFinanceEnabled) ? " AND PRReportTable.staffid = prproject_staff.staffid " : "";
                    sRptSQL += " AND " + sReportTable + ".staffid = prstaffgroup.staffid" + isPayEnable +
                                " AND prstaffgroup.payrollid = " + clsGeneral.PAYROLL_ID + "{ AND prproject_staff.project_id IN(?PROJECT_ID)}";
                }
                else
                {
                    isPayEnable = (SettingProperty.PayrollFinanceEnabled) ? " AND PRReportTable.staffid = prproject_staff.staffid " : "";
                    sRptSQL += " AND " + sReportTable + ".staffid = prstaffgroup.staffid " + isPayEnable +
                                " AND prstaffgroup.payrollid = " + clsGeneral.PAYROLL_ID + " {AND prproject_staff.project_id IN(?PROJECT_ID)}" +
                                " order by prstaffgroup.stafforder,prstaffgroup.groupid";
                }
            }
            else if (nReportId == 1) //Abstract Payroll Departmentwise
            {
                string sWhere = string.Empty;
                if (nCompId != "0")
                {
                    sWhere = " and " + sReportTable + ".groupid in( " + nCompId + ")";
                }

                string sSelSQL = this.GetFieldName(nCompId, 0, false, true);
                string sRptField = "";

                for (int j = 0; j < sSumFields1.Length; j++)
                    sRptField += sSumFields1[j].ToString() + ",";
                sRptField = sRptField.TrimEnd(',');

                string sGroupFields = " ";
                for (int j = 0; j < sGroupByFields1.Length; j++)
                    if (sGroupByFields1[j].ToString() != "")
                        sGroupFields += sGroupByFields1[j].ToString() + ",";
                sGroupFields = sGroupFields.TrimEnd(',');

                if (sRptField == "") //FetchReportFieldsWithoutGroup
                {
                    sRptSQL = "select " +
                        "hospital_departments.hdept_desc as \"Department\"" +
                        " from stfpersonal," + sReportTable +//hospital_departments,
                        " where " + sReportTable + ".staffid = stfpersonal.staffid " + sWhere;
                    //   " and stfpersonal.deptid = hospital_departments.hdept_id ";
                    //  " group by hospital_departments.hdept_desc";
                }
                else //FetchReportFieldsWithGroup
                {
                    sRptSQL = "select @s:=@s+1 'S.No', " + sRptField +
                        " from stfpersonal," + sReportTable + //hospital_departments,
                        ",(SELECT @s:= 0) as s  where  "
                        + sReportTable + ".staffid = stfpersonal.staffid" + sWhere +
                        //" and stfpersonal.deptid = hospital_departments.hdept_id " +
                        " group by " + sGroupFields + "ORDER BY stfpersonal.staffid";
                }
            }
            else if (nReportId == 4)
            {
                string sSelSQL = this.GetFieldName(nCompId, 0, false, true);
                string sRptField = "";

                for (int j = 0; j < sSumFields1.Length; j++)
                    sRptField += sSumFields1[j].ToString() + ",";
                sRptField = sRptField.TrimEnd(',');

                string sGroupFields = " ";
                for (int j = 0; j < sGroupByFields1.Length; j++)
                    if (sGroupByFields1[j].ToString() != "")
                        sGroupFields += sGroupByFields1[j].ToString() + ",";
                sGroupFields = sGroupFields.TrimEnd(',');


                //sRptSQL = "select " +
                //    "hospital_departments.hdept_desc as \"Department\"" +
                //    " from stfpersonal," + sReportTable +//hospital_departments,
                //    " where " + sReportTable + ".groupid = " + nCompId +
                //    " and " + sReportTable + ".staffid = stfpersonal.staffid ";
                ////   " and stfpersonal.deptid = hospital_departments.hdept_id ";
                ////  " group by hospital_departments.hdept_desc";
                sRptSQL = "SELECT " +
                        "PRT.NAME AS 'Name', " +
                        "SUM(PRT.`BASIC PAY`+PRT.DA) AS 'Wages', " +
                        "PRT.PF AS 'Employee /s Share 12%(PF)', " +
                        "ROUND((PRT.PF*(3.67/12)),2) AS 'Employer /s Share 3.67%(EPF)', " +
                        "ROUND((PRT.PF*(8.33/12)),2) AS 'Employer /s Share 8.33%(EPS)' " +
                        "FROM STFPERSONAL ST, " + sReportTable + " PRT WHERE PRT.GROUPID = " + nCompId +
                        " AND PRT.STAFFID = ST.STAFFID GROUP BY PRT.Name";
            }
            else if (nReportId == 2) //Abstract Component -  FetchAbstractComponent
            {
                sRptSQL = "select stfpersonal.empno as \"Id\"," + sStaffName +
                          " as \"Name\",' 'as \"Department\",truncate(prstaff.compvalue" +
                          " ,2)as \"Amount\" " +
                          " from prstaff,stfpersonal " +//hospital_departments
                          " where prstaff.payrollid = " + mvarPayRollId +
                          " and prstaff.componentid = " + nCompId +
                          " and prstaff.staffid = stfpersonal.staffid ";
                // " stfpersonal.deptid = hospital_departments.hdept_id ";
            }
            //Included By : Jules
            //Purpose     : Employee PF Report
            else if (nReportId == 5)
            {

                sRptSQL = "SELECT UAN AS \"UAN\",RT. NAME AS \"NAME\",\n" +
                            "       ROUND(SUM(RT. `BASIC PAY` + RT. DA), 2) AS 'WAGES',\n" +
                            "       ROUND(RT.PF, 2) AS \"EMPLOYEE'S SHARE 12%(PF)\",\n" +
                            "       ROUND((RT. PF) * (3.67 / 12), 2) AS \"EMPLOYER'S SHARE 3.67%(EPF)\",\n" +
                            "       ROUND((RT. PF) * (8.33 / 12), 2) AS \"EMPLOYER'S SHARE 8.33%(EPS)\"\n" +
                            "  FROM STFPERSONAL SP, PRREPORTTABLE RT, PRSTAFF PS,STFSERVICE SS\n" +
                            " WHERE RT.STAFFID = SP.STAFFID\n" +
                            "   AND PS.STAFFID = RT.STAFFID\n" +
                            "   AND SS.STAFFID = SP.STAFFID\n" +
                            "   AND PS.PAYROLLID =" + mvarPayRollId + "\n" +
                            " GROUP BY RT.NAME;";
            }
            if (sRptSQL.IndexOf("SUM") < 0)
            {
                sRptSQL = sRptSQL.Replace("group by", "order by");
            }
            return sRptSQL;
        }


        /* Purpose     : To drop the report Table
		 * Return Type : Bool - whether successfully deleted the ReportTable
		 * */
        public bool DropReportTable()
        {
            try
            {
                if (sReportTable != "")
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Payroll.DropTable))
                    {

                        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                        dataManager.Parameters.Add(dtCompMonth.TABLENAMEColumn, sReportTable);
                        dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                        resultArgs = dataManager.UpdateData();
                    }
                    return resultArgs.Success;
                }
            }
            catch
            {
                return false;
            }
            return false;
        }

        /* Purpose  : This method is used to Fill the Values for the Selected Components */

        private bool FillReport(string date1, string date2)
        {
            string sStfSQL;
            bool bUpdate = false;
            try
            {
                //using (DataManager dataManager = new DataManager(SQLCommand.Payroll.DeleteTable))
                //{
                //    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                //    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                //    dataManager.Parameters.Add(dtCompMonth.TABLENAMEColumn, sReportTable);
                //    resultArgs = dataManager.UpdateData();
                //}
                //return resultArgs.Success;

                //sStfSQL = "SELECT DISTINCT PRStaffGroup.StaffId,PRStaffGroup.GroupId," +
                //    "PRStaff.ComponentId,PRStaff.CompValue," +
                //    "PRComponent.Component FROM PRStaffGroup,PRStaff,PRComponent" +
                //    " WHERE PRComponent.ComponentId = PRStaff.ComponentId" +
                //    " AND PRStaff.PayRollId = " + clsGeneral.PAYROLL_ID +
                //    " AND PRStaffGroup.Payrollid = PRStaff.Payrollid" +
                //    " AND PRStaff.StaffId = PRStaffGroup.StaffId" +
                //    " ORDER BY PRStaffGroup.StaffId";
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchStaffByGroupByDate, "PRStaff"))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                    dataManager.Parameters.Add(dtCompMonth.TRANSACTIONDATE1Column, clsGeneral.GetMySQLDateTime(date1, DateDataType.DateNoFormatBegin));
                    dataManager.Parameters.Add(dtCompMonth.TRANSACTIONDATE2Column, clsGeneral.GetMySQLDateTime(date2, DateDataType.DateNoFormatEnd));
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
                //dh.createDataSet(sStfSQL, "PRStaff");
                if (!resultArgs.Success)
                    return resultArgs.Success;
                DataTable dtPRStaff = resultArgs.DataSource.Table;

                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchFromTable, "PRReport"))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    dataManager.Parameters.Add(dtCompMonth.TABLENAMEColumn, sReportTable);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
                if (!resultArgs.Success)
                    return resultArgs.Success;
                //    dh.createDataSet("SELECT * FROM " + sReportTable, "PRReport");
                DataTable dtPRComp = resultArgs.DataSource.Table;

                // Read grid data and seperate them according to record vice using seperators
                string sPrevStaffId = "";
                string sCurStaffId = "";
                string sFinalValues = "";
                string sSeperator = "";
                int iRecordCount = 0;
                foreach (DataRow drStaff in dtPRStaff.Rows)
                {

                    bUpdate = true;
                    if (iRecordCount++ == 0)
                    {
                        sPrevStaffId = drStaff["StaffId"].ToString();
                    }
                    else
                    {
                        sPrevStaffId = sCurStaffId;
                    }
                    sCurStaffId = drStaff["StaffId"].ToString();
                    if (sPrevStaffId == sCurStaffId)
                        sFinalValues = sFinalValues + "#"; //for the same record
                    else
                        sFinalValues = sFinalValues + "$"; // for next type of record

                    sFinalValues = sFinalValues +
                        drStaff["StaffId"].ToString() + '@' +
                        drStaff["GroupId"].ToString() + '@' +
                        drStaff["Component"].ToString().ToUpper() + '@' +
                        drStaff["CompValue"].ToString();
                    sFinalValues = sFinalValues + sSeperator;


                }
                //Sort the data string according to a record based on staffid
                string[] sRecords = sFinalValues.Split('$');
                for (int i = 0; i < sRecords.Length; i++)
                {
                    string[] sCurrentRecord = sRecords[i].Split('#');
                    iRecordCount = 0;
                    DataRow drComp = dtPRComp.NewRow();
                    for (int j = 0; j < sCurrentRecord.Length; j++)
                    {
                        if (sCurrentRecord[j].ToString() != "")
                        {
                            string[] sFields = sCurrentRecord[j].Split('@');

                            if (iRecordCount == 0)
                            {
                                string temp = sFields[0].ToString();
                                drComp["STAFFID"] = Convert.ToInt32(sFields[0].ToString());
                                drComp["GROUPID"] = Convert.ToInt32(sFields[1].ToString());
                                iRecordCount++;
                            }
                            try
                            {
                                //In accordance with the data type of the column merge data
                                int iFieldStartPosition = sFieldType.IndexOf("#" + sFields[2].ToString() + "@", 0); ;
                                int iFieldSepPosition = sFieldType.IndexOf("@", iFieldStartPosition + 1);
                                int iFieldTypeEndPosition = sFieldType.IndexOf("#", iFieldStartPosition + 1);
                                string sTempField = sFieldType.Substring(iFieldStartPosition + 1, sFields[2].ToString().Length);

                                if (sTempField.ToUpper() == sFields[2].ToString().ToUpper())
                                {
                                    string sTempFieldType = sFieldType.Substring(iFieldSepPosition + 1, (iFieldTypeEndPosition - iFieldSepPosition) - 1);
                                    if (sTempFieldType == typeof(int).ToString())
                                    {
                                        drComp[sFields[2].ToString()] = (Convert.ToInt32(sFields[3].ToString()) == 0) ? "0" : Convert.ToDouble(sFields[3].ToString()).ToString();
                                    }
                                    if (sTempFieldType == typeof(double).ToString())
                                    {
                                        drComp[sFields[2].ToString()] = (Convert.ToDouble(sFields[3].ToString()) == 0) ? "00" : Convert.ToDouble(sFields[3].ToString()).ToString();
                                    }
                                    else if (sTempFieldType == typeof(string).ToString())
                                    {
                                        drComp[sFields[2].ToString()] = (sFields[3].ToString() == "") ? "  " : sFields[3].ToString();
                                    }
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                    dtPRComp.Rows.Add(drComp);
                }
                //update individual records in the PRReportTable
                DataSet ds = new DataSet();
                ds.Tables.Add(dtPRComp);
                DataView dv = (DataView)ds.Tables[0].DefaultView;
                dvReportTableView = dv;
                if (bUpdate)
                {
                    for (int i = 0; i < dv.Table.Rows.Count; i++)
                    {
                        string sSql = "Insert into " + sReportTable;
                        string sValue = "(";
                        string sField = "(";
                        for (int j = 0; j < dv.Table.Columns.Count; j++)
                        {
                            sField = sField + "`" + dv.Table.Columns[j].ColumnName.ToString() + "`,";
                            sValue = sValue + "'" + dv.Table.Rows[i][j].ToString().Replace("'", "`") + "'" + ",";
                        }
                        sField = sField.TrimEnd(',');
                        sField = sField + ")";
                        sValue = sValue.TrimEnd(',');
                        sValue = sValue + ")";

                        sSql = sSql + "  " + sField + "  values" + sValue;
                        using (DataManager dataManager = new DataManager())
                        {
                            dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                            resultArgs = dataManager.UpdateData(sSql);
                        }
                        if (!resultArgs.Success)
                        {
                            //XtraMessageBox.Show("Unable to insert records in report.Because Components are duplicated", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            MessageRender.ShowMessage("Unable to generate the report " + resultArgs.Message);
                        }
                    }
                }
                return bUpdate;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
                return false;
            }
        }
        /* Purpose  : This method is used to Fill the Values for the Selected Components */

        private bool FillReport()
        {
            bool bUpdate = false;
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchStaffByGroup, "PRStaff"))
                {
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }

                if (resultArgs.Success)
                {
                    DataTable dtPRStaff = resultArgs.DataSource.Table;
                    using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchFromTable, "PRReport"))
                    {
                        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                        dataManager.Parameters.Add(dtCompMonth.TABLENAMEColumn, sReportTable);
                        dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                        resultArgs = dataManager.FetchData(DataSource.DataTable);
                    }
                    if (!resultArgs.Success)
                        return resultArgs.Success;
                    //    dh.createDataSet("SELECT * FROM " + sReportTable, "PRReport");
                    DataTable dtPRComp = resultArgs.DataSource.Table;

                    // Read grid data and seperate them according to record vice using seperators
                    string sPrevStaffId = "";
                    string sCurStaffId = "";
                    string sFinalValues = "";
                    string sSeperator = "";
                    int iRecordCount = 0;
                    foreach (DataRow drStaff in dtPRStaff.Rows)
                    {
                        bUpdate = true;
                        if (iRecordCount++ == 0)
                        {
                            sPrevStaffId = drStaff["StaffId"].ToString();
                        }
                        else
                        {
                            sPrevStaffId = sCurStaffId;
                        }
                        sCurStaffId = drStaff["StaffId"].ToString();
                        if (sPrevStaffId == sCurStaffId)
                            sFinalValues = sFinalValues + "#"; //for the same record
                        else
                            sFinalValues = sFinalValues + "$"; // for next type of record

                        sFinalValues = sFinalValues +
                            drStaff["StaffId"].ToString() + '@' +
                            drStaff["GroupId"].ToString() + '@' +
                            drStaff["Component"].ToString().ToUpper() + '@' +
                            drStaff["CompValue"].ToString();
                        sFinalValues = sFinalValues + sSeperator;
                    }

                    //Sort the data string according to a record based on staffid
                    string[] sRecords = sFinalValues.Split('$');
                    for (int i = 0; i < sRecords.Length; i++)
                    {
                        string[] sCurrentRecord = sRecords[i].Split('#');
                        iRecordCount = 0;
                        DataRow drComp = dtPRComp.NewRow();
                        for (int j = 0; j < sCurrentRecord.Length; j++)
                        {
                            if (sCurrentRecord[j].ToString() != "")
                            {
                                string[] sFields = sCurrentRecord[j].Split('@');

                                if (iRecordCount == 0)
                                {
                                    string temp = sFields[0].ToString();
                                    drComp["STAFFID"] = Convert.ToInt32(sFields[0].ToString());
                                    drComp["GROUPID"] = Convert.ToInt32(sFields[1].ToString());
                                    iRecordCount++;
                                }
                                try
                                {
                                    //In accordance with the data type of the column merge data
                                    int iFieldStartPosition = sFieldType.IndexOf("#" + sFields[2].ToString() + "@", 0); ;
                                    int iFieldSepPosition = sFieldType.IndexOf("@", iFieldStartPosition + 1);
                                    int iFieldTypeEndPosition = sFieldType.IndexOf("#", iFieldStartPosition + 1);
                                    string sTempField = sFieldType.Substring(iFieldStartPosition + 1, sFields[2].ToString().Length);

                                    if (sTempField.ToUpper() == sFields[2].ToString().ToUpper())
                                    {
                                        string sTempFieldType = sFieldType.Substring(iFieldSepPosition + 1, (iFieldTypeEndPosition - iFieldSepPosition) - 1);
                                        if (sTempFieldType == typeof(int).ToString())
                                        {
                                            //drComp[sFields[2].ToString()] = (Convert.ToInt32(sFields[3].ToString()) == 0) ? "0" : Convert.ToDouble(sFields[3].ToString()).ToString();
                                            drComp[sFields[2].ToString()] = (Convert.ToDouble(sFields[3].ToString()) == 0) ? "0" : Convert.ToDouble(sFields[3].ToString()).ToString();
                                        }
                                        if (sTempFieldType == typeof(double).ToString())
                                        {
                                            drComp[sFields[2].ToString()] = (Convert.ToDouble(sFields[3].ToString()) == 0) ? "00" : Convert.ToDouble(sFields[3].ToString()).ToString();
                                        }
                                        else if (sTempFieldType == typeof(string).ToString())
                                        {
                                            drComp[sFields[2].ToString()] = (sFields[3].ToString() == "") ? "  " : sFields[3].ToString();
                                        }
                                    }
                                }
                                catch
                                {
                                }
                            }
                        }
                        dtPRComp.Rows.Add(drComp);
                    }
                }
            }
            catch (Exception ex)
            {
                bUpdate = false;
                System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
            }
            return bUpdate;
        }

        /*	Purpose   : To get the field names which are apt for the selected report
		*	Arguments :	Group id, department id, 
		*	returns   : The fields as a string
		*/
        public string GetFieldName(long nGId, long nDId, bool bType, bool bGetEmpty)
        {
            string sSql = "", sWhereSql = "";
            DataTable rsGet = new DataTable();
            string sGetFieldName = "";


            if (nDId > 0) return "";

            if (bType)
                sWhereSql = " AND (prcompmonth.Type = 0 OR prcompmonth.Type = 1) ";
            else
                sWhereSql = " ";

            //sSql = "select " +
            //    "prcomponent.component,prcomponent.Type,prcomponent.CompRound " +
            //    "from prcomponent,prcompmonth " +
            //    "where " +
            //    "prcompmonth.payrollid = " + mvarPayRollId + " AND " +
            //    "prcompmonth.componentid=prcomponent.componentid AND " +
            //    "prcompmonth.salarygroupid= " + nGId + sWhereSql +
            //    " and prcompmonth.comp_order > 0 " +
            //    "order by prcompmonth.comp_order";
            object sqlQuery;
            if (nGId == 0)
            {
                sqlQuery = SQLCommand.Payroll.FetchComponentByCompOrderWithoutGroup;
            }
            else
            {
                sqlQuery = SQLCommand.Payroll.FetchComponentByCompOrder;
            }
            using (DataManager dataManager = new DataManager(sqlQuery, "Component"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                dataManager.Parameters.Add(dtCompMonth.SALARYGROUPIDColumn, nGId);
                dataManager.Parameters.Add(dtCompMonth.CONDITIONColumn, sWhereSql);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            if (resultArgs.Success)
                rsGet = resultArgs.DataSource.Table;

            //dh.createDataSet(sSql, "Component");
            //rsGet = dh.getDataSet();

            //			for(int i=0;i <= rsGet.Tables[0].Rows.Count-1;i++)
            //			{
            //				sGetFieldName = sGetFieldName + sReportTable + ".\""+ rsGet.Tables[0].Rows[i][0].ToString() +"\",";
            //			}

            sSumFields1 = new string[rsGet.Rows.Count];
            sGroupByFields1 = new string[rsGet.Rows.Count];
            string sCheckDuplicate = "";
            for (int i = 0; i < rsGet.Rows.Count; i++)
            {

                if (sCheckDuplicate.IndexOf("!" + rsGet.Rows[i]["Component"].ToString() + "!", 0) == -1)
                {
                    string sTempField = rsGet.Rows[i]["Component"].ToString();

                    if (int.Parse(rsGet.Rows[i]["Type"].ToString()) <= 1 & int.Parse(rsGet.Rows[i]["CompRound"].ToString()) == 0)
                    {
                        sGetFieldName += "IFNULL(TRUNCATE(" + sReportTable + ".`" + sTempField + "`,2),'0')" + " as \"" + sTempField + "\",";
                        sSumFields1[i] = "IFNULL(TRUNCATE(SUM(" + sReportTable + ".`" + sTempField + "`),2),'0') as \"" + sTempField + "\"";
                        sGroupByFields1[i] = "";
                    }
                    else if (int.Parse(rsGet.Rows[i]["Type"].ToString()) <= 1 & int.Parse(rsGet.Rows[i]["CompRound"].ToString()) > 0)
                    {
                        sGetFieldName += "IFNULL(TRUNCATE(" + sReportTable + ".`" + sTempField + "`,2),'0.00')" + " as \"" + sTempField + "\",";
                        sSumFields1[i] = "IFNULL(TRUNCATE(SUM(" + sReportTable + ".`" + sTempField + "`),2),'0.00') as \"" + sTempField + "\"";
                        sGroupByFields1[i] = "";
                    }

                    else
                    {
                        sGetFieldName += sReportTable + ".`" + sTempField + "`,";
                        sSumFields1[i] = "" + sReportTable + ".`" + sTempField + "` as \"" + sTempField + "\"";
                        sGroupByFields1[i] = sReportTable + ".`" + sTempField + "`";
                    }
                }
                sCheckDuplicate = sCheckDuplicate + "!" + rsGet.Rows[i]["component"].ToString() + "!";


            }
            //=========

            sGetFieldName = sGetFieldName.Trim();

            if (sGetFieldName != "")
                sGetFieldName = objPrLoan.RemoveTrailingSpace(sGetFieldName, 1);
            else if (bGetEmpty)
                sGetFieldName = sReportTable + ".staffid";

            return sGetFieldName;
        }

        public string GetFieldName(string nGId, long nDId, bool bType, bool bGetEmpty)
        {
            string sSql = "", sWhereSql = "";
            DataTable rsGet = new DataTable();
            string sGetFieldName = "";


            if (nDId > 0) return "";

            if (bType)
                sWhereSql = " AND (prcompmonth.Type = 0 OR prcompmonth.Type = 1) ";
            else
                sWhereSql = " ";

            //sSql = "select " +
            //    "prcomponent.component,prcomponent.Type,prcomponent.CompRound " +
            //    "from prcomponent,prcompmonth " +
            //    "where " +
            //    "prcompmonth.payrollid = " + mvarPayRollId + " AND " +
            //    "prcompmonth.componentid=prcomponent.componentid AND " +
            //    "prcompmonth.salarygroupid= " + nGId + sWhereSql +
            //    " and prcompmonth.comp_order > 0 " +
            //    "order by prcompmonth.comp_order";
            object sqlQuery;
            if (nGId == string.Empty)
            {
                sqlQuery = SQLCommand.Payroll.FetchComponentByCompOrderWithoutGroup;
            }
            else
            {
                sqlQuery = SQLCommand.Payroll.FetchComponentByCompOrder;
            }
            using (DataManager dataManager = new DataManager(sqlQuery, "Component"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                //dataManager.Parameters.Add(dtCompMonth.SALARYGROUPIDColumn, nGId); 
                dataManager.Parameters.Add(dtCompMonth.IDsColumn, nGId);
                dataManager.Parameters.Add(dtCompMonth.CONDITIONColumn, sWhereSql);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            if (resultArgs.Success)
                rsGet = resultArgs.DataSource.Table;

            //dh.createDataSet(sSql, "Component");
            //rsGet = dh.getDataSet();

            //			for(int i=0;i <= rsGet.Tables[0].Rows.Count-1;i++)
            //			{
            //				sGetFieldName = sGetFieldName + sReportTable + ".\""+ rsGet.Tables[0].Rows[i][0].ToString() +"\",";
            //			}

            sSumFields1 = new string[rsGet.Rows.Count];
            sGroupByFields1 = new string[rsGet.Rows.Count];
            string sCheckDuplicate = "";
            for (int i = 0; i < rsGet.Rows.Count; i++)
            {

                if (sCheckDuplicate.IndexOf("!" + rsGet.Rows[i]["Component"].ToString() + "!", 0) == -1)
                {
                    string sTempField = rsGet.Rows[i]["Component"].ToString();

                    if (int.Parse(rsGet.Rows[i]["Type"].ToString()) <= 1 & int.Parse(rsGet.Rows[i]["CompRound"].ToString()) == 0)
                    {
                        sGetFieldName += "IFNULL(TRUNCATE(" + sReportTable + ".`" + sTempField + "`,2),'0')" + " as \"" + sTempField + "\",";
                        sSumFields1[i] = "IFNULL(TRUNCATE(SUM(" + sReportTable + ".`" + sTempField + "`),2),'0') as \"" + sTempField + "\"";
                        sGroupByFields1[i] = "";
                    }
                    else if (int.Parse(rsGet.Rows[i]["Type"].ToString()) <= 1 & int.Parse(rsGet.Rows[i]["CompRound"].ToString()) > 0)
                    {
                        sGetFieldName += "IFNULL(TRUNCATE(" + sReportTable + ".`" + sTempField + "`,2),'0.00')" + " as \"" + sTempField + "\",";
                        sSumFields1[i] = "IFNULL(TRUNCATE(SUM(" + sReportTable + ".`" + sTempField + "`),2),'0.00') as \"" + sTempField + "\"";
                        sGroupByFields1[i] = "";
                    }

                    else
                    {
                        sGetFieldName += sReportTable + ".`" + sTempField + "`,";
                        sSumFields1[i] = "" + sReportTable + ".`" + sTempField + "` as \"" + sTempField + "\"";
                        sGroupByFields1[i] = sReportTable + ".`" + sTempField + "`";
                    }
                }
                sCheckDuplicate = sCheckDuplicate + "!" + rsGet.Rows[i]["component"].ToString() + "!";


            }
            //=========

            sGetFieldName = sGetFieldName.Trim();

            if (sGetFieldName != "")
                sGetFieldName = objPrLoan.RemoveTrailingSpace(sGetFieldName, 1);
            else if (bGetEmpty)
                sGetFieldName = sReportTable + ".staffid";

            return sGetFieldName;
        }


        /*	Purpose   : To get the field names which are apt for the selected report (Type 2 is not taken to report)
       *	Arguments :	Group id, department id, 
       *	returns   : The fields as a string
       */
        public string GetFieldNameForWages(long nGId, long nDId, bool bType, bool bGetEmpty)
        {
            string sSql = "", sWhereSql = "";
            DataTable rsGet = new DataTable();
            string sGetFieldName = "";


            if (nDId > 0) return "";

            if (bType)
                sWhereSql = " AND (prcompmonth.Type = 0 OR prcompmonth.Type = 1) ";
            else
                sWhereSql = " ";

            //sSql = "select " +
            //    "prcomponent.component,prcomponent.Type,prcomponent.CompRound " +
            //    "from prcomponent,prcompmonth " +
            //    "where " +
            //    "prcompmonth.payrollid = " + mvarPayRollId + " AND " +
            //    "prcompmonth.componentid=prcomponent.componentid AND " +
            //    "prcompmonth.salarygroupid= " + nGId + sWhereSql +
            //    " and prcompmonth.comp_order > 0 " +
            //    "order by prcompmonth.comp_order";
            object sqlQuery;
            if (nGId == 0)
                sqlQuery = SQLCommand.Payroll.FetchComponentByCompOrderForWagesWithoutGroup;
            else
                sqlQuery = SQLCommand.Payroll.FetchComponentByCompOrderForWages;

            using (DataManager dataManager = new DataManager(sqlQuery, "Component"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                dataManager.Parameters.Add(dtCompMonth.SALARYGROUPIDColumn, nGId);
                dataManager.Parameters.Add(dtCompMonth.CONDITIONColumn, sWhereSql);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            if (resultArgs.Success)
                rsGet = resultArgs.DataSource.Table;

            //dh.createDataSet(sSql, "Component");
            //rsGet = dh.getDataSet();

            //			for(int i=0;i <= rsGet.Tables[0].Rows.Count-1;i++)
            //			{
            //				sGetFieldName = sGetFieldName + sReportTable + ".\""+ rsGet.Tables[0].Rows[i][0].ToString() +"\",";
            //			}

            sSumFields1 = new string[rsGet.Rows.Count];
            sGroupByFields1 = new string[rsGet.Rows.Count];
            string sCheckDuplicate = "";
            for (int i = 0; i < rsGet.Rows.Count; i++)
            {

                if (sCheckDuplicate.IndexOf("!" + rsGet.Rows[i]["Component"].ToString() + "!", 0) == -1)
                {
                    string sTempField = rsGet.Rows[i]["Component"].ToString();

                    if (int.Parse(rsGet.Rows[i]["Type"].ToString()) <= 1 & int.Parse(rsGet.Rows[i]["CompRound"].ToString()) == 0)
                    {
                        sGetFieldName += "IFNULL(TRUNCATE(" + sReportTable + ".`" + sTempField + "`,2),'0')" + " as \"" + sTempField + "\",";
                        sSumFields1[i] = "IFNULL(TRUNCATE(SUM(" + sReportTable + ".`" + sTempField + "`),2),'0') as \"" + sTempField + "\"";
                        sGroupByFields1[i] = "";
                    }
                    else if (int.Parse(rsGet.Rows[i]["Type"].ToString()) <= 1 & int.Parse(rsGet.Rows[i]["CompRound"].ToString()) > 0)
                    {
                        sGetFieldName += "IFNULL(TRUNCATE(" + sReportTable + ".`" + sTempField + "`,2),'0.00')" + " as \"" + sTempField + "\",";
                        sSumFields1[i] = "IFNULL(TRUNCATE(SUM(" + sReportTable + ".`" + sTempField + "`),2),'0.00') as \"" + sTempField + "\"";
                        sGroupByFields1[i] = "";
                    }

                    else
                    {
                        sGetFieldName += sReportTable + ".`" + sTempField + "`,";
                        sSumFields1[i] = "" + sReportTable + ".`" + sTempField + "` as \"" + sTempField + "\"";
                        sGroupByFields1[i] = sReportTable + ".`" + sTempField + "`";
                    }
                }
                sCheckDuplicate = sCheckDuplicate + "!" + rsGet.Rows[i]["component"].ToString() + "!";


            }
            //=========

            sGetFieldName = sGetFieldName.Trim();

            if (sGetFieldName != "")
                sGetFieldName = objPrLoan.RemoveTrailingSpace(sGetFieldName, 1);
            else if (bGetEmpty)
                sGetFieldName = sReportTable + ".staffid";

            return sGetFieldName;
        }

        public string GetFieldNameForWages(string nGId, long nDId, bool bType, bool bGetEmpty)
        {
            string sSql = "", sWhereSql = "";
            DataTable rsGet = new DataTable();
            string sGetFieldName = "";


            if (nDId > 0) return "";

            if (bType)
                sWhereSql = " AND (prcompmonth.Type = 0 OR prcompmonth.Type = 1) ";
            else
                sWhereSql = " ";

            //sSql = "select " +
            //    "prcomponent.component,prcomponent.Type,prcomponent.CompRound " +
            //    "from prcomponent,prcompmonth " +
            //    "where " +
            //    "prcompmonth.payrollid = " + mvarPayRollId + " AND " +
            //    "prcompmonth.componentid=prcomponent.componentid AND " +
            //    "prcompmonth.salarygroupid= " + nGId + sWhereSql +
            //    " and prcompmonth.comp_order > 0 " +
            //    "order by prcompmonth.comp_order";
            object sqlQuery;
            if (nGId == string.Empty)
                sqlQuery = SQLCommand.Payroll.FetchComponentByCompOrderForWagesWithoutGroup;
            else
                sqlQuery = SQLCommand.Payroll.FetchComponentByCompOrderForWages;

            using (DataManager dataManager = new DataManager(sqlQuery, "Component"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                dataManager.Parameters.Add(dtCompMonth.IDsColumn, nGId);
                dataManager.Parameters.Add(dtCompMonth.CONDITIONColumn, sWhereSql);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            if (resultArgs.Success)
                rsGet = resultArgs.DataSource.Table;

            //dh.createDataSet(sSql, "Component");
            //rsGet = dh.getDataSet();

            //			for(int i=0;i <= rsGet.Tables[0].Rows.Count-1;i++)
            //			{
            //				sGetFieldName = sGetFieldName + sReportTable + ".\""+ rsGet.Tables[0].Rows[i][0].ToString() +"\",";
            //			}

            sSumFields1 = new string[rsGet.Rows.Count];
            sGroupByFields1 = new string[rsGet.Rows.Count];
            string sCheckDuplicate = "";
            for (int i = 0; i < rsGet.Rows.Count; i++)
            {

                if (sCheckDuplicate.IndexOf("!" + rsGet.Rows[i]["Component"].ToString() + "!", 0) == -1)
                {
                    string sTempField = rsGet.Rows[i]["Component"].ToString();

                    if (int.Parse(rsGet.Rows[i]["Type"].ToString()) <= 1 & int.Parse(rsGet.Rows[i]["CompRound"].ToString()) == 0)
                    {
                        sGetFieldName += "IFNULL(TRUNCATE(" + sReportTable + ".`" + sTempField + "`,2),'0')" + " as \"" + sTempField + "\",";
                        sSumFields1[i] = "IFNULL(TRUNCATE(SUM(" + sReportTable + ".`" + sTempField + "`),2),'0') as \"" + sTempField + "\"";
                        sGroupByFields1[i] = "";
                    }
                    else if (int.Parse(rsGet.Rows[i]["Type"].ToString()) <= 1 & int.Parse(rsGet.Rows[i]["CompRound"].ToString()) > 0)
                    {
                        sGetFieldName += "IFNULL(TRUNCATE(" + sReportTable + ".`" + sTempField + "`,2),'0.00')" + " as \"" + sTempField + "\",";
                        sSumFields1[i] = "IFNULL(TRUNCATE(SUM(" + sReportTable + ".`" + sTempField + "`),2),'0.00') as \"" + sTempField + "\"";
                        sGroupByFields1[i] = "";
                    }

                    else
                    {
                        sGetFieldName += sReportTable + ".`" + sTempField + "`,";
                        sSumFields1[i] = "" + sReportTable + ".`" + sTempField + "` as \"" + sTempField + "\"";
                        sGroupByFields1[i] = sReportTable + ".`" + sTempField + "`";
                    }
                }
                sCheckDuplicate = sCheckDuplicate + "!" + rsGet.Rows[i]["component"].ToString() + "!";


            }
            //=========

            sGetFieldName = sGetFieldName.Trim();

            if (sGetFieldName != "")
                sGetFieldName = objPrLoan.RemoveTrailingSpace(sGetFieldName, 1);
            else if (bGetEmpty)
                sGetFieldName = sReportTable + ".staffid";

            return sGetFieldName;
        }

        public string GetReportSQL(string sRptId, string sWhere, string sOrder)
        {
            string sSql = "";
            string sloanDate = "";
            sRptId = sRptId.ToUpper();

            switch (sRptId)
            {
                case "RPT-LLED01":
                    sSql = "select @s:=@s+1 'S.No'," +
                           "stfpersonal.empno as \"Id\"," + sStaffName + " as \"Name\"," +
                        // "hospital_departments.hdept_desc as \"Department\","
                            " prloan.loanname as \"Loan\", " +
                           "ifnull(TRUNCATE(prloanpaid.amount,2),'0.00') as \"Amount\" " + sloanDate +
                           "from prloan,prloanpaid,stfpersonal,(SELECT @s:= 0) as s,prloanget where " + //,hospital_departments
                           "prloan.loanid = prloanpaid.loanid and " +
                           "stfpersonal.staffid = prloanpaid.staffid and prloanget.prloangetid = " +
                           "prloanpaid.prloangetid and prloanpaid.amount > 0  " +
                        //  "stfpersonal.deptid = hospital_departments.hdept_id" +
                          (sWhere != "" ? " AND " + sWhere : "") +
                           " order by stfpersonal.staffid ," + (sOrder != "" ? sOrder : "stfpersonal.empno");
                    break;
                //case "RPT-LLED02":
                //    sSql = "select @s:=@s+1 'S.No'," +
                //            "stfpersonal.empno as \"Id\"," +
                //            sStaffName + " as \"Name\",prloan.loanname as \"Department\", " + //,hospital_departments.hdept_desc
                //            "ifnull(TRUNCATE(sum(prloanget.amount),2),'0.00') as \"Loan\", " +
                //            "ifnull(TRUNCATE(sum(prloanget.intrestamount),2),'0.00') as \"Interest\", " +
                //            "ifnull(TRUNCATE(Max((select sum(prloanpaid.amount) from prloanpaid " +
                //            "where prloanpaid.staffid = stfpersonal.staffid)),2),'0.00') as \"Paid\", " +
                //            "ifnull(truncate(sum(prloanget.amount) + sum(prloanget.intrestamount)- " +
                //            "Max((select ifnull(Sum(PRLoanPaid.Amount),0) " +
                //            " from prloanpaid where prloanpaid.staffid = stfpersonal.staffid)),2),'0.00') as \"Balance\" " +
                //            " from prloan,prloanget,stfpersonal ,prproject_staff,(SELECT @s:= 0) as s " + //hospital_departments,
                //            "where stfpersonal.staffid = prloanget.staffid  and prloan.loanid=prloanget.loanid " +
                //        //   "stfpersonal.deptid = hospital_departments.hdept_id" +
                //            (sWhere != "" ? " AND " : "") +
                //            " group by stfpersonal.empno," + sStaffName +
                //            ",stfpersonal.staffid order by  stfpersonal.staffid "; //hospital_departments.hdept_desc,

                //    break;

                case "RPT-LLED02":
                    //sSql = "select @s:=@s+1 'S.No'," +
                    //        "stfpersonal.empno as \"Id\"," +
                    //        sStaffName + " as \"Name\",prloan.loanname as \"Department\", " +
                    //        "ifnull(TRUNCATE(prloanget.amount,2),0.00) as \"Loan\", " +
                    //        "ifnull(TRUNCATE(prloanget.interest, 2), 0.00) as 'Interest_Rate'," +
                    //        "ifnull(TRUNCATE(prloanget.intrestamount,2),0.00) as \"Interest\", " +
                    //        "ifnull(TRUNCATE(Max((select sum(prloanpaid.amount) from prloanpaid " +
                    //        "where prloanpaid.staffid = stfpersonal.staffid)),2),0.00) as \"Paid\", " +
                    //        "ifnull(truncate(prloanget.amount + prloanget.intrestamount- " +
                    //        "Max((select ifnull(Sum(PRLoanPaid.Amount),0) " +
                    //        " from prloanpaid where prloanpaid.staffid = stfpersonal.staffid)),2),0.00) as \"Balance\" " +
                    //        " from prloan,prloanget,stfpersonal ,(SELECT @s:= 0) as s " +
                    //        "where stfpersonal.staffid = prloanget.staffid  and prloan.loanid=prloanget.loanid " +
                    //        (sWhere != "" ? " AND " : "") +
                    //        " group by stfpersonal.empno," + sStaffName +
                    //        ",stfpersonal.staffid order by  stfpersonal.staffid ";

                    sSql = "  SELECT @s:=@s+1 'S.No', sf.empno as 'Id', CONCAT(IFNULL(CONCAT(PNT.NAME_TITLE, ' '), ''), sf.firstname,CONCAT(' ', IFNULL(sf.MIDDLE_NAME,'')), CONCAT(' ',sf.lastname)) as 'Name', " +
                      " L.LOANNAME, LB.Interest_Rate, LB.Interest, LB.AMOUNT AS LOAN, LB.AMOUNT_PAID AS PAID, (LB.AMOUNT - LB.AMOUNT_PAID) AS BALANCE FROM stfpersonal AS sf " +
                       "INNER JOIN (select lg.STAFFID AS STAFF_ID, lg.LOANID, IFNULL(lg.amount, 0) AS AMOUNT, SUM(IFNULL(lp.amount, 0)) AS AMOUNT_PAID, " +
                       "IFNULL(lg.interest,  0.00) as Interest_Rate, IFNULL(lg.intrestamount,0.00) as Interest " +
                        " FROM prloanget lg " +
                       "LEFT JOIN prloanpaid lp ON lp.STAFFID = lg.STAFFID AND lp.PRLOANGETID = " +
                       "lg.PRLOANGETID AND lp.LOANID = lg.LOANID WHERE LG.COMPLETED=0 " +
                      "GROUP BY lp.STAFFID, lp.PRLOANGETID) AS LB ON LB.STAFF_ID = sf.STAFFID " +
                      "LEFT JOIN PR_NAME_TITLE PNT ON PNT.NAME_TITLE_ID = sf.NAME_TITLE_ID " +
                      " INNER JOIN PRLOAN L ON L.LOANID = LB.LOANID,(SELECT @s:= 0) as s ";

                    break;

            }
            return sSql;
        }
        public bool CreateReportTableNew()
        {
            string sSql = "";
            string sCreateSql = "";
            string sCheckField = "";
            DataTable rsComp = new DataTable();

            //sSql = "select " +
            //    " distinct prcomponent.component, prcomponent.Type," +
            //    " prcompmonth.comp_order,prcomponent.CompRound " +
            //    " from prcomponent,prcompmonth " +
            //    " where prcomponent.componentid=prcompmonth.componentid and " +
            //    " prcompmonth.payrollid = " + clsGeneral.PAYROLL_ID +
            //    " and prcompmonth.comp_order > 0 " +
            //    " order by prcompmonth.comp_order";
            try
            {

                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchComponentReport, "Component"))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
                if (resultArgs.Success)
                    rsComp = resultArgs.DataSource.Table;
                //dh.createDataSet(sSql, "Component");
                //rsComp = dh.getDataSet();
                DataTable dtPRComp = rsComp;
                sCreateSql = "create table " + sReportTable + "(staffid " + "INT(10)" +
                              ",groupid " + "INT(10),TRANSACTIONDATE " + " VARCHAR(200) ";
                sSumFields = new string[rsComp.Rows.Count];
                sGroupByFields = new string[rsComp.Rows.Count];

                for (int i = 0; i < rsComp.Rows.Count; i++)
                {

                    if (sCheckField.IndexOf("!" + rsComp.Rows[i]["Component"].ToString() + "!", 0) == -1)
                    {
                        string sTempField = rsComp.Rows[i]["Component"].ToString();

                        if (int.Parse(rsComp.Rows[i]["Type"].ToString()) <= 1 & int.Parse(rsComp.Rows[i]["CompRound"].ToString()) == 0)
                        {
                            sFieldType += sTempField.ToUpper() + "@" + typeof(double).ToString() + "#";

                            sCreateSql += ",`" + sTempField + "`  " + "varchar(200)";
                            sReportField += sReportTable + ".\"" + sTempField + "\",";
                            sFinalReportField += "IFNULL(truncate(" + sReportTable + ".\"" + sTempField + "\",2),'0')" + " as \"" + sTempField + "\",";
                            sColumnTotalFields += sTempField + "@";
                            sSumFields[i] = "IFNULL(truncate(SUM(" + sReportTable + ".\"" + sTempField + "\"),2),'0') as \"" + sTempField + "\"";
                            sGroupByFields[i] = "";
                            sNumericFields += "IFNULL(truncate(" + sReportTable + ".\"" + sTempField + "\",2),'0')" + " as \"" + sTempField + "\",";
                        }
                        else if (int.Parse(rsComp.Rows[i]["Type"].ToString()) <= 1 & int.Parse(rsComp.Rows[i]["CompRound"].ToString()) > 0)
                        {
                            sFieldType += sTempField.ToUpper() + "@" + typeof(double).ToString() + "#";

                            sCreateSql += ",`" + sTempField + "`  " + "varchar(200)";
                            sReportField += sReportTable + ".\"" + sTempField + "\",";
                            sFinalReportField += "IFNULL(truncate(" + sReportTable + ".\"" + sTempField + "\",2),'0.00')" + " as \"" + sTempField + "\",";
                            sColumnTotalFields += sTempField + "@";
                            sSumFields[i] = "IFNULL(truncate(SUM(" + sReportTable + ".\"" + sTempField + "\"),2),'0.00') as \"" + sTempField + "\"";
                            sGroupByFields[i] = "";
                            sNumericFields += "IFNULL(truncate(" + sReportTable + ".\"" + sTempField + "\",2),'0.00')" + " as \"" + sTempField + "\",";
                        }

                        else
                        {
                            sFieldType += sTempField.ToUpper() + "@" + typeof(string).ToString() + "#";

                            sCreateSql += ",`" + sTempField + "`  " + "varchar(200)";
                            sReportField += sReportTable + ".\"" + sTempField + "\",";
                            sFinalReportField += sReportTable + ".\"" + sTempField + "\",";
                            sSumFields[i] = "" + sReportTable + ".\"" + sTempField + "\"  as \"" + sTempField + "\"";
                            sGroupByFields[i] = sReportTable + ".\"" + sTempField + "\"";
                            sTextFields += sReportTable + ".\"" + sTempField + "\",";
                        }
                    }
                    sCheckField = sCheckField + "!" + rsComp.Rows[i]["component"].ToString() + "!";


                }
                sCreateSql = sCreateSql + ")";
                if (sReportField != null)
                {
                    sReportField = sReportField.Trim();
                    sFinalReportField = sFinalReportField.Trim();
                    sNumericFields = sNumericFields.Trim();
                    sTextFields = sTextFields.Trim();
                }
                if (sReportField != null)
                {
                    sReportField = sReportField.TrimEnd(',');
                    sFinalReportField = sFinalReportField.TrimEnd(',');
                    sNumericFields = sNumericFields.TrimEnd(',');
                    sTextFields = sTextFields.TrimEnd(',');
                }
                //dh.createDataSet(sCreateSql,"PrReport")

                using (DataManager dataManager = new DataManager())
                {
                    resultArgs = dataManager.UpdateData(sCreateSql);
                }
                if (!resultArgs.Success)
                {
                    MessageRender.ShowMessage("Unable to create report " + resultArgs.Message);
                    return false;
                }
                FillReportNew();
                rsComp.Dispose();
                return resultArgs.RowsAffected > 0;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }
        private bool FillReportNew()
        {
            string sStfSQL;
            bool bUpdate = false;
            try
            {
                //using (DataManager dataManager = new DataManager(SQLCommand.Payroll.DeleteTable))
                //{
                //    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                //    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                //    dataManager.Parameters.Add(dtCompMonth.TABLENAMEColumn, sReportTable);
                //    resultArgs = dataManager.UpdateData();
                //}
                //return resultArgs.Success;

                //sStfSQL = "SELECT DISTINCT PRStaffGroup.StaffId,PRStaffGroup.GroupId," +
                //    "PRStaff.ComponentId,PRStaff.CompValue," +
                //    "PRComponent.Component FROM PRStaffGroup,PRStaff,PRComponent" +
                //    " WHERE PRComponent.ComponentId = PRStaff.ComponentId" +
                //    " AND PRStaff.PayRollId = " + clsGeneral.PAYROLL_ID +
                //    " AND PRStaffGroup.Payrollid = PRStaff.Payrollid" +
                //    " AND PRStaff.StaffId = PRStaffGroup.StaffId" +
                //    " ORDER BY PRStaffGroup.StaffId";
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchStaffByGroup, "PRStaff"))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
                //dh.createDataSet(sStfSQL, "PRStaff");
                if (!resultArgs.Success)
                    return resultArgs.Success;
                DataTable dtPRStaff = resultArgs.DataSource.Table;

                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchFromTable, "PRReport"))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    dataManager.Parameters.Add(dtCompMonth.TABLENAMEColumn, sReportTable);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
                if (!resultArgs.Success)
                    return resultArgs.Success;
                //    dh.createDataSet("SELECT * FROM " + sReportTable, "PRReport");
                DataTable dtPRComp = resultArgs.DataSource.Table;

                // Read grid data and seperate them according to record vice using seperators
                string sPrevStaffId = "";
                string sCurStaffId = "";
                string sFinalValues = "";
                string sSeperator = "";
                int iRecordCount = 0;
                foreach (DataRow drStaff in dtPRStaff.Rows)
                {

                    bUpdate = true;
                    if (iRecordCount++ == 0)
                    {
                        sPrevStaffId = drStaff["StaffId"].ToString();
                    }
                    else
                    {
                        sPrevStaffId = sCurStaffId;
                    }
                    sCurStaffId = drStaff["StaffId"].ToString();
                    if (sPrevStaffId == sCurStaffId)
                        sFinalValues = sFinalValues + "#"; //for the same record
                    else
                        sFinalValues = sFinalValues + "$"; // for next type of record

                    sFinalValues = sFinalValues +
                        drStaff["StaffId"].ToString() + '@' +
                        drStaff["GroupId"].ToString() + '@' +
                        drStaff["Component"].ToString().ToUpper() + '@' +
                        drStaff["CompValue"].ToString() + '@' +
                    drStaff["TRANSACTIONDATE"].ToString().ToUpper();
                    sFinalValues = sFinalValues + sSeperator;


                }
                //Sort the data string according to a record based on staffid
                string[] sRecords = sFinalValues.Split('$');
                for (int i = 0; i < sRecords.Length; i++)
                {
                    string[] sCurrentRecord = sRecords[i].Split('#');
                    iRecordCount = 0;
                    DataRow drComp = dtPRComp.NewRow();
                    for (int j = 0; j < sCurrentRecord.Length; j++)
                    {
                        if (sCurrentRecord[j].ToString() != "")
                        {
                            string[] sFields = sCurrentRecord[j].Split('@');

                            if (iRecordCount == 0)
                            {
                                string temp = sFields[0].ToString();
                                drComp["STAFFID"] = Convert.ToInt32(sFields[0].ToString());
                                drComp["GROUPID"] = Convert.ToInt32(sFields[1].ToString());
                                drComp["TRANSACTIONDATE"] = clsGeneral.GetMySQLDateTime(sFields[4].ToString(), DateDataType.DateTime);
                                iRecordCount++;
                            }
                            try
                            {
                                //In accordance with the data type of the column merge data
                                int iFieldStartPosition = sFieldType.IndexOf("#" + sFields[2].ToString() + "@", 0); ;
                                int iFieldSepPosition = sFieldType.IndexOf("@", iFieldStartPosition + 1);
                                int iFieldTypeEndPosition = sFieldType.IndexOf("#", iFieldStartPosition + 1);
                                string sTempField = sFieldType.Substring(iFieldStartPosition + 1, sFields[2].ToString().Length);

                                if (sTempField.ToUpper() == sFields[2].ToString().ToUpper())
                                {
                                    string sTempFieldType = sFieldType.Substring(iFieldSepPosition + 1, (iFieldTypeEndPosition - iFieldSepPosition) - 1);
                                    if (sTempFieldType == typeof(int).ToString())
                                    {
                                        drComp[sFields[2].ToString()] = (Convert.ToInt32(sFields[3].ToString()) == 0) ? "0" : Convert.ToDouble(sFields[3].ToString()).ToString();
                                    }
                                    if (sTempFieldType == typeof(double).ToString())
                                    {
                                        drComp[sFields[2].ToString()] = (Convert.ToDouble(sFields[3].ToString()) == 0) ? "00" : Convert.ToDouble(sFields[3].ToString()).ToString();
                                    }
                                    else if (sTempFieldType == typeof(string).ToString())
                                    {
                                        drComp[sFields[2].ToString()] = (sFields[3].ToString() == "") ? "  " : sFields[3].ToString();
                                    }
                                }
                            }
                            catch
                            {
                            }
                        }
                    }
                    dtPRComp.Rows.Add(drComp);
                }
                //update individual records in the PRReportTable
                DataSet ds = new DataSet();
                ds.Tables.Add(dtPRComp);
                DataView dv = (DataView)ds.Tables[0].DefaultView;
                dvReportTableView = dv;
                if (bUpdate)
                {
                    for (int i = 0; i < dv.Table.Rows.Count; i++)
                    {
                        string sSql = "Insert into " + sReportTable;
                        string sValue = "(";
                        string sField = "(";
                        for (int j = 0; j < dv.Table.Columns.Count; j++)
                        {
                            sField = sField + "`" + dv.Table.Columns[j].ColumnName.ToString() + "`,";
                            sValue = sValue + "'" + dv.Table.Rows[i][j].ToString().Replace("'", "`") + "'" + ",";
                        }
                        sField = sField.TrimEnd(',');
                        sField = sField + ")";
                        sValue = sValue.TrimEnd(',');
                        sValue = sValue + ")";

                        sSql = sSql + "  " + sField + "  values" + sValue;
                        using (DataManager dataManager = new DataManager())
                        {
                            resultArgs = dataManager.UpdateData(sSql);
                        }
                        if (!resultArgs.Success)
                        {
                            //XtraMessageBox.Show("Unable to insert records in report.Because Components are duplicated", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            MessageRender.ShowMessage("Unable to generate the report " + resultArgs.Message);
                        }
                    }
                }
                return bUpdate;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
                return false;
            }
        }
        public DataTable GetEmployeeDailyPFReport(int groupID, string staffName)
        {
            CreateReportTable();
            ResultArgs resultArgs = null;
            DataTable dtSource = null;

            string condition = string.Empty;
            if (groupID == 0 && staffName == string.Empty)
                condition = " ";
            else if (groupID == 0 && staffName != string.Empty)
                condition = "AND CONCAT(FIRSTNAME ,' ',LASTNAME) = '" + staffName + "' ";
            else if (groupID != 0 && staffName == string.Empty)
                condition = "AND PRT.GROUPID = " + groupID;
            else
                condition = "AND PRT.GROUPID = " + groupID + " AND CONCAT(FIRSTNAME ,' ',LASTNAME) = '" + staffName + "' ";
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.DailyPFReports))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add("CONDITION", condition);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }

            DropReportTable();
            return resultArgs.DataSource.Table;
        }
        /// <summary>
        /// Included By : Jules A
        /// Purpose     : Fetch the Records for Employee PF Report
        /// </summary>
        /// <returns></returns>
        public DataTable GetEmployeePFReportDetails(string staffName)
        {
            CreateReportTable();
            ResultArgs resultArgs = null;
            DataTable dtSource = null;

            string condition = string.Empty;
            if (staffName == string.Empty)
                condition = " ";
            else if (staffName != string.Empty)
                condition = "AND CONCAT(FIRSTNAME ,' ',LASTNAME) = '" + staffName + "' ";
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.EmployeePFReport))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                dataManager.Parameters.Add("CONDITION", condition);
                resultArgs = dataManager.FetchData(DataSource.DataTable);

            }
            return resultArgs.DataSource.Table;
        }

        /// <summary>
        /// Included By : Rajeswari N
        /// Purpose     : Fetch the Records for Wages Report
        /// </summary>
        /// <returns></returns>
        public DataTable GetWagesReport(int nCompId)
        {
            CreateReportTable();
            string sSelSQL = this.GetFieldName(nCompId, 0, false, true);
            ResultArgs resultArgs = null;
            DataTable dtSource = null;
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchWagesReport))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                dataManager.Parameters.Add(dtCompMonth.TABLENAMEColumn, sReportTable);
                dataManager.Parameters.Add(dtCompMonth.FIELDVALUEColumn, sSelSQL);
                dataManager.Parameters.Add(dtCompMonth.GROUPIDColumn, nCompId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);

            }
            return resultArgs.DataSource.Table;
        }

        public DataTable GetEmployeeMonthlyPFReport(int groupID, DateTime dateFrom, DateTime dateTo)
        {
            CreateReportTable();
            ResultArgs resultArgs = null;
            DataTable dtSource = null;
            if (groupID == 0)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.MonthlyPFReportsForAll))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtCompMonth.TABLENAMEColumn, sReportTable);
                    dataManager.Parameters.Add("DATEFROM", dateFrom.ToString("yyyy-MM-dd") + " 00:00:00", DataType.DateTime);
                    dataManager.Parameters.Add("DATETO", dateTo.ToString("yyyy-MM-dd") + " 23:59:59", DataType.DateTime);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            else
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.MonthlyPFReports))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtCompMonth.GROUPIDColumn, groupID);
                    dataManager.Parameters.Add(dtCompMonth.TABLENAMEColumn, sReportTable);
                    dataManager.Parameters.Add("DATEFROM", dateFrom.ToString("yyyy-MM-dd") + " 00:00:00", DataType.DateTime);
                    dataManager.Parameters.Add("DATETO", dateTo.ToString("yyyy-MM-dd") + " 23:59:59", DataType.DateTime);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            DropReportTable();
            return resultArgs.DataSource.Table;
        }

        public DataTable GetStaffDetails()
        {
            DataTable dtPRStaff = null;
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchStaffDetailsForDailyReport, "PRstaff"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                    dtPRStaff = resultArgs.DataSource.Table;
            }
            return dtPRStaff;
        }
        public DataTable getPayrollDateInterval()
        {
            DataTable dtPayroll = null;
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPayrollDateInterval, "payroll"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                    dtPayroll = resultArgs.DataSource.Table;
            }
            return dtPayroll;
        }
        public DataTable getPayrollProcessDate(long groupid)
        {
            DataTable dtPayroll = null;
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPayrollProcessDate, "payroll"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                //dataManager.Parameters.Add(dtCompMonth.GROUPIDColumn, groupid);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                    dtPayroll = resultArgs.DataSource.Table;
            }
            return dtPayroll;
        }
    }
}
