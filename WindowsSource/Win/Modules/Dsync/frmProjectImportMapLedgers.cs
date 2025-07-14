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
using System.IO;
using System.Threading;
using Bosco.Model.Dsync;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Views.Base;
using Bosco.Model.UIModel;
using DevExpress.XtraGrid.Views.Grid;

namespace ACPP.Modules.Dsync
{
    public partial class frmProjectImportMapLedgers : frmFinanceBaseAdd
    {
        #region Variables
        ResultArgs resultArgs = new ResultArgs();
        public DataTable dtProjectImportedMappedLedger = new DataTable();
        private bool IsMappingRequired = false;
        #endregion

        #region Constructor
        public frmProjectImportMapLedgers()
        {
            InitializeComponent();
        }

        public frmProjectImportMapLedgers(DataTable dtLedgers, bool mapAll)
            : this()
        {
            IsMappingRequired = mapAll;
            dtProjectImportedMappedLedger = dtLedgers;
            dtProjectImportedMappedLedger.TableName = "MAPPED_LEDGERS";
            using (LedgerSystem ledgersystem = new LedgerSystem())
            {
                if (!dtProjectImportedMappedLedger.Columns.Contains(ledgersystem.AppSchema.ProjectImportExport.MERGE_LEDGER_IDColumn.ColumnName))
                {
                    dtProjectImportedMappedLedger.Columns.Add(ledgersystem.AppSchema.ProjectImportExport.MERGE_LEDGER_IDColumn.ColumnName, typeof(System.Int32));
                    dtProjectImportedMappedLedger.Columns[ledgersystem.AppSchema.ProjectImportExport.MERGE_LEDGER_IDColumn.ColumnName].DefaultValue = 0;
                }

                if (!dtProjectImportedMappedLedger.Columns.Contains(ledgersystem.AppSchema.ProjectImportExport.MERGE_LEDGER_NAMEColumn.ColumnName))
                {
                    dtProjectImportedMappedLedger.Columns.Add(ledgersystem.AppSchema.ProjectImportExport.MERGE_LEDGER_NAMEColumn.ColumnName, typeof(System.String));
                    dtProjectImportedMappedLedger.Columns[ledgersystem.AppSchema.ProjectImportExport.MERGE_LEDGER_NAMEColumn.ColumnName].DefaultValue = string.Empty;
                }
            }
        }

        #endregion

        #region Events
        /// <summary>
        /// Load the Headofffice and Branchoffice Mapped and Unmapped Ledgers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmUnMappedLedgers_Load(object sender, EventArgs e)
        {
            LoadUnMappedLedgers();
            LoadExistingLedgers();
        }

        /// <summary>
        /// To Map Headoffice ledger to Branchoffice Ledgers
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnMapLedgers_Click(object sender, EventArgs e)
        {
            if (IsMappingRequired)
            {
                if (IsValidMismatchedGrid())
                {
                    MapImportedLedgers();
                    this.Close();
                }
            }
            else
            {
                MapImportedLedgers();
            }
        }


