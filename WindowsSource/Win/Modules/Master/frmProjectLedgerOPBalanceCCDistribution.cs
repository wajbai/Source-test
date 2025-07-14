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
    public partial class frmProjectLedgerOPBalanceCCDistribution : frmFinanceBaseAdd
    {
        #region Variable Declaration
        ResultArgs resultArgs = null;
        #endregion

        #region Properites
       
        private DataTable dtallpurposesccDistribution = null;
        private DataTable dtAllPurposesCCDistribution
        {
            set { dtallpurposesccDistribution = value; }
            get { return dtallpurposesccDistribution; }
        }

        private DataTable dtpurposeccDistribution = null;
        private DataTable dtPurposeCCDistribution
        {
            set { dtpurposeccDistribution = value; }
            get { return dtpurposeccDistribution; }
        }
                        
        private double PurposeCCDistributionSummary
        {
            get { return this.UtilityMember.NumberSet.ToDouble(colAmount.SummaryItem.SummaryValue.ToString()); }
        }

        private Int32 projectid = 0;
        private Int32 ProjectId
        {
            get { return projectid; }
            set { projectid = value; }
        }

        private Int32 ledgerid = 0;
        private Int32 LedgerId
        {
            get { return ledgerid; }
            set { ledgerid = value; }
        }

        private string transmode = string.Empty;
        private string TransMode
        {
            get { return transmode; }
            set { transmode = value; }
        }

        private double distributionAmount = 0;
        private double DistributionAmount
        {
            set { distributionAmount = value; }
            get { return distributionAmount; }
        }

        private Int32 CCId
        {
            get
            {
                int ccid = gvProjectLedgerCostCentre.GetFocusedRowCellValue(colCCName) != null ? this.UtilityMember.NumberSet.ToInteger(gvProjectLedgerCostCentre.GetFocusedRowCellValue(colCCName).ToString()) : 0;
                return ccid;
            }
        }
                
        private double CCAmountt
        {
            get
            {
                double ccAmount = gvProjectLedgerCostCentre.GetFocusedRowCellValue(colAmount) != null ? this.UtilityMember.NumberSet.ToDouble(gvProjectLedgerCostCentre.GetFocusedRowCellValue(colAmount).ToString()) : 0;
                return ccAmount;
            }
        }
        

        private bool TransacationGridNewItem
        {
            set
            {
                if (value)
                {
                    DataTable dtBind = gcProjectLedgerCostCentre.DataSource as DataTable;
                    dtBind.Rows.Add(dtBind.NewRow());
                    gcProjectLedgerCostCentre.DataSource = dtBind;
                    gvProjectLedgerCostCentre.MoveNext();
                    gvProjectLedgerCostCentre.FocusedColumn = colCCName;
                    gvProjectLedgerCostCentre.ShowEditor();
                }
            }
        }

        #endregion

        #region Constructor
        public frmProjectLedgerOPBalanceCCDistribution()
        {
            InitializeComponent();
            RealColumnEditTransAmount();
        }

        public frmProjectLedgerOPBalanceCCDistribution(int distributeProjectId, int distributeLedgerId, double distributeAmount,
            string distributeLedgerName, DataTable dtalldistributeCC, string isdistributePurposeTransMode)
            : this()
        {
            projectid = distributeProjectId;
            ledgerid = distributeLedgerId;
            DistributionAmount = distributeAmount;
            dtallpurposesccDistribution = dtalldistributeCC;
            TransMode = isdistributePurposeTransMode;
                        
            //Assign given purpose distribution amount
            dtallpurposesccDistribution.DefaultView.RowFilter = "LEDGER_ID = " + distributeLedgerId + " AND AMOUNT >0" ;
            dtpurposeccDistribution = dtallpurposesccDistribution.DefaultView.ToTable();
            dtallpurposesccDistribution.DefaultView.RowFilter = string.Empty;

            lblLedgerAmount.Text = this.UtilityMember.NumberSet.ToNumber(distributeAmount) + " " + TransMode;
            lblCCName.Text = distributeLedgerName;
        }
        #endregion

        #region Events
        private void frmTransactionCostCenter_Load(object sender, EventArgs e)
        {
            gvProjectLedgerCostCentre.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            BindCostCenter();
            AssignValues();
        }

        private void RealColumnEditTransAmount()
        {
            colAmount.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditTransAmount_EditValueChanged);
            this.gvProjectLedgerCostCentre.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvProjectLedgerCostCentre.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colAmount)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvProjectLedgerCostCentre.ShowEditorByMouse();
                    }));
                }
            };
        }

        private void RealColumnEditTransAmount_EditValueChanged(object sender, System.EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvProjectLedgerCostCentre.PostEditor();
            gvProjectLedgerCostCentre.UpdateCurrentRow();
            if (gvProjectLedgerCostCentre.ActiveEditor == null)
            {
                gvProjectLedgerCostCentre.ShowEditor();
            }

            TextEdit txtTransAmount = edit as TextEdit;
            int grpCounts = (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.DecimalPlaces) + 1);
            if (txtTransAmount.Text.Length > grpCounts && txtTransAmount.SelectionLength == txtTransAmount.Text.Length)
                txtTransAmount.Select(txtTransAmount.Text.Length - grpCounts, 0);
        }

        private void gcProjectLedgerCostCentre_ProcessGridKey(object sender, KeyEventArgs e)
        {
            bool CanFocusOk = false;
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) && gvProjectLedgerCostCentre.FocusedColumn == colAmount)
            {
                if (CCId == 0 && CCAmountt == 0 && gvProjectLedgerCostCentre.IsFirstRow) { this.Close(); }
                double dAmt = this.UtilityMember.NumberSet.ToDouble(DistributionAmount.ToString());

                if (dAmt == PurposeCCDistributionSummary)
                {
                    if (gvProjectLedgerCostCentre.IsLastRow) { CanFocusOk = true; }
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

                if (dAmt != PurposeCCDistributionSummary )
                {
                    if (CCId > 0 && CCAmountt > 0 && gvProjectLedgerCostCentre.IsLastRow)
                    {
                        TransacationGridNewItem = true;
                        if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                        {
                            e.SuppressKeyPress = true;
                        }
                    }
                    else if (CCId > 0 && CCAmountt > 0 && !gvProjectLedgerCostCentre.IsLastRow)
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
            gvProjectLedgerCostCentre.MoveNext();
            gvProjectLedgerCostCentre.FocusedColumn = colCCName;
            gvProjectLedgerCostCentre.ShowEditor();
        }

        private void frmTransactionCostCenter_Shown(object sender, EventArgs e)
        {
            gcProjectLedgerCostCentre.Select();
            gvProjectLedgerCostCentre.FocusedRowHandle = DevExpress.XtraGrid.GridControl.NewItemRowHandle;
            gvProjectLedgerCostCentre.FocusedColumn = gvProjectLedgerCostCentre.VisibleColumns[0];
            gvProjectLedgerCostCentre.ShowEditor();
        }

        private void rglkpCostCenter_Leave(object sender, EventArgs e)
        {
            double CurrentAmt = this.UtilityMember.NumberSet.ToDouble(gvProjectLedgerCostCentre.GetFocusedRowCellValue(colAmount).ToString());
            if (CCId > 0)
            {
                gvProjectLedgerCostCentre.UpdateCurrentRow();
                gvProjectLedgerCostCentre.UpdateTotalSummary();
                if ((this.UtilityMember.NumberSet.ToDecimal(DistributionAmount.ToString()) > 0) && (PurposeCCDistributionSummary < this.UtilityMember.NumberSet.ToDouble(DistributionAmount.ToString())))
                {
                    CurrentAmt = this.UtilityMember.NumberSet.ToDouble(gvProjectLedgerCostCentre.GetFocusedRowCellValue(colAmount).ToString());
                    double Amount = this.UtilityMember.NumberSet.ToDouble(DistributionAmount.ToString()) - PurposeCCDistributionSummary;
                    gvProjectLedgerCostCentre.SetFocusedRowCellValue(colAmount, Amount.ToString());

                    if (dtpurposeccDistribution != null && dtpurposeccDistribution.Rows.Count > 0)
                    {
                        Amount = this.UtilityMember.NumberSet.ToDouble(DistributionAmount.ToString()) - (PurposeCCDistributionSummary - CurrentAmt);
                        gvProjectLedgerCostCentre.SetFocusedRowCellValue(colAmount, Amount.ToString());
                    }
                }
                else if ((PurposeCCDistributionSummary > this.UtilityMember.NumberSet.ToDouble(DistributionAmount.ToString())))
                {
                    if (dtpurposeccDistribution != null && dtpurposeccDistribution.Rows.Count > 0)
                    {
                        double Amount = (this.UtilityMember.NumberSet.ToDouble(DistributionAmount.ToString())) - (PurposeCCDistributionSummary - CurrentAmt);
                        if (Amount > 0)
                            gvProjectLedgerCostCentre.SetFocusedRowCellValue(colAmount, Amount.ToString());
                    }
                }

            }
        }

        private void frmTransactionCostCenter_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Alt | Keys.T))
            {
                frmCostCentreAdd costcentre = new frmCostCentreAdd((int)AddNewRow.NewRow, ProjectId);
                costcentre.ShowDialog();
                if (costcentre.DialogResult == DialogResult.OK)
                {
                    BindCostCenter();
                }
            }
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
                    costCenterSystem.ProjectId = ProjectId;
                    costCenterSystem.LedgerId = LedgerId;
                    resultArgs = costCenterSystem.FetchforLookUpDetails();
                    rglkpCC.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        resultArgs.DataSource.Table.DefaultView.Sort = costCenterSystem.AppSchema.CostCentre.COST_CENTRE_NAMEColumn.ColumnName;
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
            if (dtpurposeccDistribution != null)
            {
                {
                    dtpurposeccDistribution.Rows.Add(dtpurposeccDistribution.NewRow());
                }
                gcProjectLedgerCostCentre.DataSource = dtpurposeccDistribution;
            }
            else
            {
                MessageRender.ShowMessage("Purpose distribution data source is empty");
            }
        }

        private void SetGridFocus()
        {
            gcProjectLedgerCostCentre.Select();
            gvProjectLedgerCostCentre.MoveFirst();
            gvProjectLedgerCostCentre.FocusedColumn = colCCName;
            gvProjectLedgerCostCentre.ShowEditor();
        }

        private bool IsValidCCDistribution()
        {
            bool isValid = true;
            
            if (!IsValidDistributionGrid())
            {
                isValid = false;
            }
            else if (DistributionAmount < PurposeCCDistributionSummary)
            {
                isValid = false;
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VoucherCostCentre.VOUCHER_COST_CENTRE_ALLOCATION_AMOUNT_GREATER));
                SetGridFocus();
            }
            else if (DistributionAmount > PurposeCCDistributionSummary)
            {
                isValid = false;
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VoucherCostCentre.VOUCHER_COST_CENTRE_ALLOCATION_AMOUNT_LESS));
                SetGridFocus();
            }
            return isValid;
        }

        private bool IsValidDistributionGrid()
        {
            DataTable dt = gcProjectLedgerCostCentre.DataSource as DataTable;

            int Id = 0;
            decimal Amt = 0;
            int RowPosition = 0;
            bool isValid = false;
            //string validateMessage = "Required Information not filled";
            string validateMessage = this.GetMessage(MessageCatalog.Master.Transaction.TRANS_COSTCENTER_VALIDATION_MESSAGE);
            if (dt != null)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "(COST_CENTRE_ID > 0 OR AMOUNT > 0)";
                gvProjectLedgerCostCentre.FocusedColumn = colCCName;
                if (dv.Count > 0)
                {
                    isValid = true;
                    foreach (DataRowView drTrans in dv)
                    {
                        Id = this.UtilityMember.NumberSet.ToInteger(drTrans["COST_CENTRE_ID"].ToString());
                        Amt = this.UtilityMember.NumberSet.ToDecimal(drTrans["AMOUNT"].ToString());

                        if ((Id == 0 || Amt == 0))
                        {
                            if (Id == 0)
                            {
                                //validateMessage = "Required Information not filled, Ledger is empty";
                                validateMessage = this.GetMessage(MessageCatalog.Master.Transaction.TRANS_COSTCENTER_NAME_EMPTY);
                                gvProjectLedgerCostCentre.FocusedColumn = colCCName;
                            }
                            if (Amt == 0)
                            {
                                //validateMessage = "Required Information not filled, Amount is empty";
                                validateMessage = this.GetMessage(MessageCatalog.Master.Transaction.TRANS_COSTCENTER_AMOUNT_EMPTY);
                                gvProjectLedgerCostCentre.FocusedColumn = colAmount;
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
                    gvProjectLedgerCostCentre.CloseEditor();
                    gvProjectLedgerCostCentre.FocusedRowHandle = gvProjectLedgerCostCentre.GetRowHandle(RowPosition);
                    gvProjectLedgerCostCentre.ShowEditor();
                }
            }

            return isValid;
        }

        private void IncludeLedgerCCDistribution()
        {
            try
            {
                bool isValid = true;
                DataTable dtCCDistribution = gcProjectLedgerCostCentre.DataSource as DataTable;
                dtCCDistribution.DefaultView.RowFilter = "COST_CENTRE_ID > 0 AND AMOUNT > 0";
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

                if ( isValid)
                {
                    //Remove current Ledger all distribution rows
                    dtallpurposesccDistribution.DefaultView.RowFilter = "LEDGER_ID <> " + ledgerid;
                    dtallpurposesccDistribution = dtallpurposesccDistribution.DefaultView.ToTable();
                    dtallpurposesccDistribution.DefaultView.RowFilter = string.Empty;

                    //Attach current purpose all distribution rows from grid
                    if (isValid)
                    {
                        foreach (DataRow dr in dtCCDistributionAffected.Rows)
                        {
                            dr.BeginEdit();
                            dr["PROJECT_ID"] = ProjectId;
                            dr["LEDGER_ID"] = ledgerid;
                            dr["TRANS_MODE"] = TransMode;
                            dr.EndEdit();
                        }
                        dtCCDistributionAffected.AcceptChanges();
                        dtallpurposesccDistribution.Merge(dtCCDistributionAffected);
                    }
                    this.ReturnValue = dtallpurposesccDistribution;
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private bool IsValidRow()
        {
            bool IsCostCentreValid = false;
            try
            {
                IsCostCentreValid = (CCId > 0 && CCAmountt > 0);
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            return IsCostCentreValid;
        }

        private void DeleteCostcentre()
        {
            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                gvProjectLedgerCostCentre.DeleteRow(gvProjectLedgerCostCentre.FocusedRowHandle);
                if (gvProjectLedgerCostCentre.RowCount == 0)
                {
                    //gvPurposeCC.AddNewRow();
                    AssignValues();
                }
            }
        }
                
        #endregion

        

       

    }
}