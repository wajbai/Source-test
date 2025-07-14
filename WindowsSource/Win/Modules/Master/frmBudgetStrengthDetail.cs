using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Utility;
using Bosco.Model.UIModel.Master;
using Bosco.Model.Transaction;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using Bosco.Model.UIModel;

namespace ACPP.Modules.Master
{
    public partial class frmBudgetStrengthDetail : frmFinanceBaseAdd
    {
        private Int32 BudgetId
        {
            get;
            set;
        }

        private string ProjectIds
        {
            get;
            set;
        }

        private DataTable dtBudgetStrengthDetails = new DataTable();
        public DataTable BudgetStrengthDetails
        {
            set { dtBudgetStrengthDetails = value; }
            get { return dtBudgetStrengthDetails; }

        }
        
        
        private Int32 CCId
        {
            get
            {
                Int32 ccid = gvBudgetStrengthDetail.GetFocusedRowCellValue(colCC) != null ? this.UtilityMember.NumberSet.ToInteger(gvBudgetStrengthDetail.GetFocusedRowCellValue(colCC).ToString()) : 0;
                return ccid;
            }
        }
        
        private Int32 NewCount
        {
            get
            {
                Int32 newcount = gvBudgetStrengthDetail.GetFocusedRowCellValue(colNewCount) != null ? this.UtilityMember.NumberSet.ToInteger(gvBudgetStrengthDetail.GetFocusedRowCellValue(colNewCount).ToString()) : 0;
                return newcount;
            }
        }
                
        private Int32 PresentCount
        {
            get
            {
                Int32 presentcount = gvBudgetStrengthDetail.GetFocusedRowCellValue(colPresentCount) != null ? this.UtilityMember.NumberSet.ToInteger(gvBudgetStrengthDetail.GetFocusedRowCellValue(colPresentCount).ToString()) : 0;
                return presentcount;
            }
        }


        //Focus to new row
        private bool GridNewItem
        {
            set
            {
                if (value)
                {
                    DataTable dtbudgetstreanthdetails = gcBudgetStrengthDetail.DataSource as DataTable;
                    AddNewRow(dtbudgetstreanthdetails);
                    gcBudgetStrengthDetail.DataSource = dtbudgetstreanthdetails;
                    gvBudgetStrengthDetail.MoveNext();
                    gvBudgetStrengthDetail.FocusedColumn = colCC;
                    gvBudgetStrengthDetail.ShowEditor();
                }
            }
        }

        public frmBudgetStrengthDetail()
        {
            InitializeComponent();
            this.PageTitle = "Budget Strength Detail";
        }

        public frmBudgetStrengthDetail(int budgetid, string projectIds, DataTable dtbudgetstrengthdetails)
            : this()
        {
            BudgetId = budgetid;
            ProjectIds = projectIds;
            BudgetStrengthDetails = dtbudgetstrengthdetails.DefaultView.ToTable();

            BindEmptySource();
            //Attach amount chage event
            RealColumnEditNewCount();
            RealColumnEditPresentCount();

            gvBudgetStrengthDetail.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
        }

        private void frmBudgetStrengthDetail_Load(object sender, EventArgs e)
        {
            gvBudgetStrengthDetail.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            BindCostCenter();
            BindBudgetStatisticsDetail();    
        }

        private void frmBudgetStrengthDetail_Shown(object sender, EventArgs e)
        {
            gcBudgetStrengthDetail.Select();
            gvBudgetStrengthDetail.FocusedRowHandle = DevExpress.XtraGrid.GridControl.NewItemRowHandle;
            gvBudgetStrengthDetail.FocusedColumn = gvBudgetStrengthDetail.VisibleColumns[0];
            gvBudgetStrengthDetail.ShowEditor();
        }

