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
using Bosco.DAO.Data;
using Payroll.DAO.Schema;
using Bosco.Utility;

namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmComponenttoGroupWise : DevExpress.XtraEditors.XtraForm
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

        #endregion

        #region Constructors
        public frmComponenttoGroupWise()
        {
            InitializeComponent();
        }
        public frmComponenttoGroupWise(SimpleButton btn, SimpleButton btn1, DevExpress.XtraGrid.GridControl component, SimpleButton btnInc, SimpleButton btnDec, DataTable ds)
        {
            InitializeComponent();
            gcComponentDetails = component;
            this.btnProcess = btn;
            this.btnsSave = btn1;
            this.btnIncrease = btnInc;
            this.btnDecrease = btnDec;
            this.getds = ds;
        }
        #endregion

        #region Properties

        #endregion

        #region Events
        private void frmComponenttoGroupWise_Load(object sender, EventArgs e)
        {
            LoadGroupList();
            fillComponents();

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion


        #region Methods
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
                        //dtGradeList.Columns["GROUP ID"].ColumnMapping = MappingType.Hidden;
                        //glkpComponent.Properties.DataSource = dtGradeList;
                        //glkpComponent.Properties.ValueMember = "GROUP ID";
                        //glkpComponent.Properties.DisplayMember = "Group Name";
                        gcGroups.DataSource = dtGradeList;
                        gcGroups.RefreshDataSource();

                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }
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
                    DataTable dtComponentList = resultArgs.DataSource.Table;
                    dtComponentList.Columns["COMPONENTID"].ColumnMapping = MappingType.Hidden;
                    glkpComponent.Properties.DataSource = dtComponentList;
                    glkpComponent.Properties.ValueMember = "COMPONENTID";
                    glkpComponent.Properties.DisplayMember = "Component Name";
                    glkpComponent.EditValue = glkpComponent.Properties.GetKeyValue(0);
                }
                else
                {
                    gcGroups.DataSource = null;
                    gcGroups.RefreshDataSource();
                }

            }
            // objDBHand = new DataHandling(getQr, "Component");
            //MessageBox.Show(objDBHand.getRecordCount().ToString()); //shows no of Components..
            //if (dtComponent == null || dtComponent.Rows.Count <= 0)
            //{
            //    dgComponents.Enabled = false;
            //    dgGroups.Enabled = false;
            //    MessageBox.Show("No Records found for Components..", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //}
            //else
            //{
            //    dgComponents.DataSource = new DataView(dtComponent);
            //    string[,] TextBoxProperty1 = new string[,]{
            //                                                {"COMPONENTID","FALSE","TRUE","TRUE","TRUE","25","0","INT","0","FALSE"},
            //                                                {"COMPONENTNAME","FALSE","TRUE","TRUE","FALSE","100","200","STRING","","FALSE"}
            //                                              };
            //    dgComponents.AllowAdd = dgComponents.AllowDelete = dgComponents.AllowEdit = false;
            //    dgComponents.CreateGridTextBox("Component", TextBoxProperty1);
            //}
            ////string getQuery = "Select * from prloanpaid";
            //DataTable dtGroup = null;
            //object getQuery = objComponent.getPayrollComponentAllocateQry(clsPayrollConstants.PAYROLL_GETGROUP_LIST);
            //using (DataManager dataManager = new DataManager(getQuery, "Group"))
            //{
            //    resultArgs = dataManager.FetchData(DataSource.DataTable);
            //    if (resultArgs.Success)
            //        dtGroup = resultArgs.DataSource.Table;
            //}
            ////objDBHand = new DataHandling(getQuery, "Group");
            ////MessageBox.Show(objDBHand.getRecordCount().ToString()); //shows no of Grades..
            //if (dtGroup == null || dtGroup.Rows.Count <= 0)
            //{
            //    dgComponents.Enabled = false;
            //    dgGroups.Enabled = false;
            //    MessageBox.Show("No Records found for Grade..", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
            //    this.Close();
            //}
            //else
            //{
            //    dgGroups.DataSource = new DataView(dtGroup);
            //    string[,] TextBoxProperty = new string[,]{
            //                                                {"SELECT","FALSE","FALSE","FALSE","FALSE","1","35","BOOLEAN","0","FALSE"},
            //                                                {"GROUPNAME","FALSE","TRUE","TRUE","TRUE","100","200","STRING","","FALSE"},
            //                                                {"GROUPID","FALSE","TRUE","TRUE","TRUE","25","0","INT","0","FALSE"}
            //                                             };
            //    dgGroups.CreateGridTextBox("Group", TextBoxProperty);
            //}

        }

        protected virtual void SetFocusRowFilter(DevExpress.XtraGrid.Views.Grid.GridView gridview, DevExpress.XtraGrid.Columns.GridColumn colGridColumn)
        {
            gridview.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
            gridview.FocusedColumn = colGridColumn;
            gridview.OptionsFind.AllowFindPanel = false;
            gridview.ShowEditor();
        }

        private void LoadMappedGroups()
        {
            try
            {

                // getRowId = Convert.ToInt32(glkpComponent.EditValue.ToString());
                getRowId = UtilityMember.NumberSet.ToInteger(glkpComponent.EditValue.ToString());

                object getQuery = objComponent.getPayrollComponentAllocateQry(clsPayrollConstants.PAYROLL_COMP_CHANGE);
                DataTable dtGetGroup = null;

                clsPrComponent objcompallocate = new clsPrComponent();
                DataTable dt = objcompallocate.CompAllocateQuery(long.Parse(clsGeneral.PAYROLL_ID.ToString()), long.Parse(getRowId.ToString()));
                if (dt.Rows.Count > 0)
                    dtGetGroup = dt;

                if (dtGetGroup != null && dtGetGroup.Rows.Count > 0)
                {
                    gcGroups.DataSource = new DataView(dtGetGroup);
                    gcGroups.RefreshDataSource();


                    SelectMappedGroups(dtGetGroup);

                    //string[,] TextBoxProperty = new string[,]{
                    //                                        {"SELECT","FALSE","FALSE","FALSE","FALSE","1","35","BOOLEAN","0","FALSE"},
                    //                                        {"GROUPNAME","FALSE","TRUE","TRUE","TRUE","100","200","STRING","","FALSE"},
                    //                                        {"GROUPID","FALSE","TRUE","TRUE","TRUE","25","0","INT","0","FALSE"}
                    //                                         };
                    //dgGroups.CreateGridTextBox("Group", TextBoxProperty);
                }
                else
                {
                    DataTable dtGRoup = null;
                    getQuery = objComponent.getPayrollComponentAllocateQry(clsPayrollConstants.PAYROLL_GETGROUP_LIST);
                    using (DataManager dataManager = new DataManager(getQuery, "Group"))
                    {
                        dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                        resultArgs = dataManager.FetchData(DataSource.DataTable);
                        if (resultArgs.Success)
                            dtGRoup = resultArgs.DataSource.Table;
                    }
                    //getQuery = getQuery.Replace("<compid>", compIndex.ToString());
                    // objDBHand = new DataHandling(getQuery, "Group");
                    gcGroups.DataSource = new DataView(dtGRoup);
                    gcGroups.RefreshDataSource();
                    SelectMappedGroups(dtGetGroup);
                    //string[,] TextBoxProperty = new string[,]{
                    //                                        {"SELECT","FALSE","FALSE","FALSE","FALSE","1","35","BOOLEAN","0","FALSE"},
                    //                                        {"GROUPNAME","FALSE","TRUE","TRUE","TRUE","100","200","STRING","","FALSE"},
                    //                                        {"GROUPID","FALSE","TRUE","TRUE","TRUE","25","0","INT","0","FALSE"}
                    //                                         };
                    //dgGroups.CreateGridTextBox("Group", TextBoxProperty); ;
                }
                //iPreviousRow = dgComponents.CurrentRowIndex;
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION);
            }
        }
        private void SelectMappedGroups(DataTable Group)
        {

            if (Group != null && Group.Rows.Count > 0)
            {
                for (int i = 0; i < Group.Rows.Count; i++)
                {
                    if (Group.Rows[i]["Select"].ToString() == "1")
                        gvGroups.SelectRow(i);
                    else
                        gvGroups.UnselectRow(i);
                }
            }
        }

        private string GetSelectedGroups(DataTable Groups)
        {
            string GroupIds = string.Empty;
            for (int i = 0; i < Groups.Rows.Count; i++)
            {
                if (gvGroups.IsRowSelected(i))
                    GroupIds += Groups.Rows[i]["groupid"].ToString() + ",";
            }
            return GroupIds = GroupIds.TrimEnd(',');
        }
        #endregion

        private void btnSave_Click(object sender, EventArgs e)
        {
            DataTable dtable = ((DataView)gcGroups.DataSource).Table;
            int igetId = Convert.ToInt32(clsGeneral.PAYROLL_ID);
            int iCompId = UtilityMember.NumberSet.ToInteger(glkpComponent.EditValue.ToString());
            int iGroupId = 0;
            DataTable dt = null;

            try
            {
                for (int i = 0; i < dtable.Rows.Count; i++)
                {
                    //iGroupId = Convert.ToInt32(dtable.Rows[i]["groupid"].ToString());
                    iGroupId = UtilityMember.NumberSet.ToInteger(dtable.Rows[i]["groupid"].ToString());

                    if (gvGroups.IsRowSelected(i))
                    {
                        dt = objAllocateComp.getCompDetails(iCompId);
                        try
                        {
                            objComponent.SaveGroupComponent(igetId, iCompId, dt, iGroupId.ToString(),0 );

                            //if (UpdateHeld != null)
                            //    UpdateHeld(this, e);
                        }
                        catch
                        {
                            XtraMessageBox.Show("Component Not Mapped to a CommonMember UtilityMember = new CommonMember(); Group", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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
                            XtraMessageBox.Show(" Group Not DeSeleted", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        }
                    }
                }
                XtraMessageBox.Show("Component is Allocated to Groups Successfully", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch
            {
                XtraMessageBox.Show(" Group Not DeSeleted", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }

        }

        private void glkpComponent_EditValueChanged(object sender, EventArgs e)
        {
            LoadMappedGroups();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvGroups.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvGroups, colGroupName);
            }
        }

        private void gvGroups_RowCountChanged(object sender, EventArgs e)
        {
            lblRowCount.Text = gvGroups.RowCount.ToString();
        }

        private void gcGroups_Click(object sender, EventArgs e)
        {

        }

    }


}