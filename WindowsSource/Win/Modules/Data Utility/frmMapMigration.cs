using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.DAO.Data;
using Bosco.Model.UIModel;
using Bosco.Utility;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;

namespace ACPP.Modules.Data_Utility
{
    public partial class frmMapMigration : frmFinanceBaseAdd
    {
        #region Variables
        bool IsMap = false;
        private DataTable dtLedgerSource = new DataTable();
        private DataTable dtLedgerAcMEERP = new DataTable();
        public DataTable dtMappedLedgers;

        #endregion

        #region Properties

        #endregion

        #region Constutor
        public frmMapMigration()
        {
            InitializeComponent();
        }

        public frmMapMigration(DataTable dtSourceLedger)
            : this()
        {
            dtLedgerSource = dtSourceLedger;
            dtLedgerAcMEERP = LoadAcMEERPLedgers();
            BindDataSource();
        }


        #endregion

        #region Events
        private void frmMapMigration_Load(object sender, EventArgs e)
        {

        }

        private void frmMapMigration_Shown(object sender, EventArgs e)
        {
            bandedgvMappedLedgers.FocusedColumn = colACode;
            bandedgvMappedLedgers.ShowEditor();

        }

        private void bandedgvMappedLedgers_GotFocus(object sender, EventArgs e)
        {
            bandedgvMappedLedgers.ShowEditor();
        }

        private void glkpLedgerCode_EditValueChanged(object sender, EventArgs e)
        {
            MapSelecteLedger(sender);
        }

        private void glkpLedger_EditValueChanged(object sender, EventArgs e)
        {
            MapSelecteLedger(sender);
        }

