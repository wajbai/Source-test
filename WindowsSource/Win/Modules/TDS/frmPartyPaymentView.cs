using System;
using System.Data;
using System.Windows.Forms;
using Bosco.Utility;
using Bosco.Model.TDS;
using Bosco.Model.UIModel;

namespace ACPP.Modules.TDS
{
    public partial class frmPartyPaymentView : frmFinanceBase
    {
        #region Decelaration
        TDSPayTypes tdsPaymentTypes;
        ResultArgs resultArgs = null;
        private int RowIndex = 0;
        #endregion

        #region Constructor
        public frmPartyPaymentView()
        {
            InitializeComponent();
        }

        public frmPartyPaymentView(TDSPayTypes tdsPayTypes)
            : this()
        {
            this.tdsPaymentTypes = tdsPayTypes;
        }
        #endregion

        #region Properties
        private int VoucherId
        {
            get
            {
                RowIndex = gvPartyPayment.FocusedRowHandle;
                return gvPartyPayment.GetFocusedRowCellValue(colTDSVoucherId) != null ? this.UtilityMember.NumberSet.ToInteger(gvPartyPayment.GetFocusedRowCellValue(colTDSVoucherId).ToString()) : 0;
            }
        }

        private int BookingId
        {
            get { return gvPartyPayment.GetFocusedRowCellValue(colTDSBookingId) != null ? this.UtilityMember.NumberSet.ToInteger(gvPartyPayment.GetFocusedRowCellValue(colTDSBookingId).ToString()) : 0; }
        }

        private int BookingDetailId
        {
            get { return gvPartyPayment.GetFocusedRowCellValue(colBookingDetailId) != null ? this.UtilityMember.NumberSet.ToInteger(gvPartyPayment.GetFocusedRowCellValue(colBookingDetailId).ToString()) : 0; }
        }

        private int TDSPaymentId
        {
            get { return gvPartyPayment.GetFocusedRowCellValue(colTDSPaymentId) != null ? this.UtilityMember.NumberSet.ToInteger(gvPartyPayment.GetFocusedRowCellValue(colTDSPaymentId).ToString()) : 0; }
        }

        private int PartyLedgerId
        {
            get { return gvPartyPayment.GetFocusedRowCellValue("PARTY_LEDGER_ID") != null ? this.UtilityMember.NumberSet.ToInteger(gvPartyPayment.GetFocusedRowCellValue("PARTY_LEDGER_ID").ToString()) : 0; }
        }

        private int TDSPaymentDetailId
        {
            get { return gvPartyPayment.GetFocusedRowCellValue(colTDSPaymentDetailId) != null ? this.UtilityMember.NumberSet.ToInteger(gvPartyPayment.GetFocusedRowCellValue(colTDSPaymentDetailId).ToString()) : 0; }
        }

        private int DeductionDetailId
        {
            get { return gvPartyPayment.GetFocusedRowCellValue(colTDSPaymentDetailId) != null ? this.UtilityMember.NumberSet.ToInteger(gvPartyPayment.GetFocusedRowCellValue(colTDSPaymentDetailId).ToString()) : 0; }
        }

        private string VoucherNo
        {
            get { return gvPartyPayment.GetFocusedRowCellValue(colVoucherNo) != null ? gvPartyPayment.GetFocusedRowCellValue(colVoucherNo).ToString() : string.Empty; }
        }

        private string BookingDate
        {
            get { return gvPartyPayment.GetFocusedRowCellValue(colVoucherDate) != null ? gvPartyPayment.GetFocusedRowCellValue(colVoucherDate).ToString() : string.Empty; }
        }

        private int CashBankId
        {
            get { return gvPartyPayment.GetFocusedRowCellValue(colCashBankId) != null ? this.UtilityMember.NumberSet.ToInteger(gvPartyPayment.GetFocusedRowCellValue(colCashBankId).ToString()) : 0; }
        }

