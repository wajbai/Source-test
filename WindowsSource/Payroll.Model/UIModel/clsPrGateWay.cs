using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Linq;
using DevExpress.XtraEditors;
using Bosco.Utility;
using Bosco.DAO.Data;
using Payroll.DAO.Schema;
using System.Windows.Forms;
using DataGridCustomCellFont;
using System.Drawing;
using Bosco.Utility.Common;
using Bosco.Utility.ConfigSetting;
using Bosco.Model.UIModel;
using Bosco.Model.Transaction;
using AcMEDSync.Model;

namespace Payroll.Model.UIModel
{
    public class clsPrGateWay : SystemBase
    {
        ApplicationSchema.PayrollDataTable dtPayroll = null;
        ApplicationSchema.PRPROJECT_STAFFDataTable dtProjectStaff = new ApplicationSchema.PRPROJECT_STAFFDataTable();
        ApplicationSchema.PRCOMPMONTHDataTable dtprcomponent = new ApplicationSchema.PRCOMPMONTHDataTable();
        ApplicationSchema.PAYROLL_FINANCEDataTable dtpayrollFinance = new ApplicationSchema.PAYROLL_FINANCEDataTable();
        //public string[] LockingComponentsForOtherCoutnry = new string[] { "EPF", "ESI", "DA", "LOPDays" };

        public clsPrGateWay()
        {
            dtPayroll = this.AppSchema.Payroll;
            //  createDataSet("SELECT * FROM PRCreate Order by Payrollid Asc", "PRCreate");
            FetchRecord(SQLCommand.Payroll.FetchPrGateWay, "PRCreate");
        }
        public clsPrGateWay(int id)
        {
            FillPostPaymentsetails(id);
        }
        ResultArgs resultArgs = null;
        private DataTable dsetPRComp = new DataTable();
        private DataTable dsetPRCreate = new DataTable();
        private DataTable dsetOther = new DataTable();
        private DataTable dtable = new DataTable();

        private string mvarPRName = "";
        private string mvarPRDate = "";
        private string mvarCompImport = "";
        private string mvarPRImport = "";
        private long mvarPayRollId;
        private long mvarPRPrevId;
        private string mvarPRLocked = "";
        private string fromDate = string.Empty;
        private string toDate = string.Empty;
        private string sSql = "";

        #region Properties
        public string PRLocked
        {
            set { mvarPRLocked = value; }
            get { return mvarPRLocked; }
        }
        public string PRImport
        {
            set { mvarPRImport = value; }
            get { return mvarPRImport; }
        }
        public string CompImport
        {
            set { mvarCompImport = value; }
            get { return mvarCompImport; }
        }
        public long PRPrevId
        {
            set { mvarPRPrevId = value; }
            get { return mvarPRPrevId; }
        }
        public string PRDate   //DateTime
        {
            set { mvarPRDate = value; }
            get { return mvarPRDate; }
        }
        public string PRName
        {
            set { mvarPRName = value; }
            get { return mvarPRName; }
        }
        public long PayRollId
        {
            set { mvarPayRollId = value; }
            get { return mvarPayRollId; }
        }
        public string FromDate
        {
            set { fromDate = value; }
            get { return fromDate; }
        }
        public string ToDate
        {
            set { toDate = value; }
            get { return toDate; }
        }
        #endregion Properties

        #region PostPaymentProperties

        public long PostedPayrollId { get; set; }
        public DateTime postDate { get; set; }
        public int VoucherId { get; set; }
        public string VoucherNo { get; set; }
        public int ProjectId { get; set; }
        public int PayrollGroupId { get; set; }
        public string PayrollGroupIds { get; set; }
        public int CashBankLedgerId { get; set; }
        public decimal PostAmount { get; set; }
        public string RefChequeDDNumber { get; set; }
        public string Narration { get; set; }
        public DataTable dtPayrollVoucherDetails { get; set; }
        public DataTable dtPayrollComponentDetails { get; set; }

        #endregion