        private void bandedgcMappedLedgers_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter
                || e.KeyCode == Keys.Tab)
            {
                if (bandedgvMappedLedgers.IsLastRow)
                {
                    GridControl grid = sender as GridControl;
                    GridView view = grid.FocusedView as GridView;
                    if (view.IsEditing)
                        view.CloseEditor();
                    grid.SelectNextControl(btnMap, e.Modifiers == Keys.None, true, false, true);
                    btnMap.Focus();
                    e.Handled = true;
                }
                else
                {
                    bandedgvMappedLedgers.MoveNext();
                }
            }
        }
        #endregion

        #region Methods
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

        private void BindDataSource()
        {
            //Mapped Ledgers
            dtMappedLedgers = new DataTable();
            dtMappedLedgers.Columns.Add("LedgerId", typeof(System.Int32));
            dtMappedLedgers.Columns.Add("LedgerCode", typeof(System.String));
            dtMappedLedgers.Columns.Add("LedgerName", typeof(System.String));
            dtMappedLedgers.Columns.Add("Parent", typeof(System.String));
            dtMappedLedgers.Columns.Add("GROUPID", typeof(System.Int32));
            dtMappedLedgers.Columns.Add("PrimaryGroup", typeof(System.String));
            dtMappedLedgers.Columns.Add("OpeningBalance", typeof(System.Double));
            dtMappedLedgers.Columns.Add("ClosingBalance", typeof(System.Double));
            dtMappedLedgers.Columns.Add("IsCostCentersOn", typeof(System.String));
            dtMappedLedgers.Columns.Add("BankHolderName", typeof(System.String));
            dtMappedLedgers.Columns.Add("BankDetails", typeof(System.String));
            dtMappedLedgers.Columns.Add("BankBranchName", typeof(System.String));
            dtMappedLedgers.Columns.Add("BankType", typeof(System.String));
            dtMappedLedgers.Columns.Add("IFSCode", typeof(System.String));
            dtMappedLedgers.Columns.Add("Address", typeof(System.String));
            dtMappedLedgers.Columns.Add("PAN/IT", typeof(System.String));
            dtMappedLedgers.Columns.Add("NameOnPan", typeof(System.String));
            dtMappedLedgers.Columns.Add("TDSDedecteeType", typeof(System.String));
            dtMappedLedgers.Columns.Add("IKHEAD", typeof(System.Int32));
            dtMappedLedgers.Columns.Add("ParentId", typeof(System.Int32));

            dtMappedLedgers.Columns.Add("Ledger_Id", typeof(System.Int32));
            dtMappedLedgers.Columns.Add("Ledger_Code", typeof(System.String));
            dtMappedLedgers.Columns.Add("Ledger_Name", typeof(System.String));
            dtMappedLedgers.Columns.Add("Group", typeof(System.String));

            //Map ledger
            doMapWithAcMEERP();

            //Bind AcMeERP Ledger Code into lookup Edit
            this.glkpLedgerCode.DataSource = dtLedgerAcMEERP;
            this.glkpLedgerCode.ValueMember = "LEDGER_CODE";
            this.glkpLedgerCode.DisplayMember = "LEDGER_CODE";

            //Bind AcMeERP Ledger into lookup Edit
            this.glkpLedger.DataSource = dtLedgerAcMEERP;
            this.glkpLedger.ValueMember = "LEDGER_NAME";
            this.glkpLedger.DisplayMember = "LEDGER_NAME";

            //bind Grid
            DataView dvMappedLedgers = new DataView(dtMappedLedgers);
            dvMappedLedgers.RowFilter = "LedgerId<>0";

            bandedgcMappedLedgers.DataSource = dvMappedLedgers.ToTable();
        }

        private void doMapWithAcMEERP()
        {
            if (dtLedgerSource != null && dtLedgerSource.Rows.Count > 0 && dtLedgerAcMEERP != null && dtLedgerAcMEERP.Rows.Count > 0)
            {
                var resultMapped = from drLedgerSource in dtLedgerSource.AsEnumerable()
                                   join drLedgerAcMEERP in dtLedgerAcMEERP.AsEnumerable()
                                          on drLedgerSource.Field<string>("LedgerName") equals
                                            drLedgerAcMEERP.Field<string>("LEDGER_NAME")
                                            into mapledgeracmeerpjoin
                                   from drLedgerAcMEERP in mapledgeracmeerpjoin.DefaultIfEmpty()
                                   select dtMappedLedgers.LoadDataRow(new object[]
                                    {
                                        drLedgerSource.Field<Int32>("LedgerId"),
                                        drLedgerSource.Field<string>("LedgerCode"),
                                        drLedgerSource.Field<string>("LedgerName"),
                                        drLedgerSource.Field<string>("Parent"),
                                        drLedgerSource.Field<Int32>("GROUPID"),
                                        drLedgerSource.Field<string>("PrimaryGroup"),
                                        drLedgerSource.Field<Int32?>("OpeningBalance"),
                                        drLedgerSource.Field<Int32?>("ClosingBalance"),
                                        drLedgerSource.Field<string>("IsCostCentersOn"),
                                        drLedgerSource.Field<string>("BankHolderName"),
                                        drLedgerSource.Field<string>("BankDetails"),
                                        drLedgerSource.Field<string>("BankBranchName"),
                                        drLedgerSource.Field<string>("BankType"),
                                        drLedgerSource.Field<string>("IFSCode"),
                                        drLedgerSource.Field<string>("Address"),
                                        drLedgerSource.Field<string>("PAN/IT"),
                                        drLedgerSource.Field<string>("NameOnPan"),
                                        drLedgerSource.Field<string>("TDSDedecteeType"),
                                        drLedgerSource.Field<Boolean?>("IKHEAD"),
                                        drLedgerSource.Field<Int32?>("ParentId"),
                                        (drLedgerAcMEERP==null?null:drLedgerAcMEERP.Field<UInt32?>("LEDGER_ID")),
                                        (drLedgerAcMEERP==null?string.Empty:drLedgerAcMEERP.Field<string>("LEDGER_CODE")),
                                        (drLedgerAcMEERP==null?string.Empty:drLedgerAcMEERP.Field<string>("LEDGER_NAME")),
                                        (drLedgerAcMEERP==null?string.Empty:drLedgerAcMEERP.Field<string>("GROUP")),
                                    }, true);

                dtMappedLedgers = resultMapped.CopyToDataTable();
            }
        }

        private void MapSelecteLedger(object sender)
        {
            if (sender != null)
            {
                GridLookUpEdit gridLKPEdit = sender as GridLookUpEdit;
                int index = gridLKPEdit.Properties.GetIndexByKeyValue(gridLKPEdit.EditValue);
                if (index >= 0)
                {
                    int LedgerId = 0;
                    string LedgerCode = string.Empty;
                    string LedgerName = string.Empty;
                    string group = string.Empty;
                    //DataRowView drvLedger = gridLKPEdit.GetSelectedDataRow() as DataRowView;
                    DataRowView drvLedger = gridLKPEdit.Properties.View.GetRow(index) as DataRowView;
                    if (drvLedger != null)
                    {
                        LedgerId = UtilityMember.NumberSet.ToInteger(drvLedger["Ledger_Id"].ToString());
                        LedgerCode = drvLedger["Ledger_Code"].ToString();
                        LedgerName = drvLedger["Ledger_Name"].ToString();
                        group = drvLedger["Group"].ToString();

                        bandedgvMappedLedgers.SetFocusedRowCellValue(colAId, LedgerId);

                        if (bandedgvMappedLedgers.FocusedColumn == colALedger)
                            bandedgvMappedLedgers.SetFocusedRowCellValue(colACode, LedgerCode);
                        else
                            bandedgvMappedLedgers.SetFocusedRowCellValue(colALedger, LedgerName);

                        bandedgvMappedLedgers.SetFocusedRowCellValue(colAGroup, group);
                    }
                }
            }
        }
        #endregion

        private void btnMap_Click(object sender, EventArgs e)
        {
            bandedgvMappedLedgers.PostEditor();
            bandedgvMappedLedgers.UpdateCurrentRow();
            dtMappedLedgers = bandedgcMappedLedgers.DataSource as DataTable;
            IsMap = true;
            this.Close();
        }

        private void btnSkip_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmMapMigration_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!IsMap)
            {
                dtMappedLedgers = bandedgcMappedLedgers.DataSource as DataTable;
                //Updtae Ledger_Id, Ledger_Code, Ledger_Name, Group to null values
                dtMappedLedgers.Select().ToList<DataRow>().ForEach(r => { r["Ledger_Id"] = 0; r["Ledger_Code"] = ""; r["Ledger_Name"] = ""; r["Group"] = ""; });
            }
        }

    }
}