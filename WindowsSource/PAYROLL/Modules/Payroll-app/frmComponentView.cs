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
using Bosco.Utility.ConfigSetting;
using Bosco.Utility.Common;
using Bosco.DAO.Data;
using Bosco.Utility;

namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmComponentView : frmPayrollBase
    {

        #region Variable Declaration
        private int RowIndex = 0;
        clsprCompBuild ComponentBuild = new clsprCompBuild();
        private DataTable dtComponentDetails;
        clsPayrollBase payrollbase = new clsPayrollBase();
        CommonMember commem = new CommonMember();
        ResultArgs resultArgs = new ResultArgs();
        #endregion

        #region Properties
        int Componentid = 0;
        private int ComponentId
        {
            set
            {
                Componentid = value;
            }
            get
            {
                return Componentid;
            }
        }
        private int Accessflag = 0;
        public int CompAccessFlag
        {
            get { return gvComponentDetails.GetFocusedRowCellValue(colAccessFlag) != null ? Convert.ToInt32(gvComponentDetails.GetFocusedRowCellValue(colAccessFlag)) : 0; }
        }
        public int LedgerProcesstype { get; set; }
        #endregion

        #region Constrcutor
        public frmComponentView()
        {
            InitializeComponent();
        }
        #endregion

        #region Methods

        private void SetTitle()
        {
            this.Text = this.GetMessage(MessageCatalog.Payroll.Component.COMPONENT_VIEW_CAPTION);
        }

        private void LoadComponentDetails()
        {
            try
            {
                int i = 0;
                string EnumProcessType = string.Empty;
                string DescriptionofEnum = string.Empty;
                dtComponentDetails = ComponentBuild.PayrollComponentType();
                DataTable dtComponentProcessType = dtComponentDetails;
                dtComponentProcessType.Columns.Add("PROCESS_TYPE", typeof(string));
                if (dtComponentDetails.Rows.Count > 0 && dtComponentDetails != null)
                {
                    /*foreach (DataRow dr in dtComponentDetails.Rows)
                    {
                        if (!string.IsNullOrEmpty(dr["PROCESS_TYPE_ID"].ToString()))
                        {
                            LedgerProcesstype = commem.NumberSet.ToInteger(dr["PROCESS_TYPE_ID"].ToString());
                            EnumProcessType = commem.EnumSet.GetEnumItemNameByValue(typeof(Processtype), LedgerProcesstype);
                            DescriptionofEnum = commem.EnumSet.GetDescriptionFromEnumValue(((Processtype)Enum.Parse(typeof(Processtype), EnumProcessType)));
                            dtComponentProcessType.Rows[i]["PROCESS_TYPE"] = DescriptionofEnum;
                            i++;
                        }
                        else
                        {
                            dtComponentProcessType.Rows[i]["PROCESS_TYPE"] = string.Empty;
                            i++;
                        }
                    }*/
                    dtComponentProcessType.DefaultView.Sort = "TYPE_SORT";
                    dtComponentProcessType = dtComponentProcessType.DefaultView.ToTable();
                    gcComponentDetails.DataSource = dtComponentProcessType;
                    gcComponentDetails.RefreshDataSource();

                }
                else
                {
                    gcComponentDetails.DataSource = null;
                }

            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }
        }
        private void showEditForm(int comId)
        {
            if (comId != 0)
            {
                frmComponentAdd components = new frmComponentAdd(comId, "Edit");
                components.UpdateHeld += new EventHandler(OnUpdateHeld);
                components.ShowDialog();
            }
        }
        private void ShowForm()
        {
            frmComponentAdd components = new frmComponentAdd("Add");
            components.UpdateHeld += new EventHandler(OnUpdateHeld);
            components.ShowDialog();
        }
        private bool ValidatecomponentDelete()
        {
            bool IsGroupVaild = true;
            if (CompAccessFlag == (int)AccessFlag.Readonly || CompAccessFlag == (int)AccessFlag.Editable)
            {
                IsGroupVaild = false;
            }
            return IsGroupVaild;
        }

        private bool ValidatecomponentEdit()
        {
            bool IsGroupVaild = true;
            if (CompAccessFlag == (int)AccessFlag.Readonly)
            {
                IsGroupVaild = false;
            }
            return IsGroupVaild;
        }
        private void EnableColumns()
        {
            if (SettingProperty.PayrollFinanceEnabled == true)
            {
                this.colProcesstype.Visible = true;
                this.colLedgerName.Visible = true;
            }
            else
            {
                this.colProcesstype.Visible = false;
                this.colLedgerName.Visible = false;
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
            this.enumUserRights.Add(Payrollcomponent.CreatePayrollComponent);
            this.enumUserRights.Add(Payrollcomponent.EditPayrollComponent);
            this.enumUserRights.Add(Payrollcomponent.DeletePayrollComponent);
            this.enumUserRights.Add(Payrollcomponent.ViewPayrollComponent);
            this.ApplyUserRights(ucToolBar2, enumUserRights, (int)Menus.PayrollComponent);
        }
        #endregion

        #region Events
        private void frmComponentView_Load(object sender, EventArgs e)
        {
            EnableColumns();
            LoadComponentDetails();
            SetTitle();
            //15/02/2022, For temp----------------------------
            //ApplyUserRights();
            ucToolBar2.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Always;
            ucToolBar2.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
            ucToolBar2.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Always;
            //-------------------------------------------
        }

        private void ucToolBar1_AddClicked(object sender, EventArgs e)
        {
            ShowForm();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvComponentDetails.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvComponentDetails, colComponent);
            }
        }
        public void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadComponentDetails();
            gvComponentDetails.FocusedRowHandle = RowIndex;
        }
        private void ucToolBar1_EditClicked(object sender, EventArgs e)
        {
            if (ValidatecomponentEdit())
            {
                ComponentId = gvComponentDetails.GetFocusedRowCellValue(colComponentId) != null ? Convert.ToInt32(gvComponentDetails.GetFocusedRowCellValue(colComponentId)) : 0;
                if (ComponentId == 0)
                    //XtraMessageBox.Show(MessageCatalog.Common.COMMON_NO_RECORD_SELECTED,"Payroll",MessageBoxButtons.OK,MessageBoxIcon.Information);
                    //XtraMessageBox.Show("No record is available to edit", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_EDIT_GRID_EMTPY));
                showEditForm(ComponentId);
            }
            else
            {
                //XtraMessageBox.Show(MessageCatalog.Common.PAYROLL_FIXED_COMPONENT_CANNOT_EDIT, MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                XtraMessageBox.Show(this.GetMessage(MessageCatalog.Common.PAYROLL_FIXED_COMPONENT_CANNOT_EDIT), this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
        private void gvComponentDetails_DoubleClick(object sender, EventArgs e)
        {
            ucToolBar1_EditClicked(sender, e);
        }

        private void gvComponentDetails_RowCountChanged(object sender, EventArgs e)
        {
            lblRecord.Text = gvComponentDetails.RowCount.ToString();
        }
        private void ucToolBar2_DeleteClicked(object sender, EventArgs e)
        {
            if (ValidatecomponentDelete())
            {
                ComponentId = gvComponentDetails.GetFocusedRowCellValue(colComponentId) != null ? Convert.ToInt32(gvComponentDetails.GetFocusedRowCellValue(colComponentId)) : 0;
                if (ComponentId > 0)
                {
                    //DialogResult result = XtraMessageBox.Show(MessageCatalog.Payroll.Component.COMMON_DELETED_CONFIRMATION, MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //DialogResult result = XtraMessageBox.Show(this.GetMessage(MessageCatalog.Payroll.Component.COMMON_DELETED_CONFIRMATION), 
                    //                this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION), MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    //if (result != DialogResult.Yes) return;
                    if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_DELETED_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)//Are you sure to Delete?
                    {
                        resultArgs = ComponentBuild.DeleteComponentDetails(ComponentId);
                        if (resultArgs.Success)
                        {
                            this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Payroll.Component.COMPONENT_DELETE_SUCCESS_INFO));
                            LoadComponentDetails();
                        }
                        else
                        {
                            this.ShowMessageBox(resultArgs.Message);
                        }
                    }
                }
                else
                {
                    XtraMessageBox.Show(this.GetMessage(MessageCatalog.Common.COMMON_NO_RECORD_SELECTED), this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION), MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            else
            {
                //XtraMessageBox.Show(MessageCatalog.Common.PAYROLL_FIXED_COMPONENT_CANNOT_DELETE, MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION, MessageBoxButtons.OK, MessageBoxIcon.Information);
                XtraMessageBox.Show(this.GetMessage(MessageCatalog.Common.PAYROLL_FIXED_COMPONENT_CANNOT_DELETE), this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_BOX_CAPTION), MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void ucToolBar2_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ucToolBar2_PrintClicked(object sender, EventArgs e)
        {
            //payrollbase.PrintGridView(gcComponentDetails, this.Text, PrintType.DT, gvComponentDetails, false);
            //payrollbase.PrintGridView(gcComponentDetails, this.GetMessage(MessageCatalog.Payroll.Component.COMPONENT_PRINT_CAPTION), PrintType.DT, gvComponentDetails, false);

            PrintGridViewDetails(gcComponentDetails, this.GetMessage(MessageCatalog.Payroll.Component.COMPONENT_PRINT_CAPTION), PrintType.DT, gvComponentDetails);
        }

        private void ucToolBar2_RefreshClicked(object sender, EventArgs e)
        {
            LoadComponentDetails();
        }
        private void frmComponentView_ShowFilterClicked(object sender, EventArgs e)
        {
            chkShowFilter.Checked = (chkShowFilter.Checked) ? false : true;
        }
        private void frmComponentView_EnterClicked(object sender, EventArgs e)
        {

            ComponentId = gvComponentDetails.GetFocusedRowCellValue(colComponentId) != null ? Convert.ToInt32(gvComponentDetails.GetFocusedRowCellValue(colComponentId)) : 0;
            if (ComponentId == 0)
                //XtraMessageBox.Show(MessageCatalog.Common.COMMON_NO_RECORD_SELECTED,"Payroll",MessageBoxButtons.OK,MessageBoxIcon.Information);
                //XtraMessageBox.Show("No record is available to Edit", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.CommonPayroll.PAYROLL_COMMON_EDIT_GRID_EMTPY));
            showEditForm(ComponentId);
        }
        #endregion
    }
}