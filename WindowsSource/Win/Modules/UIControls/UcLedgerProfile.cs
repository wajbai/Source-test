using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraLayout.Utils;

using Bosco.Model.TDS;
using Bosco.Utility;

namespace ACPP.Modules.UIControls
{
    public partial class UcLedgerProfile : DevExpress.XtraEditors.XtraUserControl
    {
        #region Variable Decelaration
        Bosco.Utility.CommonMemberSet.NumberSetMember numberSet = new Bosco.Utility.CommonMemberSet.NumberSetMember();
        Bosco.Utility.CommonMemberSet.ComboSetMember comboSetMember = new Bosco.Utility.CommonMemberSet.ComboSetMember();
        public TDSLedgerTypes tdsLedgerTypes;
        ResultArgs resultArgs = null;
        private const string NATURE_PAY_ID = "NATURE_PAY_ID";
        #endregion

        #region Properties
        public int LedgerId { get; set; }
        public int NatureofPaymentsId { get; set; }
        public int BankTransId { get; set; }
        public int CreditorsProfileId { get; set; }
        public bool isBankDetails { get; set; }
        public string NickName { get; set; }
        public string FavouringName { get; set; }
        public string BankName { get; set; }
        public string BankAcNo { get; set; }
        public string IFSNo { get; set; }
        public string MailName { get; set; }
        public string MailAddress { get; set; }
        public string PANNo { get; set; }
        public string State { get; set; }
        public string PinCode { get; set; }
        public string SalesNo { get; set; }
        public string CSTNo { get; set; }
        public DataTable dtLedgerDetails { get; set; }

        public LayoutVisibility MailGroups
        {
            set { lcgMailingAddress.Visibility = value; }
        }
        public LayoutVisibility BankDetails
        {
            set { lcgBankDetails.Visibility = value; }
        }
        public LayoutVisibility BankDetailsEmptySpace
        {
            set { emptySpaceItem2.Visibility = value; }
        }

        public LayoutVisibility InformationMessage
        {
            set { lblInformatoin.Visibility = value; }
        }

        //public 

        public string DefaultCaption
        {
            set { lcgNOP.Text = value; }
        }
        #endregion

        #region Events
        public event EventHandler SaveLedgerProfile;
        public event EventHandler CloseLedgerProfile;
        public event EventHandler BankTransEditChanged;
        public event EventHandler BankIndexChanged;
        #endregion

        #region Constructors
        public UcLedgerProfile()
        {
            InitializeComponent();
        }
        #endregion

        #region Form Events
        private void cboBankDetails_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (BankIndexChanged != null)
            {
                lcgBankInfo.Visibility = cboBankDetails.SelectedIndex.Equals(0) ? DevExpress.XtraLayout.Utils.LayoutVisibility.Never : DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                isBankDetails = cboBankDetails.SelectedIndex.Equals(0) ? false : true;
                if (!isBankDetails)
                {
                    txtNickName.Text = txtFavouringName.Text = txtBankName.Text = txtAcNo.Text = txtIFSCode.Text = glkpTransType.Text = string.Empty;
                }
                BankIndexChanged(this, e);
            }
        }

