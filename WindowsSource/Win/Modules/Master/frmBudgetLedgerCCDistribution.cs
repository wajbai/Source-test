using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;

using Bosco.DAO;
using Bosco.DAO.Data;
using Bosco.Utility;
using Bosco.Model.Transaction;
using Bosco.Model.UIModel;
using System.Runtime.Serialization;
using DevExpress.XtraGrid;
using ACPP.Modules.Master;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace ACPP.Modules.Master
{
    public partial class frmBudgetLedgerCCDistribution : frmFinanceBaseAdd
    {
        #region Variable Declaration
        ResultArgs resultArgs = null;
        #endregion

        #region Properites
               
        private DataTable dtAllBudgetLedgerCCDistribution
        {
             get; set; 
        }
                
        private DataTable dtBudgetLedgerCCDistribution
        {
             get; set; 
        }
                        
        private double BudgetLedgerProposedCCDistributionSummary
        {
            get {
                double rtn = 0;
                if (colProposedAmount.SummaryItem.SummaryValue != null)
                {
                    rtn = UtilityMember.NumberSet.ToDouble(colProposedAmount.SummaryItem.SummaryValue.ToString());
                }

                return rtn;    
            }
        }

        private double BudgetLedgerApprovedCCDistributionSummary
        {
            get
            {
                double rtn = 0;
                if (colApprovedAmount.SummaryItem.SummaryValue != null)
                {
                    rtn = UtilityMember.NumberSet.ToDouble(colApprovedAmount.SummaryItem.SummaryValue.ToString());
                }

                return rtn;
            }
        }

        private bool IsDistributeByBudgetLedger
        {
            get;
            set; 
        }
        
        private Int32 BudgetId
        {
             get; set; 
        }

        private string ProjectIds
        {
            get;
            set;
        }

        private Int32 DistributeSouceId
        {
             get; set; 
        }
                
        private string DistributeSouceName
        {
            get;
            set; 
        }

        private string TransMode
        {
            get;
            set; 
        }

        private double DistributeProposedAmount
        {
            get;
            set; 
        }

        private double DistributeApprovedAmount
        {
            get;
            set; 
        }

        private Int32 CCId
        {
            get
            {
                int ccid = gvBudgetLedgerCostCentre.GetFocusedRowCellValue(colCCName) != null ? this.UtilityMember.NumberSet.ToInteger(gvBudgetLedgerCostCentre.GetFocusedRowCellValue(colCCName).ToString()) : 0;
                return ccid;
            }
        }

        private string CCName
        {
            get
            {
                string rtn = string.Empty;
                if (rglkpCC.GetRowByKeyValue(CCId) != null)
                {
                    using (BudgetSystem budgetsys = new BudgetSystem())
                    {
                        DataRowView drvledger = rglkpCC.GetRowByKeyValue(CCId) as DataRowView;
                        rtn = drvledger[budgetsys.AppSchema.BudgetCostCentre.COST_CENTRE_NAMEColumn.ColumnName].ToString();
                    }
                }
                return rtn;
            }

        }
                
        private double CCProposedAmountt
        {
            get
            {
                double ccPropsedAmount = gvBudgetLedgerCostCentre.GetFocusedRowCellValue(colProposedAmount) != null ? this.UtilityMember.NumberSet.ToDouble(gvBudgetLedgerCostCentre.GetFocusedRowCellValue(colProposedAmount).ToString()) : 0;
                return ccPropsedAmount;
            }
        }

        private double CCApprovedAmountt
        {
            get
            {
                double CCApprovedAmountt = gvBudgetLedgerCostCentre.GetFocusedRowCellValue(colApprovedAmount) != null ? this.UtilityMember.NumberSet.ToDouble(gvBudgetLedgerCostCentre.GetFocusedRowCellValue(colApprovedAmount).ToString()) : 0;
                return CCApprovedAmountt;
            }
        }
        
        private bool TransacationGridNewItem
        {
            set
            {
                if (value)
                {
                    DataTable dtBind = gcBudgetLedgerCostCentre.DataSource as DataTable;
                    dtBind.Rows.Add(dtBind.NewRow());
                    gcBudgetLedgerCostCentre.DataSource = dtBind;
                    gvBudgetLedgerCostCentre.MoveNext();
                    gvBudgetLedgerCostCentre.FocusedColumn = colCCName;
                    gvBudgetLedgerCostCentre.ShowEditor();
                }
            }
        }

        #endregion

        #region Constructor
        public frmBudgetLedgerCCDistribution()
        {
            InitializeComponent();
            RealColumnEditTransAmount();
        }

        public frmBudgetLedgerCCDistribution(bool isdistributeByBudgetLedger, Int32 distributeBudgetid, string projectids, Int32 distributeId, string distributeSouceName,
                                                 double distributePropsedAmount, double distributeApprovedAmount, string isdistributeBudgetLedgerTransMode, DataTable dtallBudgetLedgerCC)
            : this()
        {
            IsDistributeByBudgetLedger = isdistributeByBudgetLedger;
            BudgetId = distributeBudgetid;
            ProjectIds = projectids;
            DistributeSouceId = distributeId;
            DistributeSouceName = distributeSouceName;
            DistributeProposedAmount= distributePropsedAmount;
            DistributeApprovedAmount= distributeApprovedAmount;
            TransMode = isdistributeBudgetLedgerTransMode;
            dtAllBudgetLedgerCCDistribution = dtallBudgetLedgerCC;
            
            //Assign given ledger distribution amount
            using (BudgetSystem budgetsystem = new BudgetSystem())
            {
                if (IsDistributeByBudgetLedger)
                {
                    dtAllBudgetLedgerCCDistribution.DefaultView.RowFilter = budgetsystem.AppSchema.BudgetCostCentre.LEDGER_IDColumn.ColumnName + " = " + distributeId +
                                             " AND " + budgetsystem.AppSchema.BudgetCostCentre.TRANS_MODEColumn.ColumnName + " = '"  + TransMode + "' AND "+ 
                                             "(" + budgetsystem.AppSchema.BudgetCostCentre.PROPOSED_AMOUNTColumn.ColumnName + " > 0 OR " +
                                             budgetsystem.AppSchema.BudgetCostCentre.APPROVED_AMOUNTColumn.ColumnName + " > 0)";
                }
                else
                {
                    if (!dtAllBudgetLedgerCCDistribution.Columns.Contains(budgetsystem.AppSchema.ReportNewBudgetProject.ACC_YEAR_IDColumn.ColumnName))
                    {
                        dtAllBudgetLedgerCCDistribution.Columns.Add(budgetsystem.AppSchema.ReportNewBudgetProject.ACC_YEAR_IDColumn.ColumnName, typeof(System.Int32));
                    }
                    dtAllBudgetLedgerCCDistribution.DefaultView.RowFilter = budgetsystem.AppSchema.BudgetCostCentre.LEDGER_IDColumn.ColumnName + " = " + distributeId;
                }
                dtBudgetLedgerCCDistribution = dtAllBudgetLedgerCCDistribution.DefaultView.ToTable();
                dtAllBudgetLedgerCCDistribution.DefaultView.RowFilter = string.Empty;

                //Set Bind column name
                lcApprovedAmtCaption.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lblApprovedAmt.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                lcMove.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                if (IsDistributeByBudgetLedger)
                {
                    lcApprovedAmtCaption.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    lblApprovedAmt.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;

                    lcMove.Visibility = DistributeApprovedAmount > 0 ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
                }
            }

            lblAlocationLedgerName.Text = distributeSouceName;
            lblProposedAmt.Text = this.UtilityMember.NumberSet.ToNumber(DistributeProposedAmount) + " " + TransMode;
            lblApprovedAmt.Text =  this.UtilityMember.NumberSet.ToNumber(DistributeApprovedAmount) + " " + TransMode;
            
            if (!IsDistributeByBudgetLedger)
            {
                this.Text = this.AppSetting.ConsiderBudgetNewProject == 0? "New Project" : "Developmental Project";
                this.Text += " - Distribute Total Cost";
                colApprovedAmount.Visible = false;
            }
        }
        #endregion

        #region Events
        private void frmTransactionCostCenter_Load(object sender, EventArgs e)
        {
            gvBudgetLedgerCostCentre.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            BindCostCenter();
            AssignValues();
        }

        private void RealColumnEditTransAmount()
        {
            colProposedAmount.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditTransAmount_EditValueChanged);
            this.gvBudgetLedgerCostCentre.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvBudgetLedgerCostCentre.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colProposedAmount)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvBudgetLedgerCostCentre.ShowEditorByMouse();
                    }));
                }
            };
        }

        private void RealColumnEditTransAmount_EditValueChanged(object sender, System.EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvBudgetLedgerCostCentre.PostEditor();
            gvBudgetLedgerCostCentre.UpdateCurrentRow();
            if (gvBudgetLedgerCostCentre.ActiveEditor == null)
            {
                gvBudgetLedgerCostCentre.ShowEditor();
            }

            TextEdit txtTransAmount = edit as TextEdit;
            int grpCounts = (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.DecimalPlaces) + 1);
            if (txtTransAmount.Text.Length > grpCounts && txtTransAmount.SelectionLength == txtTransAmount.Text.Length)
                txtTransAmount.Select(txtTransAmount.Text.Length - grpCounts, 0);
        }

        private void gcProjectLedgerCostCentre_ProcessGridKey(object sender, KeyEventArgs e)
        {
            bool CanFocusOk = false;
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) && 
                ((gvBudgetLedgerCostCentre.FocusedColumn == colProposedAmount && !IsDistributeByBudgetLedger) ||  
                 (gvBudgetLedgerCostCentre.FocusedColumn == colApprovedAmount && IsDistributeByBudgetLedger)))
            {
                if (CCId == 0 && CCProposedAmountt == 0 && CCApprovedAmountt == 0 && gvBudgetLedgerCostCentre.IsFirstRow) { this.Close(); }
                double dAmtProposed = this.UtilityMember.NumberSet.ToDouble(DistributeProposedAmount.ToString());
                double dAmtApproved = this.UtilityMember.NumberSet.ToDouble(DistributeApprovedAmount.ToString());

                if (dAmtProposed == BudgetLedgerProposedCCDistributionSummary && dAmtApproved == BudgetLedgerApprovedCCDistributionSummary)
                {
                    if (gvBudgetLedgerCostCentre.IsLastRow) { CanFocusOk = true; }
                    else {
                        MoveNextRow();
                        e.SuppressKeyPress = true;
                    }
                }
                if (CanFocusOk)
                {
                    btnOk.Select();
                    btnOk.Focus();
                    btnOk.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.True;
                }

                if (dAmtProposed != BudgetLedgerProposedCCDistributionSummary || dAmtApproved!= BudgetLedgerApprovedCCDistributionSummary)
                {
                    if (CCId > 0 && (CCProposedAmountt > 0 || CCApprovedAmountt > 0) && gvBudgetLedgerCostCentre.IsLastRow)
                    {
                        TransacationGridNewItem = true;
                        if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                        {
                            e.SuppressKeyPress = true;
                        }
                    }
                    else if (CCId > 0 && (CCProposedAmountt > 0 || CCApprovedAmountt > 0) && !gvBudgetLedgerCostCentre.IsLastRow)
                    {
                        MoveNextRow();
                        e.SuppressKeyPress = true;
                    }
                    else
                    {
                        btnOk.Select();
                        btnOk.Focus();
                        btnOk.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.True;
                    }
                }
            }
        }

        private void MoveNextRow()
        {
            gvBudgetLedgerCostCentre.MoveNext();
            gvBudgetLedgerCostCentre.FocusedColumn = colCCName;
            gvBudgetLedgerCostCentre.ShowEditor();
        }

        private void frmTransactionCostCenter_Shown(object sender, EventArgs e)
        {
            gcBudgetLedgerCostCentre.Select();
            gvBudgetLedgerCostCentre.FocusedRowHandle = DevExpress.XtraGrid.GridControl.NewItemRowHandle;
            gvBudgetLedgerCostCentre.FocusedColumn = gvBudgetLedgerCostCentre.VisibleColumns[0];
            gvBudgetLedgerCostCentre.ShowEditor();
        }

        private void rglkpCostCenter_Leave(object sender, EventArgs e)
        {
            double CurrentPropsedAmt = this.UtilityMember.NumberSet.ToDouble(gvBudgetLedgerCostCentre.GetFocusedRowCellValue(colProposedAmount).ToString());
            double CurrentApprovedAmt = this.UtilityMember.NumberSet.ToDouble(gvBudgetLedgerCostCentre.GetFocusedRowCellValue(colProposedAmount).ToString());
            if (CCId > 0)
            {
                //For Propsed Amount
                if ((this.UtilityMember.NumberSet.ToDecimal(DistributeProposedAmount.ToString()) > 0) &&
                    (BudgetLedgerProposedCCDistributionSummary < this.UtilityMember.NumberSet.ToDouble(DistributeProposedAmount.ToString())))
                {
                    CurrentPropsedAmt = this.UtilityMember.NumberSet.ToDouble(gvBudgetLedgerCostCentre.GetFocusedRowCellValue(colProposedAmount).ToString());
                    double Amount = this.UtilityMember.NumberSet.ToDouble(DistributeProposedAmount.ToString()) - BudgetLedgerProposedCCDistributionSummary;
                    gvBudgetLedgerCostCentre.SetFocusedRowCellValue(colProposedAmount, Amount.ToString());

                    if (dtBudgetLedgerCCDistribution != null && dtBudgetLedgerCCDistribution.Rows.Count > 0)
                    {
                        Amount = this.UtilityMember.NumberSet.ToDouble(DistributeProposedAmount.ToString()) - (BudgetLedgerProposedCCDistributionSummary - CurrentPropsedAmt);
                        gvBudgetLedgerCostCentre.SetFocusedRowCellValue(colProposedAmount, Amount.ToString());
                    }
                }
                else if ((BudgetLedgerProposedCCDistributionSummary > this.UtilityMember.NumberSet.ToDouble(DistributeProposedAmount.ToString())))
                {
                    if (dtBudgetLedgerCCDistribution != null && dtBudgetLedgerCCDistribution.Rows.Count > 0)
                    {
                        double Amount = (this.UtilityMember.NumberSet.ToDouble(DistributeProposedAmount.ToString())) - (BudgetLedgerProposedCCDistributionSummary - CurrentPropsedAmt);
                        if (Amount > 0)
                            gvBudgetLedgerCostCentre.SetFocusedRowCellValue(colProposedAmount, Amount.ToString());
                    }
                }

                //For Approved Amount
                if ((this.UtilityMember.NumberSet.ToDecimal(DistributeApprovedAmount.ToString()) > 0) &&
                    (BudgetLedgerApprovedCCDistributionSummary < this.UtilityMember.NumberSet.ToDouble(DistributeApprovedAmount.ToString())))
                {
                    CurrentApprovedAmt = this.UtilityMember.NumberSet.ToDouble(gvBudgetLedgerCostCentre.GetFocusedRowCellValue(colApprovedAmount).ToString());
                    double Amount = this.UtilityMember.NumberSet.ToDouble(DistributeApprovedAmount.ToString()) - BudgetLedgerApprovedCCDistributionSummary;
                    gvBudgetLedgerCostCentre.SetFocusedRowCellValue(colApprovedAmount, Amount.ToString());

                    if (dtBudgetLedgerCCDistribution != null && dtBudgetLedgerCCDistribution.Rows.Count > 0)
                    {
                        Amount = this.UtilityMember.NumberSet.ToDouble(DistributeApprovedAmount.ToString()) - (BudgetLedgerApprovedCCDistributionSummary - CurrentApprovedAmt);
                        gvBudgetLedgerCostCentre.SetFocusedRowCellValue(colApprovedAmount, Amount.ToString());
                    }
                }
                else if ((BudgetLedgerApprovedCCDistributionSummary > this.UtilityMember.NumberSet.ToDouble(DistributeApprovedAmount.ToString())))
                {
                    if (dtBudgetLedgerCCDistribution != null && dtBudgetLedgerCCDistribution.Rows.Count > 0)
                    {
                        double Amount = (this.UtilityMember.NumberSet.ToDouble(DistributeApprovedAmount.ToString())) - (BudgetLedgerApprovedCCDistributionSummary - CurrentApprovedAmt);
                        if (Amount > 0)
                            gvBudgetLedgerCostCentre.SetFocusedRowCellValue(colApprovedAmount, Amount.ToString());
                    }
                }
            }
        }

        private void frmTransactionCostCenter_KeyDown(object sender, KeyEventArgs e)
        {
            /*if (e.KeyData == (Keys.Alt | Keys.T))
            {
                frmCostCentreAdd costcentre = new frmCostCentreAdd((int)AddNewRow.NewRow, ProjectId);
                costcentre.ShowDialog();
                if (costcentre.DialogResult == DialogResult.OK)
                {
                    BindCostCenter();
                }
            }*/
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            IncludeLedgerCCDistribution();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rbeDeleteCostCentre_Click(object sender, EventArgs e)
        {
            DeleteCostcentre();
        }

        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            if (KeyData == (Keys.Alt | Keys.D))
            {
                DeleteCostcentre();
            }
            return base.ProcessCmdKey(ref msg, KeyData);
        }
        #endregion

        #region Methods
        
        private void BindCostCenter()
        {
            try
            {
                using (CostCentreSystem costCenterSystem = new CostCentreSystem())
                {
                    costCenterSystem.ProjectIds = ProjectIds;
                    costCenterSystem.LedgerId = 0; // DistributeSouceId;
                    if (IsDistributeByBudgetLedger && this.AppSetting.CostCeterMapping == 1)
                    {
                        costCenterSystem.LedgerId = DistributeSouceId;
                        resultArgs = costCenterSystem.FetchforLookUpDetailsByProjectIds();
                    }
                    else
                    {
                        resultArgs = costCenterSystem.FetchMappedProjectCostCentre();
                    }
                    rglkpCC.DataSource = null;

                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        resultArgs.DataSource.Table.DefaultView.Sort = costCenterSystem.AppSchema.CostCentre.ABBREVATIONColumn.ColumnName + ","+ 
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

        private void AssignValues()
        {
            if (dtBudgetLedgerCCDistribution != null)
            {
                dtBudgetLedgerCCDistribution.Rows.Add(dtBudgetLedgerCCDistribution.NewRow());
                gcBudgetLedgerCostCentre.DataSource = dtBudgetLedgerCCDistribution;
            }
            else
            {
                MessageRender.ShowMessage("Purpose distribution data source is empty");
            }
        }

        private void SetGridFocus()
        {
            gcBudgetLedgerCostCentre.Select();
            gvBudgetLedgerCostCentre.MoveFirst();
            gvBudgetLedgerCostCentre.FocusedColumn = colCCName;
            gvBudgetLedgerCostCentre.ShowEditor();
        }

        private bool IsValidCCDistribution()
        {
            
            bool isValid = true;
            
            if (!IsValidDistributionGrid())
            {
                isValid = false;
            }
            else if ((BudgetLedgerProposedCCDistributionSummary >0 && DistributeProposedAmount < BudgetLedgerProposedCCDistributionSummary) ||
                    (BudgetLedgerApprovedCCDistributionSummary >0 && DistributeApprovedAmount < BudgetLedgerApprovedCCDistributionSummary))
            {
                isValid = false;
                string message = this.GetMessage(MessageCatalog.Transaction.VoucherCostCentre.VOUCHER_COST_CENTRE_ALLOCATION_AMOUNT_GREATER);

                if (DistributeProposedAmount < BudgetLedgerProposedCCDistributionSummary)
                {
                    this.ShowMessageBox("Proposed " + message);
                }
                else
                {
                    this.ShowMessageBox("Approved " + message);
                }
                
                SetGridFocus();
            }
            else if ( (BudgetLedgerProposedCCDistributionSummary >0 && DistributeProposedAmount > BudgetLedgerProposedCCDistributionSummary) ||
                      (BudgetLedgerApprovedCCDistributionSummary >0 && DistributeApprovedAmount > BudgetLedgerApprovedCCDistributionSummary))
            {
                isValid = false;
                string message =  this.GetMessage(MessageCatalog.Transaction.VoucherCostCentre.VOUCHER_COST_CENTRE_ALLOCATION_AMOUNT_LESS);

                if (DistributeProposedAmount > BudgetLedgerProposedCCDistributionSummary)
                {
                    this.ShowMessageBox("Proposed " + message);
                }
                else 
                {
                    this.ShowMessageBox("Approved " + message);
                }
                SetGridFocus();
            }
            return isValid;
        }

        private bool IsValidDistributionGrid()
        {
            DataTable dt = gcBudgetLedgerCostCentre.DataSource as DataTable;

            int Id = 0;
            int RowPosition = 0;
            bool isValid = false;
            //string validateMessage = "Required Information not filled";
            string validateMessage = this.GetMessage(MessageCatalog.Master.Transaction.TRANS_COSTCENTER_VALIDATION_MESSAGE);
            if (dt != null)
            {
                using (BudgetSystem budgetsys = new BudgetSystem())
                {
                    DataView dv = new DataView(dt);

                    string filter = budgetsys.AppSchema.BudgetCostCentre.COST_CENTRE_IDColumn.ColumnName + " > 0 AND " +
                                                        budgetsys.AppSchema.BudgetCostCentre.PROPOSED_AMOUNTColumn.ColumnName + " > 0";
                    if (IsDistributeByBudgetLedger)
                    {
                        filter = budgetsys.AppSchema.BudgetCostCentre.COST_CENTRE_IDColumn.ColumnName + " > 0 AND " +
                                                   "(" + budgetsys.AppSchema.BudgetCostCentre.PROPOSED_AMOUNTColumn.ColumnName + " > 0 OR " +
                                                        budgetsys.AppSchema.BudgetCostCentre.APPROVED_AMOUNTColumn.ColumnName + " > 0 )";
                    }
                    dv.RowFilter = filter;
                    gvBudgetLedgerCostCentre.FocusedColumn = colCCName;

                    if (dv.Count > 0)
                    {
                        isValid = true;
                        foreach (DataRowView drTrans in dv)
                        {
                            Id = this.UtilityMember.NumberSet.ToInteger(drTrans[budgetsys.AppSchema.BudgetCostCentre.COST_CENTRE_IDColumn.ColumnName].ToString());
                            double AmtProposed = this.UtilityMember.NumberSet.ToDouble(drTrans[budgetsys.AppSchema.BudgetCostCentre.PROPOSED_AMOUNTColumn.ColumnName].ToString());
                            double AmtApproved = 0;
                            if (IsDistributeByBudgetLedger)
                            {
                                AmtApproved = this.UtilityMember.NumberSet.ToDouble(drTrans[budgetsys.AppSchema.BudgetCostCentre.APPROVED_AMOUNTColumn.ColumnName].ToString());
                            }

                            if (Id == 0 && (AmtProposed == 0 || AmtApproved==0))
                            {
                                if (Id == 0 && (AmtProposed > 0 || AmtApproved>0))
                                {
                                    //validateMessage = "Required Information not filled, Ledger is empty";
                                    validateMessage = this.GetMessage(MessageCatalog.Master.Transaction.TRANS_COSTCENTER_NAME_EMPTY);
                                    gvBudgetLedgerCostCentre.FocusedColumn = colCCName;
                                }
                                if (Id > 0 && (AmtProposed == 0 && AmtApproved == 0))
                                {
                                    //validateMessage = "Required Information not filled, Amount is empty";
                                    validateMessage = this.GetMessage(MessageCatalog.Master.Transaction.TRANS_COSTCENTER_AMOUNT_EMPTY);
                                    gvBudgetLedgerCostCentre.FocusedColumn = colProposedAmount;
                                }
                                isValid = false;
                                break;
                            }
                            RowPosition = RowPosition + 1;
                        }
                    }
                    else
                    {
                        isValid = true;
                    }

                    if (!isValid)
                    {
                        this.ShowMessageBox(validateMessage);
                        gvBudgetLedgerCostCentre.CloseEditor();
                        gvBudgetLedgerCostCentre.FocusedRowHandle = gvBudgetLedgerCostCentre.GetRowHandle(RowPosition);
                        gvBudgetLedgerCostCentre.ShowEditor();
                    }
                }
            }

            return isValid;
        }

        private void IncludeLedgerCCDistribution()
        {
            try
            {
                using (BudgetSystem budgetsys = new BudgetSystem())
                {
                    bool isValid = true;
                    DataTable dtCCDistribution = gcBudgetLedgerCostCentre.DataSource as DataTable;

                    string filter = budgetsys.AppSchema.BudgetCostCentre.COST_CENTRE_IDColumn.ColumnName + " > 0 AND " +
                                                        budgetsys.AppSchema.BudgetCostCentre.PROPOSED_AMOUNTColumn.ColumnName + " > 0";
                    if (IsDistributeByBudgetLedger)
                    {
                        filter = budgetsys.AppSchema.BudgetCostCentre.COST_CENTRE_IDColumn.ColumnName + " > 0 AND " +
                                                   "(" + budgetsys.AppSchema.BudgetCostCentre.PROPOSED_AMOUNTColumn.ColumnName + " > 0 OR " +
                                                        budgetsys.AppSchema.BudgetCostCentre.APPROVED_AMOUNTColumn.ColumnName + " > 0 )";
                    }

                    dtCCDistribution.DefaultView.RowFilter = filter;
                    DataTable dtCCDistributionAffected = dtCCDistribution.DefaultView.ToTable();
                    dtCCDistribution.DefaultView.RowFilter = string.Empty;

                    if (dtCCDistribution.Rows.Count == 1 && dtCCDistributionAffected.Rows.Count == 0)
                    {
                        isValid = true;
                    }
                    else if (!IsValidCCDistribution())
                    {
                        isValid = false;
                    }

                    if (isValid)
                    {
                        //Remove current Ledger all distribution rows
                        string affectedrows = budgetsys.AppSchema.BudgetCostCentre.LEDGER_IDColumn.ColumnName + " IN (" + DistributeSouceId + ") AND " + budgetsys.AppSchema.BudgetCostCentre.TRANS_MODEColumn.ColumnName + "='" + TransMode + "'";
                        DataRow[] results = dtAllBudgetLedgerCCDistribution.Select(affectedrows);
                        foreach (DataRow drAffectedRows in results)
                        {
                            drAffectedRows.Delete();
                        }
                        
                        dtAllBudgetLedgerCCDistribution = dtAllBudgetLedgerCCDistribution.DefaultView.ToTable();
                        dtAllBudgetLedgerCCDistribution.DefaultView.RowFilter = string.Empty;

                        //Attach current purpose all distribution rows from grid
                        if (isValid)
                        {
                            foreach (DataRow dr in dtCCDistributionAffected.Rows)
                            {
                                Int32 costcentreid = UtilityMember.NumberSet.ToInteger(dr[budgetsys.AppSchema.BudgetCostCentre.COST_CENTRE_IDColumn.ColumnName].ToString());
                                Double proposedamt = UtilityMember.NumberSet.ToDouble(dr[budgetsys.AppSchema.BudgetCostCentre.PROPOSED_AMOUNTColumn.ColumnName].ToString());
                                Double approvedamt = 0;

                                dr.BeginEdit();
                                dr[budgetsys.AppSchema.BudgetCostCentre.BUDGET_IDColumn.ColumnName] = BudgetId;
                                dr[budgetsys.AppSchema.BudgetCostCentre.LEDGER_IDColumn.ColumnName] = DistributeSouceId;
                                dr[budgetsys.AppSchema.BudgetCostCentre.LEDGER_NAMEColumn.ColumnName] = DistributeSouceName;
                                dr[budgetsys.AppSchema.BudgetCostCentre.TRANS_MODEColumn.ColumnName] = TransMode;

                                if (!IsDistributeByBudgetLedger)
                                {
                                    dr[budgetsys.AppSchema.ReportNewBudgetProject.ACC_YEAR_IDColumn.ColumnName] = UtilityMember.NumberSet.ToInteger(this.AppSetting.AccPeriodId);
                                }
                                else
                                {
                                    approvedamt = UtilityMember.NumberSet.ToDouble(dr[budgetsys.AppSchema.BudgetCostCentre.APPROVED_AMOUNTColumn.ColumnName].ToString());
                                    dr[budgetsys.AppSchema.BudgetCostCentre.APPROVED_AMOUNTColumn.ColumnName] = approvedamt;
                                }

                                if (rglkpCC.GetRowByKeyValue(costcentreid) != null)
                                {
                                    DataRowView drvledger = rglkpCC.GetRowByKeyValue(costcentreid) as DataRowView;
                                    dr[budgetsys.AppSchema.BudgetCostCentre.COST_CENTRE_NAMEColumn.ColumnName] = drvledger[budgetsys.AppSchema.BudgetCostCentre.COST_CENTRE_NAMEColumn.ColumnName].ToString();
                                }
                                dr.EndEdit();
                            }
                            dtCCDistributionAffected.AcceptChanges();
                            dtAllBudgetLedgerCCDistribution.Merge(dtCCDistributionAffected);
                        }
                        this.ReturnValue = dtAllBudgetLedgerCCDistribution;
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        //private bool IsValidRow()
        //{
        //    bool IsCostCentreValid = false;
        //    try
        //    {
        //        IsCostCentreValid = (CCId > 0 && DistributeApprovedAmount > 0);
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageRender.ShowMessage(ex.ToString(), true);
        //    }
        //    return IsCostCentreValid;
        //}

        private void DeleteCostcentre()
        {
            //if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            if (this.ShowConfirmationMessage("Are you sure to delete current row '" + CCName + "' ?",  MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                gvBudgetLedgerCostCentre.DeleteRow(gvBudgetLedgerCostCentre.FocusedRowHandle);
                if (gvBudgetLedgerCostCentre.RowCount == 0)
                {
                    //gvPurposeCC.AddNewRow();
                    AssignValues();
                }
            }
        }        
        #endregion

        private void peReplaceValue_Click(object sender, EventArgs e)
        {
            if (gcBudgetLedgerCostCentre.DataSource != null && gcBudgetLedgerCostCentre.DataSource != null)
            {
                if (ShowConfirmationMessage("Are you sure to assign Proposed Amount to Approved Amount ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == System.Windows.Forms.DialogResult.Yes)
                {
                    using (BudgetSystem budgetsystem = new BudgetSystem())
                    {
                        DataTable dtLedgerCC = gcBudgetLedgerCostCentre.DataSource as DataTable;
                        foreach (DataRow drledgercc in dtLedgerCC.Rows)
                        {
                            Int32 ccid =  this.UtilityMember.NumberSet.ToInteger(drledgercc[budgetsystem.AppSchema.BudgetCostCentre.COST_CENTRE_IDColumn.ColumnName].ToString());
                            if (ccid > 0)
                            {
                                drledgercc[budgetsystem.AppSchema.BudgetCostCentre.APPROVED_AMOUNTColumn.ColumnName] =
                                    this.UtilityMember.NumberSet.ToDecimal(drledgercc[budgetsystem.AppSchema.BudgetCostCentre.PROPOSED_AMOUNTColumn.ColumnName].ToString());
                            }
                        }

                        gcBudgetLedgerCostCentre.DataSource = dtLedgerCC;
                    }
                }
            }
            else
            {
                MessageRender.ShowMessage("Budget Cost Centres are not available");
            }
        }

    }
}