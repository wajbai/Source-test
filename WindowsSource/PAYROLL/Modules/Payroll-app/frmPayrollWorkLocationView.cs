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
    public partial class frmPayrollWorkLocationView : frmPayrollBase
    {
        #region VariableDeclartion
        private int RowIndex = 0;
        CommonMember memset = new CommonMember();
        clsPayrollBase payrollbase = new clsPayrollBase();
        #endregion

        #region Constructors
        public frmPayrollWorkLocationView()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties
        private int payrollworklocationid = 0;
        private int PayrollWorkLocationId
        {
            get
            {
                RowIndex = gvPayrollWorkLocationView.FocusedRowHandle;
                payrollworklocationid = gvPayrollWorkLocationView.GetFocusedRowCellValue(colPayrollWorkLocationId) != null ? Convert.ToInt32(gvPayrollWorkLocationView.GetFocusedRowCellValue(colPayrollWorkLocationId).ToString()) : 0;
                return payrollworklocationid;
            }
            set
            {
                payrollworklocationid = value;
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
            FetchWorkLocationDetails();
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
            ShowPayrollWorkLocationAddForm((int)AddNewRow.NewRow);
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
            if (gvPayrollWorkLocationView.RowCount != 0)
            {
                if (PayrollWorkLocationId != 0)
                {
                    if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_DELETED_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)//Are you sure to Delete?
                    {
                        DeletePayrollWorkLocation();
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
        /// <param name="e"></param>m
        private void ucToolBar1_DoubleClick(object sender, EventArgs e)
        {
            EditPayrollWorkLocation();
        }

        private void ucToolBar1_EditClicked(object sender, EventArgs e)
        {
            EditPayrollWorkLocation();
        }

        private void ucToolBar1_PrintClicked(object sender, EventArgs e)
        {
            payrollbase.PrintGridView(gcPayrollWorkLocationView, this.GetMessage(MessageCatalog.Payroll.PayrollWorkLocation.PAYROLL_WORKLOCATION_VIEW_CAPTION), PrintType.DT, gvPayrollWorkLocationView, false);
        }

        /// <summary>
        /// To Refresh the Values After Add,Edit,Delete Operations.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBar1_RefreshClicked(object sender, EventArgs e)
        {
            FetchWorkLocationDetails();
        }
        #endregion

        #region Methods

        public void OnUpdateHeld(object sender, EventArgs e)
        {
            FetchWorkLocationDetails();
            gvPayrollWorkLocationView.FocusedRowHandle = RowIndex;
        }
        private void ShowPayrollWorkLocationAddForm(int worklocationid)
        {
            try
            {
                frmPayrollWorkLocation frmPayrollworklocation = new frmPayrollWorkLocation(worklocationid);
                frmPayrollworklocation.UpdateHeld += new EventHandler(OnUpdateHeld);
                frmPayrollworklocation.ShowDialog();
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }
            finally { }
        }

        private void EditPayrollWorkLocation()
        {
            if (gvPayrollWorkLocationView.RowCount != 0)
            {
                if (PayrollWorkLocationId != 0)
                {
                    ShowPayrollWorkLocationAddForm(PayrollWorkLocationId);
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_EDIT_GRID_EMTPY));
            }
        }

        private void DeletePayrollWorkLocation()
        {
            try
            {
                if (gvPayrollWorkLocationView.RowCount == 0) return;
                using (PayrollWorkLocationSystem PayrollWorkLocationSystem = new PayrollWorkLocationSystem())
                {
                    PayrollWorkLocationSystem.PayrollWorkLocationId = gvPayrollWorkLocationView.GetFocusedRowCellValue(colPayrollWorkLocationId) != null ? this.memset.NumberSet.ToInteger(gvPayrollWorkLocationView.GetFocusedRowCellValue(colPayrollWorkLocationId).ToString()) : 0;
                    
                    if (PayrollWorkLocationId > 0)
                    {
                        ResultArgs resultArgs = PayrollWorkLocationSystem.DeletePayrollWorkLocation();
                        if (resultArgs.Success)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Payroll.PayrollGroup.GROUP_DELETE_SUCCESS));
                            FetchWorkLocationDetails();
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
            this.Text = this.GetMessage(MessageCatalog.Payroll.PayrollWorkLocation.PAYROLL_WORKLOCATION_VIEW_CAPTION);
        }
        
        private void FetchWorkLocationDetails()
        {
            try
            {
                using (PayrollWorkLocationSystem payrollworklocationSystem = new PayrollWorkLocationSystem())
                {
                    ResultArgs result = payrollworklocationSystem.FetchPayrollWorkLocation();
                    gcPayrollWorkLocationView.DataSource = null;
                    if (result.Success)
                    {
                        gcPayrollWorkLocationView.DataSource = result.DataSource.Table;
                    }
                    gcPayrollWorkLocationView.RefreshDataSource();
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
            EditPayrollWorkLocation();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvPayrollWorkLocationView.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvPayrollWorkLocationView, colPayrollWorkLocation);
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
            lblRowCount.Text = gvPayrollWorkLocationView.RowCount.ToString();
        }

        private void frmPayrollGroupView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }

        private void frmPayrollGroupView_EnterClicked(object sender, EventArgs e)
        {
            EditPayrollWorkLocation();
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