        private void glkpTransType_EditValueChanged(object sender, EventArgs e)
        {
            if (BankTransEditChanged != null)
            {
                BankTransEditChanged(this, e);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            if (CloseLedgerProfile != null)
            {
                CloseLedgerProfile(this, e);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (SaveLedgerProfile != null)
            {
                dtLedgerDetails = ConstructLedgerProfileDetails();
                using (LedgerProfileSystem ledgerProfileSystem = new LedgerProfileSystem())
                {
                    dtLedgerDetails.Rows.Add
                        (glkNOP.EditValue != null ? numberSet.ToInteger(glkNOP.EditValue.ToString()) : 0,
                        LedgerId, 
                        cboBankDetails.SelectedIndex.Equals(0) ? 0 : 1, 
                        !string.IsNullOrEmpty(txtNickName.Text) ? txtNickName.Text : string.Empty, 
                        !string.IsNullOrEmpty(txtFavouringName.Text) ? txtFavouringName.Text : string.Empty, 
                        glkpTransType.EditValue != null && !string.IsNullOrEmpty(glkpTransType.Text) ? numberSet.ToInteger(glkpTransType.EditValue.ToString()) : 0,
                        !string.IsNullOrEmpty(txtAcNo.Text) ? txtAcNo.Text : string.Empty, 
                        !string.IsNullOrEmpty(txtBankName.Text) ? txtBankName.Text : string.Empty,
                        !string.IsNullOrEmpty(txtIFSCode.Text) ? txtIFSCode.Text : string.Empty,
                        !string.IsNullOrEmpty(txtName.Text) ? txtName.Text : string.Empty, 
                        !string.IsNullOrEmpty(mtxtAddress.Text) ? mtxtAddress.Text : string.Empty, 
                        !string.IsNullOrEmpty(txtState.Text) ? txtState.Text : string.Empty,
                        !string.IsNullOrEmpty(txtPinCode.Text) ? txtPinCode.Text : string.Empty, 
                        !string.IsNullOrEmpty(txtPANNo.Text) ? txtPANNo.Text : string.Empty, 
                        !string.IsNullOrEmpty(txtPANName.Text) ? txtPANName.Text : string.Empty, 
                        !string.IsNullOrEmpty(txtSalesTaxNo.Text) ? txtSalesTaxNo.Text : string.Empty,
                        !string.IsNullOrEmpty(txtCSTNo.Text) ? txtCSTNo.Text : string.Empty, 
                        string.Empty,
                        string.Empty,
                        string.Empty);
                }
                SaveLedgerProfile(this, e);
            }
        }
        #endregion

        #region Methods
        private void FetchDefaultNatureofPayments()
        {
            using (DeducteeTaxSystem deducteeSystem = new DeducteeTaxSystem())
            {
                comboSetMember.BindGridLookUpCombo(glkNOP, deducteeSystem.NOP(), deducteeSystem.AppSchema.DeducteeTypes.NAMEColumn.ColumnName, NATURE_PAY_ID);
                glkNOP.EditValue = glkNOP.Properties.GetKeyValue(0);
            }
        }

        public void BankTransType()
        {
            DataTable dtBankTrans = new DataTable();
            dtBankTrans.Columns.AddRange(new DataColumn[] { new DataColumn("ID", typeof(int)), new DataColumn("BANK_TRANS_TYPE", typeof(string)) });
            dtBankTrans.Rows.Add(1, "Cheque");
            dtBankTrans.Rows.Add(2, "Electronic Cheque");
            dtBankTrans.Rows.Add(3, "Electronic DD/PO");
            dtBankTrans.Rows.Add(4, "Inter Bank Transfer");
            dtBankTrans.Rows.Add(5, "Same Bank Transfer");
            dtBankTrans.Rows.Add(6, "Others");
            comboSetMember.BindGridLookUpCombo(glkpTransType, dtBankTrans, "BANK_TRANS_TYPE", "ID");
            glkpTransType.EditValue = glkpTransType.Properties.GetKeyValue(0);
        }

        private DataTable ConstructLedgerProfileDetails()
        {
            DataTable dtLedgerProfile = new DataTable();
            using (Bosco.Model.TDS.LedgerProfileSystem ledgerProfileSystem = new LedgerProfileSystem())
            {
                dtLedgerProfile.Columns.AddRange(new DataColumn[]
            { new DataColumn(ledgerProfileSystem.AppSchema.LedgerProfileData.DEDUTEE_TYPE_IDColumn.ColumnName, typeof(Int32)), 
              new DataColumn(ledgerProfileSystem.AppSchema.LedgerProfileData.LEDGER_IDColumn.ColumnName, typeof(Int32)),
              new DataColumn(ledgerProfileSystem.AppSchema.LedgerProfileData.IS_BANK_DETAILSColumn.ColumnName, typeof(Int32)),
              new DataColumn(ledgerProfileSystem.AppSchema.LedgerProfileData.NICK_NAMEColumn.ColumnName, typeof(string)),
              new DataColumn(ledgerProfileSystem.AppSchema.LedgerProfileData.FAVOURING_NAMEColumn.ColumnName, typeof(string)),
              new DataColumn(ledgerProfileSystem.AppSchema.LedgerProfileData.TRANSACTION_TYPEColumn.ColumnName, typeof(Int32)),
              new DataColumn(ledgerProfileSystem.AppSchema.LedgerProfileData.ACCOUNT_NUMBERColumn.ColumnName, typeof(string)),
              new DataColumn(ledgerProfileSystem.AppSchema.LedgerProfileData.BANK_NAMEColumn.ColumnName, typeof(string)),
              new DataColumn(ledgerProfileSystem.AppSchema.LedgerProfileData.IFS_CODEColumn.ColumnName, typeof(string)),
              new DataColumn(ledgerProfileSystem.AppSchema.LedgerProfileData.NAMEColumn.ColumnName, typeof(string)),
              new DataColumn(ledgerProfileSystem.AppSchema.LedgerProfileData.ADDRESSColumn.ColumnName, typeof(string)),
              new DataColumn(ledgerProfileSystem.AppSchema.LedgerProfileData.STATEColumn.ColumnName, typeof(string)),
              new DataColumn(ledgerProfileSystem.AppSchema.LedgerProfileData.PIN_CODEColumn.ColumnName, typeof(string)),
              new DataColumn(ledgerProfileSystem.AppSchema.LedgerProfileData.PAN_NUMBERColumn.ColumnName, typeof(string)),
              new DataColumn(ledgerProfileSystem.AppSchema.LedgerProfileData.PAN_IT_HOLDER_NAMEColumn.ColumnName, typeof(string)),
              new DataColumn(ledgerProfileSystem.AppSchema.LedgerProfileData.SALES_TAX_NOColumn.ColumnName, typeof(string)),
              new DataColumn(ledgerProfileSystem.AppSchema.LedgerProfileData.CST_NUMBERColumn.ColumnName, typeof(string)),
              new DataColumn(ledgerProfileSystem.AppSchema.LedgerProfileData.CONTACT_PERSONColumn.ColumnName, typeof(string)),
              new DataColumn(ledgerProfileSystem.AppSchema.LedgerProfileData.CONTACT_NUMBERColumn.ColumnName, typeof(string)),
              new DataColumn(ledgerProfileSystem.AppSchema.LedgerProfileData.EMAILColumn.ColumnName, typeof(string)),
            });
            }
            return dtLedgerProfile;
        }

        private void FetchDeducteeTypes()
        {
            using (DeducteeTypeSystem deducteeSystem = new DeducteeTypeSystem())
            {
                resultArgs = deducteeSystem.FetchDeducteeTypes();
                if (resultArgs.DataSource.Table.Columns[deducteeSystem.AppSchema.DeducteeTypes.DEDUCTEE_TYPE_IDColumn.ColumnName].ColumnName.Equals(deducteeSystem.AppSchema.DeducteeTypes.DEDUCTEE_TYPE_IDColumn.ColumnName) && resultArgs.DataSource.Table.Columns[deducteeSystem.AppSchema.DeducteeTypes.NAMEColumn.ColumnName].ColumnName.Equals(deducteeSystem.AppSchema.DeducteeTypes.NAMEColumn.ColumnName))
                {
                    resultArgs.DataSource.Table.Columns[deducteeSystem.AppSchema.DeducteeTypes.DEDUCTEE_TYPE_IDColumn.ColumnName].ColumnName = "NATURE_PAY_ID";
                }
                comboSetMember.BindGridLookUpCombo(glkNOP, resultArgs.DataSource.Table, deducteeSystem.AppSchema.DeducteeTypes.NAMEColumn.ColumnName, "NATURE_PAY_ID");
                glkNOP.EditValue = glkNOP.Properties.GetKeyValue(0);
            }
        }

        private void FillLedgerProfileDetails()
        {
            try
            {
                if (LedgerId > 0)
                {
                    using (LedgerProfileSystem ledgerProfileSystem = new LedgerProfileSystem(LedgerId))
                    {
                        glkNOP.EditValue = ledgerProfileSystem.NatureofPaymentsId;
                        txtNickName.Text = ledgerProfileSystem.NickName;
                        txtFavouringName.Text = ledgerProfileSystem.FavouringName;
                        txtBankName.Text = ledgerProfileSystem.BankName;
                        txtAcNo.Text = ledgerProfileSystem.BankAcNo;
                        glkpTransType.EditValue = ledgerProfileSystem.BankTransType;
                        txtIFSCode.Text = ledgerProfileSystem.IFSNo;
                        txtName.Text = ledgerProfileSystem.MailName;
                        mtxtAddress.Text = ledgerProfileSystem.MailAddress;
                      //  txtState.Text = ledgerProfileSystem.State;
                        txtPANNo.Text = ledgerProfileSystem.PANNo;
                        txtPANName.Text = ledgerProfileSystem.PANName;
                        txtPinCode.Text = ledgerProfileSystem.PinCode;
                        txtSalesTaxNo.Text = ledgerProfileSystem.SalesNo;
                        txtCSTNo.Text = ledgerProfileSystem.CSTNo;
                    }
                }
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message + Environment.NewLine + ex.Source, "AcME ERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { }
        }
        #endregion

        private void UcLedgerProfile_Load(object sender, EventArgs e)
        {
        }

        private void txtPANNo_Validating(object sender, CancelEventArgs e)
        {

        }

        public void AssignValues()
        {
            if (tdsLedgerTypes.Equals(TDSLedgerTypes.SunderyCreditors))
            {
                FetchDeducteeTypes();
            }
            else
            {
                FetchDefaultNatureofPayments();
            }
        }

        public void AssignLedgerProfile()
        {
            try
            {
                if (LedgerId > 0)
                {
                    using (Bosco.Model.TDS.LedgerProfileSystem ledgerProfileSystem = new LedgerProfileSystem(LedgerId))
                    {
                        if (tdsLedgerTypes.Equals(TDSLedgerTypes.SunderyCreditors))
                        {
                            glkNOP.EditValue = ledgerProfileSystem.NatureofPaymentsId;
                            LedgerId = ledgerProfileSystem.LedgerProfileId;
                            CreditorsProfileId = ledgerProfileSystem.CreditorsProfileId;
                            isBankDetails = ledgerProfileSystem.isBankDetails;
                            cboBankDetails.SelectedIndex = isBankDetails ? 1 : 0;
                            txtNickName.Text = ledgerProfileSystem.NickName;
                            txtFavouringName.Text = ledgerProfileSystem.FavouringName;
                            txtAcNo.Text = ledgerProfileSystem.BankAcNo;
                            txtBankName.Text = ledgerProfileSystem.BankName;
                            txtIFSCode.Text = ledgerProfileSystem.IFSNo;
                            glkpTransType.EditValue = ledgerProfileSystem.BankTransType;
                            txtName.Text = ledgerProfileSystem.MailName;
                            mtxtAddress.Text = ledgerProfileSystem.MailAddress;
                          //  txtState.Text = ledgerProfileSystem.State;
                            txtPinCode.Text = ledgerProfileSystem.PinCode;
                            txtPANNo.Text = ledgerProfileSystem.PANNo;
                            txtPANName.Text = ledgerProfileSystem.PANName;
                            txtSalesTaxNo.Text = ledgerProfileSystem.SalesNo;
                            txtCSTNo.Text = ledgerProfileSystem.CSTNo;
                            if (cboBankDetails.SelectedIndex > 0)
                            {
                                lcgBankDetails.Visibility = LayoutVisibility.Always;
                            }
                        }
                        else
                        {
                            glkNOP.EditValue = ledgerProfileSystem.NatureofPaymentsId;
                            LedgerId = ledgerProfileSystem.LedgerProfileId;
                            CreditorsProfileId = ledgerProfileSystem.CreditorsProfileId;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                DevExpress.XtraEditors.XtraMessageBox.Show(ex.Message + Environment.NewLine + ex.Source, "AcME ERP", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally { }
        }
    }
}
