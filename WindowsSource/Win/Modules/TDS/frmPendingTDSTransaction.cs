using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraGrid.Views.Grid;
using Bosco.Model.UIModel;
using Bosco.Utility;

namespace ACPP.Modules.TDS
{
    public partial class frmPendingTDSTransaction : frmFinanceBaseAdd
    {
        #region Variables
        #endregion

        #region Properties
        Bosco.Model.TDS.TDSDeductionSystem pendingDeduction = new Bosco.Model.TDS.TDSDeductionSystem();
        public DataTable dtPendingTDSTransaction { get; set; }
        public DataTable dePendingTDSSelected { get; set; }
        public string BookingId { get; set; }
        TDSPendingType PendingType;
        string FilterRowId = string.Empty;
        #endregion

        #region Constructor
        public frmPendingTDSTransaction()
        {
            InitializeComponent();
        }

        public frmPendingTDSTransaction(TDSPendingType PendingType)
            : this()
        {
            this.PendingType = PendingType;
        }
        #endregion

        #region Events
        private void frmPendingTDSTransaction_Load(object sender, EventArgs e)
        {
            switch (PendingType)
            {
                case TDSPendingType.DeductTDSPending:
                    {
                        gvPendingTDS.Columns[2].Visible = true;
                        gvPendingTDS.Columns[3].Visible = true;
                        gvPendingTDS.Columns[4].Visible = true;
                        gvPendingTDS.Columns[5].Visible = false;
                        gvPendingTDS.Columns[6].Visible = false;
                        FilterRowId = "BOOKING_DETAIL_ID";
                        break;
                    }
                case TDSPendingType.PartyPaymentPending:
                    {
                        gvPendingTDS.Columns[5].Visible = true;
                        gvPendingTDS.Columns[6].Visible = true;
                        gvPendingTDS.Columns[2].Visible = false;
                        gvPendingTDS.Columns[3].Visible = false;
                        gvPendingTDS.Columns[4].Visible = false;
                        FilterRowId = "BOOKING_ID";
                        break;
                    }
            }
            gcPendingTDS.DataSource = dtPendingTDSTransaction;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if ((gcPendingTDS.DataSource as DataTable) != null && (gcPendingTDS.DataSource as DataTable).Rows.Count > 0)
            {
                string BookingDetailId = GetSelectedRows(gvPendingTDS);
                if (!string.IsNullOrEmpty(BookingDetailId))
                {
                    DataView dvTDSLedger = new DataView((gcPendingTDS.DataSource as DataTable));
                    dvTDSLedger.RowFilter = String.Format("{1} IN({0})", BookingDetailId, FilterRowId);
                    dePendingTDSSelected = dvTDSLedger.ToTable();
                    this.Close();
                }
                else
                {
                    ShowMessageBox(GetMessage(MessageCatalog.TDS.TDSPolicy.TDS_SELECT_EMPTY));
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gcPendingTDS_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode.Equals(Keys.Tab))
            {
                if (gvPendingTDS.IsLastRow && gvPendingTDS.FocusedColumn.Equals(colTDSBalance))
                {
                    btnOk.Focus();
                }
            }
        }
        #endregion

        #region Methods

        /// <summary>
        /// Getting all the selected rows of the GridControl
        /// </summary>
        /// <param name="view"></param>
        /// <returns>returns Collection of selected Voucher Id  </returns>
        string GetSelectedRows(GridView view)
        {
            string BookingDetailId = "";
            if (view.OptionsSelection.MultiSelectMode != GridMultiSelectMode.CellSelect)
            {
                foreach (int i in gvPendingTDS.GetSelectedRows())
                {
                    DataRow row = gvPendingTDS.GetDataRow(i);
                    BookingDetailId += String.Format("{0},", row[FilterRowId]);
                    BookingId += row["BOOKING_ID"] + ",";
                }
                BookingId = BookingId.TrimEnd(',');
                BookingDetailId = BookingDetailId.TrimEnd(',');
            }
            return BookingDetailId;
        }
        #endregion

        private void gcPendingTDS_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if (gvPendingTDS.IsLastRow)
            {
                e.SuppressKeyPress = true;
                e.Handled = true;
                btnOk.Focus();
            }
        }
    }
}