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
using AcMEDSync.Model;
using Bosco.Utility;
using Bosco.Utility.CommonMemberSet;

namespace AcMEDSync.Forms
{
    public partial class frmMapHeadOfficeLedgers : DevExpress.XtraEditors.XtraForm
    {
        ResultArgs result = null;
        DataTable dtMisMatchedLedger = null;
        DataTable dtModifiedLedgers = null;
        DsyncSystemBase acMEDSyncSQL = new DsyncSystemBase();

        public frmMapHeadOfficeLedgers()
        {
            InitializeComponent();
        }

        public frmMapHeadOfficeLedgers(DataTable dtLedgers, DataTable dtHOLedgers)
            : this()
        {
            dtMisMatchedLedger = dtLedgers;
            dtModifiedLedgers = dtHOLedgers;
        }

        private void frmMapHeadOfficeLedgers_Load(object sender, EventArgs e)
        {
            LoadMisMatchedLedgers();
            LoadHeadOfficeLedgers();
            FocusMismatchedGrid();
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
            DataTable dtTemp = new DataTable();
            if (dtModifiedLedgers == null)
            {
                dtTemp.Columns.Add("LEDGER_NAME", typeof(string));
            }
            else
            {
                dtTemp = dtModifiedLedgers.Copy();
            }

            gcNew.DataSource = dtModifiedLedgers;
            ComboSetMember glkpEdit = new ComboSetMember();
            rglkpHOLedger.DataSource = glkpEdit.AddEmptyItem(dtTemp, "LEDGER_NAME", "LEDGER_NAME", "---Delete Ledger---");
            rglkpHOLedger.DisplayMember = "LEDGER_NAME";
            rglkpHOLedger.ValueMember = "LEDGER_NAME";
        }

        private bool IsValidMismatchedGrid()
        {
            DataTable dtNewLedgers = gcLedgerMapping.DataSource as DataTable;
            string LedgerName = string.Empty;
            int RowPosition = 0;
            bool isValid = false;


            if (dtNewLedgers.Rows.Count > 0)
            {
                isValid = true;
                foreach (DataRow drTrans in dtNewLedgers.Rows)
                {
                    LedgerName = drTrans["LEDGER_NAME"].ToString();

                    if (LedgerName == string.Empty) //&& !(Id == 0 && Amt == 0))
                    {
                        XtraMessageBox.Show(acMEDSyncSQL.GetMessage(MessageCatalog.DataSynchronization.Mapping.MAP_HEADOFFICE_lEDGERS) , "AcME++", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        isValid = false;
                        break;
                    }
                    RowPosition = RowPosition + 1;
                }
            }

            if (!isValid)
            {
                gvLedger.FocusedColumn = colHeadOfficeLedger;
                gvLedger.CloseEditor();
                gvLedger.FocusedRowHandle = gvLedger.GetRowHandle(RowPosition);
                gvLedger.ShowEditor();
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
            if (IsValidInput())
            {
                ResultArgs result = null;
                DataTable dtMappedLedger = gcLedgerMapping.DataSource as DataTable;
                if (dtMappedLedger != null && dtMappedLedger.Rows.Count > 0)
                {
                    using (HeadOfficeLedgersSystem ledgerSystem = new HeadOfficeLedgersSystem())
                    {
                        result = ledgerSystem.UpdateHeadOfficeLedgers(dtMappedLedger);

                        if (result.Success)
                        {
                            DataTable dtNewLedgers = gcNew.DataSource as DataTable;
                            result = ledgerSystem.SaveMasterHeadOfficeLedgers(dtNewLedgers);
                        }

                        if (result.Success)
                        {
                            this.DialogResult = DialogResult.OK;
                            this.Close();
                        }
                        else
                        {
                            this.DialogResult = DialogResult.Cancel;
                        }
                    }
                }
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
                    LedgerName = drv["LEDGER_NAME"].ToString();
                    gvLedger.SetRowCellValue(gvLedger.FocusedRowHandle, colHeadOfficeLedger, LedgerName);

                    if (!string.IsNullOrEmpty(LedgerName))
                    {
                        if (LedgerName == "---Delete Ledger---")
                        {
                            using (HeadOfficeLedgersSystem ledgerSystem = new HeadOfficeLedgersSystem())
                            {
                                if (!ledgerSystem.HasLedgerEntries(LedgerId))
                                {
                                    e.Cancel = true;
                                    XtraMessageBox.Show(acMEDSyncSQL.GetMessage(MessageCatalog.DataSynchronization.Mapping.VALIDATE_OP_TRANS),
                                           "AcME++", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                }

                            }
                        }
                    }
                }
            }
        }

        private void rglkpHOLedger_Leave(object sender, EventArgs e)
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
                    DataTable dtMappedLedger = gcLedgerMapping.DataSource as DataTable;

                    if ((dtModifiedLedgers!=null && dtModifiedLedgers.Rows.Count>0))
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

        private void gcLedgerMapping_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && !e.Shift && !e.Alt && !e.Control &&
                (gvLedger.FocusedColumn == colHeadOfficeLedger && gvLedger.IsLastRow))
            {
                btnImportLedgers.Select();
                btnImportLedgers.Focus();
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
    }
}