using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Bosco.DAO.Data;

using Payroll.DAO.Schema;
using Bosco.Utility;
using Bosco.Utility.Common;
using System.Windows.Forms;
using DataGridCustomCellFont;
using System.Drawing;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;


namespace Payroll.Model.UIModel
{
    public class clsPrComponent : SystemBase
    {
        private DataTable rsComp = new DataTable();
        private string[] strValues;
        private DataTable dt = new DataTable();
        private bool bCount = true;
        private string[] strFields;
        ResultArgs resultArgs = null;
        clsModPay objModPay = new clsModPay();
        ApplicationSchema.PRCOMPMONTHDataTable dtCompMonth = null;
        ApplicationSchema.PayrollDataTable Payrollschema = null;
        ApplicationSchema.PRCOMPMONTHDataTable prComponent = null;
        ApplicationSchema.LedgerDataTable dtledger = new ApplicationSchema.LedgerDataTable();
        CommonMember UtilityMember = new CommonMember();

        public clsPrComponent()
        {
            //  createDataSet("SELECT * FROM PRComponent", "PRComponent");
            //  rsComp = getDataSet();
            dtCompMonth = this.AppSchema.PRCOMPMONTH;
            rsComp = (new clsprCompBuild()).PayrollComponent();
            Payrollschema = this.AppSchema.Payroll;
            prComponent = this.AppSchema.PRCOMPMONTH;
        }

