using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using Bosco.Utility;
using Bosco.Model.UIModel;
using DevExpress.XtraEditors;
using Bosco.DAO.Data;
using Bosco.Model;
using DevExpress.XtraGrid;
using DevExpress.XtraEditors.Controls;
using Bosco.Model.Transaction;

namespace ACPP.Modules.Inventory.Asset
{
    public partial class frmRenewInsuranceVoucherAdd : frmFinanceBaseAdd
    {
        #region VariableDeclaration
        ResultArgs resultArgs = new ResultArgs();
        public event EventHandler UpdateHeld;
        public int ItemId = 0;
        public int InsuranceDetailId = 0;
        public int ItemDetailId = 0;
        private int RowNo = 0;
        private decimal AssetIdAmount;
        private string AssetId = string.Empty;
        private string AssetRegistrationDate = string.Empty;
        private string AssetItem = string.Empty;
        private string PolicyNo = string.Empty;
        private int HistoryRecordCount = 0;
        private string Amount = string.Empty;
        public DialogResult Dialogresult = DialogResult.Cancel;
        public AssetInOut AssetInoutType;

        #endregion

        private double BankTransSummaryVal
        {
            get { return ucAssetJournal1.BankTransSummaryVal; }
        }

        public int VoucherId { get; set; }

        #region Properties
        public int mode { get; set; }
        AssetInsurance PurchaseData = new AssetInsurance();
        public DataTable dtInsuranceList = new DataTable();

        private Bosco.DAO.Schema.AppSchemaSet.ApplicationSchemaSet appSchema = null;
        private Bosco.DAO.Schema.AppSchemaSet.ApplicationSchemaSet AppSchemas
        {
            get { return appSchema = new Bosco.DAO.Schema.AppSchemaSet().AppSchema; }
        }
        // public Dictionary<int, DataTable> AssetInsuranceCollections = new Dictionary<int, DataTable>();
        public Dictionary<Tuple<int, int>, DataTable> AssetMultiInsuranceCollection = new Dictionary<Tuple<int, int>, DataTable>();
        public Dictionary<Tuple<int, int>, DataTable> AssetMultiInsuranceVoucherCollection = new Dictionary<Tuple<int, int>, DataTable>();
        private DateTime dtRenewDate = DateTime.Now;
        public int Projectid { get; set; }
        public string ProjectName { get; set; }
        #endregion

        #region Constructor
        public frmRenewInsuranceVoucherAdd()
        {
            InitializeComponent();
        }
        public frmRenewInsuranceVoucherAdd(int insDetailsId, int IDetailId, string AItem, string AId, int mode, string RenewalDate, string amount)
            : this()
        {
            InsuranceDetailId = insDetailsId;
            ItemDetailId = IDetailId;
            lblAssetItemData.Text = AssetItem = AItem;
            lblAssetIdData.Text = AssetId = AId;
            AssetRegistrationDate = this.UtilityMember.DateSet.ToDate(RenewalDate);
            Amount = amount;
            this.mode = mode;
        }
        public frmRenewInsuranceVoucherAdd(int itemdetailid, int itemid, int rowNo, string AItem, string AId, decimal IdAmount, AssetInsurance purchase)
            : this()
        {
            ItemDetailId = itemdetailid;
            ItemId = itemid;
            RowNo = rowNo;
            AssetItem = AItem;
            AssetId = AId;
            PurchaseData = purchase;
            lblAssetItemData.Text = AItem;
            lblAssetIdData.Text = AId;
            AssetIdAmount = IdAmount;
        }
        #endregion

        #region Events
        /// <summary>
        /// load Insurance
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmRenewInsurance_Load(object sender, EventArgs e)
        {
            lblProject.Text = ProjectName;
            if (!string.IsNullOrEmpty(ProjectName))
            {
                lblProject.Text = ProjectName;
                //lblProjectTitle.Text = "Project :";
                lblProjectTitle.Text = this.GetMessage(MessageCatalog.Asset.InsuranceRenew.INSURANCE_PROJECT_INFO);
            }
            else
            {
                lblProject.Text = lblProjectTitle.Text = " ";
            }
            //lblVoucherCaption.Text = "Voucher No. :";
            lblVoucherCaption.Text = this.GetMessage(MessageCatalog.Asset.InsuranceRenew.INSURANCE_VOUCHERNO_INFO);
            LoadRenewalDate();
            LoadInsurancePlan();
            DefaultLoad();
            AssignValueToControls();
            LoadInsurance();
            ShowRenewalDate();
            AssignSumInsuredAmount();
            SetDefaults();
            LoadNarrationAutoComplete();
            FinanceIntegrationDefaults();

            if (PurchaseData.Equals(AssetInsurance.Purchase) || PurchaseData.Equals(AssetInsurance.InKind))
            {
                //btnSave.Text = "  Ok  ";
                btnSave.Text = this.GetMessage(MessageCatalog.Asset.InsuranceRenew.INSURANCE_OK_CAPTION);
                //btnClose.Text = "Cancel";
                btnClose.Text = this.GetMessage(MessageCatalog.Asset.InsuranceRenew.INSURANCE_CANCEL_CAPTION);
                btnSave.Size = new System.Drawing.Size(62, 21);
                btnClose.Size = new System.Drawing.Size(62, 21);
                UpdateDataTableValues();
            }
            else if (PurchaseData.Equals(AssetInsurance.Opening))
            {
                HideVoucherRenewDetails();
            }
        }