        /// <summary>
        /// Skip already selected statistic type
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvBudgetStrengthDetail_ShownEditor(object sender, EventArgs e)
        {
            DevExpress.XtraGrid.Views.Grid.GridView view;
            view = sender as DevExpress.XtraGrid.Views.Grid.GridView;
            if (view.FocusedColumn == colCC && view.ActiveEditor is DevExpress.XtraEditors.GridLookUpEdit)
            {
                string selectedrefs = GetSelectedReferenceNosInGrid(CCId);
                DevExpress.XtraEditors.GridLookUpEdit edit;
                edit = (DevExpress.XtraEditors.GridLookUpEdit)view.ActiveEditor;

                DataTable table = (DataTable)edit.Properties.DataSource; ;
                DataView clone = new DataView(table);
                using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
                {
                    string selected = GetSelectedReferenceNosInGrid(CCId);
                    clone.RowFilter = string.Empty;
                    if (!String.IsNullOrEmpty(selected))
                    {
                        clone.RowFilter = vouchersystem.AppSchema.BudgetStrength.COST_CENTRE_IDColumn.ColumnName + " NOT IN (" + selected + ")";
                    }

                    DataTable dtbind = clone.ToTable();
                    edit.Properties.DataSource = dtbind;
                    gvBudgetStrengthDetail.ShowEditor();
                    return;
                }
            }
            else if (view.FocusedColumn == colNewCount)
            {
                //var editor = (TextEdit)gvBudgetStatisticsDetail.ActiveEditor;
                //editor.SelectionLength = 0;
                //editor.SelectionStart = editor.Text.Length;
            }
        }

        /// <summary>
        /// Event for amount changed
        /// </summary>
        private void RealColumnEditTransAmount_EditValueChanged(object sender, System.EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvBudgetStrengthDetail.PostEditor();
            gvBudgetStrengthDetail.UpdateCurrentRow();
            if (gvBudgetStrengthDetail.ActiveEditor == null)
            {
                gvBudgetStrengthDetail.ShowEditor();
            }

            TextEdit txtTotalCount = edit as TextEdit;
            int grpCounts = (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.DecimalPlaces) + 1);
            if (txtTotalCount.Text.Length > grpCounts && txtTotalCount.SelectionLength == txtTotalCount.Text.Length)
                txtTotalCount.Select(txtTotalCount.Text.Length - grpCounts, 0);
        }