        /*    Purpose: This method is used to Create the new payroll the Condition is                                
		 *    1.If ne                           w payroll the selectiong month may be current month or next month or less than current month    
		 *    2.If anthing already exist then the next month of the previous payroll only created.                   
		 */
        public int GetCurrentPayroll()
        {
            DataTable dtCurrentPayroll = null;
            using (DataManager dataManager = new DataManager((SQLCommand.Payroll.GetCurrentPayroll)))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);

                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    dtCurrentPayroll = resultArgs.DataSource.Table;
                    if (!string.IsNullOrEmpty(dtCurrentPayroll.Rows[0]["PRId"].ToString()))
                    {
                        return this.NumberSet.ToInteger(dtCurrentPayroll.Rows[0]["PRId"].ToString());
                    }

                }
            }
            return 0;
        }

        /// <summary>
        /// On 07/02/2023, To get latest payroll id
        /// </summary>
        /// <returns></returns>
        public ResultArgs GetLatestPayroll()
        {
            ResultArgs result = new ResultArgs();
            using (DataManager dataManager = new DataManager((SQLCommand.Payroll.GetLatestPayroll)))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            return result;
        }

        /// <summary>
        /// On 07/02/2023, To get latest payroll id
        /// </summary>
        /// <returns></returns>
        public Int32 GetLatestPayrollId()
        {
            Int32 payrollid = 0;
            ResultArgs result = GetLatestPayroll();
                        
            if (resultArgs.Success && resultArgs.RowsAffected > 0)
            {
                DataTable dtLatestPayroll = resultArgs.DataSource.Table;
                if (!string.IsNullOrEmpty(dtLatestPayroll.Rows[0][dtPayroll.PAYROLLIDColumn.ColumnName].ToString()))
                {
                    payrollid = this.NumberSet.ToInteger(dtLatestPayroll.Rows[0][dtPayroll.PAYROLLIDColumn.ColumnName].ToString());
                }
            }
            
            return payrollid;
        }

        string sInSql = "";
        public bool CreatePayRoll()
        {
            DataTable dsetPRCreate = null;
            DataManager dataManager;
            try
            {

                dataManager = new DataManager((SQLCommand.Payroll.PayrollAdd));
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtPayroll.PRDATEColumn, DateTime.Parse(mvarPRDate).ToString(clsGeneral.DATE_FORMAT));
                dataManager.Parameters.Add(dtPayroll.PRNAMEColumn, mvarPRName);
                resultArgs = dataManager.UpdateData();

                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    dataManager = new DataManager(SQLCommand.Payroll.FetchPayrollGateWay, "PRCreate");
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success && resultArgs.RowsAffected > 0)
                    {
                        dsetPRCreate = resultArgs.DataSource.Table;
                    }
                    mvarPayRollId = GetCurrentPayroll();
                    if (mvarPayRollId == 0)
                    {
                        mvarPayRollId = long.Parse(dsetPRCreate.Rows[0]["PayRollId"].ToString());
                    }
                    mvarPRLocked = "N";
                    UpdateStatus();
                    UpdateDateInterval();
                    return true;
                }
                return false;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return false;
            }
        }

        //Purpose    : This method is used to Update the Payroll Status
        private bool UpdateStatus()
        {
            // sSql = "INSERT INTO PRStatus (PayRollId,CompCreated,Lockedstatus) VALUES ('" + mvarPayRollId + "','" + mvarCompImport + "','" + mvarPRLocked + "')";

            using (DataManager dataManager = new DataManager((SQLCommand.Payroll.UpdatePayrollStatus)))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtPayroll.PAYROLLIDColumn, mvarPayRollId);
                dataManager.Parameters.Add(dtPayroll.COMPCREATEDColumn, mvarCompImport);
                dataManager.Parameters.Add(dtPayroll.LOCKEDSTATUSColumn, mvarPRLocked);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs.Success;
        }
        //Purpose    : This method is used to Update the Payroll Status
        private bool UpdateDateInterval()
        {
            // sSql = "INSERT INTO PRStatus (PayRollId,CompCreated,Lockedstatus) VALUES ('" + mvarPayRollId + "','" + mvarCompImport + "','" + mvarPRLocked + "')";

            using (DataManager dataManager = new DataManager((SQLCommand.Payroll.PayrollDetailsAdd)))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtPayroll.PAYROLLIDColumn, mvarPayRollId);
                dataManager.Parameters.Add(dtPayroll.FROMDATEColumn, clsGeneral.GetMySQLDateTime(FromDate, DateDataType.DateTime));
                dataManager.Parameters.Add(dtPayroll.TODATEColumn, clsGeneral.GetMySQLDateTime(ToDate, DateDataType.DateTime));
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs.Success;
        }
        private void column_SetDataGridCellFormat(object sender, DataGridCustomEventArgs e)
        {
            if (e.cellData == "ADDITION")
            {
                e.g.FillRectangle(e.backBrush, e.bounds.X, e.bounds.Y, e.bounds.Width, e.bounds.Height);

                System.Drawing.Font font = new Font(System.Drawing.FontFamily.GenericSansSerif, (float)9, FontStyle.Bold);
                e.g.DrawString(e.cellData, font, Brushes.Black, e.bounds.X - 2, e.bounds.Y - 3);
                e.isRendered = true;

            }
            if (e.cellData == "DEDUCTION")
            {
                e.g.FillRectangle(e.backBrush, e.bounds.X, e.bounds.Y, e.bounds.Width, e.bounds.Height);

                System.Drawing.Font font = new Font(System.Drawing.FontFamily.GenericSansSerif, (float)9, FontStyle.Bold);
                e.g.DrawString(e.cellData, font, Brushes.Black, e.bounds.X - 2, e.bounds.Y - 3);
                e.isRendered = true;

            }
            if (e.cellData == "Total Deduction is")
            {
                e.g.FillRectangle(e.backBrush, e.bounds.X, e.bounds.Y, e.bounds.Width, e.bounds.Height);

                System.Drawing.Font font = new Font(System.Drawing.FontFamily.GenericSansSerif, (float)9, FontStyle.Bold);
                e.g.DrawString(e.cellData, font, Brushes.Black, e.bounds.X - 2, e.bounds.Y - 3);
                e.isRendered = true;

            }
            if (e.cellData == "Total Amount is")
            {
                e.g.FillRectangle(e.backBrush, e.bounds.X, e.bounds.Y, e.bounds.Width, e.bounds.Height);

                System.Drawing.Font font = new Font(System.Drawing.FontFamily.GenericSansSerif, (float)9, FontStyle.Bold);
                e.g.DrawString(e.cellData, font, Brushes.Black, e.bounds.X - 2, e.bounds.Y - 3);
                e.isRendered = true;

            }
        }
        //Purpose : To add DataTable Columns

        private DataTable addColumns()
        {
            DataTable dt = new DataTable();
            DataColumnCollection columnCollection = dt.Columns;
            columnCollection.Add("Component", typeof(string));
            columnCollection.Add("Amount", typeof(string));
            return dt;
        }
        // Purpose : To Display the Payroll information in the Grid

        public void ShowPayRollAbstract(DataGrid objGrid, bool bDeptSel, string sId, int ProjectId = 0)
        {
            double dDeduction = 0.0;
            DataSet dsPay = new DataSet();
            if (sId.Trim() == "")
                sId = "0";

            try
            {
                object sqlQuery = string.Empty;
                if (bDeptSel)
                {
                    sqlQuery = SQLCommand.Payroll.FetchPayrollComponentByDept;
                }
                else
                {
                    sqlQuery = SQLCommand.Payroll.FetchPayrollComponentByGroup;
                }
                using (DataManager dataManager = new DataManager(sqlQuery, "PRCOMPONENT"))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    dataManager.Parameters.Add(dtPayroll.GROUPIDSColumn, sId);
                    if (ProjectId != 0)
                    {
                        dataManager.Parameters.Add(dtProjectStaff.PROJECT_IDColumn, ProjectId);
                    }
                    dataManager.Parameters.Add(dtPayroll.PAYROLLIDColumn, mvarPayRollId);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        dsetPRComp = resultArgs.DataSource.Table;
                    }
                    else
                    {
                        dsetPRComp.Clear();
                    }
                }

                dtable = addColumns();
                DataRow dr1 = null;
                dr1 = dtable.NewRow();
                dr1[0] = "EARNING";
                dtable.Rows.Add(dr1);
                //DataRow dr2 = null;
                //dr2 = dtable.NewRow();
                //dtable.Rows.Add(dr2);
                double iSum = 0.0;

                if (dsetPRComp.Rows.Count > 0)
                {
                    foreach (DataRow dr in dsetPRComp.Rows)
                    {
                        DataRow drow;
                        drow = dtable.NewRow();
                        drow[0] = dr[0].ToString();
                        drow[1] = Double.Parse(dr[1].ToString()).ToString("#,##,##,##,##0.00");
                        iSum += Double.Parse(dr[1].ToString());
                        dtable.Rows.Add(drow);
                    }
                }
                DataRow drTotal = null;
                drTotal = dtable.NewRow();
                drTotal[0] = "TOTAL EARNINGS";
                drTotal[1] = iSum.ToString("#,##,##,##,##0.00");
                dtable.Rows.Add(drTotal);

                object sqlGroupQuery = string.Empty;
                if (bDeptSel)
                {
                    sqlGroupQuery = SQLCommand.Payroll.FetchPayrollGroupByDept;
                }
                else
                {
                    sqlGroupQuery = SQLCommand.Payroll.FetchPayrollGroupByGroup;
                }
                using (DataManager dataManager = new DataManager(sqlGroupQuery, "PAYROLLGROUP"))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    dataManager.Parameters.Add(dtPayroll.GROUPIDSColumn, sId);
                    if (ProjectId != 0)
                    {
                        dataManager.Parameters.Add(dtProjectStaff.PROJECT_IDColumn, ProjectId);
                    }
                    dataManager.Parameters.Add(dtPayroll.PAYROLLIDColumn, mvarPayRollId);
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        dsetPRComp = resultArgs.DataSource.Table;
                    }
                    else
                    {
                        dsetPRComp.Clear();
                    }
                }


                DataRow dr4 = null;
                dr4 = dtable.NewRow();
                dtable.Rows.Add(dr4);

                DataRow dr3 = null;
                dr3 = dtable.NewRow();
                dr3[0] = "DEDUCTION";
                dtable.Rows.Add(dr3);

                //DataRow dr5 = null;
                //dr5 = dtable.NewRow();
                //dtable.Rows.Add(dr5);

                if (dsetPRComp.Rows.Count > 0)
                {
                    foreach (DataRow dr in dsetPRComp.Rows)
                    {
                        DataRow dsample;
                        double Deduction;
                        dsample = dtable.NewRow();
                        double Amt = NumberSet.ToDouble(dr[1].ToString());
                        if (Amt <= 0)
                        {
                            // dtable.Rows.Remove(dr);
                            dr.Delete();
                        }
                        else
                        {

                            dsample[0] = dr[0].ToString();
                            dsample[1] = Double.Parse(dr[1].ToString()).ToString("#,##,##,##,##0.00");
                            Double.TryParse(dr[1].ToString(), out Deduction);
                            dDeduction += Deduction;

                        }
                        if (!dsample.IsNull("Component") && !dsample.IsNull("Amount"))
                        {
                            dtable.Rows.Add(dsample);
                        }
                    }
                }
                DataRow dTotal = null;
                //foreach (DataRow dr2 in dtable.Rows)
                //{
                //    if (string.IsNullOrEmpty(dr2["Amount"].ToString()))
                //        dtable.Rows.Remove(dr2);
                //}
                dTotal = dtable.NewRow();
                dTotal[0] = "TOTAL DEDUCTIONS";
                dTotal[1] = dDeduction.ToString("#,##,##,##,##0.00");
                dtable.Rows.Add(dTotal);


                dtable.Rows.Add(dtable.NewRow());
                DataRow drNetPay = null;
                drNetPay = dtable.NewRow();
                drNetPay[0] = "NET PAY";
                drNetPay[1] = (iSum - dDeduction).ToString("#,##,##,##,##0.00");
                dtable.Rows.Add(drNetPay);

                DataGridTableStyle dstyle = new DataGridTableStyle();
                dstyle.MappingName = dtable.TableName;
                ThisDataGridTextBoxColumn dText = new ThisDataGridTextBoxColumn();
                for (int i = 0; i < dtable.Columns.Count; i++)
                {
                    dText = new ThisDataGridTextBoxColumn();
                    dText.MappingName = dtable.Columns[i].ColumnName;
                    dText.HeaderText = dtable.Columns[i].ColumnName;
                    dText.SetDataGridCellFormat += new DataGridCustomEventHandler(column_SetDataGridCellFormat);
                    if (dtable.Columns[i].ColumnName == "Amount")
                    {
                        dText.Alignment = HorizontalAlignment.Right;
                        dText.Width = 300;
                    }
                    if (dtable.Columns[i].DataType.Name == "String")
                    {
                        dText.Alignment = HorizontalAlignment.Left;
                        dText.Width = 400;
                    }
                    dText.NullText = "";
                    dstyle.GridColumnStyles.Add(dText);
                }
                dstyle.AlternatingBackColor = System.Drawing.Color.Lavender;
                dstyle.BackColor = System.Drawing.Color.WhiteSmoke;
                dstyle.ForeColor = System.Drawing.Color.MidnightBlue;
                dstyle.GridLineColor = System.Drawing.Color.Silver;
                dstyle.HeaderBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(172)))), ((int)(((byte)(206)))), ((int)(((byte)(252)))));
                dstyle.HeaderForeColor = System.Drawing.Color.Black;
                dstyle.LinkColor = System.Drawing.Color.Teal;
                dstyle.SelectionBackColor = System.Drawing.Color.Teal;
                dstyle.SelectionForeColor = System.Drawing.Color.PaleGreen;
                //dstyle.RowHeadersVisible = false;
                //dstyle.ColumnHeadersVisible = false;

                dstyle.AllowSorting = false;
                dstyle.PreferredRowHeight = 20;
                dstyle.HeaderFont = new Font(System.Drawing.FontFamily.GenericSansSerif, (float)11, FontStyle.Bold);
                objGrid.TableStyles.Add(dstyle);
                objGrid.DataSource = dtable;
                dtData = dtable;
                objGrid.ReadOnly = true;
            }
            catch
            {

            }

        }//Purpose    : This method is used to set the Previous month payroll id
        public string GetPreviousValue()
        {

            string sDate = "";
            //sSql = "SELECT to_char(P.PRDATE, 'dd/MM/YYYY') as PRDATE,P.PAYROLLID FROM PRCREATE P WHERE P.PAYROLLID IN(SELECT MAX (P.PAYROLLID) FROM PRCREATE P) ORDER BY P.PRDATE DESC";
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.GetPreviousValue, "PRCreate"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
            }
            //createDataSet(sSql, "PRCreate");
            if (resultArgs.Success)
                dsetOther = resultArgs.DataSource.Table;
            if (dsetOther != null && dsetOther.Rows.Count > 0)
            {
                sDate = dsetOther.Rows[0]["PRDate"].ToString();
                PRPrevId = Int32.Parse(dsetOther.Rows[0]["PAYROLLID"].ToString());
            }
            else
            {
                XtraMessageBox.Show("No record available.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            return sDate;
        }
        //Purpose   :New Staff Group for each Payroll
        public void CreateNewStaffGroup(int nLastPayrollId, int nNewPRId)
        {
            //sSql = "INSERT INTO PRStaffGroup(StaffId,GroupId,StaffOrder,PayrollId) " +
            //    "SELECT StaffId,GroupId,StaffOrder," + nNewPRId + " FROM " +
            //    "PRStaffGroup WHERE Payrollid = " + nLastPayrollId;
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.AddPrStaff))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtPayroll.NEWPAYROLLIDColumn, nNewPRId);
                dataManager.Parameters.Add(dtPayroll.PAYROLLIDColumn, nLastPayrollId);
                resultArgs = dataManager.UpdateData();
            }
            //insertRecord(sSql);
        }

        //Purpose   :New Staff Group for each Payroll, if there is only one payrol, those payroll groups staff will not be deleted,
        //Compontent, Statutory compliance
        //when we create new staff again, it will be updated by new payroll (
        public void UpdtaeNewStaffGroup(int nNewPRId)
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.UpdatePrStaffForLastPayroll))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtPayroll.PAYROLLIDColumn.ColumnName, nNewPRId);
                resultArgs = dataManager.UpdateData();
            }
        }

        /// <summary>
        /// On 03/02/2023, to delete invlaid payroll details
        /// </summary>
        /// <returns></returns>
        public ResultArgs ClearInvalidPaydetails()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.ClearInvalidPaydetails))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs;
        }

        //Purpose    : This method is used to Lock or Unlock the Selected Payroll.
        public bool LockUnLockPayRoll()
        {
            //sSql = "UPDATE PRStatus SET Lockedstatus = '" + PRLocked + "' WHERE " +
            //    "PayRollId = " + PayRollId;
            //return insertRecord(sSql);
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.LockUnlockPayroll))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtPayroll.PAYROLLIDColumn, PayRollId);
                dataManager.Parameters.Add(dtPayroll.LOCKEDSTATUSColumn, PRLocked);
                resultArgs = dataManager.UpdateData();
            }
            return resultArgs.Success;
        }
        public bool DeletePayRoll(long nPRId)
        {

            object sSql = "";
            try
            {
                resultArgs = DeleteProcessPayVouher();
                sSql = SQLCommand.Payroll.DeletePRStaff;
                if (!InsertRecord(sSql, nPRId))
                    return false;
                sSql = SQLCommand.Payroll.DeletePRStaffTemo;
                if (!InsertRecord(sSql, nPRId))
                    return false;
                //sSql = SQLCommand.Payroll.DeletePRStaffGroup;
                //if (!InsertRecord(sSql, nPRId))
                //    return false;
                sSql = SQLCommand.Payroll.DeletePRCompMonth;
                if (!InsertRecord(sSql, nPRId))
                    return false;
                sSql = SQLCommand.Payroll.DeletePRStatus;
                if (!InsertRecord(sSql, nPRId))
                    return false;
                sSql = SQLCommand.Payroll.UpdateInstallment;
                if (!InsertRecord(sSql, nPRId))
                    return false;
                sSql = SQLCommand.Payroll.DeletePRLoanPaid;
                if (!InsertRecord(sSql, nPRId))
                    return false;
                sSql = SQLCommand.Payroll.DeletePRCreate;
                if (!InsertRecord(sSql, nPRId))
                    return false;
            }
            catch
            {
                return false;
            }
            return true;
        }
        private bool InsertRecord(object sql, long Id)
        {
            try
            {
                using (DataManager dataManager = new DataManager(sql))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtPayroll.PAYROLLIDColumn, Id);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.UpdateData();
                    if (resultArgs.Success)
                        return true;

                }
            }
            catch
            {
                return false;
            }
            return false;
        }
        private ResultArgs DeleteProcessPayVouher()
        {
            try
            {
                clsPayrollProcess mapledger = new clsPayrollProcess();
                int VoucherrId = FetchVouchermastersByPayrollId();
                resultArgs = DeleteVoucherTrans(VoucherrId);
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            return resultArgs;
        }
        public int FetchVouchermastersByPayrollId()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchVoucherMastersByPayrollId))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dataManager.Parameters.Add(dtPayroll.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dataManager.FetchData(DataSource.Scalar);
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        private ResultArgs DeleteVoucherTrans(int VoucherId)
        {
            try
            {
                using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                {
                    voucherTransaction.VoucherId = VoucherId;
                    voucherTransaction.tdsTransType = TDSTransType.TDSPartyPayment;
                    resultArgs = voucherTransaction.DeleteVoucherTrans();
                }
            }
            catch (Exception ex)
            {
            }
            finally { }
            return resultArgs;
        }
        public DataTable FetchRecord(object sql, string tableName)
        {
            try
            {
                using (DataManager dataManager = new DataManager(sql, tableName))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                    if (resultArgs.Success)
                        return resultArgs.DataSource.Table;
                }
            }
            catch
            {
                return null;
            }
            return null;
        }

        public ResultArgs FetchPayrollList()
        {
            using (DataManager dataManager = new DataManager(SQLCommand.Payroll.PayrollList, "PayrollList"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                    {
                        DateTime dtYearFrom = this.UtilityMember.DateSet.ToDate(dr["PAYROLL_FROM_DATE"].ToString(), false);

                        dr["PAYROLL_TO_DATE"] = dtYearFrom.AddMonths(1).AddDays(-1);
                    }
                }
            }
            return resultArgs;
        }

        public DataTable GetPayroll()
        {
            return FetchRecord(SQLCommand.Payroll.FetchPrGateWay, "PRCreate");
        }
        public ResultArgs FetchComponentsUnrelatedComponents(int GroupId)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPayrollComponentforpayment))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtPayroll.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                    dataManager.Parameters.Add(dtPayroll.SALARYGROUPIDColumn, GroupId);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            return resultArgs;

        }
        #region PayrollPostPayment
        public ResultArgs FetchExpenseLedgersByProjectId(int ProjectId)
        {
            try
            {
                using (DataManager dtManager = new DataManager(SQLCommand.Payroll.FetchLegderDetailsByPorjectId))
                {
                    dtManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dtManager.Parameters.Add(dtProjectStaff.PROJECT_IDColumn, ProjectId);
                    dtManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dtManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            return resultArgs;
        }

        //public ResultArgs GetPostVoucherBalanceAmount(int TYPE_ID, int PAYROLL_ID)
        //{
        //    try
        //    {
        //        using (DataManager dtManager = new DataManager(SQLCommand.Payroll.FetchPostPaymentVoucherDetails))
        //        {
        //            //dtManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
        //            //dtManager.Parameters.Add(dtpayrollFinance.PAYROLL_IDColumn, PAYROLL_ID);
        //            //dtManager.Parameters.Add(dtpayrollFinance.TYPE_IDColumn, TYPE_ID);
        //            //dtManager.DataCommandArgs.IsDirectReplaceParameter = true;
        //            //resultArgs = dtManager.FetchData(DataSource.DataTable);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageRender.ShowMessage(ex.ToString(), true);
        //    }
        //    return resultArgs;
        //}

        public ResultArgs FetchLiabilityLedgersByProjectId(int ProjectId)
        {
            try
            {
                using (DataManager dtManager = new DataManager(SQLCommand.Payroll.FetchLiabilityLedgersByProjectId))
                {
                    dtManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dtManager.Parameters.Add(dtProjectStaff.PROJECT_IDColumn, ProjectId);
                    dtManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dtManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            return resultArgs;
        }
        public decimal FetchProcessValuesByComponentId(int GroupId)
        {
            try
            {
                using (DataManager dtManager = new DataManager(SQLCommand.Payroll.FetchProcessedValuesBySelectedComponentID))
                {
                    dtManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dtManager.Parameters.Add(dtPayroll.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                    dtManager.Parameters.Add(dtprcomponent.SALARYGROUPIDColumn, GroupId);
                    dtManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dtManager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            decimal Processamount = this.UtilityMember.NumberSet.ToDecimal(resultArgs.DataSource.Sclar.ToString);
            return Processamount;
        }


        /// <summary>
        /// On 12/06/2019, post payroll voucher to Finnace
        /// </summary>
        /// <returns></returns>
        public ResultArgs PostPaymentVoucher()
        {
            try
            {
                //1. Post Payroll Payment Voucher to Finance
                using (DataManager dmPostPayroll = new DataManager(SQLCommand.Payroll.InsertPostPaymentDetails))
                {
                    dmPostPayroll.BeginTransaction();
                    resultArgs = SaveVoucherDetails(dmPostPayroll, PostedPayrollId);
                    if (resultArgs.Success)
                    {
                        if (VoucherId == 0)
                        {
                            VoucherId = UtilityMember.NumberSet.ToInteger(resultArgs.ReturnValue.ToString());
                        }

                        //2. Delete and Insert/Update details in Payroll
                        if (VoucherId > 0)
                        {
                            //# delete payroll post from prroll_voucher table
                            resultArgs = DeletePayrollPost(VoucherId, dmPostPayroll);
                            if (resultArgs.Success)
                            {
                                foreach (DataRow drPayrollVoucher in dtPayrollComponentDetails.Rows)
                                {
                                    using (DataManager dtManager = new DataManager(SQLCommand.Payroll.InsertPostPaymentDetails))
                                    {
                                        dtManager.Database = dmPostPayroll.Database;
                                        dtManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                                        dtManager.Parameters.Add(dtpayrollFinance.PAYROLL_IDColumn.ColumnName, PostedPayrollId);
                                        dtManager.Parameters.Add(dtpayrollFinance.SALARY_GROUP_IDColumn.ColumnName, UtilityMember.NumberSet.ToInteger(drPayrollVoucher[dtpayrollFinance.GROUP_IDColumn.ColumnName].ToString()));
                                        dtManager.Parameters.Add(dtpayrollFinance.COMPONENT_IDColumn.ColumnName, UtilityMember.NumberSet.ToInteger(drPayrollVoucher[dtpayrollFinance.COMPONENT_IDColumn.ColumnName].ToString()));
                                        dtManager.Parameters.Add(dtpayrollFinance.LEDGER_IDColumn.ColumnName, UtilityMember.NumberSet.ToInteger(drPayrollVoucher[dtpayrollFinance.LEDGER_IDColumn.ColumnName].ToString()));
                                        dtManager.Parameters.Add(dtpayrollFinance.VOUCHER_IDColumn.ColumnName, VoucherId);
                                        dtManager.Parameters.Add(dtpayrollFinance.AMOUNTColumn.ColumnName, UtilityMember.NumberSet.ToDecimal(drPayrollVoucher[dtpayrollFinance.AMOUNTColumn.ColumnName].ToString()));
                                        dtManager.DataCommandArgs.IsDirectReplaceParameter = true;
                                        resultArgs = dtManager.UpdateData();
                                    }

                                    if (!resultArgs.Success)
                                    {
                                        break;
                                    }
                                }
                            }
                        }
                        else
                        {
                            resultArgs.Message = "Payroll Payment Voucher is not Posted";
                        }
                    }

                    if (resultArgs.Success)
                    {
                        dmPostPayroll.EndTransaction();
                    }
                    else
                    {
                        dmPostPayroll.RollBackTransaction();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            return resultArgs;
        }

        public ResultArgs SaveVoucherDetails(DataManager dmPostPayroll, long PostedPayrollId)
        {
            try
            {
                decimal exchangerate = 1;

                using (DataManager dm = new DataManager())
                {
                    dm.Database = dmPostPayroll.Database;
                    using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem(VoucherId))
                    {
                        voucherTransaction.VoucherId = VoucherId;
                        voucherTransaction.ProjectId = ProjectId;
                        voucherTransaction.VoucherNo = VoucherNo;
                        voucherTransaction.TransVoucherMethod = (int)TransactionVoucherMethod.Automatic;
                        voucherTransaction.VoucherType = DefaultVoucherTypes.Payment.ToString();
                        voucherTransaction.VoucherDefinitionId = (int)DefaultVoucherTypes.Payment;
                        voucherTransaction.VoucherDate = postDate;
                        voucherTransaction.VoucherSubType = VoucherSubTypes.PAY.ToString();
                        voucherTransaction.ClientReferenceId = UtilityMember.NumberSet.ToInteger(PostedPayrollId.ToString()).ToString();
                        voucherTransaction.Narration = Narration;
                        voucherTransaction.Status = 1;

                        //On 21/10/2024, If multi currency enabled, set local exchange rate and amount alone -----------------------------------------
                        voucherTransaction.CurrencyCountryId = voucherTransaction.ExchageCountryId = 0;
                        voucherTransaction.ExchangeRate = exchangerate;
                        voucherTransaction.CalculatedAmount = 0;
                        voucherTransaction.ActualAmount = 0;
                        if (this.AllowMultiCurrency == 1)
                        {
                            using (CountrySystem country = new CountrySystem())
                            {
                                exchangerate = country.FetchSettingCountryCurrencyExchangeRate(postDate); //DateSet.ToDate(this.YearFrom, false)
                            }
                            
                        }
                        //----------------------------------------------------------------------------------------------------------------------------

                        string[] definedColumns = new string[] { dtpayrollFinance.SOURCEColumn.ColumnName, dtpayrollFinance.LEDGER_IDColumn.ColumnName, 
                                                                dtpayrollFinance.AMOUNTColumn.ColumnName };

                        //# Transaction General ledgers (Dr first and along wtih Cr)
                        string transmode = String.Empty;
                        DataTable dtTransLedgers = dtPayrollVoucherDetails.DefaultView.ToTable(false, definedColumns);
                                                
                        dtTransLedgers.Columns.Add(dtpayrollFinance.EXCHANGE_RATEColumn.ColumnName, typeof(decimal));
                        dtTransLedgers.Columns.Add(dtpayrollFinance.LIVE_EXCHANGE_RATEColumn.ColumnName, typeof(decimal));

                        //# Combin Ledgers
                        DataTable dtTransLedgers1 = dtTransLedgers.AsEnumerable()
                            .GroupBy(r1 => new { source = r1.Field<Int32>("SOURCE"), ledgerid = r1.Field<decimal>("LEDGER_ID") })
                            .Select(g =>
                         {
                             var row = dtTransLedgers.NewRow();
                             row["SOURCE"] = g.Key.source;
                             row["LEDGER_ID"] = g.Key.ledgerid;
                             row["AMOUNT"] = g.Select(r => r.Field<decimal>("AMOUNT")).Sum();
                             row[dtpayrollFinance.EXCHANGE_RATEColumn.ColumnName] = exchangerate;
                             row[dtpayrollFinance.LIVE_EXCHANGE_RATEColumn.ColumnName] = exchangerate;
                             return row;
                         }).CopyToDataTable();
                        dtTransLedgers1.Columns.Add(dtpayrollFinance.NARRATIONColumn.ColumnName, typeof(string));
                        dtTransLedgers1.Columns.Add(dtpayrollFinance.CHEQUE_NOColumn.ColumnName, typeof(string));
                        dtTransLedgers1.Columns.Add(dtpayrollFinance.MATERIALIZED_ONColumn.ColumnName, typeof(DateTime));
                        
                        //# Transaction Cash/Bank ledgers Cash/Bank group
                        this.TransInfo = dtTransLedgers1.DefaultView;
                        resultArgs = voucherTransaction.ConstructVoucherData(CashBankLedgerId, PostAmount, RefChequeDDNumber, postDate);

                        if (resultArgs.Success)
                        {
                            DataTable dtCashBankLedgers = resultArgs.DataSource.Table;
                            //On 03/02/2002, assign source value for cash & bank ledgers
                            //dtCashBankLedgers.Columns.Add(dtpayrollFinance.SOURCEColumn.ColumnName, typeof(Int32));
                            DataColumn dcCashBankSource = new DataColumn(dtpayrollFinance.SOURCEColumn.ColumnName, typeof(Int32));
                            dcCashBankSource.DefaultValue = 1;
                            dtCashBankLedgers.Columns.Add(dcCashBankSource);

                            this.CashTransInfo = dtCashBankLedgers.DefaultView;
                            decimal dTransDRAmount = UtilityMember.NumberSet.ToDecimal(this.TransInfo.Table.Compute("SUM(" + dtpayrollFinance.AMOUNTColumn.ColumnName + ")", dtpayrollFinance.SOURCEColumn.ColumnName + "=" + (int)TransSource.Dr).ToString());
                            decimal dTransCRAmount = UtilityMember.NumberSet.ToDecimal(this.TransInfo.Table.Compute("SUM(" + dtpayrollFinance.AMOUNTColumn.ColumnName + ")", dtpayrollFinance.SOURCEColumn.ColumnName + "=" + (int)TransSource.Cr).ToString());
                            decimal dCashBankAMount = UtilityMember.NumberSet.ToDecimal(this.CashTransInfo.Table.Compute("SUM(" + dtpayrollFinance.AMOUNTColumn.ColumnName + ")", string.Empty).ToString());
                            if ((dTransDRAmount - dTransCRAmount) == dCashBankAMount)
                            {
                                if (this.AllowMultiCurrency == 1)
                                {
                                    voucherTransaction.CurrencyCountryId = UtilityMember.NumberSet.ToInteger(string.IsNullOrEmpty(this.Country) ? "0" : this.Country);
                                    voucherTransaction.ExchageCountryId = voucherTransaction.CurrencyCountryId;
                                    voucherTransaction.ExchangeRate = 1;
                                    voucherTransaction.CalculatedAmount = (dTransDRAmount * 1);
                                    voucherTransaction.ContributionAmount = dTransDRAmount;
                                    voucherTransaction.ActualAmount = (dTransDRAmount * 1);
                                }

                                resultArgs = voucherTransaction.SaveVoucherDetails(dm);
                                if (resultArgs.Success)
                                {
                                    VoucherId = voucherTransaction.VoucherId;
                                    resultArgs.ReturnValue = VoucherId;
                                }
                            }
                            else
                            {
                                resultArgs.Message = ", Crs and Drs Amount is mismatching";
                            }
                        }
                        else
                        {
                            resultArgs.Message = "Cash/Bank Ledger is not found";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }

            return resultArgs;
        }

        
        public bool ExistsPayrollPostPaymentsByPayrollIdCompId(long Payrollid, Int32 CompId)
        {
            bool Rtn = true;
            try
            {
                using (DataManager dtmanger = new DataManager(SQLCommand.Payroll.FetchPayrollPostPaymentsByPayrollIdCompId))
                {
                    dtmanger.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dtmanger.Parameters.Add(this.AppSchema.PayrollFinance.PAYROLL_IDColumn.ColumnName, Payrollid);
                    dtmanger.Parameters.Add(this.AppSchema.PayrollFinance.COMPONENT_IDColumn.ColumnName, CompId);
                    dtmanger.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dtmanger.FetchData(DataSource.DataTable);
                }

                if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected == 0)
                {
                    Rtn = false;
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return Rtn;
        }

        public bool ExistsPayrollPostPaymentsByCompId(Int32 CompId)
        {
            bool Rtn = true;
            try
            {
                using (DataManager dtmanger = new DataManager(SQLCommand.Payroll.FetchPayrollPostPaymentsByPayrollIdCompId))
                {
                    dtmanger.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dtmanger.Parameters.Add(this.AppSchema.PayrollFinance.COMPONENT_IDColumn.ColumnName, CompId);
                    dtmanger.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dtmanger.FetchData(DataSource.DataTable);
                }

                if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected == 0)
                {
                    Rtn = false;
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return Rtn;
        }

        public ResultArgs FetchPayrollPostPaymentsByPayrollId(long Payrollid)
        {
            try
            {
                using (DataManager dtmanger = new DataManager(SQLCommand.Payroll.FetchPayrollPostPaymentsByPayrollId))
                {
                    dtmanger.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dtmanger.Parameters.Add(this.AppSchema.PayrollFinance.PAYROLL_IDColumn.ColumnName, Payrollid);
                    dtmanger.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dtmanger.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        public ResultArgs FetchPayrollPostPaymentVouhcerMaster(long Payrollid)
        {
            try
            {
                DateTime datefrom = DateSet.ToDate(clsGeneral.PAYROLLDATE, false);
                DateTime dateto = datefrom.AddMonths(1).AddDays(-1);
                using (DataManager dtmanger = new DataManager(SQLCommand.Payroll.FetchPayrollPostPaymentVouhcerMaster))
                {
                    dtmanger.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dtmanger.Parameters.Add(this.AppSchema.PayrollFinance.PAYROLL_IDColumn.ColumnName, Payrollid);
                    //dtmanger.Parameters.Add(this.AppSchema.PayrollFinance.DATE_FROMColumn.ColumnName, datefrom, DataType.Date);
                    //dtmanger.Parameters.Add(this.AppSchema.PayrollFinance.DATE_TOColumn.ColumnName, dateto, DataType.Date);
                    dtmanger.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dtmanger.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        public ResultArgs FetchPayrollPostPaymentVouhcerDetails()
        {
            try
            {
                DateTime datefrom = DateSet.ToDate(clsGeneral.PAYROLLDATE, false);
                DateTime dateto = datefrom.AddMonths(1).AddDays(-1);
                using (DataManager dtmanger = new DataManager(SQLCommand.Payroll.FetchPayrollPostPaymentVouhcerDetails))
                {
                    dtmanger.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dtmanger.Parameters.Add(this.AppSchema.PayrollFinance.PAYROLL_IDColumn.ColumnName, clsGeneral.PAYROLL_ID);
                    //dtmanger.Parameters.Add(this.AppSchema.PayrollFinance.DATE_FROMColumn.ColumnName, datefrom, DataType.Date);
                    //dtmanger.Parameters.Add(this.AppSchema.PayrollFinance.DATE_TOColumn.ColumnName, dateto, DataType.Date);

                    dtmanger.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dtmanger.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        public ResultArgs FillPostPaymentsetails(int PostPayid)
        {
            try
            {
                resultArgs = FetchPostPayment(PostPayid);
                if (resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    //postDate = this.UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0]["DATE"].ToString(), false);
                    //postLedgerId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0]["LEDGER_ID"].ToString());
                    //postLedgerName = resultArgs.DataSource.Table.Rows[0]["LEDGER"].ToString();
                    //postAmount = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0]["AMOUNT"].ToString());
                    //ProjectId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0]["PROJECT_ID"].ToString());
                    //CashBankLedgerId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0]["CASHBANK_LEDGER_ID"].ToString());
                    //CashBankLedgerName = resultArgs.DataSource.Table.Rows[0]["CASHBANK_LEDGER"].ToString();
                    //NetAmount = this.NumberSet.ToDecimal(resultArgs.DataSource.Table.Rows[0]["AMOUNT"].ToString());
                    //TypeId = this.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0]["TYPE_ID"].ToString());
                    //Narration = resultArgs.DataSource.Table.Rows[0]["NARRATION"].ToString();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }
        private ResultArgs FetchPostPayment(int id)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPayrollpostpayment))
                {
                    //long value = 4;
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtpayrollFinance.POST_IDColumn, id);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }


        /// <summary>
        /// On 11/06/2019, to get pending posting amout
        /// </summary>
        /// <param name="PayrollGroupdIds"></param>
        /// <returns></returns>
        public ResultArgs FetchPayrollPostPending(long PayrollId, string PayrollGroupdIds, Int32 ProjectId)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchPayrollPostPending))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtPayroll.PAYROLLIDColumn, PayrollId);
                    dataManager.Parameters.Add(dtPayroll.SALARYGROUPIDColumn.ColumnName, PayrollGroupdIds);
                    dataManager.Parameters.Add(dtpayrollFinance.PROJECT_IDColumn, ProjectId);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        public int FetchVoucherIdByPostId()
        {
            try
            {
                using (DataManager dtmanager = new DataManager(SQLCommand.Payroll.FetchVoucherIdByPostId))
                {
                    //dtmanager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    //dtmanager.Parameters.Add(dtpayrollFinance.POST_IDColumn, postid);
                    //dtmanager.DataCommandArgs.IsDirectReplaceParameter = true;
                    //resultArgs = dtmanager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs.DataSource.Sclar.ToInteger;
        }
        public ResultArgs DeleteVoucherDetails(int VoucherId)
        {
            try
            {
                using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                {
                    voucherTransaction.VoucherId = VoucherId;
                    resultArgs = voucherTransaction.DeleteVoucherTrans();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        /// <summary>
        /// This method is used to delete all the payroll records in all affected payroll tables
        /// </summary>
        /// <param name="nPRId"></param>
        /// <returns></returns>
        public bool DeletePayrollByPayrollId(long nPRId)
        {
            bool Rtn = false;
            try
            {
                using (DataManager dtmanager = new DataManager(SQLCommand.Payroll.DeletePayrollByPayrollId))
                {
                    dtmanager.BeginTransaction();

                    //# Remove all posted payroll payment vouchers and delete affected vouchers
                    resultArgs = FetchPayrollPostPaymentsByPayrollId(nPRId);
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtPostedPayrollVouchers = resultArgs.DataSource.Table;
                        foreach (DataRow dr in dtPostedPayrollVouchers.Rows)
                        {
                            Int32 voucherid = UtilityMember.NumberSet.ToInteger(dr[dtpayrollFinance.VOUCHER_IDColumn.ColumnName].ToString());
                            resultArgs = DeletePayrollPostPaymentVouchers(voucherid, dtmanager);
                            if (!resultArgs.Success)
                            {
                                break;
                            }
                        }
                    }

                    if (resultArgs.Success)
                    {
                        //# Remove all payroll realtaed tables
                        dtmanager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                        dtmanager.Parameters.Add("PAYROLLID", nPRId);
                        dtmanager.DataCommandArgs.IsDirectReplaceParameter = true;
                        resultArgs = dtmanager.UpdateData();
                    }

                    if (!resultArgs.Success)
                    {
                        dtmanager.TransExecutionMode = ExecutionMode.Fail;
                    }
                    dtmanager.EndTransaction();
                    Rtn = resultArgs.Success;
                }
            }
            catch (Exception err)
            {
                AcMELog.WriteLog("Unable to Delete Current Payroll, " + err.Message);
                Rtn = false;
            }
            return Rtn;
        }

        /// <summary>
        /// This method is used to delete Payroll posted from prroll_voucher table
        /// </summary>
        /// <param name="voucherid"></param>
        /// <returns></returns>
        private ResultArgs DeletePayrollPost(Int32 voucherid, DataManager dm)
        {
            try
            {
                using (DataManager dtmanager = new DataManager(SQLCommand.Payroll.DeletePayrollPostByVoucherId))
                {
                    dtmanager.Database = dm.Database;
                    dtmanager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dtmanager.Parameters.Add(dtpayrollFinance.VOUCHER_IDColumn, voucherid);
                    dtmanager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dtmanager.UpdateData();
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        public ResultArgs DeletePayrollPostPaymentVouchers(Int32 voucherid)
        {
            int projectid = 0;
            DateTime voucherdate;

            using (DataManager dtmanager = new DataManager(SQLCommand.Payroll.DeletePayrollPostPaymentVouchersByVoucherId))
            {
                using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem(voucherid))
                {
                    projectid = vouchersystem.ProjectId;
                    voucherdate = vouchersystem.VoucherDate;
                }

                dtmanager.BeginTransaction();
                //# Delete Voucher and Payroll Voucher Details
                resultArgs = DeletePayrollPostPaymentVouchers(voucherid, dtmanager);

                if (resultArgs.Success)
                {
                    //# Regernate Voucher No
                    if (this.NumberSet.ToInteger(this.EnableVoucherRegenerationDeletion) == (int)YesNo.Yes)
                    {
                        using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem(voucherid))
                        {
                            vouchersystem.VoucherType = VoucherSubTypes.PY.ToString();
                            vouchersystem.VoucherDefinitionId = (Int32)DefaultVoucherTypes.Payment;
                            vouchersystem.ProjectId = projectid;
                            resultArgs = vouchersystem.RegenerateVoucherNumbers(dtmanager, voucherdate, voucherdate);
                        }
                    }
                }

                if (!resultArgs.Success)
                {
                    dtmanager.TransExecutionMode = ExecutionMode.Fail;
                }
                dtmanager.EndTransaction();
            }

            return resultArgs;
        }

        /// <summary>
        /// This method is used to Posed Payroll payment voucher, deleted pysically from Voucher and payroll tables (from prroll_voucher table)
        /// </summary>
        /// <param name="voucherid"></param>
        /// <returns></returns>
        public ResultArgs DeletePayrollPostPaymentVouchers(Int32 voucherid, DataManager baseDM)
        {
            using (DataManager dtmanager = new DataManager(SQLCommand.Payroll.DeletePayrollPostPaymentVouchersByVoucherId))
            {
                dtmanager.Database = baseDM.Database;

                using (BalanceSystem balanceSystem = new BalanceSystem())
                {
                    resultArgs = balanceSystem.UpdateTransBalance(voucherid, TransactionAction.Cancel);
                    if (resultArgs.Success)
                    {
                        dtmanager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                        dtmanager.Parameters.Add(dtpayrollFinance.VOUCHER_IDColumn, voucherid);
                        dtmanager.DataCommandArgs.IsDirectReplaceParameter = true;
                        resultArgs = dtmanager.UpdateData();
                    }
                }
            }
            return resultArgs;
        }

        /// <summary>
        /// Check Payroll Payment posted or not
        /// </summary>
        /// <param name="VoucherId"></param>
        /// <returns></returns>
        public bool IsPayrollPostPaymentPosted(long PayrollId, Int32 PayrollGroupId)
        {
            bool RtnPayrollPaymentPosted = false;

            using (DataManager dtmanager = new DataManager(SQLCommand.Payroll.IsPayrollPostPaymentPosted))
            {
                dtmanager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                dtmanager.Parameters.Add(dtpayrollFinance.PAYROLL_IDColumn.ColumnName, PayrollId);
                dtmanager.Parameters.Add(dtpayrollFinance.SALARY_GROUP_IDColumn.ColumnName, PayrollGroupId);
                dtmanager.DataCommandArgs.IsDirectReplaceParameter = true;
                resultArgs = dtmanager.FetchData(DataSource.DataTable);
            }

            if (resultArgs.Success)
            {
                RtnPayrollPaymentPosted = (resultArgs.RowsAffected > 0);
            }

            return RtnPayrollPaymentPosted;
        }


        /// <summary>
        /// Get Post Payment details for given voucher id
        /// </summary>
        /// <param name="VoucherId"></param>
        /// <returns></returns>
        public ResultArgs FetchPayrollPostPaymentByVoucherId(Int32 VoucherId)
        {
            try
            {
                using (DataManager dtmanager = new DataManager(SQLCommand.Payroll.FetchPayrollPostPaymentByVoucherId))
                {
                    dtmanager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dtmanager.Parameters.Add(dtpayrollFinance.VOUCHER_IDColumn.ColumnName, VoucherId);
                    dtmanager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dtmanager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                resultArgs.Message = ex.ToString();
            }
            return resultArgs;
        }

        public ResultArgs FetchDeductionComponents(int GroupId)
        {
            try
            {
                using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchDecutionComponents))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtPayroll.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                    dataManager.Parameters.Add(dtPayroll.SALARYGROUPIDColumn, GroupId);
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            return resultArgs;

        }
        public decimal FetchProcessValuesofDeductionComponents(int GroupId)
        {
            try
            {
                using (DataManager dtManager = new DataManager(SQLCommand.Payroll.FetchProcessedValuesofDeductionComponents))
                {
                    dtManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dtManager.Parameters.Add(dtPayroll.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                    dtManager.Parameters.Add(dtprcomponent.COMPONENTIDColumn, GroupId);
                    dtManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dtManager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            decimal Processamount = this.UtilityMember.NumberSet.ToDecimal(resultArgs.DataSource.Sclar.ToString);
            return Processamount;
        }
        public double FetchSumofPostvoucheramountBypayrollid()
        {
            try
            {
                using (DataManager dtManager = new DataManager(SQLCommand.Payroll.FetchsumofPostPaymentAmountByPayrollId))
                {
                    dtManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dtManager.Parameters.Add(dtPayroll.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                    dtManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dtManager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            double SumofPostamount = this.UtilityMember.NumberSet.ToDouble(resultArgs.DataSource.Sclar.ToString);
            return SumofPostamount;
        }

        public double ValidateFetchSumofPostvoucheramountBypayrollid()
        {
            try
            {
                using (DataManager dtManager = new DataManager(SQLCommand.Payroll.ValidateSumofPostvoucheramountBypayrollid))
                {
                    //dtManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    //dtManager.Parameters.Add(dtPayroll.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
                    //dtManager.Parameters.Add(dtprcomponent.TYPEColumn, TypeId);
                    //dtManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    //resultArgs = dtManager.FetchData(DataSource.Scalar);
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            double SumofPostamount = this.UtilityMember.NumberSet.ToDouble(resultArgs.DataSource.Sclar.ToString);
            return SumofPostamount;
        }


        /// <summary>
        /// On 30/07/2019, for pay extra info (earning1, earning2, earning3, deduction1, deduction2
        /// 
        /// To Clear and Reset payextra info in prstafftemp and reset from staff profile 
        /// this method will clear values for above values in prstafftemp and import for satff profile
        /// 
        /// </summary>
        /// <returns></returns>
        public ResultArgs RestPayExtraInfoFromStaffProfile(long PayrollId, Int32 SalagryGrpId, double TotalDaysInPayMonth)
        {
            ResultArgs resultArgs = new ResultArgs();
            try
            {
                using (DataManager dtManager = new DataManager(SQLCommand.Payroll.ClearPayExtraInfo))
                {
                    //#. Clear Payextra information in prstafftemp
                    dtManager.BeginTransaction();
                    dtManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    dtManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dtManager.Parameters.Add(dtPayroll.SALARYGROUPIDColumn, SalagryGrpId);
                    dtManager.Parameters.Add(dtPayroll.PAYROLLIDColumn, PayrollId);
                    resultArgs = dtManager.UpdateData();

                    if (resultArgs.Success)
                    {
                        if (SalagryGrpId > 0)
                        {
                            using (DataManager dtManager1 = new DataManager(SQLCommand.Payroll.ResetPayExtraInfo))
                            {
                                //#. Reset values from staff profile
                                dtManager1.Database = dtManager.Database;
                                dtManager1.DataCommandArgs.IsDirectReplaceParameter = true;
                                dtManager1.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                                dtManager1.Parameters.Add(dtPayroll.PAYROLLIDColumn, PayrollId);
                                dtManager1.Parameters.Add(dtPayroll.SALARYGROUPIDColumn, SalagryGrpId);
                                dtManager1.Parameters.Add(dtPayroll.TOTALDAYSINPAYMONTHColumn.ColumnName, TotalDaysInPayMonth);
                                resultArgs = dtManager1.UpdateData();
                            }
                        }
                    }

                    if (resultArgs.Success)
                    {
                        dtManager.EndTransaction();
                    }
                    else
                    {
                        dtManager.RollBackTransaction();
                    }
                }
            }
            catch (Exception err)
            {
                resultArgs.Message = "Unable to reset Pay Extra details" + err.Message;
            }
            return resultArgs;
        }

        #endregion


    }
}
