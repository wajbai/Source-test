using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using System.Configuration;
using Bosco.DAO.Data;

using Bosco.Utility;
using Bosco.Utility.CommonMemberSet;
using Bosco.Model.Dsync;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace ACPP.Modules.Dsync
{
    public partial class frmMapHeadOfficeLedgers : frmFinanceBaseAdd
    {
        ResultArgs result = null;
        DataTable dtMisMatchedLedger = null;
        DataTable dtModifiedLedgers = null;
        DataTable dtDeletedLedgers = new DataTable();
        // DsyncSystemBase acMEDSyncSQL = new DsyncSystemBase();

        public frmMapHeadOfficeLedgers()
        {
            InitializeComponent();
            // RealColumnEditTransAmount();
        }

        private int LedgerId
        {
            get
            {
                int ledgerId = 0;
                ledgerId = gvLedger.GetFocusedRowCellValue(colLedgerId) != null ? this.UtilityMember.NumberSet.ToInteger(gvLedger.GetRowCellValue(gvLedger.FocusedRowHandle, colLedgerId).ToString()) : 0;
                return ledgerId;
            }
        }

        private void frmMapHeadOfficeLedgers_Load(object sender, EventArgs e)
        {
            dtMisMatchedLedger = this.AppSetting.MisMatchedLedgers;
            dtModifiedLedgers = this.AppSetting.ModifiedLedgers;
            dtDeletedLedgers.Columns.Add("HEADOFFICE_LEDGER_ID", typeof(Int32));
            LoadMisMatchedLedgers();
            LoadHeadOfficeLedgers();
            FocusMismatchedGrid();
        }

        private void RealColumnEditTransAmount()
        {
            colHeadOfficeLedger.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditTransAmount_EditValueChanged);
            this.gvLedger.MouseDown += (object sender, MouseEventArgs e) =>
            {
                GridHitInfo hitInfo = gvLedger.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colHeadOfficeLedger)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvLedger.ShowEditorByMouse();
                    }));
                }
            };
        }

        void RealColumnEditTransAmount_EditValueChanged(object sender, System.EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvLedger.PostEditor();
            gvLedger.UpdateCurrentRow();
            if (gvLedger.ActiveEditor == null)
            {
                gvLedger.ShowEditor();
            }


        }

        private void LoadMisMatchedLedgers()
        {
            if (dtMisMatchedLedger != null && dtMisMatchedLedger.Rows.Count > 0)
            {
                gcLedgerMapping.DataSource = dtMisMatchedLedger;
            }
        }

        private void LoadHeadOfficeLedgers()
        {
            //DataTable dtTemp = new DataTable();
            //if (dtModifiedLedgers == null)
            //{
            //    dtTemp.Columns.Add("LEDGER_NAME", typeof(string));
            //}
            //else
            //{
            //    dtTemp = dtModifiedLedgers.Copy();
            //}

            gcNew.DataSource = dtModifiedLedgers;
            // ComboSetMember glkpEdit = new ComboSetMember();
            rglkpHOLedger.DataSource = gcNew.DataSource = dtModifiedLedgers;    //glkpEdit.AddEmptyItem(dtTemp, "LEDGER_NAME", "LEDGER_NAME", "---Delete Ledger---");
            rglkpHOLedger.DisplayMember = "LEDGER_NAME";
            rglkpHOLedger.ValueMember = "LEDGER_NAME";
        }

        private bool IsValidMismatchedGrid()
        {
            DataTable dtNewLedgers = gcLedgerMapping.DataSource as DataTable;
            string LedgerName = string.Empty;
            int RowPosition = 0;
            bool isValid = true;

            if (dtNewLedgers != null && dtNewLedgers.Rows.Count > 0)
            {
                foreach (DataRow drTrans in dtNewLedgers.Rows)
                {
                    LedgerName = drTrans["LEDGER_NAME"].ToString();

                    if (LedgerName == string.Empty) //&& !(Id == 0 && Amt == 0))
                    {
                        XtraMessageBox.Show(this.GetMessage(MessageCatalog.DataSynchronization.Mapping.DSYNC_MAP_HEADOFFICE_lEDGERS), this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        isValid = false;
                        break;
                    }
                    RowPosition = RowPosition + 1;
                }

                if (!isValid)
                {
                    gvLedger.FocusedColumn = colHeadOfficeLedger;
                    gvLedger.CloseEditor();
                    gvLedger.FocusedRowHandle = gvLedger.GetRowHandle(RowPosition);
                    gvLedger.ShowEditor();
                }
            }
            return isValid;
        }

        private bool IsValidInput()
        {
            bool isValid = true;
            if (!IsValidMismatchedGrid())
            {
                isValid = false;
            }
            return isValid;
        }

        private void btnImportLedgers_Click(object sender, EventArgs e)
        {
            ResultArgs result = new ResultArgs();
            result.Success = true;
            using (ImportMasterSystem ledgerSystem = new ImportMasterSystem())
            {
                //DialogResult dialogResult = this.ShowConfirmationMessage("How do you want to proceed to map the Head Office Ledger?" + Environment.NewLine + Environment.NewLine +
                //                             "Yes      : Head Office Ledgers which are not mapped will be added as new Ledgers." + Environment.NewLine + Environment.NewLine +
                //                             "No       : Head Office Ledgers which are not mapped will not be added in Branch Office." + Environment.NewLine + Environment.NewLine +
                //                             "Cancel  : Stop Ledger Mapping Process." + Environment.NewLine, MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                //if (dialogResult == DialogResult.Yes || dialogResult == DialogResult.No)
                //{
                if (IsValidInput())
                {
                    DataTable dtMappedLedger = gcLedgerMapping.DataSource as DataTable;
                    if (dtMappedLedger != null && dtMappedLedger.Rows.Count > 0)
                    {
                        DataView dvMappedLedger = dtMappedLedger.Copy().DefaultView;
                        dvMappedLedger.RowFilter = "LEDGER_NAME IS NOT NULL";
                        if (dvMappedLedger != null && dvMappedLedger.Count > 0)
                        {
                            result = ledgerSystem.SaveMismatchedLedgers(dvMappedLedger.ToTable(), dtDeletedLedgers);
                        }
                    }

                    //if (dialogResult == DialogResult.Yes)
                    //{
                    if (result == null || result.Success)
                    {
                        DataTable dtNewLedgers = gcNew.DataSource as DataTable;
                        if (dtNewLedgers != null && dtNewLedgers.Rows.Count > 0)
                        {
                           // result = ledgerSystem.SaveMasterHeadOfficeLedgers(dtNewLedgers);
                        }
                    }

                    if (result.Success)
                    {
                        this.DialogResult = DialogResult.OK;
                        this.Close();
                    }
                    else
                    {
                        XtraMessageBox.Show(result.Message, this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                //  }
                //  }

            }

        }

        private void rglkpHOLedger_EditValueChanged(object sender, EventArgs e)
        {
            //DataTable dtLedger = gcLedgerMapping.DataSource as DataTable;
            //string LedgerName = gvLedger.GetRowCellValue(gvLedger.FocusedRowHandle, colHeadOfficeLedger).ToString();
            //if (!string.IsNullOrEmpty(LedgerName))
            //{
            //    DataView dvLedger = dtModifiedLedgers.DefaultView;
            //    dvLedger.RowFilter = "LEDGER_NAME=" + LedgerName;
            //    gcNew.DataSource = dvLedger.ToTable();
            //    dvLedger.RowFilter = "";
            //}
        }

        /// <summary>
        /// Fetch MisMatched Ledgers
        /// </summary>
        /// <param name="dtMappedLedger"></param>
        /// <param name="dtNewLedger"></param>
        /// <returns></returns>
        private DataTable FetchMisMatchedLedgers(DataTable dtMappedLedger, DataTable dtNewLedger)
        {
            DataTable dtLedgers = new DataTable();

            var matched = from table1 in dtNewLedger.AsEnumerable()
                          join table2 in dtMappedLedger.AsEnumerable() on table1.Field<string>("LEDGER_NAME") equals table2.Field<string>("LEDGER_NAME")
                          select table1;


            var missing = from table1 in dtNewLedger.AsEnumerable()
                          where !matched.Contains(table1)
                          select table1;

            if (missing.Count() > 0)
            {
                dtLedgers = missing.CopyToDataTable();
            }
            return dtLedgers;
        }

        private void rglkpHOLedger_Validating(object sender, CancelEventArgs e)
        {
            string LedgerName = string.Empty;
            int LedgerId = 0;
            GridLookUpEdit gridLKPEdit = sender as GridLookUpEdit;
            if (gridLKPEdit.EditValue != null)
            {
                DataRowView drv = gridLKPEdit.GetSelectedDataRow() as DataRowView;

                if (drv != null)
                {
                    LedgerId = Convert.ToInt32(gvLedger.GetFocusedRowCellValue(colLedgerId).ToString());
                    LedgerName = drv["LEDGER_NAME"].ToString();
                    gvLedger.SetRowCellValue(gvLedger.FocusedRowHandle, colHeadOfficeLedger, LedgerName);
                    gvLedger.PostEditor();
                    gvLedger.UpdateCurrentRow();
                    DataTable dtMappedLedger = gcLedgerMapping.DataSource as DataTable;

                    if ((dtModifiedLedgers != null && dtModifiedLedgers.Rows.Count > 0))
                    {
                        DataTable dtTemp = FetchMisMatchedLedgers(dtMappedLedger, dtModifiedLedgers);
                        if (dtTemp != null)
                        {
                            gcNew.DataSource = dtTemp;

                            DataTable dtHOLedgers = gcLedgerMapping.DataSource as DataTable;
                            if (dtHOLedgers != null && dtHOLedgers.Rows.Count > 0)
                            {
                                DataView dvTemp = new DataView(dtHOLedgers);
                                dvTemp.RowFilter = "LEDGER_NAME='" + CommonMethod.EscapeLikeValue(LedgerName) + @"'";
                                if (dvTemp != null && dvTemp.Count > 1)
                                {
                                    e.Cancel = true;
                                    gvLedger.SetRowCellValue(gvLedger.FocusedRowHandle, colHeadOfficeLedger, null);
                                    this.ShowMessageBoxWarning(LedgerName + " is already mapped");
                                }
                            }

                            // rglkpHOLedger.DataSource = dtTemp;
                            // rglkpHOLedger.DisplayMember = "LEDGER_NAME";
                            //  rglkpHOLedger.ValueMember = "LEDGER_NAME";
                        }
                    }
                }
            }
        }

        private void rglkpHOLedger_Leave(object sender, EventArgs e)
        {

        }

        private void gcLedgerMapping_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && !e.Shift && !e.Alt && !e.Control &&
                (gvLedger.FocusedColumn == colHeadOfficeLedger))
            {
                if (gvLedger.IsLastRow)
                {
                    btnImportLedgers.Select();
                    btnImportLedgers.Focus();
                }
                else
                {
                   // gvLedger.MoveNext(); //DevExpress.XtraGrid.GridControl.NewItemRowHandle;
                    gvLedger.FocusedColumn = gvLedger.Columns.ColumnByName(colHeadOfficeLedger.Name);
                    gvLedger.ShowEditor();
                }
            }
        }

        private void FocusMismatchedGrid()
        {
            //gcTransaction.Select();
            gcLedgerMapping.Focus();
            gvLedger.MoveFirst(); //DevExpress.XtraGrid.GridControl.NewItemRowHandle;
            gvLedger.FocusedColumn = gvLedger.Columns.ColumnByName(colHeadOfficeLedger.Name);
            gvLedger.ShowEditor();
        }

        private void rbtnDeleteLedger_Click(object sender, EventArgs e)
        {
            //string LedgerName = string.Empty;
            //int LedgerId = 0;
            //GridLookUpEdit gridLKPEdit = sender as GridLookUpEdit;
            //if (gridLKPEdit.EditValue != null)
            //{
            //    DataRowView drv = gridLKPEdit.GetSelectedDataRow() as DataRowView;

            //    if (drv != null)
            //    {
            //        LedgerId = Convert.ToInt32(gvLedger.GetFocusedRowCellValue(colLedgerId).ToString());
            //        LedgerName = drv["LEDGER_NAME"].ToString();
            //        gvLedger.SetRowCellValue(gvLedger.FocusedRowHandle, colHeadOfficeLedger, LedgerName);

            //        if (!string.IsNullOrEmpty(LedgerName))
            //        {
            //            if (LedgerName == "---Delete Ledger---")
            //            {
            using (ImportMasterSystem ledgerSystem = new ImportMasterSystem())
            {
                if (!ledgerSystem.HasLedgerEntries(LedgerId))
                {
                    gvLedger.SetRowCellValue(gvLedger.FocusedRowHandle, colHeadOfficeLedger, null);
                   // XtraMessageBox.Show("The ledger cannot be deleted. It has balance.",
                     //     this.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.FinanceDataSynch.MAP_LEDGER_LEDGER_CANNOT_DELETE_INFO));
                }
                else
                {
                    if (dtDeletedLedgers != null)
                    {
                        dtDeletedLedgers.Rows.Add(LedgerId);
                    }
                    gvLedger.DeleteRow(gvLedger.FocusedRowHandle);
                    gvLedger.UpdateCurrentRow();
                    gcLedgerMapping.RefreshDataSource();


                    DataTable dtMappedLedger = gcLedgerMapping.DataSource as DataTable;
                    dtMappedLedger.AcceptChanges();

                    if ((dtModifiedLedgers != null && dtModifiedLedgers.Rows.Count > 0))
                    {
                        DataTable dtTemp = FetchMisMatchedLedgers(dtMappedLedger, dtModifiedLedgers);
                        if (dtTemp != null)
                        {
                            gcNew.DataSource = dtTemp;
                        }
                    }
                }

            }
        }

        private void rglkpHOLedger_Enter(object sender, EventArgs e)
        {

        }
    }
}