using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Payroll.Model.UIModel;
using Bosco.Utility.Common;
using System.Data.OleDb;
using Bosco.DAO.Data;
using Bosco.Utility;
using Bosco.Utility.ConfigSetting;

namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmStaffView : frmPayrollBase
    {
        #region Declaration
        private int RowIndex = 0;
        private int ReturnValue;
        private DataTable StaffDeails;
        private DataTable dtPayMonthStaffProfile = new DataTable();
        clsPayrollBase payrollbase = new clsPayrollBase();
        CommonMember commem = new CommonMember();
        ResultArgs resultArgs = null;
        #endregion

        #region Properties

        int sStaffId = 0;
        private int StaffId
        {
            get
            {
                RowIndex = gvStaff.FocusedRowHandle;
                sStaffId = gvStaff.GetFocusedRowCellValue(colStaffId) != null ? this.UtilityMember.NumberSet.ToInteger(gvStaff.GetFocusedRowCellValue(colStaffId).ToString()) : 0;
                return sStaffId;
            }
            set
            {
                sStaffId = value;
            }
            //set
            //{
            //    sStaffId = value;
            //}
            //get
            //{
            //    return sStaffId;
            //}
        }
        private string FileName { get; set; }
        #endregion

        #region Constractors

        public frmStaffView()
        {
            InitializeComponent();
        }

        #endregion

        #region Events

        private void ucToolBar1_AddClicked(object sender, EventArgs e)
        {
            if (clsGeneral.checkPayrollexists())
            {
                //rgGroup.SelectedIndex = 0;
                StaffId = 0;
                showForm(0);
            }
        }
        private void ucToolBar1_EditClicked(object sender, EventArgs e)
        {
            if (clsGeneral.checkPayrollexists())
            {
                StaffId = gvStaff.GetFocusedRowCellValue(colStaffId) != null ? Convert.ToInt32(gvStaff.GetFocusedRowCellValue(colStaffId)) : 0;
                if (StaffId == 0)
                    //XtraMessageBox.Show(MessageCatalog.Common.COMMON_NO_RECORD_SELECTED, MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_NO_RECORD_SELECTED));
                else
                    showForm(StaffId);
            }
        }
        private void ucToolBar1_DeleteClicked(object sender, EventArgs e)
        {
            using (clsPayrollStaff Staff = new clsPayrollStaff())
            {
                StaffId = gvStaff.GetFocusedRowCellValue(colStaffId) != null ? Convert.ToInt32(gvStaff.GetFocusedRowCellValue(colStaffId)) : 0;
                string staffname = gvStaff.GetFocusedRowCellValue(colStaffName) != null ? gvStaff.GetFocusedRowCellValue(colStaffName).ToString().Trim() : string.Empty;
                if (StaffId > 0)
                {
                    string msg = "Are you sure to delete '" + staffname + "' complete Profile and all Payroll processed information ? ";
                    DialogResult result = XtraMessageBox.Show(msg, this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE), MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                    if (result != DialogResult.Yes) return;

                    //ReturnValue = Staff.DeleteStaff(StaffId);
                    //ReturnValue = Staff.DeleteUnmappedStaff(StaffId);
                    ResultArgs resultarg = Staff.DeleteUnmappedStaff(StaffId);
                    if (resultarg != null && resultarg.Success)
                    {
                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Payroll.Staff.STAFF_DELETE_SUCCESS));
                        LoadStaffDetails();
                    }
                    else if (resultarg != null && !resultarg.Success)
                    {
                        this.ShowSuccessMessage(resultarg.Message);
                    }
                }
                else
                {
                    //this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_NO_RECORD_SELECTED), MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION);
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_NO_RECORD_SELECTED));

                }
            }
        }
        private void ucToolBar1_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ucToolBar1_RefreshClicked(object sender, EventArgs e)
        {
            rgGroup.SelectedIndex = 2;
            LoadStaffDetails();
        }
        private void frmStaffView_Load(object sender, EventArgs e)
        {
            if (SettingProperty.PayrollFinanceEnabled)
            {
                this.colProjectName.Visible = true;
            }
            else
            {
                this.colProjectName.Visible = false;
            }
            //this.colProjectName.Visible = SettingProperty.PayrollFinanceEnabled;
            rgGroup.SelectedIndex = 2;
            deDateAson.DateTime = DateTime.Now.Date;
            LoadStaffDetails();
            SetTitle();
            //ApplyUserRights();
            ucToolBar1.VisibleAddButton = ucToolBar1.VisibleEditButton = ucToolBar1.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.AttachGridContextMenu(gcStaff);
            this.AttachGridContextMenu(gcPaymonthStaffProfile);

            //On 06/12/2024, To hide few properties for other or multi currency enabled ----------------
            if (this.AppSetting.AllowMultiCurrency == 1 || this.AppSetting.IsCountryOtherThanIndia)
            {
                colStatutoryCompliance.Visible = false;
                colStatutoryComplaiance.Visible = false;
            }
            //------------------------------------------------------------------------------------------
        }
        private void gcStaff_DoubleClick(object sender, EventArgs e)
        {
            StaffId = gvStaff.GetFocusedRowCellValue(colStaffId) != null ? Convert.ToInt32(gvStaff.GetFocusedRowCellValue(colStaffId)) : 0;
            showForm(StaffId);
        }
        public void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadStaffDetails();
            gvStaff.FocusedRowHandle = RowIndex;
        }
        private void gvStaff_RowCountChanged(object sender, EventArgs e)
        {
            lblRowcount.Text = gvStaff.RowCount.ToString();

           
        }
        private void rgGroup_SelectedIndexChanged(object sender, EventArgs e)
        {
            ShowStaffDetails();
        }
        private void ShowStaffDetails()
        {
            using (clsPayrollStaff Staff = new clsPayrollStaff())
            {
                 dtPayMonthStaffProfile = Staff.getPaymonthStaffProfile();
                if ((int)rgGroup.EditValue == (int)StaffStatus.InService)
                {
                    StaffDeails = Staff.getInService(deDateAson.Text);
                    // StaffDeails = Staff.getPayrollStaffList();
                    LoadGrid(StaffDeails);
                }
                else if ((int)rgGroup.EditValue == (int)StaffStatus.Outof)
                {
                    StaffDeails = Staff.getOutofService(deDateAson.Text);
                    LoadGrid(StaffDeails);
                }
                else
                {
                    //LoadGrid(StaffDeails);
                    LoadStaffDetails();
                }
            }
        }
        private void ucToolBar_PrintClicked(object sender, EventArgs e)
        {

        }
        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvStaff.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvStaff, colStaffCode);
            }

        }
        private void ucToolBar1_PrintClicked(object sender, EventArgs e)
        {
            PrintGridViewDetails(gcStaff, GetMessage(MessageCatalog.Payroll.Staff.STAFF_PRINT_CAPTION), PrintType.DT, gvStaff, true);
        }
        private void frmStaffView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }
        private void gvStaff_RowCellStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowCellStyleEventArgs e)
        {

            if (!chkShowFilter.Checked)
            {
                //if (gvProcess.GetRowCellValue(e.RowHandle, colIdentification).ToString() == "G")
                //{

                //    e.Appearance.ForeColor = System.Drawing.Color.Green;
                //    e.Appearance.Font = new System.Drawing.Font("Tahoma", 8F, System.Drawing.FontStyle.Bold);
                //    e.Appearance.BackColor = Color.Gainsboro;
                //    // e.Appearance.BackColor = System.Drawing.Color.MidnightBlue;

                //}
            }

        }
        private void frmStaffView_EnterClicked(object sender, EventArgs e)
        {
            //EditStaffDetails();

            StaffId = gvStaff.GetFocusedRowCellValue(colStaffId) != null ? Convert.ToInt32(gvStaff.GetFocusedRowCellValue(colStaffId)) : 0;
            if (StaffId == 0)
                //XtraMessageBox.Show(MessageCatalog.Common.COMMON_NO_RECORD_SELECTED, MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_NO_RECORD_SELECTED));
            else
                showForm(StaffId);
        }
        private void deDateAson_EditValueChanged(object sender, EventArgs e)
        {
            ShowStaffDetails();
        }
        private void ucToolBar1_ImportClicked(object sender, EventArgs e)
        {
            rgGroup.SelectedIndex = 2;
            if (clsGeneral.checkPayrollexists())
            {
                frmGetTemplate exceltemplate = new frmGetTemplate();
                exceltemplate.UpdateHeld += new EventHandler(OnUpdateHeld);
                exceltemplate.Show();
            }
        }

        private void gvStaff_FocusedRowChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedRowChangedEventArgs e)
        {
            gcPaymonthStaffProfile.DataSource = null;
            lcGrpPayMonthStaffProfile.Text = "Staff Details Paymonth-wise";
            if (dtPayMonthStaffProfile != null && gcStaff.DataSource != null)
            {
                int stfid = gvStaff.GetFocusedRowCellValue(colStaffId) != null ? this.UtilityMember.NumberSet.ToInteger(gvStaff.GetFocusedRowCellValue(colStaffId).ToString()) : 0;
                string stfcode = gvStaff.GetFocusedRowCellValue(colStaffCode) != null ? gvStaff.GetFocusedRowCellValue(colStaffCode).ToString() : string.Empty;
                string stfname = gvStaff.GetFocusedRowCellValue(colStaffName) != null ? gvStaff.GetFocusedRowCellValue(colStaffName).ToString() : string.Empty;

                lcGrpPayMonthStaffProfile.Text += " (Code : " + stfcode + "   Name : " + stfname + ")";
                dtPayMonthStaffProfile.DefaultView.RowFilter = string.Empty;
                dtPayMonthStaffProfile.DefaultView.RowFilter = "STAFFID = " + stfid;
                dtPayMonthStaffProfile.DefaultView.Sort = "PRDATE DESC";
                gcPaymonthStaffProfile.DataSource = dtPayMonthStaffProfile.DefaultView.ToTable(); ;
                dtPayMonthStaffProfile.DefaultView.RowFilter = string.Empty;
            }
        }

        //private void btnImport_Click(object sender, EventArgs e)
        //{
        //try
        //{
        //    int projectExists;
        //    string projectName = string.Empty;
        //    string ProjectList = string.Empty;
        //    ofdImportExcel.Filter = "Excel Files (.xlsx)|*.xlsx|(.xls)|*.xls";
        //    using (clsPayrollStaff objclspaystaff = new clsPayrollStaff())
        //    {
        //        if (ofdImportExcel.ShowDialog() == DialogResult.OK)
        //        {
        //            FileName = ofdImportExcel.FileName;
        //            txtFileName.Text = FileName;
        //            if (!string.IsNullOrEmpty(ofdImportExcel.FileName))
        //            {
        //                DataTable dt = new DataTable();
        //                dt = conExcel("SELECT * FROM [stfpersonal$]", "stfpersonal", FileName);
        //                if (dt != null && dt.Rows.Count > 0)
        //                {
        //                    //Filtering the groups
        //                    DataTable dtresult = dt.AsEnumerable()
        //                                .GroupBy(r => r.Field<string>("Group")).Select(g => g.First()).CopyToDataTable();
        //                    DataView dvGroups = dtresult.AsDataView();
        //                    dtresult = dvGroups.ToTable("Selected", false, "Group");

        //                    //Filtering the projects
        //                    DataTable dtproject = dt.AsEnumerable()
        //                                 .GroupBy(r => r.Field<string>("Project")).Select(g => g.First()).CopyToDataTable();
        //                    DataView dvProject = dtproject.AsDataView();
        //                    dtproject = dvProject.ToTable("Selected", false, "Project");


        //                    if (dtproject != null && dtproject.Rows.Count > 0)
        //                    {
        //                        foreach (DataRow drproject in dtproject.Rows)
        //                        {
        //                            if (drproject != null && !string.IsNullOrEmpty(drproject["Project"].ToString()))
        //                            {
        //                                projectName = drproject["Project"].ToString();
        //                                projectExists = objclspaystaff.CheckProjectExists(projectName);
        //                                if (projectExists == 0)
        //                                {
        //                                    ProjectList += projectName + ",";
        //                                }
        //                            }
        //                        }
        //                        ProjectList = ProjectList.TrimEnd(',');
        //                        if (string.IsNullOrEmpty(ProjectList))
        //                        {
        //                            //XtraMessageBox.Show("Project " + ProjectName + " is not exists", MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
        //                            resultArgs = objclspaystaff.UpdateExcelgroups(dtresult);
        //                            if (resultArgs.Success && resultArgs.RowsAffected > 0)
        //                            {
        //                                foreach (DataRow dr in dt.Rows)
        //                                {
        //                                    objclspaystaff.CommonId = StaffId == 0 ? 0 : StaffId;
        //                                    // Staff.IncrementMonth = dr["PAYINCM1"].ToString();
        //                                    objclspaystaff.ScaleofPay = dr["Scale of pay"].ToString();
        //                                    resultArgs = objclspaystaff.savePayrollStaffData(dr["Employee number"].ToString(),
        //                                        dr["Employee number"].ToString(),
        //                                        dr["Firstname"].ToString(),
        //                                        dr["Lastname"].ToString(),
        //                                        dr["Gender"].ToString(),
        //                                        dr["Date of Birth"].ToString(),
        //                                        dr["Date of Join"].ToString(),
        //                                        dr["Retirement date"].ToString(),
        //                                        dr["Known as"].ToString(),
        //                                        dr["Leaving date"].ToString(),
        //                                        dr["Leaving reason"].ToString(),
        //                                        dr["Designation"].ToString(),
        //                                        dr["Qualification"].ToString(),
        //                                        string.Empty, 0, 0,
        //                                        dr["PF number"].ToString(),
        //                                        StaffId == 0 ? clsPayrollConstants.PAYROLL_STAFF_INSERT : clsPayrollConstants.PAYROLL_STAFF_EDIT,
        //                                        0,
        //                                        dr["Scale of pay"].ToString(), dr["Account number"].ToString(), this.commem.NumberSet.ToDouble(dr["Year of service"].ToString()));
        //                                }
        //                            }
        //                        }
        //                        else
        //                        {
        //                            XtraMessageBox.Show("Project "+ProjectList+" not exsits", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information); ;
        //                        }
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}
        //catch (Exception ex)
        //{
        //    XtraMessageBox.Show(ex.ToString(), MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION);
        //}
        //finally { }

        //}
        #endregion

        #region Methods

        private void SetTitle()
        {
            this.Text = this.GetMessage(MessageCatalog.Payroll.Staff.STAFF_VIEW_CAPTION);
            lblPayMonth.Text = "Paymonth - " + clsGeneral.PAYROLL_MONTH;
        }

        private void showForm(int StaffID)
        {

            frmStaffDetails staff = new frmStaffDetails(StaffID);
            staff.UpdateHeld += new EventHandler(OnUpdateHeld);
            staff.ShowDialog();
        }

        public void LoadStaffDetails()
        {
            try
            {
                using (clsPayrollStaff Staff = new clsPayrollStaff())
                {
                    StaffDeails = Staff.getPayrollStaffList();
                    LoadGrid(StaffDeails);
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }
        }

        private void LoadGrid(DataTable dtStaff)
        {
            try
            {
                gcStaff.DataSource = null;
                if (dtStaff != null && dtStaff.Rows.Count > 0)
                {
                    using (clsPayrollStaff Staff = new clsPayrollStaff())
                    {
                        dtPayMonthStaffProfile = Staff.getPaymonthStaffProfile();

                        dtStaff.DefaultView.Sort = "GROUP, STAFFORDER";
                        dtStaff = dtStaff.DefaultView.ToTable();
                        gcStaff.DataSource = dtStaff;
                    }
                }
                else
                    gcStaff.DataSource = null;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION);
            }
        }

        protected virtual void SetFocusRowFilter(DevExpress.XtraGrid.Views.Grid.GridView gridview, DevExpress.XtraGrid.Columns.GridColumn colGridColumn)
        {
            gridview.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
            gridview.FocusedColumn = colGridColumn;
            gridview.OptionsFind.AllowFindPanel = false;
            gridview.ShowEditor();
        }

        private void ApplyUserRights()
        {
            this.enumUserRights.Add(Staff.CreateStaff);
            this.enumUserRights.Add(Staff.EditStaff);
            this.enumUserRights.Add(Staff.DeleteStaff);
            this.enumUserRights.Add(Staff.ViewStaff);
            this.ApplyUserRights(ucToolBar1, enumUserRights, (int)Menus.Staff);
        }

        #endregion

        private void rbiPaymonthStaffProfile_Click(object sender, EventArgs e)
        {
            frmPaymonthStaffProfile frmpaymonthstaffprofilew = new frmPaymonthStaffProfile(StaffId);
            frmpaymonthstaffprofilew.UpdateHeld += new EventHandler(OnUpdateHeld);
            frmpaymonthstaffprofilew.ShowDialog();
        }

      

    }
}