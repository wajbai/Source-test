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

namespace ACPP.Modules.Transaction
{
    public partial class frmTransactionCostCenter : frmFinanceBaseAdd
    {
        #region Variable Declaration
        ResultArgs resultArgs = null;
        private int ledgerId = 0;
        public DataTable dtValues = new DataTable();
        CostCentreSystem costCentre = new CostCentreSystem();
        #endregion

        #region Properites
        private double amt = 0;
        private double Amt
        {
            set { amt = value; }
            get { return amt; }
        }

        private DataView dvEditCostCentre = null;
        private DataView DvEditCostCentre
        {
            set { dvEditCostCentre = value; }
            get { return dvEditCostCentre; }
        }


        private int costCenterId;
        private int CostCenterId
        {
            set
            {
                costCenterId = value;
            }
            get
            {
                costCenterId = gvCostCenter.GetFocusedRowCellValue(colCostCentreName) != null ? this.UtilityMember.NumberSet.ToInteger(gvCostCenter.GetFocusedRowCellValue(colCostCentreName).ToString()) : 0;
                return costCenterId;
            }
        }

        private double costCentreSummaryVal = 0;
        private double CostCentreSummaryVal
        {
            get { return this.UtilityMember.NumberSet.ToDouble(colAmount.SummaryItem.SummaryValue.ToString()); }

        }

        private int projectId = 0;
        private int ProjectId
        {
            get { return projectId; }
            set { projectId = value; }
        }

        private int costCentreId;
        private int CostCentreId
        {
            set
            {
                costCentreId = value;
            }
            get
            {
                costCentreId = gvCostCenter.GetFocusedRowCellValue(colCostCentreName) != null ? this.UtilityMember.NumberSet.ToInteger(gvCostCenter.GetFocusedRowCellValue(colCostCentreName).ToString()) : 0;
                return costCentreId;
            }
        }

        private double costCentreAmount;
        private double CostCentreAmount
        {
            set
            {
                costCentreAmount = value;
            }
            get
            {
                costCentreAmount = gvCostCenter.GetFocusedRowCellValue(colAmount) != null ? this.UtilityMember.NumberSet.ToDouble(gvCostCenter.GetFocusedRowCellValue(colAmount).ToString()) : 0;
                return costCentreAmount;
            }
        }
        public DataTable dtRecord
        {
            set { dtValues = value; }
            get { return dtValues; }

        }

        private bool TransacationGridNewItem
        {
            set
            {
                if (value)
                {
                    DataTable dtCostCentre = gcCostCenter.DataSource as DataTable;
                    dtCostCentre.Rows.Add(dtCostCentre.NewRow());
                    gcCostCenter.DataSource = dtCostCentre;
                    gvCostCenter.MoveNext();
                    gvCostCenter.FocusedColumn = colCostCentreName;
                    gvCostCenter.ShowEditor();
                }
            }
        }

        #endregion

        #region Constructor
        public frmTransactionCostCenter()
        {
            InitializeComponent();
            RealColumnEditTransAmount();
        }

        public frmTransactionCostCenter(int projectId, DataView dvCostCentre, int LedgerID, double Amount, string LedgerName)
            : this()
        {
            ledgerId = LedgerID;
            Amt = Amount;
            DvEditCostCentre = dvCostCentre;

            //On 05/09/2024, To show without currency
            //lblLedgerAmount.Text = this.UtilityMember.NumberSet.ToCurrency(Amount) + "Dr";
            lblLedgerAmount.Text = this.UtilityMember.NumberSet.ToNumber(Amount) + "Dr";

            // lblCostCenterCaption.Text = "Cost Allocation for " + " : ";
            lblLedgerName.Text = LedgerName;
            ProjectId = projectId;
        }
        #endregion

        #region Events
        private void frmTransactionCostCenter_Load(object sender, EventArgs e)
        {
            gvCostCenter.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
            BindCostCenter();
            ConstructTransEmptySource();
            AssignValues();
        }

