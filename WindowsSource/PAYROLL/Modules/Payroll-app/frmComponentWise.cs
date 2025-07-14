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


namespace PAYROLL.Modules
{
    public partial class frmComponentWise : DevExpress.XtraEditors.XtraForm
    {
        #region Variable Declaration
        //private System.Windows.Forms.Panel pnlTitle;
        private System.Windows.Forms.Label lblMonthYear;
        //private System.Windows.Forms.Label lblUser;
        //private System.Windows.Forms.Label lblComponent;
        //private System.Windows.Forms.Label lblTitle;
        //private System.Windows.Forms.Label label1;
        //private System.Windows.Forms.Label lblPayrollGroups;
        //private System.Windows.Forms.Panel panel1;
        //private System.Windows.Forms.Splitter splitter1;
        //private UserControl.clsWorkSheet dgGroups;
        private clsAllocateComponent objAllocateComp = new clsAllocateComponent();
        //private DataHandling objDBHand = new DataHandling();
        private DataTable dt = new DataTable();
        private DataTable dtable = new DataTable();
        private DataView dvComponentAllocate = new DataView();
        private clsEvalExpr objEvalExpr = new clsEvalExpr();
        private clsPrComponent objComponent = new clsPrComponent();
        private long igetId = 0, iCompId;
        private int getRowId = 0;
        private int iGroupId;
        //private int getvalue = 0, getDvalue = 0;
        //private int compIndex;
        //private clsWorkSheet dgComponents;
        private int iPreviousRow = 0;
        private DataTable getds;
        //variable to fetch the constructor arguments..
        private UserControl.ucGrid ucGetGroup;
        private Label lblGroup = null;
        private DevExpress.XtraGrid.GridControl gcComponentDetails;
        private ListBox lstGetGroup = null;
        private SimpleButton btnProcess = null;
        private SimpleButton btnSave = null;
        private SimpleButton btnIncrease = null;
        private SimpleButton btnDecrease = null;
        private Panel pnlBtns;
        public event EventHandler UpdateHeld;
        private clsPayrollComponent objPayrollComp = new clsPayrollComponent();
        ApplicationSchema.PRCOMPONENTDataTable dtComptbl = null;
        ResultArgs resultArgs = new ResultArgs();
        #endregion

        #region Constructors

        public frmComponentWise()
        {
            InitializeComponent();
        }
        public frmComponentWise(SimpleButton btn, SimpleButton btn1, DevExpress.XtraGrid.GridControl component, SimpleButton btnInc, SimpleButton btnDec, DataTable ds)
		{
			InitializeComponent();
            gcComponentDetails = component;
			this.btnProcess  = btn;
			this.btnSave     = btn1;
			this.btnIncrease = btnInc;
			this.btnDecrease = btnDec;
			this.getds = ds;
		}

        #endregion

        #region Events
        
        private void dgComponents_Click(object sender, EventArgs e)
        {
            try
            {
                dgComponents.SelectionBackColor = Color.DarkBlue;
                dgComponents.Select(dgComponents.CurrentRowIndex);
            }
            catch { }

        }

        //When a Group is Selected, To make entry in DB

        private void dgGroups_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            dtable = ((DataView)dgGroups.DataSource).Table;
            igetId = Convert.ToInt32(clsGeneral.PAYROLL_ID);
            iCompId = Convert.ToInt32(dgComponents[iPreviousRow, 0]);

