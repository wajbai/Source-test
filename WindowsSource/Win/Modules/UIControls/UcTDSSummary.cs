using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;

namespace ACPP.Modules.UIControls
{
    public partial class UcTDSSummary : DevExpress.XtraEditors.XtraUserControl
    {
        #region Variables

        #endregion

        #region Properties
        public DataTable UpdateTDSSummary
        {
            set
            {
                if (value != null)
                {
                    gcTDSSummary.DataSource = value;
                }
            }
            get
            {
                return gcTDSSummary.DataSource as DataTable;
            }
        }

        public GridView UcTransGrid
        {
            get { return gvTDSSummary; }
        }
        public GridColumn UcTransColumn
        {
            get { return colTransMode; }
        }
        public GridColumn UcLedgerColumn
        {
            get { return colNOP; }
        }
        
        public GridColumn UcAmount
        {
            get { return colAmount; }
        }

        #endregion

        #region Events
        public event EventHandler PreviewKeyDownEvent;
        public event EventHandler RowCellStyleEvent;
        #endregion

        #region Constructor
        public UcTDSSummary()
        {
            InitializeComponent();
        }
        #endregion

        private void UcTDSSummary_Load(object sender, EventArgs e)
        {
            // ConstructTable();
        }

        #region Events
        private void gcTDSSummary_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (PreviewKeyDownEvent != null)
            {
                if (gvTDSSummary.IsLastRow && gvTDSSummary.FocusedColumn == colTransMode)
                {
                    PreviewKeyDownEvent(this, e);
                }
            }
        }

        private void gvTDSSummary_RowCellStyle(object sender, RowCellStyleEventArgs e)
        {
            if (RowCellStyleEvent != null)
            {
                RowCellStyleEvent(this, e);
            }
        }

        #endregion

        #region Methods
        public DataTable ConstructTable()
        {
            DataTable dtTransSummary = new DataTable();
            try
            {
                dtTransSummary.Columns.Add("NATURE_OF_PAYMENT_ID", typeof(int));
                dtTransSummary.Columns.Add("NATURE_PAYMENTS", typeof(string));
                dtTransSummary.Columns.Add("LEDGER_ID", typeof(int));
                dtTransSummary.Columns.Add("DEBIT", typeof(decimal));
                dtTransSummary.Columns.Add("CREDIT", typeof(decimal));
                dtTransSummary.Columns.Add("AMOUNT", typeof(decimal));
                dtTransSummary.Columns.Add("TRANS_MODE", typeof(string));
                dtTransSummary.Columns.Add("VALUE", typeof(int));
                UpdateTDSSummary = dtTransSummary;
            }
            catch (Exception ex)
            {
            }
            finally { }
            return dtTransSummary;
        }
        #endregion
    }
}