        private int ProjectId
        {
            get { return gvPartyPayment.GetFocusedRowCellValue(colProjectId) != null ? this.UtilityMember.NumberSet.ToInteger(gvPartyPayment.GetFocusedRowCellValue(colProjectId).ToString()) : 0; }
        }

        private string Narration
        {
            get { return gvPartyPayment.GetFocusedRowCellValue(colNarration) != null ? gvPartyPayment.GetFocusedRowCellValue(colNarration).ToString() : string.Empty; }
        }

        private int glkpProjectId
        {
            get { return glkpProject.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0; }
        }
        #endregion

        #region Events
        private void frmPartyPaymentView_Load(object sender, EventArgs e)
        {
            this.Text = this.tdsPaymentTypes.Equals(TDSPayTypes.PartyPayment) ? this.GetMessage(MessageCatalog.TDS.TDSPayment.TDS_PARTY_PAYMENT_CAPTION) : this.GetMessage(MessageCatalog.TDS.TDSPayment.TDS_PAYMENT_CAPTION);
        }

        private void frmPartyPaymentView_Activated(object sender, EventArgs e)
        {
            LoadDefaults();
            FetchTDSPaymentTrans();
        }
        private void ucToolBar_AddClicked(object sender, EventArgs e)
        {
            ShowForm((int)AddNewRow.NewRow, (int)AddNewRow.NewRow);
        }

        private void ucToolBar_EditClicked(object sender, EventArgs e)
        {
            if (this.tdsPaymentTypes.Equals(TDSPayTypes.PartyPayment))
            {
                ShowForm(PartyLedgerId, ProjectId);
            }
            else
            {
                ShowForm(TDSPaymentId, ProjectId);
            }
        }

        private void gcPartyPayment_DoubleClick(object sender, EventArgs e)
        {
            if (this.tdsPaymentTypes.Equals(TDSPayTypes.PartyPayment))
            {
                ShowForm(PartyLedgerId, ProjectId);
            }
            else
            {
                ShowForm(TDSPaymentId, ProjectId);
            }
        }

        private void ucToolBar_DeleteClicked(object sender, EventArgs e)
        {
            resultArgs = DeleteTDSPayments();
            if (resultArgs != null && resultArgs.Success && resultArgs.RowsAffected > 0)
            {
                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                FetchTDSPaymentTrans();
            }
        }

        private void ucToolBar_PrintClicked(object sender, EventArgs e)
        {
            if (tdsPaymentTypes.Equals(TDSPayTypes.TDSPayment))
            {
                this.PrintGridViewDetails(gcPartyPayment, this.GetMessage(MessageCatalog.TDS.TDSPayment.TDS_PAYMENT_PRINT), PrintType.DS, gvPartyPayment, true);
            }
        }

        private void ucToolBar_RefreshClicked(object sender, EventArgs e)
        {
            FetchTDSPaymentTrans();
        }

