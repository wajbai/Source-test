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
using Bosco.Model.Dsync;
using Bosco.Model.Transaction;
//using DevExpress.CodeRush.StructuralParser;


namespace ACPP.Modules.Master
{
    public partial class frmLedgerBankAccountAdd : frmFinanceBaseAdd
    {
        #region Variables
        ResultArgs resultArgs = null;
        public event EventHandler UpdateHeld;
        private int LedgerAccId = 0;
        DialogResult mappingDialogResult = DialogResult.Cancel;
        ledgerSubType ledType;
        int ProjectId = 0;
        #endregion

        #region Properties

        private int ledgerInsertId = 0;
        private int LedgerInsertId
        {
            set { ledgerInsertId = value; }
            get { return ledgerInsertId; }
        }
        private int NatureId { get; set; }
        private string NatureName { get; set; }
        private int GroupId { get; set; }
        private DataTable dtLedgerGroups { get; set; }
        private DataTable dtLedgerProfile { get; set; }
        private TDSLedgerTypes tdsLedgersTypes { get; set; }
        private int CreditorsProfileId { get; set; }

        #endregion

        #region Constructor
        public frmLedgerBankAccountAdd()
        {
            InitializeComponent();
        }

        public frmLedgerBankAccountAdd(int LedgerId, ledgerSubType ledgerType, int ProjetId = 0)
            : this()
        {
            UcMappingLedger.ProjectId = ProjetId;
            UcMappingLedger.Id = LedgerAccId = LedgerId;
            ledType = ledgerType;
            ProjectId = ProjetId;
            UcMappingLedger.FormType = ledType != ledgerSubType.FD ? MapForm.Ledger : MapForm.FDLedger;
            lcInvestmentType.Visibility = (ledType == ledgerSubType.FD ? LayoutVisibility.Always : LayoutVisibility.Never);
            
            //On 22/08/22024, To Show currency details
            lcgCurrencyDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (this.AppSetting.AllowMultiCurrency == 1 && ledType == ledgerSubType.FD)
            {
                lcgCurrencyDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                lcOpeningAvgExchangeRateCaption.Text = (lcOpeningAvgExchangeRateCaption.Text + " " + AppSetting.FirstFYDateFrom.ToShortDateString()) + " : ";
                LoadCurrency();
            }

            LedgerAccessFlag(LedgerId);
            AssignLedgerDetails();
        }
        #endregion

