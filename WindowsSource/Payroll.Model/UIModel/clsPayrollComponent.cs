using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using Bosco.Utility.Common;
using System.Data.SqlClient;
using Payroll.DAO.Schema;
using Bosco.Utility;
using Bosco.DAO.Data;
using System.Windows.Forms;

namespace Payroll.Model.UIModel
{
    public class clsPayrollComponent : SystemBase
    {
        ApplicationSchema.PRCOMPONENTDataTable dtComponent = null;
        clsprCompBuild comBuild = new clsprCompBuild();
        public clsPayrollComponent()
        {
            dtComponent = this.AppSchema.PRCOMPONENT;
        }
        //  private DataHandling objDBHand = new DataHandling();
        private object strQuery = "";
        public int ComponentId;
        public string Component = "";
        public string Description = "";
        public string Type = "";
        public string DefValue = "";
        public string LinkValue = "";
        public int LinkValueID=0;
        public string Equation = string.Empty;
        public string EquationId = string.Empty;
        public double MaxSlab = 0;
        public int CompRound = 0;
        private int Accessflag = 0;
        public string IFCondition = "";
        public ResultArgs resultArgs = null;
        public DataTable dtTable = null;
        private int iLedgerId = 0;

        private string sLedgerName = "";

        public string LedgerName
        {
            set { this.sLedgerName = value; }
            get { return this.sLedgerName; }
        }

        public int LedgerId
        {
            set { this.iLedgerId = value; }
            get { return this.iLedgerId; }
        }
        int PTypeId = 0;
        public int ProcessTypeId
        {
            set { this.PTypeId = value; }
            get { return this.PTypeId; }
        }
        private string PLedgerName = "";
        public string ProcessLedgername
        {
            set { this.PLedgerName = value; }
            get { return this.PLedgerName; }
        }
        
        public Int32 ComId
        {
            set { this.ComponentId = value; }
            get { return this.ComponentId; }
        }
        public string Com
        {
            set { this.Component = value; }
            get { return this.Component; }
        }
        public string Des
        {
            set { this.Description = value; }
            get { return this.Description; }
        }
        public string ComType
        {
            set { this.Type = value; }
            get { return this.Type; }
        }
        public string DefaultValue
        {
            set { this.DefValue = value; }
            get { return this.DefValue; }
        }
        public string LValue
        {
            set { this.LinkValue = value; }
            get { return this.LinkValue; }
        }
        public string ComEquation
        {
            set { this.Equation = value; }
            get { return this.Equation; }
        }
        public string ComEquationId
        {
            set { this.EquationId = value; }
            get { return this.EquationId; }
        }
        public double ComMaxSlab
        {
            set { this.MaxSlab = value; }
            get { return this.MaxSlab; }
        }
        public Int32 ComRound
        {
            set { this.CompRound = value; }
            get { return this.CompRound; }
        }
        public Int32 CompAccessFlag
        {
            set { this.Accessflag = value; }
            get { return this.Accessflag; }
        }
        public string ComCondition
        {
            set { this.IFCondition = value; }
            get { return this.IFCondition; }
        }
        public int Payable{get;set;}
        public int IsEditable { get; set; }
        public int ProcessComponentType { get; set; }
        public int DontShowInBrowse { get; set; }
        public int DontImportValuePreviousPR { get; set; }