        public int FetchRangeComponent(int componentid)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchRangeComponentById))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtCompMonth.COMPONENTIDColumn, componentid);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }

        public ResultArgs FetchRangeComponent()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchRangeComponents))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        
        private DataTable FetchRecord(object query, string PayrollId)
        {
            using (DataManager dataManager = new DataManager(query))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, PayrollId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                    return resultArgs.DataSource.Table;
            }
            return null;
        }
        private bool IsEditable(string sCompName)
        {
            clsModPay objModPay = new clsModPay();
            string Loan = "";
            string type = objModPay.GetValue("PRComponent", "type", "component = '" + sCompName + "'");
            string Link = objModPay.GetValue("PRComponent", "LINKVALUE", "component = '" + sCompName + "'");
            string EQ = objModPay.GetValue("PRComponent", "EQUATION", "component = '" + sCompName + "'");
            if (Link.Trim() != "")
            {
                if (Link.Length >= 6)
                {
                    if (Link.Substring(0, 6).ToUpper() == "LOAN :") return true;
                    else if (Link.ToUpper().Trim() == "BASICPAY")
                        return true;
                    else
                        return false;
                }
            }
            if (type.Trim() == "0" && Link.ToUpper().Trim() == "BASICPAY")
            {
                return true;
            }
            else if (type.Trim() == "0" && Link.Trim() == "" && EQ.Trim() == "")
            {
                return true;
            }
            else if (type.Trim() == "1" && Link.Trim() == "" && EQ.Trim() == "")
            {
                return true;
            }
            else if (type.Trim() == "1" && Loan.Trim() == "LOAN")
            {
                return true;
            }

            return false;
        }
        public void GetPayRoll(string nPayRollId, long nSalaryGroup, DataGrid objGrid, bool bEdit)
        {
            //Filling the Values into the Grid Control
            string sSql = "";
            int i;
            string sStartDate = "";
            string sFinishDate = "";
            bCount = true;
            DataSet tRs = new DataSet();
            DataTable tRsPRDate = new DataTable();
            DataSet tRs1 = new DataSet();
            DataSet rsDate1 = new DataSet();
            DataSet rsDate2 = new DataSet();

            DataTable dtGrp = new DataTable();
            DataTable dtGrp1 = new DataTable();
            DataTable dtPayroll = new DataTable();
            dtPayroll.Clear();
            DataColumnCollection columns;
            columns = dtPayroll.Columns;
            DataRow dr = null;
            //  sSql = "Select to_char(prDate, 'DD/MM/YYYY') as prDate from PRCreate where payrollid = " + nPayRollId;
            try
            {
                //createDataSet(sSql, "PRCreate");
                tRsPRDate = FetchRecord(SQLCommand.Payroll.FetchPayrollDate, nPayRollId);

                DateTime dtTemp = DateTime.Parse(tRsPRDate.Rows[0]["prDate"].ToString(), clsGeneral.DATE_FORMAT);
                sStartDate = dtTemp.ToShortDateString();

                dtTemp = dtTemp.AddMonths(1);
                sFinishDate = dtTemp.AddDays(-1).ToShortDateString();
            }
            catch
            {
                return;
            }
            //FetchPRStaff
            //sSql = "Select prstaffgroup.staffid, IIF(ISNULL(stfpersonal.firstname), '', stfpersonal.firstname) + IIF(ISNULL(' '), '', ' ') + IIF(ISNULL(stfpersonal.lastname), '', stfpersonal.lastname)  as Name,PRStaffGroup.Groupid from PRStaffGroup,stfpersonal,stfService where prstaffgroup.Groupid = 1 and prstaffgroup.payrollid = 17 and stfpersonal.staffid = prstaffgroup.staffid  AND stfService.StaffId = stfPersonal.StaffId AND  DateValue('30/04/2005')  >= stfPersonal.dateofJoin  and (stfPersonal.LeavingDate is null or stfPersonal.LeavingDate >  DateValue('01/04/2005') )  AND (( DateValue('30/04/2005')  BETWEEN stfService.DateofAppointment AND stfService.DateofTermination)  OR (stfService.DateofTermination is null AND  DateValue('30/04/2005')  > stfService.DateofAppointment)) order by PRStaffGroup.GroupId,PRStaffGroup.StaffOrder";
            //sSql = "Select prstaffgroup.staffid,stfpersonal.firstname || ' ' || stfpersonal.lastname as Name,PRStaffGroup.Groupid from PRStaffGroup,stfpersonal,stfService " +
            //    "where prstaffgroup.Groupid = " + nSalaryGroup + " and prstaffgroup.payrollid = " + nPayRollId +
            //    " and stfpersonal.staffid = prstaffgroup.staffid AND stfService.StaffId = stfPersonal.StaffId AND to_date('" +
            //    sFinishDate + "','dd/MM/yyyy') >= stfPersonal.dateofJoin " +
            //    " and (stfPersonal.LeavingDate is null or stfPersonal.LeavingDate > to_date('" +
            //    sStartDate + "','dd/MM/yyyy')) " +
            //    " AND ((to_date('" + sFinishDate + "','dd/MM/yyyy') BETWEEN " +
            //    "stfService.DateofAppointment AND stfService.DateofTermination) " +
            //    " OR (stfService.DateofTermination is null AND to_date('" +
            //    sFinishDate + "','dd/MM/yyyy') > stfService.DateofAppointment)) " +
            //    "order by PRStaffGroup.GroupId,PRStaffGroup.StaffOrder";
            //createDataSet(sSql, "PRstaff");
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPRStaff))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtCompMonth.SALARYGROUPIDColumn, nSalaryGroup);
                dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, nPayRollId);
                dataManager.Parameters.Add(dtCompMonth.STARTDATEColumn, clsGeneral.GetMySQLDateTime(sStartDate, DateDataType.Date));
                dataManager.Parameters.Add(dtCompMonth.ENDDATEColumn, clsGeneral.GetMySQLDateTime(sFinishDate, DateDataType.Date));
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                    dtGrp = resultArgs.DataSource.Table;
            }
            //dtGrp = getDataSet().Tables["PRstaff"];
            if (dtGrp.Rows.Count == 0)
                return;
            //sSql ="Select distinct PRComponent.component,[prcompMonth].[order],PRComponent.componentid  from PRComponent,PRStaff,PRCompMonth,PRSalaryGroup where PRCompMonth.ComponentId = PRComponent.ComponentId AND PRCompMonth.PayRollId = 17 AND PRCompMonth.Payrollid = PRStaff.Payrollid and prstaff.componentid = PRCompMonth.Componentid and PRSalaryGroup.Groupid = PRCompMonth.SalaryGroupid and prCompMonth.SalaryGroupid = 1 ORDER BY PRComponent.componentid ";
            //FetchPProcessStaffGroup
            //sSql = "Select distinct PRComponent.component,prcompMonth.Comp_Order,PRComponent.componentid  from PRComponent," +
            //    "PRStaff,PRCompMonth,PRSalaryGroup where " +
            //    "PRCompMonth.ComponentId = PRComponent.ComponentId AND " +
            //    "PRCompMonth.PayRollId = " + nPayRollId + " AND " +
            //    "PRCompMonth.Payrollid = PRStaff.Payrollid and " +
            //    "prstaff.componentid = PRCompMonth.Componentid and " +
            //    "PRSalaryGroup.Groupid = PRCompMonth.SalaryGroupid and " +
            //    "prCompMonth.SalaryGroupid = " + dtGrp.Rows[0][2].ToString() + " ORDER BY PRCOMPMONTH.COMP_ORDER ";
            //createDataSet(sSql, "PRstaffgroup");
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPProcessStaffGroup))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, nPayRollId);
                dataManager.Parameters.Add(dtCompMonth.SALARYGROUPIDColumn, (dtGrp != null && dtGrp.Rows.Count > 0) ? dtGrp.Rows[0][2].ToString() : string.Empty);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                {
                    dtGrp1 = resultArgs.DataSource.Table;
                }
            }
            //dtGrp1 = getDataSet().Tables["PRstaffgroup"];
            objGrid.TableStyles.Clear();
            DataGridTableStyle tStyle = new DataGridTableStyle();

            ThisDataGridTextBoxColumn columnStyle;//DataGridTextBoxColumn columnStyle;
            strFields = new string[dtGrp1.Rows.Count + 1];
            if (dtGrp.Rows.Count > 0) //creating dynamic data table to view the staff processed component details according to count..
            {
                for (int j = 0; j < dtGrp1.Rows.Count + 1; j++)
                {
                    columnStyle = new ThisDataGridTextBoxColumn();//DataGridTextBoxColumn();
                    if (j == dtGrp1.Rows.Count)
                    {
                        string Head = "StaffId";
                        strFields[j] = Head;
                        columns.Add(Head, typeof(Int32));
                        columnStyle.MappingName = Head;
                        columnStyle.HeaderText = Head;
                        columnStyle.ReadOnly = true;
                        columnStyle.Width = 0;
                        columnStyle.NullText = "";
                    }
                    else
                    {
                        string Head = dtGrp1.Rows[j][0].ToString();
                        strFields[j] = Head;
                        columns.Add(Head, typeof(System.String)); // valuetype
                        columnStyle.MappingName = Head;
                        columnStyle.HeaderText = Head;
                        columnStyle.Width = 100;

                        /*when the iseditable is equals to 0 - Editable
                         *when the iseditable is equals to 1 - Not editable
                         * By pragasam
                         */

                        if (Convert.ToInt32(dtGrp1.Rows[j]["iseditable"]).Equals(0))
                        {
                            columnStyle.ReadOnly = false;
                            columnStyle.NullText = "0";
                            columnStyle.SetDataGridCellFormat += new DataGridCustomEventHandler(columnStyle_SetDataGridCellFormat);
                        }
                        else
                        {
                            columnStyle.ReadOnly = true;
                            columnStyle.NullText = "";
                        }
                    }
                    tStyle.GridColumnStyles.Add(columnStyle);
                }
                int n = 0;
                for (i = 0; i < dtGrp.Rows.Count; i++)
                {
                    //sSql =" Select distinct PRComponent.description,PRStaff.CompValue,[PRCompMonth].[Order],PRComponent.componentid  from PRComponent,PRStaff,PRCompMonth,PRStaffGroup where PRCompMonth.PayRollId = 17 AND PRCompMonth.ComponentId = PRComponent.ComponentId AND PRCompMonth.Payrollid = PRStaff.Payrollid and PRStaff.staffid = 3 and PRStaff.Staffid = PRStaffGroup.Staffid and PRStaffGroup.Groupid = PRCompMonth.SalaryGroupid and PRStaffGroup.Payrollid = PRStaff.Payrollid and prstaff.componentid = prcomponent.componentid ORDER BY PRComponent.componentid ";
                    // FetchPayrollStaffGroup
                    //sSql = "Select distinct PRComponent.component,PRStaff.CompValue," +
                    //    "PRCompMonth.Comp_Order,PRComponent.componentid  from PRComponent,PRStaff,PRCompMonth," +
                    //    "PRStaffGroup where PRCompMonth.PayRollId = " + nPayRollId + " AND " +
                    //    "PRCompMonth.ComponentId = PRComponent.ComponentId AND " +
                    //    "PRCompMonth.Payrollid = PRStaff.Payrollid and " +
                    //    "PRStaff.staffid = " + dtGrp.Rows[i][0].ToString() + " and " +
                    //    "PRStaff.Staffid = PRStaffGroup.Staffid and " +
                    //    "PRStaffGroup.Groupid = PRCompMonth.SalaryGroupid and " + //PRCompMonth.SalaryGroupid ="+ dtGrp.Rows[i][0].ToString() + "and 
                    //    "PRStaffGroup.Payrollid = PRStaff.Payrollid and " +
                    //    "prstaff.componentid = prcomponent.componentid " +
                    //    "ORDER BY PRCOMPMONTH.COMP_ORDER ";
                    //createDataSet(sSql, "PRstaffgroup");
                    using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPayrollStaffGroup))
                    {
                        dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                        dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, nPayRollId);
                        dataManager.Parameters.Add(dtCompMonth.STAFFIDColumn, (dtGrp != null && dtGrp.Rows.Count > 0) ? dtGrp.Rows[i][0].ToString() : string.Empty);
                        resultArgs = dataManager.FetchData(DataSource.DataTable);
                        if (resultArgs.Success)
                        {
                            dtGrp1 = resultArgs.DataSource.Table;
                        }
                    }
                    //dtGrp1 = getDataSet().Tables["PRstaffgroup"];
                    try
                    {
                        //						if(dtGrp1.Rows.Count == 0 && n==0)
                        //						{
                        //							bCount = false;
                        //							n=n+1;
                        //						}
                        //						else
                        //							n=n+1;
                        if (dr != null) dtPayroll.Rows.Add(dr);
                        dtPayroll.Rows.Add(dtPayroll.NewRow());
                        if (dtGrp1.Rows.Count > 0)
                        {
                            for (int J = 0; J < dtGrp1.Rows.Count + 1; J++)
                            {
                                if (J == dtGrp1.Rows.Count)
                                //if(dtGrp1.Rows[J][0].ToString() == "StaffId")
                                {
                                    dtPayroll.Rows[i]["StaffId"] = dtGrp.Rows[i]["staffid"].ToString();
                                }
                                //else
                                else if (dtGrp1.Rows[J]["component"].ToString() == strFields[J])
                                {
                                    try
                                    {
                                        dtPayroll.Rows[i][J] = DateTime.Parse(dtGrp1.Rows[J]["CompValue"].ToString()).ToShortDateString();
                                        //dtPayroll.Rows[i][J] =  dtGrp1.Rows[J]["CompValue"].ToString();
                                    }
                                    catch
                                    {
                                        dtPayroll.Rows[i][J] = (dtGrp1.Rows[J]["CompValue"].ToString() == "") ? "" : dtGrp1.Rows[J]["CompValue"].ToString();
                                    }
                                }
                            }
                        }
                    }
                    catch { }
                }
            }
            else
            {

                //sSql ="Paset teh Query";
                // FetchPrCompMonthStaffOrder
                //sSql = "Select distinct PRComponent.component,prcompMonth.Comp_Order " +
                //    "from PRComponent,PRCompMonth,PRSalaryGroup where " +
                //    "PRCompMonth.ComponentId = PRComponent.ComponentId AND " +
                //    "PRCompMonth.PayRollId = " + nPayRollId + " AND " +
                //    "PRSalaryGroup.Groupid = PRCompMonth.SalaryGroupid and " +
                //    "prCompMonth.SalaryGroupid = " + dtGrp.Rows[0][0].ToString() +
                //    " ORDER BY PRCompMonth.Comp_Order";
                //createDataSet(sSql, "PRstfgroup");
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPrCompMonthStaffOrder))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, nPayRollId);
                    dataManager.Parameters.Add(dtCompMonth.SALARYGROUPIDColumn, (dtGrp != null && dtGrp.Rows.Count > 0) ? dtGrp.Rows[0][0].ToString() : string.Empty);

                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                        dtGrp1 = resultArgs.DataSource.Table;
                }
                //dtGrp1 = getDataSet().Tables["PRstfgroup"];

                if (dtGrp.Rows.Count > 0)
                {
                    for (int j = 0; j < dtGrp1.Rows.Count; j++)
                    {
                        string Head = dtGrp1.Rows[j][0].ToString();
                        columns.Add(Head, typeof(System.ValueType));
                    }
                    for (i = 0; i < dtGrp.Rows.Count; i++)
                    {
                        //						sSql =" Select distinct PRComponent.description,PRStaff.CompValue,PRCompMonth.Comp_Order,PRComponent.componentid  from PRComponent,PRStaff,PRCompMonth,PRStaffGroup where PRCompMonth.PayRollId = 17 AND PRCompMonth.ComponentId = PRComponent.ComponentId AND PRCompMonth.Payrollid = PRStaff.Payrollid and PRStaff.staffid = 3 and PRStaff.Staffid = PRStaffGroup.Staffid and PRStaffGroup.Groupid = PRCompMonth.SalaryGroupid and PRStaffGroup.Payrollid = PRStaff.Payrollid and prstaff.componentid = prcomponent.componentid ORDER BY PRComponent.componentid ";
                        //						dtGrp1 = ExecuteQuery(sSql);
                        if (dr != null) dtPayroll.Rows.Add(dr);
                        dtPayroll.Rows.Add(dtPayroll.NewRow());
                        for (int J = 0; J < dtGrp1.Rows.Count; J++)
                        {
                            dtPayroll.Rows[i][J] = dtGrp1.Rows[J]["CompValue"].ToString();
                        }
                    }
                }

            }
            if (dtPayroll.Columns.Count > 1)
            {
                for (int l = 0; l < dtPayroll.Rows.Count; l++)
                {
                    if (dtPayroll.Rows[l][dtPayroll.Columns.Count - 1].ToString() == "")
                    {
                        dtPayroll.Rows.RemoveAt(l);
                        l = l - 1;
                    }
                }
            }

            //			if(bCount)
            //			{
            tStyle.AlternatingBackColor = System.Drawing.Color.Lavender;
            tStyle.BackColor = System.Drawing.Color.WhiteSmoke;
            tStyle.ForeColor = System.Drawing.Color.MidnightBlue;
            tStyle.GridLineColor = System.Drawing.Color.Gainsboro;
            tStyle.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(206)))), ((int)(((byte)(252)))));
            tStyle.HeaderForeColor = System.Drawing.Color.Black;
            tStyle.LinkColor = System.Drawing.Color.Teal;
            tStyle.SelectionBackColor = System.Drawing.Color.Teal;
            tStyle.SelectionForeColor = System.Drawing.Color.PaleGreen;

            tStyle.AllowSorting = true;
            objGrid.TableStyles.Add(tStyle);
            if (dtPayroll.Columns.Count > 1)
                objGrid.DataSource = dtPayroll.DefaultView;
            //			}

        }
        public bool EditableComponent(string sCompName, ref int nCompType, ref string sLinkName, int nFieldWidth)
        {
            string sEQ = "";
            string sLinkVal = "";
            bool bReturnStatus = false;
            if (rsComp.Rows.Count > 0)
            {
                DataView dvComp = rsComp.DefaultView;
                dvComp.RowFilter = "Component = " + sCompName;
                if (dvComp.Count != 0)
                {
                    sEQ = dvComp.Table.Rows[0]["EquationId"].ToString() + "";
                    sLinkVal = dvComp.Table.Rows[0]["LnkValue"].ToString() + "";
                    nCompType = Convert.ToInt32(dvComp.Table.Rows[0]["Type"]);
                    sLinkName = dvComp.Table.Rows[0]["LnkValue"].ToString().ToUpper() + "";
                    if (sEQ == "" && (sLinkVal == "" || sLinkVal == "BasicPay" || sLinkVal == "SacleofPay"))
                        bReturnStatus = true;
                    else
                        bReturnStatus = false;
                    if (sEQ != "")
                    {
                        nFieldWidth = Convert.ToInt32(dvComp.Table.Columns["EquationId"].MaxLength.ToString());
                    }
                    else
                    {
                        if (sLinkVal == "BasicPay" || sLinkVal == "SacleofPay") nFieldWidth = 15;
                        else nFieldWidth = 50;
                    }
                    dvComp.RowFilter = "";
                }
            }
            return bReturnStatus;
        }
        public bool SaveAssignedComponent(long nPayRollId, long nGroupId, DataTable objdt)
        {
            //Purpose    : This method is used to Update or Insert into PRCompMonth for the Selected Component
            //Argument   : objGrid as ucGrid
            int i;
            string sDelSQL = "";
            string sSelSQL = "";
            string sUpdateSQL = "";
            DataTable rsCompSave = new DataTable();
            if (clsModPay.ProcessRunning(true, nPayRollId, false))
                return true;
            rsCompSave = FetchPayroll(nPayRollId, nGroupId);
            //sSelSQL = "SELECT * FROM PRCompMonth WHERE PayRollId = " + nPayRollId + " AND SalaryGroupId = " + nGroupId;
            //createDataSet(sSelSQL, "PRCompMonth");
            // rsCompSave = getDataSet();
            //MessageBox.Show(rsCompSave.Tables["prcompmonth"].Rows[0][2].ToString()); //shows the first record's componentid from ucgrid..

            if (rsCompSave.Rows.Count == 0)
                return false;
            if (Convert.ToInt32(rsCompSave.Rows[0][2].ToString()) == 0)//if componentid is zero then, (goes on)(note: code part mostly not executed..)
            {
                for (i = 0; i < rsCompSave.Rows.Count; i++)
                {
                    //sDelSQL = "DELETE FROM prcompmonth WHERE PayRollId = " + nPayRollId + " AND SalaryGroupId = " + nGroupId;//COMPONENTID=" + rsCompSave.Tables["prcompMonth"].Rows[0][2].ToString();
                    //deleteRecord(sDelSQL);
                    using (DataManager dataManager = new DataManager(SQLCommand.Payroll.DeletePayrollCompMonthByGrouId))
                    {
                        dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                        dataManager.Parameters.Add(dtCompMonth.SALARYGROUPIDColumn, nGroupId);
                        dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, nPayRollId);
                        resultArgs = dataManager.UpdateData();
                    }
                }
                rsCompSave = FetchPayroll(nPayRollId, nGroupId);
            }

            for (i = 1; i <= objdt.Rows.Count; i++)  //then update the comp_order field of prcompmonth ( note: using i)table and sort the grid using the comp_order and view..
            {
                //sUpdateSQL = "update prcompmonth set comp_order=" + i + " where PayRollId = " + nPayRollId + " AND SalaryGroupId = " + nGroupId + " AND componentid=" + long.Parse(objdt.Rows[i - 1][0].ToString());
                //updateRecord(sUpdateSQL);
                //sUpdateSQL = "";
                UpdatePayrollCompMonth(i, nPayRollId, nGroupId, long.Parse(objdt.Rows[i - 1][0].ToString()));
            }
            if (clsModPay.ProcessRunning(false, nPayRollId, false))
                return true;
            return true;
        }
        public DataTable FetchPayroll(long payrollId, long groupId)
        {
            DataTable dtTable = null;
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPayrollCompMonthByGroupId, "PRCompMonth"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtCompMonth.SALARYGROUPIDColumn, groupId);
                dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, payrollId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                {
                    dtTable = resultArgs.DataSource.Table;
                }
            }
            return dtTable;
        }
        private bool UpdatePayrollCompMonth(int compOrder, long payrollId, long groupId, long componentId)
        {
            bool Updated = false;
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.UpdatePayrollCompMonthByGroupId))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, payrollId);
                dataManager.Parameters.Add(dtCompMonth.SALARYGROUPIDColumn, groupId);
                dataManager.Parameters.Add(dtCompMonth.COMPONENTIDColumn, componentId);
                dataManager.Parameters.Add(dtCompMonth.COMP_ORDERColumn, compOrder);
                resultArgs = dataManager.UpdateData();
                if (resultArgs.Success)
                {
                    Updated = true;
                }
            }
            return Updated;
        }

        public DataTable CompAllocateQuery(long payrollId, long componentId)
        {
            // bool rowaffected = false;
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.PayrollCompchange))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, payrollId);
                dataManager.Parameters.Add(dtCompMonth.COMPONENTIDColumn, componentId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                    return resultArgs.DataSource.Table;
            }
            return null;
        }
        public DataTable FetchMapComponent(long payrollId, long GroupId)
        {
            // bool rowaffected = false;
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchMapComponent))
            {

                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, payrollId);
                dataManager.Parameters.Add(dtCompMonth.SALARYGROUPIDColumn, GroupId);
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                    return resultArgs.DataSource.Table;
            }
            return null;
        }

        public DataTable GetPayrollcompallocate(long componentId)
        {
            // bool rowaffected = false;
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.PayrollFullCompList))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                // dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, payrollId);
                dataManager.Parameters.Add(dtCompMonth.COMPONENTIDColumn, componentId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                    return resultArgs.DataSource.Table;
            }
            return null;
        }
        public object getPayrollComponentAllocateQry(int iQueryId)
        {
            object sQry = "";
            switch (iQueryId)
            {
                case clsPayrollConstants.PAYROLL_COMPONENT_LIST:
                    sQry = SQLCommand.Payroll.PayrollComponentList;
                    break;
                case clsPayrollConstants.PAYROLL_GETGROUP_LIST:
                    sQry = SQLCommand.Payroll.PayrollGetGroupList;
                    break;
                case clsPayrollConstants.PAYROLL_FULLCOMP_LIST:
                    sQry = SQLCommand.Payroll.PayrollFullCompList;
                    break;
                case clsPayrollConstants.PAYROLL_COMP_INSERT:
                    sQry = SQLCommand.Payroll.PayrollCompInsert;
                    break;
                case clsPayrollConstants.PAYROLL_COMPCHECK_SELECT:
                    sQry = SQLCommand.Payroll.PayrollCompCheckSelect;
                    break;
                case clsPayrollConstants.PAYROLL_COMP_DELETE:
                    sQry = SQLCommand.Payroll.PayrollCompDelete;
                    break;
                case clsPayrollConstants.PAYROLL_COMP_CHANGE:
                    sQry = SQLCommand.Payroll.PayrollCompchange;
                    break;
                case clsPayrollConstants.PAYROLL_FORMULA_FOR_GROUP:
                    sQry = SQLCommand.Payroll.PayrollFormulaForGroup;
                    break;
                case clsPayrollConstants.PAYROLL_COMP_STAFFID:
                    sQry = SQLCommand.Payroll.PayrollCompStaffID;
                    break;
                case clsPayrollConstants.PAYROLL_COMP_NAME:
                    sQry = SQLCommand.Payroll.PayrollCompName;
                    break;
                case clsPayrollConstants.PAYROLL_PROCESS_DELETE:
                    sQry = SQLCommand.Payroll.PayrollProcessDelete;
                    break;
                case clsPayrollConstants.PAYROLL_PROCESS_CHECK:
                    sQry = SQLCommand.Payroll.PayrollProcessCheck;
                    break;
                case clsPayrollConstants.PAYROLL_INSERT_PROCESS:
                    sQry = SQLCommand.Payroll.PayrollInsertProcess;
                    break;
                case clsPayrollConstants.PAYROLL_COMPID_RETURN:
                    sQry = SQLCommand.Payroll.PayrollCompIdReturn;
                    break;
            }
            return sQry;
        }
        public DataTable FetchcompCheckSelect(object query, long payId, long compId, int salaryId)
        {
            using (DataManager dataManager = new DataManager(query))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, payId);
                dataManager.Parameters.Add(dtCompMonth.COMPONENTIDColumn, compId);
                dataManager.Parameters.Add(dtCompMonth.SALARYGROUPIDColumn, salaryId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                    return resultArgs.DataSource.Table;
            }
            return null;
        }
        //To save allocated component while allocating components to one or more groups..
        public void SaveGroupComponent(long nPayRollId, long nCompId, DataTable aComp, string sGroupId, int compOrder)
        {
            /*'aComp -> contains the Filed Name, Filed  Value of a selected Component in 'PRCompMonth'
                        Array(i)(0) - Field Name, Array(i)(1) - Field Value
                        sGroupId -> is a Selected Group Id concatinated with ',' separator
                        an id with suffixed character 'D' is for Delete the particular Group  from 'PRCompMonth'
                        an id with suffixed character 'N' is for Add a New Group to 'PRCompMonth' */
            string sSql = "";
            string[] aGrpId = null;
            dt = aComp;
            dt.ColumnChanging += new DataColumnChangeEventHandler(dt_ColumnChanging);
            DataTable rsGrpComp = new DataTable();
            try
            {

                if (clsModPay.ProcessRunning(true, nPayRollId, false))
                    return;
                //sSql = "SELECT * FROM PRCompMonth WHERE PayRollId = " + nPayRollId + " and ComponentId = " + nCompId;
                //createDataSet(sSql, "PRCompMonth");
                //rsGrpComp = getDataSet();
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPRCompMonthByComponentId, "PRCompMonth"))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, nPayRollId);
                    dataManager.Parameters.Add(dtCompMonth.COMPONENTIDColumn, nCompId);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                        rsGrpComp = resultArgs.DataSource.Table;
                }
                aGrpId = sGroupId.Split(',');

                DataView dvGrpComp = new DataView(rsGrpComp);
                dvGrpComp.RowFilter = "payrollid = " + nPayRollId + " and componentid = " + nCompId + " and salarygroupid = " + Convert.ToInt32(sGroupId);
                if (dvGrpComp.Count == 0)
                {

                    using (DataManager dataManager = new DataManager(SQLCommand.Payroll.PrcompMonthAdd))
                    {
                        dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                        //strQuery = strQuery.Replace("<SALARYGROUPID>", sGroupId);
                        dataManager.Parameters.Add(dtCompMonth.SALARYGROUPIDColumn, sGroupId);
                        //strQuery = strQuery.Replace("<PAYROLLID>", clsGeneral.PAYROLL_ID.ToString());
                        dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, clsGeneral.PAYROLL_ID.ToString());
                        //strQuery = strQuery.Replace("<COMPONENTID>", nCompId.ToString());
                        dataManager.Parameters.Add(dtCompMonth.COMPONENTIDColumn, nCompId);
                        //strQuery = strQuery.Replace("<TYPE>", dt.Rows[0][0].ToString());
                        dataManager.Parameters.Add(dtCompMonth.TYPEColumn, dt.Rows[0][0].ToString());
                        //strQuery = strQuery.Replace("<DEFVALUE>", dt.Rows[0][1].ToString());
                        dataManager.Parameters.Add(dtCompMonth.DEFVALUEColumn, dt.Rows[0][1].ToString());
                        if (dt.Rows[0][2].ToString() != "")
                            //strQuery = strQuery.Replace("<LNKVALUE>", dt.Rows[0][2].ToString());
                            dataManager.Parameters.Add(dtCompMonth.LNKVALUEColumn, dt.Rows[0][2].ToString());
                        else
                            //strQuery = strQuery.Replace("<LNKVALUE>", "");
                            dataManager.Parameters.Add(dtCompMonth.LNKVALUEColumn, string.Empty);
                        if (dt.Rows[0][3].ToString() != "")
                            dataManager.Parameters.Add(dtCompMonth.EQUATIONColumn, dt.Rows[0][3].ToString());
                        // strQuery = strQuery.Replace("<EQUATION>", dt.Rows[0][3].ToString());
                        else
                            // strQuery = strQuery.Replace("<EQUATION>", "");
                            dataManager.Parameters.Add(dtCompMonth.EQUATIONColumn, string.Empty);
                        if (dt.Rows[0][4].ToString() != "")
                            //strQuery = strQuery.Replace("<EQUATIONID>", dt.Rows[0][4].ToString());
                            dataManager.Parameters.Add(dtCompMonth.EQUATIONIDColumn, dt.Rows[0][4].ToString());
                        else
                            //strQuery = strQuery.Replace("<EQUATIONID>", "");
                            dataManager.Parameters.Add(dtCompMonth.EQUATIONIDColumn, string.Empty);

                        //strQuery = strQuery.Replace("<MAXSLAB>", dt.Rows[0][5].ToString());
                        dataManager.Parameters.Add(dtCompMonth.MAXSLABColumn, dt.Rows[0][5].ToString());
                        //strQuery = strQuery.Replace("<COMP_ORDER>", "0");
                        dataManager.Parameters.Add(dtCompMonth.COMP_ORDERColumn, compOrder );
                        //strQuery = strQuery.Replace("<COMPROUND>", dt.Rows[0][6].ToString());
                        dataManager.Parameters.Add(dtCompMonth.COMPROUNDColumn, dt.Rows[0][6].ToString());
                        //strQuery = strQuery.Replace("<IFCONDITION>", dt.Rows[0][7].ToString());
                        dataManager.Parameters.Add(dtCompMonth.IFCONDITIONColumn, NumberSet.ToInteger(dt.Rows[0][7].ToString()));

                        resultArgs = dataManager.UpdateData();
                        if (!resultArgs.Success)
                            return;
                    }
                    // insertRecord(strQuery);
                }
                dvGrpComp.RowFilter = "";
                if (clsModPay.ProcessRunning(false, nPayRollId, false))
                    return;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString(), "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }

        //To Save the Component Group for a selected component in 'frmPRCompAssign'
        public void SaveGroupComponent(long nPayRollId, long nCompId, string[] aComp, string sGroupId)
        {
            /*'aComp -> contains the Filed Name, Filed  Value of a selected Component in 'PRCompMonth'
                        Array(i)(0) - Field Name, Array(i)(1) - Field Value
                        sGroupId -> is a Selected Group Id concatinated with ',' separator
                        an id with suffixed character 'D' is for Delete the particular Group  from 'PRCompMonth'
                        an id with suffixed character 'N' is for Add a New Group to 'PRCompMonth' */
            int i;
            long nGrpId;
            string sSql = "";
            string sDelSQL = "";
            //string sFldName="";
            //string sFldVal="";
            string[] aGrpId;

            DataTable rsGrpComp = new DataTable();
            try
            {

                if (clsModPay.ProcessRunning(true, nPayRollId, false))
                    return;
                //sSql = "SELECT * FROM PRCompMonth WHERE PayRollId = " + nPayRollId + " and ComponentId = " + nCompId;
                //createDataSet(sSql, "PRCompMonth");
                //rsGrpComp = getDataSet();
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPRCompMonthByComponentId, "PRCompMonth"))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, nPayRollId);
                    dataManager.Parameters.Add(dtCompMonth.COMPONENTIDColumn, nCompId);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                        rsGrpComp = resultArgs.DataSource.Table;
                }
                aGrpId = sGroupId.Split(',');

                for (i = 0; i < aGrpId.Length; i++)
                {
                    nGrpId = long.Parse(aGrpId[i].ToString());

                    if (aGrpId[i].IndexOf("D", 1) > 0) //Delete the Component from the Group
                    {
                        //sDelSQL = "DELETE FROM PRCompMonth WHERE PayRollId = " + nPayRollId + " and ComponentId = " + nCompId + " and SalaryGroupId = " + nGrpId;
                        //deleteRecord(sDelSQL);
                        using (DataManager dataManager = new DataManager(SQLCommand.Payroll.PRCompMonthDeleteByCompGroup, "PRCompMonth"))
                        {
                            dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                            dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, nPayRollId);
                            dataManager.Parameters.Add(dtCompMonth.COMPONENTIDColumn, nCompId);
                            dataManager.Parameters.Add(dtCompMonth.SALARYGROUPIDColumn, nGrpId);
                            resultArgs = dataManager.UpdateData();
                            if (!resultArgs.Success)
                                return;
                        }
                    }
                    else if (aGrpId[i].IndexOf("N", 1) > 0) //Add New component to a particular Group
                    {
                        DataView dvGrpComp = new DataView(rsGrpComp);

                        dvGrpComp.RowFilter = "payrollid = " + nPayRollId + " and componentid = " + nCompId + " and salarygroupid = " + nGrpId;

                        if (dvGrpComp.Count == 0)
                        {

                            for (int k = 0; k < aComp.Length; k++)
                            {
                                strValues[k] = aComp[k][1].ToString();
                            }
                            //string strQuery = "INSERT INTO PRCOMPMONTH(SALARYGROUPID,PAYROLLID,COMPONENTID,TYPE,DEFVALUE,LNKVALUE,EQUATION,EQUATIONID,MAXSLAB,COMP_ORDER,COMPROUND,IFCONDITION)" +
                            //    " VALUES('<SALARYGROUPID>','<PAYROLLID>','<COMPONENTID>','<TYPE>','<DEFVALUE>','<LNKVALUE>','<EQUATION>','<EQUATIONID>','<MAXSLAB>','<COMP_ORDER>','<COMPROUND>','<IFCONDITION>')";
                            //strQuery = strQuery.Replace("<SALARYGROUPID>", nGrpId.ToString());
                            //strQuery = strQuery.Replace("<PAYROLLID>", strValues[0]);
                            //strQuery = strQuery.Replace("<COMPONENTID>", strValues[1]);
                            //strQuery = strQuery.Replace("<TYPE>", strValues[2]);
                            //strQuery = strQuery.Replace("<DEFVALUE>", strValues[3]);
                            //strQuery = strQuery.Replace("<LNKVALUE>", strValues[4]);
                            //strQuery = strQuery.Replace("<EQUATION>", strValues[5]);
                            //strQuery = strQuery.Replace("<EQUATIONID>", strValues[6]);
                            //strQuery = strQuery.Replace("<MAXSLAB>", strValues[7]);
                            //strQuery = strQuery.Replace("<COMP_ORDER>", strValues[8]);
                            //strQuery = strQuery.Replace("<COMPROUND>", strValues[9]);
                            //strQuery = strQuery.Replace("<IFCONDITION>", strValues[10]);
                            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.PrcompMonthAdd))
                            {
                                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                                //strQuery = strQuery.Replace("<SALARYGROUPID>", nGrpId.ToString());
                                dataManager.Parameters.Add(dtCompMonth.SALARYGROUPIDColumn, nGrpId.ToString());
                                //strQuery = strQuery.Replace("<PAYROLLID>", strValues[0]);
                                dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, strValues[0]);
                                //strQuery = strQuery.Replace("<COMPONENTID>", strValues[1]);
                                dataManager.Parameters.Add(dtCompMonth.COMPONENTIDColumn, strValues[1]);
                                //strQuery = strQuery.Replace("<TYPE>", strValues[2]);
                                dataManager.Parameters.Add(dtCompMonth.TYPEColumn, strValues[2]);
                                //strQuery = strQuery.Replace("<DEFVALUE>", strValues[3]);
                                dataManager.Parameters.Add(dtCompMonth.DEFVALUEColumn, strValues[3]);
                                //strQuery = strQuery.Replace("<LNKVALUE>", strValues[4]);
                                dataManager.Parameters.Add(dtCompMonth.LNKVALUEColumn, strValues[4]);
                                //strQuery = strQuery.Replace("<EQUATION>", strValues[5]);
                                dataManager.Parameters.Add(dtCompMonth.EQUATIONColumn, strValues[5]);
                                //strQuery = strQuery.Replace("<EQUATIONID>", strValues[6]);
                                dataManager.Parameters.Add(dtCompMonth.EQUATIONIDColumn, strValues[6]);
                                //strQuery = strQuery.Replace("<MAXSLAB>", strValues[7]);
                                dataManager.Parameters.Add(dtCompMonth.MAXSLABColumn, strValues[7]);
                                //strQuery = strQuery.Replace("<COMP_ORDER>", strValues[8]);
                                dataManager.Parameters.Add(dtCompMonth.COMP_ORDERColumn, strValues[8]);
                                //strQuery = strQuery.Replace("<COMPROUND>", strValues[9]);
                                dataManager.Parameters.Add(dtCompMonth.COMPROUNDColumn, strValues[9]);
                                //strQuery = strQuery.Replace("<IFCONDITION>", strValues[10]);
                                dataManager.Parameters.Add(dtCompMonth.IFCONDITIONColumn, strValues[10]);
                                resultArgs = dataManager.UpdateData();
                                if (!resultArgs.Success)
                                    return;
                            }
                            // insertRecord(strQuery);
                        }

                        dvGrpComp.RowFilter = "";
                    }
                }
                if (clsModPay.ProcessRunning(false, nPayRollId, false))
                    return;
            }
            catch
            {
                return;
                //e.Message.ToString();
            }
        }
        private void dt_ColumnChanging(object sender, DataColumnChangeEventArgs e)
        {

        }
        public void FillProcessComponent(DataGrid objGrid, long nPayRollId)
        {
            string sSql = "";
            DataTable tRs = new DataTable();

            //sSql = "SELECT DISTINCT PRSalaryGroup.GroupId,PRSalaryGroup.GroupName," +
            //    "PRCompMonth.ComponentId,PRComponent.Component FROM " +
            //    "PRSalaryGroup,PRCompMonth,PRComponent WHERE PRCompMonth.PayRollId = " + nPayRollId + " AND PRCompMonth.SalaryGroupId = PRSalaryGroup.GroupId " +
            //    "AND PRCompMonth.ComponentId = PRComponent.ComponentId ORDER BY PRSalaryGroup.GroupName,PRComponent.Component";
            //createDataSet(sSql, "PR");
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPrReProcess, "PR"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, nPayRollId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                {
                    tRs = resultArgs.DataSource.Table;
                }
            }
            //tRs = getDataSet();

            // Now create the column styles within the table style.
            string GrName = "";
            DataTable DtSample = new DataTable();
            DataTable DtSamHeading = GetCrossTabTable();
            DtSample = tRs;
            DataRow dr = null;
            DataRow dr1 = null;
            int K = -1;
            for (int J = 0; J < DtSample.Rows.Count; J++)
            {
                //				dr1 = DtSamHeading.NewRow();
                //				dr1["groupname"] = " ";
                //				DtSamHeading.Rows.Add(dr1);
                //				dr1 = null;
                GrName = DtSample.Rows[J][1].ToString();
                if (dr != null) DtSamHeading.Rows.Add(dr);
                dr = DtSamHeading.NewRow();
                dr["groupname"] = DtSample.Rows[J][1].ToString() + " ";
                dr["groupid"] = Convert.ToInt32(DtSample.Rows[J][0].ToString());
                dr["Identification"] = "G";
                for (int i = J; i < DtSample.Rows.Count; i++)
                {
                    if (GrName == DtSample.Rows[i][1].ToString())
                    {
                        if (dr != null) DtSamHeading.Rows.Add(dr);
                        dr = DtSamHeading.NewRow();
                        dr["groupname"] = DtSample.Rows[i][3].ToString();
                        dr["groupid"] = Convert.ToInt32(DtSample.Rows[i][2].ToString());
                        dr["Identification"] = "C";
                        K = K + 1;
                    }
                    J = K;
                }

            }
            if (dr != null) DtSamHeading.Rows.Add(dr);

            DataGridTableStyle tStyle = new DataGridTableStyle();
            ThisDataGridTextBoxColumn column = new ThisDataGridTextBoxColumn();

            tStyle.MappingName = DtSamHeading.TableName;

            DataGridBoolColumn boolColumn;

            boolColumn = new DataGridBoolColumn();
            boolColumn.MappingName = "Check";
            boolColumn.HeaderText = "Select";
            boolColumn.Width = 90;
            tStyle.GridColumnStyles.Add(boolColumn);

            column = new ThisDataGridTextBoxColumn();
            column.MappingName = "groupname";
            column.HeaderText = "GroupName";
            column.Width = 250;
            column.SetDataGridCellFormat += new DataGridCustomEventHandler(column_SetDataGridCellFormat);
            column.ReadOnly = true;
            tStyle.GridColumnStyles.Add(column);

            column = new ThisDataGridTextBoxColumn();
            column.MappingName = "groupid";
            column.HeaderText = "Groupid";
            column.Width = 0;
            //column.SetDataGridCellFormat +=new DataGridCustomEventHandler(column_SetDataGridCellFormat);
            column.ReadOnly = true;
            tStyle.GridColumnStyles.Add(column);

            column = new ThisDataGridTextBoxColumn();
            column.MappingName = "Identification";
            column.HeaderText = "Identification";
            column.Width = 0;
            column.SetDataGridCellFormat += new DataGridCustomEventHandler(column_SetDataGridCellFormat);
            tStyle.GridColumnStyles.Add(column);
            //Set design properties for the table style
            tStyle.AlternatingBackColor = System.Drawing.Color.Lavender;
            tStyle.BackColor = System.Drawing.Color.WhiteSmoke;
            tStyle.ForeColor = System.Drawing.Color.MidnightBlue;
            tStyle.GridLineColor = System.Drawing.Color.Gainsboro;
            tStyle.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(206)))), ((int)(((byte)(252)))));
            tStyle.HeaderForeColor = System.Drawing.Color.Black;
            tStyle.LinkColor = System.Drawing.Color.Teal;
            tStyle.SelectionBackColor = System.Drawing.Color.Teal;
            tStyle.SelectionForeColor = System.Drawing.Color.PaleGreen;
            tStyle.HeaderFont = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);

            tStyle.AllowSorting = false;
            objGrid.TableStyles.Add(tStyle);
            objGrid.DataSource = DtSamHeading.DefaultView;
            DataView dv = (DataView)objGrid.DataSource;
        }

        public DataTable GetData(long nPayRollId)
        {
            string sSql = "";
            DataTable tRs = new DataTable();
            DataTable DtSamHeading = GetCrossTabTable();
            //sSql = "SELECT DISTINCT PRSalaryGroup.GroupId,PRSalaryGroup.GroupName," +
            //    "PRCompMonth.ComponentId,PRComponent.Component FROM " +
            //    "PRSalaryGroup,PRCompMonth,PRComponent WHERE PRCompMonth.PayRollId = " + nPayRollId + " AND PRCompMonth.SalaryGroupId = PRSalaryGroup.GroupId " +
            //    "AND PRCompMonth.ComponentId = PRComponent.ComponentId ORDER BY PRSalaryGroup.GroupName,PRComponent.Component";
            //createDataSet(sSql, "PR");
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPrReProcess, "PR"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, nPayRollId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                {
                    tRs = resultArgs.DataSource.Table;
                }
                string GrName = "";
                DataTable DtSample = new DataTable();
                DtSamHeading = GetCrossTabTable();
                DtSample = tRs;
                DataRow dr = null;
                DataRow dr1 = null;
                int K = -1;
                for (int J = 0; J < DtSample.Rows.Count; J++)
                {
                    //				dr1 = DtSamHeading.NewRow();
                    //				dr1["groupname"] = " ";
                    //				DtSamHeading.Rows.Add(dr1);
                    //				dr1 = null;
                    GrName = DtSample.Rows[J][1].ToString();
                    if (dr != null) DtSamHeading.Rows.Add(dr);
                    dr = DtSamHeading.NewRow();
                    dr["groupname"] = DtSample.Rows[J][1].ToString() + " ";
                    dr["groupid"] = Convert.ToInt32(DtSample.Rows[J][0].ToString());
                    dr["Identification"] = "G";
                    for (int i = J; i < DtSample.Rows.Count; i++)
                    {
                        if (GrName == DtSample.Rows[i][1].ToString())
                        {
                            if (dr != null) DtSamHeading.Rows.Add(dr);
                            dr = DtSamHeading.NewRow();
                            dr["groupname"] = DtSample.Rows[i][3].ToString();
                            dr["groupid"] = Convert.ToInt32(DtSample.Rows[i][2].ToString());
                            dr["Identification"] = "C";
                            K = K + 1;
                        }
                        J = K;
                    }

                }
            }
            return DtSamHeading;
        }
        private DataTable GetCrossTabTable()
        {
            //Get the Headings alone.
            DataTable DtReturn = new DataTable();
            DataColumnCollection columns;
            columns = DtReturn.Columns;
            columns.Add("Check", typeof(bool));
            columns.Add("groupname", typeof(System.String));
            columns.Add("groupid", typeof(System.Int32));
            columns.Add("Identification", typeof(System.String));
            return DtReturn;
        }
        private void column_SetDataGridCellFormat(object sender, DataGridCustomEventArgs e)
        {
            string str = e.cellData.Substring(e.cellData.Length - 1);
            if (str == " ")
            {
                e.backBrush = System.Drawing.Brushes.AliceBlue;
                e.foreBrush = System.Drawing.Brushes.DarkRed;
                e.g.FillRectangle(e.backBrush, e.bounds.X, e.bounds.Y, e.bounds.Width, e.bounds.Height);
                System.Drawing.Font font = new Font(System.Drawing.FontFamily.GenericSansSerif, (float)12, FontStyle.Bold);
                e.g.DrawString(e.cellData, font, e.foreBrush, e.bounds.X, e.bounds.Y);
                e.isRendered = true;
            }
            else
            {
                e.isRendered = false;
            }
        }

        private void columnStyle_SetDataGridCellFormat(object sender, DataGridCustomEventArgs e)
        {
            e.backBrush = System.Drawing.Brushes.WhiteSmoke;
            e.foreBrush = System.Drawing.Brushes.DarkGreen;
            e.g.FillRectangle(e.backBrush, e.bounds.X, e.bounds.Y, e.bounds.Width, e.bounds.Height);
            System.Drawing.Font font = new Font(System.Drawing.FontFamily.GenericSansSerif, (float)8, FontStyle.Regular);
            e.g.DrawString(e.cellData, font, e.foreBrush, e.bounds.X, e.bounds.Y);
            e.isRendered = true;
        }
        public bool DeleteComponent(long nCompId)
        {
            object sDelSQL = "";
            if (!ComponentIsRelated(Convert.ToInt32(nCompId)))
            {
                //sDelSQL = "DELETE FROM PRComponent WHERE ComponentId = " + nCompId;
                sDelSQL = SQLCommand.Payroll.DeletePrComponent;
                return insertRecord(sDelSQL, nCompId);
            }
            else
            {
                //MessageBox.Show("Can't Delete. It is already used.","MedSysB",MessageBoxButtons.OK,MessageBoxIcon.Information);
                return false;
            }

        }
        private bool insertRecord(object squery, long nCompId)
        {
            using (DataManager dataManager = new DataManager(squery))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtCompMonth.COMPONENTIDColumn, nCompId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs.Success;
        }
        public bool ComponentIsRelated(int nCompId)
        {
            string sWhere = "";
            if (CheckNoOfAbsent(nCompId))
            {
                return true;
            }
            if (objModPay.CheckDuplicate("PRCompMonth", "ComponentId = " + nCompId))
            {
                return true;
            }
            //  sWhere = "INSTR(EQUATIONID,'<" + nCompId + ">',1) > 0";
            sWhere = "FIND_IN_SET(EQUATIONID,'" + nCompId + "') > 0";
            if (objModPay.CheckDuplicate("PRComponent", sWhere))
            {
                return true;
            }
            return false;
        }
        public bool CheckNoOfAbsent(int nCompId)
        {
            DataTable RsCom = new DataTable();
            //createDataSet("Select component from PRComponent Where Componentid=" + nCompId + " and component='NoOfAbsent'", "PRComponent");
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchNoOfAbsents, "PRComponent"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtCompMonth.COMPONENTIDColumn, nCompId);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            if (resultArgs.Success)
                RsCom = resultArgs.DataSource.Table;
            //RsCom = getDataSet();
            if (RsCom.Rows.Count > 0)
                return true;
            else
                return false;
        }


        public bool VerifyCurrentPayrollDependency(int componentId, int payrollId)
        {
            clsPayrollComponent objPayroll = new clsPayrollComponent();
            object strQuery = objPayroll.getPayrollComponentQuery(clsPayrollConstants.PAYROLL_EDIT_VERIFY_COMP_LINK);

            //strQuery = strQuery.Replace("<PAYROLLID>",payrollId.ToString());
            //strQuery = strQuery.Replace("<COMPONENTID>",componentId.ToString());
            using (DataManager dataManager = new DataManager(strQuery))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(Payrollschema.PAYROLLIDColumn, payrollId);
                dataManager.Parameters.Add(prComponent.COMPONENTIDColumn, componentId);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            string componentMapped = resultArgs.DataSource.Sclar.ToString;
            if (componentMapped == "" || componentMapped == "0") return false;
            else return true;

        }

        public void GetAcMeERPPayRoll(string nPayRollId, long nSalaryGroup, DevExpress.XtraGrid.GridControl objGrid, bool bEdit)
        {
            //Filling the Values into the Grid Control
            string sSql = "";
            int i;
            string sStartDate = "";
            string sFinishDate = "";
            bCount = true;
            DataSet tRs = new DataSet();
            DataTable tRsPRDate = new DataTable();
            DataSet tRs1 = new DataSet();
            DataSet rsDate1 = new DataSet();
            DataSet rsDate2 = new DataSet();

            DataTable dtGrp = new DataTable();
            DataTable dtGrp1 = new DataTable();
            DataTable dtPayroll = new DataTable();
            dtPayroll.Clear();
            DataColumnCollection columns;
            columns = dtPayroll.Columns;
            DataRow dr = null;
            //  sSql = "Select to_char(prDate, 'DD/MM/YYYY') as prDate from PRCreate where payrollid = " + nPayRollId;
            try
            {
                //createDataSet(sSql, "PRCreate");
                tRsPRDate = FetchRecord(SQLCommand.Payroll.FetchPayrollDate, nPayRollId);

                DateTime dtTemp = DateTime.Parse(tRsPRDate.Rows[0]["prDate"].ToString(), clsGeneral.DATE_FORMAT);
                sStartDate = dtTemp.ToShortDateString();

                dtTemp = dtTemp.AddMonths(1);
                sFinishDate = dtTemp.AddDays(-1).ToShortDateString();
            }
            catch
            {
                return;
            }
            //FetchPRStaff
            //sSql = "Select prstaffgroup.staffid, IIF(ISNULL(stfpersonal.firstname), '', stfpersonal.firstname) + IIF(ISNULL(' '), '', ' ') + IIF(ISNULL(stfpersonal.lastname), '', stfpersonal.lastname)  as Name,PRStaffGroup.Groupid from PRStaffGroup,stfpersonal,stfService where prstaffgroup.Groupid = 1 and prstaffgroup.payrollid = 17 and stfpersonal.staffid = prstaffgroup.staffid  AND stfService.StaffId = stfPersonal.StaffId AND  DateValue('30/04/2005')  >= stfPersonal.dateofJoin  and (stfPersonal.LeavingDate is null or stfPersonal.LeavingDate >  DateValue('01/04/2005') )  AND (( DateValue('30/04/2005')  BETWEEN stfService.DateofAppointment AND stfService.DateofTermination)  OR (stfService.DateofTermination is null AND  DateValue('30/04/2005')  > stfService.DateofAppointment)) order by PRStaffGroup.GroupId,PRStaffGroup.StaffOrder";
            //sSql = "Select prstaffgroup.staffid,stfpersonal.firstname || ' ' || stfpersonal.lastname as Name,PRStaffGroup.Groupid from PRStaffGroup,stfpersonal,stfService " +
            //    "where prstaffgroup.Groupid = " + nSalaryGroup + " and prstaffgroup.payrollid = " + nPayRollId +
            //    " and stfpersonal.staffid = prstaffgroup.staffid AND stfService.StaffId = stfPersonal.StaffId AND to_date('" +
            //    sFinishDate + "','dd/MM/yyyy') >= stfPersonal.dateofJoin " +
            //    " and (stfPersonal.LeavingDate is null or stfPersonal.LeavingDate > to_date('" +
            //    sStartDate + "','dd/MM/yyyy')) " +
            //    " AND ((to_date('" + sFinishDate + "','dd/MM/yyyy') BETWEEN " +
            //    "stfService.DateofAppointment AND stfService.DateofTermination) " +
            //    " OR (stfService.DateofTermination is null AND to_date('" +
            //    sFinishDate + "','dd/MM/yyyy') > stfService.DateofAppointment)) " +
            //    "order by PRStaffGroup.GroupId,PRStaffGroup.StaffOrder";
            //createDataSet(sSql, "PRstaff");
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPRStaff))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtCompMonth.SALARYGROUPIDColumn, nSalaryGroup);
                dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, nPayRollId);
                dataManager.Parameters.Add(dtCompMonth.STARTDATEColumn, clsGeneral.GetMySQLDateTime(sStartDate, DateDataType.Date));
                dataManager.Parameters.Add(dtCompMonth.ENDDATEColumn, clsGeneral.GetMySQLDateTime(sFinishDate, DateDataType.Date));
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                    dtGrp = resultArgs.DataSource.Table;
            }
            //dtGrp = getDataSet().Tables["PRstaff"];
            if (dtGrp.Rows.Count == 0)
                return;
            //sSql ="Select distinct PRComponent.component,[prcompMonth].[order],PRComponent.componentid  from PRComponent,PRStaff,PRCompMonth,PRSalaryGroup where PRCompMonth.ComponentId = PRComponent.ComponentId AND PRCompMonth.PayRollId = 17 AND PRCompMonth.Payrollid = PRStaff.Payrollid and prstaff.componentid = PRCompMonth.Componentid and PRSalaryGroup.Groupid = PRCompMonth.SalaryGroupid and prCompMonth.SalaryGroupid = 1 ORDER BY PRComponent.componentid ";
            //FetchPProcessStaffGroup
            //sSql = "Select distinct PRComponent.component,prcompMonth.Comp_Order,PRComponent.componentid  from PRComponent," +
            //    "PRStaff,PRCompMonth,PRSalaryGroup where " +
            //    "PRCompMonth.ComponentId = PRComponent.ComponentId AND " +
            //    "PRCompMonth.PayRollId = " + nPayRollId + " AND " +
            //    "PRCompMonth.Payrollid = PRStaff.Payrollid and " +
            //    "prstaff.componentid = PRCompMonth.Componentid and " +
            //    "PRSalaryGroup.Groupid = PRCompMonth.SalaryGroupid and " +
            //    "prCompMonth.SalaryGroupid = " + dtGrp.Rows[0][2].ToString() + " ORDER BY PRCOMPMONTH.COMP_ORDER ";
            //createDataSet(sSql, "PRstaffgroup");
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPProcessStaffGroup))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, nPayRollId);
                dataManager.Parameters.Add(dtCompMonth.SALARYGROUPIDColumn, (dtGrp != null && dtGrp.Rows.Count > 0) ? dtGrp.Rows[0][2].ToString() : string.Empty);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                {
                    dtGrp1 = resultArgs.DataSource.Table;
                }
            }
            //dtGrp1 = getDataSet().Tables["PRstaffgroup"];
            //objGrid.ta.Clear();
            DataGridTableStyle tStyle = new DataGridTableStyle();

            ThisDataGridTextBoxColumn columnStyle;//DataGridTextBoxColumn columnStyle;
            strFields = new string[dtGrp1.Rows.Count + 1];
            if (dtGrp.Rows.Count > 0) //creating dynamic data table to view the staff processed component details according to count..
            {
                for (int j = 0; j < dtGrp1.Rows.Count + 1; j++)
                {
                    columnStyle = new ThisDataGridTextBoxColumn();//DataGridTextBoxColumn();
                    if (j == dtGrp1.Rows.Count)
                    {
                        string Head = "StaffId";
                        strFields[j] = Head;
                        columns.Add(Head, typeof(Int32));
                        columnStyle.MappingName = Head;
                        columnStyle.HeaderText = Head;
                        columnStyle.ReadOnly = true;
                        columnStyle.Width = 0;
                        columnStyle.NullText = "";
                    }
                    else
                    {
                        string Head = dtGrp1.Rows[j][0].ToString();
                        strFields[j] = Head;
                        columns.Add(Head, typeof(System.String)); // valuetype
                        columnStyle.MappingName = Head;
                        columnStyle.HeaderText = Head;
                        columnStyle.Width = 100;

                        /*when the iseditable is equals to 0 - Editable
                         *when the iseditable is equals to 1 - Not editable
                         * By pragasam
                         */

                        if (Convert.ToInt32(dtGrp1.Rows[j]["iseditable"]).Equals(0))
                        {
                            columnStyle.ReadOnly = false;
                            columnStyle.NullText = "0";
                            columnStyle.SetDataGridCellFormat += new DataGridCustomEventHandler(columnStyle_SetDataGridCellFormat);
                        }
                        else
                        {
                            columnStyle.ReadOnly = true;
                            columnStyle.NullText = "";
                        }
                    }
                    tStyle.GridColumnStyles.Add(columnStyle);
                }
                int n = 0;
                for (i = 0; i < dtGrp.Rows.Count; i++)
                {
                    //sSql =" Select distinct PRComponent.description,PRStaff.CompValue,[PRCompMonth].[Order],PRComponent.componentid  from PRComponent,PRStaff,PRCompMonth,PRStaffGroup where PRCompMonth.PayRollId = 17 AND PRCompMonth.ComponentId = PRComponent.ComponentId AND PRCompMonth.Payrollid = PRStaff.Payrollid and PRStaff.staffid = 3 and PRStaff.Staffid = PRStaffGroup.Staffid and PRStaffGroup.Groupid = PRCompMonth.SalaryGroupid and PRStaffGroup.Payrollid = PRStaff.Payrollid and prstaff.componentid = prcomponent.componentid ORDER BY PRComponent.componentid ";
                    // FetchPayrollStaffGroup
                    //sSql = "Select distinct PRComponent.component,PRStaff.CompValue," +
                    //    "PRCompMonth.Comp_Order,PRComponent.componentid  from PRComponent,PRStaff,PRCompMonth," +
                    //    "PRStaffGroup where PRCompMonth.PayRollId = " + nPayRollId + " AND " +
                    //    "PRCompMonth.ComponentId = PRComponent.ComponentId AND " +
                    //    "PRCompMonth.Payrollid = PRStaff.Payrollid and " +
                    //    "PRStaff.staffid = " + dtGrp.Rows[i][0].ToString() + " and " +
                    //    "PRStaff.Staffid = PRStaffGroup.Staffid and " +
                    //    "PRStaffGroup.Groupid = PRCompMonth.SalaryGroupid and " + //PRCompMonth.SalaryGroupid ="+ dtGrp.Rows[i][0].ToString() + "and 
                    //    "PRStaffGroup.Payrollid = PRStaff.Payrollid and " +
                    //    "prstaff.componentid = prcomponent.componentid " +
                    //    "ORDER BY PRCOMPMONTH.COMP_ORDER ";
                    //createDataSet(sSql, "PRstaffgroup");
                    using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPayrollStaffGroup))
                    {
                        dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                        dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, nPayRollId);
                        dataManager.Parameters.Add(dtCompMonth.STAFFIDColumn, (dtGrp != null && dtGrp.Rows.Count > 0) ? dtGrp.Rows[i][0].ToString() : string.Empty);
                        resultArgs = dataManager.FetchData(DataSource.DataTable);
                        if (resultArgs.Success)
                        {
                            dtGrp1 = resultArgs.DataSource.Table;
                        }
                    }
                    //dtGrp1 = getDataSet().Tables["PRstaffgroup"];
                    try
                    {
                        //						if(dtGrp1.Rows.Count == 0 && n==0)
                        //						{
                        //							bCount = false;
                        //							n=n+1;
                        //						}
                        //						else
                        //							n=n+1;
                        if (dr != null) dtPayroll.Rows.Add(dr);
                        dtPayroll.Rows.Add(dtPayroll.NewRow());
                        if (dtGrp1.Rows.Count > 0)
                        {
                            for (int J = 0; J < dtGrp1.Rows.Count + 1; J++)
                            {
                                if (J == dtGrp1.Rows.Count)
                                //if(dtGrp1.Rows[J][0].ToString() == "StaffId")
                                {
                                    dtPayroll.Rows[i]["StaffId"] = dtGrp.Rows[i]["staffid"].ToString();
                                }
                                //else
                                else if (dtGrp1.Rows[J]["component"].ToString() == strFields[J])
                                {
                                    try
                                    {
                                        // dtPayroll.Rows[i][J] = DateTime.Parse(dtGrp1.Rows[J]["CompValue"].ToString()).ToShortDateString();
                                        dtPayroll.Rows[i][J] = dtGrp1.Rows[J]["CompValue"].ToString();
                                    }
                                    catch
                                    {
                                        dtPayroll.Rows[i][J] = (dtGrp1.Rows[J]["CompValue"].ToString() == "") ? "" : dtGrp1.Rows[J]["CompValue"].ToString();
                                    }
                                }
                            }
                        }
                    }
                    catch { }
                }
            }
            else
            {

                //sSql ="Paset teh Query";
                // FetchPrCompMonthStaffOrder
                //sSql = "Select distinct PRComponent.component,prcompMonth.Comp_Order " +
                //    "from PRComponent,PRCompMonth,PRSalaryGroup where " +
                //    "PRCompMonth.ComponentId = PRComponent.ComponentId AND " +
                //    "PRCompMonth.PayRollId = " + nPayRollId + " AND " +
                //    "PRSalaryGroup.Groupid = PRCompMonth.SalaryGroupid and " +
                //    "prCompMonth.SalaryGroupid = " + dtGrp.Rows[0][0].ToString() +
                //    " ORDER BY PRCompMonth.Comp_Order";
                //createDataSet(sSql, "PRstfgroup");
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPrCompMonthStaffOrder))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, nPayRollId);
                    dataManager.Parameters.Add(dtCompMonth.SALARYGROUPIDColumn, (dtGrp != null && dtGrp.Rows.Count > 0) ? dtGrp.Rows[0][0].ToString() : string.Empty);

                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                        dtGrp1 = resultArgs.DataSource.Table;
                }
                //dtGrp1 = getDataSet().Tables["PRstfgroup"];

                if (dtGrp.Rows.Count > 0)
                {
                    for (int j = 0; j < dtGrp1.Rows.Count; j++)
                    {
                        string Head = dtGrp1.Rows[j][0].ToString();
                        columns.Add(Head, typeof(System.ValueType));
                    }
                    for (i = 0; i < dtGrp.Rows.Count; i++)
                    {
                        //						sSql =" Select distinct PRComponent.description,PRStaff.CompValue,PRCompMonth.Comp_Order,PRComponent.componentid  from PRComponent,PRStaff,PRCompMonth,PRStaffGroup where PRCompMonth.PayRollId = 17 AND PRCompMonth.ComponentId = PRComponent.ComponentId AND PRCompMonth.Payrollid = PRStaff.Payrollid and PRStaff.staffid = 3 and PRStaff.Staffid = PRStaffGroup.Staffid and PRStaffGroup.Groupid = PRCompMonth.SalaryGroupid and PRStaffGroup.Payrollid = PRStaff.Payrollid and prstaff.componentid = prcomponent.componentid ORDER BY PRComponent.componentid ";
                        //						dtGrp1 = ExecuteQuery(sSql);
                        if (dr != null) dtPayroll.Rows.Add(dr);
                        dtPayroll.Rows.Add(dtPayroll.NewRow());
                        for (int J = 0; J < dtGrp1.Rows.Count; J++)
                        {
                            dtPayroll.Rows[i][J] = dtGrp1.Rows[J]["CompValue"].ToString();
                        }
                    }
                }

            }
            if (dtPayroll.Columns.Count > 1)
            {
                for (int l = 0; l < dtPayroll.Rows.Count; l++)
                {
                    if (dtPayroll.Rows[l][dtPayroll.Columns.Count - 1].ToString() == "")
                    {
                        dtPayroll.Rows.RemoveAt(l);
                        l = l - 1;
                    }
                }
            }

            //			if(bCount)
            //			{
            tStyle.AlternatingBackColor = System.Drawing.Color.Lavender;
            tStyle.BackColor = System.Drawing.Color.WhiteSmoke;
            tStyle.ForeColor = System.Drawing.Color.MidnightBlue;
            tStyle.GridLineColor = System.Drawing.Color.Gainsboro;
            tStyle.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(206)))), ((int)(((byte)(252)))));
            tStyle.HeaderForeColor = System.Drawing.Color.Black;
            tStyle.LinkColor = System.Drawing.Color.Teal;
            tStyle.SelectionBackColor = System.Drawing.Color.Teal;
            tStyle.SelectionForeColor = System.Drawing.Color.PaleGreen;

            tStyle.AllowSorting = true;
            //objGrid.TableStyles.Add(tStyle);
            if (dtPayroll.Columns.Count > 1)
            {

                objGrid.DataSource = null;
                objGrid.DataSource = dtPayroll;
                //objGrid.DataSource = dtPayroll.DefaultView;
            }
            //			}

        }

        public List<string> GetAcMeERPPayRoll(string nPayRollId, int ProjectId, long nSalaryGroup, DevExpress.XtraGrid.GridControl objGrid, bool bEdit)
        {
            //Filling the Values into the Grid Control
            int i;
            string sStartDate = "";
            string sFinishDate = "";
            bCount = true;
            DataSet tRs = new DataSet();
            DataTable tRsPRDate = new DataTable();
            DataSet tRs1 = new DataSet();
            DataSet rsDate1 = new DataSet();
            DataSet rsDate2 = new DataSet();

            DataTable dtGrp = new DataTable();
            DataTable dtGrp1 = new DataTable();
            DataTable dtPayroll = new DataTable();
            
            dtPayroll.Clear();
            DataColumnCollection columns;
            columns = dtPayroll.Columns;
            DataRow dr = null;
            var EditableColumns = new List<string>();            
            try
            {
                tRsPRDate = FetchRecord(SQLCommand.Payroll.FetchPayrollDate, nPayRollId);

                DateTime dtTemp = DateTime.Parse(tRsPRDate.Rows[0]["prDate"].ToString(), clsGeneral.DATE_FORMAT);
                sStartDate = dtTemp.ToShortDateString();

                dtTemp = dtTemp.AddMonths(1);
                sFinishDate = dtTemp.AddDays(-1).ToShortDateString();
            }
            catch
            {
                return EditableColumns;
            }
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPRStaff))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                if (nSalaryGroup != 0)
                {
                    dataManager.Parameters.Add(dtCompMonth.SALARYGROUPIDColumn, nSalaryGroup);
                }
                dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, nPayRollId);
                if (ProjectId != 0)
                {
                    dataManager.Parameters.Add(dtCompMonth.PROJECT_IDColumn, ProjectId);
                }
                dataManager.Parameters.Add(dtCompMonth.STARTDATEColumn, clsGeneral.GetMySQLDateTime(sStartDate, DateDataType.Date));
                dataManager.Parameters.Add(dtCompMonth.ENDDATEColumn, clsGeneral.GetMySQLDateTime(sFinishDate, DateDataType.Date));
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                    dtGrp = resultArgs.DataSource.Table;
            }
            if (dtGrp.Rows.Count == 0)
                return EditableColumns;
                        
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPProcessStaffGroup))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, nPayRollId);
                if (nSalaryGroup != 0)
                {
                    dataManager.Parameters.Add(dtCompMonth.SALARYGROUPIDColumn, (dtGrp != null && dtGrp.Rows.Count > 0) ? dtGrp.Rows[0][2].ToString() : string.Empty);
                }
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                {
                    dtGrp1 = resultArgs.DataSource.Table;
                }
            }
            strFields = new string[dtGrp1.Rows.Count + 1];
            if (dtGrp.Rows.Count > 0) //creating dynamic data table to view the staff processed component details according to count..
            {
                for (int j = 0; j < dtGrp1.Rows.Count + 1; j++)
                {
                    //columnStyle = new ThisDataGridTextBoxColumn();//DataGridTextBoxColumn();
                    if (j == dtGrp1.Rows.Count)
                    {
                        string Head = "StaffId";
                        strFields[j] = Head;
                        columns.Add(Head, typeof(Int32));
                        
                    }
                    else
                    {
                        string Head = dtGrp1.Rows[j][0].ToString();
                        strFields[j] = Head;
                        columns.Add(Head, typeof(System.String)); // valuetype
                        

                        /*when the iseditable is equals to 0 - Editable
                         *when the iseditable is equals to 1 - Not editable
                         * By pragasam
                         */
                        if (Convert.ToInt32(dtGrp1.Rows[j]["iseditable"]).Equals(0) )
                        {
                            EditableColumns.Add(Head);
                        }
                        else
                        {
                            //EditableColumns.Add(Head);
                        }
                    }
                    //tStyle.GridColumnStyles.Add(columnStyle);
                }

                //On 12/01/2017 Instead of taking PayRollstaff component for each staff (takes long time), take it for all staff for given group
                //It will be used for below process
                DataTable dtPayRollStaff = new DataTable();
                if (dtGrp.Rows.Count > 0)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPayrollStaffGroup))
                    {
                        dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                        dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, nPayRollId);
                        dataManager.Parameters.Add(dtCompMonth.SALARYGROUPIDColumn, nSalaryGroup);
                        //dataManager.Parameters.Add(dtCompMonth.STAFFIDColumn, (dtGrp != null && dtGrp.Rows.Count > 0) ? dtGrp.Rows[i][0].ToString() : string.Empty);
                        resultArgs = dataManager.FetchData(DataSource.DataTable);
                        if (resultArgs.Success)
                        {
                            dtPayRollStaff = resultArgs.DataSource.Table;
                        }
                    }

                }
                //-------------------------------------------------------------------------------------------------------------

                for (i = 0; i < dtGrp.Rows.Count; i++)
                {
                    //On 12/01/2017 Instead of taking PayRollstaff component for each staff (takes long time), take it for all staff before this loop for given group
                    //Filter it for each staff
                    //using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPayrollStaffGroup))
                    //{
                    //    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    //    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    //    dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, nPayRollId);
                    //    dataManager.Parameters.Add(dtCompMonth.STAFFIDColumn, (dtGrp != null && dtGrp.Rows.Count > 0) ? dtGrp.Rows[i][0].ToString() : string.Empty);
                    //    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    //    if (resultArgs.Success)
                    //    {
                    //        dtGrp1 = resultArgs.DataSource.Table;
                    //    }
                    //}
                    //-----------------------------------------------------------------------------------------------------------------------------------

                    if (dtPayRollStaff != null && dtPayRollStaff.Rows.Count > 0)
                    {
                        //On 12/01/2017 Instead of taking PayRollstaff component for every staff (takes long time), 
                        dtPayRollStaff.DefaultView.RowFilter = string.Empty;
                        dtPayRollStaff.DefaultView.RowFilter = "staffid = " + dtGrp.Rows[i][0].ToString();
                        dtGrp1 = dtPayRollStaff.DefaultView.ToTable();
                        //---------------------------------------------------------------------------

                        try
                        {
                            Double myDec;
                            Double tmpValue;
                            if (dr != null) dtPayroll.Rows.Add(dr);
                            dtPayroll.Rows.Add(dtPayroll.NewRow());
                            if (dtGrp1.Rows.Count > 0)
                            {
                                for (int J = 0; J < dtGrp1.Rows.Count + 1; J++)
                                {
                                    if (J == dtGrp1.Rows.Count)
                                    {
                                        dtPayroll.Rows[i]["StaffId"] = dtGrp.Rows[i]["staffid"].ToString();
                                    }
                                    else if (dtGrp1.Rows[J]["component"].ToString() == strFields[J])
                                    {
                                        try
                                        {
                                            var Result = double.TryParse(dtGrp1.Rows[J]["CompValue"].ToString(), out myDec);
                                            if (Result)
                                            {
                                                tmpValue = UtilityMember.NumberSet.ToDouble(dtGrp1.Rows[J]["CompValue"].ToString());
                                                //dtPayroll.Rows[i][J] = UtilityMember.NumberSet.ToNumber(tmpValue);
                                                if (dtGrp1.Rows[J]["LinkValue"].ToString() == "Account_Number")
                                                {
                                                    dtPayroll.Rows[i][J] = tmpValue.ToString();
                                                }
                                                else if (dtGrp1.Rows[J]["LinkValue"].ToString() == "UAN")
                                                {
                                                    dtPayroll.Rows[i][J] = tmpValue.ToString();
                                                }
                                                else if (dtGrp1.Rows[J]["LinkValue"].ToString().ToUpper() == PayRollExtraPayInfo.PAYING_SALARY_DAYS.ToString().Replace("_","").ToString())
                                                {
                                                    dtPayroll.Rows[i][J] = tmpValue.ToString();
                                                }
                                                else
                                                {
                                                    dtPayroll.Rows[i][J] = UtilityMember.NumberSet.ToNumber(tmpValue);
                                                }
                                            }
                                            else
                                            {
                                                dtPayroll.Rows[i][J] = dtGrp1.Rows[J]["CompValue"].ToString();
                                            }
                                        }
                                        catch
                                        {
                                            dtPayroll.Rows[i][J] = (dtGrp1.Rows[J]["CompValue"].ToString() == "") ? "" : dtGrp1.Rows[J]["CompValue"].ToString();
                                        }
                                    }
                                    else
                                    {
                                        foreach (string item in strFields)
                                        {
                                            if (dtGrp1.Rows[J]["component"].ToString() == item)
                                            {
                                                int compIndex = Array.IndexOf(strFields, dtGrp1.Rows[J]["component"].ToString());
                                                var Result = double.TryParse(dtGrp1.Rows[J]["CompValue"].ToString(), out myDec);
                                                if (Result)
                                                {
                                                    tmpValue = UtilityMember.NumberSet.ToDouble(dtGrp1.Rows[J]["CompValue"].ToString());

                                                    if (dtGrp1.Rows[J]["LinkValue"].ToString() == "Account_Number")
                                                    {
                                                        dtPayroll.Rows[i][compIndex] = tmpValue.ToString();
                                                    }
                                                    else if (dtGrp1.Rows[J]["LinkValue"].ToString() == "UAN")
                                                    {
                                                        dtPayroll.Rows[i][compIndex] = tmpValue.ToString();
                                                    }
                                                    else
                                                    {
                                                        dtPayroll.Rows[i][compIndex] = UtilityMember.NumberSet.ToNumber(tmpValue);
                                                    }
                                                    // dtPayroll.Rows[i][compIndex] = UtilityMember.NumberSet.ToNumber(tmpValue);
                                                }
                                                else
                                                {
                                                    dtPayroll.Rows[i][compIndex] = dtGrp1.Rows[J]["CompValue"].ToString();
                                                }
                                            }
                                        }
                                        //int StaffComponentCount = dtGrp1.Rows.Count;
                                        //int MaxCompcount = strFields.Length;
                                        //int DiffinComp = MaxCompcount - StaffComponentCount;
                                    }

                                }
                            }
                        }
                        catch { }
                    }
                }
            }
            else
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPrCompMonthStaffOrder))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, nPayRollId);
                    if (nSalaryGroup != 0)
                    {
                        dataManager.Parameters.Add(dtCompMonth.SALARYGROUPIDColumn, (dtGrp != null && dtGrp.Rows.Count > 0) ? dtGrp.Rows[0][0].ToString() : string.Empty);
                    }
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                        dtGrp1 = resultArgs.DataSource.Table;
                }

                if (dtGrp.Rows.Count > 0)
                {
                    for (int j = 0; j < dtGrp1.Rows.Count; j++)
                    {
                        string Head = dtGrp1.Rows[j][0].ToString();
                        columns.Add(Head, typeof(System.ValueType));
                    }
                    for (i = 0; i < dtGrp.Rows.Count; i++)
                    {
                        if (dr != null) dtPayroll.Rows.Add(dr);
                        dtPayroll.Rows.Add(dtPayroll.NewRow());
                        for (int J = 0; J < dtGrp1.Rows.Count; J++)
                        {
                            dtPayroll.Rows[i][J] = dtGrp1.Rows[J]["CompValue"].ToString();
                        }
                    }
                }

            }
            if (dtPayroll.Columns.Count > 1)
            {
                //for (int l = 0; l < dtPayroll.Rows.Count; l++)
                //{
                //    if (dtPayroll.Rows[l][dtPayroll.Columns.Count - 1].ToString() == "")
                //    {
                //        dtPayroll.Rows.RemoveAt(l);
                //        l = l - 1;
                //    }
                //}
                dtPayroll.DefaultView.RowFilter = dtPayroll.Columns[dtPayroll.Columns.Count - 1].ColumnName + " > 0";
                dtPayroll = dtPayroll.DefaultView.ToTable();
            }

            if (dtPayroll.Columns.Count > 1)
            {
                dtPayroll.Columns.Add("StaffOrder", typeof(System.Int32));
                objGrid.DataSource = null;
                Double myDec = 0;
                for (int iRow = 0; iRow < dtPayroll.Rows.Count; iRow++)
                {
                    //----------------------------------------------------------------------------------
                    Int32 staffid = UtilityMember.NumberSet.ToInteger(dtPayroll.Rows[iRow]["StaffId"].ToString());
                    dtGrp.DefaultView.RowFilter = string.Empty;
                    dtGrp.DefaultView.RowFilter = "STAFFID = " + staffid;
                    if (dtGrp.DefaultView.Count > 0)
                    {
                        dtPayroll.Rows[iRow]["StaffOrder"] = dtGrp.DefaultView[0]["StaffOrder"].ToString();
                    }
                    dtGrp.DefaultView.RowFilter = string.Empty;
                    //----------------------------------------------------------------------------------

                    /* For Temp on 22/11/2023, To show empty value for linked text compo
                    for (int ival = 0; ival < dtPayroll.Columns.Count - 1; ival++)
                    {
                        var Result = double.TryParse(dtPayroll.Rows[iRow][ival].ToString(), out myDec);
                        if (!Result)
                        {
                            if (dtPayroll.Rows[iRow][ival].ToString() == "")
                            {
                                dtPayroll.Rows[iRow][ival] = "0.00";
                            }
                        }
                    }*/

                }

                dtPayroll.DefaultView.Sort = "StaffOrder";
                dtPayroll = dtPayroll.DefaultView.ToTable();
                dtPayroll.Columns.Remove("StaffOrder");
                objGrid.DataSource = dtPayroll;
                //-------------------------------------------------------------------------------------------------
            }

            return EditableColumns;
        }


        #region Component Transaction Methods
        public ResultArgs IsLedgerExists(string LedgerName)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.IsLedgerExists))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtledger.LEDGER_NAMEColumn, LedgerName);
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs;
        }
        public ResultArgs FetchLedger(string LedgerName)
        {

            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchLedgerByLedgerName))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtledger.LEDGER_NAMEColumn, LedgerName);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs FetchLedgerById(string LegderIds)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchLedgerIdLedgerNameByLedgerId))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(dtledger.LEDGER_IDColumn, LegderIds);
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultArgs;
        }
        public ResultArgs MapProjectLedger(int LedgerId, DataTable ProjectIds)
        {
            if (ProjectIds != null && ProjectIds.Rows.Count > 0)
            {
                foreach (DataRow dr in ProjectIds.Rows)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Payroll.ProjectLedgerMapping))
                    {
                        dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                        dataManager.Parameters.Add(dtledger.PROJECT_IDColumn, this.NumberSet.ToInteger(dr[dtledger.PROJECT_IDColumn.ColumnName].ToString()));
                        dataManager.Parameters.Add(dtledger.LEDGER_IDColumn, LedgerId);
                        resultArgs = dataManager.UpdateData();
                        if (!resultArgs.Success)
                            break;
                    }
                }
            }
            return resultArgs;
        }

        public ResultArgs DeleteProjectLedgerMapping(int LedgerId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.ProjectLedgerMappingDelete))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(dtledger.LEDGER_IDColumn, LedgerId);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        public ResultArgs MapProjectLedger(string LedgerId, string ProjectIds)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.ProjectLedgerMapping))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.Parameters.Add(dtledger.PROJECT_IDColumn, this.NumberSet.ToInteger(ProjectIds));
                dataManager.Parameters.Add(dtledger.LEDGER_IDColumn, this.NumberSet.ToInteger(LedgerId));
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }
        #endregion
    }
}