        private void gcBudgetStrengthDetail_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) && gvBudgetStrengthDetail.FocusedColumn == colPresentCount && !e.Shift && !e.Alt && !e.Control)
            {
                if (CCId == 0 && NewCount == 0 && PresentCount == 0 && gvBudgetStrengthDetail.IsLastRow)
                {
                    btnOk.Select();
                    btnOk.Focus();
                }
                else if (CCId > 0 && (NewCount > 0 || PresentCount > 0) && gvBudgetStrengthDetail.IsLastRow)
                {
                    if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                    {
                        GridNewItem = true;
                        e.SuppressKeyPress = true;
                    }
                }
                else
                {
                    if (IsValidRows())
                    {
                        MoveNextRow();
                        e.SuppressKeyPress = true;
                    }
                    else
                    {
                        e.SuppressKeyPress = true;
                    }
                }
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (IsValidRows())
            {
                UpdateBudgetStrengthDetails();
                this.Close();
            }
            else
            {
                gvBudgetStrengthDetail.MoveFirst();
                gvBudgetStrengthDetail.FocusedColumn = colCC;
                gvBudgetStrengthDetail.ShowEditor();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbeBudgetStatistcsDetails_Click(object sender, EventArgs e)
        {
            DeleteBudgetStrengthDetails();
        }

    
        /// <summary>
        /// Key press
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="KeyData"></param>
        /// <returns></returns>
        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            if (KeyData == (Keys.Alt | Keys.D))
            {
                DeleteBudgetStrengthDetails();
            }
            return base.ProcessCmdKey(ref msg, KeyData);
        }

        /// <summary>
        /// This method is uued to bind emptry datasource
        /// </summary>
        private void BindEmptySource()
        {
            using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
            {
                dtBudgetStrengthDetails = AddNewRow(dtBudgetStrengthDetails);
                gcBudgetStrengthDetail.DataSource = dtBudgetStrengthDetails;
            }
        }

        /// <summary>
        /// This method is used to bind leger reference details
        /// </summary>
        private void BindBudgetStatisticsDetail()
        {
            try
            {
                using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
                {
                    //dtBudgetStrengthDetails = AddNewRow(dtBudgetStrengthDetails);
                    gcBudgetStrengthDetail.DataSource = dtBudgetStrengthDetails;
                    gvBudgetStrengthDetail.MoveFirst();
                    gvBudgetStrengthDetail.FocusedColumn = colCC;
                    gvBudgetStrengthDetail.ShowEditor();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void BindCostCenter()
        {
            try
            {
                using (CostCentreSystem costCenterSystem = new CostCentreSystem())
                {
                    costCenterSystem.ProjectIds = ProjectIds;
                    costCenterSystem.LedgerId = 0; // DistributeSouceId;
                    ResultArgs resultArgs = costCenterSystem.FetchMappedProjectCostCentre();
                    rglkpCC.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        resultArgs.DataSource.Table.DefaultView.Sort = costCenterSystem.AppSchema.CostCentre.ABBREVATIONColumn.ColumnName + "," +
                                                costCenterSystem.AppSchema.CostCentre.COST_CENTRE_NAMEColumn.ColumnName;
                        rglkpCC.DataSource = resultArgs.DataSource.Table;
                        rglkpCC.DisplayMember = costCenterSystem.AppSchema.CostCentre.COST_CENTRE_NAMEColumn.ColumnName;
                        rglkpCC.ValueMember = costCenterSystem.AppSchema.CostCentre.COST_CENTRE_IDColumn.ColumnName;
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
        /// To Focus grid with first row and reference column
        /// </summary>
        private void SetGridFocus()
        {
            gcBudgetStrengthDetail.Select();
            gvBudgetStrengthDetail.MoveFirst();
            gvBudgetStrengthDetail.FocusedColumn = colCC;
            gvBudgetStrengthDetail.ShowEditor();
        }

        /// <summary>
        /// This method is used to check all rows should have stati and amount
        /// and foucs to concern rows
        /// </summary>
        /// <returns></returns>
        private bool IsValidRows()
        {
            bool isValid = true;
            string ccname = string.Empty;
            DataTable dt = gcBudgetStrengthDetail.DataSource as DataTable;

            //Check Duplicate CC
            if (dt != null && dt.Rows.Count>0)
            {
                using (CostCentreSystem costCenterSystem = new CostCentreSystem())
                {
                    DataTable dtUnique = dt.DefaultView.ToTable(true, costCenterSystem.AppSchema.CostCentre.COST_CENTRE_IDColumn.ColumnName);

                    //#. Check duplicate budget new projects in grid
                    if (dtUnique.Rows.Count != dt.Rows.Count)
                    {
                        var duplicates = dt.AsEnumerable().GroupBy(r => r[costCenterSystem.AppSchema.CostCentre.COST_CENTRE_IDColumn.ColumnName]).Where(gr => gr.Count() > 1).ToList();
                        if (duplicates.Any())
                        {
                            Int32 id = UtilityMember.NumberSet.ToInteger(duplicates[0].Key.ToString());

                            costCenterSystem.FillCostCentreProperties(id);
                            ccname = costCenterSystem.CostCentreName;
                            isValid = false;
                            //string duplicatedNames = String.Join(System.Environment.NewLine, duplicates.Select(dupl => dupl.Key));
                            //string duplicatedNames = String.Join(", ", duplicates.Select(dupl => dupl.Key));
                        }
                    }
                    
                    //var query = from row in dt.AsEnumerable()
                    //            group row by row.Field<UInt32>(costCenterSystem.AppSchema.CostCentre.COST_CENTRE_IDColumn.ColumnName) into ccstrength
                    //            orderby ccstrength.Key
                    //            select new
                    //            {
                    //                CCId = ccstrength.Key,
                    //                CountOfccs = ccstrength.Count()
                    //            };
                    //if (query != null)
                    //{
                    //    foreach (var cccount in query)
                    //    {
                    //        bool duplicated = (cccount.CountOfccs > 1);

                    //        if (duplicated)
                    //        {
                                

                    //            costCenterSystem.FillCostCentreProperties(id);
                    //            ccname = costCenterSystem.CostCentreName;
                    //            isValid = !duplicated;
                    //            break;
                    //        }
                    //    }
                    //}
                }

                if (!isValid)
                {
                    this.ShowMessageBox("Strength '" + ccname + "' is duplicated in the List");
                }
            }
            
            return isValid;
        }

        private void UpdateBudgetStrengthDetails()
        {
            try
            {
                DataTable dtgridbudgetstrength = gcBudgetStrengthDetail.DataSource as DataTable;
                if (dtgridbudgetstrength != null && dtgridbudgetstrength.Rows.Count > 0)
                {
                    
                    using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
                    {
                        BudgetStrengthDetails.DefaultView.RowFilter = vouchersystem.AppSchema.BudgetStrength.COST_CENTRE_IDColumn.ColumnName + " > 0 AND " +
                                                                          " (" + vouchersystem.AppSchema.BudgetStrength.NEW_COUNTColumn.ColumnName + " > 0 OR " +
                                                                              vouchersystem.AppSchema.BudgetStrength.PRESENT_COUNTColumn.ColumnName + " > 0)";
                        BudgetStrengthDetails = BudgetStrengthDetails.DefaultView.ToTable();
                    }
                    
                    this.ReturnValue = BudgetStrengthDetails;
                    this.DialogResult = DialogResult.OK;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// Add Empty row 
        /// </summary>
        /// <param name="dtSource"></param>
        /// <returns></returns>
        private DataTable AddNewRow(DataTable dtSource)
        {
            using (VoucherTransactionSystem vouchersystem = new VoucherTransactionSystem())
            {
                DataRow dr = dtSource.NewRow();
                dr[vouchersystem.AppSchema.BudgetStrength.BUDGET_IDColumn.ColumnName] = 0;
                dr[vouchersystem.AppSchema.BudgetStrength.COST_CENTRE_IDColumn.ColumnName] = 0;
                dr[vouchersystem.AppSchema.BudgetStrength.NEW_COUNTColumn.ColumnName] = 0;
                dr[vouchersystem.AppSchema.BudgetStrength.PRESENT_COUNTColumn.ColumnName] = 0;
                dtSource.Rows.Add(dr);
            }
            return dtSource;
        }

        private void RealColumnEditNewCount()
        {
            colNewCount.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditTransAmount_EditValueChanged);
            this.gvBudgetStrengthDetail.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvBudgetStrengthDetail.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colNewCount)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvBudgetStrengthDetail.ShowEditorByMouse();
                    }));
                }
            };
        }

        private void RealColumnEditPresentCount()
        {
            colPresentCount.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditTransAmount_EditValueChanged);
            this.gvBudgetStrengthDetail.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvBudgetStrengthDetail.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colPresentCount)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvBudgetStrengthDetail.ShowEditorByMouse();
                    }));
                }
            };
        }

        /// <summary>
        /// Delete row in the grid
        /// </summary>
        public void DeleteBudgetStrengthDetails()
        {
            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                gvBudgetStrengthDetail.DeleteRow(gvBudgetStrengthDetail.FocusedRowHandle);
                if (gvBudgetStrengthDetail.RowCount == 0)
                {
                    BindEmptySource();
                }
                gvBudgetStrengthDetail.MoveFirst();
                gvBudgetStrengthDetail.FocusedColumn = colCC;
                gvBudgetStrengthDetail.ShowEditor();

            }
        }

        /// <summary>
        /// This method is used to get reference voucher Ids, which are already availbale in grid
        /// </summary>
        /// <param name="activerefid"></param>
        /// <returns></returns>
        private string GetSelectedReferenceNosInGrid(Int32 activerefid)
        {
            string rtn = string.Empty;
            try
            {
                DataTable dtRef = gcBudgetStrengthDetail.DataSource as DataTable;
                foreach (DataRow dr in dtRef.Rows)
                {
                    Int32 selectedrefid = UtilityMember.NumberSet.ToInteger(dr["COST_CENTRE_ID"].ToString());
                    
                    if (activerefid != selectedrefid)
                    {
                        rtn += selectedrefid.ToString() + ",";
                    }
                }
                rtn = rtn.TrimEnd(',');
            }
            catch (Exception err)
            {
                rtn = string.Empty;
            }
            return rtn;
        }

        /// <summary>
        /// This method is used to move next row in the grid
        /// </summary>
        private void MoveNextRow()
        {
            if (IsValidRows())
            {
                gvBudgetStrengthDetail.MoveNext();
                gvBudgetStrengthDetail.FocusedColumn = colCC;
                gvBudgetStrengthDetail.ShowEditor();
            }
        }

        private void frmBudgetStatisticsDetail_FormClosing(object sender, FormClosingEventArgs e)
        {
           
        }
    }
}