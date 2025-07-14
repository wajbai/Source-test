using System;
using System.Data;
using System.Linq;
using DevExpress.XtraEditors.Controls;
using System.Globalization;

using Bosco.Utility;
using Bosco.Model;
using Bosco.Model.UIModel;
using Bosco.Model.Setting;

using System.Windows.Forms;

namespace ACPP.Modules
{
    public partial class frmAssetSettings : frmFinanceBaseAdd
    {
        #region Declaration
        DataTable dtSetting = null;
        DataView dtSettings = new DataView();
        ResultArgs resultArgs = new ResultArgs();
        DataTable dtUISetting = null;
        string assetledid = string.Empty;
        bool isDeleteImport = true;
        #endregion

        #region Constructors

        public frmAssetSettings()
        {
            InitializeComponent();
        }

        #endregion

        #region Events
        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                if (ValidateMappingLedgers())
                {
                    ISetting isetting;
                    SaveAssetSetting();
                    ValidateMappingLedgers();
                    if (this.UtilityMember.NumberSet.ToInteger(LoginUser.LoginUserId) == 1)
                    {
                        isetting = new GlobalSetting();
                        resultArgs = isetting.SaveSetting(dtSetting);
                    }
                    else
                    {
                        isetting = new UISetting();
                        resultArgs = isetting.SaveSetting(dtSetting);
                    }
                    if (resultArgs.Success)
                    {
                        ApplySetting();
                        //this.ShowSuccessMessage("Saved");
                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Asset.AssetItemLedgerMapping.ASSET_SETTING_SUCCESS_INFO));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frmAssetSettings_Load(object sender, EventArgs e)
        {
            HideLedgerDetails();
            LoadAssetLedgers();
            LoadExpenseLedgers();

            //IsLedgerCreationAllowed();
            LoadMonths();
            ApplySetting();
            BindValues();
            //   layoutControlItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            //  layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;

            // 16.09.2022
            layoutControlGroup3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
        }

        private void glkpAccountLedger_Leave(object sender, EventArgs e)
        {
            this.SetBorderColor(glkpAccountLedger);
        }

        private void glkpDepLedger_Leave(object sender, EventArgs e)
        {
            //   this.SetBorderColor(glkpDepLedger);
        }

        private void glkpDisposalLedger_Leave(object sender, EventArgs e)
        {
            // this.SetBorderColor(glkpDisposalLedger);
        }