        #region Page Events
        private void frmLedgerBankAccountAdd_Load(object sender, EventArgs e)
        {
            SetTitle();
                       

            if (LedgerAccId == 0) { loadLedgerType(true); } else { loadLedgerType(false); }
            if (LedgerAccId == 0) { LoadLedgerGroup(true); } else { LoadLedgerGroup(false); }
            LoadLedgerCodes();
            // if (LedgerAccId == 0) { FetchProjects(); }
            UcMappingLedger.SelectFixedWidth = true;
            if (this.LoginUser.TDSEnabled.Equals("1"))
            {
                // emptySpaceItem3.Visibility = layoutControlItem1.Visibility = LayoutVisibility.Always;
                // layoutControlItem4.Visibility = emptySpaceItem6.Visibility = LayoutVisibility.Never;

            }
            lblCode.Text = "Code";
            // btnNew.Visible=false;

            //On 17/10/2023, Map HO ledger---------
            ShowMapHOLedger();
            //-------------------------------------

            //On 07/05/2024, To load investment type
            LoadInvestmentType();

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (ValidateLedgerDetails())
            {
                try
                {
                    using (LedgerSystem ledgerSystem = new LedgerSystem())
                    {
                        ledgerSystem.LedgerCode = txtCode.Text.Trim().ToUpper();
                        ledgerSystem.LedgerName = txtName.Text.Trim();
                        ledgerSystem.GroupId = this.UtilityMember.NumberSet.ToInteger(lkpGroup.EditValue.ToString());
                        ledgerSystem.IsCostCentre = (chkIncludeCostCenter.Checked) ? (int)YesNo.Yes : (int)YesNo.No;
                        ledgerSystem.IsBankInterestLedger = (chkBankInterestLedger.Checked) ? (int)YesNo.Yes : (int)YesNo.No;
                        ledgerSystem.LedgerType = GetLedgerType(ledgerSystem.GroupId);// CA-Cash Leger, GN -General Ledger
                        ledgerSystem.LedgerSubType = ledType == ledgerSubType.FD ? LedgerTypes.FD.ToString() : GetSubType(ledgerSystem.GroupId);
                        ledgerSystem.LedgerId = (LedgerAccId == (int)AddNewRow.NewRow) ? (int)AddNewRow.NewRow : LedgerAccId;
                        ledgerSystem.LedgerNotes = mtxtNotes.Text.Trim();
                        ledgerSystem.SortId = ledType == ledgerSubType.FD ? (int)LedgerSortOrder.FD : GetSortOrder(ledgerSystem.GroupId);// (int)LedgerSortOrder.Bank;
                        ledgerSystem.FDType = ledType == ledgerSubType.FD ? ledgerSubType.FD.ToString() : ledgerSubType.GN.ToString();
                        ledgerSystem.ledgerTypes = ledType;
                        ledgerSystem.IsTDSLedger = chkTDSLedgers.Checked ? (int)YesNo.Yes : (int)YesNo.No;
                        ledgerSystem.CreditorsProfileId = CreditorsProfileId;

                        //On 22/08/2024, To get Currency details
                        ledgerSystem.LedgerCurrencyCountryId = 0;
                        ledgerSystem.LedgerCurrencyOPExchangeRate = 1;
                        if (this.AppSetting.AllowMultiCurrency == 1 && ledType == ledgerSubType.FD)
                        {
                            ledgerSystem.LedgerCurrencyCountryId = glkpCurrencyCountry.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString()) : 0;
                            ledgerSystem.LedgerCurrencyOPExchangeRate = UtilityMember.NumberSet.ToDouble(txtOPExchangeRate.Text);
                        }

                        if (!string.IsNullOrEmpty(this.AppSetting.BookBeginFrom))
                        {
                            ledgerSystem.dtMappingLedgers = UcMappingLedger.GetMappingDetails;
                            ledgerSystem.MapLedgerId = ledgerSystem.LedgerId;
                            ledgerSystem.FDLeger = false;
                            ledgerSystem.dtLedgerProile = chkTDSLedgers.Checked ? dtLedgerProfile : null;
                            ledgerSystem.tdsLedgerTypes = tdsLedgersTypes;

                            //On 02/07/2019, Map Ledger Office Ledger -------------------------------------------------------------------------------------------------------
                            ledgerSystem.HeadofficeLedgerId = 0;
                            if (AppSetting.EnablePortal == 1)
                            {
                                if (ledType==ledgerSubType.FD)
                                {
                                    ledgerSystem.HeadofficeLedgerId = glkpHOLedger.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpHOLedger.EditValue.ToString()) : 0;
                                }
                            }
                            //------------------------------------------------------------------------------------------------------------------------------------------------

                            //FD Investment Type Id ---------------------------------------------------------------------------------------------------------------------------
                            ledgerSystem.FDInvestmentTypeId = 0;
                            if (ledType == ledgerSubType.FD)
                            {
                                ledgerSystem.FDInvestmentTypeId = glkpInvestmentType.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpInvestmentType.EditValue.ToString()) : 0;
                            }
                            //------------------------------------------------------------------------------------------------------------------------------------------------


                            //On 19/10/2021 ------------------ Ledger Closed Date
                            if (!string.IsNullOrEmpty(deLedgerDateClosed.Text.Trim()))
                            {
                                ledgerSystem.LedgerDateClosed = deLedgerDateClosed.DateTime;
                            }
                            //--------------------------------

                            resultArgs = ledgerSystem.SaveLedger();
                            if (resultArgs != null && resultArgs.Success)
                            {
                                //if (mappingDialogResult.Equals(DialogResult.Cancel))
                                //    this.DialogResult = DialogResult.OK;
                                //mappingDialogResult = DialogResult.OK;
                                this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                                LedgerInsertId = this.UtilityMember.NumberSet.ToInteger(resultArgs.RowUniqueId.ToString());
                                txtCode.Focus();
                                if (LedgerAccId == 0)
                                {
                                    UcMappingLedger.GridClear = true;
                                    ClearControls();
                                    LoadLedgerCodes();
                                    LoadLedgerGroup();
                                }
                                if (UpdateHeld != null)
                                    UpdateHeld(this, e);
                                // UcMappingLedger.ProcessGridKey = null;
                            }
                            else
                                LoadLedgerCodes();
                        }
                        else
                            ShowMessageBox(GetMessage(MessageCatalog.Master.Mapping.BOOK_BEGINNING_DATE_EMPTY));
                    }
                }
                catch (Exception Ex)
                {
                    MessageRender.ShowMessage(Ex.Message + System.Environment.NewLine + Ex.Source);
                }
                finally
                {
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.DialogResult = mappingDialogResult;
            this.Close();
        }

