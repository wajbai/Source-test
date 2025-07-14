using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using ACPP.Modules;
using Bosco.Model.UIModel;
using Bosco.Utility;
using DevExpress.XtraEditors;
using DevExpress.XtraEditors.Repository;

namespace DataMigration.Resource
{
    public partial class MigrationMapping : frmBaseAdd
    {
        #region Variables
        ResultArgs resultArgs = null;
        #endregion

        #region Construtor
        public MigrationMapping()
        {
            InitializeComponent();
        }

        #endregion

        #region Events
        private void MigrationMapping_Load(object sender, EventArgs e)
        {
            LoadLedgers();
            // BindLedgerToLookUp();
        }
        #endregion

        #region Methods
        private void LoadLedgers()
        {
            using (LedgerSystem ledgerSystem = new LedgerSystem())
            {
                resultArgs = ledgerSystem.FetchLedgerDetails();
                if (resultArgs.Success && resultArgs != null)
                {
                    gcMapMigratedLedger.DataSource = resultArgs.DataSource.Table;
                    DataView dvLedgers = gvMapMigratedLedger.DataSource as DataView;

                    repglkpMapLedgers.DataSource = AddEmptySpace(dvLedgers.ToTable(), ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName);
                    repglkpMapLedgers.DisplayMember = "LEDGER_NAME";
                    repglkpMapLedgers.ValueMember = "LEDGER_ID";
                }
            }
        }

        private void BindLedgerToLookUp()
        {
            using (LedgerSystem ledgerSystem = new LedgerSystem())
            {
                resultArgs = ledgerSystem.FetchLedgerDetails();
                if (resultArgs.Success && resultArgs != null)
                {
                    repglkpMapLedgers.DataSource = AddEmptySpace(resultArgs.DataSource.Table, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName);
                    repglkpMapLedgers.DisplayMember = "LEDGER_NAME";
                    repglkpMapLedgers.ValueMember = "LEDGER_ID";
                }
            }
        }

        private void MapLedgers()
        {
            DataView dvLedgers = gvMapMigratedLedger.DataSource as DataView;
            DataTable dtMainLedgers = AddEmptySpace(dvLedgers.ToTable(), "LEDGER_ID", "LEDGER_NAME"); ;
            DataTable dtMappLedgers = repglkpMapLedgers.DataSource as DataTable;
            int Count = 0;
            if (dtMainLedgers != null && (repglkpMapLedgers.DataSource as DataTable) != null)
            {
                foreach (DataRow dr in dtMainLedgers.Rows)
                {
                    using (MappingSystem MapLedgers = new MappingSystem())
                    {
                        if (!repglkpMapLedgers.GetDisplayText(Count).Equals(string.Empty))
                        {
                            //MapLedgers.LedgerId = UtilityMember.NumberSet.ToInteger(dr["LEDGER_ID"].ToString());
                            //MapLedgers.MapLedgerId = UtilityMember.NumberSet.ToInteger(repglkpMapLedgers.GetKeyValue(Count++).ToString());
                            //if (!MapLedgers.LedgerId.Equals(MapLedgers.MapLedgerId))
                            //    resultArgs = MapLedgers.MergeMigratedLedger();

                            MapLedgers.LedgerIdCollection = dr["LEDGER_ID"].ToString();
                            MapLedgers.MapLedgerId = UtilityMember.NumberSet.ToInteger(repglkpMapLedgers.GetKeyValue(Count++).ToString());
                            if (!MapLedgers.LedgerIdCollection.Equals(MapLedgers.MapLedgerId))
                                resultArgs = MapLedgers.MergeMigratedLedger();
                        }
                        else Count++;
                    }

                }
            }
        }

        private DataTable AddEmptySpace(DataTable dtLedgerName, string ValueField, string DisplayField)
        {
            DataRow dr = null;
            if (dtLedgerName.Columns.Contains(ValueField) && dtLedgerName.Columns.Contains(DisplayField) && UtilityMember.NumberSet.ToInteger(dtLedgerName.Rows[0][0].ToString()) != 0)
            {
                dr = dtLedgerName.NewRow();
                dr[ValueField] = 0;
                dr[DisplayField] = String.Empty;
                dtLedgerName.Rows.InsertAt(dr, 0);
            }
            return dtLedgerName;
        }
        #endregion

        private void btnMapLedger_Click(object sender, EventArgs e)
        {
            MapLedgers();
            LoadLedgers();
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvMapMigratedLedger.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
                this.SetFocusRowFilter(gvMapMigratedLedger, gvColLedgerName);
        }
    }
}
