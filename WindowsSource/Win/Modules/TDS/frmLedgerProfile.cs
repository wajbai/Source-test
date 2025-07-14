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

namespace ACPP.Modules.TDS
{
    public partial class frmLedgerProfile : frmFinanceBaseAdd
    {
        #region Decelaration
        ResultArgs resultArgs = null;
        public TDSLedgerTypes tdsLederTypes;
        public int GroupId = 0;
        //private const string NATURE_OF_PAYMENT = "Default Nature of Payments";
        //private const string DEDUCTEE_TYPE = "Default Deductee Types";
        private const string NATURE_OF_PAYMENT ="";
        private const string DEDUCTEE_TYPE ="";
        #endregion

        #region Constructor
        public frmLedgerProfile()
        {
            InitializeComponent();
            this.GetMessage(MessageCatalog.TDS.DutyTax.TDS_LEDGER_PROFILE_DEFAULT_NOP);
            this.GetMessage(MessageCatalog.TDS.DutyTax.TDS_LEDGER_PROFILE_DEFAULT_DEDUCTEE_TYPE);
        }

        public frmLedgerProfile(TDSLedgerTypes tdsLedger)
            : this()
        {
            tdsLederTypes = tdsLedger;
        }
        #endregion

        #region Properties
        public string LedgerType { get; set; }
        public string LedgerName { get; set; }
        public DataTable dtLedgerProfile { get; set; }
        public int LedgerProfileId { get; set; }
        public int CreditorsProfileId { get; set; }

        #endregion

        #region Events
        private void frmLedgerProfile_Load(object sender, EventArgs e)
        {
            this.CancelButton = ucLedgerProfile.btnClose;
            this.Height = tdsLederTypes.Equals(TDSLedgerTypes.SunderyCreditors) ? 330 : 110;
            this.Text = "TDS Info" + " " + LedgerName;
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);

            ucLedgerProfile.tdsLedgerTypes = tdsLederTypes.Equals(TDSLedgerTypes.SunderyCreditors) ? TDSLedgerTypes.SunderyCreditors : tdsLederTypes.Equals(TDSLedgerTypes.DutiesandTaxes) ? TDSLedgerTypes.DutiesandTaxes : TDSLedgerTypes.DirectExpense;
            ucLedgerProfile.MailGroups = tdsLederTypes.Equals(TDSLedgerTypes.SunderyCreditors) ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            ucLedgerProfile.BankDetails = ucLedgerProfile.BankDetailsEmptySpace = ucLedgerProfile.InformationMessage = tdsLederTypes.Equals(TDSLedgerTypes.SunderyCreditors) ? DevExpress.XtraLayout.Utils.LayoutVisibility.Always : DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            ucLedgerProfile.DefaultCaption = ucLedgerProfile.tdsLedgerTypes != TDSLedgerTypes.SunderyCreditors ? NATURE_OF_PAYMENT : DEDUCTEE_TYPE;
            ucLedgerProfile.AssignValues();
            ucLedgerProfile.LedgerId = LedgerProfileId;
            ucLedgerProfile.AssignLedgerProfile();
            this.StartPosition = FormStartPosition.CenterParent;
        }

        private void ucLedgerProfile_BankIndexChanged(object sender, EventArgs e)
        {
            if (tdsLederTypes.Equals(TDSLedgerTypes.SunderyCreditors))
            {
                this.Height = ucLedgerProfile.isBankDetails ? 430 : 330;
                ucLedgerProfile.BankTransType();
                this.StartPosition = FormStartPosition.CenterParent;
            }
        }

        private void ucLedgerProfile_BankTransEditChanged(object sender, EventArgs e)
        {

        }

        private void ucLedgerProfile_SaveLedgerProfile(object sender, EventArgs e)
        {
            dtLedgerProfile = ucLedgerProfile.dtLedgerDetails;
            CreditorsProfileId = ucLedgerProfile.CreditorsProfileId;
            LedgerProfileId = ucLedgerProfile.LedgerId;
            this.Close();
        }

        private void ucLedgerProfile_CloseLedgerProfile(object sender, EventArgs e)
        {
            this.Close();
        }
        #endregion

        #region Methods

        #endregion

    }
}