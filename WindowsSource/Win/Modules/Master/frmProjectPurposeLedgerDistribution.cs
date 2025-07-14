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
    public partial class frmProjectPurposeLedgerDistribution : frmFinanceBaseAdd
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

        private Int32 purposeid = 0;
        private Int32 PurposeId
        {
            get { return purposeid; }
            set { purposeid = value; }
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
                int ccid = gvPurposeCC.GetFocusedRowCellValue(colCCName) != null ? this.UtilityMember.NumberSet.ToInteger(gvPurposeCC.GetFocusedRowCellValue(colCCName).ToString()) : 0;
                return ccid;
            }
        }
                
        private double CCAmountt
        {
            get
            {
                double ccAmount = gvPurposeCC.GetFocusedRowCellValue(colAmount) != null ? this.UtilityMember.NumberSet.ToDouble(gvPurposeCC.GetFocusedRowCellValue(colAmount).ToString()) : 0;
                return ccAmount;
            }
        }
        

        private bool TransacationGridNewItem
        {
            set
            {
                if (value)
                {
                    DataTable dtBind = gcPurposeCostCentre.DataSource as DataTable;
                    dtBind.Rows.Add(dtBind.NewRow());
                    gcPurposeCostCentre.DataSource = dtBind;
                    gvPurposeCC.MoveNext();
                    gvPurposeCC.FocusedColumn = colCCName;
                    gvPurposeCC.ShowEditor();
                }
            }
        }

        #endregion

        #region Constructor
        public frmProjectPurposeLedgerDistribution()
        {
            InitializeComponent();
            RealColumnEditTransAmount();
        }

        public frmProjectPurposeLedgerDistribution(int distributeProjectId, int distributePurposeId, double distributeAmount,
            string distributePurposeName, DataTable dtalldistributePurposes, string isdistributePurposeTransMode)
            : this()
        {
            projectid = distributeProjectId;
            purposeid = distributePurposeId;
            DistributionAmount = distributeAmount;
            dtallpurposesccDistribution = dtalldistributePurposes;
            TransMode = isdistributePurposeTransMode;
                        
            //Assign given purpose distribution amount
            dtallpurposesccDistribution.DefaultView.RowFilter = "CONTRIBUTION_ID = " + distributePurposeId;
            dtpurposeccDistribution = dtallpurposesccDistribution.DefaultView.ToTable();
            dtallpurposesccDistribution.DefaultView.RowFilter = string.Empty;

            lblLedgerAmount.Text = this.UtilityMember.NumberSet.ToNumber(distributeAmount) + " " + TransMode;
            lblPurposeName.Text = distributePurposeName;
        }
        #endregion

        #region Events
        private void frmTransactionCostCenter_Load(object sender, EventArgs e)
        {
            gvPurposeCC.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            BindCostCenter();
            AssignValues();
        }

        private void RealColumnEditTransAmount()
        {
            colAmount.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditTransAmount_EditValueChanged);
            this.gvPurposeCC.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvPurposeCC.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colAmount)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvPurposeCC.ShowEditorByMouse();
                    }));
                }
            };
        }

        private void RealColumnEditTransAmount_EditValueChanged(object sender, System.EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvPurposeCC.PostEditor();
            gvPurposeCC.UpdateCurrentRow();
            if (gvPurposeCC.ActiveEditor == null)
            {
                gvPurposeCC.ShowEditor();
            }

            TextEdit txtTransAmount = edit as TextEdit;
            int grpCounts = (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.DecimalPlaces) + 1);
            if (txtTransAmount.Text.Length > grpCounts && txtTransAmount.SelectionLength == txtTransAmount.Text.Length)
                txtTransAmount.Select(txtTransAmount.Text.Length - grpCounts, 0);
        }
        
        private void gcCostCenter_ProcessGridKey(object sender, KeyEventArgs e)
        {
            bool CanFocusOk = false;
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) && gvPurposeCC.FocusedColumn == colAmount)
            {
                if (CCId == 0 && CCAmountt == 0 && gvPurposeCC.IsFirstRow) { this.Close(); }
                double dAmt = this.UtilityMember.NumberSet.ToDouble(DistributionAmount.ToString());

                if (dAmt == PurposeCCDistributionSummary)
                {
                    if (gvPurposeCC.IsLastRow) { CanFocusOk = true; }
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
                    if (CCId > 0 && CCAmountt > 0 && gvPurposeCC.IsLastRow)
                    {
                        TransacationGridNewItem = true;
                        if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                        {
                            e.SuppressKeyPress = true;
                        }
                    }
                    else if (CCId > 0 && CCAmountt > 0 && !gvPurposeCC.IsLastRow)
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
            gvPurposeCC.MoveNext();
            gvPurposeCC.FocusedColumn = colCCName;
            gvPurposeCC.ShowEditor();
        }

        private void frmTransactionCostCenter_Shown(object sender, EventArgs e)
        {
            gcPurposeCostCentre.Select();
            gvPurposeCC.FocusedRowHandle = DevExpress.XtraGrid.GridControl.NewItemRowHandle;
            gvPurposeCC.FocusedColumn = gvPurposeCC.VisibleColumns[0];
            gvPurposeCC.ShowEditor();
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
                    resultArgs = costCenterSystem.FetchforLookUpDetailsByProject();
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
                //if (dtpurposeccDistribution.Rows.Count == 0)
                {
                    dtpurposeccDistribution.Rows.Add(dtpurposeccDistribution.NewRow());
                }
                gcPurposeCostCentre.DataSource = dtpurposeccDistribution;
            }
            else
            {
                MessageRender.ShowMessage("Purpose distribution data source is empty");
            }
        }

        private void SetGridFocus()
        {
            gcPurposeCostCentre.Select();
            gvPurposeCC.MoveFirst();
            gvPurposeCC.FocusedColumn = colCCName;
            gvPurposeCC.ShowEditor();
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
            DataTable dt = gcPurposeCostCentre.DataSource as DataTable;

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
                gvPurposeCC.FocusedColumn = colCCName;
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
                                gvPurposeCC.FocusedColumn = colCCName;
                            }
                            if (Amt == 0)
                            {
                                //validateMessage = "Required Information not filled, Amount is empty";
                                validateMessage = this.GetMessage(MessageCatalog.Master.Transaction.TRANS_COSTCENTER_AMOUNT_EMPTY);
                                gvPurposeCC.FocusedColumn = colAmount;
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
                    gvPurposeCC.CloseEditor();
                    gvPurposeCC.FocusedRowHandle = gvPurposeCC.GetRowHandle(RowPosition);
                    gvPurposeCC.ShowEditor();
                }
            }

            return isValid;
        }

        private void SavePurposeDistribution()
        {
            try
            {
                bool isValid = true;
                DataTable dtCCDistribution = gcPurposeCostCentre.DataSource as DataTable;
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
                    //Remove current purpose all distribution rows
                    dtallpurposesccDistribution.DefaultView.RowFilter = "CONTRIBUTION_ID <> " + purposeid;
                    dtallpurposesccDistribution = dtallpurposesccDistribution.DefaultView.ToTable();
                    dtallpurposesccDistribution.DefaultView.RowFilter = string.Empty;

                    //Attach current purpose all distribution rows from grid
                    if (isValid)
                    {
                        foreach (DataRow dr in dtCCDistributionAffected.Rows)
                        {
                            dr.BeginEdit();
                            dr["PROJECT_ID"] = ProjectId;
                            dr["CONTRIBUTION_ID"] = PurposeId;
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
                
        #endregion

        private void rglkpCostCenter_Leave(object sender, EventArgs e)
        {
            double CurrentAmt = this.UtilityMember.NumberSet.ToDouble(gvPurposeCC.GetFocusedRowCellValue(colAmount).ToString());
            if (CCId > 0)
            {
                gvPurposeCC.UpdateCurrentRow();
                gvPurposeCC.UpdateTotalSummary();
                if ((this.UtilityMember.NumberSet.ToDecimal(DistributionAmount.ToString()) > 0) && (PurposeCCDistributionSummary < this.UtilityMember.NumberSet.ToDouble(DistributionAmount.ToString())))
                {
                    CurrentAmt = this.UtilityMember.NumberSet.ToDouble(gvPurposeCC.GetFocusedRowCellValue(colAmount).ToString());
                    double Amount = this.UtilityMember.NumberSet.ToDouble(DistributionAmount.ToString()) - PurposeCCDistributionSummary;
                    gvPurposeCC.SetFocusedRowCellValue(colAmount, Amount.ToString());

                    if (dtpurposeccDistribution != null && dtpurposeccDistribution.Rows.Count > 0)
                    {
                        Amount = this.UtilityMember.NumberSet.ToDouble(DistributionAmount.ToString()) - (PurposeCCDistributionSummary - CurrentAmt);
                        gvPurposeCC.SetFocusedRowCellValue(colAmount, Amount.ToString());
                    }
                }
                else if ((PurposeCCDistributionSummary > this.UtilityMember.NumberSet.ToDouble(DistributionAmount.ToString())))
                {
                    if (dtpurposeccDistribution != null && dtpurposeccDistribution.Rows.Count > 0)
                    {
                        double Amount = (this.UtilityMember.NumberSet.ToDouble(DistributionAmount.ToString())) - (PurposeCCDistributionSummary - CurrentAmt);
                        if (Amount > 0)
                            gvPurposeCC.SetFocusedRowCellValue(colAmount, Amount.ToString());
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
            SavePurposeDistribution();
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

        private void DeleteCostcentre()
        {
            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                gvPurposeCC.DeleteRow(gvPurposeCC.FocusedRowHandle);
                if (gvPurposeCC.RowCount == 0)
                {
                    //gvPurposeCC.AddNewRow();
                    AssignValues();
                }
            }
        }

    }
}