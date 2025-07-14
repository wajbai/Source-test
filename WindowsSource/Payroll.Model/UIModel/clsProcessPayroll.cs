using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Data;
using Payroll.DAO.Schema;
using Bosco.DAO.Data;
using Bosco.Utility;
using Bosco.Utility.ConfigSetting;
using Bosco.Utility.Common;
using System.Windows.Forms;
using Microsoft.VisualBasic;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace Payroll.Model.UIModel
{
    public class clsProcessPayroll : SystemBase
    {
       // ApplicationSchema.PRCOMPONENTDataTable dtComponent = new ApplicationSchema.PRCOMPONENTDataTable();

       // ResultArgs resultArgs = null;
       // private clsPayrollActivities objActivities = new clsPayrollActivities();
       // private clsprCompBuild objmodBuild = new clsprCompBuild();
       // private DataView dvStaff = new DataView();
       // private DataTable rsComponent = new DataTable();
       // private DataTable rsLoan = new DataTable();
       // private DataSet rsStaff = new DataSet();
       // private DataTable rsDefVal = new DataTable();    //DataSet for retriving default values
       // private DataTable rsCompF = new DataTable();    //DataSet for Component Filter
       // private DataTable rsPR = new DataTable();    //DataSet for Staff Payroll Creation

       // // private DataView dvComponent = new DataView();	//rsComponent.Tables[0].DefaultView;
       // private DataView dvDefVal = new DataView();	//rsDefVal.Tables[0].DefaultView;
       // private DataView dvCompM = new DataView();	//new DataView(rsCompM.Tables[0]);
       // private bool bNewPayRollProcess; //Process new payroll or Reprocess the Payroll
       // private long nPayRollId { get; set; }
       // private bool bEquation;
       // private CommonMember commem = new CommonMember();
       // private clsPrLoan objPrLoan = new clsPrLoan();
       // clsPrComponent clscomp;

       // private DataTable rsLoanG = new DataTable();    //Loan Received Information of Staff
       // private DataTable rsLoanP = new DataTable();    //Loan Paid Information of Staff
       // private DataTable rsLoanN = new DataTable();
       // private DataView dvLoanP = new DataView();

       // public void ProcessComponent(long nPRId, string sGroupId, string sCompId, string sStaffId, bool bNewPayRoll, ProgressBar objProgress, bool bModify, DataView dvStaffDetails)
       // {
       //     bool bProgress;
       //     int nMax;
       //     long nCompId;
       //     string sEquationId = "";
       //     int nCompOrder = 0;
       //     string sRetVal = "";
       //     string sDefValSQL = "";
       //     string sCompMSQL = "";
       //     string sCompFSQL = "";
       //     string sPRSQL = "";
       //     string sWhere = "";
       //     string sPRDelSQL = "";
       //     string nExistCompId = ""; //Deleting Component records from PRStaff which is removed from the PRCompMonth
       //     string sSql = "";
       //     string sgetVal = "";
       //     double setDVal = 0.0;

       //     if (!bModify)
       //         if (clsModPay.ProcessRunning(true, nPRId, true)) return;
       //     rsComponent = objmodBuild.PayrollComponent();
       //     //  this.dvComponent = rsComponent.DefaultView;
       //     rsLoan = objmodBuild.PayrollLoan();
       //     bNewPayRollProcess = bNewPayRoll;
       //     nPayRollId = nPRId;
       //     if (bNewPayRollProcess) objmodBuild.ImportGroupComponent();
       //     if (!bModify) //This RecordSet is Created in the Modification Function
       //     {
       //         CreateStaffRSet(sGroupId, sStaffId);
       //         CreateLoanRSet(sStaffId);
       //     }
       //     //------ Create Group Component Recordset (To Process payroll according to given order)
       //     sWhere = "PRCompMonth.PayRollId = " + nPayRollId + ((sGroupId != "") ? " AND " +
       //              "SalaryGroupId IN (" + sGroupId + ")" : "") +
       //              ((sCompId != "") ? " AND ComponentId IN (" + sCompId + ")" : "") +
       //              ((sStaffId != "") ? " AND PRStaffGroup.StaffId IN (" + sStaffId + ")" : "");
       //     // Details got from the thread source

       //     //using (DataManager dataManager = new DataManager(SQLCommand.Payroll.ProcessPayrollInOrder, "CompMonth"))
       //     //{
       //     //    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
       //     //    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
       //     //    dataManager.Parameters.Add(dtComponent.CONDITIONColumn, sWhere);
       //     //    dataManager.Parameters.Add(dtComponent.PAYROLLIDColumn, nPRId);
       //     //    dataManager.Parameters.Add(dtComponent.PAYROLLDATEColumn, commem.DateSet.ToDate(clsGeneral.PAYROLLDATE));
       //     //    resultArgs = dataManager.FetchData(DataSource.DataTable);

       //     //    if (resultArgs.Success)
       //     //    {
       //     //        dvCompM = resultArgs.DataSource.Table.DefaultView;
       //     //    }
       //     //}
       //     dvCompM = dvStaffDetails;
       //     dvCompM.Sort = "empno,comp_order";
       //     if (sCompId == "" && !bNewPayRoll)
       //     {
       //         sWhere = "PayRollId = " + nPayRollId + ((sGroupId != "") ? " AND " +
       //                  "SalaryGroupId IN (" + sGroupId + ")" : "");
       //         DataView dvCompF = null;
       //         using (DataManager dataManager = new DataManager(SQLCommand.Payroll.GetComponentIdByGroupId, "PrCompMonth"))
       //         {
       //             dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
       //             dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
       //             dataManager.Parameters.Add(dtComponent.CONDITIONColumn, sWhere);
       //             resultArgs = dataManager.FetchData(DataSource.DataTable);

       //             if (resultArgs.Success)
       //             {
       //                 dvCompF = resultArgs.DataSource.Table.DefaultView;
       //             }
       //         }
       //         dvCompF.Sort = "componentid";

       //         for (int i = 0; i < dvCompF.Table.Rows.Count; i++)
       //         {
       //             sCompId = sCompId + dvCompF.Table.Rows[i]["componentid"].ToString() + ",";
       //         }
       //         sCompId = objPrLoan.RemoveTrailingSpace(sCompId, 1);
       //     }
       //     //----------------------------------------------------------------------------
       //     //Deleting Component records from PRStaff which is removed from the PRCompMonth
       //     if (!bNewPayRoll)
       //     {
       //         DataView dvCompF = null;
       //         sWhere = "PayRollId = " + nPayRollId + ((sGroupId != "") ? " AND " +
       //                  "SalaryGroupId IN (" + sGroupId + ")" : "");
       //         using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchComponentIDbySalaryGroupId, "PrCompMonth"))
       //         {
       //             dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
       //             dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
       //             dataManager.Parameters.Add(dtComponent.CONDITIONColumn, sWhere);
       //             resultArgs = dataManager.FetchData(DataSource.DataTable);

       //             if (resultArgs.Success)
       //             {
       //                 dvCompF = resultArgs.DataSource.Table.DefaultView;
       //             }
       //         }
       //         dvCompF.Sort = "componentid";

       //         for (int i = 0; i < dvCompF.Table.Rows.Count; i++)
       //         {
       //             nExistCompId = nExistCompId + dvCompF.Table.Rows[i]["componentid"].ToString() + ",";
       //         }
       //         nExistCompId = objPrLoan.RemoveTrailingSpace(nExistCompId, 1);
       //     }
       //     sCompFSQL = ((sGroupId != "") ? " AND " +
       //           "prstaffgroup.GroupId IN (" + sGroupId + ")" : "");
       //     using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchComponentNotinPRMonth, "StaffGroup"))
       //     {
       //         dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
       //         dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
       //         dataManager.Parameters.Add(dtComponent.PAYROLLIDColumn, nPRId);
       //         dataManager.Parameters.Add(dtComponent.CONDITIONColumn, sCompFSQL);
       //         resultArgs = dataManager.FetchData(DataSource.DataTable);

       //         if (resultArgs.Success)
       //         {
       //             rsCompF = resultArgs.DataSource.Table;
       //         }
       //     }
       //     for (int i = 0; i < rsCompF.Rows.Count; i++)
       //     {
       //         sCompId = ((sCompId != "") ? sCompId + "," : sCompId + "") + rsCompF.Rows[i]["componentid"].ToString();
       //     }
       //     if (sCompId != "") sCompId = sCompId.TrimEnd(','); //Line does not exist in vb..(Included Extra)


       //     //------ SQL for Payroll --------------------------------------------
       //     //Before going to Process the Payroll, Delete the existing Payroll from 'PRStaff'
       //     if (bNewPayRoll)
       //     {
       //         sWhere = "PayRollId = " + nPayRollId;
       //     }
       //     else
       //     {
       //         sWhere = "(PayRollId = " + nPayRollId + ((sStaffId != "") ?
       //                 " AND StaffId IN (" + sStaffId + ")" : "") +
       //                 ((sCompId != "") ? " AND ComponentId IN (" + sCompId + ")" : "") +
       //                 ((sGroupId != "") ? " AND StaffId IN (SELECT StaffId FROM PRStaffGroup " +
       //                 "WHERE GroupId IN (" + sGroupId + ") and PRStaffGroup.payrollid = " + nPayRollId + "))" : ")");
       //     }
       //     using (DataManager dataManager = new DataManager(SQLCommand.Payroll.DeleteExistingPayroll))
       //     {
       //         dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
       //         dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
       //         dataManager.Parameters.Add(dtComponent.CONDITIONColumn, sWhere);
       //         resultArgs = dataManager.UpdateData();
       //     }
       //     //using (DataManager dataManager = new DataManager(SQLCommand.Payroll.DeletePRStaffTemo))
       //     //{
       //     //    dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
       //     //    dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
       //     //    dataManager.Parameters.Add(dtComponent.PAYROLLIDColumn, clsGeneral.PAYROLL_ID);
       //     //    resultArgs = dataManager.UpdateData();
       //     //}

       //     if (!resultArgs.Success)
       //     {
       //         MessageBox.Show("Processing Error:" + resultArgs.Message);
       //         return;
       //     }
       //     using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchStaffDetailsToProcessPayroll, "PrStaff"))
       //     {
       //         dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
       //         dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
       //         dataManager.Parameters.Add(dtComponent.CONDITIONColumn, sWhere);
       //         resultArgs = dataManager.FetchData(DataSource.DataTable);
       //         if (resultArgs.Success)
       //         {
       //             rsPR = resultArgs.DataSource.Table;
       //         }
       //     }
       //     //Open the Table 'PRStaffTemp': This table contains the component default values of each staff
       //     using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchStaffTempDetailComponent, "StaffTemp"))
       //     {
       //         dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
       //         dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
       //         dataManager.Parameters.Add(dtComponent.CONDITIONColumn, sWhere);
       //         resultArgs = dataManager.FetchData(DataSource.DataTable);
       //         if (resultArgs.Success)
       //         {
       //             rsDefVal = resultArgs.DataSource.Table;
       //         }
       //     }
       //     try
       //     {
       //         this.dvDefVal = rsDefVal.DefaultView;
       //     }
       //     catch (Exception ex)
       //     {
       //         MessageBox.Show("Processing Error:" + ex.Message.ToString());
       //         return;
       //     }
       //     //Initialize Progress Bar Status
       //     nMax = dvCompM.Table.Rows.Count + 1;
       //     bProgress = ResetProgress(objProgress, nMax);

       //     if (bNewPayRollProcess)
       //     {
       //         dvCompM.RowFilter = "equationId = null";
       //         nMax = nMax + dvCompM.Table.Rows.Count + 1;
       //         bProgress = ResetProgress(objProgress, nMax);
       //         bNewPayRollProcess = false;
       //         dvCompM.RowFilter = "";
       //     }

       //     //DataTable dt = dvCompM.ToTable();
       //     //List<DataTable> tables = new List<DataTable>();
       //     //tables = SplitTable(dt, dt.Rows.Count / 5);
       //     //foreach (DataTable dtList in tables)
       //     //{
       //     // Thread thread = new Thread(() => UpdateCompdata(dtList.AsDataView(), rsComponent.AsDataView()));
       //     //thread.Start();
       //     //}
       //     UpdateCompdata(dvCompM, rsComponent.AsDataView());
       //     using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchStaffDetailsAfterProcess, "PrStaff"))
       //     {
       //         dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
       //         dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
       //         dataManager.Parameters.Add(dtComponent.CONDITIONColumn, sWhere);
       //         resultArgs = dataManager.FetchData(DataSource.DataTable);
       //         if (resultArgs.Success)
       //         {
       //             rsPR = resultArgs.DataSource.Table;
       //         }
       //     }
       //     if (!bModify)
       //     {
       //         clsModPay.ProcessRunning(false, nPRId, true);
       //     }
       // }

       // public List<DataTable> SplitTable(DataTable originalTable, int batchSize)
       // {
       //     List<DataTable> tables = new List<DataTable>();
       //     int i = 0;
       //     int j = 1;
       //     DataTable newDt = originalTable.Clone();
       //     newDt.TableName = "Table_" + j;
       //     newDt.Clear();
       //     foreach (DataRow row in originalTable.Rows)
       //     {
       //         DataRow newRow = newDt.NewRow();
       //         newRow.ItemArray = row.ItemArray;
       //         newDt.Rows.Add(newRow);
       //         i++;
       //         if (i == batchSize)
       //         {
       //             tables.Add(newDt);
       //             j++;
       //             newDt = originalTable.Clone();
       //             newDt.TableName = "Table_" + j;
       //             newDt.Clear();
       //             i = 0;
       //         }
       //     }
       //     return tables;
       // }

       // private void UpdateCompdata(DataView dvData, DataView dvCompData)
       // {
       //     long nCompId;
       //     string sEquationId = "";
       //     int nCompOrder = 0;
       //     string sRetVal = "";
       //     string sgetVal = "";
       //     double setDVal = 0.0;
       //     long ntmpstaffid = 0;
       //     for (int i = 0; i < dvData.Table.Rows.Count; i++)
       //     {
       //         ntmpstaffid = long.Parse(dvData.Table.Rows[i]["StaffId"] + "");
       //         DataView dvtmpstaff = FindStaff(rsStaff, ntmpstaffid);
       //         if (dvtmpstaff.ToTable().Rows.Count > 0)
       //         {
       //             nCompId = long.Parse(dvData.Table.Rows[i]["ComponentId"] + "");
       //             nCompOrder = int.Parse(dvData.Table.Rows[i]["comp_order"].ToString());
       //             sEquationId = dvData.Table.Rows[i]["EquationId"].ToString() + "";
       //             bEquation = (sEquationId.Trim() != "");
       //             if (dvData.Table.Rows[i]["MaxSlab"].ToString() != "")
       //                 setDVal = double.Parse(dvData.Table.Rows[i]["MaxSlab"].ToString());
       //             else
       //                 setDVal = 0.0;

       //             sRetVal = ProcessGetProcessedValue(long.Parse(dvData.Table.Rows[i]["ComponentId"] + ""), sEquationId, "",
       //                             double.Parse(setDVal + ""), int.Parse(dvData.Table.Rows[i]["CompRound"] + ""),
       //                 int.Parse(dvData.Table.Rows[i]["IFCondition"] + ""), ntmpstaffid, dvCompData);

       //             //  Added by Praveen to calculate Range Value
       //             DataView dvTempComp = dvCompData;
       //             dvTempComp.RowFilter = "componentid = " + nCompId;
       //             if (dvTempComp.Table.Rows.Count > 0)
       //             {
       //                 if ((commem.NumberSet.ToInteger(dvTempComp[0]["DefValue"].ToString()) == 0) &&
       //                           string.IsNullOrEmpty(dvTempComp[0]["LinkValue"] + "") && sEquationId.Trim() == "")
       //                 {
       //                     clscomp = new clsPrComponent();
       //                     if (clscomp.FetchRangeComponent(commem.NumberSet.ToInteger(nCompId.ToString())) > 0)
       //                     {
       //                         sRetVal = objmodBuild.GetRangeofValue(ntmpstaffid, nCompId);
       //                     }
       //                 }
       //             }

       //             sRetVal = sRetVal.Trim();
       //             sgetVal = ((sRetVal == "") ? null : sRetVal);
       //             using (DataManager dataManager = new DataManager(SQLCommand.Payroll.UpdateProcessedPayroll))
       //             {
       //                 dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
       //                 dataManager.Parameters.Add(dtComponent.PAYROLLIDColumn, nPayRollId);
       //                 dataManager.Parameters.Add(dtComponent.STAFFIDColumn, ntmpstaffid);
       //                 dataManager.Parameters.Add(dtComponent.COMPONENTIDColumn, nCompId);
       //                 dataManager.Parameters.Add(dtComponent.COMPVALUEColumn, sgetVal);
       //                 dataManager.Parameters.Add(dtComponent.COMPORDERColumn, nCompOrder);
       //                 dataManager.Parameters.Add(dtComponent.TRANSACTIONDATEColumn, commem.DateSet.ToDate(System.DateTime.Now.ToString()));

       //                 resultArgs = dataManager.UpdateData();
       //                 if (!resultArgs.Success)
       //                 {
       //                     return;
       //                 }
       //             }
       //         }
       //     }
       // }

       // #region Process Payroll Methods

       // public DataView FilterComponentbyId(DataView dvmveComp, long nCompId)
       // {
       //     dvmveComp.RowFilter = "";
       //     dvmveComp = rsComponent.DefaultView;
       //     if (dvmveComp.Count > 0)
       //     {
       //         dvmveComp.RowFilter = "componentid = " + nCompId;
       //     }
       //     return dvmveComp;
       // }

       // private string ProcessGetProcessedValue(long nCompId, string sEQ, string sVal, double dMaxSlab,
       //int nCompRnd, int nIFType, long tmpStaffId, DataView dvCompSample)
       // {
       //     int k = 0;
       //     int nType = 0, nCompRnd1 = 0;
       //     string sVal1 = "", sDefVal = "", sLnkVal = ""; //, sCompId = "";
       //     string sRetVal = ""; //,sEQ1 = "", sCharExp = "";

       //     string formula = "";
       //     string[] aformulaGroup;
       //     string[] aformula;
       //     string staffGroup = "";
       //     int formulaStaffGroupId = 0; //Contains list of StaffId for the formula

       //     try //catches error..
       //     {
       //         DataView dvtmp = FilterComponentbyId(dvCompSample, nCompId);
       //         if (dvtmp.Table.Rows.Count == 0) goto EndLine;
       //         nType = int.Parse(dvCompSample[0]["Type"] + "");
       //         sDefVal = Strings.Trim(dvCompSample[0]["DefValue"] + "");
       //         sLnkVal = Strings.Trim(dvCompSample[0]["LinkValue"] + "");
       //         nCompRnd1 = int.Parse(dvCompSample[0]["CompRound"] + "");

       //         if (sEQ.Trim() != "") //Equation
       //         {
       //             if (nIFType > 0) //It is an IF-Condition Equation
       //             {
       //                 aformulaGroup = sEQ.Split('$');

       //                 for (k = 0; k < aformulaGroup.Length; k++)
       //                 {
       //                     aformula = aformulaGroup[k].Split('~');
       //                     formula = aformula[0];
       //                     nIFType = Convert.ToInt32(aformula[1]);
       //                     formulaStaffGroupId = Convert.ToInt32(aformula[2]);
       //                     string defaultValue = ProcessGetDefaultValueForFormula(nCompId, tmpStaffId);
       //                     //                            double defaultValue = string.IsNullOrEmpty(defaultValue
       //                     if (nIFType > 0)
       //                     {
       //                         sVal1 = ProcessEvaluateIFEquation(nCompId, formula, nIFType, dvCompSample, tmpStaffId);
       //                     }
       //                     else if (!string.IsNullOrEmpty(defaultValue))
       //                     {
       //                         sVal1 = defaultValue.ToString();
       //                     }
       //                     else
       //                     {
       //                         sVal1 = processEvaluateEquation(nCompId, formula, sVal, nIFType, dMaxSlab, nCompRnd, dvCompSample, tmpStaffId);
       //                     }
       //                     //Expression is true
       //                     if (Convert.ToDouble(sVal1) > 0)
       //                     {
       //                         if (formulaStaffGroupId > 0)
       //                         {
       //                             staffGroup = objActivities.getFormulaGroupStaffIdCollection(formulaStaffGroupId);

       //                             if (staffGroup.IndexOf("@" + tmpStaffId.ToString() + "@") >= 0)
       //                             {
       //                                 break;
       //                             }
       //                         }
       //                         else //for all staff
       //                         {
       //                             break;
       //                         }
       //                     }
       //                 }
       //             }
       //             else
       //             {
       //                 sVal1 = processEvaluateEquation(nCompId, sEQ, sVal, nIFType, dMaxSlab, nCompRnd, dvCompSample, tmpStaffId);
       //             }
       //         }
       //         else //Link Value or Default Value
       //         {
       //             if (sLnkVal.Trim() != "") //Link Value  from 'stfPersonal' and 'stfService'
       //             {
       //                 sRetVal = GetLinkValue(sLnkVal, tmpStaffId);  //link values for income,deduction,text..
       //             }
       //             else //Default Value  from 'PRComponent' or 'stfStaffTemp'
       //             {
       //                 sRetVal = ProcessGetDefaultValue(sDefVal, nCompId, tmpStaffId); //Default value..
       //             }
       //             if (nType == 0 || nType == 1) //Income or Deduction
       //             {
       //                 if (sRetVal != "")
       //                     sRetVal = Math.Round(double.Parse(sRetVal), 2).ToString(); //Round Value
       //                 else
       //                 {
       //                     sRetVal = "0.0";
       //                     sRetVal = Math.Round(double.Parse(sRetVal), 2).ToString();
       //                 }
       //                 if (bEquation) sVal1 = sRetVal;
       //             }
       //         }
       //     EndLine:

       //         if (bEquation)
       //         {
       //             sVal = sVal + sVal1;
       //         }
       //         else
       //         {
       //             sVal = sRetVal;
       //         }

       //         return sVal;
       //     }
       //     catch (Exception ex)
       //     {
       //         MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
       //         return "0";
       //     }
       // }

       // private string ProcessGetDefaultValueForFormula(long nCompId, long tmpStaffid)
       // {
       //     DataTable prdt = null;
       //     string defaultValue = string.Empty;
       //     using (DataManager datamanager = new DataManager(SQLCommand.Payroll.FetchprDefValueDetails, "StaffTemp"))
       //     {
       //         datamanager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
       //         datamanager.Parameters.Add(dtComponent.COMPONENTIDColumn, nCompId);
       //         datamanager.Parameters.Add(dtComponent.STAFFIDColumn, tmpStaffid);
       //         datamanager.Parameters.Add(dtComponent.PAYROLLIDColumn, nPayRollId);
       //         resultArgs = datamanager.FetchData(DataSource.DataTable);
       //         if (resultArgs.Success)
       //         {
       //             prdt = resultArgs.DataSource.Table;
       //         }
       //         if (prdt != null && prdt.Rows.Count > 0)
       //         {
       //             string[] resultString = prdt.Rows[0][0].ToString().Split('.');
       //             defaultValue = resultString[0];
       //             defaultValue = defaultValue.Replace(",", "");
       //         }
       //     }
       //     return defaultValue;
       // }

       // private string ProcessGetDefaultValue(string sDefVal, long nCompId, long tmpStaffId)
       // {
       //     string sRetVal = "";
       //     this.dvDefVal.RowFilter = "staffid =" + tmpStaffId + " and componentid = " + nCompId;
       //     if (dvDefVal.Table != null && dvDefVal.Table.Rows.Count > 0)
       //     {
       //         var ResDefValues = (from SelectedItems in dvDefVal.ToTable().AsEnumerable()
       //                             where (SelectedItems.Field<System.UInt32>("staffid") == tmpStaffId
       //                                           && SelectedItems.Field<System.UInt32>("componentid") == nCompId)
       //                             select SelectedItems);
       //         if (ResDefValues.Count() > 0)
       //         {
       //             DataTable dtResDefValues = ResDefValues.CopyToDataTable();
       //             if (dtResDefValues != null && dtResDefValues.Rows.Count > 0)
       //             {
       //                 sDefVal = dtResDefValues.Rows[0]["COMPVALUE"].ToString();
       //             }
       //         }
       //     }
       //     sRetVal = sDefVal.Trim();
       //     using (DataManager dataManager = new DataManager(SQLCommand.Payroll.InsertNewDataValueForStaff))
       //     {
       //         dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
       //         dataManager.Parameters.Add(dtComponent.PAYROLLIDColumn, nPayRollId.ToString());
       //         dataManager.Parameters.Add(dtComponent.STAFFIDColumn, tmpStaffId.ToString());
       //         dataManager.Parameters.Add(dtComponent.COMPONENTIDColumn, nCompId.ToString());
       //         dataManager.Parameters.Add(dtComponent.COMPVALUEColumn, (sRetVal == "" ? "0" : sRetVal));
       //         resultArgs = dataManager.UpdateData();

       //         if (!resultArgs.Success)
       //             return string.Empty;
       //     }
       //     using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchStaffTempDetails, "StaffTemp"))
       //     {
       //         dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
       //         resultArgs = dataManager.FetchData(DataSource.DataTable);

       //         if (resultArgs.Success && resultArgs.RowsAffected > 0)
       //         {
       //             this.dvDefVal = resultArgs.DataSource.Table.DefaultView;
       //         }
       //     }
       //     return sRetVal;
       // }

       // private string ProcessEvaluateIFEquation(long nCompId, string sIFEQ, int nIFType, DataView dvCommp, long tpStaffId)
       // {
       //     int nOpr1, nOpr2, nLOpr = 0;

       //     long nCondCompId1 = 0, nCondCompId2;
       //     double dCondEQVal1 = 0.0, dCondEQVal2 = 0.0;
       //     double dCondVal1 = 0.0, dCondVal2 = 0.0, dRetVal = 0.0;
       //     string sEQ1 = "", sEQ2 = "", sEQ3 = "";
       //     string maxiSlab = "";
       //     string[] aIFEQ = null;
       //     bool bIF1 = false, bIF2 = false, bIF3 = false;

       //     aIFEQ = sIFEQ.Split((char)160);  //IF Equations are delimited by 'Chr$(160)'

       //     sEQ3 = objmodBuild.RemoveBrace(aIFEQ[0], true);
       //     //...sugan nCondCompId1 = long.Parse(this.RemoveBrace(aIFEQ[0], true, true)); //Condition Component 1 (eg: IF 'a')
       //     DataView dvtmp1 = FilterComponentbyId(dvCommp, nCompId);
       //     if (dvtmp1.Table.Rows.Count > 0)
       //     { //Evaluate First Conditional Component Value
       //         if (dvtmp1[0]["MaxSlap"].ToString() != "")
       //             maxiSlab = dvtmp1[0]["MaxSlap"].ToString();
       //         else
       //             maxiSlab = "0.0";
       //         dCondEQVal1 = double.Parse(ProcessGetProcessedValue(nCompId, sEQ3,
       //             "", double.Parse(maxiSlab + ""),
       //             int.Parse(dvtmp1[0]["CompRound"] + ""), 0, tpStaffId, dvCommp));
       //     }
       //     nOpr1 = int.Parse(objmodBuild.RemoveBrace(aIFEQ[1], true));
       //     dCondVal1 = double.Parse(objmodBuild.RemoveBrace(aIFEQ[2], true));
       //     /* Common Condition for all IF Type (There must be atleast on condition in the IF equation)
       //      * Relational Operators (nOpr1) Id : 1 =, 2 >, 3 <, 4 >=, 5 <=, 6 <>
       //      * Find out, which condition is matched for calculated value
       //      * */
       //     switch (nOpr1)
       //     {
       //         case 0:// =
       //             bIF1 = (dCondEQVal1 == dCondVal1);
       //             break;
       //         case 2: // >
       //             bIF1 = (dCondEQVal1 > dCondVal1);
       //             break;
       //         case 1: // <
       //             bIF1 = (dCondEQVal1 < dCondVal1);
       //             break;
       //         case 3: // >=
       //             bIF1 = (dCondEQVal1 >= dCondVal1);
       //             break;
       //         case 4: // <=
       //             bIF1 = (dCondEQVal1 <= dCondVal1);
       //             break;
       //         case 5: //!=
       //             bIF1 = (dCondEQVal1 != dCondVal1);
       //             break;
       //     }
       //     switch (nIFType)
       //     {
       //         case 1: //IF...ELSE
       //         case 3:
       //             if (bIF1)
       //             {
       //                 sEQ1 = objmodBuild.RemoveBrace(aIFEQ[3], true, false);
       //                 DataView dvtmp2 = FilterComponentbyId(dvCommp, nCompId);
       //                 if (dvtmp2.Table.Rows.Count > 0)
       //                 {
       //                     dRetVal = double.Parse(ProcessGetProcessedValue(nCompId, sEQ1, "", double.Parse(dvtmp2[0]["MaxSlap"] + ""), int.Parse(dvtmp2[0]["CompRound"] + ""), 0, tpStaffId, dvCommp));
       //                 }
       //             }
       //             else if (nIFType == 3)
       //             {// ELSE block True :Equation for 'ELSE' Block
       //                 sEQ2 = objmodBuild.RemoveBrace(aIFEQ[4], true);
       //                 DataView dvtmp2 = FilterComponentbyId(dvCommp, nCompId);
       //                 if (dvtmp2.Table.Rows.Count > 0)
       //                 {
       //                     dRetVal = double.Parse(ProcessGetProcessedValue(nCompId, sEQ2, "", double.Parse(dvtmp2[0]["MaxSlap"] + ""), int.Parse(dvtmp2[0]["CompRound"] + ""), 0, tpStaffId, dvCommp));
       //                 }
       //             }
       //             break;
       //         case 2:
       //         case 4: //IF...AND, IF...AND...ELSE
       //             nLOpr = int.Parse(objmodBuild.RemoveBrace(aIFEQ[3], true)); //Logical Operator 'AND or OR
       //             nCondCompId2 = long.Parse(objmodBuild.RemoveBrace(aIFEQ[4], true));
       //             DataView dvtmp3 = FilterComponentbyId(dvCommp, nCondCompId2);
       //             if (dvtmp3.Table.Rows.Count > 0)
       //             {
       //                 dCondEQVal2 = double.Parse(ProcessGetProcessedValue(nCondCompId2, Strings.Trim(dvtmp3[0]["equationid"].ToString() + ""), "", double.Parse(dvtmp3[0]["maxslap"].ToString() + ""), int.Parse(dvtmp3[0]["compround"].ToString() + ""), int.Parse(dvtmp3[0]["ifcondition"].ToString() + ""), tpStaffId, dvCommp));
       //             }

       //             //Second Operator in After AND/OR Condition in IF (Condition Bock After AND/OR in IF)
       //             nOpr2 = int.Parse(objmodBuild.RemoveBrace(aIFEQ[5], true));
       //             dCondVal2 = double.Parse(objmodBuild.RemoveBrace(aIFEQ[6], true)); //Condition Value 2
       //             //Find out, which condition is matched for calculated value
       //             switch (nOpr2)
       //             {
       //                 case 0: // =
       //                     bIF2 = (dCondEQVal2 == dCondVal2);
       //                     break;
       //                 case 2:// >
       //                     bIF2 = (dCondEQVal2 > dCondVal2);
       //                     break;
       //                 case 1: // <
       //                     bIF2 = (dCondEQVal2 < dCondVal2);
       //                     break;
       //                 case 3: // >=
       //                     bIF2 = (dCondEQVal2 >= dCondVal2);
       //                     break;
       //                 case 4: // <=
       //                     bIF2 = (dCondEQVal2 <= dCondVal2);
       //                     break;
       //                 case 5: // !=
       //                     bIF2 = (dCondEQVal2 != dCondVal2);
       //                     break;
       //             }
       //             if (nLOpr == 2) //OR (Logical Operator)
       //                 bIF3 = bIF1 || bIF2;
       //             else  // AND
       //                 bIF3 = bIF1 && bIF2;

       //             if (bIF3)
       //             {//Both Condition is True in IF...AND
       //                 sEQ2 = objmodBuild.RemoveBrace(aIFEQ[7], true);
       //                 DataView dvtmp4 = FilterComponentbyId(dvCommp, nCompId);
       //                 if (dvtmp4.Table.Rows.Count > 0)
       //                     dRetVal = double.Parse(ProcessGetProcessedValue(nCompId, sEQ2, "", double.Parse(dvtmp4[0]["MaxSlap"].ToString() + ""), int.Parse(dvtmp4[0]["compround"].ToString() + ""), 0, tpStaffId, dvCommp));
       //             }
       //             else if (nIFType == 4)
       //             { // IF...AND...ELSE :Both Condition is False in IF...AND
       //                 sEQ2 = objmodBuild.RemoveBrace(aIFEQ[8], true);
       //                 DataView dvtmp4 = FilterComponentbyId(dvCommp, nCompId);
       //                 if (dvtmp4.Table.Rows.Count > 0)
       //                     dRetVal = double.Parse(ProcessGetProcessedValue(nCompId, sEQ2, "", double.Parse(dvtmp4[0]["MaxSlap"].ToString() + ""), int.Parse(dvtmp4[0]["compround"].ToString() + ""), 0, tpStaffId, dvCommp));
       //             }
       //             break;
       //     }
       //     return (dRetVal.ToString() + "");
       // }	/*	To Find the Given Staff Id and move the record pointer to selected position */

       // private string GetLinkValue(string sFldName, long tpStaffid)
       // {
       //     string sVal = "", sLoanName = "";
       //     int nIncM1, nIncM2, nIncM = 0, nPRM = 0;
       //     int nMin, nMax;
       //     DataSet Rs = new DataSet();
       //     DataView dvtmpstaff = FindStaff(rsStaff, tpStaffid);
       //     if (dvtmpstaff.Table.Rows.Count <= 0)
       //         goto EndLine;

       //     if (Strings.UCase(Strings.Left(sFldName, 6)) == "LOAN :") // Type : Deduction, No:1
       //     {
       //         sLoanName = sFldName.Substring(7).Trim();
       //         sVal = objmodBuild.GetLoanPaidAmount(sLoanName).ToString();
       //     }
       //     else if (sFldName.ToUpper() == "INCREMENTMONTH")
       //     {
       //         nIncM1 = int.Parse(dvStaff[0][0].ToString() + "");
       //         nIncM2 = int.Parse(dvStaff[0][0].ToString() + "");

       //         if ((clsGeneral.PAYROLLDATE == "") || (nIncM1 <= 0 && nIncM2 <= 0))
       //             goto EndLine;
       //         nPRM = DateTime.Parse(clsGeneral.PAYROLLDATE).Month;

       //         // Get the Next Increment Date
       //         if (nIncM1 > 0 && nIncM2 > 0)
       //         {
       //             nMin = (nIncM1 <= nIncM2) ? nIncM1 : nIncM2;
       //             nMax = (nIncM1 >= nIncM2) ? nIncM1 : nIncM2;

       //             if ((nMin < nPRM && nMax < nPRM) || (nMin >= nPRM && nMax >= nPRM))
       //                 nIncM = nMin;
       //             else
       //                 if (nMin >= nPRM) nIncM = nMin;
       //             if (nMax >= nPRM) nIncM = nMax;
       //         }
       //         else
       //             nIncM = (nIncM1 > 0) ? nIncM1 : nIncM2;

       //         if (nIncM == 0)
       //             goto EndLine;
       //         sVal = "01/" + nIncM + "/" + ((nIncM >= nPRM) ? DateTime.Parse(clsGeneral.PAYROLLDATE).Year : DateTime.Parse(clsGeneral.PAYROLLDATE).Year + 1);
       //     }
       //     else if (sFldName.ToUpper() == "BASICPAY")  // Type : Income, No:0
       //     {
       //         sVal = dvtmpstaff[0]["basicpay"].ToString();
       //         //sVal = dvStaff[0][sFldName].ToString() + "";
       //         nIncM1 = string.IsNullOrEmpty(dvtmpstaff[0]["PayIncM1"].ToString()) ? 0 : int.Parse(dvtmpstaff[0]["PayIncM1"].ToString());
       //         nIncM2 = string.IsNullOrEmpty(dvtmpstaff[0]["PayIncM2"].ToString()) ? 0 : int.Parse(dvtmpstaff[0]["PayIncM2"].ToString());
       //         if ((bNewPayRollProcess) && ((nIncM1 == DateTime.Parse(clsGeneral.PAYROLLDATE).Month) || (nIncM2 == DateTime.Parse(clsGeneral.PAYROLLDATE).Month)))
       //         {
       //             sVal = this.GetBasicPay().ToString();
       //         }
       //     }

       //     else if (sFldName.ToUpper() == "DEPTID")
       //     {
       //         sVal = dvtmpstaff[0]["DepartmentId"].ToString() + "";
       //     }
       //     else if (sFldName == "Account_Number")
       //     {
       //         sVal = dvtmpstaff[0]["Account_Number"].ToString() + "";
       //     }
       //     else
       //         sVal = dvtmpstaff[0][sFldName].ToString() + "";  // Type : Text, No:2
       // EndLine:
       //     return sVal;
       // }

       // private string processEvaluateEquation(long nCompId, string sEQ, string sVal, int nIFType,
       //  double dMaxSlab, int nCompRnd, DataView dvCommp, long tpStaffId)
       // {
       //     int i, nPos = 0, nPos1, nEvalExpr = 0;
       //     int nIFType1, nCompRnd1;
       //     long nCompId1;
       //     double dMaxSlab1 = 0.0;
       //     string sCompId = "";
       //     string sVal1 = "", sEQ1 = "", sCharExp = "";

       //     for (i = 1; i <= sEQ.Length; i++)
       //     {
       //         sCharExp = Strings.Mid(sEQ, i, 1); //sEQ.Substring(i,1);
       //         if (sCharExp != "<" && sCharExp != ">")
       //         {
       //             sVal1 = sVal1 + sCharExp;
       //         }
       //         else
       //         {
       //             nPos = Strings.InStr(i, sEQ, "<", CompareMethod.Text); // sEQ.IndexOf("<",i);
       //             if (nPos > 0)
       //             {
       //                 nPos1 = Strings.InStr(i, sEQ, ">", CompareMethod.Text); //sEQ.IndexOf(">",i);
       //                 if (nPos1 > 0)
       //                 {
       //                     sCompId = Strings.Mid(sEQ, nPos, nPos1 - (nPos - 1));
       //                     //sCompId = sEQ.Substring(nPos, nPos1 - (nPos - 1));
       //                     nCompId1 = long.Parse(objmodBuild.RemoveBrace(sCompId, false, true, 1));

       //                     DataView dvtm = FilterComponentbyId(dvCommp, nCompId1);
       //                     if (dvtm.Table.Rows.Count > 0)
       //                     {
       //                         //These values are only for Equation Type Field
       //                         //sEQ1="(<9>-<14>)";
       //                         clscomp = new clsPrComponent();
       //                         if (clscomp.FetchRangeComponent(NumberSet.ToInteger(nCompId1.ToString())) > 0)
       //                         {
       //                             if (dvtm[0]["Type"].ToString() == "1"
       //                                 && dvtm[0]["DefValue"].ToString() == "0"
       //                                 && dvtm[0]["LinkValue"].ToString().ToString() == ""
       //                                 && dvtm[0]["Equation"].ToString().ToString() == ""
       //                                 && dvtm[0]["EquationId"].ToString() == "")
       //                             {
       //                                 sVal1 += GetRangeofValue(tpStaffId, nCompId1);
       //                                 i = nPos1;
       //                             }
       //                         }
       //                         else
       //                         {
       //                             sEQ1 = Strings.Trim(dvtm[0]["EquationId"] + "");
       //                             //MessageBox.Show(rsComponent.Tables[0].Rows[i]["CompRound"].ToString());
       //                             if (dvtm[0]["CompRound"].ToString() != "")
       //                                 nCompRnd1 = int.Parse(dvtm[0]["CompRound"].ToString() + "");
       //                             else
       //                                 nCompRnd1 = 0;
       //                             //MessageBox.Show(rsComponent.Tables[0].Rows[i]["MaxSlap"].ToString());
       //                             if (dvtm[0]["MaxSlap"].ToString() != "")
       //                                 dMaxSlab1 = double.Parse(dvtm[0]["MaxSlap"].ToString() + "");
       //                             else
       //                                 dMaxSlab1 = 0.0;
       //                             if (dvtm[0]["IFCondition"].ToString() != "")
       //                                 nIFType1 = int.Parse(dvtm[0]["IFCondition"].ToString() + "");
       //                             else
       //                                 nIFType1 = 0;
       //                             //Recursive function to calculate the interlinked Components value
       //                             sVal1 = ProcessGetProcessedValue(nCompId1, sEQ1, sVal1, dMaxSlab1, nCompRnd1, nIFType1, tpStaffId, dvCommp);
       //                             i = nPos1;
       //                         }
       //                     }
       //                 }
       //             }
       //         }
       //     }

       // EndLine:

       //     //It is a function which is used to evaluate the Expression
       //     nEvalExpr = this.EvaluateExpression(sVal1);

       //     if (nEvalExpr == 0) //Errors in Equation (Return Value : 0-Error, -1-Not error)
       //     {
       //         //MsgBox "Errors in Equation !", vbInformation, g_Message
       //     }
       //     //sVal1= dRetVal +""; the existing vb code
       //     sVal1 = nEvalExpr.ToString();
       //     sVal1 = RoundValue(double.Parse(sVal1), nCompRnd).ToString(); //Round Value
       //     //Check the Maximum Slab Value
       //     if (dMaxSlab > 0) sVal1 = ((double.Parse(sVal1) > dMaxSlab) ? dMaxSlab.ToString() : sVal1);
       //     sVal = sVal1; // sVal + sVal1 + "";
       //     return sVal; //Return value for Equation component
       // }
       // private int EvaluateExpression(string sVal1)
       // {
       //     try
       //     {
       //         object objCheck = (clsGeneral.ObjExcel).Evaluate(sVal1);

       //         if (Convert.ToInt32(objCheck) >= 0)
       //         {
       //             return Convert.ToInt32(objCheck);
       //         }
       //         else
       //         {
       //             return 0; //Invalid formula
       //         }
       //     }
       //     catch (Exception ex)
       //     {
       //         return 0;
       //     }
       // }

       // private double RoundValue(double dAmount, int nRType)
       // {
       //     double dAmt;
       //     dAmt = dAmount;
       //     switch (nRType)
       //     {
       //         case 0: //Ceiling (Eg: 51.15 => 52)
       //             dAmt = System.Math.Ceiling(dAmt);
       //             break;
       //         case 1: //Floor (Eg: 51.99 => 51)
       //             dAmt = Conversion.Int(dAmt);
       //             break;
       //         case 2: //Rounded to Next or Previous Integer (Eg: 51.49 => 51 or 51.50 => 52)
       //             dAmt = System.Math.Round(dAmt);
       //             break;
       //     }
       //     return dAmt;
       // }

       // private double GetBasicPay()
       // {
       //     string sScaleofPay = "";
       //     string[] aScaleofPay = new string[50];
       //     double dBPay = 0.0, dCurBP = 0.0;
       //     long nServiceId = 0;

       //     if (dvStaff[0]["BasicPay"].ToString() != "")
       //         dBPay = double.Parse(dvStaff[0]["BasicPay"].ToString() + "");
       //     else dBPay = 0.0;
       //     dCurBP = dBPay;
       //     sScaleofPay = Strings.Trim(dvStaff[0]["ScaleofPay"].ToString() + "");
       //     nServiceId = long.Parse(dvStaff[0]["ServiceId"].ToString() + "");

       //     if (sScaleofPay != "")
       //     {
       //         //ScaleofPay 5000-500-25000 (0-Basic Bay,1-Increment, 2-Max Slab)
       //         aScaleofPay = sScaleofPay.Split('-');
       //         if (aScaleofPay.GetUpperBound(0) == 2)
       //         {
       //             //find if BPay Reaches the Maximum slab
       //             if (int.Parse(aScaleofPay[1].ToString()) > 0)
       //             {
       //                 if (int.Parse(aScaleofPay[2].ToString()) <= 0)
       //                 {
       //                     dBPay = dBPay + int.Parse(aScaleofPay[1].ToString());
       //                 }
       //                 else
       //                 {
       //                     if (dBPay + int.Parse(aScaleofPay[1].ToString()) <= int.Parse(aScaleofPay[2].ToString()))
       //                     {
       //                         dBPay = dBPay + int.Parse(aScaleofPay[1].ToString());
       //                     }
       //                 }
       //             }
       //             if (dBPay != dCurBP)
       //             {
       //                 objmodBuild.UpdateBasicPay(dBPay, nServiceId);
       //             }
       //         }
       //     }
       //     return dBPay;
       // }

       // public DataView FindStaff(DataSet dsStf, long nStaffId)
       // {
       //     DataView dcsff = new DataView();
       //     dcsff.RowFilter = "";
       //     if (dsStf != null && dsStf.Tables.Count > 0)
       //     {
       //         dcsff = dsStf.Tables[0].DefaultView;
       //         if (dcsff.Count >= 0)
       //             dcsff.RowFilter = "staffid = " + nStaffId;
       //     }
       //     return dcsff;
       // }

       // public bool ResetProgress(ProgressBar objProcess, int nMax)
       // {
       //     if (objProcess != null)
       //     {
       //         // objProcess.Minimum = 0;
       //         //objProcess.Maximum = nMax;
       //         //  objProcess.Value = 0;
       //         return true;
       //     }
       //     return false;
       // }

       // public void SetProgressValue(ProgressBar objProg, bool bProgress)
       // {
       //     if (bProgress)//Set the Progress value according to the loop
       //     {
       //         // if (objProg.Value < objProg.Maximum)
       //         // objProg.Value = objProg.Value + 1;
       //     }
       // }

       // public string GetRangeofValue(long Staffid, long CompId)
       // {
       //     string rtnval = "0.00";
       //     string rtntmpval = "0.00";
       //     ResultArgs result = new ResultArgs();
       //     DataTable dtRangeList = new DataTable();
       //     string linkComId = string.Empty;

       //     DataTable dtTemp = new DataTable();

       //     try
       //     {
       //         result = objmodBuild.FetchRangeValuesbyCompId(CompId);
       //         if (result.Success && result.DataSource.Table.Rows.Count > 0)
       //         {
       //             dtRangeList = result.DataSource.Table;
       //             foreach (DataRow dr in dtRangeList.Rows)
       //             {
       //                 linkComId = dr["LINK_COMPONENT_ID"].ToString();
       //                 dvCompM.RowFilter = "STAFFID=" + Staffid + " AND COMPONENTID=" + linkComId + "";
       //                 dtTemp = dvCompM.ToTable();
       //                 if (dtTemp != null && dtTemp.Rows.Count > 0)
       //                 {
       //                     foreach (DataRow drtemp in dtTemp.Rows)
       //                     {
       //                         rtntmpval = ProcessGetProcessedValue(long.Parse(linkComId), drtemp["EquationId"].ToString() + "", "",
       //                                                       double.Parse(drtemp["MaxSlab"].ToString() + ""), int.Parse(drtemp["CompRound"] + ""),
       //                                                       int.Parse(drtemp["IFCondition"] + ""), Staffid, rsComponent.AsDataView());
       //                         rtnval = ProcessCompRangeValue(CompId.ToString(), rtntmpval);
       //                     }
       //                 }
       //             }
       //         }
       //     }
       //     catch (Exception ex)
       //     {
       //         MessageRender.ShowMessage(ex.Message + System.Environment.NewLine + ex.Source, true);
       //         return "0";
       //     }
       //     return rtnval;
       // }

       // public string ProcessCompRangeValue(string compId, string lnkCompValue)
       // {
       //     string ptrtnVal = string.Empty;
       //     double lnkcomamount = commem.NumberSet.ToDouble(lnkCompValue);
       //     long compID = 0;
       //     double MinVal = 0;
       //     double MaxVal = 0;
       //     double RangeMaxSlab = 0;
       //     DataTable dtRangeList = new DataTable();
       //     ResultArgs result = new ResultArgs();

       //     compID = commem.NumberSet.ToInteger(compId);
       //     result = objmodBuild.FetchRangeValuesbyCompId(compID);
       //     if (result.Success && result.DataSource.Table.Rows.Count > 0)
       //     {
       //         dtRangeList = result.DataSource.Table;
       //         foreach (DataRow dr in dtRangeList.Rows)
       //         {
       //             MinVal = commem.NumberSet.ToDouble(dr["MIN_VALUE"].ToString());
       //             MaxVal = commem.NumberSet.ToDouble(dr["MAX_VALUE"].ToString());
       //             RangeMaxSlab = commem.NumberSet.ToDouble(dr["MAX_SLAB"].ToString());

       //             if (lnkcomamount >= MinVal && lnkcomamount <= MaxVal)
       //             {
       //                 ptrtnVal = RangeMaxSlab.ToString();
       //             }
       //         }
       //     }
       //     return ptrtnVal;
       // }

       // public ResultArgs FetchStaffDetails(string GrpID, long PayID)
       // {
       //     resultArgs = new ResultArgs();
       //     using (DataManager dataManager = new DataManager(SQLCommand.Payroll.ProcessPayrollInOrder, "CompMonth"))
       //     {
       //         string sWhere = "PRCompMonth.PayRollId = " + PayID + ((GrpID != "") ? " AND " +
       //              "SalaryGroupId IN (" + GrpID + ")" : "");
       //         dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
       //         dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
       //         dataManager.Parameters.Add(dtComponent.CONDITIONColumn, sWhere);
       //         dataManager.Parameters.Add(dtComponent.PAYROLLIDColumn, PayID);
       //         dataManager.Parameters.Add(dtComponent.PAYROLLDATEColumn, commem.DateSet.ToDate(clsGeneral.PAYROLLDATE));
       //         resultArgs = dataManager.FetchData(DataSource.DataTable);
       //     }
       //     return resultArgs;
       // }

       // public void CreateStaffRSet(string sGroupId, string sStaffId)
       // {
       //     string sStaffSQL, sWhere, sToDate;

       //     DataTable dtStaffTable = null;
       //     //------ Create Staff Information Recordset (for getting Link Value of a payroll) -------
       //     //It Contains Personal Info, Pay Info about Staff

       //     sToDate = commem.DateSet.ToDate(clsGeneral.PAYROLLDATE, false).AddMonths(1).AddDays(-1).ToShortDateString();
       //     sWhere = ((sStaffId != "") ? " AND stfPersonal.StaffId IN (" + sStaffId + ")" : "") +
       //         ((sGroupId != "") ? " AND PRStaffGroup.GroupId IN (" + sGroupId + ")" : "");
       //     //"AND " + "To_Date('" + sToDate + "', 'dd/mm/yyyy')" +
       //     //" >= stfPersonal.dateofJoin and (stfPersonal.LeavingDate is null or " +
       //     //"stfPersonal.LeavingDate > " + "To_Date('" + DateTime.Parse(clsGeneral.PAYROLLDATE).ToShortDateString() + "', 'dd/mm/yyyy')" + ") " +
       //     //" AND ((" + "To_Date('" + sToDate + "', 'dd/mm/yyyy')" + " BETWEEN " +
       //     //"stfService.DateofAppointment AND stfService.DateofTermination) " +
       //     //" OR (stfService.DateofTermination is null AND " +
       //     //"To_Date('" + sToDate + "', 'dd/mm/yyyy')" + " > stfService.DateofAppointment))";

       //     //sStaffSQL = "SELECT stfPersonal.StaffId,stfService.serviceid as ServiceId,EmpNo as \"EmployeeNo\",FirstName,LastName," +
       //     //    "firstname || ' ' || lastname as \"Name\"," +
       //     //    "KnownAs,Gender,DateofBirth,DateofJoin,DateofAppointment," +
       //     //    "Designation,Department," +
       //     //    "RetirementDate,stfService.Pay AS BasicPay," +
       //     //    "stfService.ScaleofPay,PayIncM1,PayIncM2 " +
       //     //    "FROM stfPersonal,stfService,PRStaffGroup,hospital_departments WHERE " +
       //     //    "stfPersonal.StaffId = PRStaffGroup.StaffId AND " +
       //     //    "PRStaffGroup.Payrollid = " + nPayRollId +
       //     //    " and stfPersonal.StaffId = stfService.StaffId AND " +
       //     //    "stfPersonal.deptid = hospital_departments.hdept_id AND " + sWhere +
       //     //    " ORDER BY stfPersonal.StaffId"; DataTable dtFetchTopId = null;

       //     using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchStaffDetails, "Staff"))
       //     {
       //         dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
       //         dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
       //         dataManager.Parameters.Add(dtComponent.PAYROLLDATEColumn, commem.DateSet.ToDate(clsGeneral.PAYROLLDATE));
       //         dataManager.Parameters.Add(dtComponent.PAYROLLIDColumn, nPayRollId);
       //         dataManager.Parameters.Add(dtComponent.TODATEColumn, sToDate);
       //         dataManager.Parameters.Add(dtComponent.CONDITIONColumn, sWhere);
       //         dataManager.Parameters.Add(dtComponent.LEAVEREMARKSColumn, clsGeneral.LeavingReason.Resigned.ToString());
       //         resultArgs = dataManager.FetchData(DataSource.DataTable);

       //         if (resultArgs.Success)
       //         {
       //             dtStaffTable = resultArgs.DataSource.Table;
       //         }
       //     }
       //     rsStaff = new DataSet();
       //     rsStaff.Tables.Add(dtStaffTable);
       //     rsStaff.AcceptChanges();
       //     dvStaff = dtStaffTable.DefaultView;
       // }

       // public void CreateLoanRSet(string sStaffId)
       // {
       //     string sLoanGSQL = "", sLoanPSQL = "", sWhere = "";

       //     //------ SQL for Staff Loan Get Information ----------------------------------
       //     sWhere = ((sStaffId != "") ? " StaffId IN (" + sStaffId + ")" : "");
       //     sWhere = ((sWhere != "") ? " WHERE " + sWhere : "");
       //     //sLoanGSQL = "SELECT * FROM PRLoanGet" + ((sWhere != "") ? " WHERE " + sWhere : "") +
       //     //    " ORDER BY PRLoanGetId";
       //     //dh.createDataSet(sLoanGSQL, "LoanGet");
       //     //rsLoanG = dh.getDataSet();


       //     //------ SQL for Staff Loan Get Information ----------------------------------
       //     using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchStaffLoanGet, "LoanGet"))
       //     {
       //         dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
       //         //dataManager.Parameters.Add(sWhere);
       //         dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
       //         dataManager.Parameters.Add(dtComponent.CONDITIONColumn, sWhere);
       //         resultArgs = dataManager.FetchData(DataSource.DataTable);

       //         if (resultArgs.Success)
       //         {
       //             rsLoanG = resultArgs.DataSource.Table;
       //         }
       //     }
       //     //--------------------------------------------------------------------------

       //     //------ SQL for Staff Loan Paid Information ----------------------------------
       //     sWhere = "PayRollId = " + nPayRollId + ((sStaffId != "") ? " AND StaffId IN (" + sStaffId + ")" : "");
       //     using (DataManager dataManager = new DataManager(SQLCommand.Payroll.FetchStaffLoanPaid, "LoanPaid"))
       //     {
       //         dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
       //         //  dataManager.Parameters.Add(sWhere);
       //         dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
       //         dataManager.Parameters.Add(dtComponent.CONDITIONColumn, sWhere);
       //         resultArgs = dataManager.FetchData(DataSource.DataTable);

       //         if (resultArgs.Success)
       //         {
       //             rsLoanP = resultArgs.DataSource.Table;
       //         }
       //     }
       //     dvLoanP = rsLoanP.DefaultView;
       // }

       // #endregion

    }
}
