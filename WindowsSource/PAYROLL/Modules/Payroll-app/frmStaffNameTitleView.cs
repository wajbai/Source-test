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
    public partial class frmStaffNameTitleView : frmPayrollBase
    {
        #region VariableDeclartion
        private int RowIndex = 0;
        CommonMember memset = new CommonMember();
        clsPayrollBase payrollbase = new clsPayrollBase();
        #endregion

        #region Constructors
        public frmStaffNameTitleView()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties
        private int staffnametilte = 0;
        private int StaffNameTitleId
        {
            get
            {
                RowIndex = gvStaffNameTitleView.FocusedRowHandle;
                staffnametilte = gvStaffNameTitleView.GetFocusedRowCellValue(colStaffNameTitleId) != null ? Convert.ToInt32(gvStaffNameTitleView.GetFocusedRowCellValue(colStaffNameTitleId).ToString()) : 0;
                return staffnametilte;
            }
            set
            {
                staffnametilte = value;
            }
        }
        #endregion

        #region Events
        
        /// <summary>
        /// To Fetch the Payroll Group Details form DB Table.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void frmPayrollGroupView_Load(object sender, EventArgs e)
        {
            FetchStaffNameTitleDetails();
            SetTitle();
            ApplyUserRights();
        }


        /// <summary>
        /// To Add the Payroll Group Details.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>

        private void ucToolBar1_AddClicked(object sender, EventArgs e)
        {
            ShowStaffNameTitleAddForm((int)AddNewRow.NewRow);
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
            if (gvStaffNameTitleView.RowCount != 0)
            {
                if (StaffNameTitleId != 0)
                {
                    if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_DELETED_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)//Are you sure to Delete?
                    {
                        DeletePayrollDepartment();
                    }
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_GRID_EMTPY_INFO));
            }
        }

        /// <summary>
        /// To Edit the Payroll Group Details.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBar1_DoubleClick(object sender, EventArgs e)
        {
            EditStaffNameTitle();
        }

        private void ucToolBar1_EditClicked(object sender, EventArgs e)
        {
            EditStaffNameTitle();
        }

        private void ucToolBar1_PrintClicked(object sender, EventArgs e)
        {
            payrollbase.PrintGridView(gcStaffNameTitle, this.GetMessage(MessageCatalog.Payroll.PayrollGroup.PAYROLL_GROUP_VIEW_CAPTION), PrintType.DT, gvStaffNameTitleView, false);
        }

        /// <summary>
        /// To Refresh the Values After Add,Edit,Delete Operations.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBar1_RefreshClicked(object sender, EventArgs e)
        {
            FetchStaffNameTitleDetails();
        }
        #endregion

        #region Methods

        public void OnUpdateHeld(object sender, EventArgs e)
        {
            FetchStaffNameTitleDetails();
            gvStaffNameTitleView.FocusedRowHandle = RowIndex;
        }
        private void ShowStaffNameTitleAddForm(int nametitleid)
        {
            try
            {
                frmStaffNameTitle frmstaffnametitle = new frmStaffNameTitle(nametitleid);
                frmstaffnametitle.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmstaffnametitle.ShowDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }
            finally { }
        }

        private void EditStaffNameTitle()
        {
            if (gvStaffNameTitleView.RowCount != 0)
            {
                if (StaffNameTitleId != 0)
                {
                    ShowStaffNameTitleAddForm(StaffNameTitleId);
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_EDIT_GRID_EMTPY));
            }
        }

        private void DeletePayrollDepartment()
        {
            try
            {
                if (gvStaffNameTitleView.RowCount == 0) return;
                using (PayrollDepartmentSystem payrollDepSystem = new PayrollDepartmentSystem())
                {
                    payrollDepSystem.DepartmentId = gvStaffNameTitleView.GetFocusedRowCellValue(colStaffNameTitleId) != null ? this.memset.NumberSet.ToInteger(gvStaffNameTitleView.GetFocusedRowCellValue(colStaffNameTitleId).ToString()) : 0;
                    
                    if (StaffNameTitleId > 0)
                    {
                        ResultArgs resultArgs = payrollDepSystem.DeletePayrollDepartments();
                        if (resultArgs.Success)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Payroll.PayrollGroup.GROUP_DELETE_SUCCESS));
                            FetchStaffNameTitleDetails();
                        }
                        else
                        {
                            this.ShowMessageBox(resultArgs.Message);
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

        private void SetTitle()
        {
            this.Text = this.GetMessage(MessageCatalog.Payroll.PayrollDepartment.PAYROLL_DEPARTMENT_VIEW_CAPTION);
        }
        
        private void FetchStaffNameTitleDetails()
        {
            try
            {
                using (NameTitleSystem nametitleSystem = new NameTitleSystem())
                {
                    ResultArgs result = nametitleSystem.FetchNameTitle();
                    gcStaffNameTitle.DataSource = null;
                    if (result.Success)
                    {
                        gcStaffNameTitle.DataSource = result.DataSource.Table;
                    }
                    gcStaffNameTitle.RefreshDataSource();
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
            EditStaffNameTitle();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvStaffNameTitleView.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvStaffNameTitleView, colStaffNameTitle);
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
            lblRowCount.Text = gvStaffNameTitleView.RowCount.ToString();
        }

        private void frmPayrollGroupView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void frmPayrollGroupView_EnterClicked(object sender, EventArgs e)
        {
            EditStaffNameTitle();
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