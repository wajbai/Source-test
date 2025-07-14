using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Model.UIModel;
using Bosco.Utility;

namespace ACPP.Modules.Data_Utility
{
    public partial class frmMapBeforeMigration : frmFinanceBaseAdd
    {

        #region Variables and Properties
        public DataTable dtLedgerToBeMigrated;
        private DataTable dtAcMeERPLedgers;
        private DataTable dtAllAcMeERPLedger;
        MigrationType migrationType;
        #endregion

        #region Constructor
        //No default constructor is needed
        //Making the object to pass a datatable as a paramter
        public frmMapBeforeMigration(DataTable dtMigrationLedgers, MigrationType type = MigrationType.AcMePlus)
        {
            InitializeComponent();
            dtLedgerToBeMigrated = dtMigrationLedgers;
            migrationType = type;
            dtAllAcMeERPLedger = dtAcMeERPLedgers = LoadAcMEERPLedgers();
        }
        #endregion

        #region Events
        private void frmMapBeforeMigration_Load(object sender, EventArgs e)
        {
            if (migrationType == MigrationType.Tally) colLCode.Visible = false;
            else colLCode.Visible = true;
            BindData();
        }

        private void chkAcMeERPLedgerFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvAcMeERPLedgers.OptionsView.ShowAutoFilterRow = chkAcMeERPLedgerFilter.Checked;
            if (chkAcMeERPLedgerFilter.Checked)
            {
                this.SetFocusRowFilter(gvAcMeERPLedgers, colALedgerName);
            }
        }

