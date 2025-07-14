using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using Payroll.Model.UIModel;
using Bosco.Utility.ConfigSetting;
using Bosco.Utility.Common;
using System.Windows.Forms;
using Bosco.DAO.Data;
using Payroll.DAO.Schema;
using Bosco.Utility;
using DevExpress.XtraPrinting;
using System.Drawing;
using DevExpress.XtraEditors;
using Bosco.Utility.ConfigSetting;
using System.Text.RegularExpressions;

namespace PAYROLL.Modules
{
    public class clsPayrollBase : IDisposable
    {
        #region Varaible Payroll Declaration
        public clsPrGateWay objPRGW = new clsPrGateWay();
        public DataTable dtStatus = new DataTable();
        public clsPayrollGrade objClsGrade = new clsPayrollGrade();
        public clsModPay objModPay = new clsModPay();
        public clsGeneral objGeneral = new clsGeneral();
        public clsPrReport objPRReport = new clsPrReport();
        ResultArgs resultargs = new ResultArgs();
        clsPRPSReport objPRPaySlip = new clsPRPSReport();
        ApplicationSchema.PAYWagesDataTable dtCompMonth = new ApplicationSchema.PAYWagesDataTable();
        ApplicationSchema.PRCOMPMONTHDataTable dtcomp = new ApplicationSchema.PRCOMPMONTHDataTable();
        ApplicationSchema.PRSTAFFGROUPDataTable dtstaffgrp = new ApplicationSchema.PRSTAFFGROUPDataTable();
        ApplicationSchema.PR_DEPARTMENTDataTable dtDepartment = new ApplicationSchema.PR_DEPARTMENTDataTable();
        CommonMember UtilityMember = new CommonMember();
        private string PrintPageTitle = string.Empty;
        SettingProperty appsetting = new Bosco.Utility.ConfigSetting.SettingProperty();
        #endregion

        #region Properties
        public long PAYROLL_Id
        {
            get { return clsGeneral.PAYROLL_ID; }
            set { clsGeneral.PAYROLL_ID = value; }
        }
        private static string payrollgroupid = string.Empty;
        public static string PAYROLL_GROUP_ID
        {
            get { return payrollgroupid; }
            set { payrollgroupid = value; }
        }
        private static int payrollLoanledger = 0;
        public static int PAYROLL_LOAN_LEDGER_ID
        {
            get { return payrollLoanledger; }
            set { payrollLoanledger = value; }
        }
        private static string payrollCompId = string.Empty;
        public static string PAYROLL_COMPONENT_ID
        {
            get { return payrollCompId; }
            set { payrollCompId = value; }
        }
        private static string payrollStaffId = string.Empty;
        public static string PAYROLL_STAFF_ID
        {
            get { return payrollStaffId; }
            set { payrollStaffId = value; }
        }
        public string PAYROLL_MONTH
        {
            get { return clsGeneral.PAYROLL_MONTH; }
        }
        public DataTable PayrollList
        {
            get { return FetchPayroll(); }
        }
        public DataTable Payrollgroup
        {
            get { return FetchPayrollGroup(); }
        }
        // Ids for Payslip starts
        private static int PAYSlipId = 0;
        public static int PaySlipPayId
        {
            get { return PAYSlipId; }
            set { PAYSlipId = value; }
        }
        private static string PayslipStaffID = string.Empty;
        public static string payslipstaffid
        {
            get { return PayslipStaffID; }
            set { PayslipStaffID = value; }
        }

        private static int PayslipGroupID = 0;
        public static int payslipGroupid
        {
            get { return PayslipGroupID; }
            set { PayslipGroupID = value; }
        }
        // Ids for Payslip ends
        private static string PayslipGroupNAME = string.Empty;
        public static string payslipGroupName
        {
            get { return PayslipGroupNAME; }
            set { PayslipGroupNAME = value; }
        }
        private static string PayrollName = string.Empty;
        public static string payrollname
        {
            get { return PayrollName; }
            set { PayrollName = value; }
        }
        private static string PayrollComponent = string.Empty;
        public static string payrollcomponent
        {
            get { return PayrollComponent; }
            set { PayrollComponent = value; }
        }
        private static string ColumnNames = string.Empty;
        public static string PAYROLL_COLUMN_NAMES
        {
            get { return clsGeneral.PAYROLL_RPT_COL_TOTAL_FIELDS; }
        }

        private static string ProjectId = string.Empty;
        public static string projectid
        {
            get { return clsGeneral.RPTPROJECTID; }
            set { clsGeneral.RPTPROJECTID = value; }
        }
        #endregion

