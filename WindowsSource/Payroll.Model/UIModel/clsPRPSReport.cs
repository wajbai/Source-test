using System;
using System.Windows.Forms;
using System.Data;
using Bosco.DAO.Data;
using Payroll.DAO.Schema;
using Bosco.Utility;
//using CrystalDecisions.Shared;
//using CrystalDecisions.CrystalReports.Engine;
using Bosco.Utility.CommonMemberSet;
using Bosco.Utility.Common;

namespace Payroll.Model.UIModel
{
    public class clsPRPSReport : SystemBase
    {
        private string sReportTable;
        private string sReportField;
        //private int nVLeft;

        ResultArgs resultArgs = null;

        private long payRollId, groupId;
        private string payRollDate;
        private string payrollName;
        private string instituteName;
        private DataView dvReportTableView;
        private string sFieldType = "#";
        public string sFieldType1 = "";
        ListSetMember objCommon = new ListSetMember();
        ApplicationSchema.PRCOMPMONTHDataTable dtCompMonth = null;
        public clsPRPSReport()
            : this(0, "", "")
        {
            dtCompMonth = this.AppSchema.PRCOMPMONTH;
        }
        public clsPRPSReport(long payRollId, string payRollDate, string payrollName)
        {
            this.payRollId = payRollId;
            this.payRollDate = payRollDate;
            this.payrollName = payrollName;

            //     dataHandler = new DataHandling();

            //CreateReportTable();
        }
        public string InstituteName
        {
            get { return instituteName; }
            set { instituteName = value; }
        }

        public long PayRollId
        {
            get { return payRollId; }
            set { payRollId = value; }
        }

        public string PayRollDate
        {
            get { return payRollDate; }
            set { payRollDate = value; }
        }

        public string PayRollName
        {
            get { return payrollName; }
            set { payrollName = value; }
        }

        public long GroupId
        {
            get { return groupId; }
            set { groupId = value; }
        }
        public void FillStaffList(long nGroupId, CheckedListBox chkListBox)
        {
            string sName;
            string sStartDate, sFinishDate;
            //chkListBox.Items.Clear();
            sName = " concat(stfPersonal.FirstName , ' ' ,stfPersonal.LastName) ";
            //sSql = "Select to_char(prDate, 'DD/MM/YYYY') as prDate from PRCreate where payrollid = " + clsGeneral.PAYROLL_ID;
            try
            {
                DataTable tRsPRDate = null;
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPayrollDate, "PRCreate"))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                        tRsPRDate = resultArgs.DataSource.Table;
                }

