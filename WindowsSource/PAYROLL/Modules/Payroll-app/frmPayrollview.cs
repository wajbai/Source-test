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
using Bosco.Utility;
using Bosco.Utility.Common;
using DevExpress.Utils.Frames;
using DevExpress.XtraGrid.Views.Grid;
using Bosco.Model.UIModel;

namespace PAYROLL.Modules.Payroll_app
{
    public partial class frmPayrollview : frmPayrollBase
    {
        #region VariableDeclaration
        private clsPrGateWay objPayrollGateWays = new clsPrGateWay();
        private DataGrid dgShowPayrollDetails = new DataGrid();
        private string strShow = "";
        ApplicationCaption8_1 captionPanel;
        ResultArgs resultArgs = null;
        CommonMember commem = new CommonMember();
        #endregion

        #region Constructor
        public frmPayrollview()
        {
            InitializeComponent();
        }
        #endregion

        #region Property
        public string SetCaption
        {
            set { captionPanel.Text = value; }
        }
        public int ProjectId
        {
            get;
            set;
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
                        chkGroupWise.Properties.DataSource = dtGradeList;
                        chkGroupWise.Properties.ValueMember = "GROUP ID";
                        chkGroupWise.Properties.DisplayMember = "Group Name";
                        chkGroupWise.RefreshEditValue();
                    }
                    else
                    {
                        chkGroupWise.Properties.DataSource = null;
                    }
                }
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.ToString());
            }
        }

        /// <summary>
        /// 
        /// </summary>
        public void LoadData()
        {
            objPayrollGateWays.PayRollId = clsGeneral.PAYROLL_ID;
            object obf = chkGroupWise.Properties.GetCheckedItems();
            objPayrollGateWays.ShowPayRollAbstract(dgShowPayrollDetails, false, obf.ToString(), ProjectId);
            gcPayrollView.DataSource = SettingProperty.dtData;
            gcPayrollView.RefreshDataSource();
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

        private bool ValidateDetails()
        {
            bool isValid = true;
            if (SettingProperty.PayrollFinanceEnabled)
            {
                if (glkpProject.EditValue == null || string.IsNullOrEmpty(glkpProject.Text) || glkpProject.EditValue.ToString() == "0")
                {
                    //XtraMessageBox.Show("Project is empty.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.PayrollView.PAYOLL_VIEW_PROJECT_EMPTY));
                    isValid = false;
                }
            }
            else if (chkGroupWise.EditValue == null || string.IsNullOrEmpty(chkGroupWise.Text) || chkGroupWise.EditValue.ToString() == "0")
            {
                //XtraMessageBox.Show("Group is empty.", "Payroll", MessageBoxButtons.OK, MessageBoxIcon.Information);
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Payroll.PayrollView.PAYOLL_VIEW_GROUP_EMPTY));
                isValid = false;
            }
            return isValid;
        }

        #endregion

        #region Events
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmPayrollview_Load(object sender, EventArgs e)
        {
            LoadGroupList();
            LoadData();
            //lblSelectedGroups.Text = " ";
            lciProject.Visibility = (SettingProperty.PayrollFinanceEnabled) ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always
                : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

        }



        /// <summary>
        /// 
        /// </summary>
        public class AppCpation : ApplicationCaption8_1
        {
            protected override Image DXLogo
            {
                get
                {
                    return new Bitmap(5, 5);
                }
            }

            protected override void ResetImage()
            {
                base.ResetImage();
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvPayrollView_RowCountChanged(object sender, EventArgs e)
        {
            // lblRecordCounts.Text = gvPayrollView.RowCount.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="gridview"></param>
        /// <param name="colGridColumn"></param>
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
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvPayrollView.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvPayrollView, colComponent);
            }
        }

        public virtual void OnUpdateHeld(object sender, EventArgs e)
        {
            LoadData();
            //gvComponentDetails.FocusedRowHandle = RowIndex;
        }

        private void gvPayrollView_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            GridView view = sender as GridView;
            if (e.RowHandle >= 0)
            {
                if (view.GetRowCellDisplayText(e.RowHandle, view.Columns["Component"]).ToString() == "EARNING"
                    || view.GetRowCellDisplayText(e.RowHandle, view.Columns["Component"]).ToString() == "TOTAL EARNINGS")
                {
                    e.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
                    e.Appearance.ForeColor = Color.DarkGreen;
                    // e.Appearance.BackColor = Color.Gainsboro;
                }

                if (view.GetRowCellDisplayText(e.RowHandle, view.Columns["Component"]).ToString() == "DEDUCTION" &&
                    view.GetRowCellDisplayText(e.RowHandle, view.Columns["Amount"]).ToString() == string.Empty
                    || view.GetRowCellDisplayText(e.RowHandle, view.Columns["Component"]).ToString() == "TOTAL DEDUCTIONS")
                {
                    e.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
                    e.Appearance.ForeColor = Color.Red;
                    // e.Appearance.BackColor = Color.Gainsboro;
                }

                if (view.GetRowCellDisplayText(e.RowHandle, view.Columns["Component"]).ToString() == "NET PAY")
                {
                    e.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
                    e.Appearance.ForeColor = Color.Red;
                    // e.Appearance.BackColor = Color.Gainsboro;
                }
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            objPayrollGateWays.PayRollId = clsGeneral.PAYROLL_ID;
            objPayrollGateWays.PRName = clsGeneral.PAYROLL_MONTH;
            strShow = "";
            try
            {
                if (ValidateDetails())
                {
                    if (chkGroupWise.Properties.Items.GetCheckedValues().Count == 0)
                    {
                        if (SettingProperty.PayrollFinanceEnabled)
                        {
                            ProjectId = commem.NumberSet.ToInteger(glkpProject.EditValue.ToString());
                            objPayrollGateWays.ShowPayRollAbstract(dgShowPayrollDetails, false, "", ProjectId);
                        }
                        else
                        {
                            objPayrollGateWays.ShowPayRollAbstract(dgShowPayrollDetails, false, "");
                        }
                        //lblSelectedGroups.Text = " ";
                    }
                    if (chkGroupWise.Properties.Items.GetCheckedValues().Count == chkGroupWise.Properties.Items.Count)
                    {
                        //lblSelectedGroups.Text = "All";
                    }
                    if (chkGroupWise.Properties.Items.Count >= 0)
                    {
                        //lblGroupSelectedGroup.Text = 
                        object obf = chkGroupWise.Properties.GetCheckedItems();
                        //lblSText.Text = "";
                        string value = chkGroupWise.Properties.GetDisplayText(obf);
                        //lblSelectedGroups.Text = obf != string.Empty ? value : " ";
                        //lblSText.Text = value.Substring(0, value.Length - 1);
                        //  objPayrollGateWays.ShowPayRollAbstract(dgShowPayrollDetails, false, obf.ToString());
                        LoadData();
                        //for (int i = 0; i < ; i++)
                        //{
                        //    strShow += chkListGrade.CheckedItems[i].ToString() + ",";
                        //    strCol1 += htGrade[chkListGrade.CheckedItems[i].ToString()].ToString() + ",";

                        //}
                        //string strCon = strCol1.Remove(strCol1.LastIndexOf(","), 1);

                        //    objPRGW.ShowPayRollAbstract(dgShowPayrollDetails,(strCon == "" ? false : true),strCon);
                        //  objPayrollGateWays.ShowPayRollAbstract(dgShowPayrollDetails, false, strCon);
                        //objPayrollGateWays.ShowPayRollAbstract(dgShowPayrollDetails, false, "");
                    }

                    //if (chkListGrade.CheckedItems.Count >= 1)
                    //{
                    //    for (int i = 0; i < chkListGrade.CheckedItems.Count; i++)
                    //    {
                    //        strShow += chkListGrade.CheckedItems[i].ToString() + ",";
                    //        strCol1 += htGrade[chkListGrade.CheckedItems[i].ToString()].ToString() + ",";

                    //    }
                    //    string strCon = strCol1.Remove(strCol1.LastIndexOf(","), 1);

                    //    //    objPRGW.ShowPayRollAbstract(dgShowPayrollDetails,(strCon == "" ? false : true),strCon);
                    //    objPayrollGateWays.ShowPayRollAbstract(dgShowPayrollDetails, false, strCon);
                    //}

                    //  lblGroupSelectedGroup.Text = strShow.Substring(0, strShow.Length - 1);
                    // lblGroupSelectedGroup.Text = checkedComboBoxEdit1.Text;
                }
            }
            catch
            {
                return;
            }
        }

        private void checkedComboBoxEdit1_EditValueChanged(object sender, EventArgs e)
        {
            //objPayrollGateWays.PayRollId = clsGeneral.PAYROLL_ID;
            //objPayrollGateWays.PRName = clsGeneral.PAYROLL_MONTH;
            //strShow = "";
            //try
            //{
            //    if (checkedComboBoxEdit1.Properties.Items.GetCheckedValues().Count == 0)
            //    {
            //        objPayrollGateWays.ShowPayRollAbstract(dgShowPayrollDetails, false, "");
            //        lblSText.Text = "Group Wise";
            //    }
            //    if (checkedComboBoxEdit1.Properties.Items.GetCheckedValues().Count == checkedComboBoxEdit1.Properties.Items.Count)
            //    {
            //        lblSText.Text = "All";
            //    }
            //    if (checkedComboBoxEdit1.Properties.Items.Count >= 0)
            //    {
            //        //lblGroupSelectedGroup.Text = 
            //        object obf = checkedComboBoxEdit1.Properties.GetCheckedItems();
            //        lblSText.Text = "";
            //        string value = checkedComboBoxEdit1.Properties.GetDisplayText(obf);
            //        lblSText.Text = value.Substring(0, value.Length - 1);

            //        //for (int i = 0; i < ; i++)
            //        //{
            //        //    strShow += chkListGrade.CheckedItems[i].ToString() + ",";
            //        //    strCol1 += htGrade[chkListGrade.CheckedItems[i].ToString()].ToString() + ",";

            //        //}
            //        //string strCon = strCol1.Remove(strCol1.LastIndexOf(","), 1);

            //        //    objPRGW.ShowPayRollAbstract(dgShowPayrollDetails,(strCon == "" ? false : true),strCon);
            //        //  objPayrollGateWays.ShowPayRollAbstract(dgShowPayrollDetails, false, strCon);
            //        //objPayrollGateWays.ShowPayRollAbstract(dgShowPayrollDetails, false, "");
            //    }

            //    //if (chkListGrade.CheckedItems.Count >= 1)
            //    //{
            //    //    for (int i = 0; i < chkListGrade.CheckedItems.Count; i++)
            //    //    {
            //    //        strShow += chkListGrade.CheckedItems[i].ToString() + ",";
            //    //        strCol1 += htGrade[chkListGrade.CheckedItems[i].ToString()].ToString() + ",";

            //    //    }
            //    //    string strCon = strCol1.Remove(strCol1.LastIndexOf(","), 1);

            //    //    //    objPRGW.ShowPayRollAbstract(dgShowPayrollDetails,(strCon == "" ? false : true),strCon);
            //    //    objPayrollGateWays.ShowPayRollAbstract(dgShowPayrollDetails, false, strCon);
            //    //}

            //    //  lblGroupSelectedGroup.Text = strShow.Substring(0, strShow.Length - 1);
            //    // lblGroupSelectedGroup.Text = checkedComboBoxEdit1.Text;
            //}
            //catch
            //{
            //    return;
            //}
        }
        #endregion

        private void glkpProject_Enter(object sender, EventArgs e)
        {
            LoadProjects();
        }
    }
}