        private void btnLinktoLedger_Click(object sender, EventArgs e)
        {
            frmMapProjectLedger frmMapPro = new frmMapProjectLedger(MapForm.Ledger, (LedgerInsertId == 0) ? LedgerAccId : LedgerInsertId);
            frmMapPro.ShowDialog();
        }

        private void lkpGroup_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(lkpGroup);
        }

        private void txtCode_Leave(object sender, EventArgs e)
        {
           // this.SetBorderColor(txtCode);
        }

        private void txtName_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtName);
            txtName.Text = this.UtilityMember.StringSet.ToSentenceCase(txtName.Text);
        }

        private void lkpGroup_EditValueChanged(object sender, EventArgs e)
        {
            if (lkpGroup.EditValue != null)
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    mappingSystem.LedgerGroupId = UtilityMember.NumberSet.ToInteger(lkpGroup.EditValue.ToString());
                    mappingSystem.LedgerId = LedgerAccId;
                    resultArgs = mappingSystem.LoadProjectMappingGrid();
                    if (resultArgs.Success && resultArgs.DataSource.Table != null)
                    {
                        DataTable dtProject = resultArgs.DataSource.Table;

                        dtProject.Columns.Add("SELECT", typeof(Int32));
                        foreach (DataRow dr in dtProject.Rows)
                        {
                            dr["SELECT"] = dr["SELECT_TEMP"];
                        }

                        if (ProjectId != 0)
                        {
                            foreach (DataRow Irow in dtProject.Rows)
                            {
                                if (Irow["PROJECT_ID"].ToString() == ProjectId.ToString())
                                {
                                    Irow["SELECT"] = 1;
                                }
                            }
                        }
                        DataView dvProject = new DataView(dtProject);
                        dvProject.Sort = "SELECT" + " DESC";
                        UcMappingLedger.GetMappingDetails = dvProject.ToTable();
                    }
                }
            }
        }

        //private void UcMappingLedger_PreviewKeyDown(object sender, EventArgs e)
        //{
        //    if (UcMappingLedger.ucGridControl.IsLastRow)
        //        mtxtNotes.Focus();
        //}

        private void UcMappingLedger_ProcessGridKey(object sender, EventArgs e)
        {
            if (UcMappingLedger.ucGridControl.IsLastRow)
                mtxtNotes.Focus();
        }

        private void chkIncludeCostCenter_CheckedChanged(object sender, EventArgs e)
        {
            CheckEdit chked = (CheckEdit)sender;
            if (!chked.Checked)
            {
                using (LedgerSystem ledgersystem = new LedgerSystem())
                {
                    resultArgs = ledgersystem.MadeTransactionByLedger(LedgerAccId.ToString());
                    //if row count is more zero than  transaction is made
                    if (resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        chked.Checked = true;
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Ledger.LEDGER_TRANSACTION_MADE));
                    }
                }
            }
        }

        private void chkTDSLedgers_CheckedChanged(object sender, EventArgs e)
        {
            if (chkTDSLedgers.Checked)
            {
                ShowLedgerProfile();
            }
        }

        private void btnTDSLedger_Click(object sender, EventArgs e)
        {
            ShowLedgerProfile();
            this.chkTDSLedgers.CheckedChanged -= new System.EventHandler(this.chkTDSLedgers_CheckedChanged);
            //chkTDSLedgers.Checked = true;
            this.chkTDSLedgers.CheckedChanged += new System.EventHandler(this.chkTDSLedgers_CheckedChanged);
        }

        private void ShowLedgerProfile()
        {
            try
            {
                if (!string.IsNullOrEmpty(txtName.Text))
                {
                    if (EnableTDS())
                    {
                        TDSLedgerTypes tdsLedgerTypes = this.UtilityMember.NumberSet.ToInteger(lkpGroup.EditValue.ToString()).Equals(8) || NatureId.Equals(2) ? TDSLedgerTypes.DirectExpense :
                            this.UtilityMember.NumberSet.ToInteger(lkpGroup.EditValue.ToString()).Equals(24) ? TDSLedgerTypes.DutiesandTaxes : TDSLedgerTypes.SunderyCreditors;
                        ACPP.Modules.TDS.frmLedgerProfile frmLedger = new TDS.frmLedgerProfile(tdsLedgerTypes);
                        frmLedger.LedgerName = txtName.Text;
                        frmLedger.LedgerProfileId = LedgerAccId;
                        frmLedger.ShowDialog();
                        dtLedgerProfile = frmLedger.dtLedgerProfile;
                        CreditorsProfileId = frmLedger.CreditorsProfileId;
                        tdsLedgersTypes = frmLedger.tdsLederTypes;
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Ledger.LEDGER_NAME_EMPTY));
                    this.SetBorderColor(txtName);
                    txtName.Focus();
                    chkTDSLedgers.Checked = false;
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void ucLedgerUsedCodes_Click(object sender, EventArgs e)
        {
            ucLedgerUsedCodes.FetchCodes(MapForm.Ledger);
        }
        #endregion

        #region Methods

        /// <summary>
        /// To load the ledger Group
        /// </summary>
        /// <param name="InitialValue"></param>
        private void LoadLedgerGroup(bool InitialValue = true)
        {
            try
            {
                using (LedgerGroupSystem ledgerSystem = new LedgerGroupSystem())
                {
                    resultArgs = ledgerSystem.LoadLedgerGroupforLedgerLoodkup(ledType);

                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        dtLedgerGroups = resultArgs.DataSource.Table;
                        if (ledType == ledgerSubType.TDS)
                        {
                            DataView dvLedgers = resultArgs.DataSource.Table.DefaultView;
                            dvLedgers.RowFilter = "GROUP_ID IN(" + (int)TDSDefaultLedgers.DirectExpense + "," + (int)TDSDefaultLedgers.DutiesandTaxes + "," + (int)TDSDefaultLedgers.SunderyCreditors + ") OR NATURE_ID IN(" + (int)Natures.Expenses + ") OR NATURE_ID IN(" + (int)Natures.Expenses + ")";
                            this.UtilityMember.ComboSet.BindLookUpEditCombo(lkpGroup, dvLedgers.ToTable(), ledgerSystem.AppSchema.LedgerGroup.LEDGER_GROUPColumn.ToString(), ledgerSystem.AppSchema.LedgerGroup.GROUP_IDColumn.ToString());
                            dvLedgers.RowFilter = "";
                        }
                        else
                        {
                            this.UtilityMember.ComboSet.BindLookUpEditCombo(lkpGroup, resultArgs.DataSource.Table, ledgerSystem.AppSchema.LedgerGroup.LEDGER_GROUPColumn.ToString(), ledgerSystem.AppSchema.LedgerGroup.GROUP_IDColumn.ToString());

                            if (ledType.Equals(ledgerSubType.FD))
                            {
                                lkpGroup.EditValue = lkpGroup.Properties.GetDataSourceValue(lkpGroup.Properties.ValueMember, 0);
                            }
                        }
                        if (InitialValue)
                        {
                            //  lkpGroup.EditValue = lkpGroup.Properties.GetDataSourceValue(lkpGroup.Properties.ValueMember, 0);
                        }
                        lkpGroup.Enabled = ledType == ledgerSubType.GN ? true : ledType == ledgerSubType.TDS ? true : false;
                    }
                }

            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message + System.Environment.NewLine + Ex.Source);
            }
            finally { }
        }

        /// <summary>
        /// To assign the details in the clrts
        /// </summary>
        private void AssignLedgerDetails()
        {
            try
            {
                if (LedgerAccId != 0)
                {
                    using (LedgerSystem ledgerSystem = new LedgerSystem(LedgerAccId))
                    {
                        txtCode.Text = ledgerSystem.LedgerCode;

                        txtName.Text = ledgerSystem.LedgerName;
                        lkpGroup.EditValue = ledgerSystem.GroupId.ToString();
                        mtxtNotes.Text = ledgerSystem.LedgerNotes;
                        grdLedgerType.EditValue = (ledgerSystem.LedgerType == ledgerSubType.GN.ToString()) ? (int)LedgerType.General : (int)LedgerType.InKind;
                        loadLedgerType(false);
                        if (ledgerSystem.IsCostCentre == (int)YesNo.No)
                            chkIncludeCostCenter.Checked = false;
                        else
                            chkIncludeCostCenter.Checked = true;
                        if (ledgerSystem.IsBankInterestLedger == (int)YesNo.No)
                            chkBankInterestLedger.Checked = false;
                        else
                            chkBankInterestLedger.Checked = true;

                        if (ledgerSystem.IsTDSLedger.Equals((int)YesNo.Yes))
                            chkTDSLedgers.Checked = true;
                        else
                            chkTDSLedgers.Checked = false;

                        if (LedgerAccessRights == 1)
                        {
                            txtName.Enabled = false;
                        }
                        else
                        {
                            txtName.Enabled = true;
                        }
                                                
                        glkpHOLedger.EditValue = ledgerSystem.HeadofficeLedgerId;
                        glkpInvestmentType.EditValue = ledgerSystem.FDInvestmentTypeId;

                        //On 08/05/2024, To lock investment type if it has FD accoutns
                        //if (ledgerSystem.FDInvestmentTypeId == (int)FDInvestmentType.MutualFund)
                        //{
                            using (FDAccountSystem fdsystem = new FDAccountSystem())
                            {
                                /*bool rtn = fdsystem.IsFDAccountsExistsByInvestmentType(FDInvestmentType.MutualFund);
                                glkpInvestmentType.Enabled = !rtn;*/

                                resultArgs = fdsystem.FetchByLedgerId(LedgerAccId);

                                if (resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                                {
                                    glkpInvestmentType.Enabled = false;
                                    glkpInvestmentType.ToolTip = "'" + glkpInvestmentType.Text + "' has FD Account(s), you can't change.";
                                }
                            }
                        //}


                        if (lcLedgerCloseDate.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
                        {
                            //deLedgerDateClosed.Text = ledgerSystem.LedgerDateClosed != DateTime.MinValue ? ledgerSystem.LedgerDateClosed.ToShortDateString() : "";

                            if (ledgerSystem.LedgerDateClosed != DateTime.MinValue)
                            {
                                deLedgerDateClosed.DateTime = ledgerSystem.LedgerDateClosed;
                            }
                        }

                        glkpCurrencyCountry.EditValue = null;
                        if (this.AppSetting.AllowMultiCurrency == 1)
                        {
                            glkpCurrencyCountry.EditValue = ledgerSystem.LedgerCurrencyCountryId;

                            glkpCurrencyCountry.Enabled = true;
                            using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
                            {
                                voucherTransaction.LedgerId = LedgerAccId;
                                resultArgs = voucherTransaction.MadeTransactionForLedger();
                                if (resultArgs.DataSource.Table.Rows.Count != 0)
                                {
                                    glkpCurrencyCountry.Enabled = false;
                                    txtOPExchangeRate.Enabled = false;
                                }
                            }

                            txtOPExchangeRate.Text = ledgerSystem.LedgerCurrencyOPExchangeRate.ToString();
                            txtOPExchangeRate.Tag = txtOPExchangeRate.Text;
                        }

                    }

                    
                    // btnNew.Enabled = false;
                }

            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        private void LoadCurrency()
        {
            try
            {
                using (CountrySystem countrySystem = new CountrySystem())
                {
                    resultArgs = countrySystem.FetchCountryCurrencyDetails(AppSetting.FirstFYDateFrom.Date);
                    if (resultArgs.Success)
                    {
                        DataTable dtCurrencies = resultArgs.DataSource.Table;
                        dtCurrencies.DefaultView.RowFilter = countrySystem.AppSchema.Country.EXCHANGE_RATEColumn.ColumnName + "> 0 ";
                        dtCurrencies = dtCurrencies.DefaultView.ToTable();

                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpCurrencyCountry, dtCurrencies,
                            countrySystem.AppSchema.Country.CURRENCYColumn.ToString(), countrySystem.AppSchema.Country.COUNTRY_IDColumn.ToString());
                       
                       object findcountry = glkpCurrencyCountry.Properties.GetDisplayValueByKeyValue(this.AppSetting.Country);
                       if (findcountry != null)
                       {
                           glkpCurrencyCountry.EditValue = this.AppSetting.Country;
                       }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// On 17/10/2023 To show option to Map with Head office Ledger
        /// </summary>
        private void ShowMapHOLedger()
        {
            
            lciHOLedger.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            if (AppSetting.EnablePortal == 1)
            {
                if (ledType == ledgerSubType.FD)
                {
                    lciHOLedger.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
                    LoadHeadOfficeLedgers();
                }
            }
        }

        /// <summary>
        /// To Validate Ledger Details
        /// </summary>
        /// <returns></returns>

        private bool ValidateLedgerDetails()
        {
            bool IsGroudValid = true;
            //if (string.IsNullOrEmpty(txtCode.Text.Trim()))
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Ledger.lEDGER_CODE_EMPTY));
            //    this.SetBorderColor(txtCode);
            //    IsGroudValid = false;
            //    txtCode.Focus();
            //}
            if (string.IsNullOrEmpty(txtName.Text.Trim()))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Ledger.LEDGER_NAME_EMPTY));
                this.SetBorderColor(txtName);
                IsGroudValid = false;
                txtName.Focus();
            }
            else if (lkpGroup.EditValue == null)
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Ledger.LEDGER_GROUP_EMPTY));
                this.SetBorderColor(lkpGroup);
                IsGroudValid = false;
                lkpGroup.Focus();
            }
            else if (this.AppSetting.AllowMultiCurrency == 1 && lcgCurrencyDetails.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always)
            {
                int CountryCurrencyId = (glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString()));
                double exchangerate = UtilityMember.NumberSet.ToDouble(txtOPExchangeRate.Text);
                if (CountryCurrencyId == 0 || exchangerate==0)
                {
                    MessageRender.ShowMessage("As Multi Currency option is enabled, Currecny details should be filled.");
                    IsGroudValid = false;
                }
            }
            else
            {
                if (lciHOLedger.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always &&
                     this.AppSetting.EnablePortal == 1 && this.AppSetting.MapHeadOfficeLedger == 1) //17/10/2023, to check mapping ledger is mandatory
                {
                    int HOMappedLedgerId = glkpHOLedger.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpHOLedger.EditValue.ToString()) : 0;
                    if (HOMappedLedgerId == 0)
                    {
                        this.ShowMessageBox("Map Branch Ledger with Head Office Ledger");
                        this.SetBorderColor(lkpGroup);
                        IsGroudValid = false;
                        glkpHOLedger.Focus();
                    }
                }
            }

            if (IsGroudValid && lcLedgerCloseDate.Visibility == DevExpress.XtraLayout.Utils.LayoutVisibility.Always && !string.IsNullOrEmpty(deLedgerDateClosed.Text.Trim()))
            {
                using (LedgerSystem ledgersystem = new LedgerSystem())
                {
                    resultArgs = ledgersystem.CheckLedgerClosedDate(LedgerAccId, deLedgerDateClosed.DateTime);
                    if (!resultArgs.Success)
                    {
                        this.ShowMessageBox(resultArgs.Message);
                        IsGroudValid = false;
                    }
                }
            }
            return IsGroudValid;
        }

        /// <summary>
        /// On 17/10/2023, load head office ledgers
        /// </summary>
        private void LoadHeadOfficeLedgers()
        {
            try
            {
                if (AppSetting.EnablePortal == 1 && ledType==ledgerSubType.FD)
                {
                    using (ExportVoucherSystem exportVoucher = new ExportVoucherSystem())
                    {
                        resultArgs = exportVoucher.HeadOfficeLedgers();
                        if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                        {
                            DataTable dtHoLedgers = resultArgs.DataSource.Table;
                            //On 28/11/2019, to skip HO FD ledger for generl ledger mapping
                            dtHoLedgers.DefaultView.RowFilter = exportVoucher.AppSchema.LedgerGroup.GROUP_IDColumn.ColumnName  + " = " + (int)FixedLedgerGroup.FixedDeposit;
                            dtHoLedgers = dtHoLedgers.DefaultView.ToTable();
                                                        
                            this.UtilityMember.ComboSet.BindGridLookUpComboEmptyItem(glkpHOLedger, dtHoLedgers, "HEADOFFICELEDGER", "HEADOFFICE_LEDGER_ID", true, "");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
        }

        private void LoadInvestmentType()
        {
            try
            {
                using (MappingSystem mappingsys = new MappingSystem())
                {
                    resultArgs = mappingsys.FetchInvestmentType();
                    if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataTable dtInvestmentType = resultArgs.DataSource.Table;

                        this.UtilityMember.ComboSet.BindGridLookUpComboEmptyItem(glkpInvestmentType, dtInvestmentType,
                            mappingsys.AppSchema.FDInvestmentType.INVESTMENT_TYPEColumn.ColumnName, mappingsys.AppSchema.FDInvestmentType.INVESTMENT_TYPE_IDColumn.ColumnName,false,string.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString());
            }
        }

        /// <summary>
        /// To load the available Leger codes in the combo box.
        /// </summary>
        private void LoadLedgerCodes()
        {
            using (LedgerSystem ledgersystem = new LedgerSystem())
            {
                resultArgs = ledgersystem.FetchLedgerCodes();
                if (resultArgs.DataSource != null && resultArgs.RowsAffected > 0)
                {
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpLedgerCodes, resultArgs.DataSource.Table, ledgersystem.AppSchema.Ledger.LEDGER_CODEColumn.ColumnName, ledgersystem.AppSchema.Ledger.LEDGER_CODEColumn.ColumnName);

                    glkpLedgerCodes.EditValue = glkpLedgerCodes.Properties.GetKeyValue(0);
                    if (LedgerAccId == 0)
                    {
                        DataTable dtCode = resultArgs.DataSource.Table;
                        txtCode.Text = CodePredictor(glkpLedgerCodes.Properties.GetKeyValue(0).ToString(), dtCode);
                    }
                }
            }
        }

        /// <summary>
        /// To set the form title in runtime
        /// </summary>

        private void SetTitle()
        {
            if (ledType == ledgerSubType.GN)
            {
                this.Text = LedgerAccId == 0 ? this.GetMessage(MessageCatalog.Master.Ledger.LEDGER_ADD_CAPTION) : this.GetMessage(MessageCatalog.Master.Ledger.LEDGER_EDIT_CAPTION);
            }
            else if (ledType == ledgerSubType.FD)
            {
                this.Text = LedgerAccId == 0 ? this.GetMessage(MessageCatalog.Master.FDLedger.FD_LEDGER_ADD) : this.GetMessage(MessageCatalog.Master.FDLedger.FD_LEDGER_EDIT);
            }
            else if (ledType == ledgerSubType.TDS)
            {
                //this.Text = LedgerAccId == 0 ? this.GetMessage(MessageCatalog.Master.FDLedger.TDS_ADD) : this.GetMessage(MessageCatalog.Master.FDLedger.TDS_EDIT);
                this.Text = LedgerAccId == 0 ? this.GetMessage(MessageCatalog.Master.Bank.TDS_LEDGER_ADD_CAPTION) : this.GetMessage(MessageCatalog.Master.Bank.TDS_LEDGER_EDIT_CAPTION);
            }
        }

        /// <summary>
        /// To clear the control Values
        /// </summary>
        private void ClearControls()
        {
            txtCode.Text = "";
            txtName.Text = "";
            mtxtNotes.Text = "";
            chkTDSLedgers.Checked = chkIncludeCostCenter.Checked = chkBankInterestLedger.Checked = false;
            lkpGroup.EditValue = null;
            deLedgerDateClosed.Text = string.Empty;
            txtOPExchangeRate.Text = "";
            glkpCurrencyCountry.EditValue = UtilityMember.NumberSet.ToInteger(AppSetting.Country);

            object findcountry = glkpCurrencyCountry.Properties.GetDisplayValueByKeyValue(this.AppSetting.Country);
            if (findcountry == null) glkpCurrencyCountry.EditValue = null;
        }
        private void loadLedgerType(bool initialValue)
        {
            LedgerType ledgerType = new LedgerType();
            DataView dvReceiptType = this.UtilityMember.EnumSet.GetEnumDataSource(ledgerType, Sorting.Ascending);
            grdLedgerType.Properties.DataSource = dvReceiptType.ToTable();
            grdLedgerType.Properties.DisplayMember = "Name";
            grdLedgerType.Properties.ValueMember = "Id";
            if (initialValue)
                grdLedgerType.EditValue = grdLedgerType.Properties.GetKeyValue(0);

            if (ledType == ledgerSubType.FD)
            {
                grdLedgerType.EditValue = grdLedgerType.Properties.GetKeyValue(0);
                grdLedgerType.Enabled = lkpGroup.Enabled = false;
                layoutControlItem3.Visibility = LayoutVisibility.Never;
                layoutControlItem6.Visibility = LayoutVisibility.Never;
                emptySpaceItem4.Visibility = LayoutVisibility.Never;
                emptySpaceItem5.Visibility = LayoutVisibility.Never;
                UcMappingLedger.OpBalanceVisible = initialValue == true ? false : true;
                UcMappingLedger.TransModeVisible = false;
                UcMappingLedger.SelectFixedWidth = true;
                UcMappingLedger.FDLedgerSubType = ledType;
                UcMappingLedger.FDAccountID = LedgerAccId;
            }
            else if (ledType == ledgerSubType.TDS)
            {
                grdLedgerType.Enabled = lkpGroup.Enabled = false;
                layoutControlItem3.Visibility = LayoutVisibility.Never;
                layoutControlItem6.Visibility = LayoutVisibility.Never;
                emptySpaceItem4.Visibility = LayoutVisibility.Never;
                emptySpaceItem5.Visibility = LayoutVisibility.Never;
                emptySpaceItem3.Visibility = layoutControlItem1.Visibility = LayoutVisibility.Always;
                layoutControlItem4.Visibility = emptySpaceItem6.Visibility = LayoutVisibility.Always;
            }
        }

        private string GetLedgerType(int groupId)
        {
            string ledgerType = "";
            if (this.UtilityMember.NumberSet.ToInteger(grdLedgerType.EditValue.ToString()) == (int)LedgerType.General)
                ledgerType = ledgerSubType.GN.ToString();
            else
                ledgerType = ledgerSubType.IK.ToString();
            return ledgerType;
        }

        private string GetSubType(int groupId)
        {
            string ledgerSubtype = "";
            if (this.UtilityMember.NumberSet.ToInteger(grdLedgerType.EditValue.ToString()) == (int)LedgerType.General)
                ledgerSubtype = (groupId == (int)FixedLedgerGroup.Cash) ? ledgerSubType.CA.ToString() : ledgerSubType.GN.ToString();
            else
                ledgerSubtype = ledgerSubType.IK.ToString();
            return ledgerSubtype;
        }

        private int GetSortOrder(int groupId)
        {
            int sortId = 0;
            if (this.UtilityMember.NumberSet.ToInteger(grdLedgerType.EditValue.ToString()) == (int)LedgerType.General)
                sortId = (groupId == (int)FixedLedgerGroup.Cash) ? (int)LedgerSortOrder.Cash : (int)LedgerSortOrder.GN;
            else
                sortId = (int)LedgerSortOrder.IK;
            return sortId;
        }

        private bool EnableTDS()
        {
            bool isTDSEnable = false;
            NatureName = lkpGroup.Text;
            GroupId = lkpGroup.EditValue != null && !lkpGroup.EditValue.ToString().Equals("0") ? this.UtilityMember.NumberSet.ToInteger(lkpGroup.EditValue.ToString()) : 0;
            if (GroupId > 0 && dtLedgerGroups != null
                && dtLedgerGroups.Rows.Count > 0)
            {
                DataView dvLedgerGroup = dtLedgerGroups.DefaultView;
                dvLedgerGroup.RowFilter = "GROUP_ID =" + GroupId + "";
                if (dvLedgerGroup != null && dvLedgerGroup.Count > 0)
                {
                    NatureId = dvLedgerGroup.ToTable().Rows[0]["NATURE_ID"] != null ? this.UtilityMember.NumberSet.ToInteger(dvLedgerGroup.ToTable().Rows[0]["NATURE_ID"].ToString()) : 0;
                }
                if (NatureId.Equals(2) || NatureName.Equals("Duties and Taxes") || NatureName.Equals("Sundry Creditors") || GroupId.Equals(24) || GroupId.Equals(26))
                {
                    chkTDSLedgers.Enabled = true;
                }
                else
                {
                    chkTDSLedgers.Checked = chkTDSLedgers.Enabled = false;
                }
                isTDSEnable = true;
            }
            return isTDSEnable;
        }

        private void ucLedgerUsedCodes_Iconclicked(object sender, EventArgs e)
        {
            ucLedgerUsedCodes.FetchCodes(MapForm.Ledger);
        }

        private void glkpCurrencyCountry_EditValueChanged(object sender, EventArgs e)
        {
            //On 09/01/2025 - In edit mode, if they change country, we can show exchange rate
            if (this.LedgerAccId > 0) txtOPExchangeRate.Tag = null;
            ShowCurrencyDetails();
        }



        //private void FetchProjects()
        //{
        //    using (MappingSystem mappingSystem = new MappingSystem())
        //    {
        //        mappingSystem.LedgerGroupId = lkpGroup.EditValue != null ? UtilityMember.NumberSet.ToInteger(lkpGroup.EditValue.ToString()) : 0;
        //        mappingSystem.LedgerId = LedgerAccId;
        //        resultArgs = mappingSystem.LoadProjectMappingGrid();
        //        UcMappingLedger.GetMappingDetails = resultArgs.DataSource.Table;
        //    }
        //}

        private void ShowCurrencyDetails()
        {
            int CountryId = (glkpCurrencyCountry.EditValue == null ? 0 : this.UtilityMember.NumberSet.ToInteger(glkpCurrencyCountry.EditValue.ToString()));
            lblCurrencyName.Text = string.Empty;
            lblCurrencySymbol.Text = string.Empty;
            //lblOpeningAvgExchangeRate.Text = "0.00";
            txtOPExchangeRate.Text = "1";
            try
            {
                if (CountryId != 0)
                {
                    using (CountrySystem countrySystem = new CountrySystem())
                    {
                        ResultArgs result = countrySystem.FetchCountryCurrencyExchangeRateByCountryDate(CountryId, AppSetting.FirstFYDateFrom.Date);
                        if (result.Success)
                        {
                            lblCurrencySymbol.Text = countrySystem.CurrencySymbol;
                            lblCurrencyName.Text = countrySystem.CurrencyName;
                            //lblOpeningAvgExchangeRate.Text = UtilityMember.NumberSet.ToNumber(countrySystem.ExchangeRate);
                            if (txtOPExchangeRate.Tag==null) txtOPExchangeRate.Text = UtilityMember.NumberSet.ToNumber(countrySystem.ExchangeRate);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }
        #endregion


    }
}