        private void LoadRenewalDate()
        {
            dtRegRenewalDate.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dtRegRenewalDate.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            dtRegRenewalDate.DateTime = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
        }

        private void FinanceIntegrationDefaults()
        {
            ucAssetJournal1.ProjectId = Projectid;
            ucAssetJournal1.ShowDeleteColumn = false;
            ucAssetJournal1.Flag = AssetInOut.INS;
            ucAssetJournal1.LoadLedger();
            ucAssetJournal1.NextFocusControl = txtNarration;

            if (mode == (int)AssetInsurance.Update || mode == (int)AssetInsurance.InKind || mode == (int)AssetInsurance.Purchase && VoucherId > 0)
            {
                ucAssetJournal1.AssignValues(VoucherId);
                LoadFinanceProperties();
            }
            else
            {
                VoucherId = 0;
                ucAssetJournal1.ConstructCashTransEmptySournce();
            }
        }

        //PENDING
        private void SetTittle()
        {
             this.Text =VoucherId == 0 ? this.GetMessage(MessageCatalog.Asset.InsuranceRenew.INSURANCE_VOUCHER_ADD_CAPTION) : this.GetMessage(MessageCatalog.Asset.InsuranceRenew.INSURANCE_VOUCHER_EDIT_CAPTION);
        }

