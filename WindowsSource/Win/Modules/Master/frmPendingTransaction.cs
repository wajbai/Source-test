using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraLayout.Utils;
using Bosco.Model.UIModel;
using Bosco.Utility;
using Bosco.DAO.Schema;
using System.Xml;
using Bosco.DAO.Data;
using DevExpress.XtraEditors.Repository;

namespace ACPP.Modules.Master
{
    public partial class frmPendingTransaction : frmFinanceBaseAdd
    {
        #region Variables
        private TDSTransTypes tdsTypes;
        #endregion

        #region Properties
        public DataTable dtTDSSelectedLedgers { get; set; }
        public string ProjectName { get; set; }
        public DataTable dtTDSPending { get; set; }
        public int ProjectId { get; set; }
        public string BookingDate { get; set; }
        public DialogResult dialogResult { get; set; }
        #endregion

        #region Constructor
        public frmPendingTransaction()
        {
            InitializeComponent();
        }
        public frmPendingTransaction(TDSTransTypes tdsTransTypes)
            : this()
        {
            ConstructColumns(tdsTransTypes);
            tdsTypes = tdsTransTypes;
        }

        public frmPendingTransaction(int PartLedgerId, int DeductyTypeId)
            : this()
        {
            ucPendingTransaction.PartyPaymentId = PartLedgerId;
            ucPendingTransaction.DeductyTypeId = DeductyTypeId;
        }
        #endregion

        #region Events
        private void frmPendingTransaction_Load(object sender, EventArgs e)
        {
            if (dtTDSSelectedLedgers != null && dtTDSSelectedLedgers.Rows.Count > 0)
            {
                DataView dvTDSPayment = dtTDSSelectedLedgers.DefaultView;
                dvTDSPayment.RowFilter = "AMOUNT >0";
                dtTDSSelectedLedgers = dvTDSPayment.ToTable();
                ucPendingTransaction.ucPendingTrans = dtTDSSelectedLedgers;
                dvTDSPayment.RowFilter = "";
            }
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            try
            {
                dtTDSPending = ucPendingTransaction.getSelectedTDSLedgers();
                if (dtTDSSelectedLedgers != null && dtTDSSelectedLedgers.Rows.Count > 0)
                {
                    DialogResult = DialogResult.OK;
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Mapping.SELECT_ANYTDS_LEDGER));
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            dtTDSPending = null;
        }
        private void frmPendingTransaction_FormClosing(object sender, FormClosingEventArgs e)
        {
            dtTDSPending = dtTDSPending != null && dtTDSPending.Rows.Count > 0 ? dtTDSPending : null;
        }
        private void ucPendingTransaction1_previewEvent(object sender, EventArgs e)
        {
            switch (tdsTypes)
            {
                case TDSTransTypes.DeductTDS:
                    {
                        if (ucPendingTransaction.gvPendingTransaction.IsLastRow)
                        {
                            btnOk.Focus();
                        }
                        break;
                    }
                case TDSTransTypes.TDSParty:
                    {
                        if (ucPendingTransaction.gvPendingTransaction.IsLastRow)
                        {
                            btnOk.Focus();
                        }
                        break;
                    }
                case TDSTransTypes.TDSPayments:
                    {
                        if (ucPendingTransaction.gvPendingTransaction.IsLastRow)
                        {
                            btnOk.Focus();
                        }
                        break;
                    }
            }
        }
        #endregion

        #region Methods
        private void ConstructColumns(TDSTransTypes tdsTransTypes)
        {
            switch (tdsTransTypes)
            {
                case TDSTransTypes.DeductTDS:
                    {
                        this.Text = this.GetMessage(MessageCatalog.Master.Mapping.DEDUCT_TDS);
                        ucPendingTransaction._colTDSLedger = ucPendingTransaction._colPartyName = ucPendingTransaction._colTransMode = false;
                        ucPendingTransaction._Caption = this.GetMessage(MessageCatalog.Master.Mapping.TDS_BALANCE);
                        this.Width = 650;
                        break;
                    }
                case TDSTransTypes.TDSParty:
                    {
                        this.Text = this.GetMessage(MessageCatalog.Master.Mapping.PARTY_PAYMENTS);
                        ucPendingTransaction._colNatureofPayments = ucPendingTransaction._colAssessValue = ucPendingTransaction._colTDSLedger =
                        ucPendingTransaction._colPartyName = false;
                        ucPendingTransaction._Caption = this.GetMessage(MessageCatalog.Master.Mapping.BALANCE);
                        this.Width = 350;
                        break;
                    }
                case TDSTransTypes.TDSPayments:
                    {
                        this.Text = this.GetMessage(MessageCatalog.Master.Mapping.TDSPAYMENTS);
                        ucPendingTransaction._colAssessValue = false;
                        ucPendingTransaction._Caption = this.GetMessage(MessageCatalog.Master.Mapping.BALANCE);
                        break;
                    }
            }
        }

        private void FetchTDSPendingTrans()
        {
            DataTable dtTDSPendingTrans = new DataTable();
            using (Bosco.Model.TDS.TDSPaymentSystem TDSPayment = new Bosco.Model.TDS.TDSPaymentSystem())
            {
                TDSPayment.ProjectId = ProjectId;
                TDSPayment.BookingDate = this.UtilityMember.DateSet.ToDate(BookingDate, false);
                dtTDSPendingTrans = TDSPayment.FetchTDSPendingPayment();
                if (dtTDSPendingTrans != null && dtTDSPendingTrans.Rows.Count > 0)
                {
                    ucPendingTransaction.ucPendingTrans = dtTDSPendingTrans;
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Mapping.SELECT_ANYTDS_LEDGER));
                }
            }
        }
        #endregion
    }
}