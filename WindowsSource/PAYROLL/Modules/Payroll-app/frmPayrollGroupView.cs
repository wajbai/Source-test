using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using PAYROLL;
using Payroll.Model.UIModel;
using Bosco.Utility.Common;
using Bosco.Utility;


namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmPayrollGroupView : frmPayrollBase
    {
        #region VariableDeclartion
        ResultArgs resultArgs = null;
        private int RowIndex = 0;
        CommonMember memset = new CommonMember();
        clsPayrollBase payrollbase = new clsPayrollBase();
        #endregion

        #region Constructors
        public frmPayrollGroupView()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties
        private int GroupId = 0;
        private int groupId
        {
            get
            {
                RowIndex = gvPayrollGroupView.FocusedRowHandle;
                GroupId = gvPayrollGroupView.GetFocusedRowCellValue(colGroupId) != null ? Convert.ToInt32(gvPayrollGroupView.GetFocusedRowCellValue(colGroupId).ToString()) : 0;
                return GroupId;
            }
            set
            {
                GroupId = value;
            }
        }
        #endregion

        #region Events

        /// <summary>
        /// To Add the Payroll Group Details.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ucToolBar1_AddClicked(object sender, EventArgs e)
        {
            ShowGroupAddForm((int)AddNewRow.NewRow);
        }

        /// <summary>
        /// To Fetch the Payroll Group Details form DB Table.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void frmPayrollGroupView_Load(object sender, EventArgs e)
        {
            FetchPayrollGroupDetails();
            SetTitle();
            ApplyUserRights();
        }

        /// <summary>
        /// To Close the Payroll Group Form.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ucToolBar1_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// To Delete the Details.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBar1_DeleteClicked(object sender, EventArgs e)
        {
            if (gvPayrollGroupView.RowCount != 0)
            {
                if (groupId != 0)
                {
                    if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_DELETED_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)//Are you sure to Delete?
                    {
                        DeletePayrollGroup();
                    }
                }
            }
            else
            {
                //XtraMessageBox.Show("No record is available in the grid to delete", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_DELETE_GRID_EMTPY));
            }
        }

        /// <summary>
        /// To Edit the Payroll Group Details.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBar1_DoubleClick(object sender, EventArgs e)
        {
            EditPayrollGroup();
        }

        private void ucToolBar1_EditClicked(object sender, EventArgs e)
        {
            EditPayrollGroup();
        }

        private void ucToolBar1_PrintClicked(object sender, EventArgs e)
        {
            //payrollbase.PrintGridView(gcPayrollGroupview, this.Text, PrintType.DT, gvPayrollGroupView, false);
            payrollbase.PrintGridView(gcPayrollGroupview, this.GetMessage(MessageCatalog.Payroll.PayrollGroup.PAYROLL_GROUP_VIEW_CAPTION), PrintType.DT, gvPayrollGroupView, false);
        }

        /// <summary>
        /// To Refresh the Values After Add,Edit,Delete Operations.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBar1_RefreshClicked(object sender, EventArgs e)
        {
            FetchPayrollGroupDetails();
        }
        #endregion

        #region Methods

        public void OnUpdateHeld(object sender, EventArgs e)
        {
            FetchPayrollGroupDetails();
            gvPayrollGroupView.FocusedRowHandle = RowIndex;
        }
        private void ShowGroupAddForm(int GroupId)
        {
            try
            {
                frmPayrollGroup frmGroup = new frmPayrollGroup(GroupId);
                frmGroup.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmGroup.ShowDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }
            finally { }
        }

        private void EditPayrollGroup()
        {
            if (gvPayrollGroupView.RowCount != 0)
            {
                if (groupId != 0)
                {
                    ShowGroupAddForm(groupId);
                }
                else
                {
                    if (!chkShowFilter.Checked)
                    {
                        //XtraMessageBox.Show("No record is selected to Edit.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.PayrollGroup.PAYROLL_GROUP_NORECORD_SELECT_EDIT_INFO));
                    }
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_EDIT_GRID_EMTPY));
            }
        }

        private void DeletePayrollGroup()
        {
            try
            {
                if (gvPayrollGroupView.RowCount == 0) return;
                clsPayrollGrade objGradeDelete = new clsPayrollGrade();

                objGradeDelete.GradeId = gvPayrollGroupView.GetFocusedRowCellValue(colGroupId) != null ? this.memset.NumberSet.ToInteger(gvPayrollGroupView.GetFocusedRowCellValue(colGroupId).ToString()) : 0;
                long iGroupid = long.Parse(gvPayrollGroupView.GetFocusedRowCellValue(colGroupId).ToString());
                if (iGroupid > 0)
                {
                    ResultArgs resultArgs = objGradeDelete.DeletePayrollGroup(iGroupid);
                    if (resultArgs.Success)
                    {
                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Payroll.PayrollGroup.GROUP_DELETE_SUCCESS));
                        FetchPayrollGroupDetails();
                    }
                    else
                    {
                        //this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.PayrollGroup.GROUP_CAN_NOT_DELETE));
                        this.ShowMessageBox(resultArgs.Message);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void SetTitle()
        {
            this.Text = this.GetMessage(MessageCatalog.Payroll.PayrollGroup.PAYROLL_GROUP_VIEW_CAPTION);
        }


        private void FetchPayrollGroupDetails()
        {
            try
            {
                using (clsPayrollGrade payrollSystem = new clsPayrollGrade())
                {
                    DataTable dtvalue = new DataTable();
                    dtvalue = payrollSystem.getPayrollGradeList();
                    gcPayrollGroupview.DataSource = dtvalue;
                    gcPayrollGroupview.RefreshDataSource();
                    Refresh();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void gvPayrollGroupView_DoubleClick(object sender, EventArgs e)
        {
            EditPayrollGroup();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvPayrollGroupView.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvPayrollGroupView, colPayrollGroupName);
            }
        }

        protected virtual void SetFocusRowFilter(DevExpress.XtraGrid.Views.Grid.GridView gridview, DevExpress.XtraGrid.Columns.GridColumn colGridColumn)
        {
            gridview.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
            gridview.FocusedColumn = colGridColumn;
            gridview.OptionsFind.AllowFindPanel = false;
            gridview.ShowEditor();
        }

        private void gvPayrollGroupView_RowCountChanged(object sender, EventArgs e)
        {
            lblRowCount.Text = gvPayrollGroupView.RowCount.ToString();
        }

        private void frmPayrollGroupView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void frmPayrollGroupView_EnterClicked(object sender, EventArgs e)
        {
            EditPayrollGroup();
        }

        private void ApplyUserRights()
        {
            this.enumUserRights.Add(PayrollGroup.CreatePayrollGroup);
            this.enumUserRights.Add(PayrollGroup.EditPayrollGroup);
            this.enumUserRights.Add(PayrollGroup.DeletePayrollGroup);
            this.enumUserRights.Add(PayrollGroup.ViewPayrollGroup);
            this.ApplyUserRights(ucToolBar1, enumUserRights, (int)Menus.PayrollGroup);
        }
    }
}
        #endregion