        private void chkNewLedgerShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvNewLedgers.OptionsView.ShowAutoFilterRow = chkNewLedgerShowFilter.Checked;
            if (chkNewLedgerShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvNewLedgers, colLLedger);
            }
        }

        private void gvAcMeERPLedgers_RowCountChanged(object sender, EventArgs e)
        {
            lblAcMeERPRowCount.Text = gvAcMeERPLedgers.RowCount.ToString();
        }

        private void gvNewLedgers_RowCountChanged(object sender, EventArgs e)
        {
            lblNewLedgerCount.Text = gvNewLedgers.RowCount.ToString();
        }

        private void btnMapLedgers_Click(object sender, EventArgs e)
        {
            MappLedgers();
        }

        private void btnUnMapLedgers_Click(object sender, EventArgs e)
        {
            DataTable dtMappedLedger = gcMappedLedgers.DataSource as DataTable;
            DataTable dtNewLedger = gcNewLedgers.DataSource as DataTable;
            DataTable dtAcMeERPLedgers = gcAcMeERPLedgers.DataSource as DataTable;
            string LedgerName = gvMappedLedgers.GetRowCellValue(gvMappedLedgers.FocusedRowHandle, "LEDGER_NAME").ToString();
            DataView dvMappedLedger = new DataView(dtMappedLedger);
            LedgerName = LedgerName.Contains('\'') ? LedgerName.Replace('\'', ' ') : LedgerName;
            dvMappedLedger.RowFilter = String.Format("LEDGER_NAME<>'{0}'", LedgerName);
            dtMappedLedger = dvMappedLedger.ToTable();
            gcMappedLedgers.DataSource = dtMappedLedger;

            if (dtMappedLedger != null && dtMappedLedger.Rows.Count > 0)
            {
                DataTable dtAcMeERLLedgerFilter = dtMappedLedger.AsEnumerable().GroupBy(r => r.Field<String>("LEDGER_NAME")).Select(g => g.First()).CopyToDataTable();
                if (dtAcMeERLLedgerFilter != null)
                {
                    string MakeCondition = string.Empty;
                    int InitialCount = 0;
                    int TotalCount = dtAcMeERLLedgerFilter == null ? 0 : dtAcMeERLLedgerFilter.Rows.Count;
                    foreach (DataRow drItem in dtAcMeERLLedgerFilter.Rows)
                    {
                        string Led = drItem["LEDGER_NAME"].ToString().Contains('\'') ? drItem["LEDGER_NAME"].ToString().Replace('\'', ' ') : drItem["LEDGER_NAME"].ToString();
                        if (++InitialCount == TotalCount)
                            MakeCondition += String.Format("LEDGER_NAME<>'{0}'", drItem["LEDGER_NAME"].ToString());
                        else
                            MakeCondition += String.Format("LEDGER_NAME<>'{0}' AND ", drItem["LEDGER_NAME"].ToString());
                    }

                    DataTable dtERPLedger = gcAcMeERPLedgers.DataSource as DataTable;
                    if (dtERPLedger != null)
                    {
                        DataView dvAllAcMeERPLedger = new DataView(dtAllAcMeERPLedger);
                        dvAllAcMeERPLedger.RowFilter = MakeCondition;

                        DataTable dtUnSelect = dvAllAcMeERPLedger.ToTable();
                        dtUnSelect.Select().ToList<DataRow>().ForEach(r => r["SELECT"] = 0);
                        gcAcMeERPLedgers.DataSource = dtUnSelect;
                    }
                }
            }
            else
            {
                dtAllAcMeERPLedger.Select().ToList<DataRow>().ForEach(r => r["SELECT"] = 0);
                gcAcMeERPLedgers.DataSource = dtAllAcMeERPLedger;
            }


            //Bind back againg to the New Ledgers
            if (dtMappedLedger != null && dtMappedLedger.Rows.Count > 0)
            {
                int InitialCount = 0;
                string MakeCondition = string.Empty;
                int TotalCount = dtMappedLedger == null ? 0 : dtMappedLedger.Rows.Count;
                foreach (DataRow drItem in dtMappedLedger.Rows)
                {

                    if (++InitialCount == TotalCount)
                        MakeCondition += String.Format("LedgerId<>{0}", drItem["LedgerId"].ToString());
                    else
                        MakeCondition += String.Format("LedgerId<>{0} AND ", drItem["LedgerId"].ToString());
                }

                DataTable dtAllNewLedger = gcNewLedgers.DataSource as DataTable;
                if (dtAllNewLedger != null)
                {
                    DataView dvAllMigratedLedger = new DataView(dtLedgerToBeMigrated);
                    dvAllMigratedLedger.RowFilter = MakeCondition;

                    DataTable dtUnSelect = dvAllMigratedLedger.ToTable();
                    dtUnSelect.Select().ToList<DataRow>().ForEach(r => r["SELECT"] = 0);
                    gcNewLedgers.DataSource = dtUnSelect;
                }

            }
            else
            {
                dtLedgerToBeMigrated.Select().ToList<DataRow>().ForEach(r => r["SELECT"] = 0);
                gcNewLedgers.DataSource = dtLedgerToBeMigrated;
            }

        }

        private void btnMap_Click(object sender, EventArgs e)
        {
            DataTable dtNewLedger = gcNewLedgers.DataSource as DataTable;
            DataTable dtMapped = gcMappedLedgers.DataSource as DataTable;
            if (!dtNewLedger.Columns.Contains("LEDGER_ID"))
            {
                dtNewLedger.Columns.Add("LEDGER_ID", typeof(System.Int32));
                dtNewLedger.Columns.Add("LEDGER_NAME", typeof(System.String));
            }
            dtNewLedger.Select().ToList<DataRow>().ForEach(r => r["LEDGER_ID"] = 0);
            if (dtMapped != null)
            {
                dtNewLedger.Merge(dtMapped);
            }
            dtLedgerToBeMigrated = dtNewLedger;
            this.Close();
        }

        private void btnSkip_Click(object sender, EventArgs e)
        {
            DataTable dtNewLedger = gcNewLedgers.DataSource as DataTable;
            if (!dtNewLedger.Columns.Contains("LEDGER_ID"))
            {
                dtNewLedger.Columns.Add("LEDGER_ID", typeof(System.Int32));
                dtNewLedger.Columns.Add("LEDGER_NAME", typeof(System.String));
            }
            dtNewLedger.Select().ToList<DataRow>().ForEach(r => r["LEDGER_ID"] = 0);
            dtLedgerToBeMigrated = dtNewLedger;
            this.Close();
        }
        #endregion

        #region Methods

        private void UnSelectPayroll(int ledgerId, int status)
        {
            DataTable dtPayrollSource = (DataTable)gcAcMeERPLedgers.DataSource;
            for (int i = 0; i < dtPayrollSource.Rows.Count; i++)
            {
                int LedgerId = dtPayrollSource.Rows[i]["LEDGER_ID"] != DBNull.Value ? UtilityMember.NumberSet.ToInteger(dtPayrollSource.Rows[i]["LEDGER_ID"].ToString()) : 0;
                if (LedgerId == ledgerId)
                {
                    dtPayrollSource.Rows[i]["SELECT"] = status;
                }
                else
                {
                    dtPayrollSource.Rows[i]["SELECT"] = (int)YesNo.No;
                }
            }
        }

        /// <summary>
        /// Load all the AcMeERP Ledgers
        /// </summary>
        /// <returns></returns>
        private DataTable LoadAcMEERPLedgers()
        {
            DataTable dtLedger = null;
            using (LedgerSystem ledger = new LedgerSystem())
            {
                ResultArgs resultArgs = ledger.FetchLedgerDetails();
                if (resultArgs.Success && resultArgs.DataSource.Table != null)
                    dtLedger = resultArgs.DataSource.Table;
            }
            return dtLedger;
        }

        private void BindData()
        {
            gcAcMeERPLedgers.DataSource = AddSelectColumn(dtAcMeERPLedgers);
            gcNewLedgers.DataSource = AddSelectColumn(dtLedgerToBeMigrated);
        }

        private DataTable AddSelectColumn(DataTable dtTableName)
        {

            if (!dtTableName.Columns.Contains("SELECT"))
            {
                //Adding SELECT column to the DataTable
                dtTableName.Columns.Add("SELECT", typeof(System.Int32));
                //Updating the newly added column value to its default values
                dtTableName.Select().ToList<DataRow>().ForEach(r => r["SELECT"] = 0);
            }
            return dtTableName;
        }

        private void MappLedgers()
        {
            //Getting only selected AcMeERP Ledgers
            DataView dvAcMeERPLedger = new DataView(gcAcMeERPLedgers.DataSource as DataTable);
            dvAcMeERPLedger.RowFilter = "SELECT=1";
            DataTable dtAcMeERPLedger = dvAcMeERPLedger.ToTable();
            if (dtAcMeERPLedger != null && dtAcMeERPLedger.Rows.Count > 0)
            {
                //Getting all the Selected New Ledgers (Going to be Migrated either from Tally or AcMePlus)
                DataView dvNewLedgers = new DataView(gcNewLedgers.DataSource as DataTable);
                dvNewLedgers.RowFilter = "SELECT=1";
                DataTable dtNewLedger = dvNewLedgers.ToTable();
                if (dtNewLedger != null && dtNewLedger.Rows.Count > 0)
                {
                    //Making available the Ledgers from dtAcMeERPLedger Table and dtNewLedger Table 
                    //into single Table dtNewLedger,We create these two columns and Fill the columns with value
                    dtNewLedger.Columns.Add("LEDGER_ID", typeof(System.Int32));
                    dtNewLedger.Columns.Add("LEDGER_NAME", typeof(System.String));
                    foreach (DataRow drItem in dtNewLedger.Rows)
                    {
                        drItem["LEDGER_ID"] = dtAcMeERPLedger.Rows[0]["LEDGER_ID"];
                        drItem["LEDGER_NAME"] = dtAcMeERPLedger.Rows[0]["LEDGER_NAME"];
                    }
                    dtNewLedger.AcceptChanges();

                    if ((gcMappedLedgers.DataSource as DataTable) == null)
                        gcMappedLedgers.DataSource = dtNewLedger;
                    else
                    {
                        //If some rows are available merge the new rows with the old rows
                        DataTable dtMergeLedgers = gcMappedLedgers.DataSource as DataTable;
                        dtMergeLedgers.Merge(dtNewLedger);
                        gcMappedLedgers.DataSource = dtMergeLedgers;
                    }
                    //Reset the Grid with unselected rows and remove selected rows
                    dvAcMeERPLedger.RowFilter = "SELECT=0";
                    if (dvAcMeERPLedger.ToTable() != null) gcAcMeERPLedgers.DataSource = dvAcMeERPLedger.ToTable();
                    else gcAcMeERPLedgers.DataSource = null;

                    //Reset the Grid with unselected rows and remove selected rows
                    dvNewLedgers.RowFilter = "SELECT=0";
                    if (dvNewLedgers.ToTable() != null) gcNewLedgers.DataSource = dvNewLedgers.ToTable();
                    else gcNewLedgers.DataSource = null;
                }
                //else ShowMessageBox("Select Ledgers to be Mapped");
                else ShowMessageBox(this.GetMessage(MessageCatalog.Master.DataUtilityForms.SELECT_LEDGERS_TO_BE_MAPPED));
            }
            //else ShowMessageBox("Select AcMeERP Ledger to Map");
            else ShowMessageBox(this.GetMessage(MessageCatalog.Master.DataUtilityForms.SELECT_ACMEERP_LEDGER_TO_MAP));
        }
        #endregion

        private void reprbtnSelect_Click(object sender, EventArgs e)
        {
            if (gvAcMeERPLedgers.RowCount > 0)
            {
                int LedgerId = gvAcMeERPLedgers.GetFocusedRowCellValue(colALedgerId) != null ? UtilityMember.NumberSet.ToInteger(gvAcMeERPLedgers.GetFocusedRowCellValue(colALedgerId).ToString()) : 0;
                CheckEdit chkEdit = sender as CheckEdit;
                int status = Convert.ToInt32(chkEdit.CheckState);
                UnSelectPayroll(LedgerId, status);
            }
        }



    }
}