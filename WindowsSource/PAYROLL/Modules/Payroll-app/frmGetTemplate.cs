using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.OleDb;
using Bosco.Model.UIModel;
using Bosco.Utility.ConfigSetting;
using Payroll.Model.UIModel;
using Bosco.Model.Transaction;
using System.Text.RegularExpressions;
using Bosco.Utility.Common;
using Microsoft.VisualBasic;
using DevExpress.XtraEditors;
using Bosco.Utility.Common;
using System.Data.OleDb;
using Bosco.Utility;
using System.IO;
using System.Reflection;
using System.Diagnostics;
using DevExpress.XtraLayout;

namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmGetTemplate : frmPayrollBase
    {

        #region Declaration
        public static string ACPERP_GENERAL_LOG = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "AcppGeneralLog.txt");
        int RetrunValue;
        ResultArgs resultArgs = null;
        CommonMember commem = new CommonMember();
        PayrollSystem paysys = new PayrollSystem();
        public event EventHandler UpdateHeld;
        #endregion

        #region Constructor
        public frmGetTemplate()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        int sStaffId = 0;
        private int StaffId
        {
            set
            {
                sStaffId = value;
            }
            get
            {
                return sStaffId;
            }
        }
        private string FileName { get; set; }
        #endregion

        #region Methods
        public DataTable conExcel(string query, string tablename, string connectionstring)
        {
            string connstr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + connectionstring + ";Extended Properties='Excel 8.0'";
            DataTable dt = new DataTable();
            try
            {

                OleDbConnection conn = new OleDbConnection(connstr);
                conn.Open();
                DataTable dtExcelSchema = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
                foreach (DataRow dr in dtExcelSchema.Rows)
                {
                    DataRow drSheetname = dr;
                    string nm = drSheetname["TABLE_NAME"].ToString();
                    tablename = tablename + '$';
                    if (nm.Equals(tablename))
                    {
                        OleDbCommand cmd = new OleDbCommand(query, conn);
                        OleDbDataAdapter ada = new OleDbDataAdapter(cmd);
                        ada.Fill(dt);
                    }
                    else
                    {
                        //XtraMessageBox.Show("Invalid data", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.GetTemplate.GET_TEMP_INVALID_DATA_INFO));
                    }
                }

                conn.Close();
            }
            catch (Exception ex)
            {
                //XtraMessageBox.Show("Excel file is opened already close while Importing","Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.GetTemplate.GET_TEMP_VALIDATE_EXCEL_INFO));
            }
            return dt;
        }
        public static void WriteLog(string msg)
        {
            string logpath = ACPERP_GENERAL_LOG;
            try
            {
                StreamWriter sw = new StreamWriter(logpath, true);

                if (msg.Replace("-", "").Length > 0)
                {
                    msg = (DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt")) + " || " + msg;
                }
                sw.WriteLine(msg);
                sw.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Error in writing log " + ex.Message);
            }
        }

        private void LoadDefaults()
        {
            this.Height = 99;
            layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //  layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            layoutControlGroup2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }
        private bool IsValidRow(DataRow dr)
        {
            bool isvalid = false;
            if (SettingProperty.PayrollFinanceEnabled)
            {
                if (!string.IsNullOrEmpty(dr["Employee number"].ToString()) && !string.IsNullOrEmpty(dr["Firstname"].ToString())
                    && !string.IsNullOrEmpty(dr["Date of Birth"].ToString()) && !string.IsNullOrEmpty(dr["Date of Join"].ToString())
                    && !string.IsNullOrEmpty(dr["Gender"].ToString()) && !string.IsNullOrEmpty(dr["Designation"].ToString())
                    && !string.IsNullOrEmpty(dr["Group"].ToString()) && !string.IsNullOrEmpty(dr["Project"].ToString()))
                {
                    isvalid = true;
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(dr["Employee number"].ToString()) && !string.IsNullOrEmpty(dr["Firstname"].ToString())
                   && !string.IsNullOrEmpty(dr["Date of Birth"].ToString()) && !string.IsNullOrEmpty(dr["Date of Join"].ToString())
                   && !string.IsNullOrEmpty(dr["Gender"].ToString()) && !string.IsNullOrEmpty(dr["Designation"].ToString())
                   && !string.IsNullOrEmpty(dr["Group"].ToString()))
                {
                    isvalid = true;
                }
            }
            return isvalid;
        }
        #endregion

        #region Events
        private void btnBrowse_Click(object sender, EventArgs e)
        {
            try
            {
                ofdImportExcel.Filter = "Excel Files (.xlsx)|*.xlsx|(.xls)|*.xls";
                if (ofdImportExcel.ShowDialog() == DialogResult.OK)
                {
                    FileName = ofdImportExcel.FileName;
                    txtImport.Text = FileName;
                }
                if (!string.IsNullOrEmpty(FileName))
                {
                    layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION);
            }
            finally { }
        }


        private void llgettemplate_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            try
            {
                string path = Path.Combine(Application.StartupPath.ToString(), "STAFF_DETAILS_TEMPLATE.xlsx");
                if (File.Exists(Path.Combine(Application.StartupPath.ToString(), "STAFF_DETAILS_TEMPLATE.xlsx")))
                {
                    Process.Start(path);
                }
                else
                {
                    //XtraMessageBox.Show("File is not found");
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.GetTemplate.GET_TEMP_VALIDATE_FILE_INFO));
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION);
            }
            finally { }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                pbcImport.Enabled = true;
                meRemarks.Text = string.Empty;
                string message = string.Empty;
                int projectExists;
                string projectName = string.Empty;
                string ProjectList = string.Empty;
                using (clsPayrollStaff objclspaystaff = new clsPayrollStaff())
                {
                    //ShowwaitDialog("Fetching data from excel");
                    ShowwaitDialog(this.GetMessage(MessageCatalog.Payroll.GetTemplate.GET_TEMP_FETCH_DATA_EXCEL_INFO));
                    if (!string.IsNullOrEmpty(FileName))
                    {
                        DataTable dt = new DataTable();
                        dt = conExcel("SELECT * FROM [stfpersonal$]", "stfpersonal", FileName);
                        pbcImport.Properties.Maximum = dt.Rows.Count + 1;
                        //pbcImport.Properties.Step = 1;

                        if (dt != null && dt.Rows.Count > 0)
                        {
                            //Filtering the groups
                            DataTable dtresult = dt.AsEnumerable()
                                        .GroupBy(r => r.Field<string>("Group")).Select(g => g.First()).CopyToDataTable();
                            DataView dvGroups = dtresult.AsDataView();
                            dtresult = dvGroups.ToTable("Selected", false, "Group");

                            if (SettingProperty.PayrollFinanceEnabled)
                            {
                                //Filtering the projects
                                DataTable dtproject = dt.AsEnumerable()
                                             .GroupBy(r => r.Field<string>("Project")).Select(g => g.First()).CopyToDataTable();
                                DataView dvProject = dtproject.AsDataView();
                                dtproject = dvProject.ToTable("Selected", false, "Project");


                                if (dtproject != null && dtproject.Rows.Count > 0 && !string.IsNullOrEmpty(dtproject.Columns["Project"].ToString()))
                                {
                                    foreach (DataRow drproject in dtproject.Rows)
                                    {
                                        if (drproject != null && !string.IsNullOrEmpty(drproject["Project"].ToString()))
                                        {
                                            projectName = drproject["Project"].ToString();
                                            projectExists = objclspaystaff.CheckProjectExists(projectName);
                                            if (projectExists == 0)
                                            {
                                                ProjectList += projectName + ",";
                                            }
                                        }
                                    }
                                    ProjectList = ProjectList.TrimEnd(',');

                                    if (string.IsNullOrEmpty(ProjectList))
                                    {
                                        resultArgs = objclspaystaff.MapExcelProjectstoPayroll(dtproject);
                                        if (resultArgs != null && resultArgs.Success)
                                        {
                                            resultArgs = objclspaystaff.UpdateExcelgroups(dtresult);

                                            if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                                            {
                                                pbcImport.Properties.Step = 1;
                                                foreach (DataRow dr in dt.Rows)
                                                {
                                                    if (IsValidRow(dr))
                                                    {
                                                        pbcImport.PerformStep();
                                                        objclspaystaff.StaffId = StaffId == 0 ? 0 : StaffId;
                                                        // Staff.IncrementMonth = dr["PAYINCM1"].ToString();
                                                        objclspaystaff.ScaleofPay = dr["Scale of pay"].ToString();

                                                        string getProjectName = dr["Project"].ToString();
                                                        string getgroupName = dr["Group"].ToString();

                                                        DataTable dtProjectId = objclspaystaff.FetchProjectIdByProjectName(getProjectName).DataSource.Table;
                                                        DataTable dtGroupId = objclspaystaff.FetchGroupIdByGroupName(getgroupName).DataSource.Table;

                                                        Int32 GroupId = commem.NumberSet.ToInteger(dtGroupId.Rows[0]["GROUPID"].ToString());
                                                        Int32 ProjectId = commem.NumberSet.ToInteger(dtProjectId.Rows[0]["PROJECT_ID"].ToString());
                                                        string grpStaff = StaffId != 0 ? StaffId.ToString() : objclspaystaff.RtnStaffid;

                                                        resultArgs = objclspaystaff.savePayrollStaffData(GroupId, dr["Employee number"].ToString(),
                                                            dr["Employee number"].ToString(),
                                                            dr["Firstname"].ToString(),
                                                            dr["middle_name"].ToString(),
                                                            dr["father_husband_name"].ToString(),
                                                            dr["mother_name"].ToString(),
                                                            dr["no_of_children"].ToString(),
                                                            dr["blood_group"].ToString(),
                                                            dr["last_date_of_contract"].ToString(),
                                                            dr["Lastname"].ToString(),
                                                            dr["Gender"].ToString(),
                                                            dr["Date of Birth"].ToString(),
                                                            dr["Date of Join"].ToString(),
                                                            dr["Retirement date"].ToString(),
                                                            dr["Known as"].ToString(),
                                                            dr["Leaving date"].ToString(),
                                                            dr["Leaving reason"].ToString(),
                                                            dr["Designation"].ToString(),
                                                            dr["Qualification"].ToString(),
                                                            string.Empty, 0, 0,
                                                            dr["Earning1"].ToString(),
                                                            dr["Earning2"].ToString(),
                                                            dr["Earning3"].ToString(),
                                                            dr["Deduction1"].ToString(),
                                                            dr["Deduction2"].ToString(),
                                                            dr["paying_salary_days"].ToString(),
                                                            dr["UAN"].ToString(),
                                                            StaffId == 0 ? clsPayrollConstants.PAYROLL_STAFF_INSERT : clsPayrollConstants.PAYROLL_STAFF_EDIT,
                                                            0,
                                                            dr["Scale of pay"].ToString(), dr["Account number"].ToString(), this.commem.NumberSet.ToDouble(dr["Year of service"].ToString()), dr["command_on_performance"].ToString(), dr["address"].ToString(),
                                                            dr["mobile_no"].ToString(), dr["telephone_no"].ToString(), dr["emergency_contact_no"].ToString(), dr["email_id"].ToString(), dr["dependent1"].ToString(), dr["dependent2"].ToString(), dr["dependent3"].ToString(), dr["work_experience"].ToString(), dr["pan_no"].ToString(), dr["aadhar_no"].ToString(), string.Empty, string.Empty);

                                                        if (resultArgs.Success)
                                                        {
                                                           

                                                            RetrunValue = commem.NumberSet.ToInteger(grpStaff);

                                                            /*using (clsPrGroupStaff GroupStaff = new clsPrGroupStaff())
                                                            {
                                                                resultArgs = GroupStaff.DeleteStaffGroup(grpStaff);
                                                                if (resultArgs.Success)
                                                                {
                                                                    string strResult = GroupStaff.SaveNewStaffInGroup(GroupId, grpStaff);
                                                                    if (!string.IsNullOrEmpty(grpStaff))
                                                                    {
                                                                        resultArgs = GroupStaff.DeleteProjectStaff(grpStaff);
                                                                        if (resultArgs.Success)
                                                                        {
                                                                            resultArgs = GroupStaff.SaveProjectStaff(ProjectId, grpStaff);
                                                                        }

                                                                    }
                                                                }
                                                            }*/
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //this.Height = 197;
                                                        // layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                                        //layoutControlGroup2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                                        if (string.IsNullOrEmpty(dr["Employee number"].ToString()))
                                                        {
                                                            if (!string.IsNullOrEmpty(dr["Firstname"].ToString()))
                                                            {

                                                                // message = message + dr["Firstname"].ToString() + " has some empty fields"+Environment.NewLine;
                                                                message = message + dr["Firstname"].ToString() + this.GetMessage(MessageCatalog.Payroll.GetTemplate.GET_TEMP_EXCEL_EMPTY_FIELD_INFO) + Environment.NewLine;
                                                                // meRemarks.Text += message + Environment.NewLine;
                                                            }
                                                            WriteLog(message);
                                                        }
                                                        else if (string.IsNullOrEmpty(dr["Firstname"].ToString()))
                                                        {
                                                            if (!string.IsNullOrEmpty(dr["Employee number"].ToString()))
                                                            {

                                                                //message = message + dr["Employee number"].ToString() + " has some empty fields"+Environment.NewLine;
                                                                message = message + dr["Employee number"].ToString() + this.GetMessage(MessageCatalog.Payroll.GetTemplate.GET_TEMP_EXCEL_EMPTY_FIELD_INFO) + Environment.NewLine;
                                                                //meRemarks.Text += message + Environment.NewLine;
                                                            }
                                                            WriteLog(message);
                                                        }
                                                        else
                                                        {
                                                            //message  =message+ dr["Employee number"].ToString() + "/" + dr["Firstname"].ToString() + " has some empty fields" + Environment.NewLine;
                                                            message = message + dr["Employee number"].ToString() + "/" + dr["Firstname"].ToString() + this.GetMessage(MessageCatalog.Payroll.GetTemplate.GET_TEMP_EXCEL_EMPTY_FIELD_INFO) + Environment.NewLine;
                                                            // meRemarks.Text += message + Environment.NewLine;
                                                            WriteLog(message);
                                                        }
                                                    }

                                                }
                                                if (UpdateHeld != null)
                                                {
                                                    UpdateHeld(this, e);
                                                }
                                            }
                                            if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                                            {
                                                if (!string.IsNullOrEmpty(message))
                                                {
                                                    this.Height = 197;
                                                    layoutControlGroup2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                                    meRemarks.Text = message;
                                                }
                                                CloseWaitDialog();
                                                //DialogResult dresult = XtraMessageBox.Show("Staff imported successfully", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
                                                DialogResult dresult = XtraMessageBox.Show(this.GetMessage(MessageCatalog.Payroll.GetTemplate.GET_TEMP_EXCEL_IMPORT_INFO), this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information); ;
                                                if (dresult == DialogResult.OK)
                                                {
                                                    btnImport.Enabled = false;
                                                }
                                            }
                                            else
                                            {
                                                this.Height = 99;
                                                //layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                                layoutControlGroup2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                                CloseWaitDialog();
                                                //DialogResult dresult = XtraMessageBox.Show("There is no valid record", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                DialogResult dresult = XtraMessageBox.Show(this.GetMessage(MessageCatalog.Payroll.GetTemplate.GET_TEMP_NOVALID_RECORD_INFO), this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                if (dresult == DialogResult.OK)
                                                {
                                                    this.Close();
                                                }
                                            }

                                        }
                                        else
                                        {
                                            this.Height = 99;
                                            // layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                            layoutControlGroup2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                            CloseWaitDialog();
                                            //DialogResult dresult = XtraMessageBox.Show("There is no valid record", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
                                            DialogResult dresult = XtraMessageBox.Show(this.GetMessage(MessageCatalog.Payroll.GetTemplate.GET_TEMP_NOVALID_RECORD_INFO), this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            if (dresult == DialogResult.OK)
                                            {
                                                this.Close();
                                            }
                                        }
                                    }
                                    else
                                    {
                                        this.Height = 197;
                                        //  layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                        layoutControlGroup2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                        CloseWaitDialog();
                                        if (!string.IsNullOrEmpty(ProjectList))
                                        {
                                            string[] Projects = ProjectList.Split(',');
                                            //meRemarks.Text = "The following projects are not available:";
                                            meRemarks.Text = this.GetMessage(MessageCatalog.Payroll.GetTemplate.GET_TEMP_PROJECT_AVAILABLE_INFO);
                                            foreach (string item in Projects)
                                            {
                                                meRemarks.Text += Environment.NewLine + item;
                                            }
                                        }
                                        //DialogResult dresult = XtraMessageBox.Show("Project " + ProjectList + " not exsits", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
                                    }
                                }
                            }
                            else
                            {
                                resultArgs = objclspaystaff.UpdateExcelgroups(dtresult);
                                if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
                                {
                                    foreach (DataRow dr in dt.Rows)
                                    {
                                        if (IsValidRow(dr))
                                        {
                                            pbcImport.Properties.Step = 1;
                                            pbcImport.PerformStep();
                                            objclspaystaff.StaffId = StaffId == 0 ? 0 : StaffId;
                                            // Staff.IncrementMonth = dr["PAYINCM1"].ToString();
                                            objclspaystaff.ScaleofPay = dr["Scale of pay"].ToString();
                                            
                                            string getgroupName = dr["Group"].ToString();
                                            DataTable dtGroupId = objclspaystaff.FetchGroupIdByGroupName(getgroupName).DataSource.Table;
                                            Int32 GroupId = commem.NumberSet.ToInteger(dtGroupId.Rows[0]["GROUPID"].ToString());

                                            resultArgs = objclspaystaff.savePayrollStaffData(GroupId, dr["Employee number"].ToString(),
                                                dr["Employee number"].ToString(),
                                                dr["Firstname"].ToString(),
                                                dr["middle_name"].ToString(),
                                                dr["father_husband_name"].ToString(),
                                                dr["mother_name"].ToString(),
                                                dr["no_of_children"].ToString(),
                                                dr["blood_group"].ToString(),
                                                dr["last_date_of_contract"].ToString(),
                                                dr["Lastname"].ToString(),
                                                dr["Gender"].ToString(),
                                                dr["Date of Birth"].ToString(),
                                                dr["Date of Join"].ToString(),
                                                dr["Retirement date"].ToString(),
                                                dr["Known as"].ToString(),
                                                dr["Leaving date"].ToString(),
                                                dr["Leaving reason"].ToString(),
                                                dr["Designation"].ToString(),
                                                dr["Qualification"].ToString(),
                                                string.Empty, 0, 0,
                                                dr["Earning1"].ToString(),
                                                dr["Earning2"].ToString(),
                                                dr["Earning3"].ToString(),
                                                dr["Deduction1"].ToString(),
                                                dr["Deduction2"].ToString(),
                                                dr["paying_salary_days"].ToString(),
                                                dr["UAN"].ToString(),
                                                StaffId == 0 ? clsPayrollConstants.PAYROLL_STAFF_INSERT : clsPayrollConstants.PAYROLL_STAFF_EDIT,
                                                0,
                                                dr["Scale of pay"].ToString(), dr["Account number"].ToString(), this.commem.NumberSet.ToDouble(dr["Year of service"].ToString()), dr["command_on_performance"].ToString(),
                                                dr["address"].ToString(), dr["mobile_no"].ToString(), dr["telephone_no"].ToString(), dr["emergency_contact_no"].ToString(), dr["email_id"].ToString(), dr["dependent1"].ToString(), dr["dependent2"].ToString(), dr["dependent3"].ToString(), dr["work_experience"].ToString(), dr["pan_no"].ToString(), dr["aadhar_no"].ToString(), string.Empty, string.Empty);

                                            if (resultArgs.Success)
                                            {
                                                string grpStaff = StaffId != 0 ? StaffId.ToString() : objclspaystaff.RtnStaffid;
                                                RetrunValue = commem.NumberSet.ToInteger(grpStaff);

                                                /*using (clsPrGroupStaff GroupStaff = new clsPrGroupStaff())
                                                {
                                                    resultArgs = GroupStaff.DeleteStaffGroup(grpStaff);
                                                    if (resultArgs.Success)
                                                    {
                                                        string strResult = GroupStaff.SaveNewStaffInGroup(GroupId, grpStaff);
                                                    }
                                                }*/
                                            }
                                        }
                                        else
                                        {
                                            //this.Height = 197;
                                            //  layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                            // layoutControlGroup2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                            if (string.IsNullOrEmpty(dr["Employee number"].ToString()))
                                            {
                                                if (!string.IsNullOrEmpty(dr["Firstname"].ToString()))
                                                {

                                                    //message = message + dr["Firstname"].ToString() + " has some empty fields"+Environment.NewLine;
                                                    message = message + dr["Firstname"].ToString() + this.GetMessage(MessageCatalog.Payroll.GetTemplate.GET_TEMP_EXCEL_EMPTY_FIELD_INFO) + Environment.NewLine;
                                                    // meRemarks.Text += message + Environment.NewLine;
                                                }
                                                WriteLog(message);
                                            }
                                            else if (string.IsNullOrEmpty(dr["Firstname"].ToString()))
                                            {
                                                if (!string.IsNullOrEmpty(dr["Employee number"].ToString()))
                                                {

                                                    //message = message + dr["Employee number"].ToString() + " has some empty fields"+Environment.NewLine;
                                                    message = message + dr["Employee number"].ToString() + this.GetMessage(MessageCatalog.Payroll.GetTemplate.GET_TEMP_EXCEL_EMPTY_FIELD_INFO) + Environment.NewLine;
                                                    // meRemarks.Text += message + Environment.NewLine;
                                                }
                                                WriteLog(message);
                                            }
                                            else
                                            {

                                                message = message + dr["Employee number"].ToString() + "/" + dr["Firstname"].ToString() + " has some empty fields" + Environment.NewLine;
                                                //  meRemarks.Text += message + Environment.NewLine;
                                                WriteLog(message);
                                            }
                                        }

                                    }
                                    if (UpdateHeld != null)
                                    {
                                        UpdateHeld(this, e);
                                    }
                                }
                                if (resultArgs != null && resultArgs.Success)
                                {
                                    if (!string.IsNullOrEmpty(message))
                                    {
                                        this.Height = 197;
                                        layoutControlGroup2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                        meRemarks.Text = message;
                                    }
                                    CloseWaitDialog();
                                    //DialogResult dresult = XtraMessageBox.Show("Staff imported successfully", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    DialogResult dresult = XtraMessageBox.Show(this.GetMessage(MessageCatalog.Payroll.GetTemplate.GET_TEMP_EXCEL_IMPORT_INFO), this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    if (dresult == DialogResult.OK)
                                    {
                                        btnImport.Enabled = false;
                                    }
                                }
                                else
                                {
                                    this.Height = 99;
                                    //layoutControlItem5.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                    layoutControlGroup2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                    CloseWaitDialog();
                                    //DialogResult dresult = XtraMessageBox.Show("There is no valid record", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    DialogResult dresult = XtraMessageBox.Show(this.GetMessage(MessageCatalog.Payroll.GetTemplate.GET_TEMP_NOVALID_RECORD_INFO), this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
                                    if (dresult == DialogResult.OK)
                                    {
                                        this.Close();
                                    }
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION);
            }
            finally { }
        }

        private void frmGetTemplate_Load(object sender, EventArgs e)
        {
            LoadDefaults();
        }
        #endregion
    }
}
