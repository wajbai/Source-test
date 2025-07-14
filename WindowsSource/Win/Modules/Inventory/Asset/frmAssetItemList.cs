/*************************************************************************************************************************
 *                                              Purpose     : A common form to get Asset Item details of Purchase,Asset Opening Balance, 
 *                                                            Insurance and AMC
 *                                              Author      : Carmel Raj M
 *                                              Created On  : 28-October-2015
 *************************************************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using ACPP.Modules.UIControls;
using Bosco.Model;
using Bosco.Utility.ConfigSetting;
using Bosco.Utility;
using DevExpress.XtraLayout.Utils;
using Bosco.Model.UIModel;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid;

namespace ACPP.Modules.Inventory.Asset
{
    public partial class frmAssetItemList : frmFinanceBaseAdd
    {
        #region Properties
        public decimal Amount { get; set; }
        public decimal DepreciationAmount { get; set; }
        public int ItemId { get; set; }
        public string ItemName { get; set; }
        public int AssetMode { get; set; }
        public decimal PurchaseAmount { get; set; }
        public int Quantity { get; private set; }
        public string Prefix { get; set; }
        public string Sufix { get; set; }
        public int StartingNo { get; set; }
        public int Width { get; set; }
        public int ProjectId { get; set; }
        public DialogResult Dialogresult = DialogResult.Cancel;

        public DateTime InoutDate { get; set; }

        AssetInOut AssetType;

        #region Visibility Properties
        public LayoutVisibility CommonLookupVisibility { set { UcAssetItem.LookUpVisibility = value; } }
        public bool AMCVisible { set { UcAssetItem.AMCVisibility = value; } }
        public bool CostCentreVisibile { set { UcAssetItem.CostCentreVisibility = value; } }
        public bool InsuranceVisibile { set { UcAssetItem.InsuranceVisibility = value; } }
        public bool DeleteVisibile { set { UcAssetItem.DeleteVisibility = value; } }


        #endregion
        #endregion

        #region Constructor
        public frmAssetItemList(int ItemId, int ItemQuantity, int RowNo, int InOutDetailId, AssetInOut AssetInOutType, int ProjectId = 0, string ProjectName = "", string dtInoutDate = "")
        {
            InitializeComponent();
            UcAssetItem.AssetItemId = ItemId;
            UcAssetItem.NoOfItems = ItemQuantity;
            UcAssetItem.RowNo = RowNo;
            UcAssetItem.InOutDetailId = InOutDetailId;
            UcAssetItem.AssetInOutType = AssetType = AssetInOutType;
            UcAssetItem.ProjectId = ProjectId;
            UcAssetItem.ProjectName = ProjectName;
            UcAssetItem.InoutDate = this.UtilityMember.DateSet.ToDate(dtInoutDate, false);
        }

        #endregion

        #region Events
        private void frmAssetItemList_Load(object sender, EventArgs e)
        {
            //  SetCaption();
            LoadAssetItemDetails();
            GenerateAssetIdManual();
            SetTitle();
        }


        private void btnOk_Click(object sender, EventArgs e)
        {
            if (SettingProperty.AssetListCollection.ContainsKey(UcAssetItem.RowNo))
            {
                DataTable dtAssetList = SettingProperty.AssetListCollection[UcAssetItem.RowNo];
                if (ValidateAssetList(dtAssetList))
                {
                    decimal? Amount = 0;
                    decimal? SoldAmount = 0;
                    decimal? PurAmount = 0;
                    decimal? DepAmount = 0;
                    if (UcAssetItem.AssetInOutType == AssetInOut.PU || UcAssetItem.AssetInOutType == AssetInOut.IK || UcAssetItem.AssetInOutType == AssetInOut.OP)
                    {
                        //Amount = dtAssetList.AsEnumerable()
                        //         .Where(r => r.RowState != DataRowState.Deleted ? UtilityMember.NumberSet.ToInteger(r["SELECT"].ToString()) == 0 : false)
                        //         .Sum(r => r.Field<decimal?>("AMOUNT"));
                        //SoldAmount = dtAssetList.AsEnumerable()
                        //         .Where(r => r.RowState != DataRowState.Deleted ? UtilityMember.NumberSet.ToInteger(r["SELECT"].ToString()) == 1 : false)
                        //         .Sum(r => r.Field<decimal?>("AMOUNT"));
                        //Amount = Amount + SoldAmount;
                    }
                    else
                    {
                        Amount = dtAssetList.AsEnumerable()
                                 .Where(r => r.RowState != DataRowState.Deleted ? UtilityMember.NumberSet.ToInteger(r["SELECT"].ToString()) == 1 : false)
                                 .Sum(r => r.Field<decimal?>("AMOUNT"));

                        PurAmount = dtAssetList.AsEnumerable()
                                 .Where(r => r.RowState != DataRowState.Deleted ? UtilityMember.NumberSet.ToInteger(r["SELECT"].ToString()) == 1 : false)
                                 .Sum(r => r.Field<decimal?>("TEMP_AMOUNT"));
                    }
                    //Using null coalescing operator to convert nullable type to non-nullable type
                    //when the amount is null default value will be assigned to Amount
                    this.Amount = Amount ?? default(decimal);
                    this.DepreciationAmount = DepAmount ?? default(decimal);
                    this.PurchaseAmount = PurAmount ?? default(decimal);

                    this.Quantity = (UcAssetItem.AssetInOutType == AssetInOut.PU || UcAssetItem.AssetInOutType == AssetInOut.IK || UcAssetItem.AssetInOutType == AssetInOut.OP) ?
                        SettingProperty.AssetListCollection[UcAssetItem.RowNo].AsEnumerable().Count(r => r.RowState != DataRowState.Deleted ?
                            UtilityMember.NumberSet.ToInteger(r["SELECT"].ToString()) == 0 : false) :
                        SettingProperty.AssetListCollection[UcAssetItem.RowNo].AsEnumerable().Count(r => r.RowState != DataRowState.Deleted ?
                            UtilityMember.NumberSet.ToInteger(r["SELECT"].ToString()) == 1 : false);

                    if (UcAssetItem.AssetInOutType == AssetInOut.PU || UcAssetItem.AssetInOutType == AssetInOut.IK || UcAssetItem.AssetInOutType == AssetInOut.OP)
                    {
                        int DCount = SettingProperty.AssetListCollection[UcAssetItem.RowNo].AsEnumerable().Count(r => r.RowState != DataRowState.Deleted ?
                                    UtilityMember.NumberSet.ToInteger(r["STATUS"].ToString()) != 1 : false);

                        int SelectCount = SettingProperty.AssetListCollection[UcAssetItem.RowNo].AsEnumerable().Count(r => r.RowState != DataRowState.Deleted ?
                            UtilityMember.NumberSet.ToInteger(r["SELECT"].ToString()) != 1 : false);


                        decimal? TmpAmt = dtAssetList.AsEnumerable()
                                 .Where(r => r.RowState != DataRowState.Deleted ? UtilityMember.NumberSet.ToInteger(r["STATUS"].ToString()) != 1 : false)
                                 .Sum(r => r.Field<decimal?>("AMOUNT"));

                        decimal? SelectAmt = dtAssetList.AsEnumerable()
                                 .Where(r => r.RowState != DataRowState.Deleted ? UtilityMember.NumberSet.ToInteger(r["SELECT"].ToString()) != 1 : false)
                                 .Sum(r => r.Field<decimal?>("AMOUNT"));

                        decimal? DepSelectedAmount = dtAssetList.AsEnumerable()
                                 .Where(r => r.RowState != DataRowState.Deleted ? UtilityMember.NumberSet.ToInteger(r["SELECT"].ToString()) != 1 : false)
                                 .Sum(r => r.Field<decimal?>("DEPRECIATION_AMOUNT"));

                        this.Quantity = DCount + SelectCount;
                        // decimal? TAmount = TmpAmt + SelectAmt;
                        decimal? TAmount = (TmpAmt + SelectAmt) - DepSelectedAmount;
                        this.Amount = TAmount ?? default(decimal);
                        this.DepreciationAmount = DepSelectedAmount ?? default(decimal);
                    }

                    // Assign Insurance details to common collection
                    //  SettingProperty.AssetInsuranceCollection = UcAssetItem.AssetInsuranceCollections;
                    SettingProperty.AssetMultiInsuranceCollection = UcAssetItem.AssetMultiInsuranceCollection;
                    SettingProperty.AssetMultiInsuranceVoucherCollection = UcAssetItem.AssetMultiInsuranceVoucherCollection;

                    int TotCnt = dtAssetList.AsEnumerable().Count(r => r.RowState != DataRowState.Deleted ? true : false);

                    if (TotCnt > 0)
                    {
                        if (AssetInOut.SL == UcAssetItem.AssetInOutType || AssetInOut.DS == UcAssetItem.AssetInOutType && AssetInOut.DN == UcAssetItem.AssetInOutType)
                        {
                            if (!dtAssetList.Columns.Contains("TEMP_AMOUNT"))
                            {
                                dtAssetList.Columns.Add("TEMP_AMOUNT", typeof(decimal));
                            }
                            dtAssetList.AsEnumerable().ToList<DataRow>().ForEach(dr =>
                            {
                                if (dr.RowState != DataRowState.Deleted)
                                {
                                    double PurchaseAmount = 0;
                                    double SalesAmountAmount = 0;

                                    PurchaseAmount = this.UtilityMember.NumberSet.ToDouble(dr["TEMP_AMOUNT"].ToString());
                                    SalesAmountAmount = this.UtilityMember.NumberSet.ToDouble(dr["AMOUNT"].ToString());

                                    if (SalesAmountAmount > PurchaseAmount)  // Gain 
                                    {
                                        dr["GAIN_AMOUNT"] = this.UtilityMember.NumberSet.ToDecimal((SalesAmountAmount - PurchaseAmount).ToString());
                                        dr["LOSS_AMOUNT"] = 0;
                                    }
                                    else if (PurchaseAmount > SalesAmountAmount)
                                    {
                                        dr["LOSS_AMOUNT"] = this.UtilityMember.NumberSet.ToDecimal((PurchaseAmount - SalesAmountAmount).ToString());
                                        dr["GAIN_AMOUNT"] = 0;
                                    }
                                }
                                else
                                {
                                    dr.Delete();
                                }
                            });
                            dtAssetList.AcceptChanges();
                            SettingProperty.AssetListCollection[UcAssetItem.RowNo] = dtAssetList;
                        }
                        else
                        {
                            dtAssetList.AsEnumerable().ToList<DataRow>().ForEach(dr =>
                           {
                               if (dr.RowState == DataRowState.Deleted)
                                   dr.Delete();
                           });
                            dtAssetList.AcceptChanges();
                            SettingProperty.AssetListCollection[UcAssetItem.RowNo] = dtAssetList;
                        }
                    }
                    else
                    {
                        if (SettingProperty.AssetListCollection.ContainsKey(UcAssetItem.RowNo))
                            SettingProperty.AssetListCollection.Remove(UcAssetItem.RowNo);
                    }

                    Dialogresult = DialogResult.OK;
                    this.Close();
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            SettingProperty.AssetListCollection[UcAssetItem.RowNo] = UcAssetItem.dtTempAssetList;
            this.Close();
            // LoadAssetOpeningBalance();

        }

        private void LoadAssetOpeningBalance()
        {
            frmAssetOPBalance Opalance = new frmAssetOPBalance();
            Opalance.ShowDialog();
        }

        private void hlkAssetGenerate_Click(object sender, EventArgs e)
        {
            ItemName = lblItemName.Text;
            frmGenerateAsseIdManual generateAssetIdManual = new frmGenerateAsseIdManual(ItemId, ItemName, UcAssetItem.AssetItemSource);
            generateAssetIdManual.Show();
        }

        #endregion

        #region Method
        private void SetCaption()
        {
            string FormCaption = string.Empty;
            switch (AssetType)
            {
                case AssetInOut.OP:
                    //FormCaption = "Opening Item Details";
                    FormCaption = this.GetMessage(MessageCatalog.Asset.AssetItem.ASSET_OPENNING_ITEM_DETAIL);
                    break;
                case AssetInOut.PU:
                    //FormCaption = "Purchase Item Details";
                    FormCaption = this.GetMessage(MessageCatalog.Asset.AssetItem.ASSET_PUCHASE_ITEM_DETAIL);
                    break;
                case AssetInOut.IK:
                    //FormCaption = "In-kind Item Details";
                    FormCaption = this.GetMessage(MessageCatalog.Asset.AssetItem.ASSET_INKIND_ITEM_DETAIL);
                    break;
                case AssetInOut.SL:
                    //FormCaption = "Sales Item Details";
                    FormCaption = this.GetMessage(MessageCatalog.Asset.AssetItem.ASSET_SALES_ITEM_DETAIL);
                    break;
                case AssetInOut.AMC:
                    //FormCaption = "AMC Item Details";
                    FormCaption = this.GetMessage(MessageCatalog.Asset.AssetItem.ASSET_AMC_ITEM_DETAIL);
                    break;
            }
            this.Text = FormCaption;
        }

        private void LoadAssetItemDetails()
        {
            using (AssetItemSystem assetItem = new AssetItemSystem(UcAssetItem.AssetItemId))
            {
                UcAssetItem.AsseItemName = assetItem.Name;
                lblItemName.Text = String.Format(": {0}", assetItem.Name);
                lblItemClass.Text = String.Format(": {0}", assetItem.AssetClass);
                lblUoM.Text = String.Format(": {0}", assetItem.UOM.ToString());
                lblStartingNo.Text = String.Format(": {0}", assetItem.StartingNo.ToString());
                lblPrefix.Text = String.Format(": {0}", assetItem.Prefix);
                lblSuffix.Text = String.Format(": {0}", assetItem.Suffix);
                UcAssetItem.LedgerId = assetItem.AccountLeger;
                if (UcAssetItem.AssetInOutType == AssetInOut.PU || UcAssetItem.AssetInOutType == AssetInOut.IK || UcAssetItem.AssetInOutType == AssetInOut.OP)
                {
                    AMCVisible = assetItem.AMCApplicable > 0 ? true : false;
                    // InsuranceVisibile = assetItem.InsuranceApplicable > 0 ? true : false;//sudhakar for later
                    CostCentreVisibile = IsCostCentreEnables(assetItem.AccountLeger) ? true : false;
                }
                else
                {
                    AMCVisible = false;
                    //InsuranceVisibile = false;//sudhakar for later
                    CostCentreVisibile = false;
                }
            }
        }

        /// <summary>
        /// Updates the Collection of the RowNo which is assigned through Constructor as deleted
        /// </summary>
        public void DeleteAssetList()
        {
            UcAssetItem.DeleteAssetList();
        }

        private bool IsCostCentreEnables(int AccountLedgerId)
        {
            bool IsEnabled = false;
            using (LedgerSystem ledgerSystem = new LedgerSystem(AccountLedgerId))
            {
                IsEnabled = ledgerSystem.IsCostCentre > 0;
                UcAssetItem.LedgerId = ledgerSystem.LedgerId;
                UcAssetItem.LedgerName = ledgerSystem.LedgerName;
            }
            return IsEnabled;
        }

        private void GenerateManualAssetIds()
        {

        }

        private bool ValidateAssetList(DataTable dtAssetList)
        {
            bool IsValid = true;
            if (dtAssetList != null)
            {
                var GroupAssetList = from asset in dtAssetList.AsEnumerable()
                                     where asset.RowState != DataRowState.Deleted
                                     // let AssetId = asset["ASSET_ID"]
                                     group asset by asset["ASSET_ID"] into g
                                     select g;
                int AssetGroupedCount = GroupAssetList.Count();
                int TotalCount = dtAssetList.AsEnumerable().Count(r => r.RowState != DataRowState.Deleted ? true : false);
                int EmptyRowCount = dtAssetList.AsEnumerable().Count(r => r.RowState != DataRowState.Deleted ? string.IsNullOrEmpty(r["ASSET_ID"].ToString()) : false);
                int AmountZeroCount = dtAssetList.AsEnumerable().Count(r => r.RowState != DataRowState.Deleted ? UtilityMember.NumberSet.ToDecimal(r.Field<decimal?>("AMOUNT").ToString()) <= 0 : false);
                if (EmptyRowCount > 0)
                {
                    IsValid = false;
                    //ShowMessageBox("Asset Id is empty");
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSETID_EMPTY));
                }
                else if (TotalCount != AssetGroupedCount)
                {
                    IsValid = false;
                    //ShowMessageBox("Asset Id is duplicated");
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSETID_DUPLICATE_DETAIL));
                }
                else if (AmountZeroCount > 0)
                {
                    IsValid = false;
                    //ShowMessageBox("Amount shoud be greater than zero");
                    this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSETID_AMOUNT_GREATERHTAN_ZERO));
                    //UcAssetList assetList = new UcAssetList();
                    UcAssetItem.SetRatePerFocus = true;
                }
                else if (!UcAssetItem.CheckAssetIDExistsDB())
                {
                    IsValid = false;
                }
                //else if (!CheckAssetIDExistsDB(dtAssetList))
                //{
                //    IsValid = false;
                //}

                //else if (UcAssetItem.InOutDetailId == 0)
                //{
                else
                {
                    if (UcAssetItem.colInsurance.Visible && UcAssetItem.colCostCentre.Visible)  // && UcAssetItem.colAMC.Visible  // AMC / 
                    {
                        if (TotalCount != UcAssetItem.InsuranceCount)
                        {
                            //if (DialogResult.Cancel == ShowConfirmationMessage("Yet to update Insurance / Cost Centre details for " + (TotalCount - UcAssetItem.InsuranceCount).ToString() + " items", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                            if (DialogResult.Cancel == ShowConfirmationMessage(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSET_UPDATE_INS_COSTCENTER_DETAIL) + (TotalCount - UcAssetItem.InsuranceCount).ToString() + this.GetMessage(MessageCatalog.Asset.AssetItem.ASSETLIST_ITEMS_CAPTION), MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                            {
                                IsValid = false;
                            }
                        }
                    }
                    //else if (UcAssetItem.colInsurance.Visible)  // && UcAssetItem.colAMC.Visible
                    //{
                    //    if (TotalCount != UcAssetItem.InsuranceCount)   // / AMC 
                    //    {
                    //        //if (DialogResult.Cancel == ShowConfirmationMessage("Yet to update Insurance details for " + (TotalCount - UcAssetItem.InsuranceCount).ToString() + " items", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                    //        if (DialogResult.Cancel == ShowConfirmationMessage(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSET_UPDATE_INS_DETAIL) + (TotalCount - UcAssetItem.InsuranceCount).ToString() +" " + this.GetMessage(MessageCatalog.Asset.AssetItem.ASSETLIST_ITEMS_CAPTION), MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                    //        {
                    //            IsValid = false;
                    //        }
                    //    }
                    //}
                    //else if (UcAssetItem.colInsurance.Visible)
                    //{
                    //    if (TotalCount != UcAssetItem.InsuranceCount)
                    //    {
                    //        //if (DialogResult.Cancel == ShowConfirmationMessage("Yet to update Insurance details for " + (TotalCount - UcAssetItem.InsuranceCount).ToString() + " items", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                    //        if (DialogResult.Cancel == ShowConfirmationMessage(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSET_UPDATE_INS_DETAIL) + (TotalCount - UcAssetItem.InsuranceCount).ToString() + this.GetMessage(MessageCatalog.Asset.AssetItem.ASSETLIST_ITEMS_CAPTION), MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                    //        {
                    //            IsValid = false;
                    //        }
                    //    }
                    //}
                }
                //else if (UcAssetItem.colAMC.Visible)
                //{
                //    if (TotalCount != UcAssetItem.AMCCount)
                //    {
                //        if (DialogResult.Cancel == ShowConfirmationMessage("Some Items will be saved without AMC details", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                //        {
                //            IsValid = false;
                //        }
                //    }
                //}
                //else if (UcAssetItem.colCostCentre.Visible)
                //{
                //    if (TotalCount != UcAssetItem.CostCentreCount)
                //    {
                //        if (DialogResult.Cancel == ShowConfirmationMessage("Some Items will be saved without Cost centre details details", MessageBoxButtons.OKCancel, MessageBoxIcon.Question))
                //        {
                //            IsValid = false;
                //        }
                //   
                //}
                //}
            }
            return IsValid;
        }

        public void SetTitle()
        {
            this.Text = this.GetMessage(MessageCatalog.Asset.AssetItem.ASSET_ITEM_LIST_TITLE);
        }

        private void GenerateAssetIdManual()
        {
            if (UcAssetItem.AssetMode == 1)
            {
                layoutControlItem16.Visibility = LayoutVisibility.Never;
            }
            else
            {
                layoutControlItem16.Visibility = LayoutVisibility.Always;
            }

        }

        private bool CheckAssetIDExistsDB(DataTable dtAssetDetails)
        {
            ResultArgs resultArgs = new ResultArgs();
            bool isvalid = true;
            using (AssetInwardOutwardSystem inwardoutward = new AssetInwardOutwardSystem())
            {
                int ItemDetailID = 0;
                foreach (DataRow drItem in dtAssetDetails.Rows)
                {
                    if (drItem.RowState != DataRowState.Deleted)
                    {
                        ItemDetailID = drItem["ITEM_DETAIL_ID"] != null ? UtilityMember.NumberSet.ToInteger(drItem["ITEM_DETAIL_ID"].ToString()) : 0;
                        if (!string.IsNullOrEmpty(drItem["ITEM_ID"].ToString()) && !string.IsNullOrEmpty(drItem["ASSET_ID"].ToString()))
                        {
                            resultArgs = inwardoutward.CheckAssetItemIdExists(UtilityMember.NumberSet.ToInteger(drItem["ITEM_ID"].ToString()), drItem["ASSET_ID"].ToString(), ItemDetailID);

                            if (resultArgs != null && !string.IsNullOrEmpty(resultArgs.DataSource.Sclar.ToString))
                            {
                                isvalid = false;
                                this.ShowMessageBox("Asset Id [ " + drItem["ASSET_ID"].ToString() + " ] exists already for this Item.");
                                //this.ShowMessageBox(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSET_ASSETID_CAPTION) + [ " + drItem["ASSET_ID"].ToString() + " ] + this.GetMessage(MessageCatalog.Asset.AssetItem.ASSET_ALREADY_EXISTS_ITEM));
                                break;
                            }
                        }
                    }
                }

            }
            return isvalid;
        }

        public void AssignItemDetails()
        {
            UcAssetItem.AssignItemDetails();
        }

        #endregion


    }
}