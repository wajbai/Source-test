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
using PAYROLL.UserControl;
using Bosco.Utility.Common;
using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility.CommonMemberSet;
using Bosco.Utility;
using Bosco.Utility.ConfigSetting;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Bosco.Model.UIModel;
using Bosco.Model.Transaction;
//using Payroll.Model.UIModel;
//using Bosco.Utility.Common;
//using Bosco.DAO.Data;
//using Bosco.Utility;
//using Bosco.Utility.CommonMemberSet;


namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmMapComponenttoGroups : frmPayrollBase
    {
        #region Declaration

        CommonMember UtilityMember = new CommonMember();
        private DevExpress.XtraGrid.GridControl gcComponentDetails;
        private SimpleButton btnProcess = null;
        private SimpleButton btnsSave = null;
        private SimpleButton btnIncrease = null;
        private SimpleButton btnDecrease = null;
        private DataTable getds;
        private clsPrComponent objComponent = new clsPrComponent();
        private int getRowId = 0;
        private clsAllocateComponent objAllocateComp = new clsAllocateComponent();
        ResultArgs resultArgs = new ResultArgs();
        private int SelectedGroupId = 0;
        private int selectedGroupIndex = 0;
        const string SELECT_COL = "SELECT";

        CommonMember commem = new CommonMember();
        #endregion

        #region Oder Component Decalartion
        //public static string PayrollCaption = "Payroll Group Component ";
        public static string PayrollCaption = string.Empty;
        private bool[] borderChanged;
        private int SelectedGroupIndex = 0;
        private bool[] FirstTimeOrder;
        private DataTable dtConst;
        private int RowIndex = 0;
        private clsPayrollComponent objPayrollComp = new clsPayrollComponent();
        private clsPayrollStaff staff = new clsPayrollStaff();
        private clsPrComponent objPrComponent = new clsPrComponent();
        private int iSetFocus = 0;
        private int iSelectedGroupIndex = 0;
        private DataSet ds1;
        private string selectedgroup = string.Empty;
        GridHitInfo downHitInfo = null;
        TransProperty Transaction = new TransProperty();
        UserProperty LoginUser = new UserProperty();
        #endregion

        #region Constructors
        public frmMapComponenttoGroups()
        {
            InitializeComponent();
            PayrollCaption=GetMessage(MessageCatalog.Payroll.MapComponentstoGroups.PAYOLL_GROUP_CAPTION);
        }

        public frmMapComponenttoGroups(SimpleButton btn, SimpleButton btn1, DevExpress.XtraGrid.GridControl component, SimpleButton btnInc, SimpleButton btnDec, DataTable ds)
        {
            InitializeComponent();
            gcMapComponent = component;
            this.btnProcess = btn;
            this.btnsSave = btn1;
            this.btnIncrease = btnInc;
            this.btnDecrease = btnDec;
            this.getds = ds;
        }
        public frmMapComponenttoGroups(string GroupId)
            : this()
        {
            SelectedGroupId = this.UtilityMember.NumberSet.ToInteger(GroupId);

        }
        #endregion

        #region Properties

        private DataTable dtMappedComponents { get; set; }

        private DataTable dtComponents { get; set; }

        #endregion

        #region Order component Properties
        private int payrollcomponentid = 0;
        private int PayrollComponentId
        {
            get
            {
                return (glkpGroups.EditValue != null) ? commem.NumberSet.ToInteger(glkpGroups.EditValue.ToString()) : 0;
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
        //private DateTime processdate = DateTime.Now;
        //public DateTime ProcessDate
        //{
        //    get
        //    {
        //        return processdate;
        //    }
        //    set
        //    {
        //        processdate = value;
        //    }
        //}

        //private string mappedstaffid = string.Empty;
        //public string MappedStaffId
        //{
        //    get
        //    {
        //        return GetmappedStaffs();
        //    }
        //}

        //private int deductionledgerid = 0;
        //public int DeductionLedgerId
        //{
        //    get
        //    {
        //        return deductionledgerid;
        //    }
        //    set
        //    {
        //        deductionledgerid = value;
        //    }
        //}
        //private DateTime processdate = DateTime.Now;
        //public DateTime ProcessDate
        //{
        //    get
        //    {
        //        return processdate;
        //    }
        //    set
        //    {
        //        processdate = value;
        //    }
        //}

        int dragRowHandle = -1;
        int DragRowHandle
        {
            get { return dragRowHandle; }
            set
            {
                dragRowHandle = value;
                gcComponent.Invalidate();
            }
        }
        #endregion

        #region Methods
        protected void SetBorderColor(TextEdit txtEdit)
        {
            txtEdit.Properties.Appearance.BorderColor = string.IsNullOrEmpty(txtEdit.Text) ? Color.Red : Color.Empty;
        }

        private void LoadGroupList()
        {
            try
            {
                DataTable dtGradeList;
                using (clsPayrollGrade Grade = new clsPayrollGrade())
                {
                    dtGradeList = Grade.getPayrollGradeList();
                    borderChanged = new bool[resultArgs.DataSource.Table.Rows.Count];
                    FirstTimeOrder = new bool[resultArgs.DataSource.Table.Rows.Count];
                    if (dtGradeList != null && dtGradeList.Rows.Count > 0)
                    {
                        //dtGradeList.Columns["GROUP ID"].ColumnMapping = MappingType.Hidden;
                        glkpGroups.Properties.ValueMember = "GROUP ID";
                        glkpGroups.Properties.DisplayMember = "Group Name";
                        glkpGroups.Properties.DataSource = dtGradeList;
                        //selectedGroupIndex = glkpGroups.Properties.GetIndexByKeyValue(SelectedGroupId);
                        //glkpGroups.EditValue = glkpGroups.Properties.GetKeyValue(0);
                        glkpGroups.EditValue = SelectedGroupId > 0 ? glkpGroups.Properties.GetKeyValue(glkpGroups.Properties.GetIndexByKeyValue(SelectedGroupId)) : glkpGroups.Properties.GetKeyValue(0);


                        DataTable dtComponents = objPayrollComp.getComponentDetails(PayrollComponentId);
                        try
                        {
                            if (gvComponent.RowCount > 0)
                            {
                                btnInc.Enabled = btnDec.Enabled = true;
                                //SetArrowsState();
                            }
                        }
                        catch (Exception ex)
                        {
                            btnInc.Enabled = btnDec.Enabled;
                            //SetArrowsState();
                        }
                    }
                    else
                    {
                        btnInc.Enabled = btnDec.Enabled;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }
        }

        public bool ValidateMapComponentDetails()
        {
            bool isMapComp = true;
            if (string.IsNullOrEmpty(glkpGroups.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.MapComponentstoGroups.PAYROLL_GROUP_EMPTY));
                this.SetBorderColor(glkpGroups);
                glkpGroups.Focus();
                return false;
            }
            //else if (gcComponent.DataSource==null || (gcComponent.MainView!=null && gcComponent.MainView.RowCount == 0))
            //{
            //    this.ShowMessageBox("No Component(s) are mapped");
            //    this.SetBorderColor(glkpGroups);
            //    glkpGroups.Focus();
            //    return false;
            //}
            else if (!IsFormulaReferenceMapped())
            {
                this.SetBorderColor(glkpGroups);
                glkpGroups.Focus();
                return false;
            }
            else if (!IsBothPayingDaysLOPDaysMapped())
            {
                this.SetBorderColor(glkpGroups);
                glkpGroups.Focus();
                return false;
            }
            else if (!IsProcessComponentTypeMapped())
            {
                this.SetBorderColor(glkpGroups);
                glkpGroups.Focus();
                return false;
            }
            else
            {
                glkpGroups.Focus();
            }
            return isMapComp;
        }

        /// <summary>
        /// On 29/05/2019, Check Forumla's reference components are mapped or not
        /// </summary>
        /// <returns></returns>
        private bool IsFormulaReferenceMapped()
        {
            bool Rtn = true;
            DataTable dtComponent = gcMapComponent.DataSource as DataTable;
            DataTable dtCompValidation = dtComponent.Copy();
            dtCompValidation.DefaultView.RowFilter = "SELECT=1";
            dtCompValidation = dtCompValidation.DefaultView.ToTable();

            try
            {
                if (dtCompValidation != null && dtCompValidation.Rows.Count > 0)
                {
                    foreach (DataRow dr in dtCompValidation.Rows)
                    {
                        Int32 ComponentId = UtilityMember.NumberSet.ToInteger(dr["COMPONENTID"].ToString());
                        string Component = dr["COMPONENT NAME"].ToString().Trim();
                        string relatedComponents = dr["relatedcomponents"].ToString().Trim();
                        string Equcation = dr["Equation"].ToString().Trim();

                        if (relatedComponents == "" || relatedComponents == "#" || relatedComponents == "0.00")
                        {
                            Rtn = true;
                        }
                        else
                        {
                            string[] aRelatedComp = relatedComponents.Split('ê');
                            foreach (string relatedComponent in aRelatedComp)
                            {
                                Int32 relatedComponentId = UtilityMember.NumberSet.ToInteger(relatedComponent);
                                if (relatedComponentId > 0)
                                {
                                    dtCompValidation.DefaultView.RowFilter = "COMPONENTID =" + relatedComponentId + " AND SELECT=1";
                                    if (dtCompValidation.DefaultView.Count == 0)
                                    {
                                        Rtn = false;
                                        this.ShowMessageBox(Component + "'s related Component(s) [" + Equcation + "] are not yet mapped");
                                        break;
                                    }
                                    else
                                    {
                                        Rtn = true;
                                    }
                                    dtCompValidation.DefaultView.RowFilter = string.Empty;
                                }
                            }

                        }

                        if (!Rtn)
                        {
                            break;
                        }
                    }
                    dtCompValidation.DefaultView.RowFilter = string.Empty;
                }
            }
            catch (Exception err)
            {
                Rtn = false;
                MessageRender.ShowMessage(err.Message);
            }
            finally
            {
                if (dtCompValidation != null)
                {
                    dtCompValidation.DefaultView.RowFilter = string.Empty;
                }
            }
            return Rtn;
        }

        /// <summary>
        /// On 19/06/2019, To check process component type (Gross, Net, deduction)
        /// </summary>
        /// <returns></returns>
        private bool IsProcessComponentTypeMapped()
        {
            bool Rtn = true;
            string RtnMessage = string.Empty;
            try
            {
                DataTable dtComponent = gcMapComponent.DataSource as DataTable;
                DataTable dtCompValidation = dtComponent.Copy();
                dtCompValidation.DefaultView.RowFilter = "SELECT=1";
                dtCompValidation = dtCompValidation.DefaultView.ToTable();
                
                if (dtCompValidation != null && dtCompValidation.Rows.Count > 0)
                {
                    //1. Check Gross Wages
                    dtCompValidation.DefaultView.RowFilter = "PROCESS_COMPONENT_TYPE = '"+ PayRollProcessComponent.GrossWages.ToString() + "'";
                    if (dtCompValidation.DefaultView.Count == 0)
                    {
                        Rtn = false;
                        RtnMessage = "Gross Wages Process Component Type is missing";
                    }
                    else if (dtCompValidation.DefaultView.Count > 1)
                    {
                        Rtn = false;
                        RtnMessage = "Only one Gross Wages Process Component Type can be mapped";
                    }

                    //2. NET Pay
                    if (Rtn)
                    {
                        dtCompValidation.DefaultView.RowFilter = "PROCESS_COMPONENT_TYPE = '" + PayRollProcessComponent.NetPay.ToString() + "'";
                        if (dtCompValidation.DefaultView.Count == 0)
                        {
                            Rtn = false;
                            RtnMessage = "NET Pay Process Component Type is missing";
                        }
                        else if (dtCompValidation.DefaultView.Count > 1)
                        {
                            Rtn = false;
                            RtnMessage = "Only one NET Pay Process Component Type can be mapped";
                        }
                    }

                    if (!Rtn)
                    {
                        this.ShowMessageBox(RtnMessage);
                    }
                }
                dtCompValidation.DefaultView.RowFilter = string.Empty;
            }
            catch (Exception err)
            {
                Rtn = false;
                MessageRender.ShowMessage(err.Message);
            }
            return Rtn;
        }

        /// <summary>
        /// On 09/03/2022, To check Paying Salary Days and LOP Days both mapped for one Group
        /// </summary>
        /// <returns></returns>
        private bool IsBothPayingDaysLOPDaysMapped()
        {
            bool Rtn = true;
            bool PayingSalaryDaysMapped = false;
            bool LOPDaysMapped = false;
            string RtnMessage = string.Empty;
            DataTable dtComponent = gcMapComponent.DataSource as DataTable;
            DataTable dtCompValidation = dtComponent.Copy();
            dtCompValidation.DefaultView.RowFilter = "SELECT=1";
            dtCompValidation = dtCompValidation.DefaultView.ToTable();

            try
            {
                if (dtCompValidation != null && dtCompValidation.Rows.Count > 0)
                {
                    //1. Check Paying Salary Days mappded
                    dtCompValidation.DefaultView.RowFilter = "LINKVALUE = '" + PayRollExtraPayInfo.PAYING_SALARY_DAYS.ToString().Replace("_","") + "'";
                    PayingSalaryDaysMapped = (dtCompValidation.DefaultView.Count > 0);

                    //2. Check LOP Days mappded
                    dtCompValidation.DefaultView.RowFilter = string.Empty;
                    dtCompValidation.DefaultView.RowFilter = "[COMPONENT NAME] = '" + PayRollDefaultComponent.LOPDAYS.ToString() + "'";
                    LOPDaysMapped = (dtCompValidation.DefaultView.Count > 0);

                    if (PayingSalaryDaysMapped && LOPDaysMapped)
                    {
                        Rtn = false;
                        this.ShowMessageBox("Paying Salary days or Loss of Pay Days, any one of the component can be mapped for a Pay Group");
                    }
                }
                dtCompValidation.DefaultView.RowFilter = string.Empty;
            }
            catch (Exception err)
            {
                Rtn = false;
                MessageRender.ShowMessage(err.Message);
            }
            finally
            {
                if (dtCompValidation != null)
                {
                    dtCompValidation.DefaultView.RowFilter = string.Empty;
                }
            }
            return Rtn;
        }

        private void fillComponents()
        {

            object getQr = objComponent.getPayrollComponentAllocateQry(clsPayrollConstants.PAYROLL_COMPONENT_LIST);
            using (DataManager dataManager = new DataManager(getQr, "Component"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success && resultArgs.RowsAffected > 0)
                {
                    dtComponents = resultArgs.DataSource.Table;
                    if (!dtComponents.Columns.Contains("SELECT"))
                    {
                        dtComponents.Columns.Add("SELECT", typeof(Int32));
                    }
                    //dtComponentList.Columns["COMPONENTID"].ColumnMapping = MappingType.Hidden;
                    dtComponents.DefaultView.Sort = "TYPE_SORT ASC";
                    dtComponents = dtComponents.DefaultView.ToTable();
                    gcMapComponent.DataSource = dtComponents;
                    gcMapComponent.RefreshDataSource();

                }
                else
                {
                    gcMapComponent.DataSource = null;
                }
            }
        }

        protected virtual void SetFocusRowFilter(DevExpress.XtraGrid.Views.Grid.GridView gridview, DevExpress.XtraGrid.Columns.GridColumn colGridColumn)
        {
            gridview.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
            gridview.FocusedColumn = colGridColumn;
            gridview.OptionsFind.AllowFindPanel = false;
            gridview.ShowEditor();
        }


        //private void LoadMappedComponents()
        //{
        //    try
        //    {

        //        // getRowId = Convert.ToInt32(glkpComponent.EditValue.ToString());
        //        getRowId = glkpGroups.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkpGroups.EditValue.ToString()) : 0;

        //        // object getQuery = objComponent.getPayrollComponentAllocateQry(clsPayrollConstants.PAYROLL_COMP_CHANGE);
        //        DataTable dtComp = null;

        //        clsPrComponent objcompallocate = new clsPrComponent();
        //        DataTable dt = objcompallocate.FetchMapComponent(long.Parse(clsGeneral.PAYROLL_ID.ToString()), long.Parse(getRowId.ToString()));
        //        if (dt.Rows.Count > 0)
        //            dtComp = dt;

        //        if (dtComp != null && dtComp.Rows.Count > 0)
        //        {
        //            gcMapComponent.DataSource = new DataView(dtComp);
        //            gcMapComponent.RefreshDataSource();


        //            SelectMappedComponents(dtComp);

        //            //string[,] TextBoxProperty = new string[,]{
        //            //                                        {"SELECT","FALSE","FALSE","FALSE","FALSE","1","35","BOOLEAN","0","FALSE"},
        //            //                                        {"GROUPNAME","FALSE","TRUE","TRUE","TRUE","100","200","STRING","","FALSE"},
        //            //                                        {"GROUPID","FALSE","TRUE","TRUE","TRUE","25","0","INT","0","FALSE"}
        //            //                                         };
        //            //dgGroups.CreateGridTextBox("Group", TextBoxProperty);
        //        }
        //        else
        //        {
        //            DataTable dtComps = null;
        //            object getQuery = objComponent.getPayrollComponentAllocateQry(clsPayrollConstants.PAYROLL_COMPONENT_LIST);
        //            using (DataManager dataManager = new DataManager(getQuery, "Component"))
        //            {
        //                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
        //                resultArgs = dataManager.FetchData(DataSource.DataTable);
        //                if (resultArgs.Success)
        //                    dtComp = resultArgs.DataSource.Table;
        //            }
        //            //getQuery = getQuery.Replace("<compid>", compIndex.ToString());
        //            // objDBHand = new DataHandling(getQuery, "Group");
        //            gcMapComponent.DataSource = new DataView(dtComp);
        //            gcMapComponent.RefreshDataSource();
        //            SelectMappedComponents(dt);
        //            //SelectMappedGroups(dtComp);
        //            //string[,] TextBoxProperty = new string[,]{
        //            //                                        {"SELECT","FALSE","FALSE","FALSE","FALSE","1","35","BOOLEAN","0","FALSE"},
        //            //                                        {"GROUPNAME","FALSE","TRUE","TRUE","TRUE","100","200","STRING","","FALSE"},
        //            //                                        {"GROUPID","FALSE","TRUE","TRUE","TRUE","25","0","INT","0","FALSE"}
        //            //                                         };
        //            //dgGroups.CreateGridTextBox("Group", TextBoxProperty); ;
        //        }
        //        //iPreviousRow = dgComponents.CurrentRowIndex;

        //    }
        //    catch (Exception ex)
        //    {
        //        XtraMessageBox.Show(ex.ToString(), MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION);
        //    }
        //}

        private void LoadMappedComponents()
        {
            string itemValues = string.Empty;
            DataTable dtComponent = gcMapComponent.DataSource as DataTable;
            if (dtComponents != null)
            {
                using (clsPrComponent objcompallocate = new clsPrComponent())
                {
                    getRowId = glkpGroups.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkpGroups.EditValue.ToString()) : 0;

                    dtMappedComponents = objcompallocate.FetchMapComponent(long.Parse(clsGeneral.PAYROLL_ID.ToString()), long.Parse(getRowId.ToString()));

                    if (dtMappedComponents != null && dtMappedComponents.Rows.Count > 0)
                    {
                        foreach (DataRow drMappedComponent in dtMappedComponents.Rows)
                        {
                            foreach (DataRow dr in dtComponent.Rows)
                            {
                                if (commem.NumberSet.ToInteger(dr["COMPONENTID"].ToString()) == commem.NumberSet.ToInteger(drMappedComponent["COMPONENTID"].ToString()))
                                {
                                    dr["SELECT"] = drMappedComponent["SELECT"];
                                }
                            }
                        }
                    }
                    else
                    {
                        foreach (DataRow drcomp in dtComponents.Rows)
                        {
                            drcomp["SELECT"] = 0;
                        }
                        gcMapComponent.DataSource = dtComponents;
                    }
                }
            }
        }

        private void SelectMappedComponents(DataTable Components)
        {

            if (Components != null && Components.Rows.Count > 0)
            {
                for (int i = 0; i < Components.Rows.Count; i++)
                {
                    if (Components.Rows[i]["Select"].ToString() == "1")
                        gvMapComponent.SelectRow(i);
                    else
                        gvMapComponent.UnselectRow(i);
                }
            }
        }

        private string SelectMappedGroups(DataTable Groups)
        {
            string GroupIds = string.Empty;
            for (int i = 0; i < Groups.Rows.Count; i++)
            {
                if (gvMapComponent.IsRowSelected(i))
                    GroupIds += Groups.Rows[i]["groupid"].ToString() + ",";
            }
            return GroupIds = GroupIds.TrimEnd(',');
        }

        #endregion

        #region Events
        private void frmMapComponenttoGroups_Load(object sender, EventArgs e)
        {
            fillComponents();
            LoadGroupList();
            LoadMappedComponents();
        }
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateMapComponentDetails())
            {
                if (clsGeneral.checkPayrollexists())
                {
                    DataTable dtable = (gcMapComponent.DataSource) as DataTable;
                    int igetId = Convert.ToInt32(clsGeneral.PAYROLL_ID);
                    int iGroupId = glkpGroups.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkpGroups.EditValue.ToString()) : 0;
                    int iCompId = 0;
                    int compOrder = 1;
                    DataTable dt = null;
                    int IsCompSel = 0;
                    try
                    {
                        if (dtable != null && dtable.Rows.Count > 0)
                        {
                            for (int i = 0; i < dtable.Rows.Count; i++)
                            {
                                iCompId = UtilityMember.NumberSet.ToInteger(dtable.Rows[i]["COMPONENTID"].ToString());

                                if (IsRowChecked(dtable, iCompId))
                                {
                                    dt = objAllocateComp.getCompDetails(iCompId);
                                    try
                                    {
                                        objComponent.SaveGroupComponent(igetId, iCompId, dt, iGroupId.ToString(), compOrder);
                                        compOrder++;
                                        IsCompSel++;
                                    }
                                    catch
                                    {
                                        //XtraMessageBox.Show("Component Not Mapped to a Group", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.MapComponentstoGroups.PAYROLL_COMP_MAP_INFO));
                                    }
                                }
                                else
                                {
                                    dt = objAllocateComp.getCompDetails(iCompId);
                                    try
                                    {
                                        if (objAllocateComp.InsertSelect(igetId, iCompId, iGroupId) != 0)
                                            objAllocateComp.DeleteInserted(igetId, iCompId, iGroupId);
                                    }
                                    catch
                                    {
                                        //XtraMessageBox.Show("Component is not selected", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.MapComponentstoGroups.PAYROLL_COMP_SELECT_INFO));
                                    }
                                }
                            }
                            if (IsCompSel > 0)
                            {
                                //XtraMessageBox.Show("Component Allocated Successfully", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                //this.ShowSuccessMessage("Components mapped successfuly");
                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Payroll.MapComponentstoGroups.PAYROLL_COMPONENT_MAP_INFO));
                            }
                            else
                            {
                                //this.ShowSuccessMessage("Components unmapped successfuly");
                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Payroll.MapComponentstoGroups.PAYROLL_COMPONENT_UNMAP_INFO));
                            }

                        }
                        else
                        {
                            //XtraMessageBox.Show("No record is available in the grid", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_GRID_EMTPY_INFO));
                        }
                       
                        //if (ValidateOrderMapComponentDetails())
                        //{
                        //if (string.IsNullOrEmpty(glkpGroup.Text) == null)
                        //{
                        //    XtraMessageBox.Show("Group is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //}
                        //else if (gvComponent.RowCount == 0 & gvComponent.RowCount != null)
                        //{
                        //    XtraMessageBox.Show("No component is here to save the order, add components and proceed..", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    //return;
                        //}
                        // if (borderChanged[SelectedGroupIndex])
                        //  {
                        //   this.dtConst = gcComponent.DataSource as DataTable;
                        //   if (objPrComponent.SaveAssignedComponent(clsGeneral.PAYROLL_ID, long.Parse(glkpPayrollGroups.EditValue.ToString()), dtConst))
                        //       XtraMessageBox.Show("Record order is saved.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        //    borderChanged[SelectedGroupIndex] = false;
                        //  }
                        //  if (!FirstTimeOrder[SelectedGroupIndex])
                        //  {
                        this.dtConst = gcComponent.DataSource as DataTable;
                        if (objComponent.SaveAssignedComponent(clsGeneral.PAYROLL_ID, long.Parse(glkpGroups.EditValue.ToString()), dtConst))
                            //XtraMessageBox.Show("Record order is saved.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                            //this.ShowSuccessMessage("Components order is saved");
                            FirstTimeOrder[SelectedGroupIndex] = true;
                        LoadComponentDetails();
                        // }
                        //}
                    }

                    catch
                    {
                        //XtraMessageBox.Show("Component is not selected", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.MapComponentstoGroups.PAYROLL_COMP_SELECT_INFO));
                    }

                }
                else
                {
                    fillComponents();
                }
            }
        }

        private void glkpGroups_EditValueChanged_1(object sender, EventArgs e)
        {
            LoadMappedComponents();
            SelectedGroupIndex = glkpGroups.Properties.GetIndexByKeyValue(glkpGroups.EditValue);
            payrollcomponentid = UtilityMember.NumberSet.ToInteger(glkpGroups.EditValue.ToString());
            LoadComponentDetails();
            try
            {
                if (payrollcomponentid != 0)
                {
                    if (gvComponent.RowCount == 0)
                    {
                        //btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled = btnIncrease.Visible = btnDecrease.Visible = false;
                    }
                    else
                    {
                        // btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled = btnIncrease.Visible = btnDecrease.Visible = true;
                    }
                }
                else
                {
                    //btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled = btnIncrease.Visible = btnDecrease.Visible = false;
                }
                //SetArrowsState();
            }
            catch (Exception ex)
            {
                //btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled = btnIncrease.Visible = btnDecrease.Visible = false;
            }
        }

        private bool IsRowChecked(DataTable dtcomponents, int rowid)
        {
            bool isselect = false;
            DataView dv = dtcomponents.AsDataView();
            dv.RowFilter = "COMPONENTID=" + rowid + "";
            dtcomponents = dv.ToTable();

            if (dtcomponents != null && dtcomponents.Rows.Count > 0)
            {
                if (dtcomponents.Rows[0]["SELECT"].ToString() == "1")
                {
                    isselect = true;
                }
            }
            return isselect;
        }


        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvMapComponent.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvMapComponent, colComponentName);
            }
        }

        private void gvMapComponent_RowCountChanged(object sender, EventArgs e)
        {
            lblRowCount.Text = gvMapComponent.RowCount.ToString();
        }

        private void frmMapComponenttoGroups_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        //private void chkFromLocationSelectAll_CheckedChanged(object sender, EventArgs e)
        //{
        //    DataTable dtMapComponent = (DataTable)gcMapComponent.DataSource;
        //    if (dtMapComponent != null && dtMapComponent.Rows.Count > 0)
        //    {
        //        foreach (DataRow dr in dtMapComponent.Rows)
        //        {
        //            dr[SELECT_COL] = chkSelectAll.Checked;
        //        }
        //        gcMapComponent.DataSource = dtMapComponent;
        //    }
        //}

        private DataTable MakeSelectColumnZero(DataTable dtCheckedColumnSource)
        {
            if (dtCheckedColumnSource != null)
            {
                int i = 0;
                if (dtCheckedColumnSource.Columns.Contains(SELECT_COL))
                {
                    foreach (DataRow dr in dtCheckedColumnSource.Rows)
                    {
                        dtCheckedColumnSource.Rows[i++][SELECT_COL] = 0;
                    }
                }
                else
                    dtCheckedColumnSource = AddColumns(dtCheckedColumnSource);
            }
            return dtCheckedColumnSource;
        }

        private DataTable AddColumns(DataTable NewColumns)
        {
            if (!NewColumns.Columns.Contains(SELECT_COL))
                NewColumns.Columns.Add(SELECT_COL, typeof(Int32));
            return NewColumns;
        }

        private void chkSelectAll_CheckedChanged(object sender, EventArgs e)
        {
            try
            {

                DataTable dtComponents = gcMapComponent.DataSource as DataTable;
                foreach (DataRow drproject in dtComponents.Rows)
                {
                    if (chkSelectAll.Checked)
                    {
                        drproject["SELECT"] = 1;
                    }
                    else
                    {
                        drproject["SELECT"] = 0;

                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message, true);
            }
            finally { }

        }

        private void rchkEdit_CheckedChanged(object sender, EventArgs e)
        {
            //CheckEdit chkd = (CheckEdit)sender;
            //if (!chkd.Checked)
            //{
            //    chkd.Checked = true;
            //}
            //else
            //{
            //    chkd.Checked = false;
            //}
        }
        #endregion

        #region Order component Methods
        public void LoadComponentDetails()
        {
            DataTable dTDetails = objPayrollComp.getComponentDetails(PayrollComponentId);
            gcComponent.DataSource = dTDetails;
        }

        private void SetArrowsState()
        {
            if (gvComponent.RowCount >= 2)
                btnInc.Visible = true;
            else
                btnDec.Visible = false;
            if (gvComponent.RowCount >= 2)
            {
                btnDec.Enabled = btnInc.Enabled = true;
            }
            else
            {
                btnDec.Enabled = btnInc.Enabled = false;
            }
            if (gvComponent.RowCount == 1)
                btnDec.Enabled = false;
        }
        //private void LoadPayrollGroups()
        //{
        //    try
        //    {
        //        resultArgs = staff.FetchGroups("Group");
        //        glkpGroups.Properties.DataSource = null;
        //        if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
        //        {
        //            borderChanged = new bool[resultArgs.DataSource.Table.Rows.Count];
        //            FirstTimeOrder = new bool[resultArgs.DataSource.Table.Rows.Count];
        //            //this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpPayrollGroups, resultArgs.DataSource.Table, "Group Name", "GROUP ID");
        //            //glkpPayrollGroups.EditValue = glkpPayrollGroups.Properties.GetKeyValue(0);

        //            using (CommonMethod SelectAll = new CommonMethod())
        //            {
        //                //DataTable dtProject = SelectAll.AddHeaderColumn(resultArgs.DataSource.Table, "GROUP ID", "Group Name");
        //                UtilityMember.ComboSet.BindGridLookUpCombo(glkpGroups, resultArgs.DataSource.Table, "Group Name", "GROUP ID");
        //                glkpGroups.EditValue = glkpGroups.Properties.GetKeyValue(0);
        //            }

        //            DataTable dtComponents = objPayrollComp.getComponentDetails(PayrollComponentId);
        //            try
        //            {
        //                if (gvComponent.RowCount > 0)
        //                {
        //                   btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled = true;
        //                    SetArrowsState();
        //                }
        //            }
        //            catch (Exception ex)
        //            {
        //                btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled;
        //                SetArrowsState();
        //            }
        //        }
        //        else
        //        {
        //            btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled;
        //        }

        //    }
        //    catch (Exception Ex)
        //    {
        //        MessageRender.ShowMessage(Ex.Message);
        //    }
        //    finally { }
        //}

        public bool ValidateOrderMapComponentDetails()
        {
            bool isMapComp = true;
            //if (string.IsNullOrEmpty(glkpGroups.Text.Trim()))
            //{
            //    //ShowMessageBox("Staff Name is empty");
            //    XtraMessageBox.Show("Group is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    //this.SetBorderColor(glkpGroup);
            //    glkpGroups.Focus();
            //    return false;
            //    //XtraMessageBox.Show("Group is empty", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            if (gvComponent.RowCount <= 0)
            {
                //XtraMessageBox.Show("No record is available to order.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.MapComponentstoGroups.PAYROLL_COMPONENT_ORDER_INFO));
                return false;
                //return;
            }
            else
            {
                glkpGroups.Focus();
            }
            return isMapComp;
        }

        #endregion

        #region Order ComponentsEvents
        private void gvComponent_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordcount.Text = gvComponent.RowCount.ToString();
        }

        //private void glkpGroup_EditValueChanged(object sender, EventArgs e)
        //{
        //    SelectedGroupIndex = glkpGroups.Properties.GetIndexByKeyValue(glkpGroups.EditValue);
        //    payrollcomponentid = UtilityMember.NumberSet.ToInteger(glkpGroups.EditValue.ToString());
        //    LoadComponentDetails();
        //    try
        //    {
        //        if (payrollcomponentid != 0)
        //        {
        //            if (gvComponent.RowCount == 0)
        //            {
        //                //btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled = btnIncrease.Visible = btnDecrease.Visible = false;
        //            }
        //            else
        //            {
        //                // btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled = btnIncrease.Visible = btnDecrease.Visible = true;
        //            }
        //        }
        //        else
        //        {
        //            //btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled = btnIncrease.Visible = btnDecrease.Visible = false;
        //        }
        //        SetArrowsState();
        //    }
        //    catch (Exception ex)
        //    {
        //        //btnSave.Enabled = btnIncrease.Enabled = btnDecrease.Enabled = btnIncrease.Visible = btnDecrease.Visible = false;
        //    }
        //}

        private void btnIncrease_Click(object sender, EventArgs e)
        {
            if (ValidateOrderMapComponentDetails())
            {
                DevExpress.XtraGrid.Views.Grid.GridView view = gvComponent;
                view.GridControl.Focus();
                int index = view.FocusedRowHandle;
                dtConst = new DataTable("Staff");
                //ds1.Tables.Add(dtConst);
                int columnCount = gvComponent.Columns.Count;
                if (index <= 0) return;
                DataRow row1 = view.GetDataRow(index);
                DataRow row2 = view.GetDataRow(index - 1);

                object[] getRowData = row1.ItemArray;
                object[] getPrevRowData = row2.ItemArray;
                DataColumn dc;
                for (int j = 0; j < columnCount; j++)
                {
                    dc = new DataColumn(gvComponent.Columns[j].FieldName); ; // DataColumn(gvComponent.Columns[j].Caption);
                    //dc.DataType =gvComponent.Columns[j].ColumnType;
                    dtConst.Columns.Add(dc);
                }

                for (int i = 0; i < gvComponent.RowCount; i++)
                {
                    DataTable dtchangetable = gcComponent.DataSource as DataTable;
                    if (dtchangetable.Rows[i][1] != getPrevRowData[1])
                    {
                        DataRow dr = dtConst.NewRow();
                        dtConst.Rows.Add(dr);
                        for (int j = 0; j < columnCount; j++)
                            dtConst.Rows[i][j] = dtchangetable.Rows[i][j];
                    }
                    else
                    {
                        DataRow dr = dtConst.NewRow();
                        dtConst.Rows.Add(dr);
                        for (int j = 0; j < columnCount; j++)
                            dtConst.Rows[i][j] = getRowData[j];
                        dr = dtConst.NewRow();
                        dtConst.Rows.Add(dr);
                        i++;
                        iSetFocus = i - 1;
                        for (int j = 0; j < columnCount; j++)
                            dtConst.Rows[i][j] = getPrevRowData[j];
                    }
                }
                //DataTable dtStaff = dtConst;
                gcComponent.DataSource = dtConst;
                gvComponent.FocusedRowHandle = iSetFocus;
                //bOrderChanged[iSelectedGroupIndex] = true;
            }
        }

        private void btnDec_Click(object sender, EventArgs e)
        {
            if (ValidateOrderMapComponentDetails())
            {
                DevExpress.XtraGrid.Views.Grid.GridView view = gvComponent;
                view.GridControl.Focus();
                int index = view.FocusedRowHandle;
                if (index >= view.DataRowCount - 1) return;

                DataRow row1 = view.GetDataRow(index);
                DataRow row2 = view.GetDataRow(index + 1);

                ds1 = new DataSet("StaffData");
                dtConst = new DataTable("Staff");
                //ds1.Tables.Add(dtConst);
                int columnCount = gvComponent.Columns.Count;
                object[] getRowData = row1.ItemArray;
                object[] getNextRowData = row2.ItemArray;
                DataColumn dc;
                for (int j = 0; j < columnCount; j++)
                {
                    dc = new DataColumn(gvComponent.Columns[j].FieldName); // DataColumn(gvComponent.Columns[j].Caption);
                    // dc.DataType = gvComponent.Columns[j].ColumnType;
                    dtConst.Columns.Add(dc);
                }
                for (int i = 0; i < gvComponent.RowCount; i++)
                {
                    DataTable dtchangetable = gcComponent.DataSource as DataTable;
                    if (dtchangetable.Rows[i][1] != getRowData[1])
                    {
                        DataRow dr = dtConst.NewRow();
                        dtConst.Rows.Add(dr);
                        for (int j = 0; j < columnCount; j++)
                            dtConst.Rows[i][j] = dtchangetable.Rows[i][j];
                    }
                    else
                    {
                        DataRow dr = dtConst.NewRow();
                        dtConst.Rows.Add(dr);
                        for (int j = 0; j < columnCount; j++)
                            dtConst.Rows[i][j] = getNextRowData[j];
                        dr = dtConst.NewRow();
                        dtConst.Rows.Add(dr);
                        i++;
                        iSetFocus = i;
                        for (int j = 0; j < columnCount; j++)
                            dtConst.Rows[i][j] = getRowData[j];
                    }
                }

                //DataTable dtStaff = dtConst;
                gcComponent.DataSource = dtConst;
                gvComponent.FocusedRowHandle = iSetFocus;
                //   bOrderChanged[iSelectedGroupIndex] = true;
            }
        }
        #endregion
    }
}