        private void RealColumnEditTransAmount()
        {
            colAmount.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditTransAmount_EditValueChanged);
            this.gvCostCenter.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvCostCenter.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colAmount)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvCostCenter.ShowEditorByMouse();
                    }));
                }
            };
        }

        void RealColumnEditTransAmount_EditValueChanged(object sender, System.EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvCostCenter.PostEditor();
            gvCostCenter.UpdateCurrentRow();
            if (gvCostCenter.ActiveEditor == null)
            {
                gvCostCenter.ShowEditor();
            }

            TextEdit txtTransAmount = edit as TextEdit;
            int grpCounts = (this.UtilityMember.NumberSet.ToInteger(this.AppSetting.DecimalPlaces) + 1);
            if (txtTransAmount.Text.Length > grpCounts && txtTransAmount.SelectionLength == txtTransAmount.Text.Length)
                txtTransAmount.Select(txtTransAmount.Text.Length - grpCounts, 0);
        }

        private void AssignValues()
        {
            if (DvEditCostCentre != null && DvEditCostCentre.Count > 0)
            {
                gcCostCenter.DataSource = DvEditCostCentre.ToTable();
            }
        }

        private void ConstructEmptySource()
        {
            DataTable dt = gcCostCenter.DataSource as DataTable;
            dt.Rows.Add(dt.NewRow());
            gcCostCenter.DataSource = dt;
            gvCostCenter.FocusedColumn = colCostCentreName;
            gvCostCenter.ShowEditor();
        }

        private void gcCostCenter_ProcessGridKey(object sender, KeyEventArgs e)
        {
            bool CanFocusOk = false;
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Tab) && gvCostCenter.FocusedColumn == colAmount)
            {
                if (CostCentreId == 0 && CostCentreAmount == 0 && gvCostCenter.IsFirstRow) { this.Close(); }

                double dAmt = this.UtilityMember.NumberSet.ToDouble(Amt.ToString());

                if (dAmt == CostCentreSummaryVal)
                {
                    if (gvCostCenter.IsLastRow) { SaveCostCenterDetails(); }
                    else { gvCostCenter.MoveNext(); gvCostCenter.FocusedColumn = colCostCentreName; }
                }

                if (CanFocusOk)
                {
                    btnOk.Select();
                    btnOk.Focus();
                    btnOk.ShowFocusRectangle = DevExpress.Utils.DefaultBoolean.True;
                }

                if (dAmt != CostCentreSummaryVal && CostCentreId > 0 && CostCentreAmount > 0 && gvCostCenter.IsLastRow)
                {
                    TransacationGridNewItem = true;
                    if (e.KeyCode == Keys.Tab || e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return)
                    {
                        e.SuppressKeyPress = true;
                    }
                }
            }
        }

        private void frmTransactionCostCenter_Shown(object sender, EventArgs e)
        {
            gcCostCenter.Select();
            gvCostCenter.FocusedRowHandle = DevExpress.XtraGrid.GridControl.NewItemRowHandle;
            gvCostCenter.FocusedColumn = gvCostCenter.VisibleColumns[0];
            gvCostCenter.ShowEditor();
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
                    costCenterSystem.LedgerId = (this.AppSetting.CostCeterMapping==1? ledgerId : 0);
                    resultArgs = costCenterSystem.FetchforLookUpDetails();
                    rglkpCostCenter.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        rglkpCostCenter.DataSource = resultArgs.DataSource.Table;
                        rglkpCostCenter.DisplayMember = costCenterSystem.AppSchema.CostCentre.COST_CENTRE_NAMEColumn.ColumnName.ToString();
                        rglkpCostCenter.ValueMember = costCenterSystem.AppSchema.CostCentre.COST_CENTRE_IDColumn.ColumnName.ToString();
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }


        private void ConstructTransEmptySource()
        {
            DataTable dtCostCentre = new DataTable();
            dtCostCentre.Columns.Add("COST_CENTRE_ID", typeof(Int32));
            dtCostCentre.Columns.Add("AMOUNT", typeof(decimal));
            dtCostCentre.Rows.Add(dtCostCentre.NewRow());
            gcCostCenter.DataSource = dtCostCentre;
        }

        private void FetchVoucherCostCentre()
        {
            try
            {
                using (VoucherTransactionSystem costCentre = new VoucherTransactionSystem())
                {
                    if (DvEditCostCentre == null)
                    {
                        resultArgs = costCentre.FetchVoucherCostCentre();
                        gcCostCenter.DataSource = resultArgs.DataSource.TableView;
                    }
                    else
                    {
                        if (DvEditCostCentre.Table.Columns.Contains("COST_CENTRE_NAME"))
                        {
                            DvEditCostCentre.Table.Columns.Remove("COST_CENTRE_NAME");
                            gcCostCenter.DataSource = DvEditCostCentre.Table.DefaultView;
                        }
                        else
                        {
                            gcCostCenter.DataSource = DvEditCostCentre.Table.DefaultView;
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

        private void SetGridFocus()
        {
            gcCostCenter.Select();
            gvCostCenter.MoveFirst();
            gvCostCenter.FocusedColumn = colCostCentreName;
            gvCostCenter.ShowEditor();
        }

        private bool ValidateCostCentre()
        {
            bool isValid = true;
            double dAmt = this.UtilityMember.NumberSet.ToDouble(Amt.ToString());

            if (!IsValidCostCentreGrid())
            {
                isValid = false;
            }
            else if (dAmt < CostCentreSummaryVal)
            {
                isValid = false;
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VoucherCostCentre.VOUCHER_COST_CENTRE_ALLOCATION_AMOUNT_GREATER));
                SetGridFocus();
            }
            else if (dAmt > CostCentreSummaryVal)
            {
                isValid = false;
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Transaction.VoucherCostCentre.VOUCHER_COST_CENTRE_ALLOCATION_AMOUNT_LESS));
                SetGridFocus();
            }
            return isValid;
        }

        private bool IsValidCostCentreGrid()
        {
            DataTable dt = gcCostCenter.DataSource as DataTable;

            int Id = 0;
            decimal Amt = 0;
            int RowPosition = 0;
            bool isValid = false;
            //string validateMessage = "Required Information not filled";
            string validateMessage = this.GetMessage(MessageCatalog.Master.Transaction.TRANS_COSTCENTER_VALIDATION_MESSAGE);
            if (dt != null)
            {
                DataView dv = new DataView(dt);
                dv.RowFilter = "(COST_CENTRE_ID>0 OR AMOUNT>0)";
                gvCostCenter.FocusedColumn = colCostCentreName;
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
                                //validateMessage = "Required Information not filled, Cost Centre Name is empty";
                                validateMessage = this.GetMessage(MessageCatalog.Master.Transaction.TRANS_COSTCENTER_NAME_EMPTY);
                                gvCostCenter.FocusedColumn = colCostCentreName;
                            }
                            if (Amt == 0)
                            {
                                //validateMessage = "Required Information not filled, Amount is empty";
                                validateMessage = this.GetMessage(MessageCatalog.Master.Transaction.TRANS_COSTCENTER_AMOUNT_EMPTY);
                                gvCostCenter.FocusedColumn = colAmount;
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
                    gvCostCenter.CloseEditor();
                    gvCostCenter.FocusedRowHandle = gvCostCenter.GetRowHandle(RowPosition);
                    gvCostCenter.ShowEditor();
                }
            }

            return isValid;
        }

        private void SaveCostCenterDetails()
        {
            try
            {
                DataTable dtCostCentre = gcCostCenter.DataSource as DataTable;
                if (dtCostCentre != null && dtCostCentre.Rows.Count > 0)
                {
                    this.DialogResult = DialogResult.OK;
                    dtRecord = dtCostCentre;
                    dtRecord.TableName = ledgerId.ToString();
                    this.Close();
                    //DataTable dtCostCenter = dvCostCenter.Table;
                    //dtCostCenter.TableName = ledgerId.ToString();

                    //using (CostCentreSystem costCentreSystem = new CostCentreSystem())
                    //{
                    //    dtValues.Columns.Add(new DataColumn("COST_CENTRE_ID", typeof(int)));
                    //    dtValues.Columns.Add(new DataColumn("COST_CENTRE_NAME", typeof(string)));
                    //    dtValues.Columns.Add(new DataColumn("AMOUNT", typeof(double)));

                    //    costCentreSystem.ProjectId = ProjectId;
                    //    resultArgs = costCentreSystem.FetchforLookUpDetails();
                    //    DataTable dtTable = new DataTable();
                    //    dtTable = resultArgs.DataSource.Table;

                    //    for (int i = 0; i < dtTable.Rows.Count; i++)
                    //    {
                    //        for (int j = 0; j < dtCostCenter.Rows.Count; j++)
                    //        {
                    //            if (dtTable.Rows[i]["COST_CENTRE_ID"].ToString() == dtCostCenter.Rows[j]["COST_CENTRE_ID"].ToString() && this.UtilityMember.NumberSet.ToDouble(dtCostCenter.Rows[j]["AMOUNT"].ToString()) > 0)
                    //            {
                    //                DataRow dr = dtValues.NewRow();
                    //                dr["COST_CENTRE_ID"] = this.UtilityMember.NumberSet.ToInteger(dtTable.Rows[i]["COST_CENTRE_ID"].ToString());
                    //                dr["COST_CENTRE_NAME"] = dtTable.Rows[i]["COST_CENTRE_NAME"].ToString();
                    //                dr["AMOUNT"] = dtCostCenter.Rows[j]["AMOUNT"].ToString();
                    //                dtValues.Rows.Add(dr);
                    //            }
                    //        }
                    //    }
                    //    dtValues.TableName = ledgerId.ToString();
                    //}
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void rglkpCostCenter_Leave(object sender, EventArgs e)
        {
            double CurrentAmt = this.UtilityMember.NumberSet.ToDouble(gvCostCenter.GetFocusedRowCellValue(colAmount).ToString());
            if (CostCentreId > 0)
            {
                if ((this.UtilityMember.NumberSet.ToDecimal(Amt.ToString()) > 0) && (CostCentreSummaryVal < this.UtilityMember.NumberSet.ToDouble(Amt.ToString())))
                {
                    CurrentAmt = this.UtilityMember.NumberSet.ToDouble(gvCostCenter.GetFocusedRowCellValue(colAmount).ToString());
                    double Amount = this.UtilityMember.NumberSet.ToDouble(Amt.ToString()) - CostCentreSummaryVal;
                    gvCostCenter.SetFocusedRowCellValue(colAmount, Amount.ToString());

                    if (DvEditCostCentre != null && DvEditCostCentre.Table.Rows.Count > 0)
                    {
                        Amount = this.UtilityMember.NumberSet.ToDouble(Amt.ToString()) - (CostCentreSummaryVal - CurrentAmt);
                        gvCostCenter.SetFocusedRowCellValue(colAmount, Amount.ToString());
                    }
                }
                else if ((CostCentreSummaryVal > this.UtilityMember.NumberSet.ToDouble(Amt.ToString())))
                {
                    if (DvEditCostCentre != null && DvEditCostCentre.Table.Rows.Count > 0)
                    {
                        double Amount = (this.UtilityMember.NumberSet.ToDouble(Amt.ToString())) - (CostCentreSummaryVal - CurrentAmt);
                        if (Amount > 0)
                            gvCostCenter.SetFocusedRowCellValue(colAmount, Amount.ToString());
                    }
                }

            }
        }
        #endregion

        private void gvCostCenter_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            //if (!IsValidRow())
            //{
            //}
        }

        private bool IsValidRow()
        {
            bool IsCostCentreValid = false;
            try
            {
                IsCostCentreValid = (CostCenterId > 0 && CostCentreAmount > 0);
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            return IsCostCentreValid;
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
            bool isValid = true;
            DataTable dtTemp = gcCostCenter.DataSource as DataTable;
            if (dtTemp.Rows.Count == 1)
            {
                DataView dvTemp = dtTemp.DefaultView;
                dvTemp.RowFilter = "COST_CENTRE_ID>0 AND AMOUNT>0";
                if (dvTemp.Count == 0)
                {
                    isValid = false;
                    //this.Close();
                }
            }
            if (isValid)
            {
                if (ValidateCostCentre())
                {
                    //this.DialogResult = DialogResult.OK;
                    SaveCostCenterDetails();

                }
            }
            else
            {
                SaveCostCenterDetails();
            }
            //if (isValid)
            //{
            //    if (ValidateCostCentre())
            //    {
            //        //this.DialogResult = DialogResult.OK;
            //        SaveCostCenterDetails();

            //    }
            //}
            //else
            //{
            //    this.DialogResult = DialogResult.Cancel;
            //}
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

        public void DeleteCostcentre()
        {
            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                gvCostCenter.DeleteRow(gvCostCenter.FocusedRowHandle);
                if (gvCostCenter.RowCount == 0)
                {
                    gvCostCenter.AddNewRow();
                }
            }
        }

    }
}






//private void gvCostCenter_InvalidRowException(object sender, DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventArgs e)
//{
//    e.ErrorText = this.GetMessage(MessageCatalog.Common.COMMON_INVALID_EXCEPTION);
//    e.WindowCaption = this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE);
//    if (e.Exception.Message.Contains(costCentre.AppSchema.CostCentre.COST_CENTRE_IDColumn.ColumnName))
//    {
//        e.ErrorText = this.GetMessage(MessageCatalog.Master.CostCentre.COST_CENTER_SAVE_FAILURE);
//        gvCostCenter.SelectAll();
//        gvCostCenter.FocusedColumn = colCostCentreName;
//        gvCostCenter.ShowEditor();
//    }
//}

//public DataTable LoadData()
//        {
//            DataView dv = (DataView)gvCostCenter.DataSource;
//            dv.Table.Columns["COST_CENTRE_ID"].Unique = true;
//            return dv.ToTable();
//        }