        private void gvUnmappedLedgers_ShownEditor(object sender, EventArgs e)
        {
            try
            {
                //Filter Head Office ledgers based on active brach office ledger group.
                // for ex: FD ledgers (GroupID=14), should map only head office fd leadgers 
                DataRow drBrach = null;
                int BranchLedgerGroupId = 0;
                int BranchInterestLedgerId = 0;
                string filter = string.Empty;

                ColumnView cview = (ColumnView)sender;
                if (cview.FocusedColumn.FieldName == collkpExistingLedgerId.FieldName && cview.FocusedValue != null)
                {
                    GridLookUpEdit grdlkp = (GridLookUpEdit)cview.ActiveEditor;
                    drBrach = gvUnmappedLedgers.GetDataRow(gvUnmappedLedgers.FocusedRowHandle);
                    if (drBrach != null)
                    {
                        BranchLedgerGroupId = UtilityMember.NumberSet.ToInteger(drBrach["GROUP_ID"].ToString());
                        BranchInterestLedgerId = UtilityMember.NumberSet.ToInteger(drBrach["IS_BANK_INTEREST_LEDGER"].ToString());

                        if (BranchInterestLedgerId > 0)
                        {
                            filter = "IS_BANK_INTEREST_LEDGER = 1";
                        }
                        else if (BranchLedgerGroupId == (int)FixedLedgerGroup.FixedDeposit)
                        {
                            filter = "MERGE_GROUP_ID=" + (int)FixedLedgerGroup.FixedDeposit;
                        }
                        else 
                        {
                            filter = "MERGE_GROUP_ID <> " + (int)FixedLedgerGroup.FixedDeposit;
                        }

                        DataTable dtHeadOfficeLedgers = grdlkp.Properties.DataSource as DataTable;
                        if (dtHeadOfficeLedgers != null && dtHeadOfficeLedgers.Rows.Count > 0)
                        {
                            DataView dvHeadOfficeLedgers = new DataView(dtHeadOfficeLedgers);
                            dvHeadOfficeLedgers.RowFilter = filter;
                            if (dvHeadOfficeLedgers != null)
                            {
                                grdlkp.Properties.DataSource = dvHeadOfficeLedgers.ToTable();
                                this.gvUnmappedLedgers.PostEditor();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            dtProjectImportedMappedLedger.TableName = "MAPPED_LEDGERS";
            dtProjectImportedMappedLedger.Clear();
            this.Close();
        }

        private void rglkpExistingLedgers_Validating(object sender, CancelEventArgs e)
        {
            GridLookUpEdit gridLKPEdit = sender as GridLookUpEdit;
            if (gridLKPEdit.EditValue != null)
            {
                string mergeledgername = gridLKPEdit.Text;
                gvUnmappedLedgers.SetFocusedRowCellValue(colExistingLedgerName, mergeledgername);
                gvUnmappedLedgers.PostEditor();
                //gvUnmappedLedgers.UpdateCurrentRow();
            }
        }
        #endregion

        #region Methods


        private bool IsValidMismatchedGrid()
        {
            DataTable dtNewLedgers = gcUnMappedLedgers.DataSource as DataTable;
            int mappedLedgerId = 0;
            string mappedLedgerName = string.Empty;
            int RowPosition = 0;
            bool isValid = true;

            if (dtNewLedgers != null && dtNewLedgers.Rows.Count > 0)
            {
                foreach (DataRow drTrans in dtNewLedgers.Rows)
                {
                    mappedLedgerId = this.UtilityMember.NumberSet.ToInteger(drTrans["MERGE_LEDGER_ID"].ToString());
                    mappedLedgerName = drTrans["MERGE_LEDGER_NAME"].ToString();

                    if (mappedLedgerId == 0 || String.IsNullOrEmpty(mappedLedgerName))
                    {
                        this.ShowMessageBox("Map all Imported Ledgers with existing Ledgers.");
                        isValid = false;
                        break;
                    }
                    RowPosition = RowPosition + 1;
                }

                if (!isValid)
                {
                    gvUnmappedLedgers.FocusedColumn = colExistingLedger;
                    gvUnmappedLedgers.CloseEditor();
                    gvUnmappedLedgers.FocusedRowHandle = gvUnmappedLedgers.GetRowHandle(RowPosition);
                    gvUnmappedLedgers.ShowEditor();
                }
            }
            return isValid;
        }

        /// <summary>
        /// load lookup of Edit and assign Members
        /// </summary>
        private void LoadExistingLedgers()
        {
            try
            {
                using (LedgerSystem ledgersystem = new LedgerSystem())
                {
                    resultArgs = ledgersystem.FetchBOLedgers();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtExistingLedgers = resultArgs.DataSource.Table;
                        string filter = "MERGE_GROUP_ID NOT IN (" + (int)FixedLedgerGroup.Cash + "," + (int)FixedLedgerGroup.BankAccounts + "," + (int)FixedLedgerGroup.FixedDeposit + ")";
                        filter += " AND ACCESS_FLAG = 0";
                        dtExistingLedgers.DefaultView.RowFilter = filter;
                        dtExistingLedgers.DefaultView.Sort = ledgersystem.AppSchema.ProjectImportExport.MERGE_LEDGER_NAMEColumn.ColumnName;
                        dtExistingLedgers = dtExistingLedgers.DefaultView.ToTable();
                        rglkpExistingLedgers.DataSource = dtExistingLedgers;
                        rglkpExistingLedgers.ValueMember = ledgersystem.AppSchema.ProjectImportExport.MERGE_LEDGER_IDColumn.ColumnName;
                        rglkpExistingLedgers.DisplayMember = ledgersystem.AppSchema.ProjectImportExport.MERGE_LEDGER_NAMEColumn.ColumnName;

                        filter = "GROUP_ID NOT IN (" + (int)FixedLedgerGroup.Cash + "," + (int)FixedLedgerGroup.BankAccounts + "," + (int)FixedLedgerGroup.FixedDeposit + ")";
                        filter += " AND ACCESS_FLAG = 0";
                        dtProjectImportedMappedLedger.DefaultView.RowFilter = filter;
                        dtProjectImportedMappedLedger = dtProjectImportedMappedLedger.DefaultView.ToTable();
                        
                        //Assign mataching ledgers with existsing ledgers
                        using (LedgerSystem ledgerSys = new LedgerSystem())
                        {
                            ledgerSys.MapProjectImportedLedgerWithLedgers(dtProjectImportedMappedLedger, dtExistingLedgers);
                        }

                        dtProjectImportedMappedLedger.DefaultView.Sort = ledgersystem.AppSchema.ProjectImportExport.MERGE_LEDGER_NAMEColumn.ColumnName;
                        dtProjectImportedMappedLedger = dtProjectImportedMappedLedger.DefaultView.ToTable();
                        gcUnMappedLedgers.DataSource = dtProjectImportedMappedLedger;


                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
        }

        /// <summary>
        /// Load Ledgers Mapped and UnMapped ledgers
        /// </summary>
        public void LoadUnMappedLedgers()
        {
            try
            {
                if (dtProjectImportedMappedLedger.Rows.Count > 0)
                {
                    gcUnMappedLedgers.DataSource = dtProjectImportedMappedLedger;
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
        }

        /// <summary>
        /// Map Branchoffice and Headoffice Ledgers
        /// </summary>
        public void MapImportedLedgers()
        {
            resultArgs.Success = true;
            try
            {
                using (ImportMasterSystem importSystem = new ImportMasterSystem())
                {
                    //this.ShowWaitDialog("Mapping Branch Ledgers with Head Office Ledger..");
                    this.ShowWaitDialog("Mapping Project Imported Ledgers with existing Ledgers");
                    gvUnmappedLedgers.CloseEditor();
                    DataTable dtMappedProjextImportedLedgers = gcUnMappedLedgers.DataSource as DataTable;
                    using (LedgerSystem ledgersystem = new LedgerSystem())
                    {
                        resultArgs = ledgersystem.InsertUpdateProjectImportedLedgersWithLedgers(dtMappedProjextImportedLedgers);
                    }

                    if (resultArgs.Success)
                    {
                        dtProjectImportedMappedLedger = dtMappedProjextImportedLedgers.DefaultView.ToTable(false, new string[] { "LEDGER_NAME", "LEDGER_GROUP", "MERGE_LEDGER_ID", "MERGE_LEDGER_NAME" });
                        dtProjectImportedMappedLedger.TableName = "MAPPED_LEDGERS";
                    }
                    CloseWaitDialog();
                }
            }
            catch (Exception ex)
            {
                CloseWaitDialog();
                resultArgs.Message = ex.ToString();
            }

            if (resultArgs.Success)
            {
                CloseWaitDialog();
                this.DialogResult = System.Windows.Forms.DialogResult.OK;
                this.ShowSuccessMessage("Mapping Project Imported Ledgers with existing Ledgers");
                this.Close();
            }
        }
        #endregion

    }
}