using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using Bosco.Utility;
using Bosco.Model.UIModel;
using DevExpress.XtraGrid;
using DevExpress.XtraLayout.Utils;
using Bosco.Model.Transaction;
using DevExpress.XtraEditors.Repository;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraEditors;
using ACPP.Modules.Master;
using Bosco.Model.UIModel.Master;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Columns;
using DevExpress.XtraGrid.Views.Base;
using Bosco.Model.TDS;

namespace ACPP.Modules.UIControls
{
    public partial class UcPendingTransaction : DevExpress.XtraEditors.XtraUserControl
    {
        #region Decelaration
        public event EventHandler previewEvent;
        ResultArgs resultArgs;
        Bosco.Utility.CommonMemberSet.NumberSetMember numberFormat = new Bosco.Utility.CommonMemberSet.NumberSetMember();

        #endregion

        #region Properties
        public bool _colVoucherNo
        {
            set { colVoucherNo.Visible = value; }
        }
        public bool _colDate
        {
            set { colVoucherDate.Visible = value; }
        }
        public bool _colTDSLedger
        {
            set { colTDSLedger.Visible = value; }
        }

        public bool _colNatureofPayments
        {
            set { colNatureofPayments.Visible = value; }
        }
        public bool _colPartyName
        {
            set { colPartyName.Visible = value; }
        }
        public bool _colAssessValue
        {
            set { colAssessValue.Visible = value; }
        }
        public bool _colBalance
        {
            set { colBalance.Visible = value; }
        }
        public bool _colTransMode
        {
            set { colTransMode1.Visible = value; }
        }

        public string _Caption
        {
            set { colBalance.Caption = value; }
        }

        public GridColumn _colColumnTransMode
        {
            get { return colTransMode1; }
        }
        public GridColumn _colColumnSelect
        {
            get { return colSelect; }
        }
        public GridColumn _colColumnBalance
        {
            get { return colBalance; }
        }
        public DataTable ucPendingTrans
        {
            get { return gcPendingTransaction.DataSource as DataTable; }
            set { gcPendingTransaction.DataSource = value; }
        }

        public int _ProjectId { get; set; }
        public string _ProjectName { get; set; }

        public int PartyPaymentId { get; set; }
        public int DeductyTypeId { get; set; }
        #endregion

        #region constructor
        public UcPendingTransaction()
        {
            InitializeComponent();
        }
        #endregion

        #region Events
        private void UcPendingTransaction_Load(object sender, EventArgs e)
        {
            //  FetchRecord();
        }

        private void gcPendingTransaction_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (previewEvent != null)
            {
                previewEvent(this, e);
            }
        }

        #endregion

        #region Methods
        private void FetchRecord()
        {
            DataTable dtPendingTDS = new DataTable();
            using (DeducteeTaxSystem deduteeTaxSystem = new DeducteeTaxSystem())
            {
                deduteeTaxSystem.PartyPaymentId = PartyPaymentId;
                deduteeTaxSystem.DeducteeTypeId = DeductyTypeId;
                resultArgs = deduteeTaxSystem.FetchPendingTransaction();
            }
            if (resultArgs != null && resultArgs.Success)
            {
                dtPendingTDS = resultArgs.DataSource.Table;
                gcPendingTransaction.DataSource = dtPendingTDS;
            }
        }

        public DataTable getSelectedTDSLedgers()
        {
            DataTable dtTDSLedger = new DataTable();
            try
            {
                if ((gcPendingTransaction.DataSource as DataTable) != null && (gcPendingTransaction.DataSource as DataTable).Rows.Count > 0)
                {
                    string BookingDetailId = GetSelectedRows(gvPendingTransaction);
                    BookingDetailId = !string.IsNullOrEmpty(BookingDetailId) ? BookingDetailId : "0";
                    if (!string.IsNullOrEmpty(BookingDetailId) && BookingDetailId != "0")
                    {
                        DataView dvTDSLedger = ((gcPendingTransaction.DataSource as DataTable).DefaultView);
                        dvTDSLedger.RowFilter = "BOOKING_DETAIL_ID IN(" + BookingDetailId + ")";
                        dtTDSLedger = dvTDSLedger.ToTable();
                        if (dtTDSLedger.Rows.Count <= 0)
                        {
                            FetchRecord();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
            return dtTDSLedger;
        }

        string GetSelectedRows(GridView view)
        {
            string retBookingDetailId = "";
            if (view.OptionsSelection.MultiSelectMode != GridMultiSelectMode.CellSelect)
            {
                foreach (int i in gvPendingTransaction.GetSelectedRows())
                {
                    DataRow row = gvPendingTransaction.GetDataRow(i);
                    retBookingDetailId += row["BOOKING_DETAIL_ID"] + ",";
                }
                retBookingDetailId = retBookingDetailId.TrimEnd(',');
            }
            return retBookingDetailId;
        }
        #endregion
    }
}