        #region Common Events
        public void PrintGridView(DevExpress.XtraGrid.GridControl GridView, string Title, PrintType printType, DevExpress.XtraGrid.Views.Grid.GridView gvControl, bool isLandscape = false)
        {
            if (printType == PrintType.DT)
            {
                DataTable dtGridView = GridView.DataSource as DataTable;
                if (GridView.DataSource != null && dtGridView != null && dtGridView.Rows.Count != 0)
                {
                    PrintableComponentLink link = new PrintableComponentLink(new PrintingSystem());
                    link.Component = GridView;
                    PrintPageTitle = Title;
                    link.Landscape = isLandscape;
                    link.CreateMarginalHeaderArea += new CreateAreaEventHandler(link_CreateMarginalHeaderArea);
                    link.CreateDocument();
                    link.ShowPreviewDialog();
                }
                else
                {
                    XtraMessageBox.Show("There is no record to take printout", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {

                //DataView dt = gvControl.DataSource as DataView;
                //DataSet dsDataView = new DataSet();
                //dsDataView.Tables.Add(dt.ToTable());
                DataSet dsDataView = GridView.DataSource as DataSet;
                if (GridView.DataSource != null && dsDataView != null && dsDataView.Tables.Count != 0)
                {
                    PrintableComponentLink link = new PrintableComponentLink(new PrintingSystem());
                    link.Component = GridView;
                    PrintPageTitle = Title;
                    link.Landscape = isLandscape;
                    link.CreateMarginalHeaderArea += new CreateAreaEventHandler(link_CreateMarginalHeaderArea);
                    link.CreateDocument();
                    link.ShowPreviewDialog();
                }
                else
                {
                    XtraMessageBox.Show("There is no record to take printout", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }

        void link_CreateMarginalHeaderArea(object sender, CreateAreaEventArgs e)
        {
            PageInfoBrick brick = e.Graph.DrawPageInfo(PageInfo.DateTime, appsetting.InstituteName, Color.DarkBlue,

      new RectangleF(0, 0, 100, 80), BorderSide.None);

            PageInfoBrick brick2 = e.Graph.DrawPageInfo(PageInfo.DateTime, PrintPageTitle, Color.DarkBlue,

      new RectangleF(0, 0, 100, 20), BorderSide.None);

            brick.LineAlignment = BrickAlignment.Center;
            brick2.LineAlignment = BrickAlignment.Center;

            brick.Alignment = BrickAlignment.Center;
            brick2.Alignment = BrickAlignment.Center;

            brick.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            brick.AutoWidth = true;

            brick2.Font = new System.Drawing.Font("Tahoma", 10F, System.Drawing.FontStyle.Bold);
            brick2.AutoWidth = true;
        }

        /// <summary>
        /// On 25/11/2023, Check Colmuns contains only numbers 
        /// </summary>
        /// <returns></returns>
        public bool IsColumnContainsNubmerOnly(DataTable dtSource, string column)
        {
            bool isOnlyNumbers = true;
            try
            {
                if (dtSource.Columns.Contains(column) && dtSource.Columns[column].DataType == typeof(System.String))
                {
                    DataTable dt =  dtSource.DefaultView.ToTable();
                    //dt.DefaultView.RowFilter = "[" + dt.Columns[column].ColumnName + "] <>'' AND [" + dt.Columns[column].ColumnName + "] IS NOT NULL'' ";
                    dt.DefaultView.RowFilter = "ISNULL([" + dt.Columns[column].ColumnName + "], '') <>''";
                    isOnlyNumbers = false;
                    if (dt.DefaultView.Count > 0)
                    {
                        dt = dt.DefaultView.ToTable();
                        var objIsColumnsNumberOnly = dt.AsEnumerable().Where(x => Regex.IsMatch(x.Field<string>(dt.Columns[column].ColumnName), @"^\d{1,}$"));
                        if (objIsColumnsNumberOnly != null && objIsColumnsNumberOnly.AsDataView().Count > 0)
                        {
                            isOnlyNumbers = true;
                        }
                    }
                }
            }
            catch (Exception err)
            {
                MessageRender.ShowMessage(err.Message);
                isOnlyNumbers = true;
            }
            return isOnlyNumbers;
        }
        #endregion

        #region methods

        public bool GetLockStatus()
        {
            if (objModPay.GetValue("PRSTATUS", "lockedstatus", "PayRollId = " + clsGeneral.PAYROLL_ID) == "Y")
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsPayrollExists()
        {
            bool IsExists = true;
            dtStatus = objClsGrade.getPayrollMonth();
            if (dtStatus == null)
            {
                IsExists = false;
            }
            else
            {
                clsGeneral.PAYROLL_ID = Convert.ToInt32(dtStatus.Rows[0][0]);
            }
            return IsExists;
        }

        public void SetRecentPayRoll()
        {
            objPRGW.PayRollId = objPRGW.GetCurrentPayroll();
            clsGeneral.PAYROLL_ID = objPRGW.PayRollId;
            clsGeneral.PAYROLLDATE = objModPay.GetValue("PRCREATE", "DATE_FORMAT(PRDATE, '%d/%m/%Y') as PRDATE", "PAYROLLID = " + clsGeneral.PAYROLL_ID);
        }

        public bool SetPayRollCaption()
        {
            bool isPayrollExists = false;
            objPRGW.PRName = "";
            if (objPRGW.PayRollId != 0)
            {
                objPRGW.PRName = objModPay.GetValue("PRCREATE", "PRNAME", "PAYROLLID = " + clsGeneral.PAYROLL_ID);
                clsGeneral.PAYROLLDATE = objModPay.GetValue("PRCREATE", "DATE_FORMAT(PRDATE, '%d/%m/%Y') as PRDATE", "PAYROLLID = " + clsGeneral.PAYROLL_ID);
            }
            if (objPRGW.PRName != "")
                SettingProperty.PayrollMonth = objPRGW.PRName.ToString();
            clsGeneral.PAYROLL_MONTH = objPRGW.PRName;

            if (clsGeneral.PAYROLL_MONTH == "" & clsGeneral.PAYROLL_ID == 0)
            {
                isPayrollExists = false;
            }
            else
            {
                isPayrollExists = true;
            }
            return isPayrollExists;
        }

        public void SetValues()
        {
            dtStatus = objClsGrade.getPayrollMonth();
            if (dtStatus != null && dtStatus.Rows.Count > 0)
            {
                SettingProperty.PayrollMonth = dtStatus.Rows[0][2].ToString();
                clsGeneral.PAYROLL_MONTH = dtStatus.Rows[0][2].ToString();
                clsGeneral.PAYROLL_ID = Convert.ToInt32(dtStatus.Rows[0][0]);
            }
            else
            {
                clsGeneral.PAYROLL_ID = 0;
                clsGeneral.PAYROLL_MONTH = "";
                SettingProperty.PayrollMonth = "";
            }
        }

        public bool DeletePayroll()
        {
            bool Rtn = false;
            if (SettingProperty.dtOpen != null)
            {
                if (XtraMessageBox.Show("Are you sure to delete the current payroll '" + SettingProperty.PayrollMonth + "' ?", "Payroll", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    long lastpayrollid = objPRGW.GetLatestPayrollId();
                    if (lastpayrollid == clsGeneral.PAYROLL_ID)
                    {
                        if (new clsModPay().GetValue("PRSTATUS", "lockedstatus", "PayRollId = " + clsGeneral.PAYROLL_ID) == "N")
                        {
                            //if (objPRGW.DeletePayRoll(clsGeneral.PAYROLL_ID))
                            if (objPRGW.DeletePayrollByPayrollId(clsGeneral.PAYROLL_ID))
                            {
                                Rtn = true;
                                XtraMessageBox.Show("Payroll '" + SettingProperty.PayrollMonth + "' is deleted", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                SetValues();
                                SetRecentPayRoll();
                                SetPayRollCaption();
                            }
                            else
                                XtraMessageBox.Show("Current payroll can not be deleted.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                        else
                            XtraMessageBox.Show("Current payroll is locked unable to delete.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                        XtraMessageBox.Show("Payroll can not be deleted in between Payroll months, Delete from recent Payroll Month", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                XtraMessageBox.Show("Payroll is not Available", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return Rtn;
        }

        public DataTable FetchPayroll()
        {
            DataTable dtPayroll = new DataTable();
            try
            {
                using (clsPrGateWay clsgateway = new clsPrGateWay())
                {
                    dtPayroll = clsgateway.FetchRecord(SQLCommand.Payroll.PayrollExistOpen, "payrol");
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
            return dtPayroll;
        }


        public DataTable FetchPayrollGroup()
        {
            DataTable dtpayrollGroup = new DataTable();
            try
            {
                using (clsPrGateWay clsgateway = new clsPrGateWay())
                {
                    dtpayrollGroup = clsgateway.FetchRecord(SQLCommand.Payroll.GetGroupSQL, "payrollGroup");
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + "\n" + ex.StackTrace, true);
            }
            finally { }
            return dtpayrollGroup;
        }

        public DataTable FetchPayrollGroupByPayroll(int PayrollId)
        {
            DataTable dtGradeList = new DataTable();
            try
            {
                using (clsPayrollGrade Grade = new clsPayrollGrade())
                {
                    dtGradeList = Grade.getPayrollGroupByPayrollId(PayrollId);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }
            finally { }
            return dtGradeList;
        }



        /// <summary>
        /// This method is used to Posed Payroll payment voucher, deleted pysically from Voucher and payroll tables (from prroll_voucher table)
        /// </summary>
        /// <param name="voucherid"></param>
        /// <returns></returns>
        public ResultArgs DeletePayrollPostPaymentVouchers(int VoucherId)
        {
            try
            {
                using (clsPrGateWay objPrgateway = new clsPrGateWay())
                {
                    resultargs = objPrgateway.DeletePayrollPostPaymentVouchers(VoucherId);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }
            finally { }
            return resultargs;
        }
        #endregion

        #region Payroll Reports

        #region Wages Report
        public ResultArgs WagesReport()
        {
            try
            {
                string WagesQuery = string.Empty;
                objPRReport.DropReportTable();
                objPRReport.CreateReportTableNew();
                WagesQuery = objPRReport.GetReportQuery(6, payrollgroupid, 0); //0 is send instead of optional value
                using (DataManager dataManager = new DataManager())  // SQLCommand.Payroll.FetchComponentReport , "Component"
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    if (SettingProperty.PayrollFinanceEnabled)
                    {
                        dataManager.Parameters.Add(dtCompMonth.PROJECT_IDColumn, clsGeneral.RPTPROJECTID);
                    }
                    resultargs = dataManager.FetchData(Bosco.DAO.Data.DataSource.DataTable, WagesQuery);
                    clsGeneral.PAYROLL_RPT_COL_TOTAL_FIELDS = objPRReport.ColumnTotalFields;
                    objPRReport.DropReportTable();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message.ToString());
            }
            return resultargs;
        }
        #endregion

        #region Payslip Report
        public ResultArgs RpPayslip(string payrollid, string staffid, Int32 paygroupid = 0)
        {
            try
            {
                string WagesQuery = string.Empty;
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchValuesbyComponentStaffGroup))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, payrollid);
                    dataManager.Parameters.Add(dtCompMonth.StaffIdColumn, staffid);
                    if (paygroupid != 0)
                    {
                        dataManager.Parameters.Add(dtCompMonth.GroupIdColumn, paygroupid);
                    }
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultargs = dataManager.FetchData(Bosco.DAO.Data.DataSource.DataView);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message.ToString());
            }
            return resultargs;
        }

        public ResultArgs PostedPayrollVouchers(string datefrom, string dateto)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.PostedPayrollVouchers))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtcomp.FROMDATEColumn.ColumnName, datefrom, DataType.Date);
                    dataManager.Parameters.Add(dtcomp.TODATEColumn.ColumnName, dateto, DataType.Date);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultargs = dataManager.FetchData(Bosco.DAO.Data.DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message.ToString());
            }
            return resultargs;
        }

        public ResultArgs PostedPayrollVouchersDetail(string datefrom, string dateto)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.PostedPayrollVoucherDetail))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtcomp.FROMDATEColumn.ColumnName, datefrom, DataType.Date);
                    dataManager.Parameters.Add(dtcomp.TODATEColumn.ColumnName, dateto, DataType.Date);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultargs = dataManager.FetchData(Bosco.DAO.Data.DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message.ToString());
            }
            return resultargs;
        } 

        public ResultArgs GetBasicPayInfo()
        {
            string Condition = "AND SS.STAFFID in (" + PayslipStaffID + ") AND SG.GROUPID = " + PayslipGroupID + "";
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchValuesForPaySlip, "PRCreate"))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn, PAYSlipId);
                    dataManager.Parameters.Add("CONDITION", Condition);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultargs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message.ToString());
            }
            return resultargs;
        }