        private void LoadNarrationAutoComplete()
        {
            try
            {
                using (VoucherTransactionSystem vouchermastersystem = new VoucherTransactionSystem())
                {
                    resultArgs = vouchermastersystem.AutoFetchNarration();
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        DataView dvNarration = resultArgs.DataSource.Table.AsDataView();
                        AutoCompleteStringCollection collection = new AutoCompleteStringCollection();
                        foreach (DataRow dr in resultArgs.DataSource.Table.Rows)
                        {
                            collection.Add(dr[vouchermastersystem.AppSchema.VoucherMaster.NARRATIONColumn.ColumnName].ToString());
                        }
                        txtNarration.MaskBox.AutoCompleteSource = AutoCompleteSource.CustomSource;
                        txtNarration.MaskBox.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
                        txtNarration.MaskBox.AutoCompleteCustomSource = collection;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void LoadFinanceProperties()
        {
            try
            {
                using (AssetInwardOutwardSystem inwardoutward = new AssetInwardOutwardSystem())
                {
                    resultArgs = inwardoutward.FetchVoucherDetailsByVoucherId(VoucherId);
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        lblVoucherNo.Text = resultArgs.DataSource.Table.Rows[0]["VOUCHER_NO"].ToString();
                        txtNarration.Text = resultArgs.DataSource.Table.Rows[0]["NARRATION"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void LoadVoucherNo()
        {
            string vType = string.Empty;
            string pId = string.Empty;
            using (VoucherTransactionSystem voucherTransaction = new VoucherTransactionSystem())
            {
                voucherTransaction.VoucherType = VoucherSubTypes.PY.ToString();
                voucherTransaction.ProjectId = Projectid;
                voucherTransaction.VoucherDate = this.UtilityMember.DateSet.ToDate(dtRegRenewalDate.Text, false);
                lblVoucherNo.Text = voucherTransaction.TempVoucherNo();
            }
        }

        private void SetDefaults()
        {
            ResultArgs result = new ResultArgs();
            DataTable dtInsurance = new DataTable();
            if (InsuranceDetailId > 0 && ItemDetailId > 0)
            {
                result = FetchPreviousRenewal();
                if (result != null && result.Success && resultArgs.DataSource.Table != null)
                {
                    if (resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        dtPeriodFrom.Properties.MinValue = dtPeriodTo.Properties.MinValue = this.UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0]["PERIOD_TO"].ToString(), false);
                    }
                }

                result = FetchNextRenewal();
                if (result != null && result.Success && resultArgs.DataSource.Table != null)
                {
                    if (resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        dtPeriodTo.Properties.MaxValue = dtPeriodFrom.Properties.MaxValue = this.UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0]["PERIOD_FROM"].ToString(), false);
                    }
                }
            }
        }

        // <summary>
        /// Save renew Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateRenewDetails())
                {
                    if (!PurchaseData.Equals(AssetInsurance.Purchase) && !PurchaseData.Equals(AssetInsurance.Opening) && !PurchaseData.Equals(AssetInsurance.InKind))
                    {
                        using (AssetItemSystem Itemsystem = new AssetItemSystem())
                        {
                            using (AssetInwardOutwardSystem inoutSystem = new AssetInwardOutwardSystem())
                            {
                                ItemDetailId = inoutSystem.FetchItemDetailByAssetId(AssetId);
                            }
                            if (ItemDetailId > 0)
                            {
                                using (InsuranceRenewSystem InsRenewSystem = new InsuranceRenewSystem())
                                {
                                    InsRenewSystem.ProjectId = Projectid;
                                    InsRenewSystem.RenewalDate = this.UtilityMember.DateSet.ToDate(dtRegRenewalDate.Text, false);
                                    InsRenewSystem.VoucherNo = lblVoucherNo.Text;
                                    InsRenewSystem.Narration = txtNarration.Text;
                                    InsRenewSystem.VoucherId = VoucherId;
                                    resultArgs = InsRenewSystem.SaveFinanceVoucher(ucAssetJournal1.DtCashBank);
                                    if (resultArgs.Success)
                                    {
                                        InsRenewSystem.InsDetailId = (mode == (int)AssetInsurance.Update)
                                            ? InsuranceDetailId : (int)AddNewRow.NewRow;
                                        InsRenewSystem.ItemDetailId = ItemDetailId;
                                        InsRenewSystem.InsurancePlanId = this.UtilityMember.NumberSet.ToInteger(glkpInsPlan.EditValue.ToString());
                                        InsRenewSystem.PolicyNo = txtPolicyNo.Text.Trim();
                                        InsRenewSystem.SumInsured = this.UtilityMember.NumberSet.ToDecimal(txtSumInsured.Text.Trim());
                                        InsRenewSystem.PremiumAmount = this.UtilityMember.NumberSet.ToDecimal(txtPremiumAmt.Text.Trim());
                                        InsRenewSystem.PeriodFrom = this.UtilityMember.DateSet.ToDate(dtPeriodFrom.Text, false);
                                        InsRenewSystem.PeriodTo = this.UtilityMember.DateSet.ToDate(dtPeriodTo.Text, false);
                                        resultArgs = InsRenewSystem.SaveRenewDetails();
                                        if (resultArgs.Success)
                                        {
                                            this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                                            ClearControls();
                                            LoadInsurance();
                                            LoadNarrationAutoComplete();
                                            DefaultLoad();
                                            LoadVoucherNo();
                                            if (UpdateHeld != null)
                                            {
                                                UpdateHeld(this, e);
                                                this.Close();
                                            }
                                            else
                                            {
                                                glkpInsPlan.Focus();
                                            }
                                        }
                                        else
                                        {
                                            this.ShowMessageBox(resultArgs.Message);
                                        }
                                    }
                                    else
                                    {
                                        this.ShowMessageBox(resultArgs.Message);
                                    }
                                }
                            }
                        }
                    }
                    else
                    {
                        if (!dtInsuranceList.Columns.Contains(AppSchemas.ASSETItem.ITEM_IDColumn.ColumnName))
                        {
                            dtInsuranceList.Columns.Add(AppSchemas.AssetInsuranceDetail.INSURANCE_DETAIL_IDColumn.ColumnName, typeof(int));
                            dtInsuranceList.Columns.Add(AppSchemas.AssetInsuranceDetail.ITEM_DETAIL_IDColumn.ColumnName, typeof(int));
                            dtInsuranceList.Columns.Add(AppSchemas.ASSETItem.ITEM_IDColumn.ColumnName, typeof(int));
                            dtInsuranceList.Columns.Add(AppSchemas.ASSETItem.ASSET_ITEMColumn.ColumnName, typeof(string));
                            dtInsuranceList.Columns.Add(appSchema.ASSETItem.ASSET_IDColumn.ColumnName, typeof(string));
                            dtInsuranceList.Columns.Add(AppSchemas.InsurancePlan.INSURANCE_PLAN_IDColumn.ColumnName, typeof(int));
                            dtInsuranceList.Columns.Add(appSchema.ASSETItem.POLICY_NOColumn.ColumnName, typeof(string));
                            dtInsuranceList.Columns.Add(AppSchemas.AssetInsuranceDetail.RENEWAL_DATEColumn.ColumnName, typeof(DateTime));
                            dtInsuranceList.Columns.Add(AppSchemas.AssetInsuranceDetail.SUM_INSUREDColumn.ColumnName, typeof(decimal));
                            dtInsuranceList.Columns.Add(AppSchemas.AssetInsuranceDetail.PREMIUM_AMOUNTColumn.ColumnName, typeof(decimal));
                            dtInsuranceList.Columns.Add(AppSchemas.AssetInsuranceDetail.PERIOD_FROMColumn.ColumnName, typeof(DateTime));
                            dtInsuranceList.Columns.Add(AppSchemas.AssetInsuranceDetail.PERIOD_TOColumn.ColumnName, typeof(DateTime));
                            // Praveen : Voucher Integation
                            dtInsuranceList.Columns.Add(AppSchemas.Project.PROJECT_IDColumn.ColumnName, typeof(int));
                            dtInsuranceList.Columns.Add(AppSchemas.VoucherMaster.NARRATIONColumn.ColumnName, typeof(string));
                            dtInsuranceList.Columns.Add(AppSchemas.VoucherMaster.VOUCHER_NOColumn.ColumnName, typeof(string));
                            dtInsuranceList.Columns.Add(AppSchemas.AssetInsuranceDetail.VOUCHER_IDColumn.ColumnName, typeof(int));
                        }
                        dtInsuranceList.Rows.Add();
                        dtInsuranceList.AsEnumerable().ToList<DataRow>().ForEach(dr =>
                            {
                                dr[AppSchemas.AssetInsuranceDetail.INSURANCE_DETAIL_IDColumn.ColumnName] = InsuranceDetailId;
                                dr[AppSchemas.AssetInsuranceDetail.ITEM_DETAIL_IDColumn.ColumnName] = ItemDetailId;
                                dr[AppSchemas.ASSETItem.ITEM_IDColumn.ColumnName] = ItemId;
                                dr[AppSchemas.ASSETItem.ASSET_ITEMColumn.ColumnName] = AssetItem;
                                dr[appSchema.ASSETItem.ASSET_IDColumn.ColumnName] = AssetId;
                                dr[AppSchemas.InsurancePlan.INSURANCE_PLAN_IDColumn.ColumnName] = this.UtilityMember.NumberSet.ToInteger(glkpInsPlan.EditValue.ToString());
                                dr[appSchema.ASSETItem.POLICY_NOColumn.ColumnName] = txtPolicyNo.Text.Trim();
                                dr[appSchema.AssetInsuranceDetail.RENEWAL_DATEColumn.ColumnName] = this.UtilityMember.DateSet.ToDate(dtRegRenewalDate.Text, false);
                                dr[appSchema.AssetInsuranceDetail.SUM_INSUREDColumn.ColumnName] = this.UtilityMember.NumberSet.ToDecimal(txtSumInsured.Text.Trim());
                                dr[appSchema.AssetInsuranceDetail.PREMIUM_AMOUNTColumn.ColumnName] = this.UtilityMember.NumberSet.ToDecimal(txtPremiumAmt.Text.Trim());
                                dr[appSchema.AssetInsuranceDetail.PERIOD_FROMColumn.ColumnName] = this.UtilityMember.DateSet.ToDate(dtPeriodFrom.Text, false);
                                dr[appSchema.AssetInsuranceDetail.PERIOD_TOColumn.ColumnName] = this.UtilityMember.DateSet.ToDate(dtPeriodTo.Text, false);
                                // Praveen : Voucher Integation
                                dr[AppSchemas.Project.PROJECT_IDColumn.ColumnName] = Projectid;
                                dr[AppSchemas.VoucherMaster.NARRATIONColumn.ColumnName] = txtNarration.Text;
                                dr[AppSchemas.VoucherMaster.VOUCHER_NOColumn.ColumnName] = lblVoucherNo.Text;
                                dr[AppSchemas.AssetInsuranceDetail.VOUCHER_IDColumn.ColumnName] = VoucherId;

                            });

                        var Inskey = new Tuple<int, int>(ItemId, RowNo);
                        var Insvalue = dtInsuranceList;
                        var InsVoucherValue = ucAssetJournal1.DtCashBank;

                        if (!AssetMultiInsuranceCollection.ContainsKey(Inskey))
                            AssetMultiInsuranceCollection.Add(Inskey, Insvalue);
                        else
                            AssetMultiInsuranceCollection[Inskey] = Insvalue;

                        // Praveen : Voucher Integration
                        if (PurchaseData.Equals(AssetInsurance.Purchase) || PurchaseData.Equals(AssetInsurance.InKind))
                        {
                            if (!AssetMultiInsuranceVoucherCollection.ContainsKey(Inskey))
                                AssetMultiInsuranceVoucherCollection.Add(Inskey, InsVoucherValue);
                            else
                                AssetMultiInsuranceVoucherCollection[Inskey] = InsVoucherValue;
                        }
                        else
                        {
                            AssetMultiInsuranceVoucherCollection.Clear();
                        }
                        Dialogresult = DialogResult.OK;
                        this.Close();
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.ToString());
            }
            finally { }
        }

        /// <summary>
        /// Get ItemDetail Id Details
        /// </summary>
        /// <param name="AID"></param>
        /// <returns></returns>
        private int GetId(string AID)
        {
            int ItemDetailId = 0;
            using (InsuranceRenewSystem InsRenSystem = new InsuranceRenewSystem())
            {
                ItemDetailId = InsRenSystem.LoadRenewalDetailByAssetId(AID);
            }
            return ItemDetailId;
        }

        /// <summary>
        /// To update the details to controls
        /// </summary>
        private void UpdateDataTableValues()
        {
            DataTable retriveData = null;
            DataTable retriveVoucherData = null;
            try
            {
                var Inskey = new Tuple<int, int>(ItemId, RowNo);
                if (AssetMultiInsuranceCollection.ContainsKey(Inskey) && AssetMultiInsuranceVoucherCollection.ContainsKey(Inskey))
                {
                    retriveData = AssetMultiInsuranceCollection[Inskey];
                    retriveVoucherData = AssetMultiInsuranceVoucherCollection[Inskey];
                    ucAssetJournal1.AssignCashBankDetails(retriveVoucherData);
                    if (retriveData != null && retriveData.Rows.Count > 0)
                    {
                        lblAssetItemData.Text = retriveData.Rows[0][AppSchemas.ASSETItem.ASSET_ITEMColumn.ColumnName].ToString();
                        lblAssetIdData.Text = retriveData.Rows[0][AppSchemas.ASSETItem.ASSET_IDColumn.ColumnName].ToString();
                        glkpInsPlan.EditValue = retriveData.Rows[0]["INSURANCE_PLAN_ID"].ToString();
                        txtPolicyNo.Text = retriveData.Rows[0][AppSchemas.ASSETItem.POLICY_NOColumn.ColumnName].ToString();
                        dtRegRenewalDate.DateTime = this.UtilityMember.DateSet.ToDate(retriveData.Rows[0][AppSchemas.AssetInsuranceDetail.RENEWAL_DATEColumn.ColumnName].ToString(), false);
                        txtSumInsured.Text = retriveData.Rows[0][AppSchemas.AssetInsuranceDetail.SUM_INSUREDColumn.ColumnName].ToString();
                        txtPremiumAmt.Text = retriveData.Rows[0][AppSchemas.AssetInsuranceDetail.PREMIUM_AMOUNTColumn.ColumnName].ToString();
                        dtPeriodFrom.DateTime = this.UtilityMember.DateSet.ToDate(retriveData.Rows[0][AppSchemas.AssetInsuranceDetail.PERIOD_FROMColumn.ColumnName].ToString(), false);
                        dtPeriodTo.DateTime = this.UtilityMember.DateSet.ToDate(retriveData.Rows[0][AppSchemas.AssetInsuranceDetail.PERIOD_TOColumn.ColumnName].ToString(), false);

                        Projectid = this.UtilityMember.NumberSet.ToInteger(retriveData.Rows[0][AppSchemas.Project.PROJECT_IDColumn.ColumnName].ToString());
                        txtNarration.Text = retriveData.Rows[0][AppSchemas.VoucherMaster.NARRATIONColumn.ColumnName].ToString();
                        lblVoucherNo.Text = retriveData.Rows[0][AppSchemas.VoucherMaster.VOUCHER_NOColumn.ColumnName].ToString();
                        VoucherId = this.UtilityMember.NumberSet.ToInteger(retriveData.Rows[0][AppSchemas.AssetInsuranceDetail.VOUCHER_IDColumn.ColumnName].ToString());
                    }
                }
                else
                {
                    txtSumInsured.Text = AssetIdAmount.ToString();
                }

            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }

        /// <summary>
        /// Get Renew Details
        /// </summary>
        /// <param name="itemdetailId"></param>
        /// <returns></returns>
        private DataTable LoadInsuranceDetailsFromDB(int itemdetailId)
        {
            DataTable insRenewDetails = null;
            using (InsuranceRenewSystem renewSystem = new InsuranceRenewSystem())
            {
                resultArgs = renewSystem.LoadHistoryDetailsById(itemdetailId.ToString());
                insRenewDetails = resultArgs.DataSource.Table;
            }
            return insRenewDetails;
        }

        /// <summary>
        /// Border color Sum Insured
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtSumInsured_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtSumInsured);
        }

        /// <summary>
        /// Border color Premium Amount
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPremiumAmt_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtPremiumAmt);
            CalculateFirstRowValue();
        }

