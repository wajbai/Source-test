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
using Bosco.Utility;
using Bosco.Utility.Common;
using Bosco.Utility.ConfigSetting;
using DevExpress.XtraEditors.Popup;
using DevExpress.XtraEditors.Popups;
using Bosco.Model.UIModel;
using Bosco.Model.Transaction;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.Xpf.Grid;
using System.Threading;
using System.Threading.Tasks;

namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmPayrollBrowseView : frmPayrollBase
    {
        #region VariableDeclaration
        ResultArgs resultArgs = new ResultArgs();
        frmBulkUpdation bulkUpdateion = new frmBulkUpdation();
        private clsPayrollProcess objProcess = new clsPayrollProcess();
        private clsPrGateWay objPRGW = new clsPrGateWay();
        private clsModPay objModPay = new clsModPay();
        private clsPayrollStaff objClsStaff = new clsPayrollStaff();
        private clsPayrollLoan objClsLoan = new clsPayrollLoan();
        private clsPayrollComponent objClsComponent = new clsPayrollComponent();
        private clsLoanManagement objClsLoanMnt = new clsLoanManagement();
        private clsPayrollGrade objClsGrade = new clsPayrollGrade();
        private clsPrComponent objPRComp = new clsPrComponent();
        private clsprCompBuild objPRBuild = new clsprCompBuild();
        private clsPrLoan objPrLoan = new clsPrLoan();
        CommonMember commem = new CommonMember();

        private DataTable dset;
        private DataTable dtStatus;
        private DataTable dtProcess = new DataTable();
        private DataTable dt = new DataTable();
        private DataView dv = new DataView();
        private DataView dvCopyOfPayroll = new DataView();
        private DataTable dtpreviousvalues = new DataTable();


        public string subModule = "";
        private string sGroupId = "";
        //private string sStaffIds = "";
        private string strShow = "";
        private string strDepartment = "";
        private string sValueBeforeModification = "";
        private string sPrevCellValue = "";
        private string sLinkName = "";

        private bool bExit = false;
        private bool bLocked = false;
        private bool bRebuildNode = false;
        private bool bPayRoll = false;
        private bool flag = true;
        private List<string> ModifiedStaffList = new List<string>();
        #endregion

        #region Process Variable Declaration
        //CommonMember UtilityMember = new CommonMember();
        //ResultArgs resultArgs = new ResultArgs();
        // private clsPayrollComponent objPayrollComp = new clsPayrollComponent();
        //private clsPayrollProcess objPayrollProcess = new clsPayrollProcess();
        // private clsPayrollStaff staff = new clsPayrollStaff();
        // private clsPrComponent objPrComponent = new clsPrComponent();
        TransProperty Transaction = new TransProperty();
        UserProperty LoginUser = new UserProperty();
        public static DataTable dtComponents = new DataTable();
        #endregion

        #region Constructor
        public frmPayrollBrowseView()
        {
            InitializeComponent();
        }
        #endregion

        #region Process Payroll Properties
        private int payrollcomponentid = 0;
        private int PayrollComponentId
        {
            get
            {
                return 0;  // 0 Indicates Process Payroll for all the groups.
            }
            set
            {
                payrollcomponentid = value;
            }
        }
        private int processledgerid = 0;
        public int ProcessLedgerId
        {
            get
            {
                return processledgerid;
            }
            set
            {
                processledgerid = value;
            }
        }
        private string mappedstaffid = string.Empty;
        public string MappedStaffId
        {
            get
            {
                return GetmappedStaffs();
            }
        }

        private string paygroupid = string.Empty;
        public string PayGroupId
        {
            get
            {
                return GetPayrollGroupsIds();
            }
        }
        public DialogResult drs { get; set; }
        #endregion

        #region Events
        /// <summary>
        /// Load the Data
        /// </summary>
        private void frmPayrollBrowseView_Load(object sender, EventArgs e)
        {
            this.Text = "Process/View Payroll";
            this.Cursor = Cursors.WaitCursor;
            lcProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lblTotalGrossWagesCaption.Visibility = lblTotalGrossWagesValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lblTotalDeductionCaption.Visibility = lblTotalDeductionValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            lblTotalNETPayCaption.Visibility = lblTotalNETPayValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            EnableControls();
            if (SettingProperty.PayrollFinanceEnabled == true)
            {
                LoadProjects();
            }
            LoadGroupList();
            AssignProcessDate();
            LoadComponentDetails();
            if (SettingProperty.PayrollFinanceEnabled == true)
            {
                if (glkpProject.Properties.DataSource != null || lkpGroupData.Properties.DataSource != null)
                {
                    if (glkpProject.EditValue != null || commem.NumberSet.ToInteger(glkpProject.EditValue.ToString()) != 0
                        && !string.IsNullOrEmpty(lkpGroupData.Text.Trim()))
                    {
                        FillPayrollInfo();
                    }
                }
            }
            else
            {
                if (!string.IsNullOrEmpty(lkpGroupData.Text.Trim()))
                {
                    FillPayrollInfo();
                }
            }
            this.AttachGridContextMenu(gcPayrollBrowseView);

            
            this.Cursor = Cursors.Default;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnBulkUpdataion_Click(object sender, EventArgs e)
        {
            // frmBulkUpdation bulkUpdation = new frmBulkUpdation();
            bulkUpdateion.prgPRProcess = progressBar1;
            bulkUpdateion.UpdateHeld += new EventHandler(OnUpdateHeld);
            bulkUpdateion.ShowDialog();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Edit the Value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPayrollBrowseView_CellValueChanged(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            //if (lblMessage.Visible == true)\
            //   return;

            this.Cursor = Cursors.WaitCursor;
            DateTime dtStartTime = DateTime.Now;
            lblTime.Text = string.Empty;

            dv = (DataView)gcPayrollBrowseView.DataSource;
            object ColumnName = e.Column.FieldName;  //e.Column; On 15/05/2019, to get actual component name
            object ColumnValue = e.Value;
            object StaffId = gvPayrollBrowseView.GetFocusedRowCellValue("StaffId");
            try
            {
                DataView dvpreviousvalues = new DataView(dtpreviousvalues);
                dvpreviousvalues.RowFilter = "StaffId =" + StaffId;
                sValueBeforeModification = dvpreviousvalues.ToTable().Rows[0][ColumnName.ToString()].ToString();
                DataTable dtBefMod = dvCopyOfPayroll.ToTable();
                //sValueBeforeModification = dtBefMod.Rows[0][ColumnName.ToString()].ToString();
                if (!string.IsNullOrEmpty(ColumnValue.ToString()))
                {
                    if (!objPRBuild.ModifyStaffComponent(clsGeneral.PAYROLL_ID, sGroupId, ColumnName.ToString(),
                        StaffId.ToString(),
                        ColumnValue.ToString(),
                        progressBar1, true))
                    {
                        //XtraMessageBox.Show("Invalid component for this staff.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.PayrollBrowseView.BROWSE_INVALID_COMPONENT_INFO));
                        gvPayrollBrowseView.SetFocusedRowCellValue(e.Column, sValueBeforeModification);
                    }
                    else
                    {
                        //FillPayrollInfo(); //On 30/05/2019, to avoid process for every modification of component value
                        ValidateAndEnableProcess(false, StaffId.ToString());
                    }
                }
                else { dv.Table.Rows[gvPayrollBrowseView.FocusedRowHandle][ColumnName.ToString()] = sValueBeforeModification; }
                //gvPayrollBrowseView.FocusedRowHandle = e.RowHandle;
                gvPayrollBrowseView.FocusedRowHandle = GetRowHandleByColumnValue(gvPayrollBrowseView, "StaffId", StaffId);
                gvPayrollBrowseView.FocusedColumn = gvPayrollBrowseView.Columns[e.Column.ColumnHandle];

                gvPayrollBrowseView.CloseEditor();
                this.Cursor = Cursors.Default;
                //return;

            }
            catch (Exception ex)
            {
                try
                {
                    dv.Table.Rows[gvPayrollBrowseView.FocusedRowHandle][ColumnName.ToString()] = sValueBeforeModification;
                    this.Cursor = Cursors.Default;
                    MessageRender.ShowMessage("Unable to Process, " + ex.Message);
                    //return;
                }
                catch
                {
                    this.Cursor = Cursors.Default;
                    //return;
                }
            }

            DateTime dtEndTime = DateTime.Now;
            double elapsedMillisecs = ((TimeSpan)(dtEndTime - dtStartTime)).TotalSeconds;
            //lblTime.Text = "Processed Time : " +  Math.Round( UtilityMember.NumberSet.ToDouble( elapsedMillisecs.ToString()), 2) + " sec ";
        }

        /// <summary>
        /// Change the Group
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void lkpGroupData_EditValueChanged(object sender, EventArgs e)
        {
            //if (SettingProperty.PayrollFinanceEnabled == false)
            //{
            //    lblGroup.Text = lkpGroupData.Text.ToString();
            //    this.Cursor = Cursors.WaitCursor;
            //    if (lkpGroupData.EditValue != null)
            //    {
            //        bulkUpdateion.sGroupIdData = sGroupId = lkpGroupData.EditValue.ToString();
            //    }
            //    //lcirdoGroup.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //    FillPayrollInfo();
            //}
            //this.Cursor = Cursors.Default;
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvPayrollBrowseView.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                if (gvPayrollBrowseView.RowCount > 0)
                {
                    //this.SetFocusRowFilter(gvPayrollBrowseView, gvPayrollBrowseView.Columns[1]);
                }
            }
        }

        private void gvPayrollBrowseView_RowCountChanged(object sender, EventArgs e)
        {
            lblRowCount.Text = gvPayrollBrowseView.RowCount.ToString();
        }

        private void rgBrowse_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (rgBrowse.SelectedIndex == 0)
            {
                // flyoutEditable.HidePopup();
                //flyoutNonEditable.HidePopup();
                // FillPayrollInfo();
                // lkpGroupData_EditValueChanged(sender, e);
            }
            else if (rgBrowse.SelectedIndex == 1)
            {
                //flyoutNonEditable.HidePopup();
                //flyoutEditable.ShowPopup();
            }
            else if (rgBrowse.SelectedIndex == 2)
            {
                // flyoutEditable.HidePopup();
                //flyoutNonEditable.ShowPopup();
            }
        }

        private void chkEditable_Leave(object sender, EventArgs e)
        {
            DataView dvEditableValues = (DataView)gcPayrollBrowseView.DataSource;
            chkEditable.Visible = false;

            for (int j = 0; j < chkEditable.CheckedItemsCount; j++)
            {
                for (int i = 0; i < dvEditableValues.Table.Columns.Count; i++)
                {
                    if (chkEditable.CheckedItems[j].ToString().CompareTo(dvEditableValues.Table.Columns[i].ColumnName.ToString()) == 0)
                    {
                        DataTable dtTransData = new DataTable();
                        dtTransData.Columns.Add(chkEditable.CheckedItems[j].ToString(), typeof(string));

                    }
                    //  dgPayrollProcess.TableStyles[0].GridColumnStyles[i].Width = 100;
                }
            }
            for (int j = 0; j < chkNonEditable.CheckedItems.Count; j++)
            {
                for (int i = 0; i < dvEditableValues.Table.Columns.Count; i++)
                {
                    if (chkNonEditable.CheckedItems[j].ToString().CompareTo(dvEditableValues.Table.Columns[i].ColumnName.ToString()) == 0)
                    {

                    }
                    //  dgPayrollProcess.TableStyles[0].GridColumnStyles[i].Width = 100;
                }
            }
        }

        private void chkNonEditable_Leave(object sender, EventArgs e)
        {
            chkEditable_Leave(sender, e);
        }

        protected void OnUpdateHeld(object sender, EventArgs e)
        {
            FillPayrollInfo();
        }
        private void frmPayrollBrowseView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }


        private void btnView_Click(object sender, EventArgs e)
        {
            if (ValidateBrowseViewDetails())
            {
                this.Cursor = Cursors.WaitCursor;
                DateTime dtStartTime = DateTime.Now;
                lblTime.Text = string.Empty;
                lblTotalGrossWagesCaption.Visibility = lblTotalGrossWagesValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lblTotalDeductionCaption.Visibility = lblTotalDeductionValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lblTotalNETPayCaption.Visibility = lblTotalNETPayValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                FillPayrollInfo();
                DateTime dtEndTime = DateTime.Now;
                double elapsedMillisecs = ((TimeSpan)(dtEndTime - dtStartTime)).TotalSeconds;
                //lblTime.Text = "Processed Time : " + Math.Round(UtilityMember.NumberSet.ToDouble(elapsedMillisecs.ToString()), 2) + " sec ";
                this.Cursor = Cursors.Default;
            }
        }

        private void glkpProject_EditValueChanged(object sender, EventArgs e)
        {
            // LoadProjects();
            // FillPayrollInfo();
        }

        private void glkpProject_Enter(object sender, EventArgs e)
        {
            //LoadProjects();
        }
        #endregion

        #region Methods

        /// <summary>
        /// 
        /// </summary>
        private void LoadPayroll()
        {
            using (clsPrComponent Component = new clsPrComponent())
            {
                // Component.GetPayRoll(clsGeneral.PAYROLL_ID.ToString(), long.Parse(lkpSalaryGroup.SelectedValue.ToString()),"" , false);
            }
        }

        private void LoadProjects()
        {
            try
            {
                using (PayrollSystem Paysystem = new PayrollSystem())
                {
                    resultArgs = Paysystem.FetchPayrollProjects();
                    glkpProject.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        commem.ComboSet.BindGridLookUpCombo(glkpProject, resultArgs.DataSource.Table, Paysystem.AppSchema.PRPayrollProject.PROJECTColumn.ColumnName, Paysystem.AppSchema.PRPayrollProject.PROJECT_IDColumn.ColumnName);
                        glkpProject.EditValue = glkpProject.Properties.GetKeyValue(0);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        /// <summary>
        /// 
        /// </summary>
        private void LoadGroupList()
        {
            try
            {
                DataTable dtGradeList;
                using (clsPayrollGrade Grade = new clsPayrollGrade())
                {
                    dtGradeList = Grade.getPayrollGradeList();
                    if (dtGradeList != null && dtGradeList.Rows.Count > 0)
                    {
                        using (CommonMethod SelectAll = new CommonMethod())
                        {
                            DataTable dtPayrollGroupsofProject = dtGradeList; // SelectAll.AddHeaderColumn(dtGradeList, "GROUP ID", "Group Name");
                            dtPayrollGroupsofProject.Columns["GROUP ID"].ColumnMapping = MappingType.Hidden;
                            lkpGroupData.Properties.DataSource = dtPayrollGroupsofProject;
                            lkpGroupData.Properties.ValueMember = "GROUP ID";
                            lkpGroupData.Properties.DisplayMember = "Group Name";

                            // On 11/01/2017, to remove all option in list of group and select first group item 
                            //lkpGroupData.EditValue = lkpGroupData.EditValue = lkpGroupData.Properties.GetKeyValueByDisplayText("<--All-->");
                            if (dtGradeList.Rows.Count > 0)
                            {
                                lkpGroupData.EditValue = lkpGroupData.EditValue = lkpGroupData.Properties.GetKeyValueByDisplayText(dtGradeList.Rows[0]["Group Name"].ToString());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }
        }

        protected virtual void SetFocusRowFilter(DevExpress.XtraGrid.Views.Grid.GridView gridview, DevExpress.XtraGrid.Columns.GridColumn colGridColumn)
        {
            gridview.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
            gridview.FocusedColumn = colGridColumn;
            gridview.OptionsFind.AllowFindPanel = false;
            gridview.ShowEditor();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sCompName"></param>
        /// <returns></returns>
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
                    if (Link.Substring(0, 6).ToUpper() == "LOAN :")
                        return true; //true modified for the problem of vb and with the new code
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
                return true; //true modified for the problem of vb and with the new code

            }
            return false;
        }


        public bool ValidateBrowseViewDetails()
        {
            bool isMapComp = true;
            if (SettingProperty.PayrollFinanceEnabled)
            {
                if (glkpProject.EditValue == null || commem.NumberSet.ToInteger(glkpProject.EditValue.ToString()) == 0)
                {
                    //XtraMessageBox.Show(MessageCatalog.Payroll.Staff.STAFF_PROJECT_EMPTY, MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                    //XtraMessageBox.Show("Project is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.PayrollBrowseView.BROWSE_PROJECT_EMPTY));
                    glkpProject.Focus();
                    //SetBorderColor(glkpProject);
                    return false;
                }
            }
            if (string.IsNullOrEmpty(lkpGroupData.Text.Trim()))
            {
                //ShowMessageBox("Staff Name is empty");
                //XtraMessageBox.Show("Group is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.PayrollBrowseView.BROWSE_GROUP_EMPTY));
                //this.SetBorderColor(lkpGroupData);
                lkpGroupData.Focus();
                return false;
            }
            else if (ModifiedStaffList.Count > 0)
            {
                this.ShowMessageBox("Few Staff Component(s) values are modified, Process again");
                lkpGroupData.Focus();
                return false;
            }
            else
            {
                glkpProject.Focus();
            }
            return isMapComp;
        }

        /// <summary>
        /// 
        /// </summary>
        private void FillPayrollInfo()
        {
            gcPayrollBrowseView.DataSource = null;
            //if (chkEditable.Items.Count > 0)
            //    chkEditable.Items.Clear();
            //if (chkNonEditable.Items.Count > 0)
            //    chkNonEditable.Items.Clear();
            //if (chkNonEditable.Items.Count > 0)
            //    chkNonEditable.Items.Clear();
            //if (bulkUpdateion.ComboboxData.Properties.Items.Count > 0)
            //    bulkUpdateion.ComboboxData.Properties.Items.Clear();
            //objPRComp.GetAcMeERPPayRoll(clsGeneral.PAYROLL_ID.ToString(), lkpGroupData.EditValue != null ? member.NumberSet.ToInteger(lkpGroupData.EditValue.ToString()) : 0, gcPayrollBrowseView, false);
            var EditableColumns = new List<string>();
            if (SettingProperty.PayrollFinanceEnabled)
            {
                if (glkpProject.EditValue != null)
                {
                    int ProjectId = commem.NumberSet.ToInteger(glkpProject.EditValue.ToString());
                    EditableColumns = objPRComp.GetAcMeERPPayRoll(clsGeneral.PAYROLL_ID.ToString(), ProjectId, lkpGroupData.EditValue != null ? commem.NumberSet.ToInteger(lkpGroupData.EditValue.ToString()) : 0, gcPayrollBrowseView, false);
                }
            }
            else
            {
                EditableColumns = objPRComp.GetAcMeERPPayRoll(clsGeneral.PAYROLL_ID.ToString(), 0, lkpGroupData.EditValue != null ? commem.NumberSet.ToInteger(lkpGroupData.EditValue.ToString()) : 0, gcPayrollBrowseView, false);
            }

            if (gcPayrollBrowseView.DataSource != null)
            {
                DataTable dt = (DataTable)gcPayrollBrowseView.DataSource;
                dtpreviousvalues = dt.Copy();
                dvCopyOfPayroll = dt.DefaultView;
                dv = dt.DefaultView;
                dv.AllowNew = false;
                dv.AllowDelete = false;

                gvPayrollBrowseView.Columns.Clear();
                //gcPayrollBrowseView.DataSource = dv;
                gcPayrollBrowseView.DataSource = null;
                //On 21/12/2022
                //dt.DefaultView.Sort = "NAME";
                gcPayrollBrowseView.DataSource = dt.DefaultView;

                gvPayrollBrowseView.OptionsView.ShowFooter = true;
                DataTable dtCompList = new DataTable();
                using (clsPayrollComponent cmp = new clsPayrollComponent())
                {
                    dtCompList = cmp.getPayrollComponentList();
                }

                for (int i = 0; i < gvPayrollBrowseView.Columns.Count; i++)
                {
                    string fldname = gvPayrollBrowseView.Columns[i].FieldName;
                    gvPayrollBrowseView.Columns[i].OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;

                    ///On 11/02/2022, dont show columns which are not applicable in view
                    dtCompList.DefaultView.RowFilter = string.Empty;
                    dtCompList.DefaultView.RowFilter = "Component='" + fldname + "' AND DONT_SHOWINBROWSE=1";
                    if (dtCompList.DefaultView.Count > 0)
                    {
                        gvPayrollBrowseView.Columns[i].Visible = false;
                    }

                    //For Footer Summary
                    dtCompList.DefaultView.RowFilter = string.Empty;
                    dtCompList.DefaultView.RowFilter = "Component='" + fldname + "' AND PROCESS_COMPONENT_TYPE>0";
                    if (dtCompList.DefaultView.Count > 0)
                    {
                        int processcomponenttype = UtilityMember.NumberSet.ToInteger(dtCompList.DefaultView[0]["PROCESS_COMPONENT_TYPE"].ToString());
                        //if (gvPayrollBrowseView.Columns[i].FieldName.ToUpper() == "NETPAY")
                        {
                            gvPayrollBrowseView.Columns[i].Summary.Add(DevExpress.Data.SummaryItemType.Sum, gvPayrollBrowseView.Columns[i].FieldName, "{0:n}");
                            gvPayrollBrowseView.Columns[i].AllowSummaryMenu = false;

                            if (processcomponenttype == (int)PayRollProcessComponent.GrossWages)
                            {
                                lblTotalGrossWagesCaption.Visibility = lblTotalGrossWagesValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                lblTotalGrossWagesValue.Text = gvPayrollBrowseView.Columns[i].SummaryText;
                            }
                            else if (processcomponenttype == (int)PayRollProcessComponent.Deductions)
                            {
                                lblTotalDeductionCaption.Visibility = lblTotalDeductionValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                lblTotalDeductionValue.Text = gvPayrollBrowseView.Columns[i].SummaryText;
                            }
                            else if (processcomponenttype == (int)PayRollProcessComponent.NetPay)
                            {
                                lblTotalNETPayCaption.Visibility = lblTotalNETPayValue.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                lblTotalNETPayValue.Text = gvPayrollBrowseView.Columns[i].SummaryText;
                            }
                        }
                    }

                    dtCompList.DefaultView.RowFilter = string.Empty;
                }
                int nCol = dv.Table.Columns.Count - 1;
            }
            else
            {
                // XtraMessageBox.Show("No record is available in the grid.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }


            //On 17/05/2019, to lock other component columns to allow edit ------------------------------------
            foreach (DevExpress.XtraGrid.Columns.GridColumn gc in gvPayrollBrowseView.Columns)
            {
                //On 15/02/2024
                //int index = EditableColumns.FindIndex(x => x.StartsWith(gc.FieldName));
                int index = EditableColumns.FindIndex(x => x.Equals(gc.FieldName));
                if (index >= 0) //make editable column
                {
                    gvPayrollBrowseView.Columns[gc.FieldName].OptionsColumn.AllowEdit = true;
                    gvPayrollBrowseView.Columns[gc.FieldName].OptionsColumn.AllowFocus = true;
                }
                else
                {
                    gvPayrollBrowseView.Columns[gc.FieldName].OptionsColumn.AllowEdit = false;
                    gvPayrollBrowseView.Columns[gc.FieldName].OptionsColumn.AllowFocus = false;
                    gvPayrollBrowseView.Columns[gc.FieldName].AppearanceCell.BackColor = Color.LightGray;
                }
            }
            //-------------------------------------------------------------------------------------------------

            if (gvPayrollBrowseView.RowCount > 0)
            {
                gvPayrollBrowseView.Columns["StaffId"].Visible = false;
            }
        }

        /// <summary>
        /// To Bind Data to ComboBox
        /// </summary>
        /// <param name="sCompName"></param>
        /// <returns></returns>
        private bool IsEditablecombo(string sCompName)
        {
            clsModPay objModPay = new clsModPay();
            string Loan = "";
            string type = objModPay.GetValueTocombo("PRComponent", "type", "component = '" + sCompName + "'");
            string Link = objModPay.GetValueTocombo("PRComponent", "LINKVALUE", "component = '" + sCompName + "'");
            string EQ = objModPay.GetValueTocombo("PRComponent", "EQUATION", "component = '" + sCompName + "'");

            if (Link.Trim() != "")
            {
                if (Link.Length >= 6)
                {
                    if (Link.Substring(0, 6).ToUpper() == "LOAN :")
                        return true; //true modified for the problem of vb and with the new code
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
                return true; //true modified for the problem of vb and with the new code

            }
            return false;
        }
        private void EnableControls()
        {
            if (SettingProperty.PayrollFinanceEnabled == true)
            {
                layoutControlItem10.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            }
            else
            {
                layoutControlItem10.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            }
        }
        private int GetRowHandleByColumnValue(GridView view, string ColumnFieldName, object value)
        {
            int result = 0;
            for (int i = 0; i < view.RowCount; i++)
                if (view.GetDataRow(i)[ColumnFieldName].Equals(value))
                    return i;
            return result;
        }
        #endregion

        #region Process Methods
        /// <summary>
        /// Load Component details to order components
        /// </summary>
        public void LoadComponentDetails()
        {
            dtComponents = objClsComponent.getComponentDetails(PayrollComponentId);
        }

        /// <summary>
        /// Update Payroll Process date
        /// </summary>
        private void InsertProcessDate()
        {
            try
            {
                using (clsprCompBuild objCompBuild = new clsprCompBuild())
                {
                    resultArgs = objCompBuild.UpdateProcessDate(deProcessDate.DateTime);
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        /// <summary>
        /// Assign Process date while edit mode
        /// </summary>
        public void AssignProcessDate()
        {
            try
            {
                using (clsprCompBuild objCompBuild = new clsprCompBuild())
                {
                    resultArgs = objCompBuild.GetPayrollPeriodMonth();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        deProcessDate.Properties.MinValue = UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0]["FROMDATE"].ToString(), false);
                        deProcessDate.Properties.MaxValue = UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0]["TODATE"].ToString(), false);
                        resultArgs = objCompBuild.AssignProcessDate();
                        if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            DateTime dttemp = new DateTime();
                            dttemp = UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0]["PROCESS_DATE"].ToString(), false);
                            if (dttemp >= deProcessDate.Properties.MinValue && dttemp <= deProcessDate.Properties.MaxValue)
                            {
                                deProcessDate.DateTime = UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0]["PROCESS_DATE"].ToString(), false);
                            }
                            else
                            {
                                deProcessDate.DateTime = deProcessDate.Properties.MaxValue;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private bool CheckComponentsMappedORNot()
        {
            int result = 0;
            using (clsprCompBuild objPRBuild = new clsprCompBuild())
            {
                result = objPRBuild.CheckComponentsMappedORNot(lkpGroupData.EditValue.ToString(), clsGeneral.PAYROLL_ID);

                //On 07/03/2022, In old database, Component eqname contains corrupated characters 'Â'
                if (result > 0)
                {
                    if (objPRBuild.IsComponentEquationCorrupted())
                    {
                        this.ShowMessageBox("Component equation contains corrupted characters, Run Acme.erp Updater.");
                        result = 0;
                    }
                }

            }
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private bool CheckStaffGroupMapped()
        {
            int result = 0;
            using (clsprCompBuild objPRBuilds = new clsprCompBuild())
            {
                result = objPRBuilds.CheckStaffGroupMapped(lkpGroupData.EditValue.ToString(), clsGeneral.PAYROLL_ID);
            }
            if (result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Check whether component ledger mapped with the component ledgers
        /// </summary>
        /// <returns></returns>
        public ResultArgs IsComponentLedgersMapped()
        {
            try
            {
                DataView dvLedTempCheck = new DataView();
                using (clsprCompBuild objCompBuild = new clsprCompBuild())
                {
                    AcMELog.WriteLog("Payroll.Fetching Ledgers mapped with the Components started.");
                    resultArgs = objCompBuild.FetchComponentMappedLedgers();
                    AcMELog.WriteLog("Payroll.Fetching Ledgers  mapped with the Components ended.");
                    if (resultArgs.Success && resultArgs.DataSource.TableView.Table.Rows.Count > 0)
                    {
                        AcMELog.WriteLog("Payroll.Checking all the ledgers are mapped with the Components started.");
                        dvLedTempCheck = resultArgs.DataSource.TableView;
                        dvLedTempCheck.RowFilter = "LEDGER_ID <> 0";
                        if (dvLedTempCheck.ToTable().Rows.Count == resultArgs.DataSource.TableView.Table.Rows.Count)
                        {
                            AcMELog.WriteLog("Payroll.Checking all the ledgers are mapped with the Components ended.");
                            DataTable dtTempVal = dvLedTempCheck.ToTable();
                            string LedgerIdtemp = string.Empty;
                            foreach (DataRow dr in dtTempVal.Rows)
                            {
                                LedgerIdtemp += dr["LEDGER_ID"].ToString() + ',';
                            }
                            LedgerIdtemp = LedgerIdtemp.TrimEnd(',');
                            AcMELog.WriteLog("Payroll.Checking all the ledgers are mapped with the Projects started.");
                            resultArgs = IsProjectLedgersMapped(LedgerIdtemp);
                            AcMELog.WriteLog("Payroll.Checking all the ledgers are mapped with the Projects ended.");
                            if (resultArgs.Success)
                            {
                                AcMELog.WriteLog("Payroll.Ledger mapped with project successfully.");
                            }
                            else
                            {
                                //resultArgs.Message = "Problem while mapping Ledger with the Project.";
                                resultArgs.Message = this.GetMessage(MessageCatalog.Payroll.PayrollBrowseView.BROWSE_MAP_LEDGER_PROJECT_INFO);
                            }
                        }
                        else
                        {
                            //resultArgs.Message = "Ledgers are not mapped with the Components,try after mapping the ledgers.";
                            resultArgs.Message = this.GetMessage(MessageCatalog.Payroll.PayrollBrowseView.BROWSE_MAP_LEDGER_COMPONENT_INFO);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            return resultArgs;
        }

        private ResultArgs IsLoanLedgersaremappedwithProject()
        {
            string ProjectId = glkpProject.EditValue != null ? glkpProject.EditValue.ToString() : "0";
            using (clsprCompBuild objCompBuild = new clsprCompBuild())
            {
                int ConfLedger = objCompBuild.IsLoanCompLedgerMappedwithProject(ProjectId);
                if (ConfLedger == 0)
                {
                    using (clsPrComponent objComp = new clsPrComponent())
                    {
                        resultArgs = objComp.MapProjectLedger("1001", ProjectId);
                        resultArgs = objComp.MapProjectLedger("1002", ProjectId);
                        AcMELog.WriteLog("Payroll.Mapping Ledger with the Project ended.");
                    }
                }
            }
            return resultArgs;
        }
        /// <summary>
        /// Check whether Process ledger mapped with the Process type
        /// </summary>
        /// <returns></returns>
        public ResultArgs IsProcessTypesMapped()
        {
            Processtype processtype = new Processtype();
            DataView dvPType = UtilityMember.EnumSet.GetEnumDataSource(processtype, Sorting.None);
            try
            {
                DataView dvLedTempCheck = new DataView();
                using (clsPayrollProcess objProcess = new clsPayrollProcess())
                {
                    resultArgs = objProcess.FetchMappedLedger();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        if (resultArgs.DataSource.Table.Rows.Count == dvPType.ToTable().Rows.Count)
                        {
                            DataTable dtTempVal = resultArgs.DataSource.Table;
                            string LedgerIdtemp = string.Empty;
                            foreach (DataRow dr in dtTempVal.Rows)
                            {
                                LedgerIdtemp += dr["LEDGER_ID"].ToString() + ',';
                            }
                            LedgerIdtemp = LedgerIdtemp.TrimEnd(',');
                            if (!string.IsNullOrEmpty(LedgerIdtemp))
                            {
                                resultArgs = IsProjectLedgersMapped(LedgerIdtemp);
                                if (resultArgs.Success)
                                {
                                    AcMELog.WriteLog("Payroll.Process Ledger mapped with project successfully.");
                                }
                                else
                                {
                                    //resultArgs.Message = "Problem while mapping Ledger with the Project.";
                                    resultArgs.Message = this.GetMessage(MessageCatalog.Payroll.PayrollBrowseView.BROWSE_MAP_LEDGER_PROJECT_INFO);
                                }
                            }
                        }
                        else
                        {
                            //resultArgs.Message = "Process Type Ledgers are not mapped with the Process Type.";
                            resultArgs.Message = this.GetMessage(MessageCatalog.Payroll.PayrollBrowseView.BROWSE_MAP_PROCESS_TYPE_LEDGER_MAP_INFO);
                        }
                    }
                    else
                    {
                        //resultArgs.Message = "Process Type Ledgers are not mapped with the Process Type.";
                        resultArgs.Message = this.GetMessage(MessageCatalog.Payroll.PayrollBrowseView.BROWSE_MAP_PROCESS_TYPE_LEDGER_NOTMAP_INFO);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            return resultArgs;
        }

        /// <summary>
        /// Delete Journal entry before process payroll by reference id
        /// </summary>
        /// <returns></returns>
        public ResultArgs DeleteProcessPayVouher()
        {
            try
            {
                clsPayrollProcess mapledger = new clsPayrollProcess();
                string ProjectId = glkpProject.EditValue != null ? glkpProject.EditValue.ToString() : "0";
                int VoucherrId = mapledger.FetchVoucherMasterPayVoucherbyCId(ProjectId);
                resultArgs = DeleteVoucherTrans(VoucherrId);
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            return resultArgs;
        }
        public ResultArgs IsComponentMappedForGrups(int projectId)
        {
            try
            {

            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            return resultArgs;
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

        /// <summary>
        /// Fetch staff ids mapped with the project
        /// </summary>
        /// <returns></returns>
        public string GetmappedStaffs()
        {
            string MapStaff = string.Empty;
            try
            {
                using (clsPrGroupStaff GroupStaff = new clsPrGroupStaff())
                {
                    string Projectid = (glkpProject.EditValue != null) ? glkpProject.EditValue.ToString() : "0";
                    string GroupId = !string.IsNullOrEmpty(PayGroupId) ? PayGroupId : "0";
                    DataTable dtMappedStaffs = GroupStaff.GetProjectGroupMappedStaffs(Projectid, GroupId);
                    foreach (DataRow dr in dtMappedStaffs.Rows)
                    {
                        MapStaff += dr["STAFFID"].ToString() + ',';
                    }
                    MapStaff = MapStaff.Trim(',');
                }
            }
            catch (Exception ed)
            {
                MessageRender.ShowMessage(ed.ToString(), false);
            }
            finally { }
            return MapStaff;
        }

        /// <summary>
        /// Fetch Process date to process journal entry for the date
        /// </summary>
        /// <returns></returns>
        public DateTime GetProcessDate()
        {
            DateTime dtTemp = new DateTime();
            try
            {
                using (clsprCompBuild objCompBuild = new clsprCompBuild())
                {
                    resultArgs = objCompBuild.AssignProcessDate();
                    if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        dtTemp = UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0]["PROCESS_DATE"].ToString(), false);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
            return dtTemp;
        }

        /// <summary>
        ///  Check whether all the ledger mapped with project
        /// </summary>
        /// <param name="LedgId"></param>
        /// <returns></returns>
        public ResultArgs IsProjectLedgersMapped(string LedgId)
        {
            resultArgs.Success = true;
            try
            {
                AcMELog.WriteLog("Payroll.Mapped Ledger Ids : " + LedgId);
                string[] LedgIds = LedgId.Split(',');
                foreach (string LedgerrIds in LedgIds)
                {
                    DataView dvLedTempCheck = new DataView();
                    using (clsprCompBuild objCompBuild = new clsprCompBuild())
                    {
                        AcMELog.WriteLog("Payroll.Checking Ledger mapped with the Projects started.");
                        string ProjectId = glkpProject.EditValue != null ? glkpProject.EditValue.ToString() : "0";
                        int ConfLedger = objCompBuild.IsCompLedgerMappedwithProject(LedgerrIds, ProjectId);
                        AcMELog.WriteLog("Payroll.Checking Ledger mapped with the Projects ended.");
                        if (ConfLedger == 0)
                        {
                            using (clsPrComponent objComp = new clsPrComponent())
                            {
                                AcMELog.WriteLog("Payroll.Mapping LedgerID: " + LedgerrIds + " with the Project started.");
                                resultArgs = objComp.MapProjectLedger(LedgerrIds, ProjectId);
                                AcMELog.WriteLog("Payroll.Mapping Ledger with the Project ended.");
                            }
                        }
                        else
                        {
                            AcMELog.WriteLog("Payroll.Provided Ledger mapped with the already.");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            return resultArgs;
        }

        /// <summary>
        /// Fetch Group ids mapped with staff and payroll
        /// </summary>
        /// <returns></returns>
        private string GetPayrollGroupsIds()
        {
            string PayGroup = string.Empty;
            try
            {
                resultArgs = objClsStaff.FetchGroups("Group");
                if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    DataTable dtGroups = resultArgs.DataSource.Table;
                    if (dtGroups.Rows.Count > 0)
                    {
                        foreach (DataRow dr in dtGroups.Rows)
                        {
                            PayGroup += dr["GROUP ID"].ToString() + ',';
                        }
                    }
                    PayGroup = PayGroup.TrimEnd(',');
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
            return PayGroup;
        }

        /// <summary>
        /// Process payroll by passing journal entry
        /// </summary>
        /// <returns></returns>
        public ResultArgs ProcessJournalEntry()
        {
            try
            {
                using (clsprCompBuild objCompBuild = new clsprCompBuild())
                {

                    using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                    {
                        //Voucher Master Details
                        voucherTransaction.VoucherId = 0;
                        voucherTransaction.ProjectId = UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
                        voucherTransaction.VoucherDate = GetProcessDate(); // this.UtilityMember.DateSet.ToDate(clsGeneral.PAYROLLDATE, false);
                        voucherTransaction.VoucherType = "JN";
                        voucherTransaction.Status = (int)YesNo.Yes;
                        voucherTransaction.TransVoucherMethod = (int)TransactionVoucherMethod.Automatic;
                        voucherTransaction.CreatedOn = this.UtilityMember.DateSet.ToDate(this.UtilityMember.DateSet.GetDateToday(false), false);
                        voucherTransaction.ModifiedOn = this.UtilityMember.DateSet.ToDate(this.UtilityMember.DateSet.GetDateToday(false), false);
                        voucherTransaction.CreatedBy = this.UtilityMember.NumberSet.ToInteger(this.LoginUser.LoginUserId.ToString());
                        voucherTransaction.ModifiedBy = this.UtilityMember.NumberSet.ToInteger(this.LoginUser.LoginUserId.ToString());
                        //voucherTransaction.Narration = "Payroll Processed for the " + dtTempProcess.Rows[Ptype]["Name"].ToString() + " Process type for the Month of " + clsGeneral.PAYROLL_MONTH;
                        //voucherTransaction.Narration = "Payroll Processed for the Month of " + clsGeneral.PAYROLL_MONTH;
                        voucherTransaction.Narration = this.GetMessage(MessageCatalog.Payroll.PayrollBrowseView.BROWSE_MAP_PAYROLL_PROCESS_MONTH_INFO) + clsGeneral.PAYROLL_MONTH;
                        voucherTransaction.VoucherSubType = ledgerSubType.PAY.ToString();
                        voucherTransaction.ClientReferenceId = this.UtilityMember.NumberSet.ToInteger(clsGeneral.PAYROLL_ID.ToString()).ToString();
                        //Voucher Trans Details
                        DataView dvLedgerDetails = ConstructData();
                        if (dvLedgerDetails != null && dvLedgerDetails.Table.Rows.Count > 0)
                        {
                            Transaction.TransInfo = dvLedgerDetails;
                            resultArgs = voucherTransaction.SaveTransactions();
                        }
                    }
                }
            }
            catch (Exception ed)
            {
                resultArgs.Message = ed.Message;
            }
            return resultArgs;
        }
        private ResultArgs ProcessJournalEntryforLoan()
        {
            try
            {
                using (clsprCompBuild objCompBuild = new clsprCompBuild())
                {

                    using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                    {
                        //Voucher Master Details
                        voucherTransaction.VoucherId = 0;
                        voucherTransaction.ProjectId = UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
                        voucherTransaction.VoucherDate = GetProcessDate(); // this.UtilityMember.DateSet.ToDate(clsGeneral.PAYROLLDATE, false);
                        voucherTransaction.VoucherType = "JN";
                        voucherTransaction.Status = (int)YesNo.Yes;
                        voucherTransaction.TransVoucherMethod = (int)TransactionVoucherMethod.Automatic;
                        voucherTransaction.CreatedOn = this.UtilityMember.DateSet.ToDate(this.UtilityMember.DateSet.GetDateToday(false), false);
                        voucherTransaction.ModifiedOn = this.UtilityMember.DateSet.ToDate(this.UtilityMember.DateSet.GetDateToday(false), false);
                        voucherTransaction.CreatedBy = this.UtilityMember.NumberSet.ToInteger(this.LoginUser.LoginUserId.ToString());
                        voucherTransaction.ModifiedBy = this.UtilityMember.NumberSet.ToInteger(this.LoginUser.LoginUserId.ToString());
                        //voucherTransaction.Narration = "Payroll Processed for the " + dtTempProcess.Rows[Ptype]["Name"].ToString() + " Process type for the Month of " + clsGeneral.PAYROLL_MONTH;
                        //voucherTransaction.Narration = "Payroll Processed for the Month of " + clsGeneral.PAYROLL_MONTH;
                        voucherTransaction.Narration = this.GetMessage(MessageCatalog.Payroll.PayrollBrowseView.BROWSE_MAP_PAYROLL_PROCESS_MONTH_INFO) + clsGeneral.PAYROLL_MONTH;
                        voucherTransaction.VoucherSubType = ledgerSubType.PAY.ToString();
                        voucherTransaction.ClientReferenceId = this.UtilityMember.NumberSet.ToInteger(clsGeneral.PAYROLL_ID.ToString()).ToString();
                        //Voucher Trans Details
                        DataView dvLedgerDetails = ConstructDataforLoan();
                        if (dvLedgerDetails != null && dvLedgerDetails.Table.Rows.Count > 0)
                        {
                            Transaction.TransInfo = dvLedgerDetails;
                            resultArgs = voucherTransaction.SaveTransactions();
                        }
                    }

                }
            }
            catch (Exception ed)
            {
                resultArgs.Message = ed.Message;
            }
            return resultArgs;
        }

        private DataView ConstructDataforLoan()
        {
            DataView dvtemp = new DataView();
            DataView dvLedgerDetails = new DataView();
            DataTable dtLedgerTemp = new DataTable();
            Processtype processtype = new Processtype();

            DataView dvPType = UtilityMember.EnumSet.GetEnumDataSource(processtype, Sorting.None);
            DataTable dtTempProcess = dvPType.ToTable();
            AcMELog.WriteLog("Constructs empty data structure started..");
            dtLedgerTemp = ConstructEmptySource();
            AcMELog.WriteLog("Constructs empty data structure ended..");
            using (clsprCompBuild objCompBuild = new clsprCompBuild())
            {
                AcMELog.WriteLog("Fetching Processed value by staff id to process journal entry started");
                resultArgs = objCompBuild.FetchLoanComponentValueToProcess(MappedStaffId);
                AcMELog.WriteLog("Fetching Processed value by staff id to process journal entry ended");
                dvtemp = resultArgs.DataSource.TableView;
                if (dtTempProcess != null && dtTempProcess.Rows.Count > 0)
                {
                    for (int Ptype = 0; Ptype < dtTempProcess.Rows.Count; Ptype++)
                    {
                        dvtemp.RowFilter = string.Empty;
                        dvtemp.RowFilter = "PROCESS_TYPE_ID=" + Ptype + "";
                        if (dvtemp.ToTable().Rows.Count > 0)
                        {
                            foreach (DataRow dr in dvtemp.ToTable().Rows)
                            {
                                ProcessLedgerId = UtilityMember.NumberSet.ToInteger(dr["PROCESS_LEDGER_ID"].ToString());
                                dtLedgerTemp.Rows.Add(1001, string.Empty, 0.00, UtilityMember.NumberSet.ToDecimal(dr["ACTUAL_AMOUNT"].ToString()), 0.00, 0.00, string.Empty, 0);
                                dtLedgerTemp.Rows.Add(1002, string.Empty, 0.00, UtilityMember.NumberSet.ToDecimal(dr["INRESTAMOUNT"].ToString()), 0.00, 0.00, string.Empty, 0);
                            }
                            dtLedgerTemp.Rows.Add(ProcessLedgerId, string.Empty, this.UtilityMember.NumberSet.ToDecimal(dvtemp.ToTable().Compute("SUM(AMOUNT)", "PROCESS_TYPE_ID=" + Ptype + "").ToString()), 0.00, 0.00, 0.00, string.Empty, 0);
                        }

                    }
                    dvLedgerDetails = dtLedgerTemp.AsDataView();
                }
            }
            return dvLedgerDetails;
        }
        /// <summary>
        /// Construct empty source
        /// </summary>
        /// <returns></returns>
        private DataTable ConstructEmptySource()
        {
            DataTable dt = new DataTable();
            dt.Columns.Add("LEDGER_ID", typeof(string));
            dt.Columns.Add("NARRATION", typeof(string));
            dt.Columns.Add("DEBIT", typeof(decimal));
            dt.Columns.Add("CREDIT", typeof(decimal));
            dt.Columns.Add("TEMP_CREDIT", typeof(decimal));
            dt.Columns.Add("TEMP_DEBIT", typeof(decimal));
            dt.Columns.Add("LEDGER_BALANCE", typeof(string));
            dt.Columns.Add("VALUE", typeof(int));
            return dt;
        }

        /// <summary>
        /// Construct data structure for voucher
        /// </summary>
        /// <returns></returns>
        private DataView ConstructData()
        {
            DataView dvtemp = new DataView();
            DataView dvLedgerDetails = new DataView();
            DataTable dtLedgerTemp = new DataTable();
            Processtype processtype = new Processtype();

            DataView dvPType = UtilityMember.EnumSet.GetEnumDataSource(processtype, Sorting.None);
            DataTable dtTempProcess = dvPType.ToTable();
            AcMELog.WriteLog("Constructs empty data structure started..");
            dtLedgerTemp = ConstructEmptySource();
            AcMELog.WriteLog("Constructs empty data structure ended..");
            using (clsprCompBuild objCompBuild = new clsprCompBuild())
            {
                AcMELog.WriteLog("Fetching Processed value by staff id to process journal entry started");
                //resultArgs = objCompBuild.FetchComponentValueToProcessLedger(MappedStaffId);
                resultArgs = objCompBuild.FetchLoanComponentValueToProcess(MappedStaffId);
                AcMELog.WriteLog("Fetching Processed value by staff id to process journal entry ended");
                dvtemp = resultArgs.DataSource.TableView;
                if (dtTempProcess != null && dtTempProcess.Rows.Count > 0)
                {
                    for (int Ptype = 0; Ptype < dtTempProcess.Rows.Count; Ptype++)
                    {
                        dvtemp.RowFilter = string.Empty;
                        dvtemp.RowFilter = "PROCESS_TYPE_ID=" + Ptype + "";
                        //if (Ptype != 3)
                        //{
                        if (dvtemp.ToTable().Rows.Count > 0)
                        {
                            foreach (DataRow dr in dvtemp.ToTable().Rows)
                            {
                                ProcessLedgerId = UtilityMember.NumberSet.ToInteger(dr["PROCESS_LEDGER_ID"].ToString());
                                dtLedgerTemp.Rows.Add(dr["LEDGER_ID"], string.Empty, UtilityMember.NumberSet.ToDecimal(dr["AMOUNT"].ToString()), 0.00, 0.00, 0.00, string.Empty, 0);
                            }
                            dtLedgerTemp.Rows.Add(ProcessLedgerId, string.Empty, 0.00, this.UtilityMember.NumberSet.ToDecimal(dvtemp.ToTable().Compute("SUM(AMOUNT)", "PROCESS_TYPE_ID=" + Ptype + "").ToString()), 0.00, 0.00, string.Empty, 0);
                        }
                        //}
                        //else
                        //{
                        //    if (dvtemp.ToTable().Rows.Count > 0)
                        //    {
                        //        foreach (DataRow dr in dvtemp.ToTable().Rows)
                        //        {
                        //            ProcessLedgerId = UtilityMember.NumberSet.ToInteger(dr["PROCESS_LEDGER_ID"].ToString());
                        //            dtLedgerTemp.Rows.Add(1001, string.Empty, 0.00, UtilityMember.NumberSet.ToDecimal(dr["ACTUAL_AMOUNT"].ToString()), 0.00, 0.00, string.Empty, 0);
                        //            dtLedgerTemp.Rows.Add(1002, string.Empty, 0.00, UtilityMember.NumberSet.ToDecimal(dr["INRESTAMOUNT"].ToString()), 0.00, 0.00, string.Empty, 0);
                        //        }
                        //        dtLedgerTemp.Rows.Add(ProcessLedgerId, string.Empty, this.UtilityMember.NumberSet.ToDecimal(dvtemp.ToTable().Compute("SUM(AMOUNT)", "PROCESS_TYPE_ID=" + Ptype + "").ToString()), 0.00, 0.00, 0.00, string.Empty, 0);
                        //    }

                        //}
                    }
                    dvLedgerDetails = dtLedgerTemp.AsDataView();
                }
            }
            return dvLedgerDetails;
        }

        ///// <summary>
        ///// Fetch Projects
        ///// </summary>
        //private void LoadProjects()
        //{
        //    try
        //    {
        //        using (PayrollSystem Paysystem = new PayrollSystem())
        //        {
        //            resultArgs = Paysystem.FetchPayrollProjects();
        //            glkpProject.Properties.DataSource = null;
        //            if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
        //            {
        //                UtilityMember.ComboSet.BindGridLookUpCombo(glkpProject, resultArgs.DataSource.Table, Paysystem.AppSchema.PRPayrollProject.PROJECTColumn.ColumnName, Paysystem.AppSchema.PRPayrollProject.PROJECT_IDColumn.ColumnName);
        //                glkpProject.EditValue = glkpProject.Properties.GetKeyValue(0);
        //            }
        //        }
        //    }
        //    catch (Exception Ex)
        //    {
        //        MessageRender.ShowMessage(Ex.Message);
        //    }
        //    finally { }
        //}

        private bool ValidateFields()
        {
            bool isvalid = true;
            //if (glkProject.EditValue == null || string.IsNullOrEmpty(glkProject.Text) || glkProject.EditValue.ToString() == "0")
            if (SettingProperty.PayrollFinanceEnabled)
            {
                if (glkpProject.EditValue == null || string.IsNullOrEmpty(glkpProject.Text))
                {
                    //XtraMessageBox.Show("Project is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.PayrollBrowseView.BROWSE_MAP_LEDGER_PROJECT_INFO));
                    isvalid = false;
                }
            }
            return isvalid;
        }


        /// <summary>
        /// On 30/05/2019, to lock changing group and view after modifed;
        /// </summary>
        /// <param name="EnableProcess"></param>
        /// <param name="staffid"></param>
        private void ValidateAndEnableProcess(bool EnableProcess, string staffid)
        {
            Int32 payrollgroupId = 0;
            if (EnableProcess)
            {
                lkpGroupData.Enabled = btnView.Enabled = true;
                ModifiedStaffList.Clear();
                payrollgroupId = (lkpGroupData.EditValue != null ? UtilityMember.NumberSet.ToInteger(lkpGroupData.EditValue.ToString()) : 0);
                bool PayrollPaymentPosted = objPRGW.IsPayrollPostPaymentPosted(clsGeneral.PAYROLL_ID, payrollgroupId);
                if (PayrollPaymentPosted)
                {
                    MessageRender.ShowMessage("Payroll Payment Voucher was already posted for '" + lkpGroupData.Text + "', Update Payroll Payment Voucher");
                }
            }
            else
            {
                lkpGroupData.Enabled = btnView.Enabled = false;
                ModifiedStaffList.Add(staffid);
                lblTime.Text = "Few Staff Component(s) values are modified, Process again";
            }
        }
        #endregion

        #region Process payroll Events
        private void btnProcess_Click(object sender, EventArgs e)
        {
            bool IsProcessed = false;
            if (this.ShowConfirmationMessage("Are you sure to Process Payroll for " + clsGeneral.PAYROLL_MONTH + "?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
            {
                bool IsRestLinkComponents = false;
                gvPayrollBrowseView.Columns.Clear();
                gcPayrollBrowseView.DataSource = null;

                DateTime dtStartTime = DateTime.Now;
                lblTime.Text = string.Empty;

                if (CheckComponentsMappedORNot())
                {
                    if (CheckStaffGroupMapped())
                    {
                        try
                        {
                            //31/07/2019, to get confirmation for reset link components
                            if (chkResetLinkValue.Checked)
                            {
                                string msg = "Are you sure to get linked component's value from Staff Profile (Pay Info basic information) and " +
                                            "Recalculate editable formula Components ? " +
                                            System.Environment.NewLine + System.Environment.NewLine
                                            + "Yes : Take values from Staff Profile for the Payroll Process and Recalculate editable formula Components." + System.Environment.NewLine +
                                              "No  : Take values from Current Month Payroll values from the grid.";

                                IsRestLinkComponents = (this.ShowConfirmationMessage(msg, MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes);
                            }

                            lcProcess.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                            this.Cursor = Cursors.WaitCursor;
                            lkpGroupData.Enabled = false;
                            if (ValidateFields())
                            {
                                InsertProcessDate();
                                if (SettingProperty.PayrollFinanceEnabled)
                                {
                                    resultArgs = IsComponentLedgersMapped();
                                    if (resultArgs.Success)
                                    {
                                        resultArgs = IsProcessTypesMapped();
                                        if (resultArgs.Success)
                                        {
                                            resultArgs = DeleteProcessPayVouher();
                                            if (resultArgs.Success)
                                            {
                                                if (glkpProject.EditValue != null)
                                                {

                                                    int projectId = UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
                                                    resultArgs = objProcess.FetchmappedComponentsByProjectId(projectId);
                                                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                                                    {
                                                        if (clsGeneral.PAYROLL_ID == 0)
                                                            return;
                                                        fraProcess.Visible = true;
                                                        fraProcess.Refresh();
                                                        if (MappedStaffId != string.Empty && PayGroupId != string.Empty)
                                                        {
                                                            lcProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                                            using (clsprCompBuild objCompBuild = new clsprCompBuild())
                                                            {
                                                                AcMELog.WriteLog("Process payroll started");

                                                                //using (clsProcessPayroll objprPayroll = new clsProcessPayroll())
                                                                //{
                                                                //    resultArgs = objprPayroll.FetchStaffDetails(PayGroupId, clsGeneral.PAYROLL_ID);
                                                                //    if (resultArgs != null && resultArgs.DataSource.Table.Rows.Count > 0)
                                                                //    {
                                                                //        DataTable dt = resultArgs.DataSource.Table;
                                                                //        List<DataTable> tables = new List<DataTable>();
                                                                //        tables = objprPayroll.SplitTable(dt, dt.Rows.Count / 1);
                                                                //        foreach (DataTable dtList in tables)
                                                                //        {
                                                                //            Task.Factory.StartNew(() => objprPayroll.ProcessComponent(clsGeneral.PAYROLL_ID, PayGroupId, "", MappedStaffId, false, fraProcess, false, dtList.AsDataView()));

                                                                //            //Thread thread = new Thread(() => objprPayroll.ProcessComponent(clsGeneral.PAYROLL_ID, PayGroupId, "", MappedStaffId, false, fraProcess, false, dtList.AsDataView()));
                                                                //            //thread.Start();
                                                                //        }

                                                                //    }
                                                                //}

                                                                IsProcessed = objCompBuild.ProcessComponent(clsGeneral.PAYROLL_ID, PayGroupId, "", MappedStaffId, false, fraProcess, false, IsRestLinkComponents, true);
                                                                AcMELog.WriteLog("Process payroll ended");
                                                                AcMELog.WriteLog("Order Components started.");
                                                                string[] grpcollection = PayGroupId.Split(',');
                                                                foreach (string Grpid in grpcollection)
                                                                {
                                                                    objPRComp.SaveAssignedComponent(clsGeneral.PAYROLL_ID, long.Parse(Grpid), dtComponents);
                                                                }
                                                                AcMELog.WriteLog("Order Components ended.");
                                                                AcMELog.WriteLog("Process Journal entry started.");
                                                                resultArgs = IsLoanLedgersaremappedwithProject();
                                                                if (resultArgs.Success)
                                                                {
                                                                    resultArgs = ProcessJournalEntry();
                                                                    // if (resultArgs.Success)
                                                                    //{
                                                                    // resultArgs = ProcessJournalEntryforLoan();
                                                                    AcMELog.WriteLog("Process Journal entry ended.");
                                                                    if (resultArgs.Success)
                                                                    {
                                                                        AcMELog.WriteLog("Processed payroll successfully..");
                                                                        //DialogResult drs = XtraMessageBox.Show("Processed....", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                                        drs = DialogResult.OK;
                                                                        //this.ShowSuccessMessage("Processed....");
                                                                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Payroll.PayrollBrowseView.BROWSE_MAP_PROCESS_INFO));
                                                                        if (drs == DialogResult.OK)
                                                                        {

                                                                            //PAYROLL.Modules.Payroll_app.frmPayrollBrowseView PayrollView = new PAYROLL.Modules.Payroll_app.frmPayrollBrowseView();
                                                                            //PayrollView.MdiParent = this;
                                                                            //PayrollView.Show();

                                                                            //frmPayrollBrowseView browseview = new frmPayrollBrowseView();
                                                                            //browseview.ShowDialog();
                                                                            lcProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                                                            lkpGroupData.Enabled = true;
                                                                        }
                                                                        else
                                                                        {
                                                                            //frmProcessPayroll objpayrollprocess = new frmProcessPayroll();
                                                                            //objpayrollprocess.ShowDialog();
                                                                        }
                                                                    }
                                                                    else
                                                                    {
                                                                        drs = DialogResult.Cancel;
                                                                        MessageRender.ShowMessage(resultArgs.Message.ToString());
                                                                    }
                                                                    fraProcess.Refresh();
                                                                    fraProcess.Visible = false;
                                                                    // }
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            //XtraMessageBox.Show("Staff are not mapped with the group.Map the staff and try again.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.PayrollBrowseView.BROWSE_MAP_STAFF_MAPWITH_GROUP_INFO));
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //XtraMessageBox.Show("Components are not mapped to the groups.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.PayrollBrowseView.BROWSE_MAP_COMPONENT_MAPWITH_GROUP_INFO));
                                                    }
                                                }
                                                else
                                                {
                                                    //XtraMessageBox.Show("Projects are not available to process payroll.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.PayrollBrowseView.BROWSE_MAP_PROJECT_NOT_AVAILABLE_INFO));
                                                }
                                            }
                                            else
                                            {
                                                //XtraMessageBox.Show(resultArgs.Message, "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                                XtraMessageBox.Show(resultArgs.Message, this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE));
                                            }
                                        }
                                        else
                                        {
                                            // XtraMessageBox.Show("Ledgers are not mapped with either Project or Components.Map the Ledgers and try again.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            //XtraMessageBox.Show(resultArgs.Message, "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                            XtraMessageBox.Show(resultArgs.Message, this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE));
                                        }
                                    }
                                    else
                                    {
                                        // XtraMessageBox.Show("Ledgers are not mapped with either Project or Components.Map the Ledgers and try again.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        //XtraMessageBox.Show(resultArgs.Message, "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        XtraMessageBox.Show(resultArgs.Message, this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_TITLE));
                                    }
                                }
                                else
                                {
                                    using (clsprCompBuild objCompBuild = new clsprCompBuild())
                                    {

                                        lkpGroupData.Enabled = true;
                                        fraProcess.Visible = true;
                                        fraProcess.Refresh();
                                        lcProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                                        string SelectedGroupId = lkpGroupData.EditValue.ToString();
                                        if (!string.IsNullOrEmpty(SelectedGroupId) && SelectedGroupId != "0")
                                        {
                                            //On 30/07/2019, to clear and reset payextra info in prstafftemp and reset from staff profile ---------------------------------
                                            if (IsRestLinkComponents)
                                            {
                                                using (clsPrGateWay prgateway = new clsPrGateWay())
                                                {
                                                    double TotalDaysinPaymonth = objCompBuild.GetTotalDaysInPayMonth();
                                                    prgateway.RestPayExtraInfoFromStaffProfile(clsGeneral.PAYROLL_ID, UtilityMember.NumberSet.ToInteger(SelectedGroupId), TotalDaysinPaymonth);
                                                }
                                            }
                                            //-------------------------------------------------------------------------------------------------------------------------------

                                            // objCompBuild.DeleteStaffTempDetails();
                                            //On 30/05/2019, to process only modified staff -------------------------------------------------------------------------------
                                            //objCompBuild.ProcessComponent(clsGeneral.PAYROLL_ID, SelectedGroupId, "", "", false, fraProcess, false);
                                            string ModifiedStaff = string.Empty;
                                            foreach (string itemStaff in ModifiedStaffList)
                                            {
                                                ModifiedStaff += itemStaff + ",";
                                            }
                                            ModifiedStaff = ModifiedStaff.TrimEnd(',');
                                            objCompBuild.ProcessComponent(clsGeneral.PAYROLL_ID, SelectedGroupId, "", ModifiedStaff, false, fraProcess, false, IsRestLinkComponents, true);
                                            ValidateAndEnableProcess(true, string.Empty);

                                            //------------------------------------------------------------------------------------------------------------------------------

                                            //using (clsProcessPayroll objprPayroll = new clsProcessPayroll())
                                            //{
                                            //    resultArgs = objprPayroll.FetchStaffDetails(PayGroupId, clsGeneral.PAYROLL_ID);
                                            //    if (resultArgs != null && resultArgs.DataSource.Table.Rows.Count > 0)
                                            //    {
                                            //        DataTable dt = resultArgs.DataSource.Table;
                                            //        List<DataTable> tables = new List<DataTable>();
                                            //        tables = objprPayroll.SplitTable(dt, dt.Rows.Count / 2);
                                            //        foreach (DataTable dtList in tables)
                                            //        {
                                            //            Task.Factory.StartNew(() => objprPayroll.ProcessComponent(clsGeneral.PAYROLL_ID, SelectedGroupId, "", MappedStaffId, false, fraProcess, false, dtList.AsDataView()));

                                            //            //Thread thread = new Thread(() => objprPayroll.ProcessComponent(clsGeneral.PAYROLL_ID, PayGroupId, "", MappedStaffId, false, fraProcess, false, dtList.AsDataView()));
                                            //            //thread.Start();
                                            //        }

                                            //    }
                                            //}
                                        }
                                        else
                                        {
                                            objCompBuild.ProcessComponent(clsGeneral.PAYROLL_ID, PayGroupId, "", "", false, fraProcess, false, IsRestLinkComponents, true);
                                            //using (clsProcessPayroll objprPayroll = new clsProcessPayroll())
                                            //{
                                            //    resultArgs = objprPayroll.FetchStaffDetails(PayGroupId, clsGeneral.PAYROLL_ID);
                                            //    if (resultArgs != null && resultArgs.DataSource.Table.Rows.Count > 0)
                                            //    {
                                            //        DataTable dt = resultArgs.DataSource.Table;
                                            //        List<DataTable> tables = new List<DataTable>();
                                            //        tables = objprPayroll.SplitTable(dt, dt.Rows.Count / 2);
                                            //        foreach (DataTable dtList in tables)
                                            //        {
                                            //            Task.Factory.StartNew(() => objprPayroll.ProcessComponent(clsGeneral.PAYROLL_ID, PayGroupId, "", MappedStaffId, false, fraProcess, false, dtList.AsDataView()));

                                            //            //Thread thread = new Thread(() => objprPayroll.ProcessComponent(clsGeneral.PAYROLL_ID, PayGroupId, "", MappedStaffId, false, fraProcess, false, dtList.AsDataView()));
                                            //            //thread.Start();
                                            //        }

                                            //    }
                                            //}
                                        }
                                    }

                                    drs = DialogResult.OK;
                                    //this.ShowSuccessMessage("Processed....");
                                    if (IsProcessed)
                                    {
                                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Payroll.PayrollBrowseView.BROWSE_MAP_PROCESS_INFO));
                                    }

                                    this.Cursor = Cursors.WaitCursor;
                                    FillPayrollInfo();
                                    this.Cursor = Cursors.Default;
                                    if (drs == DialogResult.OK)
                                    {
                                        fraProcess.Visible = false;
                                        lcProgress.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                                    }
                                }
                            }

                            lcProcess.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            this.Cursor = Cursors.Default;
                        }
                        catch (Exception Ex)
                        {
                            lcProcess.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                            this.Cursor = Cursors.Default;
                            MessageRender.ShowMessage(Ex.Message);
                        }
                        finally { }
                    }
                    else
                    {
                        //XtraMessageBox.Show("Staffs are not Mapped to the Groups", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.PayrollBrowseView.BROWSE_MAP_STAFF_MAPWITH_GROUP_INFO));
                    }
                }
                else
                {
                    //XtraMessageBox.Show("Components are not Mapped to Groups", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.PayrollBrowseView.BROWSE_MAP_COMPONENT_MAPWITH_GROUP_INFO));
                }

                DateTime dtEndTime = DateTime.Now;
                double elapsedMillisecs = ((TimeSpan)(dtEndTime - dtStartTime)).TotalSeconds;
                lblTime.Text = "Processed Time : " + Math.Round(UtilityMember.NumberSet.ToDouble(elapsedMillisecs.ToString()), 2) + " sec ";
                chkResetLinkValue.Checked = false;
            }
        }
        #endregion

        private void glkpProject_Click(object sender, EventArgs e)
        {
            //LoadProjects();
        }

        private void glkpProject_Leave(object sender, EventArgs e)
        {
            //  LoadProjects();
        }

        //private void gcPayrollBrowseView_ProcessGridKey(object sender, KeyEventArgs e)
        //{

        //}

    }
}