        #endregion

        #region Pay Register Report
        public ResultArgs PayRegisterReport()
        {
            try
            {
                string PayRegisterQuery = string.Empty;
                objPRReport.DropReportTable();
                objPRReport.CreateReportTable();
                if (!string.IsNullOrEmpty(payrollgroupid) || payrollgroupid != "")
                {
                    PayRegisterQuery = objPRReport.GetReportQuery(0, payrollgroupid, 0); //0 is send instead of optional value
                    using (DataManager dataManager = new DataManager())
                    {
                        dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                        if (SettingProperty.PayrollFinanceEnabled)
                        {
                            dataManager.Parameters.Add(dtCompMonth.PROJECT_IDColumn, clsGeneral.RPTPROJECTID);
                        }
                        resultargs = dataManager.FetchData(Bosco.DAO.Data.DataSource.DataTable, PayRegisterQuery);
                    }
                    clsGeneral.PAYROLL_RPT_COL_TOTAL_FIELDS = objPRReport.ColumnTotalFields;
                    objPRReport.DropReportTable();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message.ToString());
            }
            return resultargs;
        }


        /// <summary>
        /// On 07/08/2019, Earlier PayRegister report is generating based on temp table, 
        /// now removed temp table and make use of DataTabe with help of existing logic
        /// </summary>
        /// <returns></returns>
        public ResultArgs GeneratePayRegister(string stfGroupIds, string payrollid = "", string SelectedStaffid = "", string datefrom = "", string dateto = "", string departmentid = "")
        {
            ResultArgs resultArg = new ResultArgs();
            try
            {
                string sCheckField = "";
                ApplicationSchema.PRCOMPMONTHDataTable dtCompMonth = new ApplicationSchema.PRCOMPMONTHDataTable();
                DataTable rsComp = new DataTable();
                DataTable dtPayRegister = new DataTable();
                DataColumn dcSno = new DataColumn("S.No", typeof(Int32));
                dcSno.AutoIncrement = true;
                dcSno.AutoIncrementSeed = 1;
                dcSno.AutoIncrementStep = 1;
                dtPayRegister.Columns.Add(dcSno);
                dtPayRegister.Columns.Add(new DataColumn(dtcomp.PAYROLLIDColumn.ColumnName, typeof(Int32)));
                dtPayRegister.Columns.Add(new DataColumn(dtcomp.PRNAMEColumn.ColumnName, typeof(String)));
                dtPayRegister.Columns.Add(new DataColumn(dtcomp.STAFFIDColumn.ColumnName, typeof(Int32)));
                dtPayRegister.Columns.Add(new DataColumn(dtstaffgrp.STAFFORDERColumn.ColumnName, typeof(Int32)));
                dtPayRegister.Columns.Add(new DataColumn(dtstaffgrp.GROUPNAMEColumn.ColumnName, typeof(string)));


                //# Get list of components for given payroll and staff group
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchComponentReport, "Component"))
                {
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtCompMonth.SALARYGROUPIDColumn.ColumnName, stfGroupIds, DataType.Int32);
                    if (!string.IsNullOrEmpty(payrollid) && payrollid != "0")
                    {
                        dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn.ColumnName, payrollid, DataType.Int32);
                    }

                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArg = dataManager.FetchData(Bosco.DAO.Data.DataSource.DataTable);
                }

                if (resultArg.Success)
                {
                    rsComp = resultArg.DataSource.Table;

                    //# Generate Payregister table with component name as column name and generate payregister datatable as empty
                    for (int i = 0; i < rsComp.Rows.Count; i++)
                    {
                        DataColumn dcPayComp = null;
                        ///On 11/02/2022, dont show columns which are not applicable in view
                        int dontshowinview = UtilityMember.NumberSet.ToInteger(rsComp.Rows[i]["DONT_SHOWINBROWSE"].ToString());
                        if (sCheckField.IndexOf("!" + rsComp.Rows[i]["Component"].ToString() + "!", 0) == -1 && dontshowinview == 0)
                        {
                            string sTempField = rsComp.Rows[i]["Component"].ToString();

                            //var numbersonly = rsComp.AsEnumerable().Where(x => Regex.IsMatch(x.Field<string>("Component"), @"^\d{1,}$")).ToList();
                            var numbersonly = rsComp.AsEnumerable().Where(x => Regex.IsMatch(x.Field<string>("Component"), @"^-?[0-9][0-9,\.]+$")).ToList();

                            //var numbersonly = dt.AsEnumerable()
                            //    .Where(x => Regex.IsMatch(x.Field<string>("Product Number"), @"^\d{1,}$"))
                            //    .ToList();

                            if ((int.Parse(rsComp.Rows[i]["Type"].ToString()) <= 1 || int.Parse(rsComp.Rows[i]["Type"].ToString()) == 3) &&
                                int.Parse(rsComp.Rows[i]["CompRound"].ToString()) == 0)
                            {
                                dcPayComp = new DataColumn(sTempField, typeof(double));

                                //On 10/03/2021, to have default value 0 for double datatype columns
                                dcPayComp.DefaultValue = 0;
                            }
                            else if (sTempField.ToUpper() == PayRollDefaultComponent.TOTALDAYS.ToString() || sTempField.ToUpper() == PayRollDefaultComponent.WORKINGDAYS.ToString() ||
                                     sTempField.ToUpper() == PayRollDefaultComponent.LEAVEDAYS.ToString() || sTempField.ToUpper() == PayRollDefaultComponent.LOPDAYS.ToString())
                            {
                                dcPayComp = new DataColumn(sTempField, typeof(double));
                            }
                            else if ((int.Parse(rsComp.Rows[i]["Type"].ToString()) <= 1 || int.Parse(rsComp.Rows[i]["Type"].ToString()) == 3)
                                                               && int.Parse(rsComp.Rows[i]["CompRound"].ToString()) > 0)
                            {
                                dcPayComp = new DataColumn(sTempField, typeof(double));

                                //On 10/03/2021, to have default value 0 for double datatype columns
                                dcPayComp.DefaultValue = 0;
                            }

                            else
                            {
                                dcPayComp = new DataColumn(sTempField, typeof(string));
                            }
                        }
                        sCheckField = sCheckField + "!" + rsComp.Rows[i]["component"].ToString() + "!";
                        if (dcPayComp != null)
                        {
                            dtPayRegister.Columns.Add(dcPayComp);
                        }
                    }

                    if (dtPayRegister.Columns.Count > 0)
                    {
                        //# get satff payroll details for given payroll and given stff group
                        using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchStaffByGroup, "PRStaff"))
                        {
                            dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                            if (!string.IsNullOrEmpty(payrollid) && payrollid != "0")
                            {
                                dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn.ColumnName, payrollid, DataType.Int32);
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(datefrom))
                                {
                                    dataManager.Parameters.Add(dtCompMonth.FROMDATEColumn.ColumnName, UtilityMember.DateSet.ToDate(datefrom), DataType.Date);
                                }
                                if (!string.IsNullOrEmpty(dateto))
                                {
                                    dataManager.Parameters.Add(dtCompMonth.TODATEColumn.ColumnName, UtilityMember.DateSet.ToDate(dateto), DataType.Date);
                                }
                            }

                            if (!string.IsNullOrEmpty(SelectedStaffid) && SelectedStaffid != "0")
                            {
                                dataManager.Parameters.Add(dtCompMonth.STAFFIDColumn.ColumnName, SelectedStaffid);
                            }

                            using (clsPrGateWay gateway = new clsPrGateWay())
                            {
                                dataManager.Parameters.Add(dtstaffgrp.RECENT_PAYROLL_IDColumn, objPRGW.GetCurrentPayroll(), DataType.Int32);
                            }

                            if (!string.IsNullOrEmpty(departmentid) && departmentid != "0")
                            {
                                dataManager.Parameters.Add(dtDepartment.DEPARTMENT_IDColumn.ColumnName, departmentid, DataType.Int32);
                            }

                            dataManager.Parameters.Add(dtCompMonth.GROUPIDColumn.ColumnName, stfGroupIds, DataType.Int32);

                            dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                            resultArg = dataManager.FetchData(Bosco.DAO.Data.DataSource.DataTable);
                        }

                        if (resultArg.Success)
                        {
                            DataTable dtPRStaff = resultArg.DataSource.Table;

                            // # Read grid data and seperate them according to record vice using seperators
                            //# Make it single staff
                            //On 05/01/2023, For Yearly Payrroll Take staff order from recent Payroll
                            DataTable dtStaffIds = dtPRStaff.DefaultView.ToTable(true, new string[] { "PAYROLLID", "GROUPNAME", "STAFFID", "NAME", "STAFFORDER" });
                            if (string.IsNullOrEmpty(payrollid) || payrollid == "0")
                            {
                                dtStaffIds = dtPRStaff.DefaultView.ToTable(true, new string[] { "PAYROLLID", "GROUPNAME", "STAFFID", "NAME", "RECENT_STAFF_ORDER" });
                            }

                            //14/03/2022, Make STaff Order
                            dtStaffIds.DefaultView.Sort = "GROUPNAME, " + ((string.IsNullOrEmpty(payrollid) || payrollid == "0") ? "RECENT_STAFF_ORDER" : "STAFFORDER");
                            dtStaffIds = dtStaffIds.DefaultView.ToTable();

                            foreach (DataRow drStaff in dtStaffIds.Rows)
                            {
                                Int32 payroll_id = UtilityMember.NumberSet.ToInteger(drStaff[dtcomp.PAYROLLIDColumn.ColumnName].ToString());
                                Int32 staffid = UtilityMember.NumberSet.ToInteger(drStaff[dtcomp.STAFFIDColumn.ColumnName].ToString());
                                string staffname = drStaff["NAME"].ToString();
                                string groupname = drStaff[dtstaffgrp.GROUPNAMEColumn.ColumnName].ToString();
                                Int32 sortorder = 0;
                                if (string.IsNullOrEmpty(payrollid) || payrollid == "0")
                                {
                                    sortorder = UtilityMember.NumberSet.ToInteger(drStaff["RECENT_STAFF_ORDER"].ToString());
                                }
                                else
                                {
                                    sortorder = UtilityMember.NumberSet.ToInteger(drStaff[dtstaffgrp.STAFFORDERColumn.ColumnName].ToString());
                                }
                                if (payroll_id > 0 && staffid > 0)
                                {
                                    dtPRStaff.DefaultView.RowFilter = dtcomp.PAYROLLIDColumn.ColumnName + "= " + payroll_id + " AND " + dtcomp.STAFFIDColumn.ColumnName + "=" + staffid;
                                    if (dtPRStaff.DefaultView.Count > 0)
                                    {
                                        DataRow drPayRegister = dtPayRegister.NewRow();
                                        drPayRegister[dtcomp.PAYROLLIDColumn.ColumnName] = payroll_id;
                                        drPayRegister[dtstaffgrp.GROUPNAMEColumn.ColumnName] = groupname;
                                        drPayRegister[dtcomp.STAFFIDColumn.ColumnName] = staffid;
                                        drPayRegister["PRNAME"] = dtPRStaff.DefaultView[0]["PRNAME"].ToString();
                                        drPayRegister["NAME"] = staffname;
                                        string pr = dtPRStaff.DefaultView[0]["PRNAME"].ToString();
                                        drPayRegister[dtstaffgrp.STAFFORDERColumn.ColumnName] = sortorder;
                                        foreach (DataRowView drStaffDetails in dtPRStaff.DefaultView)
                                        {
                                            Int32 staffgroupId = UtilityMember.NumberSet.ToInteger(drStaffDetails["GroupId"].ToString());
                                            string componentname = drStaffDetails["Component"].ToString();
                                            string componentvalue = drStaffDetails["CompValue"].ToString();


                                            if (dtPayRegister.Columns.Contains(componentname))
                                            {
                                                drPayRegister[componentname] = componentvalue;
                                            }
                                        }
                                        dtPayRegister.Rows.Add(drPayRegister);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        resultArg.Message = "Payregister column(s) are not available";
                    }
                }

                if (resultArg.Success)
                {
                    resultArg.DataSource.Data = dtPayRegister;
                    resultArg.Success = true;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
            finally { }
            return resultArg;
        }

        /// <summary>
        /// On 24/02/2022, Generate ESI Register with help of selected earnining Components and ESI Components
        /// 
        /// </summary>
        /// <returns></returns>
        public ResultArgs GenerateStaffESIRegister(string stfGroupIds, string payrollid = "", string selectedcomponentids = "", string SelectedStaffid = "", string datefrom = "", string dateto = "")
        {
            ResultArgs resultArg = new ResultArgs();
            try
            {
                string sCheckField = "";
                string sumEarninings = string.Empty;
                string component = string.Empty;
                ApplicationSchema.PRCOMPMONTHDataTable dtCompMonth = new ApplicationSchema.PRCOMPMONTHDataTable();

                DataTable dtComp = new DataTable();
                DataTable dtPayRegister = new DataTable();
                DataColumn dcSno = new DataColumn("S.No", typeof(Int32));
                dcSno.AutoIncrement = true;
                dcSno.AutoIncrementSeed = 1;
                dcSno.AutoIncrementStep = 1;
                dtPayRegister.Columns.Add(dcSno);
                dtPayRegister.Columns.Add(new DataColumn(dtcomp.PAYROLLIDColumn.ColumnName, typeof(Int32)));
                dtPayRegister.Columns.Add(new DataColumn(dtcomp.PRNAMEColumn.ColumnName, typeof(String)));
                dtPayRegister.Columns.Add(new DataColumn(dtcomp.STAFFIDColumn.ColumnName, typeof(Int32)));
                dtPayRegister.Columns.Add(new DataColumn(dtcomp.NAMEColumn.ColumnName, typeof(String)));
                dtPayRegister.Columns.Add(new DataColumn("ESI IP No", typeof(String)));

                //# Get list of components for given payroll and staff group
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchComponentReport, "Component"))
                {
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    if (!string.IsNullOrEmpty(payrollid) && payrollid != "0")
                    {
                        dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn.ColumnName, payrollid, DataType.Int32);
                    }

                    dataManager.Parameters.Add(dtCompMonth.SALARYGROUPIDColumn.ColumnName, stfGroupIds, DataType.Int32);
                    resultArg = dataManager.FetchData(Bosco.DAO.Data.DataSource.DataTable);
                }

                if (resultArg.Success)
                {
                    //Fix selected Components and have proper order
                    dtComp = resultArg.DataSource.Table;
                    if (!String.IsNullOrEmpty(selectedcomponentids))
                    {
                        dtComp.DefaultView.RowFilter = "Componentid IN (" + selectedcomponentids + ")";
                        //Fix Incomes and Orders (All Components first and then ESI Compontns and Name and ESI No 
                        string reportorder = "IIF(" + dtcomp.PROCESS_COMPONENT_TYPEColumn.ColumnName + " = " + (Int32)PayRollProcessComponent.None +
                                                ", 0, 1)";
                        dtComp.Columns.Add("ReportOrder", typeof(System.Int32), reportorder);
                        dtComp.DefaultView.Sort = "ReportOrder, " + dtcomp.COMP_ORDERColumn.ColumnName;
                        dtComp = dtComp.DefaultView.ToTable();
                    }

                    //# Generate ESI register table with component name as column name and generate ESI register datatable as empty
                    for (int i = 0; i < dtComp.Rows.Count; i++)
                    {
                        Int32 PayrollProcessCompType = UtilityMember.NumberSet.ToInteger(dtComp.Rows[i][dtcomp.PROCESS_COMPONENT_TYPEColumn.ColumnName].ToString());
                        DataColumn dcPayComp = null;
                        if (sCheckField.IndexOf("!" + dtComp.Rows[i]["Component"].ToString() + "!", 0) == -1)
                        {
                            component = dtComp.Rows[i]["Component"].ToString();
                            sCheckField = sCheckField + "!" + dtComp.Rows[i]["component"].ToString() + "!";
                            if ((int.Parse(dtComp.Rows[i]["Type"].ToString()) <= 1 || int.Parse(dtComp.Rows[i]["Type"].ToString()) == 3)
                                && int.Parse(dtComp.Rows[i]["CompRound"].ToString()) >= 0)
                            {   //For Earnings/Deductions/Calcualtions
                                dcPayComp = new DataColumn(component, typeof(double));

                                //On 10/03/2021, to have default value 0 for double datatype columns
                                dcPayComp.DefaultValue = 0;

                                if (int.Parse(dtComp.Rows[i]["Type"].ToString()) == 0 && PayrollProcessCompType != (int)PayRollProcessComponent.ESI)
                                {
                                    sumEarninings += "[" + dcPayComp.Caption + "]+";
                                }
                            }
                            else
                            {
                                dcPayComp = new DataColumn(component, typeof(string));
                            }

                            if (dcPayComp != null)
                            {
                                //To add Total Column before ESI Components
                                if (!string.IsNullOrEmpty(sumEarninings) && PayrollProcessCompType == (Int32)PayRollProcessComponent.ESI)
                                {
                                    sumEarninings = sumEarninings.TrimEnd('+');
                                    dtPayRegister.Columns.Add("TOTAL", typeof(System.Double), "(" + sumEarninings + ")");
                                    sumEarninings = string.Empty;
                                }
                                dtPayRegister.Columns.Add(dcPayComp);
                            }
                        }
                    }

                    if (dtPayRegister.Columns.Count > 0)
                    {
                        //# get satff payroll details for given payroll and given stff group
                        using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchStaffByGroup, "PRStaff"))
                        {
                            dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                            if (!string.IsNullOrEmpty(payrollid) && payrollid != "0")
                            {
                                dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn.ColumnName, payrollid, DataType.Int32);
                            }
                            else
                            {
                                if (!string.IsNullOrEmpty(datefrom))
                                {
                                    dataManager.Parameters.Add(dtCompMonth.FROMDATEColumn.ColumnName, UtilityMember.DateSet.ToDate(datefrom), DataType.Date);
                                }
                                if (!string.IsNullOrEmpty(dateto))
                                {
                                    dataManager.Parameters.Add(dtCompMonth.TODATEColumn.ColumnName, UtilityMember.DateSet.ToDate(dateto), DataType.Date);
                                }
                            }

                            if (!string.IsNullOrEmpty(SelectedStaffid) && SelectedStaffid != "0")
                            {
                                dataManager.Parameters.Add(dtCompMonth.STAFFIDColumn.ColumnName, SelectedStaffid);
                            }

                            using (clsPrGateWay gateway = new clsPrGateWay())
                            {
                                dataManager.Parameters.Add(dtstaffgrp.RECENT_PAYROLL_IDColumn, objPRGW.GetCurrentPayroll(), DataType.Int32);
                            }

                            dataManager.Parameters.Add(dtCompMonth.GROUPIDColumn.ColumnName, stfGroupIds, DataType.Int32);
                            dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                            resultArg = dataManager.FetchData(Bosco.DAO.Data.DataSource.DataTable);
                        }

                        if (resultArg.Success)
                        {
                            DataTable dtPRStaff = resultArg.DataSource.Table;

                            // # Read grid data and seperate them according to record vice using seperators
                            //# Make it single staff
                            DataTable dtStaffIds = dtPRStaff.DefaultView.ToTable(true, new string[] { "PAYROLLID", "STAFFID" });

                            foreach (DataRow drStaff in dtStaffIds.Rows)
                            {
                                Int32 payroll_id = UtilityMember.NumberSet.ToInteger(drStaff[dtcomp.PAYROLLIDColumn.ColumnName].ToString());
                                Int32 staffid = UtilityMember.NumberSet.ToInteger(drStaff[dtcomp.STAFFIDColumn.ColumnName].ToString());
                                if (payroll_id > 0 && staffid > 0)
                                {
                                    dtPRStaff.DefaultView.RowFilter = dtcomp.PAYROLLIDColumn.ColumnName + "= " + payroll_id + " AND " + dtcomp.STAFFIDColumn.ColumnName + "=" + staffid;
                                    if (dtPRStaff.DefaultView.Count > 0)
                                    {
                                        DataRow drPayRegister = dtPayRegister.NewRow();
                                        drPayRegister[dtcomp.PAYROLLIDColumn.ColumnName] = payroll_id;
                                        drPayRegister[dtcomp.STAFFIDColumn.ColumnName] = staffid;
                                        drPayRegister["PRNAME"] = dtPRStaff.DefaultView[0]["PRNAME"].ToString();
                                        foreach (DataRowView drStaffDetails in dtPRStaff.DefaultView)
                                        {
                                            Int32 staffgroupId = UtilityMember.NumberSet.ToInteger(drStaffDetails["GroupId"].ToString());
                                            string componentname = drStaffDetails["Component"].ToString();
                                            string componentvalue = drStaffDetails["CompValue"].ToString();

                                            if (dtPayRegister.Columns.Contains(componentname))
                                            {
                                                drPayRegister[componentname] = componentvalue;
                                                drPayRegister["ESI IP NO"] = drStaffDetails["ESI_IP_NO"].ToString(); ;
                                            }
                                        }
                                        dtPayRegister.Rows.Add(drPayRegister);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        resultArg.Message = "Payregister column(s) are not available";
                    }
                }

                if (resultArg.Success)
                {
                    resultArg.DataSource.Data = dtPayRegister;
                    resultArg.Success = true;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
            finally { }
            return resultArg;
        }


        /// <summary>
        /// On 02/03/2022, Generate Staff EPF Format2 Register with help of selected earnining components and EPF (Employee and Employer) share ompontents
        /// </summary>
        /// <returns></returns>
        public ResultArgs GenerateStaffEPFFormat2(string stfGroupIds, string payrollid = "", string selEarningstids = "", string selEmployeecomponentids = "", string selEmployercomponentids = "",
                    string SelectedStaffid = "", string datefrom = "", string dateto = "")
        {
            ResultArgs resultArg = new ResultArgs();
            try
            {
                string sumEarninings = string.Empty;
                string component = string.Empty;
                ApplicationSchema.PRCOMPMONTHDataTable dtCompMonth = new ApplicationSchema.PRCOMPMONTHDataTable();

                DataTable dtComp = new DataTable();
                DataTable dtPayRegister = new DataTable();
                DataColumn dcSno = new DataColumn("S.No", typeof(Int32));
                dcSno.AutoIncrement = true;
                dcSno.AutoIncrementSeed = 1;
                dcSno.AutoIncrementStep = 1;
                dtPayRegister.Columns.Add(dcSno);
                dtPayRegister.Columns.Add(new DataColumn(dtcomp.PAYROLLIDColumn.ColumnName, typeof(Int32)));
                dtPayRegister.Columns.Add(new DataColumn(dtcomp.PRNAMEColumn.ColumnName, typeof(String)));
                dtPayRegister.Columns.Add(new DataColumn(dtcomp.STAFFIDColumn.ColumnName, typeof(Int32)));
                dtPayRegister.Columns.Add(new DataColumn("UAN NUMBER", typeof(String)));
                dtPayRegister.Columns.Add(new DataColumn(dtcomp.NAMEColumn.ColumnName, typeof(String)));

                //# 1. Get list of components for given payroll and staff group
                resultArg = GenerateRegisterColumns(dtPayRegister, stfGroupIds, payrollid, selEarningstids);
                if (resultArg.Success)
                {
                    //2. Employee component
                    resultArg = GenerateRegisterColumns(dtPayRegister, stfGroupIds, payrollid, selEmployeecomponentids);
                    if (resultArg.Success)
                    {
                        //Total - EmployeeShare Share --------------
                        string EmployeeShare = (resultArg.ReturnValue == null ? string.Empty : resultArg.ReturnValue.ToString());
                        if (!string.IsNullOrEmpty(EmployeeShare))
                        {
                            EmployeeShare = "(" + EmployeeShare.Replace("!", "+") + ")";
                            dtPayRegister.Columns.Add("Total Employee", typeof(System.Double), EmployeeShare);
                        }
                        //--------------------------------------------

                        //3. Employercomponent
                        resultArg = GenerateRegisterColumns(dtPayRegister, stfGroupIds, payrollid, selEmployercomponentids);
                        if (resultArg.Success && dtPayRegister.Columns.Count > 0)
                        {
                            //Total - Employer Share --------------------
                            string EmployerShare = (resultArg.ReturnValue == null ? string.Empty : resultArg.ReturnValue.ToString());
                            if (!string.IsNullOrEmpty(EmployerShare))
                            {
                                EmployerShare = "(" + EmployerShare.Replace("!", "+") + ")";
                            }
                            dtPayRegister.Columns.Add("Total Employer", typeof(System.Double), EmployerShare);
                            //--------------------------------------------

                            //# get satff payroll details for given payroll and given stff group
                            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchStaffByGroup, "PRStaff"))
                            {
                                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                                if (!string.IsNullOrEmpty(payrollid) && payrollid != "0")
                                {
                                    dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn.ColumnName, payrollid, DataType.Int32);
                                }
                                else
                                {
                                    if (!string.IsNullOrEmpty(datefrom))
                                    {
                                        dataManager.Parameters.Add(dtCompMonth.FROMDATEColumn.ColumnName, UtilityMember.DateSet.ToDate(datefrom), DataType.Date);
                                    }
                                    if (!string.IsNullOrEmpty(dateto))
                                    {
                                        dataManager.Parameters.Add(dtCompMonth.TODATEColumn.ColumnName, UtilityMember.DateSet.ToDate(dateto), DataType.Date);
                                    }
                                }

                                if (!string.IsNullOrEmpty(SelectedStaffid) && SelectedStaffid != "0")
                                {
                                    dataManager.Parameters.Add(dtCompMonth.STAFFIDColumn.ColumnName, SelectedStaffid);
                                }

                                using (clsPrGateWay gateway = new clsPrGateWay())
                                {
                                    dataManager.Parameters.Add(dtstaffgrp.RECENT_PAYROLL_IDColumn, objPRGW.GetCurrentPayroll(), DataType.Int32);
                                }

                                dataManager.Parameters.Add(dtCompMonth.GROUPIDColumn.ColumnName, stfGroupIds, DataType.Int32);
                                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                                resultArg = dataManager.FetchData(Bosco.DAO.Data.DataSource.DataTable);
                            }

                            if (resultArg.Success)
                            {
                                DataTable dtPRStaff = resultArg.DataSource.Table;

                                // # Read grid data and seperate them according to record vice using seperators
                                //# Make it single staff
                                DataTable dtStaffIds = dtPRStaff.DefaultView.ToTable(true, new string[] { "PAYROLLID", "STAFFID", "Name" });

                                //14/03/2022, Make STaff Order
                                //dtStaffIds.DefaultView.Sort = "Name";
                                //dtStaffIds = dtStaffIds.DefaultView.ToTable();

                                foreach (DataRow drStaff in dtStaffIds.Rows)
                                {
                                    Int32 payroll_id = UtilityMember.NumberSet.ToInteger(drStaff[dtcomp.PAYROLLIDColumn.ColumnName].ToString());
                                    Int32 staffid = UtilityMember.NumberSet.ToInteger(drStaff[dtcomp.STAFFIDColumn.ColumnName].ToString());
                                    if (payroll_id > 0 && staffid > 0)
                                    {
                                        dtPRStaff.DefaultView.RowFilter = dtcomp.PAYROLLIDColumn.ColumnName + "= " + payroll_id + " AND " + dtcomp.STAFFIDColumn.ColumnName + "=" + staffid;
                                        if (dtPRStaff.DefaultView.Count > 0)
                                        {
                                            DataRow drPayRegister = dtPayRegister.NewRow();
                                            drPayRegister[dtcomp.PAYROLLIDColumn.ColumnName] = payroll_id;
                                            drPayRegister[dtcomp.STAFFIDColumn.ColumnName] = staffid;
                                            drPayRegister["PRNAME"] = dtPRStaff.DefaultView[0]["PRNAME"].ToString();
                                            foreach (DataRowView drStaffDetails in dtPRStaff.DefaultView)
                                            {
                                                Int32 staffgroupId = UtilityMember.NumberSet.ToInteger(drStaffDetails["GroupId"].ToString());
                                                string componentname = drStaffDetails["Component"].ToString();
                                                string componentvalue = drStaffDetails["CompValue"].ToString();

                                                if (dtPayRegister.Columns.Contains(componentname))
                                                {
                                                    drPayRegister[componentname] = componentvalue;
                                                    drPayRegister["UAN NUMBER"] = drStaffDetails["UAN"].ToString(); ;
                                                }
                                            }
                                            dtPayRegister.Rows.Add(drPayRegister);
                                        }
                                    }
                                }
                            }
                        }
                        else
                        {
                            resultArg.Message = "Payregister column(s) are not available (" + resultArg.Message + ")";
                        }
                    }
                    else
                    {
                        resultArg.Message = "Payregister column(s) are not available (" + resultArg.Message + ")";
                    }
                }

                if (resultArg.Success)
                {
                    resultArg.DataSource.Data = dtPayRegister;
                    resultArg.Success = true;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
            finally { }
            return resultArg;
        }


        private ResultArgs GenerateRegisterColumns(DataTable dtReport, string stfGroupIds, string payrollid = "", string selectedcomponentids = "")
        {
            ResultArgs resultArg = new ResultArgs();
            string attachedColumns = string.Empty;
            ApplicationSchema.PRCOMPMONTHDataTable dtCompMonth = new ApplicationSchema.PRCOMPMONTHDataTable();

            //# Get list of components for given payroll and staff group
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchComponentReport, "Component"))
            {
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                if (!string.IsNullOrEmpty(payrollid) && payrollid != "0")
                {
                    dataManager.Parameters.Add(dtCompMonth.PAYROLLIDColumn.ColumnName, payrollid, DataType.Int32);
                }

                dataManager.Parameters.Add(dtCompMonth.SALARYGROUPIDColumn.ColumnName, stfGroupIds, DataType.Int32);
                resultArg = dataManager.FetchData(Bosco.DAO.Data.DataSource.DataTable);
            }

            if (resultArg.Success)
            {
                //Fix selected components and have proper order
                DataTable dtComp = resultArg.DataSource.Table;
                if (!String.IsNullOrEmpty(selectedcomponentids))
                {
                    dtComp.DefaultView.RowFilter = "Componentid IN (" + selectedcomponentids + ")";
                    dtComp = dtComp.DefaultView.ToTable();

                    //# Generate ESI register table with component name as column name and generate ESI register datatable as empty
                    for (int i = 0; i < dtComp.Rows.Count; i++)
                    {
                        Int32 PayrollProcessCompType = UtilityMember.NumberSet.ToInteger(dtComp.Rows[i][dtcomp.PROCESS_COMPONENT_TYPEColumn.ColumnName].ToString());
                        DataColumn dcPayComp = null;

                        string component = dtComp.Rows[i]["Component"].ToString();
                        if ((int.Parse(dtComp.Rows[i]["Type"].ToString()) <= 1 || int.Parse(dtComp.Rows[i]["Type"].ToString()) == 3)
                            && int.Parse(dtComp.Rows[i]["CompRound"].ToString()) >= 0)
                        {   //For Earnings/Deductions/Calcualtions
                            dcPayComp = new DataColumn(component, typeof(double));

                            //On 10/03/2021, to have default value 0 for double datatype columns
                            dcPayComp.DefaultValue = 0;
                        }
                        else
                        {
                            dcPayComp = new DataColumn(component, typeof(string));
                        }
                        if (!dtReport.Columns.Contains(dcPayComp.ColumnName))
                        {
                            dtReport.Columns.Add(dcPayComp);
                            attachedColumns = attachedColumns + "[" + component + "]!";
                        }
                    }
                }
            }
            attachedColumns = attachedColumns.Trim('!');
            resultArg.ReturnValue = attachedColumns;
            return resultArg;
        }
        #endregion

        #region Loan Ledger Report
        //public ResultArgs LoanRegisterReport()
        //{
        //    try
        //    {
        //        string LoanQuery = string.Empty;
        //        if (objPRReport.CreateReportTable())
        //        {
        //            LoanQuery = objPRReport.GetReportSQL("RPT-LLED02", "", "");
        //            using (DataManager dataManager = new DataManager())
        //            {
        //                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
        //                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
        //                resultargs = dataManager.FetchData(Bosco.DAO.Data.DataSource.DataTable, LoanQuery);
        //                // objPRReport.DropReportTable();
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageRender.ShowMessage(ex.Message.ToString());
        //    }
        //    finally
        //    {
        //        objPRReport.DropReportTable();
        //    }
        //    return resultargs;
        //}

        public ResultArgs LoanRegisterReport()
        {
            try
            {
                string LoanQuery = string.Empty;

                LoanQuery = objPRReport.GetReportSQL("RPT-LLED02", "", "");
                using (DataManager dataManager = new DataManager())
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultargs = dataManager.FetchData(Bosco.DAO.Data.DataSource.DataTable, LoanQuery);
                }

            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message.ToString());
            }
            finally
            {

            }
            return resultargs;
        }
        #endregion

        #region Abstract Component Report
        public ResultArgs AbstractComponentReport()
        {
            try
            {
                if (objPRReport.CreateReportTable())
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPayrollAbstractComponent))
                    {
                        dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                        dataManager.Parameters.Add(dtcomp.IDsColumn, payrollCompId);
                        dataManager.Parameters.Add(dtCompMonth.GroupIdColumn, PAYROLL_GROUP_ID);
                        dataManager.Parameters.Add(dtcomp.PAYROLLIDColumn, PAYROLL_Id);
                        if (SettingProperty.PayrollFinanceEnabled)
                        {
                            dataManager.Parameters.Add(dtcomp.PROJECT_IDColumn, projectid);
                        }
                        dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                        resultargs = dataManager.FetchData(Bosco.DAO.Data.DataSource.DataTable);
                        objPRReport.DropReportTable();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message.ToString());
            }
            return resultargs;
        }
        #endregion

        #region Customize Report
        public DataView AbstractCustomizeReport()
        {
            DataView dvPRCustomize = new DataView();
            try
            {
                clsPRPSReport objPRCustomize = new clsPRPSReport();
                dvPRCustomize = objPRCustomize.CreateReportTable();
                PAYROLL_STAFF_ID = !string.IsNullOrEmpty(PAYROLL_STAFF_ID) ? PAYROLL_STAFF_ID : "0";
                payrollgroupid = !string.IsNullOrEmpty(payrollgroupid) ? payrollgroupid : "0";
                dvPRCustomize.RowFilter = "STAFFID IN (" + PAYROLL_STAFF_ID + ") AND GROUPID IN(" + payrollgroupid + ") ";

                string[] sSelectedComponents = new string[payrollCompId.Split(',').Count()];
                string[] sEdit = new string[dvPRCustomize.Table.Columns.Count];
                string[] sStaffList = new string[PAYROLL_STAFF_ID.Split(',').Count()];



                //Get all selected components to filter columns
                string[] paycomp = payrollcomponent.Split(',');
                for (int i = 0; i < paycomp.Count(); i++)
                    sSelectedComponents[i] = paycomp[i];

                //Get all the columns in the dataview to filter
                for (int i = 0; i < dvPRCustomize.Table.Columns.Count; i++)
                    sEdit[i] = dvPRCustomize.Table.Columns[i].ColumnName;
                //			if(cboSalaryGroup.Text == "Department Wise")
                //				dvPRCustomize.RowFilter = "GroupId = " + cboGroupValue.SelectedValue.ToString();

                //Remove unwanted colunms from dvPRCustomize
                int iExists = 0;
                for (int i = 0; i < sEdit.Length; i++)
                {
                    for (int j = 0; j < sSelectedComponents.Length; j++)
                    {
                        if (sEdit[i] == sSelectedComponents[j])
                            iExists++;
                    }

                    if (iExists == 0)
                        dvPRCustomize.Table.Columns.Remove(sEdit[i]);

                    iExists = 0;
                }

                DataView dv = dvPRCustomize;

                //Insert all the filtered rows and columns into DataTable
                DataTable dtPRCustom = new DataTable();

                //Add rows and coloumns of components as it is in the checked list box into dtPRCustom table (order of components).
                for (int i = 0; i < sSelectedComponents.Length; i++)
                {
                    DataColumn dc = new DataColumn(sSelectedComponents[i].ToString());
                    dtPRCustom.Columns.Add(dc);
                }
                for (int i = 0; i < dvPRCustomize.Count; i++)
                {
                    DataRow dr = dtPRCustom.NewRow();
                    for (int j = 0; j < sSelectedComponents.Length; j++)
                    {
                        if (dvPRCustomize.Table.Columns.Contains(sSelectedComponents[j].ToString()))
                        {
                            if (dvPRCustomize[i][sSelectedComponents[j].ToString()].ToString() == "" && dvPRCustomize.Table.Columns[sSelectedComponents[j].ToString()].DataType.Name == "Int32")
                                dr[sSelectedComponents[j].ToString()] = "0";
                            else if (dvPRCustomize[i][sSelectedComponents[j].ToString()].ToString() != "" & dvPRCustomize.Table.Columns[sSelectedComponents[j].ToString()].DataType.Name == "Int32")
                                dr[sSelectedComponents[j].ToString()] = int.Parse(dvPRCustomize[i][sSelectedComponents[j].ToString()].ToString()).ToString();
                            else if (dvPRCustomize[i][sSelectedComponents[j].ToString()].ToString() == "" && dvPRCustomize.Table.Columns[sSelectedComponents[j].ToString()].DataType.Name == "Double")
                                dr[sSelectedComponents[j].ToString()] = "0.00";
                            else if (dvPRCustomize[i][sSelectedComponents[j].ToString()].ToString() != "" && dvPRCustomize.Table.Columns[sSelectedComponents[j].ToString()].DataType.Name == "Double")
                                dr[sSelectedComponents[j].ToString()] = double.Parse(dvPRCustomize[i][sSelectedComponents[j].ToString()].ToString()).ToString("#,##0.00");
                            else
                                dr[sSelectedComponents[j].ToString()] = dvPRCustomize[i][sSelectedComponents[j].ToString()];
                        }
                    }
                    dtPRCustom.Rows.Add(dr);
                }


                string strColTotal = "@";
                for (int i = 0; i < dvPRCustomize.Table.Columns.Count; i++)
                {
                    if (dvPRCustomize.Table.Columns[i].DataType.Name == "Double" || dvPRCustomize.Table.Columns[i].DataType.Name == "Int32")
                    {
                        strColTotal = strColTotal + dvPRCustomize.Table.Columns[i].ColumnName + "@";
                    }
                }
                clsGeneral.PAYROLL_CUSTOMIZED_REPORT = true;
                clsGeneral.PAYROLL_RPT_COL_TOTAL_FIELDS = strColTotal;

            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message.ToString());
            }
            return dvPRCustomize;
        }
        #endregion

        public ResultArgs FetchPayrollComponent(string GroupIds, string payrollId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchComponentByGroupIds))
            {
                dataManager.Parameters.Add(dtcomp.IDsColumn, GroupIds);
                dataManager.Parameters.Add(dtcomp.PAYROLLIDColumn, payrollId);
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultargs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultargs;
        }
        public ResultArgs FetchPayrollStaff(string GroupIds, string payrollId, string rptProjid)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchStaffByGroupIds))
            {
                if (GroupIds.Trim() != "0")
                {
                    dataManager.Parameters.Add(dtstaffgrp.IDsColumn, GroupIds);
                }

                if (!string.IsNullOrEmpty(payrollId))
                {
                    dataManager.Parameters.Add(dtstaffgrp.PAYROLLIDColumn, payrollId);
                    dataManager.Parameters.Add(dtstaffgrp.RECENT_PAYROLL_IDColumn, payrollId, DataType.Int32);
                }

                using (clsPrGateWay gateway = new clsPrGateWay())
                {
                    dataManager.Parameters.Add(dtstaffgrp.RECENT_PAYROLL_IDColumn, objPRGW.GetCurrentPayroll(), DataType.Int32);
                }
                
                if (!string.IsNullOrEmpty(rptProjid))
                {
                    dataManager.Parameters.Add(dtCompMonth.PROJECT_IDColumn, rptProjid);
                }
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultargs = dataManager.FetchData(DataSource.DataTable);
            }
            return resultargs;
        }


        public bool UpdatePrstaffGroupByPayrollId()
        {
            try
            {
                using (DataManager dtManager = new DataManager(SQLCommand.Payroll.UpdatePRStaffGroupByPayrollId))
                {
                    dtManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dtManager.Parameters.Add(dtcomp.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                    dtManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultargs = dtManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            return resultargs.Success;
        }


        /// <summary>
        /// On 11/12/2024 to remove default componetns like
        /// 'DA', 'HRA', 'EPF', 'GROSS WAGES', 'DEDUCTIONS', 'NETPAY', 'LOPDays'
        /// </summary>
        public ResultArgs RemoveDefaultComponentsForMultiCurrency()
        {
            try
            {
                resultargs = new ResultArgs();
                if (appsetting.AllowMultiCurrency == 1)
                {
                    using (DataManager dataManager = new DataManager(SQLCommand.Payroll.RemoveDefaultComponentsForMultiCurrency))
                    {
                        dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;

                        resultargs = dataManager.FetchData(Bosco.DAO.Data.DataSource.DataView);
                    }
                }
                else
                {
                    resultargs.Success = true;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message.ToString());
            }
            return resultargs;
        }

        #endregion


        public void Dispose()
        {

        }
    }
}