        public DataTable getComponentDetails(long iGetId)
        {
            strQuery = getPayrollComponentQuery(clsPayrollConstants.PAYROLL_COMP_SELECT);
            //strQuery = strQuery.Replace("<groupid>", iGetId.ToString());
            //strQuery = strQuery.Replace("<payrollid>", clsGeneral.PAYROLL_ID.ToString());
            try
            {
                using (DataManager dataManager = new DataManager(strQuery, "Component"))
                {
                    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                    dataManager.Parameters.Add(dtComponent.PAYROLLIDColumn, clsGeneral.PAYROLL_ID.ToString());
                    if (iGetId != 0)
                    {
                        dataManager.Parameters.Add(dtComponent.SALARYGROUPIDColumn, iGetId.ToString());
                    }
                    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                    resultArgs = dataManager.FetchData(DataSource.DataTable);

                    if (resultArgs.Success)
                    {
                        resultArgs.DataSource.Table.TableName = "Component";
                        dtTable = resultArgs.DataSource.Table;
                    }
                    return dtTable;
                }
            }
            catch { return null; }
        }
        public object getPayrollComponentQuery(int iConstId)
        {
            object strQuery = "";
            switch (iConstId)
            {
                case clsPayrollConstants.PAYROLL_COMPONENT_SELECT:
                    strQuery = SQLCommand.Payroll.PayrollComponenetSelect;
                    break;
                case clsPayrollConstants.PAYROLL_COMPONENT:
                    strQuery = SQLCommand.Payroll.PayrollComponent;
                    break;
                case clsPayrollConstants.PAYROLL_COMPONENT_WITH_TYPE:
                    strQuery = SQLCommand.Payroll.PayrollComponentWithType;
                    break;
                case clsPayrollConstants.PAYROLL_COMPONENT_ADD:
                    strQuery = SQLCommand.Payroll.PayrollComponentAdd;
                    break;
                case clsPayrollConstants.PAYROLL_COMPONENT_EDIT:
                    strQuery = SQLCommand.Payroll.PayrollComponentEdit;
                    break;
                case clsPayrollConstants.PAYROLL_COMPONENT_DELETE:
                    strQuery = SQLCommand.Payroll.PayrollComponentDelete;
                    break;
                case clsPayrollConstants.PAYROLL_COMP_SELECT:
                    strQuery = SQLCommand.Payroll.PayrollCompSelect;
                    break;
                case clsPayrollConstants.PAYROLL_EDIT_VERIFY_COMP_LINK:
                    strQuery = SQLCommand.Payroll.PayrollEditVerifyCompLink;
                    break;
                case clsPayrollConstants.PAYROLL_EDIT_COMP_UPDATE:
                    strQuery = SQLCommand.Payroll.PayrollEditCompUpdate;
                    break;

            }
            return strQuery;
        }
        public bool UpdateComponentChanges(string sCompStr, long componentId, long payrollId)
        {
            object strQuery = getPayrollComponentQuery(clsPayrollConstants.PAYROLL_EDIT_COMP_UPDATE);
            using (DataManager dataManager = new DataManager(strQuery))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                //object strQuery = getPayrollComponentQuery(clsPayrollConstants.PAYROLL_EDIT_COMP_UPDATE);
                string[] aFields = sCompStr.Split('@');
                string[] aFieldDetails = new string[2];

                foreach (string field in aFields)
                {
                    aFieldDetails = field.Split('|');

                    if (aFieldDetails[1] == "#")
                        aFieldDetails[1] = "";

                    switch (aFieldDetails[0])
                    {
                        case "TYPE":
                            //strQuery = strQuery.Replace("<TYPE>", aFieldDetails[1]);
                            dataManager.Parameters.Add(dtComponent.TYPEColumn, aFieldDetails[1]);
                            break;
                        case "DEFVALUE":
                            //strQuery = strQuery.Replace("<DEFVALUE>", aFieldDetails[1]);
                            dataManager.Parameters.Add(dtComponent.DEFVALUEColumn, aFieldDetails[1]);
                            break;
                        case "EQUATION":
                            //  strQuery = strQuery.Replace("<EQUATION>", aFieldDetails[1]);
                            dataManager.Parameters.Add(dtComponent.EQUATIONColumn, aFieldDetails[1]);
                            break;
                        case "EQUATIONID":
                            // strQuery = strQuery.Replace("<EQUATIONID>", aFieldDetails[1]);
                            dataManager.Parameters.Add(dtComponent.EQUATIONIDColumn, aFieldDetails[1]);
                            break;
                        case "MAXSLAP":
                            //  strQuery = strQuery.Replace("<MAXSLAP>", aFieldDetails[1]);
                            dataManager.Parameters.Add(dtComponent.MAXSLAPColumn, aFieldDetails[1]);
                            break;
                        case "LNKVALUE":
                            // strQuery = strQuery.Replace("<LNKVALUE>", aFieldDetails[1]);
                            dataManager.Parameters.Add(dtComponent.LINKVALUEColumn, aFieldDetails[1]);
                            break;
                        case "COMPROUND":
                            //strQuery = strQuery.Replace("<COMPROUND>", aFieldDetails[1]);
                            dataManager.Parameters.Add(dtComponent.COMPROUNDColumn, aFieldDetails[1]);
                            break;
                        case "IFCONDITION":
                            // strQuery = strQuery.Replace("<IFCONDITION>", aFieldDetails[1]);
                            dataManager.Parameters.Add(dtComponent.IFCONDITIONColumn, aFieldDetails[1]);
                            break;
                    }
                }

                //strQuery = strQuery.Replace("<PAYROLLID>", payrollId.ToString());
                //strQuery = strQuery.Replace("<COMPONENTID>", componentId.ToString());
                dataManager.Parameters.Add(dtComponent.PAYROLLIDColumn, payrollId);
                dataManager.Parameters.Add(dtComponent.COMPONENTIDColumn, componentId);
                resultArgs = dataManager.UpdateData();
                if (!resultArgs.Success)
                {
                    MessageBox.Show("Could not update the component changes in the current payroll", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
                return true;
            }
        }
        public DataTable getPayrollComponent()
        {
            strQuery = getPayrollComponentQuery(clsPayrollConstants.PAYROLL_COMPONENT);
            //createDataSet(strQuery, "COMPONENT");
            using (DataManager dataManager = new DataManager(strQuery, "COMPONENT"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);

                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    dtTable = resultArgs.DataSource.Table;
                }
            }
            return dtTable;
        }

        
        public DataTable getPayrollComponentWithType()
        {
            strQuery = getPayrollComponentQuery(clsPayrollConstants.PAYROLL_COMPONENT_WITH_TYPE);
            using (DataManager dataManager = new DataManager(strQuery, "COMPONENT"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);

            }
            if (resultArgs.Success)
                return resultArgs.DataSource.Table;
            else
                return null;
        }
        public DataTable getPayrollComponentList()
        {
            strQuery = getPayrollComponentQuery(clsPayrollConstants.PAYROLL_COMPONENT_SELECT);
            //createDataSet(strQuery, "PRCOMPONENT");
            //return getDataSet();
            using (DataManager dataManager = new DataManager(strQuery, "PRCOMPONENT"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                {
                    resultArgs.DataSource.Table.TableName = "PRCOMPONENT";
                    return resultArgs.DataSource.Table;
                }
            }
            return null;
        }
        public void GetPayrollComponentDetails(int componentid)
        {
            try
            {
                DataTable dtTable = null;
                dtTable = comBuild.FetchComponentDetailsById(componentid);
                if (dtTable.Rows.Count > 0 && dtTable != null)
                {
                    Component = dtTable.Rows[0]["COMPONENT"].ToString();
                    Description = dtTable.Rows[0]["DESCRIPTION"].ToString();
                    Type = dtTable.Rows[0]["TYPE"].ToString();
                    DefValue = dtTable.Rows[0]["DEFVALUE"].ToString();
                    LinkValue = dtTable.Rows[0]["LINKVALUE"].ToString();
                    LinkValueID =NumberSet.ToInteger(dtTable.Rows[0]["SELECTED_ID"].ToString());
                    Equation = dtTable.Rows[0]["EQUATION"].ToString();
                    if (string.IsNullOrEmpty(Equation) || Equation==" ")
                    {
                        EquationId = string.Empty;
                    }
                    else
                    {
                        EquationId = dtTable.Rows[0]["EQUATIONID"].ToString();
                    }
                    MaxSlab =this.NumberSet.ToDouble(dtTable.Rows[0]["MAXSLAP"].ToString());
                    double dcompRound = this.NumberSet.ToDouble(dtTable.Rows[0]["COMPROUND"].ToString());
                    CompRound = this.NumberSet.ToInteger(dcompRound.ToString());
                    CompAccessFlag = this.NumberSet.ToInteger(dtTable.Rows[0]["ACCESS_FLAG"].ToString());
                    LedgerId = this.NumberSet.ToInteger(dtTable.Rows[0]["LEDGER_ID"].ToString());
                    ProcessTypeId=this.NumberSet.ToInteger(dtTable.Rows[0]["PROCESS_TYPE_ID"].ToString());
                    Payable = this.NumberSet.ToInteger(dtTable.Rows[0]["PAYABLE"].ToString());
                    IsEditable = this.NumberSet.ToInteger(dtTable.Rows[0]["ISEDITABLE"].ToString());
                    ProcessComponentType = this.NumberSet.ToInteger(dtTable.Rows[0]["PROCESS_COMPONENT_TYPE"].ToString());
                    DontShowInBrowse = this.NumberSet.ToInteger(dtTable.Rows[0][dtComponent.DONT_SHOWINBROWSEColumn.ColumnName].ToString());
                    DontImportValuePreviousPR= this.NumberSet.ToInteger(dtTable.Rows[0][dtComponent.DONT_IMPORT_MODIFIED_VALUE_PREV_PRColumn.ColumnName].ToString());
                }
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message, "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
        }
        public DataTable FetchRangeValues(int Componentid)
        {
            using(DataManager datamanger=new DataManager(SQLCommand.Payroll.FetchRangeValuesByComponentId))
            {
                datamanger.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                datamanger.DataCommandArgs.IsDirectReplaceParameter = true;
                datamanger.Parameters.Add(dtComponent.COMPONENTIDColumn, Componentid);
                resultArgs = datamanger.FetchData(DataSource.DataTable);
            }
            return resultArgs.DataSource.Table;
        }
        public ResultArgs DeleteRangeValuesBycomponentId(int Componentid)
        {
            using (DataManager datamanger = new DataManager(SQLCommand.Payroll.DeleteRangeByComponentId))
            {
                datamanger.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                datamanger.DataCommandArgs.IsDirectReplaceParameter = true;
                datamanger.Parameters.Add(dtComponent.COMPONENTIDColumn, Componentid);
                resultArgs = datamanger.UpdateData();
            }
            return resultArgs;
        }
    }
}