                //createDataSet(sSql, "PRCreate");
                //DataTable tRsPRDate = getDataSet().Tables["PRCreate"];
                if (tRsPRDate != null && tRsPRDate.Rows.Count > 0)
                {
                    DateTime dtTemp = DateTime.Parse(tRsPRDate.Rows[0]["prDate"].ToString(), clsGeneral.DATE_FORMAT);
                    sStartDate = dtTemp.ToShortDateString();

                    dtTemp = dtTemp.AddMonths(1);
                    sFinishDate = dtTemp.AddDays(-1).ToShortDateString();
                }
            }
            catch
            {
                return;
            }
            //sSql ="Select prstaffgroup.staffid, IIF(ISNULL(stfpersonal.firstname), '', stfpersonal.firstname) + IIF(ISNULL(' '), '', ' ') + IIF(ISNULL(stfpersonal.lastname), '', stfpersonal.lastname)  as Name,PRStaffGroup.Groupid from PRStaffGroup,stfpersonal,stfService where prstaffgroup.Groupid = 1 and prstaffgroup.payrollid = 17 and stfpersonal.staffid = prstaffgroup.staffid  AND stfService.StaffId = stfPersonal.StaffId AND  DateValue('30/04/2005')  >= stfPersonal.dateofJoin  and (stfPersonal.LeavingDate is null or stfPersonal.LeavingDate >  DateValue('01/04/2005') )  AND (( DateValue('30/04/2005')  BETWEEN stfService.DateofAppointment AND stfService.DateofTermination)  OR (stfService.DateofTermination is null AND  DateValue('30/04/2005')  > stfService.DateofAppointment)) order by PRStaffGroup.GroupId,PRStaffGroup.StaffOrder";
            //			sSql = "Select prstaffgroup.staffid,stfpersonal.firstname || ' ' || stfpersonal.lastname as Name,PRStaffGroup.Groupid from PRStaffGroup,stfpersonal,stfService,prstaff "+
            //				"where prstaffgroup.Groupid = " + nGroupId + " and prstaffgroup.payrollid = " + clsGeneral.PAYROLL_ID +
            //				" and stfpersonal.staffid = prstaffgroup.staffid and prstaff.staffid = stfpersonal.staffid AND stfService.StaffId = stfPersonal.StaffId AND to_date('" +
            //				sFinishDate +"','dd/MM/yyyy') >= stfPersonal.dateofJoin "+
            //				" and (stfPersonal.LeavingDate is null or stfPersonal.LeavingDate > to_date('" +
            //				sStartDate + "','dd/MM/yyyy')) " +
            //				" AND ((to_date('" + sFinishDate + "','dd/MM/yyyy') BETWEEN " +
            //				"stfService.DateofAppointment AND stfService.DateofTermination) " +
            //				" OR (stfService.DateofTermination is null AND to_date('" +
            //				sFinishDate + "','dd/MM/yyyy') > stfService.DateofAppointment)) " +
            //				"order by PRStaffGroup.GroupId,PRStaffGroup.StaffOrder";

            //sSql = "select distinct pr.staffid,p.firstname || ' ' || p.lastname as Name from prstaff pr,stfpersonal p,prstaffgroup pg" +
            //" where pr.staffid = p.staffid and pg.staffid = pr.staffid and pr.payrollid = " + clsGeneral.PAYROLL_ID + " and pg.payrollid = " + clsGeneral.PAYROLL_ID + " and pg.groupid =" + nGroupId;
            //createDataSet(sSql, "PRstaff");
            DataTable dtPRStaff = null;
            if (nGroupId == 0)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchStaffAllDetails, "PRstaff"))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                        dtPRStaff = resultArgs.DataSource.Table;
                }
            }
            else
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchStaffDetailsByStaffId, "PRstaff"))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                    dataManager.Parameters.Add(dtCompMonth.GROUPIDColumn, nGroupId);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                        dtPRStaff = resultArgs.DataSource.Table;
                }
            }

            // DataTable dtPRStaff = getDataSet().Tables["PRstaff"];
            chkListBox.DataSource = dtPRStaff;
            chkListBox.DisplayMember = "NAME";
            chkListBox.ValueMember = "STAFFID";
            chkListBox.Refresh();
        }

        public void FillStaffListfroDevexpressChklst(int nGroupId, DevExpress.XtraEditors.CheckedListBoxControl chkListBox)
        {
            string sName;
            string sStartDate, sFinishDate;
            //chkListBox.Items.Clear();
            sName = " concat(stfPersonal.FirstName , ' ' ,stfPersonal.LastName) ";
            //sSql = "Select to_char(prDate, 'DD/MM/YYYY') as prDate from PRCreate where payrollid = " + clsGeneral.PAYROLL_ID;
            try
            {
                DataTable tRsPRDate = null;
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPayrollDate, "PRCreate"))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                        tRsPRDate = resultArgs.DataSource.Table;
                }

                //createDataSet(sSql, "PRCreate");
                //DataTable tRsPRDate = getDataSet().Tables["PRCreate"];
                if (tRsPRDate != null && tRsPRDate.Rows.Count > 0)
                {
                    DateTime dtTemp = DateTime.Parse(tRsPRDate.Rows[0]["prDate"].ToString(), clsGeneral.DATE_FORMAT);
                    sStartDate = dtTemp.ToShortDateString();

                    dtTemp = dtTemp.AddMonths(1);
                    sFinishDate = dtTemp.AddDays(-1).ToShortDateString();
                }
            }
            catch
            {
                return;
            }
            //sSql ="Select prstaffgroup.staffid, IIF(ISNULL(stfpersonal.firstname), '', stfpersonal.firstname) + IIF(ISNULL(' '), '', ' ') + IIF(ISNULL(stfpersonal.lastname), '', stfpersonal.lastname)  as Name,PRStaffGroup.Groupid from PRStaffGroup,stfpersonal,stfService where prstaffgroup.Groupid = 1 and prstaffgroup.payrollid = 17 and stfpersonal.staffid = prstaffgroup.staffid  AND stfService.StaffId = stfPersonal.StaffId AND  DateValue('30/04/2005')  >= stfPersonal.dateofJoin  and (stfPersonal.LeavingDate is null or stfPersonal.LeavingDate >  DateValue('01/04/2005') )  AND (( DateValue('30/04/2005')  BETWEEN stfService.DateofAppointment AND stfService.DateofTermination)  OR (stfService.DateofTermination is null AND  DateValue('30/04/2005')  > stfService.DateofAppointment)) order by PRStaffGroup.GroupId,PRStaffGroup.StaffOrder";
            //			sSql = "Select prstaffgroup.staffid,stfpersonal.firstname || ' ' || stfpersonal.lastname as Name,PRStaffGroup.Groupid from PRStaffGroup,stfpersonal,stfService,prstaff "+
            //				"where prstaffgroup.Groupid = " + nGroupId + " and prstaffgroup.payrollid = " + clsGeneral.PAYROLL_ID +
            //				" and stfpersonal.staffid = prstaffgroup.staffid and prstaff.staffid = stfpersonal.staffid AND stfService.StaffId = stfPersonal.StaffId AND to_date('" +
            //				sFinishDate +"','dd/MM/yyyy') >= stfPersonal.dateofJoin "+
            //				" and (stfPersonal.LeavingDate is null or stfPersonal.LeavingDate > to_date('" +
            //				sStartDate + "','dd/MM/yyyy')) " +
            //				" AND ((to_date('" + sFinishDate + "','dd/MM/yyyy') BETWEEN " +
            //				"stfService.DateofAppointment AND stfService.DateofTermination) " +
            //				" OR (stfService.DateofTermination is null AND to_date('" +
            //				sFinishDate + "','dd/MM/yyyy') > stfService.DateofAppointment)) " +
            //				"order by PRStaffGroup.GroupId,PRStaffGroup.StaffOrder";

            //sSql = "select distinct pr.staffid,p.firstname || ' ' || p.lastname as Name from prstaff pr,stfpersonal p,prstaffgroup pg" +
            //" where pr.staffid = p.staffid and pg.staffid = pr.staffid and pr.payrollid = " + clsGeneral.PAYROLL_ID + " and pg.payrollid = " + clsGeneral.PAYROLL_ID + " and pg.groupid =" + nGroupId;
            //createDataSet(sSql, "PRstaff");
            DataTable dtPRStaff = null;
            if (nGroupId == 0)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchStaffAllDetails, "PRstaff"))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                        dtPRStaff = resultArgs.DataSource.Table;
                }
            }
            else
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchStaffDetailsByStaffId, "PRstaff"))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                    dataManager.Parameters.Add(dtCompMonth.GROUPIDColumn, nGroupId);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                        dtPRStaff = resultArgs.DataSource.Table;
                }
            }

            // DataTable dtPRStaff = getDataSet().Tables["PRstaff"];
            chkListBox.DataSource = dtPRStaff;
            chkListBox.DisplayMember = "Name";
            chkListBox.ValueMember = "STAFFID";
            chkListBox.Refresh();
        }

        public void FillStaffListfroDevexpressChklst(int nGroupId, int clsPayrollId, DevExpress.XtraEditors.CheckedListBoxControl chkListBox)
        {
            string sName = string.Empty;
            string sStartDate, sFinishDate;
            sName = " concat(stfPersonal.FirstName , ' ' ,stfPersonal.LastName) ";
            try
            {
                DataTable tRsPRDate = null;
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPayrollDate, "PRCreate"))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, clsPayrollId);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                        tRsPRDate = resultArgs.DataSource.Table;
                }

                if (tRsPRDate != null && tRsPRDate.Rows.Count > 0)
                {
                    DateTime dtTemp = DateTime.Parse(tRsPRDate.Rows[0]["prDate"].ToString(), clsGeneral.DATE_FORMAT);
                    sStartDate = dtTemp.ToShortDateString();

                    dtTemp = dtTemp.AddMonths(1);
                    sFinishDate = dtTemp.AddDays(-1).ToShortDateString();
                }
            }
            catch
            {
                return;
            }
            DataTable dtPRStaff = null;
            if (nGroupId == 0)
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchStaffAllDetails, "PRstaff"))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, clsPayrollId);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                        dtPRStaff = resultArgs.DataSource.Table;
                }
            }
            else
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchStaffDetailsByStaffId, "PRstaff"))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, clsPayrollId);
                    dataManager.Parameters.Add(dtCompMonth.GROUPIDColumn, nGroupId);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                        dtPRStaff = resultArgs.DataSource.Table;
                }
            }

            chkListBox.DataSource = dtPRStaff;
            chkListBox.DisplayMember = "Name";
            chkListBox.ValueMember = "STAFFID";
            chkListBox.Refresh();
        }
        // To get staff details by the staffid (Praveen)
        public DataTable GetStaffByGroup(string nGroupId, int clsPayrollId)
        {
            string sName = string.Empty;
            string sStartDate, sFinishDate;
            sName = " concat(stfPersonal.FirstName , ' ' ,stfPersonal.LastName) ";
            try
            {
                DataTable tRsPRDate = null;
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPayrollDate, "PRCreate"))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, clsPayrollId);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                        tRsPRDate = resultArgs.DataSource.Table;
                }

                if (tRsPRDate != null && tRsPRDate.Rows.Count > 0)
                {
                    DateTime dtTemp = DateTime.Parse(tRsPRDate.Rows[0]["prDate"].ToString(), clsGeneral.DATE_FORMAT);
                    sStartDate = dtTemp.ToShortDateString();

                    dtTemp = dtTemp.AddMonths(1);
                    sFinishDate = dtTemp.AddDays(-1).ToShortDateString();
                }
            }
            catch
            {
              //  return;
            }
            DataTable dtPRStaff = null;
            if (nGroupId == "0")
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchStaffAllDetails, "PRstaff"))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, clsPayrollId);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                        dtPRStaff = resultArgs.DataSource.Table;
                }
            }
            else
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchStaffDetailsByStaffId, "PRstaff"))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, clsPayrollId);
                    dataManager.Parameters.Add(dtCompMonth.GROUPIDColumn.ColumnName, nGroupId, DataType.Int);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                        dtPRStaff = resultArgs.DataSource.Table;
                }
            }

            //if (resultArgs.Success && dtPRStaff!=null)
            // {
            //     dtPRStaff.DefaultView.Sort = "Name";
            //     dtPRStaff = dtPRStaff.DefaultView.ToTable();

            // }
            return dtPRStaff;
            //chkListBox.DataSource = dtPRStaff;
            //chkListBox.DisplayMember = "Name";
            //chkListBox.ValueMember = "STAFFID";
            //chkListBox.Refresh();
        }
        public void FillStaffList(string nDepartment, CheckedListBox chkListBox)
        {

            string sName = string.Empty;
            string sStartDate, sFinishDate;
            //chkListBox.Items.Clear();
            sName = " concat(stfPersonal.FirstName , ' ' ,stfPersonal.LastName)";
            //  sSql = "Select to_char(prDate, 'DD/MM/YYYY') as prDate from PRCreate where payrollid = " + clsGeneral.PAYROLL_ID;
            try
            {
                //createDataSet(sSql, "PRCreate");
                //DataTable tRsPRDate = getDataSet().Tables["PRCreate"];
                DataTable tRsPRDate = null;
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPayrollDate, "PRCreate"))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                        tRsPRDate = resultArgs.DataSource.Table;
                }

                DateTime dtTemp = DateTime.Parse(tRsPRDate.Rows[0]["prDate"].ToString(), clsGeneral.DATE_FORMAT);
                sStartDate = dtTemp.ToShortDateString();

                dtTemp = dtTemp.AddMonths(1);
                sFinishDate = dtTemp.AddDays(-1).ToShortDateString();
            }
            catch
            {
                return;
            }
            //sSql ="Select prstaffgroup.staffid, IIF(ISNULL(stfpersonal.firstname), '', stfpersonal.firstname) + IIF(ISNULL(' '), '', ' ') + IIF(ISNULL(stfpersonal.lastname), '', stfpersonal.lastname)  as Name,PRStaffGroup.Groupid from PRStaffGroup,stfpersonal,stfService where prstaffgroup.Groupid = 1 and prstaffgroup.payrollid = 17 and stfpersonal.staffid = prstaffgroup.staffid  AND stfService.StaffId = stfPersonal.StaffId AND  DateValue('30/04/2005')  >= stfPersonal.dateofJoin  and (stfPersonal.LeavingDate is null or stfPersonal.LeavingDate >  DateValue('01/04/2005') )  AND (( DateValue('30/04/2005')  BETWEEN stfService.DateofAppointment AND stfService.DateofTermination)  OR (stfService.DateofTermination is null AND  DateValue('30/04/2005')  > stfService.DateofAppointment)) order by PRStaffGroup.GroupId,PRStaffGroup.StaffOrder";
            //			sSql = "Select prstaffgroup.staffid,stfpersonal.firstname || ' ' || stfpersonal.lastname as Name,PRStaffGroup.Groupid from PRStaffGroup,stfpersonal,stfService "+
            //				"where prstaffgroup.payrollid = " + clsGeneral.PAYROLL_ID +
            //				" and stfpersonal.staffid = prstaffgroup.staffid AND stfService.StaffId = stfPersonal.StaffId AND to_date('" +
            //				sFinishDate +"','dd/MM/yyyy') >= stfPersonal.dateofJoin "+
            //				" and (stfPersonal.LeavingDate is null or stfPersonal.LeavingDate > to_date('" +
            //				sStartDate + "','dd/MM/yyyy')) " +
            //				" AND ((to_date('" + sFinishDate + "','dd/MM/yyyy') BETWEEN " +
            //				"stfService.DateofAppointment AND stfService.DateofTermination) " +
            //				" OR (stfService.DateofTermination is null AND to_date('" +
            //				sFinishDate + "','dd/MM/yyyy') > stfService.DateofAppointment)) " +
            //				"and stfPersonal.Deptid = "+ nDepartment +"order by PRStaffGroup.GroupId,PRStaffGroup.StaffOrder";
            //sSql = "select distinct pr.staffid,p.firstname || ' ' || p.lastname as Name from prstaff pr,stfpersonal p" +
            //        " where pr.staffid = p.staffid and pr.payrollid =" + clsGeneral.PAYROLL_ID + " and p.deptid =" + nDepartment;
            //createDataSet(sSql, "PRstaff1");
            DataTable dtPRStaff = null;
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchStaffDetailsByStaffId, "PRstaff1"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                //dataManager.Parameters.Add(dtCompMonth.GROUPIDColumn, nGroupId);
                dataManager.Parameters.Add(dtCompMonth.DEPARTMENTIDColumn, nDepartment);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                    dtPRStaff = resultArgs.DataSource.Table;
            }
            //    DataTable dtPRStaff = getDataSet().Tables["PRstaff1"];
            chkListBox.DataSource = dtPRStaff;
            chkListBox.DisplayMember = "NAME";
            chkListBox.ValueMember = "STAFFID";
        }
        //Added by KL on 02-02-2007		Begin
        public void FillSalaryGroup(int nGroup, ComboBox cboGroup)
        {
            //Purpose    : This method is used to Fill the Values for the Selected Salary Group
            //Arguments	 : nGroup - 0-Staff group 1-Departments combobox to fill

            string strQuery = "";
            if (nGroup == 0)
            {
                //strQuery = "SELECT GROUPID,GROUPNAME FROM PRSALARYGROUP"; FetchPayrollCompMonth
                // clsGeneral.fillList(cboGroup, SQLCommand.Payroll.FetchPayrollCompMonth, "GROUPNAME", "GROUPID");
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPayrollCompMonth))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    objCommon.BindComboBox(cboGroup, resultArgs.DataSource.Table, "GROUPNAME", "GROUPID", true);
                }
            }
            //else
            //{
            //    strQuery = "SELECT HDEPT_ID,HDEPT_DESC FROM HOSPITAL_DEPARTMENTS";//PayrollStaffDeptList
            //    clsGeneral.fillList(cboGroup, strQuery, "HDEPT_DESC", "HDEPT_ID");
            //}
        }
        public DataView CreateReportTable()
        {
            //Purpose    : This method is used to Create the Dataview for All Components
            //ReturnType : As Dataview. The final report table , for the reports

            string sSql, sCheckField;
            int i;

            sReportField = "";
            sCheckField = "";

            //sSql = "SELECT DISTINCT PRComponent.Component,PRComponent.Type," +
            //    "PRCompMonth.Comp_Order,PRComponent.CompRound FROM PRComponent,PRCompMonth " +
            //    "WHERE PRComponent.ComponentId = PRCompMonth.ComponentId AND " +
            //    "PRCompMonth.PayRollId = " + clsGeneral.PAYROLL_ID +
            //    " ORDER BY PRCompMonth.Comp_Order";
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchComponentForReport, "PRComponent"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                resultArgs = dataManager.FetchData(DataSource.DataTable);

            }
            if (!resultArgs.Success)
                return null;
            //dataHandler.createDataSet(sSql, "PRComponent");
            DataTable dtPRComp = resultArgs.DataSource.Table;


            //sCreateSql = "CREATE TABLE " + sReportTable + " (StaffId long, " + //long
            //	", GroupId long "; //Long
            //sReportField = sReportTable + ".[StaffId]," + sReportTable + ".[GroupId],";

            DataTable dtPRPSTable = new DataTable("PRPaySlip");
            dtPRPSTable.Columns.Add("StaffId", typeof(long));
            dtPRPSTable.Columns.Add("GroupId", typeof(long));
            //dtPRPSTable.Columns.Add("DeptId", typeof(long));

            for (i = 0; i < dtPRComp.Rows.Count; i++)
            {
                if (sCheckField.IndexOf("!" + dtPRComp.Rows[i]["Component"].ToString() + "!", 0) < 0)
                {
                    if (int.Parse(dtPRComp.Rows[i]["Type"].ToString()) <= 1 & int.Parse(dtPRComp.Rows[i]["COMPROUND"].ToString()) == 0)
                    {
                        dtPRPSTable.Columns.Add(dtPRComp.Rows[i]["Component"].ToString(), typeof(double));
                        sFieldType += dtPRComp.Rows[i]["Component"].ToString().ToUpper() + "@" + typeof(double).ToString() + "#";
                        sFieldType1 += dtPRComp.Rows[i]["Component"].ToString() + "@" + typeof(double).ToString() + "#";
                    }
                    else if (int.Parse(dtPRComp.Rows[i]["Type"].ToString()) <= 1 & int.Parse(dtPRComp.Rows[i]["COMPROUND"].ToString()) > 0)
                    {
                        dtPRPSTable.Columns.Add(dtPRComp.Rows[i]["Component"].ToString(), typeof(double));
                        sFieldType += dtPRComp.Rows[i]["Component"].ToString().ToUpper() + "@" + typeof(double).ToString() + "#";
                        sFieldType1 += dtPRComp.Rows[i]["Component"].ToString() + "@" + typeof(double).ToString() + "#";
                    }
                    //					else if (int.Parse(dtPRComp.Rows[i]["Type"].ToString()) == 2)
                    //					{
                    //						//sCreateSql += ",[" + dtPRComp.Rows[i]["Component"].ToString() + "] date";
                    //						dtPRPSTable.Columns.Add(dtPRComp.Rows[i]["Component"].ToString(), typeof(DateTime));
                    //					}
                    else
                    {
                        dtPRPSTable.Columns.Add(dtPRComp.Rows[i]["Component"].ToString(), typeof(string));
                        sFieldType += dtPRComp.Rows[i]["Component"].ToString().ToUpper() + "@" + typeof(string).ToString() + "#";
                        sFieldType1 += dtPRComp.Rows[i]["Component"].ToString() + "@" + typeof(string).ToString() + "#";
                    }
                }

                sCheckField += "!" + dtPRComp.Rows[i]["Component"].ToString() + "!";
            }

            FillReport(dtPRPSTable);
            //			DataView dvReportTable = new DataView(dtPRPSTable);

            //			sSql = "SELECT COMPONENT,CAPTION FROM PRCOMPONENT ORDER BY COMPONENT";
            //			dataHandler.createDataSet(sSql, "Component");
            //			DataView dvPRColumns = dataHandler.getDataSet().Tables["Component"].DefaultView;
            //			
            //			sSql = "SELECT CAPTION FROM PRCOMPONENT ORDER BY CAPTION";
            //			dataHandler.createDataSet(sSql, "Component");
            //			DataView dvPRCaption = dataHandler.getDataSet().Tables["Component"].DefaultView;
            //			string strCaptions="";
            //			
            //			for ( int i =0; i< dvPRCaption.Table.Rows.Count;i++)
            //			{
            //				dvPRColumns.RowFilter = " CAPTION = ' " + dvPRCaption[i].ToString() + " ' ";
            //				if ( dvPRColumns.Count > 0)
            //				{
            //					for( int i=0; i < dvPRColumns.Count ; i++)
            //					{
            //						strCaptions += dvPRColumns[i].ToString()+"#";
            //					}
            //				}
            //				else
            //				{
            //					strCaptions += dvPRColumns[0].ToString();
            //				}
            //				strCaptions += "|";
            //			}
            //			

            return dtPRPSTable.DefaultView;

        }

        public DataView CreateReportTable(int crPayrollId, string TypeId)
        {
            //Purpose    : This method is used to Create the Dataview for All Components
            //ReturnType : As Dataview. The final report table , for the reports

            string sSql, sCheckField;
            int i;

            sReportField = "";
            sCheckField = "";

            //sSql = "SELECT DISTINCT PRComponent.Component,PRComponent.Type," +
            //    "PRCompMonth.Comp_Order,PRComponent.CompRound FROM PRComponent,PRCompMonth " +
            //    "WHERE PRComponent.ComponentId = PRCompMonth.ComponentId AND " +
            //    "PRCompMonth.PayRollId = " + clsGeneral.PAYROLL_ID +
            //    " ORDER BY PRCompMonth.Comp_Order";
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchComponentForReport, "PRComponent"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, crPayrollId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);

            }
            if (!resultArgs.Success)
                return null;
            //dataHandler.createDataSet(sSql, "PRComponent");
            DataTable dtPRComp = resultArgs.DataSource.Table;

            DataView dvslip = dtPRComp.AsDataView();
            if (dvslip != null)
            {
                dvslip.RowFilter = "TYPE IN (" + TypeId + ")";
            }
            dtPRComp = dvslip.ToTable();

            //sCreateSql = "CREATE TABLE " + sReportTable + " (StaffId long, " + //long
            //	", GroupId long "; //Long
            //sReportField = sReportTable + ".[StaffId]," + sReportTable + ".[GroupId],";

            DataTable dtPRPSTable = new DataTable("PRPaySlip");
            dtPRPSTable.Columns.Add("StaffId", typeof(long));
            dtPRPSTable.Columns.Add("GroupId", typeof(long));
            //dtPRPSTable.Columns.Add("DeptId", typeof(long));

            for (i = 0; i < dtPRComp.Rows.Count; i++)
            {
                if (sCheckField.IndexOf("!" + dtPRComp.Rows[i]["Component"].ToString() + "!", 0) < 0)
                {
                    if (int.Parse(dtPRComp.Rows[i]["Type"].ToString()) <= 1 & int.Parse(dtPRComp.Rows[i]["COMPROUND"].ToString()) == 0)
                    {
                        dtPRPSTable.Columns.Add(dtPRComp.Rows[i]["Component"].ToString(), typeof(int));
                        sFieldType += dtPRComp.Rows[i]["Component"].ToString().ToUpper() + "@" + typeof(int).ToString() + "#";
                        sFieldType1 += dtPRComp.Rows[i]["Component"].ToString() + "@" + typeof(int).ToString() + "#";
                    }
                    else if (int.Parse(dtPRComp.Rows[i]["Type"].ToString()) <= 1 & int.Parse(dtPRComp.Rows[i]["COMPROUND"].ToString()) > 0)
                    {
                        dtPRPSTable.Columns.Add(dtPRComp.Rows[i]["Component"].ToString(), typeof(double));
                        sFieldType += dtPRComp.Rows[i]["Component"].ToString().ToUpper() + "@" + typeof(double).ToString() + "#";
                        sFieldType1 += dtPRComp.Rows[i]["Component"].ToString() + "@" + typeof(double).ToString() + "#";
                    }
                    //					else if (int.Parse(dtPRComp.Rows[i]["Type"].ToString()) == 2)
                    //					{
                    //						//sCreateSql += ",[" + dtPRComp.Rows[i]["Component"].ToString() + "] date";
                    //						dtPRPSTable.Columns.Add(dtPRComp.Rows[i]["Component"].ToString(), typeof(DateTime));
                    //					}
                    else
                    {
                        dtPRPSTable.Columns.Add(dtPRComp.Rows[i]["Component"].ToString(), typeof(string));
                        sFieldType += dtPRComp.Rows[i]["Component"].ToString().ToUpper() + "@" + typeof(string).ToString() + "#";
                        sFieldType1 += dtPRComp.Rows[i]["Component"].ToString() + "@" + typeof(string).ToString() + "#";
                    }
                }

                sCheckField += "!" + dtPRComp.Rows[i]["Component"].ToString() + "!";
            }

            FillReport(dtPRPSTable, crPayrollId);
            //			DataView dvReportTable = new DataView(dtPRPSTable);

            //			sSql = "SELECT COMPONENT,CAPTION FROM PRCOMPONENT ORDER BY COMPONENT";
            //			dataHandler.createDataSet(sSql, "Component");
            //			DataView dvPRColumns = dataHandler.getDataSet().Tables["Component"].DefaultView;
            //			
            //			sSql = "SELECT CAPTION FROM PRCOMPONENT ORDER BY CAPTION";
            //			dataHandler.createDataSet(sSql, "Component");
            //			DataView dvPRCaption = dataHandler.getDataSet().Tables["Component"].DefaultView;
            //			string strCaptions="";
            //			
            //			for ( int i =0; i< dvPRCaption.Table.Rows.Count;i++)
            //			{
            //				dvPRColumns.RowFilter = " CAPTION = ' " + dvPRCaption[i].ToString() + " ' ";
            //				if ( dvPRColumns.Count > 0)
            //				{
            //					for( int i=0; i < dvPRColumns.Count ; i++)
            //					{
            //						strCaptions += dvPRColumns[i].ToString()+"#";
            //					}
            //				}
            //				else
            //				{
            //					strCaptions += dvPRColumns[0].ToString();
            //				}
            //				strCaptions += "|";
            //			}
            //			

            return dtPRPSTable.DefaultView;

        }


        private bool FillReport(DataTable dtPRPSTable)
        {
            //Purpose    : This method is used to Fill the Values for the Selected Components
            //Modified by PE on 08-03-2007

            string sStfSQL;
            bool bUpdate = false;

            //sStfSQL = "SELECT PRStaffGroup.StaffId,PRStaffGroup.GroupId," +
            //    "PRStaff.ComponentId,PRStaff.CompValue," +
            //    "PRComponent.Component FROM PRStaffGroup,PRStaff,PRComponent" +
            //    " WHERE PRComponent.ComponentId = PRStaff.ComponentId" +
            //    " AND PRStaff.PayRollId = " + clsGeneral.PAYROLL_ID +
            //    " AND PRStaffGroup.Payrollid = PRStaff.Payrollid" +
            //    " AND PRStaff.StaffId = PRStaffGroup.StaffId" +
            //    " ORDER BY PRStaffGroup.StaffId";
            DataTable dtPRStaff = null;
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchValuesbyComponent, "PRStaff"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            if (!resultArgs.Success)
                return false;
            else
            {
                dtPRStaff = resultArgs.DataSource.Table;
            }
            //dataHandler.createDataSet(sStfSQL, "PRStaff");
            //DataTable dtPRStaff = dataHandler.getDataSet().Tables["PRStaff"];

            //dataHandler.createDataSet("SELECT * FROM " + sReportTable, "PRComponent");
            //DataTable dtPRPSTable = dataHandler.getDataSet().Tables["PRComponent"];

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
                DataRow drComp = dtPRPSTable.NewRow();

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
                            int iFieldStartPosition = sFieldType.IndexOf("#" + sFields[2].ToString() + "@", 0);
                            int iFieldSepPosition = sFieldType.IndexOf("@", iFieldStartPosition + 1);
                            int iFieldTypeEndPosition = sFieldType.IndexOf("#", iFieldStartPosition + 1);
                            string sTempField = sFieldType.Substring(iFieldStartPosition + 1, sFields[2].ToString().Length);
                            string sTempVal = sFields[3].ToString();

                            if (sTempField.ToUpper() == sFields[2].ToString().ToUpper())
                            {
                                string sTempFieldType = sFieldType.Substring(iFieldSepPosition + 1, (iFieldTypeEndPosition - iFieldSepPosition) - 1);
                                if (sTempFieldType == typeof(int).ToString())
                                {
                                    drComp[sFields[2].ToString()] = (Convert.ToInt32(sFields[3].ToString()) == 0) ? 0 : Convert.ToDouble(sFields[3].ToString());
                                }
                                if (sTempFieldType == typeof(double).ToString())
                                {
                                    drComp[sFields[2].ToString()] = (Convert.ToDouble(sFields[3].ToString()) == 0) ? 0 : Convert.ToDouble(sFields[3].ToString());
                                }
                                else if (sTempFieldType == typeof(string).ToString())
                                {
                                    try
                                    {
                                        drComp[sFields[2].ToString()] = DateTime.Parse(sFields[3].ToString()).ToShortDateString();
                                    }
                                    catch
                                    {
                                        drComp[sFields[2].ToString()] = (sFields[3].ToString() == "") ? " " : sFields[3].ToString();
                                    }
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                dtPRPSTable.Rows.Add(drComp);
            }
            if (bUpdate)
            {
                dtPRPSTable.AcceptChanges();
                DataView dv = (DataView)dtPRPSTable.DefaultView;
                dvReportTableView = dv;
            }
            if (bUpdate) dtPRPSTable.AcceptChanges();
            return bUpdate;
        }

        private bool FillReport(DataTable dtPRPSTable, int PayId)
        {
            //Purpose    : This method is used to Fill the Values for the Selected Components
            //Modified by PE on 08-03-2007

            string sStfSQL;
            bool bUpdate = false;

            //sStfSQL = "SELECT PRStaffGroup.StaffId,PRStaffGroup.GroupId," +
            //    "PRStaff.ComponentId,PRStaff.CompValue," +
            //    "PRComponent.Component FROM PRStaffGroup,PRStaff,PRComponent" +
            //    " WHERE PRComponent.ComponentId = PRStaff.ComponentId" +
            //    " AND PRStaff.PayRollId = " + clsGeneral.PAYROLL_ID +
            //    " AND PRStaffGroup.Payrollid = PRStaff.Payrollid" +
            //    " AND PRStaff.StaffId = PRStaffGroup.StaffId" +
            //    " ORDER BY PRStaffGroup.StaffId";
            DataTable dtPRStaff = null;
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchValuesbyComponent, "PRStaff"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, PayId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            if (!resultArgs.Success)
                return false;
            else
            {
                dtPRStaff = resultArgs.DataSource.Table;
            }
            //dataHandler.createDataSet(sStfSQL, "PRStaff");
            //DataTable dtPRStaff = dataHandler.getDataSet().Tables["PRStaff"];

            //dataHandler.createDataSet("SELECT * FROM " + sReportTable, "PRComponent");
            //DataTable dtPRPSTable = dataHandler.getDataSet().Tables["PRComponent"];

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
                DataRow drComp = dtPRPSTable.NewRow();

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
                            int iFieldStartPosition = sFieldType.IndexOf("#" + sFields[2].ToString() + "@", 0);
                            int iFieldSepPosition = sFieldType.IndexOf("@", iFieldStartPosition + 1);
                            int iFieldTypeEndPosition = sFieldType.IndexOf("#", iFieldStartPosition + 1);
                            string sTempField = sFieldType.Substring(iFieldStartPosition + 1, sFields[2].ToString().Length);
                            string sTempVal = sFields[3].ToString();

                            if (sTempField.ToUpper() == sFields[2].ToString().ToUpper())
                            {
                                string sTempFieldType = sFieldType.Substring(iFieldSepPosition + 1, (iFieldTypeEndPosition - iFieldSepPosition) - 1);
                                if (sTempFieldType == typeof(int).ToString())
                                {
                                    drComp[sFields[2].ToString()] = (Convert.ToInt32(sFields[3].ToString()) == 0) ? 0 : Convert.ToDouble(sFields[3].ToString());
                                }
                                if (sTempFieldType == typeof(double).ToString())
                                {
                                    drComp[sFields[2].ToString()] = (Convert.ToDouble(sFields[3].ToString()) == 0) ? 0 : Convert.ToDouble(sFields[3].ToString());
                                }
                                else if (sTempFieldType == typeof(string).ToString())
                                {
                                    try
                                    {
                                        drComp[sFields[2].ToString()] = DateTime.Parse(sFields[3].ToString()).ToShortDateString();
                                    }
                                    catch
                                    {
                                        drComp[sFields[2].ToString()] = (sFields[3].ToString() == "") ? " " : sFields[3].ToString();
                                    }
                                }

                            }
                        }
                        catch (Exception ex)
                        {
                        }
                    }
                }
                dtPRPSTable.Rows.Add(drComp);
            }
            if (bUpdate)
            {
                dtPRPSTable.AcceptChanges();
                DataView dv = (DataView)dtPRPSTable.DefaultView;
                dvReportTableView = dv;
            }
            if (bUpdate) dtPRPSTable.AcceptChanges();
            return bUpdate;
        }

        public DataTable GetStaffComponentId()
        {
            //string sSql = "SELECT DISTINCT P.COMPONENTID,PRCOMPONENT.COMPONENT, " +
            //            "(SELECT distinct max(COMPORDER) FROM PRSTAFF " +
            //            "WHERE PRSTAFF.COMPONENTID = P.COMPONENTID) AS COMPORDER " +
            //            "FROM PRSTAFF P, PRCOMPONENT WHERE PRCOMPONENT.COMPONENTID = P.COMPONENTID " +
            //            "AND P.PAYROLLID = " + clsGeneral.PAYROLL_ID + " ORDER BY COMPORDER";
            //			string sSql="SELECT DISTINCT PRSTAFF.COMPONENTID,PRCOMPONENT.COMPONENT,PRSTAFF.COMPORDER FROM PRSTAFF,PRCOMPONENT WHERE PRCOMPONENT.COMPONENTID = PRSTAFF.COMPONENTID "+
            //						"AND PRSTAFF.PAYROLLID = " + clsGeneral.PAYROLL_ID +" ORDER BY PRSTAFF.COMPORDER";
            //  DataView dv = new DataView();
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.GetStaffComponentId, "PRSTAFF"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            if (resultArgs.Success)
                return resultArgs.DataSource.Table;
            else
                return null;
        }

        public DataTable FetchHeaderDetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPayrollDateForPayslip, "PRCreate"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                return resultArgs.DataSource.Table;
            }

        }
        public DataTable FetchHeaderDetails(int clsPayrollId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPayrollDateForPayslip, "PRCreate"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, clsPayrollId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                return resultArgs.DataSource.Table;
            }

        }

        /// <summary>
        /// This Method is to fetch the values for the Pay Slip Header Parameters
        /// </summary>
        /// <returns></returns>
        public DataTable FetchValuesForPaySlip(string criteria)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchValuesForPaySlip, "PRPaySlip"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                dataManager.Parameters.Add("CONDITION", criteria);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                return resultArgs.DataSource.Table;
            }
        }
        public DataTable FetchValuesForPaySlip(string criteria, int clsPayrollId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchValuesForPaySlip, "PRPaySlip"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, clsPayrollId);
                dataManager.Parameters.Add("CONDITION", criteria);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                return resultArgs.DataSource.Table;
            }
        }

    }
}