        private void glkpAccountLedger_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                if (this.AppSetting.LockMasters == (int)YesNo.No)
                {
                    ACPP.Modules.Master.frmLedgerDetailAdd frmLedgerDetailAdd = new ACPP.Modules.Master.frmLedgerDetailAdd((int)AddNewRow.NewRow, ledgerSubType.GN);
                    frmLedgerDetailAdd.ShowDialog();
                    if (frmLedgerDetailAdd.ReturnDialog == System.Windows.Forms.DialogResult.OK)
                    {
                        LoadAssetLedgers();
                        // LoadExpenseLedgers();
                        LoadAssetLedgers();
                        if (frmLedgerDetailAdd.ReturnValue != null && this.UtilityMember.NumberSet.ToInteger(frmLedgerDetailAdd.ReturnValue.ToString()) > 0)
                        {
                            glkpAccountLedger.EditValue = this.UtilityMember.NumberSet.ToInteger(frmLedgerDetailAdd.ReturnValue.ToString());
                        }
                    }
                }
                else
                {
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.LOCK_MASTERS_MESSAGE));
                }
            }
        }

        private void glkpDepLedger_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                ACPP.Modules.Master.frmLedgerDetailAdd frmBank = new ACPP.Modules.Master.frmLedgerDetailAdd((int)AddNewRow.NewRow, ledgerSubType.GN, 0);
                frmBank.ShowDialog();
                LoadExpenseLedgers();
                LoadAssetLedgers();
            }
        }

        private void glkpDisposalLedger_ButtonClick(object sender, ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                ACPP.Modules.Master.frmLedgerDetailAdd frmBank = new ACPP.Modules.Master.frmLedgerDetailAdd((int)AddNewRow.NewRow, ledgerSubType.GN, 0);
                frmBank.ShowDialog();
                LoadExpenseLedgers();
                LoadAssetLedgers();
            }
        }
        #endregion

        #region Methods
        private void BindValues()
        {
            // this.glkpMonths.Text = this.AppSetting.Months;
            //this.glkpAccountLedger.EditValue = this.AppSetting.AccountLedgerId;
            //this.glkpDepLedger.EditValue = this.AppSetting.DepreciationLedgerId;
            //this.glkpDisposalLedger.EditValue = this.AppSetting.DisposalLedgerId;
            //this.txtShowAMCAlert.Text = this.AppSetting.ShowAMCRenewalAlert;
            //this.txtShowAMCInsuranceAlert.Text = this.AppSetting.ShowInsuranceAlert;
            //this.rgDepr.SelectedIndex = this.UtilityMember.NumberSet.ToInteger(AppSetting.ShowDepr.ToString());

            //if (this.UtilityMember.NumberSet.ToInteger(LoginUser.LoginUserId) != 1)
            //{
            //    this.glkpMonths.Text = this.UIAppSetting.Months;
            //    this.glkpAccountLedger.EditValue = this.UIAppSetting.AccountLedgerId;
            //    this.glkpDepLedger.EditValue = this.UIAppSetting.DepreciationLedgerId;
            //    this.glkpDisposalLedger.EditValue = this.UIAppSetting.DisposalLedgerId;
            //    this.txtShowAMCAlert.Text = this.UIAppSetting.ShowAMCRenewalAlert;
            //    this.txtShowAMCInsuranceAlert.Text = this.UIAppSetting.ShowInsuranceAlert;
            //    this.rgDepr.SelectedIndex = this.UIAppSetting.NumberSet.ToInteger(rgDepr.SelectedIndex.ToString());
            //}

            this.rgDepr.SelectedIndex = this.UtilityMember.NumberSet.ToInteger(this.AppSetting.ShowDepr);
            string opBalDate = this.AppSetting.BookBeginFrom;
            if (string.IsNullOrEmpty(this.AppSetting.ShowOpApplyFrom)) //  || (this.UtilityMember.DateSet.ToDate(this.AppSetting.ShowOpApplyFrom, false) == DateTime.Parse(opBalDate).AddDays(-1))
            {
                //this.dtOPApplyFrom.DateTime = DateTime.Parse(opBalDate).AddDays(-1);
                this.dtOPApplyFrom.DateTime = DateTime.Parse(opBalDate);
            }
            else if (ValidateDepreciation() == 0)
            {
                this.dtOPApplyFrom.DateTime = this.UtilityMember.DateSet.ToDate(this.AppSetting.ShowOpApplyFrom, false);
                this.dtOPApplyFrom.Enabled = true;
            }
            else if (this.UtilityMember.DateSet.ToDate(this.AppSetting.ShowOpApplyFrom, false) != DateTime.Parse(opBalDate))
            {
                this.dtOPApplyFrom.DateTime = this.UtilityMember.DateSet.ToDate(this.AppSetting.ShowOpApplyFrom, false);
                this.dtOPApplyFrom.Enabled = false;
            }
            else
            {
                this.dtOPApplyFrom.DateTime = this.UtilityMember.DateSet.ToDate(this.AppSetting.ShowOpApplyFrom, false);
            }
        }

        public int ValidateDepreciation()
        {
            int validateId = 0;
            using (AssetDepreciation depr = new AssetDepreciation())
            {
                validateId = depr.FetchDepreciationExist();
            }
            return validateId;
        }

        private bool ValidateMappingLedgers()
        {
            bool isLedgerTrue = true;
            // CHINNA COMMANDED 
            if (string.IsNullOrEmpty(glkpAccountLedger.Text))
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSET_ACCOUNT_LEDGER));
                this.SetBorderColor(glkpAccountLedger);
                isLedgerTrue = false;
                this.glkpAccountLedger.Focus();
            }
            //else if (this.UtilityMember.NumberSet.ToInteger(txtShowAMCAlert.Text.ToString()) == 0 || string.IsNullOrEmpty(txtShowAMCAlert.Text))
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSET_AMC_RENEWAL_ALERT));
            //    this.SetBorderColor(txtShowAMCAlert);
            //    isLedgerTrue = false;
            //    this.txtShowAMCAlert.Focus();
            //}
            //else if (this.UtilityMember.NumberSet.ToInteger(txtShowAMCInsuranceAlert.Text.ToString()) == 0 || string.IsNullOrEmpty(txtShowAMCInsuranceAlert.Text))
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSET_INSURANCE_ALERT));
            //    this.SetBorderColor(txtShowAMCInsuranceAlert);
            //    isLedgerTrue = false;
            //    this.txtShowAMCInsuranceAlert.Focus();
            //}


            // -----------------------------------------------------ALREADY COMMANDED
            //else if (string.IsNullOrEmpty(glkpDepLedger.Text))
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSET_DEPRECIATION_LEDGER));
            //    this.SetBorderColor(glkpDepLedger);
            //    isLedgerTrue = false;
            //    this.glkpDepLedger.Focus();
            //}
            //else if (string.IsNullOrEmpty(glkpDisposalLedger.Text))
            //{
            //    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSET_DISPOSAL_LEDGER));
            //    this.SetBorderColor(glkpDisposalLedger);
            //    isLedgerTrue = false;
            //    this.glkpDisposalLedger.Focus();
            //}
            //----------------------------------------------------------

            return isLedgerTrue;
        }
        // ----------------------------------
        //private void IsLedgerCreationAllowed()
        //{
        //    if (this.AppSetting.LockMasters == (int)YesNo.Yes)
        //    {
        //        glkpMonths.Properties.Buttons[1].Visible = false;
        //        glkpAccountLedger.Properties.Buttons[1].Visible = false;
        //        glkpDepLedger.Properties.Buttons[1].Visible = false;
        //        glkpDisposalLedger.Properties.Buttons[1].Visible = false;
        //    }
        //}
        // ------------------------------------


        private void HideLedgerDetails()
        {
            //this.Height = 200;
            this.CenterToScreen();
        }
        private void LoadMonths()
        {
            try
            {
                var Month = DateTimeFormatInfo.InvariantInfo.MonthNames.TakeWhile(m => m != string.Empty).ToList();
                // glkpMonths.Properties.DataSource = Month;
                //   glkpMonths.EditValue = -1;
                //glkpMonths.Properties.GetKeyValue(8);
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message);
            }
        }

        //private ResultArgs ConstructAssetLedgers()
        //{
        //    DataView dvAssetLedgers = null;
        //    DataTable dtAssetLedgers = null;
        //    try
        //    {
        //        AssetLedgerType assetLedgerType = new AssetLedgerType();
        //        dvAssetLedgers = this.UtilityMember.EnumSet.GetEnumDataSource(assetLedgerType, Sorting.Ascending);
        //        dtAssetLedgers = dvAssetLedgers.ToTable();
        //        if (dtAssetLedgers != null)
        //        {
        //            dtAssetLedgers.Columns.Add("VALUE", typeof(int));
        //            for (int i = 0; i < dtAssetLedgers.Rows.Count; i++)
        //            {
        //                AssetLedgerType assetLedgerName = (AssetLedgerType)Enum.Parse(typeof(AssetLedgerType), dtAssetLedgers.Rows[i][1].ToString());
        //                int LedgerId = 0;
        //                switch (assetLedgerName)
        //                {
        //                    case AssetLedgerType.AccountLedger:
        //                        {
        //                            LedgerId = (glkpAccountLedger.EditValue != null) ? this.UtilityMember.NumberSet.ToInteger(glkpAccountLedger.EditValue.ToString()) : 0;
        //                            break;
        //                        }
        //                    case AssetLedgerType.DepreciationLedger:
        //                        {
        //                            LedgerId = (glkpDepLedger.EditValue != null) ? this.UtilityMember.NumberSet.ToInteger(glkpDepLedger.EditValue.ToString()) : 0;
        //                            break;
        //                        }
        //                    case AssetLedgerType.DisposalLedger:
        //                        {
        //                            LedgerId = (glkpDisposalLedger.EditValue != null) ? this.UtilityMember.NumberSet.ToInteger(glkpDisposalLedger.EditValue.ToString()) : 0;
        //                            break;
        //                        }
        //                    case AssetLedgerType.DepreciationMonth:
        //                        {
        //                            //Month = glkpMonths.Text;
        //                            break;
        //                        }
        //                }
        //                dtAssetLedgers.Rows[i][2] = LedgerId;
        //            }
        //        }
        //        else
        //        {
        //            this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        resultArgs.Message = ex.ToString();
        //    }

        //    if (resultArgs.Success)
        //    {
        //        resultArgs.DataSource.Data = dtAssetLedgers;
        //    }
        //    return resultArgs;
        //}

        private void LoadAssetLedgers()
        {
            using (LedgerSystem ledgerSystem = new LedgerSystem())
            {
                ledgerSystem.GroupId = (int)Natures.Assert;
                resultArgs = ledgerSystem.FetchLedgerByNature();
                DataView dvAccountLedger = new DataView(resultArgs.DataSource.Table);
                dvAccountLedger.RowFilter = "(GROUP_ID <> 13)";
                if (dvAccountLedger != null && resultArgs.Success)
                {
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpAccountLedger, dvAccountLedger.ToTable(),
                       ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                    glkpAccountLedger.EditValue = glkpAccountLedger.Properties.GetKeyValue(0);
                }
            }
        }

        private void LoadExpenseLedgers()
        {
            using (LedgerSystem ledgerSystem = new LedgerSystem())
            {
                ledgerSystem.GroupId = (int)Natures.Expenses;
                resultArgs = ledgerSystem.FetchLedgerByNature();

                if (resultArgs != null && resultArgs.Success)
                {
                    //this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpDepLedger, resultArgs.DataSource.Table,
                    //    ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);

                    //this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpDisposalLedger, resultArgs.DataSource.Table,
                    //    ledgerSystem.AppSchema.Ledger.LEDGER_NAMEColumn.ColumnName, ledgerSystem.AppSchema.Ledger.LEDGER_IDColumn.ColumnName);
                }
            }
        }

        private void SaveAssetSetting()
        {
            AssetSetting assetSetting = new AssetSetting();
            DataView dvSetting = null;
            dvSetting = this.UtilityMember.EnumSet.GetEnumDataSource(assetSetting, Sorting.Ascending);
            dtSetting = dvSetting.ToTable();
            if (dtSetting != null)
            {
                dtSetting.Columns.Add("Value", typeof(string));
                dtSetting.Columns.Add("UserId", typeof(string));
                for (int i = 0; i < dtSetting.Rows.Count; i++)
                {
                    AssetSetting SettingName = (AssetSetting)Enum.Parse(typeof(AssetSetting), dtSetting.Rows[i][1].ToString());
                    string Value = "";
                    switch (SettingName)
                    {
                        case AssetSetting.Months:
                            {
                                // Value = glkpMonths.EditValue != null ? glkpMonths.Text.ToString() : string.Empty;
                                break;
                            }
                        case AssetSetting.AccountLedgerId:
                            {
                                Value = glkpAccountLedger.EditValue != null ? glkpAccountLedger.EditValue.ToString() : string.Empty;
                                break;
                            }
                        case AssetSetting.DepreciationLedgerId:
                            {
                                // Value = glkpDepLedger.EditValue != null ? glkpDepLedger.EditValue.ToString() : string.Empty;
                                break;
                            }
                        case AssetSetting.DisposalLedgerId:
                            {
                                // Value = glkpDisposalLedger.EditValue != null ? glkpDisposalLedger.EditValue.ToString() : string.Empty;
                                break;
                            }
                        case AssetSetting.ShowAMCRenewalAlert:
                            {
                                //  Value = txtShowAMCAlert.Text;
                                //Value = txtShowAMCAlert.Text != null ? txtShowAMCAlert.ToString() : "";
                                break;
                            }
                        //sudhakar
                        case AssetSetting.ShowDepr:
                            {
                                Value = rgDepr.SelectedIndex.ToString();

                                break;
                            }
                        case AssetSetting.ShowInsuranceAlert:
                            {
                                //  Value = txtShowAMCInsuranceAlert.Text;
                                //Value = txtShowAMCInsuranceAlert.Text != null ? txtShowAMCInsuranceAlert.ToString() : "";
                                break;
                            }
                        case AssetSetting.ShowOpApplyFrom:
                            {
                                string getDate = dtOPApplyFrom.DateTime.ToString();
                                Value = getDate;
                                break;
                            }
                    }
                    dtSetting.Rows[i][2] = Value;
                    dtSetting.Rows[i][3] = this.LoginUser.LoginUserId;
                }
            }
            else
            {
                this.ShowMessageBox(this.GetMessage(MessageCatalog.Common.COMMON_GRID_EMPTY));
            }
        }

        private void ApplySetting()
        {
            ISetting isetting;
            isetting = new GlobalSetting();
            ResultArgs resultArg = isetting.FetchSettingDetails(this.UtilityMember.NumberSet.ToInteger(this.LoginUser.LoginUserId.ToString()));
            if (resultArg.Success && resultArg.DataSource.TableView != null && resultArg.DataSource.TableView.Count != 0)
            {
                dtSettings = resultArg.DataSource.TableView;
                this.UIAppSetting.UISettingInfo = resultArg.DataSource.TableView;
            }
        }

        private void txtShowAMCAlert_Leave(object sender, EventArgs e)
        {
            //  this.SetBorderColor(txtShowAMCAlert);
        }

        private void txtShowAMCInsuranceAlert_Leave(object sender, EventArgs e)
        {
            //  this.SetBorderColor(txtShowAMCInsuranceAlert);
        }

        #endregion

        private void btnDeleteAll_Click(object sender, EventArgs e)
        {
            string strMessage = String.Format("Record is available, Do you want to delete all the Asset vouchers and continue Import?{0}{0}Yes            :  Delete and Continue{1}No             :  Cancel",
                                     Environment.NewLine, Environment.NewLine);
            DialogResult result = this.ShowConfirmationMessage(strMessage, MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                resultArgs = DeleteAllTransaction();
                if (resultArgs.Success)
                {
                    this.ShowMessageBox("Deleted Successfull");
                }
            }
            else
            {
                this.Close();
            }
        }

        /// <summary>
        /// Delete the Transaction
        /// </summary>
        /// <returns></returns>
        private ResultArgs DeleteAllTransaction()
        {
            using (AssetInwardOutwardSystem assetinwardoutward = new AssetInwardOutwardSystem())
            {
                resultArgs = assetinwardoutward.DeleteAssetTransaction();
            }
            return resultArgs;
        }
    }
}