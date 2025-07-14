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
    public partial class frmProjectImportMappLedgers : frmFinanceBaseAdd
    {
        #region Variables
        ResultArgs resultArgs = new ResultArgs();
        public DataTable dtProjectImportedMappedLedger = new DataTable();
        private bool IsMappingRequired = false;
        #endregion

        #region Constructor
        public frmProjectImportMappLedgers()
        {
            InitializeComponent();
        }

        public frmProjectImportMappLedgers(DataTable dtLedgers, bool mapAll)
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
                        DataTable dtResult = dtProjectImportedMappedLedger.Clone();
                        
                        if (dtProjectImportedMappedLedger.Rows.Count > 0)
                        {

                            var result = from drRow in dtProjectImportedMappedLedger.AsEnumerable()
                                         join drExistingRow in dtExistingLedgers.AsEnumerable()
                                         on drRow.Field<string>("LEDGER_NAME").Trim().ToUpper() equals drExistingRow.Field<string>("MERGE_LEDGER_NAME").Trim().ToUpper()
                                         into lj
                                         from drExistingRow in lj.DefaultIfEmpty()
                                         select dtResult.LoadDataRow(new object[]
                                     {
                                        (drRow.Field<String>("LEDGER_CODE")==null? string.Empty : drRow.Field<String>("LEDGER_CODE")), 
                                        drRow.Field<UInt32>("IS_COST_CENTER"),
                                        drRow.Field<UInt32>("IS_BANK_INTEREST_LEDGER"),
                                        drRow.Field<string>("LEDGER_NAME"),
                                        drRow.Field<string>("LEDGER_GROUP"),
                                        drRow.Field<Int64>("IS_BRANCH_LEDGER"),
                                        drRow.Field<UInt32>("GROUP_ID"),
                                        drRow.Field<UInt32>("NATURE_ID"),
                                        drRow.Field<UInt32>("ACCESS_FLAG"),
                                        drRow.Field<string>("LEDGER_TYPE"),
                                        drRow.Field<string>("LEDGER_SUB_TYPE"),
                                        drRow.Field<UInt32>("SORT_ID"),
                                        drRow.Field<UInt32>("IS_COST_CENTER"),
                                        drRow.Field<UInt32>("IS_TDS_LEDGER"),
                                        drRow.Field<UInt32>("IS_BANK_INTEREST_LEDGER"),
                                        drRow.Field<UInt32>("IS_INKIND_LEDGER"),
                                        drRow.Field<UInt32>("IS_DEPRECIATION_LEDGER"),
                                        drRow.Field<UInt32>("IS_ASSET_GAIN_LEDGER"),
                                        drRow.Field<UInt32>("IS_ASSET_LOSS_LEDGER"),
                                        drRow.Field<UInt32>("IS_DISPOSAL_LEDGER"),
                                        drRow.Field<UInt32>("IS_SUBSIDY_LEDGER"),
                                        drRow.Field<UInt32>("IS_GST_LEDGERS"),
                                        drRow.Field<UInt32>("GST_SERVICE_TYPE"),
                                        (drRow.Field<Nullable<UInt32>>("GST_ID")==null? 0 : drRow.Field<Nullable<UInt32>>("GST_ID")), 
                                        drRow.Field<string>("GST_NO"),
                                        drRow.Field<string>("SLAB"),
                                        drRow.Field<Nullable<DateTime>>("DATE_CLOSED"),
                                        drRow.Field<UInt32>("BUDGET_GROUP_ID"),
                                        drRow.Field<UInt32>("BUDGET_SUB_GROUP_ID"),
                                        drExistingRow==null? 0 : drExistingRow.Field<UInt32>("MERGE_LEDGER_ID"),
                                        drExistingRow==null? string.Empty : drExistingRow.Field<string>("MERGE_LEDGER_NAME"),
                                     }, false);

                            dtProjectImportedMappedLedger = result.CopyToDataTable();
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
        #endregion

        #region Methods
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
                    DataTable dtLedgers = gcUnMappedLedgers.DataSource as DataTable;

                    dtProjectImportedMappedLedger = dtLedgers.DefaultView.ToTable(false, new string[] { "LEDGER_NAME", "LEDGER_GROUP", "MERGE_LEDGER_ID", "MERGE_LEDGER_NAME" });
                    dtProjectImportedMappedLedger.TableName = "MAPPED_LEDGERS";
                                        
                    //foreach (DataRow dr in dtLedgers.Rows)
                    //{
                    //    int BranchLedgerId = this.UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());

                    //    if (BranchLedgerId > 0)
                    //    {
                    //        importSystem.LedgerId = BranchLedgerId;
                    //        resultArgs = importSystem.DeleteMappedLedger();
                    //    }

                    //    int HeadOfficeLedgerId = this.UtilityMember.NumberSet.ToInteger(dr["HEADOFFICE_LEDGER_ID"].ToString());
                    //    if (HeadOfficeLedgerId > 0)
                    //    {
                    //        if (resultArgs.Success)
                    //        {
                    //            importSystem.LedgerId = BranchLedgerId;
                    //            importSystem.HeadOfficeLedgerId = HeadOfficeLedgerId;
                    //            resultArgs = importSystem.MapHeadOfficeLedger();

                    //            //On 06/07/2018, update newly created BO ledger's Budget Group and Budget Sub Group from HO ledger's Budget Group and Budget Sub Group
                    //            if (resultArgs.Success)
                    //            {
                    //                resultArgs = importSystem.UpdateLedgerBudgetGroup();
                    //            }
                    //        }
                    //    }
                    //}
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

        //private void gvUnmappedLedgers_CellValueChanged(object sender, CellValueChangedEventArgs e)
        //{
        //    //GridView view = sender as GridView;
        //    //if (view == null) return;
        //    //if (e.Column.FieldName != colExistingLedger.FieldName)
        //    //{
        //    //    string mergeledgername  = view.GetRowCellValue(e.RowHandle, view.Columns[collkpExistingLedgerName.FieldName]).ToString();
        //    //    view.SetRowCellValue(e.RowHandle, colExistingLedgerName, mergeledgername);
        //    //}
        //}

    }
}