        private void ucToolBar_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        private void gvPartyPayment_RowCountChanged(object sender, EventArgs e)
        {
            lblRecordCount.Text = "#" + " " + gvPartyPayment.RowCount.ToString();
        }

        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            FetchTDSPaymentTrans();
            gvPartyPayment.FocusedRowHandle = RowIndex;
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvPartyPayment.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvPartyPayment, colVoucherDate);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            FetchTDSPaymentTrans();
        }
        #endregion

        #region Methods
        private void ShowForm(int TDSPayId, int TDSProjectId)
        {
            try
            {
                if (this.tdsPaymentTypes.Equals(TDSPayTypes.PartyPayment))
                {
                    frmPaymentsParty frmPayments = new frmPaymentsParty(TDSPayId, TDSProjectId);
                    frmPayments.VoucherId = VoucherId;
                    frmPayments.PartyPaymentId = TDSPaymentId;
                    frmPayments.VoucherNo = VoucherNo;
                    frmPayments.PartyDate = BookingDate;
                    frmPayments.CashBankLedgerId = CashBankId;
                    frmPayments.UpdateHeld += new EventHandler(OnUpdateHeld);
                    frmPayments.Narration = Narration;
                    frmPayments.ShowDialog();
                }
                else
                {
                    frmPaymentsTDS frmTDSPayTrans = new frmPaymentsTDS(TDSPayId, 0, TDSProjectId, DateTime.Today, "", 0);
                    frmTDSPayTrans.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        void frmTDSPayTrans_UpdateHeld(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        private ResultArgs DeleteTDSPayments()
        {
            try
            {
                if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Information).Equals(DialogResult.Yes))
                {
                    if (this.tdsPaymentTypes.Equals(TDSPayTypes.PartyPayment))
                    {
                        using (PartyPaymentSystem PartySystem = new PartyPaymentSystem())
                        {
                            PartySystem.VoucherId = VoucherId;
                            PartySystem.IsPhysicalDelete = false;
                            PartySystem.PartyPaymentId = UtilityMember.NumberSet.ToInteger(gvPartyPayment.GetFocusedRowCellValue("TDS_PAYMENT_ID").ToString());
                            resultArgs = PartySystem.DeletePartyPaymentTrans();
                        }
                    }
                    else
                    {
                        using (TDSPaymentSystem TDSPaySystem = new TDSPaymentSystem())
                        {
                            TDSPaySystem.TDSPaymentId = TDSPaymentId;
                            resultArgs = TDSPaySystem.DeleteTDS();
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
            return resultArgs;
        }

        private void FetchTDSPaymentTrans()
        {
            if (tdsPaymentTypes.Equals(TDSPayTypes.PartyPayment))
            {
                using (PartyPaymentSystem TDSPartyPayment = new PartyPaymentSystem())
                {
                    TDSPartyPayment.ProjectId = glkpProjectId;
                    TDSPartyPayment.DateFrom = deDateFrom.DateTime;
                    TDSPartyPayment.DateTo = deDateTo.DateTime;
                    DataSet dsTDSPaymentTrans = TDSPartyPayment.FetchPartyPayment();
                    if (dsTDSPaymentTrans != null && dsTDSPaymentTrans.Tables.Count > 1)
                    {
                        gcPartyPayment.DataSource = dsTDSPaymentTrans;
                        gcPartyPayment.DataMember = "Master";
                    }
                    else
                    {
                        gcPartyPayment.DataSource = null;
                    }
                }
            }
            else
            {
                using (TDSPaymentSystem TDSPaySystem = new TDSPaymentSystem())
                {
                    TDSPaySystem.ProjectId = glkpProjectId;
                    TDSPaySystem.DateFrom = deDateFrom.DateTime;
                    TDSPaySystem.DateTo = deDateTo.DateTime;
                    DataSet dsTDSTrans = TDSPaySystem.FetchTDSPaymentTrans();
                    if (dsTDSTrans != null && dsTDSTrans.Tables.Count > 1)
                    {
                        gcPartyPayment.DataSource = dsTDSTrans;
                        gcPartyPayment.DataMember = "Master";
                    }
                    else
                    {
                        gcPartyPayment.DataSource = null;
                    }
                }
            }
        }

        private void LoadDefaults()
        {
            deDateFrom.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deDateFrom.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            deDateFrom.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deDateTo.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deDateTo.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            deDateTo.DateTime = deDateFrom.DateTime.AddMonths(1).AddDays(-1);
            LoadProject();
        }

        private void LoadProject()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    resultArgs = mappingSystem.FetchProjectsLookup();
                    glkpProject.Properties.DataSource = null;
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpProject, resultArgs.DataSource.Table, mappingSystem.AppSchema.Project.PROJECTColumn.ColumnName, mappingSystem.AppSchema.Project.PROJECT_IDColumn.ColumnName);
                        glkpProject.EditValue = glkpProject.Properties.GetKeyValue(0);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message + Environment.NewLine + Ex.Source);
            }
            finally { }
        }
        #endregion

    }
}