            if (dtable.Rows[dgGroups.CurrentRowIndex][0].ToString() == "1")
            {
                iGroupId = Convert.ToInt32(dtable.Rows[dgGroups.CurrentRowIndex][2]);

                //if(VerifyCircularReference(igetId, iGroupId,iCompId))return;

                dt = objAllocateComp.getCompDetails(iCompId);

                try
                {
                    objComponent.SaveGroupComponent(igetId, iCompId, dt, iGroupId.ToString(),0);
                    if (UpdateHeld != null)
                        UpdateHeld(this, e);
                }
                catch
                {
                    XtraMessageBox.Show("Component Not Mapped to a Group", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            else if (dtable.Rows[dgGroups.CurrentRowIndex][0].ToString() == "0")
            {
                iGroupId = Convert.ToInt32(dtable.Rows[dgGroups.CurrentRowIndex][2]);
                dt = objAllocateComp.getCompDetails(iCompId);
                try
                {
                    if (objAllocateComp.InsertSelect(igetId, iCompId, iGroupId) != 0)
                        objAllocateComp.DeleteInserted(igetId, iCompId, iGroupId);
                }
                catch
                {
                    XtraMessageBox.Show(" Group Not DeSeleted", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }


        private void frmComponentWise_Load(object sender, EventArgs e)
        {
          //  lblMonthYear.Text = clsGeneral.PAYROLL_MONTH;
             fillGrids();
            dgGroups.AllowAdd = false;
            SelectGroup(sender, e);
        }

        //To Close the ComponentWise Details Form
        private void btnClosePayroll_Click(object sender, EventArgs e)
        {

            try
            {
                igetId = long.Parse(lstGetGroup.SelectedValue.ToString());
                lblGroup.Text = lstGetGroup.Text.ToString();
                ucGetGroup.HideFilter = true;
                ucGetGroup.ShowGrid(objPayrollComp.getComponentDetails(igetId), "Component");
                this.getds = objPayrollComp.getComponentDetails(igetId);
                try
                {
                    if (ucGetGroup.RecordCount >= 2)
                    {
                        btnIncrease.Visible = true;
                        btnDecrease.Visible = true;
                        pnlBtns.Visible = true;
                    }
                    else
                    {
                        btnIncrease.Visible = false;
                        btnDecrease.Visible = false;
                        pnlBtns.Visible = false;
                    }
                    if (ucGetGroup.RecordCount == 0)
                    {
                        btnProcess.Enabled = false;
                        btnSave.Enabled = false;
                        btnIncrease.Enabled = false;
                        btnDecrease.Enabled = false;
                        this.Close();
                    }
                    else
                    {
                        btnProcess.Enabled = true;
                        btnSave.Enabled = true;
                        btnProcess.Enabled = true;
                        btnProcess.Enabled = true;
                        this.Close();
                    }
                }
                catch (Exception ex)
                {
                    btnProcess.Enabled = false;
                    btnSave.Enabled = false;
                    btnIncrease.Enabled = false;
                    btnDecrease.Enabled = false;
                    btnIncrease.Visible = false;
                    btnDecrease.Visible = false;
                    pnlBtns.Visible = false;
                    this.Close();

                }
            }
            catch { this.Close(); }
        }

        //To Select All the Items from dgGroups
        private void mnuSelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataView dv = (DataView)dgGroups.DataSource;
                if (dv.Count < 0)
                {
                    XtraMessageBox.Show("Groups are not Available", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                int currentRow = 0;
                for (int i = 0; i < dv.Count; i++)
                {
                    currentRow = i;
                    dgGroups.CurrentRowIndex = currentRow;
                    dv.Table.Rows[currentRow][0] = "1"; //calls dggroups_columnchanged automatically..
                    dgGroups.Select(currentRow);
                }
            }
            catch { }

        }

        //To Select a Single Item from dgGroups
        private void mnuSelectToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            try
            {
                DataView dv = (DataView)dgGroups.DataSource;
                if (dv.Count < 0)
                {
                    XtraMessageBox.Show("Groups are not Available", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                int currentRow = dgGroups.CurrentRowIndex;
                dv.Table.Rows[currentRow][0] = "1"; //calls dggroups_columnchanged automatically..
                dgGroups.Select(currentRow);
            }
            catch { }
        }

        //To DeSelect a Single Item from dgGroups
        private void mnudeSelectToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataView dv = (DataView)dgGroups.DataSource;
                if (dv.Count < 0)
                {
                    XtraMessageBox.Show("Groups are not Available", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                int currentRow = dgGroups.CurrentRowIndex;
                dv.Table.Rows[currentRow][0] = "0"; //calls dggroups_columnchanged automatically..
                dgGroups.Select(currentRow);
            }
            catch { }
        }

        //To DeSelect All the Items from dgGroups
        private void deSelectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                DataView dv = (DataView)dgGroups.DataSource;
                if (dv.Count < 0)
                {
                    XtraMessageBox.Show("Groups are not Available", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }
                int currentRow = 0;
                for (int i = 0; i < dv.Count; i++)
                {
                    currentRow = i;
                    dgGroups.CurrentRowIndex = currentRow;
                    dv.Table.Rows[currentRow][0] = "0"; //calls dggroups_columnchanged automatically..
                    dgGroups.Select(currentRow);
                }
            }
            catch { }
        }

        //When a Components is selected, to View already Selected Group(s)
            private void dgComponents_CurrentCellChanged(object sender, EventArgs e)
        {
            try
            {
                if (dgGroups.getSourceTable != null)
                    dgGroups.CurrentCell = dgGroups.getSourceTable.Rows.Count > 0 ? new DataGridCell(0, 0) : new DataGridCell(dgGroups.getSourceTable.Rows.Count, 0);
                if (dgComponents.getSourceTable != null)
                    getRowId = Convert.ToInt32(dgComponents[dgComponents.CurrentRowIndex, 0]);
                //MessageBox.Show(compIndex.ToString());
                object getQuery = objComponent.getPayrollComponentAllocateQry(clsPayrollConstants.PAYROLL_COMP_CHANGE);
                DataTable dtGetGroup = null;
                //getQuery = getQuery.Replace("<compid>", getRowId.ToString());
                //MessageBox.Show(this.dgComponents[dgComponents.CurrentRowIndex,1].ToString());
                //getQuery = getQuery.Replace("<payrollid>", clsGeneral.PAYROLL_ID.ToString());
                /* using (DataManager dataManager = new DataManager(getQuery, "Group"))
                 {
                     dataManager.DataCommandArgs.IsDirectReplaceParameter = true;
                     dataManager.Parameters.Add(dtComptbl.PAYROLLIDColumn, clsGeneral.PAYROLL_ID.ToString());
                     dataManager.Parameters.Add(dtComptbl.COMPONENTIDColumn, getRowId.ToString());*/
                clsPrComponent objcompallocate = new clsPrComponent();
                DataTable dt = objcompallocate.CompAllocateQuery(long.Parse(clsGeneral.PAYROLL_ID.ToString()), long.Parse(getRowId.ToString()));
                if (dt.Rows.Count > 0)
                    dtGetGroup = dt;
                // }
                // DataHandling objDBHand = new DataHandling(getQuery, "Group");
                if (dtGetGroup != null && dtGetGroup.Rows.Count > 0)
                {
                    dgGroups.DataSource = new DataView(dtGetGroup);
                    string[,] TextBoxProperty = new string[,]{
                                                            {"SELECT","FALSE","FALSE","FALSE","FALSE","1","35","BOOLEAN","0","FALSE"},
                                                            {"GROUPNAME","FALSE","TRUE","TRUE","TRUE","100","200","STRING","","FALSE"},
                                                            {"GROUPID","FALSE","TRUE","TRUE","TRUE","25","0","INT","0","FALSE"}
                                                             };
                    dgGroups.CreateGridTextBox("Group", TextBoxProperty);
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
                    dgGroups.DataSource = new DataView(dtGRoup);
                    string[,] TextBoxProperty = new string[,]{
                                                            {"SELECT","FALSE","FALSE","FALSE","FALSE","1","35","BOOLEAN","0","FALSE"},
                                                            {"GROUPNAME","FALSE","TRUE","TRUE","TRUE","100","200","STRING","","FALSE"},
                                                            {"GROUPID","FALSE","TRUE","TRUE","TRUE","25","0","INT","0","FALSE"}
                                                             };
                    dgGroups.CreateGridTextBox("Group", TextBoxProperty);;
                }
                iPreviousRow = dgComponents.CurrentRowIndex;
            }
            catch(Exception ex)
            {
                XtraMessageBox.Show(ex.ToString(), MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION);
            }
        }


        #endregion

        #region Methods

        private void fillGrids()
        {
            DataTable dtComponent = null;
            object getQr = objComponent.getPayrollComponentAllocateQry(clsPayrollConstants.PAYROLL_COMPONENT_LIST);
            using (DataManager dataManager = new DataManager(getQr, "Component"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                    dtComponent = resultArgs.DataSource.Table;
            }
            // objDBHand = new DataHandling(getQr, "Component");
            //MessageBox.Show(objDBHand.getRecordCount().ToString()); //shows no of Components..
            if (dtComponent == null || dtComponent.Rows.Count <= 0)
            {
                dgComponents.Enabled = false;
                dgGroups.Enabled = false;
                XtraMessageBox.Show("No Records found for Components..", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                dgComponents.DataSource = new DataView(dtComponent);
                string[,] TextBoxProperty1 = new string[,]{
                                                            {"COMPONENTID","FALSE","TRUE","TRUE","TRUE","25","0","INT","0","FALSE"},
                                                            {"COMPONENTNAME","FALSE","TRUE","TRUE","FALSE","100","200","STRING","","FALSE"}
                                                          };
                dgComponents.AllowAdd = dgComponents.AllowDelete = dgComponents.AllowEdit = false;
                dgComponents.CreateGridTextBox("Component", TextBoxProperty1);
            }
            //string getQuery = "Select * from prloanpaid";
            DataTable dtGroup = null;
            object getQuery = objComponent.getPayrollComponentAllocateQry(clsPayrollConstants.PAYROLL_GETGROUP_LIST);
            using (DataManager dataManager = new DataManager(getQuery, "Group"))
            {
                dataManager.DataCommandArgs.ActiveSQLAdapterType = SQLAdapterType.PayrollSQL;
                resultArgs = dataManager.FetchData(DataSource.DataTable);
                if (resultArgs.Success)
                    dtGroup = resultArgs.DataSource.Table;
            }
            //objDBHand = new DataHandling(getQuery, "Group");
            //MessageBox.Show(objDBHand.getRecordCount().ToString()); //shows no of Grades..
            if (dtGroup == null || dtGroup.Rows.Count <= 0)
            {
                dgComponents.Enabled = false;
                dgGroups.Enabled = false;
                XtraMessageBox.Show("No Records found for Grade..", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.Close();
            }
            else
            {
                dgGroups.DataSource = new DataView(dtGroup);
                string[,] TextBoxProperty = new string[,]{
                                                            {"SELECT","FALSE","FALSE","FALSE","FALSE","1","35","BOOLEAN","0","FALSE"},
                                                            {"GROUPNAME","FALSE","TRUE","TRUE","TRUE","100","200","STRING","","FALSE"},
                                                            {"GROUPID","FALSE","TRUE","TRUE","TRUE","25","0","INT","0","FALSE"}
                                                         };
                dgGroups.CreateGridTextBox("Group", TextBoxProperty);
            }
        }

        /// <summary>
        /// When first time the form is loaded, to view the selected groups for the first selected component.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        
        private void SelectGroup(object sender, System.EventArgs e)
        {
            dgComponents_CurrentCellChanged(sender, e);
        }

        #endregion
       
    }
}