        /// <summary>
        /// Border color Policy No details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void txtPolicyNo_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(txtPolicyNo);
        }

        /// <summary>
        /// Border color while empty
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtPeriodFrom_Leave(object sender, EventArgs e)
        {
            //this.SetBorderColor(dtPeriodFrom);
            //DateTime date = dtPeriodFrom.DateTime;
            //dtPeriodTo.DateTime = date.AddYears(1).AddDays(-1);
        }

        /// <summary>
        /// Border color while empty
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtPeriodTo_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(dtPeriodTo);
        }

        /// <summary>
        /// while focusing the grid the record has to be loaded
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void glkpInsPlan_Properties_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            try
            {
                if (e.Button.Kind == ButtonPredefines.Plus)
                {
                    LoadInsuranceCombo();

                    //frmInsuranceAddPlan frmInsurance = new frmInsuranceAddPlan();
                    //frmInsurance.ShowDialog();
                    //LoadInsurancePlan();
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        /// <summary>
        /// Delete the Renew details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ripeDeleteInsuranceDetails_Click(object sender, EventArgs e)
        {
            try
            {
                using (AssetItemSystem assetitemSystem = new AssetItemSystem())
                {
                    int InsDeleteDetailId = gvInsurance.GetFocusedRowCellValue(colInsuranceDetailId) != null ? this.UtilityMember.NumberSet.ToInteger(gvInsurance.GetFocusedRowCellValue(colInsuranceDetailId).ToString()) : 0;
                    DeleteInsHistoryDetail(InsDeleteDetailId);
                    if (UpdateHeld != null)
                    {
                        UpdateHeld(this, e);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.ToString());
            }
        }

        /// <summary>
        /// Edit the Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rpbeInsuranceDetails_Click(object sender, EventArgs e)
        {
            int InsEditDetailId = gvInsurance.GetFocusedRowCellValue(colInsuranceDetailId) != null ? this.UtilityMember.NumberSet.ToInteger(gvInsurance.GetFocusedRowCellValue(colInsuranceDetailId).ToString()) : 0;
            if (InsEditDetailId > 0)
            {
                InsuranceDetailId = InsEditDetailId;
                mode = (int)AssetInsurance.Update;
                AssignValueToControls();
                FinanceIntegrationDefaults();
                SetDefaults();
            }
        }

        /// <summary>
        /// This is to get the one year value
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void dtRegRenewalDate_EditValueChanged(object sender, EventArgs e)
        {
            //if (dtRegRenewalDate.EditValue != null)
            //{
            //    dtPeriodFrom.DateTime = (dtPeriodFrom.Properties.MinValue < dtRegRenewalDate.DateTime.AddDays(1)) ? dtPeriodFrom.Properties.MinValue : dtRegRenewalDate.DateTime.AddDays(1);
            //}
            LoadVoucherNo();
        }
        /// <summary>
        /// close the Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }



        #endregion

        #region Methods
        /// <summary>
        /// load insurace details
        /// </summary>
        private void LoadInsurancePlan()
        {
            try
            {
                using (InsuranceRenewSystem insuranceSystem = new InsuranceRenewSystem())
                {
                    resultArgs = insuranceSystem.FetchInsurancePlan();
                    if (resultArgs != null && resultArgs.Success)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpInsPlan, resultArgs.DataSource.Table, insuranceSystem.AppSchema.InsurancePlan.INSURANCE_PLANColumn.ColumnName, insuranceSystem.AppSchema.InsurancePlan.INSURANCE_PLAN_IDColumn.ColumnName);
                        glkpInsPlan.EditValue = glkpInsPlan.Properties.GetKeyValue(0);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        /// <summary>
        /// load imediate added insurance plan deatails in to the combo
        /// </summary>

        public void LoadInsuranceCombo()
        {
            if (this.AppSetting.LockMasters == (int)YesNo.No)
            {
                frmInsuranceAddPlan frmInsuranceAddPlan = new frmInsuranceAddPlan();
                frmInsuranceAddPlan.ShowDialog();
                if (frmInsuranceAddPlan.ReturnDialog == System.Windows.Forms.DialogResult.OK)
                {
                    LoadInsurancePlan();
                    if (frmInsuranceAddPlan.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmInsuranceAddPlan.ReturnValue.ToString()) > 0)
                    {
                        glkpInsPlan.EditValue = this.UtilityMember.NumberSet.ToInteger(frmInsuranceAddPlan.ReturnValue.ToString());
                    }
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
            }
        }


        /// <summary>
        /// Load insurance
        /// </summary>
        private void LoadInsurance()
        {
            try
            {
                using (InsuranceRenewSystem renew = new InsuranceRenewSystem())
                {
                    resultArgs = renew.LoadHistoryDetailsById(ItemDetailId.ToString());
                    if (resultArgs != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        if (!(mode == (int)AssetInsurance.Update))
                        {
                            dtRenewDate = UtilityMember.DateSet.ToDate
                                (resultArgs.DataSource.Table.Rows[resultArgs.DataSource.Table.Rows.Count - 1][AppSchemas.AssetInsuranceDetail.PERIOD_TOColumn.ColumnName].ToString(), false);
                            //dtRegRenewalDate.DateTime = dtPeriodFrom.Properties.MinValue =
                            //    dtPeriodTo.Properties.MinValue =  dtRenewDate;
                        }
                        gcInsurance.DataSource = resultArgs.DataSource.Table;
                        gcInsurance.RefreshDataSource();
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }

        }

        private ResultArgs FetchPreviousRenewal()
        {
            try
            {
                using (InsuranceRenewSystem insuranceSystem = new InsuranceRenewSystem())
                {
                    insuranceSystem.ItemDetailId = ItemDetailId;
                    insuranceSystem.InsDetailId = InsuranceDetailId;

                    resultArgs = insuranceSystem.FetchPreviousRenewal();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            return resultArgs;
        }

        private ResultArgs FetchNextRenewal()
        {
            try
            {
                using (InsuranceRenewSystem insuranceSystem = new InsuranceRenewSystem())
                {
                    insuranceSystem.ItemDetailId = ItemDetailId;
                    insuranceSystem.InsDetailId = InsuranceDetailId;

                    resultArgs = insuranceSystem.FetchNextRenewal();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            return resultArgs;
        }

        /// <summary>
        /// Load the Asset Details
        /// </summary>
        private void DefaultLoad()
        {
            Size = new System.Drawing.Size(this.Width, this.Height + 10);
            lblAssetItemData.Text = AssetItem;
            lblAssetIdData.Text = AssetId;
            if (!PurchaseData.Equals(AssetInsurance.Purchase) && !PurchaseData.Equals(AssetInsurance.Opening) && !PurchaseData.Equals(AssetInsurance.InKind))
            {
                if (mode == (int)AssetInsurance.Create)
                {
                    lcgInsRenewDetails.Text = "Insurance Details";
                    this.Text = "Create Insurance";
                    lblRenewalDate.Text = "Registration Date";
                    HideRenewDetails();
                }
                if (mode == (int)AssetInsurance.Update)
                {
                    lcgInsRenewDetails.Text = "Insurance Details";
                    this.Text = "Renewal(Edit)";
                    lblRenewalDate.Text = "Registration Date";

                }
                if (string.IsNullOrEmpty(AssetItem) && string.IsNullOrEmpty(AssetId))
                {
                    lblAssetItemData.Text = " ";
                    lblAssetIdData.Text = " ";
                }

            }
            else
            {
                lcgInsRenewDetails.Text = "Insurance Details";
                this.Text = "Insurance";
                lblRenewalDate.Text = "Registration Date";
                HideRenewDetails();

                if (string.IsNullOrEmpty(AssetItem) && string.IsNullOrEmpty(AssetId))
                {
                    lblAssetItemData.Text = " ";
                    lblAssetIdData.Text = " ";
                }
            }
        }

        /// <summary>
        /// Delete the History Details();
        /// </summary>
        private void DeleteInsHistoryDetail(int RenewInsId)
        {
            try
            {
                using (InsuranceRenewSystem RenewSystem = new InsuranceRenewSystem())
                {
                    if (gvInsurance.RowCount > 0)
                    {
                        if (gvInsurance.FocusedRowHandle != GridControl.NewItemRowHandle)
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                gvInsurance.DeleteRow(gvInsurance.FocusedRowHandle);
                                gvInsurance.UpdateCurrentRow();

                                resultArgs = RenewSystem.DeleteInsuranceVoucherByInsId(RenewInsId);
                                if (resultArgs != null && resultArgs.Success)
                                {
                                    resultArgs = RenewSystem.DeleteInsuranceDetails(RenewInsId);
                                    LoadInsurance();
                                    this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETED_CONFIRMATION));
                                    InsuranceDetailId = VoucherId = 0;
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
        }

        /// <summary>
        /// Hide the History Details while Creating Insurance
        /// </summary>
        private void HideRenewDetails()
        {
            lcgRenewalHistory.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            Size = new System.Drawing.Size(this.Width, 480);
            glkpInsPlan.Properties.PopupFormSize = new System.Drawing.Size(530, 280);
            this.CenterToScreen();
        }

        private void HideVoucherRenewDetails()
        {
            lcgLedgerDetails.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            Size = new System.Drawing.Size(this.Width, 285);
            this.CenterToScreen();
            lblVoucherCaption.Text = lblVoucherNo.Text = "  ";
        }

        /// <summary>
        /// Validate Renew Details
        /// </summary>
        /// <returns></returns>
        private bool ValidateRenewDetails()
        {
            bool isRenewTrue = true;
            if (string.IsNullOrEmpty(glkpInsPlan.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.InsuranceRenew.INSURANCE_PLAN_EMPTY));
                isRenewTrue = false;
                this.SetBorderColorForGridLookUpEdit(glkpInsPlan);
                glkpInsPlan.Focus();
            }
            else if (string.IsNullOrEmpty(txtPolicyNo.Text))
            {
                this.ShowMessageBox("Policy No is empty");
                isRenewTrue = false;
                this.SetBorderColor(txtPolicyNo);
                txtPolicyNo.Focus();
            }
            else if (string.IsNullOrEmpty(txtSumInsured.Text) || (this.UtilityMember.NumberSet.ToDouble(txtSumInsured.Text) == 0.0))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.InsuranceRenew.INSURANCE_SUM_INSURCED_EMPTY));
                isRenewTrue = false;
                this.SetBorderColor(txtSumInsured);
                txtSumInsured.Focus();
            }
            else if (string.IsNullOrEmpty(txtPremiumAmt.Text) || (this.UtilityMember.NumberSet.ToDouble(txtPremiumAmt.Text) == 0.0))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.InsuranceRenew.INSURANCE_PRIMIUM_AMOUNT_EMPTY));
                isRenewTrue = false;
                this.SetBorderColor(txtPremiumAmt);
                txtPremiumAmt.Focus();
            }
            else if (string.IsNullOrEmpty(dtPeriodFrom.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.InsuranceRenew.INSURANCE_PERION_FROM_EMPTY));
                isRenewTrue = false;
                this.SetBorderColor(dtPeriodFrom);
                dtPeriodFrom.Focus();
            }
            else if (string.IsNullOrEmpty(dtPeriodTo.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.InsuranceRenew.INSURANCE_PERION_TO_EMPTY));
                isRenewTrue = false;
                this.SetBorderColor(dtPeriodTo);
                dtPeriodTo.Focus();
            }
            else if (dtPeriodTo.DateTime != DateTime.MinValue)
            {
                if (!this.UtilityMember.DateSet.ValidateDate(dtPeriodFrom.DateTime, dtPeriodTo.DateTime))
                {
                    ShowMessageBox(this.GetMessage(MessageCatalog.Asset.InsuranceRenew.INSURANCE_PERIOD_FROM_AND_TO_VALIDATION));
                    isRenewTrue = false;
                    dtPeriodTo.Focus();
                }
            }
            else if (ucAssetJournal1.IsValidBankGrid())
            {
                isRenewTrue = false;
                ucAssetJournal1.Focus();
            }
            return isRenewTrue;
        }

        /// <summary>
        /// Assign value to Controls
        /// </summary>
        private void AssignValueToControls()
        {
            if (!PurchaseData.Equals(AssetInsurance.Purchase) && !PurchaseData.Equals(AssetInsurance.Opening) && !PurchaseData.Equals(AssetInsurance.InKind))
            {
                if (mode == (int)AssetInsurance.Create || mode == (int)AssetInsurance.Update)
                {
                    if (InsuranceDetailId > 0 && ItemDetailId > 0)
                    {
                        using (InsuranceRenewSystem RenewSystem = new InsuranceRenewSystem(InsuranceDetailId))
                        {
                            RenewSystem.InsDetailId = InsuranceDetailId;
                            RenewSystem.ItemDetailId = ItemDetailId;
                            dtRegRenewalDate.DateTime = RenewSystem.RenewalDate;
                            txtPolicyNo.Text = RenewSystem.PolicyNo;
                            glkpInsPlan.EditValue = RenewSystem.InsurancePlanId.ToString();
                            txtSumInsured.Text = RenewSystem.SumInsured.ToString();
                            txtPremiumAmt.Text = RenewSystem.PremiumAmount.ToString();
                            dtPeriodFrom.DateTime = RenewSystem.PeriodFrom;
                            dtPeriodTo.DateTime = RenewSystem.PeriodTo;
                            lblProject.Text = RenewSystem.Project;
                            VoucherId = RenewSystem.VoucherId;
                        }
                    }
                    else if (mode == (int)AssetInsurance.Create && InsuranceDetailId == 0)
                    {
                        lblProject.Text = ProjectName;
                    }
                }
                else if (mode == (int)AssetInsurance.Renew)
                {
                    if (InsuranceDetailId > 0 && ItemDetailId > 0)
                    {
                        using (InsuranceRenewSystem RenewSystem = new InsuranceRenewSystem(InsuranceDetailId))
                        {
                            RenewSystem.InsDetailId = InsuranceDetailId;
                            RenewSystem.ItemDetailId = ItemDetailId;
                            dtRegRenewalDate.DateTime = RenewSystem.RenewalDate;
                            txtPolicyNo.Text = RenewSystem.PolicyNo;
                            glkpInsPlan.EditValue = RenewSystem.InsurancePlanId.ToString();
                            //  txtSumInsured.Text = this.UtilityMember.NumberSet.ToDecimal("0.00").ToString();
                            txtSumInsured.Text = RenewSystem.SumInsured.ToString();
                            //   txtPremiumAmt.Text = this.UtilityMember.NumberSet.ToDecimal("0.00").ToString();
                            txtPremiumAmt.Text = RenewSystem.PremiumAmount.ToString();
                            dtPeriodFrom.DateTime = RenewSystem.PeriodFrom;
                            dtPeriodTo.DateTime = RenewSystem.PeriodTo;
                            lblProject.Text = RenewSystem.Project;
                        }
                    }
                }
            }
            else
            {
                if (ItemDetailId > 0)
                {
                    using (InsuranceRenewSystem RenewSystem = new InsuranceRenewSystem(ItemDetailId, true))
                    {
                        RenewSystem.InsDetailId = InsuranceDetailId;
                        RenewSystem.ItemDetailId = ItemDetailId;
                        //if (RenewSystem.RenewalDate > dtRegRenewalDate.Properties.MinValue)
                        //    dtRegRenewalDate.DateTime = RenewSystem.RenewalDate;

                        //if (RenewSystem.PeriodFrom > dtPeriodFrom.Properties.MinValue)
                        //    dtPeriodFrom.DateTime = RenewSystem.PeriodFrom;

                        //if (RenewSystem.PeriodTo > dtPeriodTo.Properties.MinValue)
                        //    dtPeriodTo.DateTime = RenewSystem.PeriodTo;

                        txtPolicyNo.Text = RenewSystem.PolicyNo;
                        glkpInsPlan.EditValue = RenewSystem.InsurancePlanId.ToString();
                        if (this.UtilityMember.NumberSet.ToInteger(glkpInsPlan.EditValue.ToString()) == 0)
                            glkpInsPlan.EditValue = 1;

                        txtSumInsured.Text = RenewSystem.SumInsured.ToString();
                        txtPremiumAmt.Text = RenewSystem.PremiumAmount.ToString();
                        lblProject.Text = RenewSystem.Project;
                        VoucherId = RenewSystem.VoucherId;
                    }
                }
            }
        }

        /// <summary>
        /// Clear controls
        /// </summary>
        private void ClearControls()
        {
            if (mode == (int)AssetInsurance.Create || mode == (int)AssetInsurance.Renew)
            {
                //  txtPremiumAmt.Text = string.Empty;
                dtRegRenewalDate.DateTime = dtPeriodFrom.DateTime = dtPeriodTo.DateTime = this.UtilityMember.DateSet.ToDate(DateTime.Now.ToString(), false);
                this.glkpInsPlan.Focus();
            }
        }
        #endregion


        private void CalculateFirstRowValue()
        {
            try
            {
                double LedgerAmount = this.UtilityMember.NumberSet.ToDouble(txtPremiumAmt.Text);
                if (LedgerAmount >= 0 && LedgerAmount != BankTransSummaryVal && VoucherId >= 0)
                {
                    //  ucAssetJournal1.EnableCashBankGrid = true;
                    ucAssetJournal1.Focus();
                    double Amount = ucAssetJournal1.BankAmount;//asset.le
                    if (Amount >= 0)
                    {
                        double dAmount = 0.0;
                        if (BankTransSummaryVal <= LedgerAmount)
                        {
                            dAmount = Math.Abs((LedgerAmount - BankTransSummaryVal) + Amount);
                        }
                        else if (BankTransSummaryVal >= LedgerAmount)
                        {
                            dAmount = Math.Abs(Amount - (BankTransSummaryVal - LedgerAmount));
                        }
                        if (dAmount >= 0)
                        {
                            ucAssetJournal1.Flag = AssetInOut.INS;
                            ucAssetJournal1.ConstructCashTransEmptySournce();
                            ucAssetJournal1.SetCashLedger(LedgerAmount);
                            ucAssetJournal1.SetExpenseLedger(LedgerAmount);
                            ucAssetJournal1.PurchaseTransSummary = LedgerAmount;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
        }

        private void ShowRenewalDate()
        {
            lblReneDate.Visibility = lblRenewDate.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Always;
            if (mode == (int)AssetInsurance.Renew)
            {
                //lblReneDate.Text = "Registration Date :";
                lblReneDate.Text = this.GetMessage(MessageCatalog.Asset.InsuranceRenew.INSURANCE_REGISTRATION_DATE);
                lblRenewDate.Text = AssetRegistrationDate;
            }
            else
            {
                lblReneDate.Text = lblRenewDate.Text = "  ";

            }
        }
        private void AssignSumInsuredAmount()
        {
            if (mode == (int)AssetInsurance.Create)
            {
                txtSumInsured.Text = Amount;
            }
        }

        private void dtPeriodFrom_EditValueChanged(object sender, EventArgs e)
        {
            //if (dtPeriodFrom.EditValue != null)
            //{
            //     dtPeriodTo.DateTime = (dtPeriodTo.Properties.MaxValue < dtPeriodFrom.DateTime.AddYears(1)) ? dtPeriodTo.Properties.MaxValue : dtPeriodFrom.DateTime.AddYears(1);
            //}
        }
    }
}