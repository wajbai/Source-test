/*************************************************************************************************************************
 *                                              Purpose     : A User control to get Asset Item details of Purchase,Asset Opening Balance, 
 *                                                            Insurance and AMC
 *                                              Author      : Carmel Raj M
 *                                              Created On  : 28-October-2015
 *************************************************************************************************************************/
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Utility;
using Bosco.Model.Inventory;
using Bosco.Model;
using ACPP.Modules.Inventory;
using DevExpress.XtraLayout.Utils;
using Bosco.Utility.ConfigSetting;
using ACPP.Modules.Inventory.Asset;
using ACPP.Modules.Transaction;
using System.Text.RegularExpressions;
using DevExpress.XtraGrid;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Columns;
using Bosco.Utility;

namespace ACPP.Modules.UIControls
{
    public partial class UcAssetList : DevExpress.XtraEditors.XtraUserControl
    {
        #region Variables
        const string ASSET_TABLE_NAME = "AssetItem";
        const string SELECT = "SELECT";
        const string HeaderCaption = "<-- Empty -->"; // "<--Select-->";
        bool IsGridModified = false;
        CommonMember utilityMember = null;
        private ResultArgs resultArgs = null;
        DataTable dtAssetItemStructure;
        public DataTable dtAssetOPImport = new DataTable("OP Balance");
        //public Dictionary<int, DataTable> AssetInsuranceCollections = new Dictionary<int, DataTable>();
        public Dictionary<Tuple<int, int>, DataTable> AssetMultiInsuranceCollection = new Dictionary<Tuple<int, int>, DataTable>();
        public Dictionary<Tuple<int, int>, DataTable> AssetMultiInsuranceVoucherCollection = new Dictionary<Tuple<int, int>, DataTable>();
        SettingProperty settingProperty = new SettingProperty();
        public bool EnableRemoveColumn = false;
        MessageRender msgRender = new MessageRender();

        //   public Dictionary<int, DataTable> AssetListCollection = new Dictionary<int, DataTable>();

        #endregion

        #region Properties
        bool IsItemGenerated = false;
        public int RowNo { get; set; }
        public int InsuranceCount { get; set; }
        public int CostCentreCount { get; set; }
        public int AMCCount { get; set; }

        public int AssetItemId { get; set; }
        public int InOutDetailId { get; set; }
        public AssetInOut AssetInOutType { get; set; }
        public int NoOfItems { private get; set; }
        public decimal Amount { get; set; }
        public string Prefix { get; set; }
        public int Width { get; set; }
        public string Sufix { get; set; }
        public int StartingNo { get; set; }
        public int tmpLastAssetNo { get; set; }
        public int ProjectId { get; set; }
        public string ProjectName { get; set; }
        public int LedgerId { get; set; }
        public string LedgerName { get; set; }
        public string AsseItemName { get; set; }
        public int AssetMode { get; set; }
        public DataTable dtTempAssetList;
        public int LocationId { get; set; }
        public int ManufactureId { get; set; }
        public DateTime InoutDate { get; set; }

        private int GetMappedCustodianId { get; set; }
        private int GetLocationId { get { return glkLocation.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkLocation.EditValue.ToString()) : 0; } }
        private int GetManufacturerId { get { return glkManufacturer.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkManufacturer.EditValue.ToString()) : 0; } }
        private double AssetitemAmount { get { return gvItem.GetFocusedRowCellValue(colAmount) != null ? this.UtilityMember.NumberSet.ToDouble(gvItem.GetFocusedRowCellValue(colAmount).ToString()) : 0; } }
        private string AssetId { get { return gvItem.GetFocusedRowCellValue(colAssetId) != null ? gvItem.GetFocusedRowCellValue(colAssetId).ToString() : ""; } }

        private double ItemAmount
        {
            get
            {
                double itemamount;
                itemamount = gvItem.GetFocusedRowCellValue(colAmount) != null ? this.UtilityMember.NumberSet.ToDouble(gvItem.GetFocusedRowCellValue(colAmount).ToString()) : 0;
                return itemamount;
            }
        }

        private double ItemDepAmt
        {
            get
            {
                double itemdep;
                itemdep = gvItem.GetFocusedRowCellValue(colDepreciation) != null ? this.UtilityMember.NumberSet.ToDouble(gvItem.GetFocusedRowCellValue(colDepreciation).ToString()) : 0;
                return itemdep;
            }
        }

        private DataSet dsCostCentre = new DataSet();
        public LayoutVisibility LookUpVisibility
        {
            set
            {
                lblApplyCaption.Visibility = lblLocationCaption.Visibility = lblDeleteStatusNote.Visibility =
                lblRateCaption.Visibility = lblManufacturerCaption.Visibility = value;
                colLocation.OptionsColumn.AllowEdit = colLocation.OptionsColumn.AllowFocus =
                colCustodian.OptionsColumn.AllowEdit = colCustodian.OptionsColumn.AllowFocus =
                colManufacturer.OptionsColumn.AllowEdit = colManufacturer.OptionsColumn.AllowFocus = LayoutVisibility.Never == value ? false : true;
            }
        }

        public bool AMCVisibility
        {
            set
            {
                //colAMC.Visible = value;
                //lblAMCIcone.Visibility = value ? LayoutVisibility.Always : LayoutVisibility.Never;
                colAMC.Visible = false;
            }
        }

        public bool SetRatePerFocus
        {
            set
            {
                txtRatePerItem.Select();
            }
        }

        public GridControl AssetItemSource
        {
            get
            {
                return gcItem;
            }
        }

        public bool CostCentreVisibility
        {
            set
            {
                //colCostCentre.Visible = value;
                //lblCostCentreIcon.Visibility = value ? LayoutVisibility.Always : LayoutVisibility.Never;
                colCostCentre.Visible = false;
            }
        }
        public bool DeleteVisibility { set { colDelete.Visible = value; } }
        public bool InsuranceVisibility
        {
            set
            {
                colInsurance.Visible = value;  // AssetInsuranceCollections = SettingProperty.AssetInsuranceCollection ;
                AssetMultiInsuranceCollection = SettingProperty.AssetMultiInsuranceCollection;
                AssetMultiInsuranceVoucherCollection = SettingProperty.AssetMultiInsuranceVoucherCollection;
                lblInsureanceIcon.Visibility = value ? LayoutVisibility.Always : LayoutVisibility.Never;
            }
        }
        #endregion

        #region Constructor
        public UcAssetList()
        {
            InitializeComponent();
            RealColumnEditAssetAmount();
            RealColumnEditDepAssetAmount();
        }
        public UcAssetList(int AssetId, int RowNo)
            : this()
        {
            this.AssetItemId = AssetId;
            this.RowNo = RowNo;

        }
        #endregion

        #region Events
        private void UcAssetOpeningBalance_Load(object sender, EventArgs e)
        {
            EnableRemoveColumn = false;
            LoadDefaultData();
            setCursorPosition(); // added by praveen for cursor position           
            //glkLocation.EditValue = glkLocation.Properties.GetKeyValue(0);
            glkLocation.EditValue = LocationId;
            glkManufacturer.EditValue = ManufactureId;

        }

        private void gvItem_CellValueChanging(object sender, DevExpress.XtraGrid.Views.Base.CellValueChangedEventArgs e)
        {
            IsGridModified = true;
        }

        private void glkLocation_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                using (frmLocationsAdd LocationAdd = new frmLocationsAdd())
                {
                    LocationAdd.ShowDialog();
                    LoadLocations();
                    LoadCustodians();
                    LoadMappedCustodian();
                    glkLocation.EditValue = LocationAdd.LocationID;
                }
            }
        }

        private void glkCustodian_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                using (frmCustodiansAdd CustodianAdd = new frmCustodiansAdd())
                {
                    CustodianAdd.ShowDialog();
                    LoadCustodians();
                }
            }
        }

        private void glkManufacturer_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        {
            if (e.Button.Kind == DevExpress.XtraEditors.Controls.ButtonPredefines.Plus)
            {
                using (frmVendorInfoAdd ManufcaturerAdd = new frmVendorInfoAdd(0, VendorManufacture.Manufacture))
                {
                    ManufcaturerAdd.ShowDialog();
                    LoadManufacturers();
                    //  glkManufacturer.EditValue = ManufcaturerAdd.;
                }
            }
        }

        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvItem.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                gvItem.FocusedRowHandle = DevExpress.XtraGrid.GridControl.AutoFilterRowHandle;
                gvItem.FocusedColumn = gvItem.VisibleColumns[1];
                gvItem.ShowEditor();
            }
        }

        private void rbtnInsurance_Click(object sender, EventArgs e)
        {
            int AssetDetailId = default(int);
            decimal ItemAmount = default(decimal);
            if (InOutDetailId > 0)
            {
                DataTable dtAssetDetail = gcItem.DataSource as DataTable;
                var ItemDetailId = dtAssetDetail.AsEnumerable().Where(dr => dr.Field<string>("ASSET_ID") == AssetId);
                if (ItemDetailId.Count() > 0)
                {
                    dtAssetDetail = ItemDetailId.CopyToDataTable();
                    if (dtAssetDetail != null && dtAssetDetail.Rows.Count > 0)
                    {
                        AssetDetailId = UtilityMember.NumberSet.ToInteger(dtAssetDetail.Rows[0]["ITEM_DETAIL_ID"].ToString());
                    }
                }
            }
            ItemAmount = gvItem.GetFocusedRowCellValue(colAmount) != null ? UtilityMember.NumberSet.ToDecimal(gvItem.GetFocusedRowCellValue(colAmount).ToString()) : 0;
            using (frmRenewInsuranceVoucherAdd frmInsuranceRenew = new frmRenewInsuranceVoucherAdd(AssetDetailId, AssetItemId, gvItem.GetFocusedDataSourceRowIndex(), AsseItemName, AssetId, ItemAmount,
                AssetInOutType == AssetInOut.OP ? AssetInsurance.Opening : AssetInOutType == AssetInOut.PU ? AssetInsurance.Purchase : AssetInsurance.InKind))
            {
                // frmInsuranceRenew.AssetInsuranceCollections = AssetInsuranceCollections;
                frmInsuranceRenew.AssetMultiInsuranceCollection = AssetMultiInsuranceCollection;
                frmInsuranceRenew.mode = AssetInOutType == AssetInOut.OP ? (int)AssetInsurance.Opening : AssetInOutType == AssetInOut.PU ? (int)AssetInsurance.Purchase : (int)AssetInsurance.InKind;
                frmInsuranceRenew.AssetMultiInsuranceVoucherCollection = AssetMultiInsuranceVoucherCollection;
                frmInsuranceRenew.Projectid = ProjectId;
                frmInsuranceRenew.ProjectName = ProjectName;
                frmInsuranceRenew.ShowDialog();
                if (frmInsuranceRenew.Dialogresult == DialogResult.OK)
                {
                    InsuranceCount++;
                    //    AssetInsuranceCollections = frmInsuranceRenew.AssetInsuranceCollections;
                    AssetMultiInsuranceCollection = frmInsuranceRenew.AssetMultiInsuranceCollection;
                    AssetMultiInsuranceVoucherCollection = frmInsuranceRenew.AssetMultiInsuranceVoucherCollection;
                }
                else
                {
                    var Inskey = new Tuple<int, int>(AssetItemId, RowNo);
                    AssetMultiInsuranceCollection.Remove(Inskey);
                    AssetMultiInsuranceVoucherCollection.Remove(Inskey);
                }
            }
        }

        private void rbtnAMC_Click(object sender, EventArgs e)
        {
            //using (frmAmcVoucherAdd frmAMCVoucher = new frmAmcVoucherAdd())
            //{
            //    AMCCount++;
            //    frmAMCVoucher.ShowDialog();
            //}
        }

        private void rbtnCostCentre_Click(object sender, EventArgs e)
        {
            DataView dvCostCentre = null;
            int RowIndex = this.UtilityMember.NumberSet.ToInteger(gvItem.GetDataSourceRowIndex(gvItem.FocusedRowHandle).ToString());
            if (dsCostCentre.Tables.Contains(RowIndex + "LDR" + LedgerId))
            {
                dvCostCentre = dsCostCentre.Tables[RowIndex + "LDR" + LedgerId].DefaultView;
            }
            using (frmTransactionCostCenter frmCostCentre = new frmTransactionCostCenter(ProjectId, dvCostCentre, LedgerId, AssetitemAmount, LedgerName))
            {
                frmCostCentre.ShowDialog();
                if (frmCostCentre.DialogResult == DialogResult.OK)
                {
                    CostCentreCount++;
                    DataTable dtValues = frmCostCentre.dtRecord;
                    if (dtValues != null)
                    {
                        dtValues.TableName = RowIndex + "LDR" + LedgerId;
                        if (dsCostCentre.Tables.Contains(dtValues.TableName))
                        {
                            dsCostCentre.Tables.Remove(dtValues.TableName);
                        }
                        dsCostCentre.Tables.Add(dtValues);
                    }
                }
            }
        }

        private void rDeleteItem_Click(object sender, EventArgs e)
        {
            string AssetId = gvItem.GetFocusedRowCellValue(colAssetId).ToString();
            if (!string.IsNullOrEmpty(AssetId))
            {
                //if (XtraMessageBox.Show("Delete this entry?", "Acme.erp", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                if (XtraMessageBox.Show(MessageRender.GetMessage(MessageCatalog.Master.FinanceUIControls.UCASSET_LIST_DELETE_CONFIRMATION_INFO), MessageRender.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    int Status = UtilityMember.NumberSet.ToInteger(gvItem.GetFocusedRowCellValue(colStatus).ToString());
                    //Status = 0 Sold or Diposed or Donated
                    if (InOutDetailId == 0 || Status != 0)
                    {
                        DataTable dtAssetList = SettingProperty.AssetListCollection[RowNo];

                        if (dtAssetList != null && dtAssetList.Rows.Count > 0)
                        {
                            using (AssetInwardOutwardSystem assetinoutsystem = new AssetInwardOutwardSystem())
                            {
                                if (dtAssetList.Rows[gvItem.FocusedRowHandle].RowState != DataRowState.Deleted)
                                {
                                    int AssId = this.UtilityMember.NumberSet.ToInteger(dtAssetList.Rows[gvItem.FocusedRowHandle]["ITEM_DETAIL_ID"].ToString());
                                    if (AssId > 0)
                                        SettingProperty.AssetDeletedItemDetailIds += AssId.ToString() + ",";
                                    // resultArgs = assetinoutsystem.DeleteItemDetail(AssId);
                                }
                                if (resultArgs.Success)
                                {
                                    gvItem.DeleteRow(gvItem.FocusedRowHandle);
                                }

                            }
                        }


                        //if (SettingProperty.AssetInsuranceCollection.ContainsKey(RowNo))
                        //{
                        //    if (XtraMessageBox.Show("This Item has Insurance Details.Once it is deleted the record cannot be retrieved.?", "Acme.erp", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        //    {
                        //        resultArgs = DeleteInsuranceDetails(AssetId);
                        //        if (resultArgs.Success)
                        //        {
                        //            DataTable dtAssetList = SettingProperty.AssetListCollection[RowNo];
                        //            gvItem.DeleteRow(gvItem.FocusedRowHandle);
                        //            SettingProperty.AssetInsuranceCollection.Remove(RowNo);
                        //        }
                        //    }
                        //}
                        //else
                        //{
                        //    if (CheckInsuranceDetailsExists(AssetId))
                        //    {
                        //        if (XtraMessageBox.Show("This Item has Insurance Details.Once it is deleted the record cannot be retrieved.?", "Acme.erp", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        //        {
                        //            resultArgs = DeleteInsuranceDetails(AssetId);
                        //            if (resultArgs.Success)
                        //            {
                        //                DataTable dtAssetList = SettingProperty.AssetListCollection[RowNo];
                        //                gvItem.DeleteRow(gvItem.FocusedRowHandle);
                        //            }
                        //        }
                        //    }
                        //}
                    }
                    else
                    {
                        //ShowMessageBox("Can not delete,Item has been sold or Diposed or Donated");
                        ShowMessageBox(MessageRender.GetMessage(MessageCatalog.Master.FinanceUIControls.UCASSET_LIST_CANNOT_DELETE_INFO));
                    }
                    //AssetMode 1 =Auto Generate AssetId else Manual
                    //if (AssetMode == 1 && (AssetInOut.PU == AssetInOutType || AssetInOut.IK == AssetInOutType || AssetInOut.OP == AssetInOutType))
                    //{
                    //// Decrease No of Items by one whenever the item is deleted
                    //LoadAssetItemDetails();
                    //int TmpNofItem = NoOfItems;
                    //int AfterDeleteNoOfItems = StartingNo;
                    //Func<string> ReGenereateAssetId = () => Prefix + AfterDeleteNoOfItems++ + Sufix;

                    //dtAssetList.AsEnumerable().ToList<DataRow>().ForEach(dr =>
                    //{
                    //    if (dr.RowState != DataRowState.Deleted && TmpNofItem != 0)
                    //    {
                    //        {
                    //            dr[AppSchemas.ASSETItem.ASSET_IDColumn.ColumnName] = ReGenereateAssetId.Invoke();
                    //            TmpNofItem--;
                    //        }
                    //    }
                    //});
                    //gcItem.DataSource = SettingProperty.AssetListCollection[RowNo] = dtAssetList;
                    //}
                }
            }
            //else ShowMessageBox("Asset Id empty");
            else ShowMessageBox(MessageRender.GetMessage(MessageCatalog.Master.FinanceUIControls.UCASSET_LIST_ASSETID_EMPTY));
        }

        private void gvItem_RowCountChanged(object sender, EventArgs e)
        {
            lblGridCount.Text = gvItem.RowCount.ToString();
        }

        private void gvItem_ShowingEditor(object sender, CancelEventArgs e)
        {
            try
            {
                if (InOutDetailId > 0)
                {
                    if (gvItem.GetRowCellValue(gvItem.FocusedRowHandle, colStatus) != null)
                    {
                        if (gvItem.GetRowCellValue(gvItem.FocusedRowHandle, colStatus).ToString() != string.Empty)
                        {
                            string Status = (string)gvItem.GetRowCellValue(gvItem.FocusedRowHandle, colStatus).ToString();
                            //Status 0 = Sold or Donoted or Diposed
                            //Status 1 = Purchase
                            if (Status == "0" && gvItem.FocusedColumn == colDelete)
                            {
                                e.Cancel = true; //Disabling the editing of the cell 
                                //this.ShowMessageBox("This Item has been Sold or Diposed or Donated");
                                this.ShowMessageBox(MessageRender.GetMessage(MessageCatalog.Master.FinanceUIControls.UCASSET_LIST_ITEM_SOLD_DISPOSE_DONATE_INFO));
                            }
                        }
                    }

                    if (AssetInOutType == AssetInOut.PU || AssetInOutType == AssetInOut.IK || AssetInOutType == AssetInOut.OP)
                    {
                        if (gvItem.GetRowCellValue(gvItem.FocusedRowHandle, colAmount) != null)
                        {
                            if (gvItem.GetRowCellValue(gvItem.FocusedRowHandle, colAmount).ToString() != string.Empty)
                            {
                                string Status = (string)gvItem.GetRowCellValue(gvItem.FocusedRowHandle, colStatus).ToString();
                                //Status 0 = Sold or Donoted or Diposed
                                //Status 1 = Purchase
                                if (Status == "0")
                                {
                                    e.Cancel = true; //Disabling the editing of the cell 
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }

        private void gvItem_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {
            try
            {
                if (InOutDetailId > 0)
                {
                    if (gvItem.GetRowCellValue(e.RowHandle, colStatus) != null)
                    {
                        if (gvItem.GetRowCellValue(e.RowHandle, colStatus).ToString() != string.Empty)
                        {
                            string Status = gvItem.GetRowCellValue(e.RowHandle, colStatus).ToString();
                            //Status 0 = sold or Donoted or Diposed
                            //Status 1 = Purchase
                            if (Status == "0")
                            {
                                e.Appearance.BackColor = Color.Red;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
        }

        private void btnApply_Click(object sender, EventArgs e)
        {
            //gcItem.DataSource = UpdateDataTableValues((gcItem.DataSource as DataTable));
            CheckLoactionExists();
            //gcItem.Focus();
            //gvItem.MoveFirst();
            //gvItem.FocusedColumn = colAssetId;
            //gvItem.ShowEditor();
        }
        private void gvItem_ValidatingEditor(object sender, DevExpress.XtraEditors.Controls.BaseContainerValidateEditorEventArgs e)
        {

            //if (gvItem.FocusedColumn.FieldName == "ASSET_ID" && IsDuplicatedValue((sender as GridView), gvItem.FocusedColumn, e.Value))
            //{
            //   //  (sender as GridView).UpdateCurrentRow();
            //}

            GridView view = sender as GridView;
            DataView currentDataView = (sender as GridView).DataSource as DataView;
            if (view.FocusedColumn.FieldName == "ASSET_ID")
            {
                //check duplicate code
                string currentCode = e.Value.ToString().ToUpper().Trim();
                for (int i = 0; i < currentDataView.Count; i++)
                {
                    if (i != view.GetDataSourceRowIndex(view.FocusedRowHandle))
                    {
                        if (!string.IsNullOrEmpty(currentCode))
                        {
                            if (currentDataView[i]["ASSET_ID"].ToString().ToUpper().Trim() == currentCode.ToUpper().Trim())
                            {
                                //this.ShowMessageBox("Asset Id is duplicated.");
                                this.ShowMessageBox(MessageRender.GetMessage(MessageCatalog.Master.FinanceUIControls.UCASSET_LIST_ASSETID_DUPLICATED_INFO));
                                //e.ErrorText = "Asset Id is duplicated.";
                                e.ErrorText = (MessageRender.GetMessage(MessageCatalog.Master.FinanceUIControls.UCASSET_LIST_ASSETID_DUPLICATED_INFO));
                                e.Valid = false;
                                break;
                            }
                        }
                    }
                }
            }
            if (view.FocusedColumn.FieldName == colAmount.FieldName)
            {
                //  string SalAmount = gvItem.GetFocusedRowCellValue(colSalvageValue) != null ? gvItem.GetFocusedRowCellValue(colSalvageValue).ToString() : string.Empty;
                //   if (utilityMember.NumberSet.ToDouble(SalAmount) == 0)
                //   {
                // gvItem.SetFocusedRowCellValue(colSalvageValue, e.Value);
                //   }

                // if (utilityMember.NumberSet.ToDouble(SalAmount) > utilityMember.NumberSet.ToDouble(e.Value.ToString()))
                //  {
                // gvItem.SetFocusedRowCellValue(colSalvageValue, e.Value);
                //  }
            }
            if (view.FocusedColumn.FieldName == colSalvageValue.FieldName)
            {
                string salvagevalue = e.Value.ToString();
                string ItemAmount = gvItem.GetFocusedRowCellValue(colAmount) != null ? gvItem.GetFocusedRowCellValue(colAmount).ToString() : string.Empty;

                if (utilityMember.NumberSet.ToDouble(salvagevalue) >= utilityMember.NumberSet.ToDouble(ItemAmount))
                {
                    //this.ShowMessageBox("Salvage Value should not be equal or exceed the Actual Amount.");
                    this.ShowMessageBox(MessageRender.GetMessage(MessageCatalog.Master.FinanceUIControls.UCASSET_LIST_SALVAGE_VALUE_INFO));
                    e.Valid = false;
                }
                if (utilityMember.NumberSet.ToDouble(salvagevalue) == 0)
                {
                    //e.Value = 1;
                    e.Value = 0;
                }
            }
        }

        //bool IsDuplicatedValue(GridView currentView, GridColumn currentColumn, object someValue)
        //{
        //    bool isexist = true;
        //    for (int i = 0; i < currentView.DataRowCount; i++)
        //    {
        //        if (currentView.GetRowCellValue(currentView.GetRowHandle(i), currentColumn).ToString() == someValue.ToString())
        //        {
        //            gvItem.FocusedRowHandle = i;
        //            gvItem.FocusedColumn = colAssetId;
        //            this.ShowMessageBox("Asset Id is duplicated.");
        //            gvItem.SelectRow(i);
        //       //     gvItem.Appearance.SelectedRow.BackColor = Color.Red;
        //            gvItem.Appearance.Row.BackColor = Color.Empty;
        //            gvItem.Appearance.FocusedRow.BackColor = Color.Green;
        //            return true;
        //        }
        //    }

        //    return false;

        //}

        public DialogResult ShowConfirmationMessage(string Message, MessageBoxButtons messageBoxButtons, MessageBoxIcon messageBoxIcon)
        {
            DialogResult drResult = DialogResult.None;
            drResult = XtraMessageBox.Show(Message, "Acme.erp", messageBoxButtons, messageBoxIcon);
            return drResult;
        }

        private void gcItem_ProcessGridKey(object sender, KeyEventArgs e)
        {
            if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab))
            {
                if (gvItem.IsLastRow && gvItem.FocusedColumn == colAmount)
                    ProcessDialogKey(e.KeyData);
                colAssetId.OptionsColumn.AllowEdit = colAssetId.OptionsColumn.AllowFocus = true;
                if (gvItem.IsLastRow && gvItem.FocusedColumn == colSalvageValue)
                    ProcessDialogKey(e.KeyData);
                colSalvageValue.OptionsColumn.AllowEdit = colSalvageValue.OptionsColumn.AllowFocus = true;
                //  colSalvageValue.OptionsColumn.AllowEdit = colSalvageValue.OptionsColumn.AllowFocus = true;

            }
            else if (gvItem.IsFirstRow && gvItem.FocusedColumn == colAssetId && e.Shift && e.KeyCode == Keys.Tab)
            {
                gvItem.CloseEditor();
                txtRatePerItem.Select();
            }
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            if (keyData != Keys.Tab)
            {
                return base.ProcessDialogKey(keyData);
            }
            Parent.SelectNextControl(this, true, true, false, false);
            return false;
        }

        private void gvItem_FocusedColumnChanged(object sender, DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventArgs e)
        {
            gvItem.UpdateTotalSummary();
        }
        #endregion

        #region Methods
        #region Defaul Load methods
        private void LoadDefaultData()
        {
            if (AssetItemId > 0)
            {
                if (SettingProperty.AssetListCollection.ContainsKey(RowNo))
                {
                    DataTable dt = SettingProperty.AssetListCollection[RowNo];
                    if (dt != null && dt.Rows.Count >
                        0)
                    {
                        if (dt.AsEnumerable().Take(1).FirstOrDefault().RowState != DataRowState.Deleted)
                        {
                            DataRow drItem = dt.AsEnumerable().Take(1).FirstOrDefault();
                            int PrvItemId = UtilityMember.NumberSet.ToInteger(drItem["ITEM_ID"].ToString());
                            if (AssetItemId != PrvItemId)
                            {
                                SettingProperty.AssetListCollection.Remove(RowNo);
                            }
                            // glkLocation.EditValue = 2;
                            //glkLocation.EditValue = glkLocation.Properties.GetKeyValue(0);
                            LocationId = UtilityMember.NumberSet.ToInteger(dt.Rows[0]["LOCATION_ID"].ToString());
                            txtRatePerItem.Text = dt.Rows[0]["TEMP_AMOUNT"].ToString();

                            // 25/10/2024 - Chinna
                            if (txtRatePerItem.Text == "0.00")
                            {
                                txtRatePerItem.Text = dt.Rows[0]["AMOUNT"].ToString();
                            }

                            if (dt.Columns.Contains("DEPRECIATION_AMOUNT"))
                            {
                                txtDepreciation.Text = dt.Rows[0]["DEPRECIATION_AMOUNT"] == null ? "" : dt.Rows[0]["DEPRECIATION_AMOUNT"].ToString();
                            }
                            ManufactureId = UtilityMember.NumberSet.ToInteger(dt.Rows[0]["ID"].ToString());
                        }
                        else
                        {
                            SettingProperty.AssetListCollection.Remove(RowNo);
                        }
                    }
                }
                LoadLocations();
                LoadCustodians();
                LoadManufacturers();
                LoadAssetItemDetails();
                BindAssetItemList();
                // colAssetId.OptionsColumn.AllowEdit = colAssetId.OptionsColumn.AllowFocus = AssetMode == 1 ? false : true;
                colAssetId.OptionsColumn.AllowEdit = colAssetId.OptionsColumn.AllowFocus = true;

                if (AssetInOutType == AssetInOut.OP || AssetInOutType == AssetInOut.PU || AssetInOutType == AssetInOut.IK)
                {
                    //  colSalvageValue.OptionsColumn.AllowEdit = colSalvageValue.OptionsColumn.AllowFocus = true;
                    // colSalvageValue.Visible = true; commond by sudhakar to enable later
                    //   colSalvageValue.VisibleIndex = 5;

                    if (AssetInOutType == AssetInOut.OP)
                    {
                        lblRateDepr.Visibility = LayoutVisibility.Always;
                        colDepreciation.Visible = true;
                        colBalance.Visible = true;
                    }
                    else
                    {
                        lblRateDepr.Visibility = LayoutVisibility.Never;
                        colDepreciation.Visible = false;
                        colBalance.Visible = false;
                    }
                }
                else
                {
                    colSalvageValue.OptionsColumn.AllowEdit = colSalvageValue.OptionsColumn.AllowFocus = true;
                    colSalvageValue.Visible = false;
                }

                if (AssetInOutType == AssetInOut.SL || AssetInOut.DN == AssetInOutType || AssetInOut.AMC == AssetInOutType || AssetInOut.DS == AssetInOutType)
                {
                    colRemove.Caption = "Select";
                    colRemove.VisibleIndex = 0;
                    colRemove.Visible = true;

                    if (AssetInOutType == AssetInOut.OP)
                    {
                        lblRateDepr.Visibility = LayoutVisibility.Always;
                        colDepreciation.Visible = true;
                        colBalance.Visible = true;
                    }
                    else
                    {
                        lblRateDepr.Visibility = LayoutVisibility.Never;
                        colDepreciation.Visible = false;
                        colBalance.Visible = false;
                    }
                }

                if (EnableRemoveColumn)
                {
                    lblDeleteStatusNote.Visibility = LayoutVisibility.Always;
                    colRemove.VisibleIndex = 0;
                    colRemove.Visible = true;
                }
            }
        }

        private void LoadLocations()
        {
            using (LocationSystem locationSystem = new LocationSystem())
            {
                locationSystem.ProjectId = ProjectId;
                resultArgs = locationSystem.FetchLocationByProject();
                if (resultArgs != null && resultArgs.Success)
                {
                    using (CommonMethod SelectAll = new CommonMethod())
                    {
                        //DataTable dtLocationList = resultArgs.DataSource.Table.Copy();

                        DataTable dtLocationList = SelectAll.AddHeaderColumn(resultArgs.DataSource.Table.Copy(), locationSystem.AppSchema.ASSETLocationDetails.LOCATION_IDColumn.ColumnName, locationSystem.AppSchema.ASSETLocationDetails.LOCATIONColumn.ColumnName, HeaderCaption);
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkLocation, dtLocationList, locationSystem.AppSchema.ASSETLocationDetails.LOCATIONColumn.ColumnName, locationSystem.AppSchema.ASSETLocationDetails.LOCATION_IDColumn.ColumnName);
                        glkLocation.EditValue = glkLocation.Properties.GetKeyValue(0);
                        this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkLocation, resultArgs.DataSource.Table, locationSystem.AppSchema.ASSETLocationDetails.LOCATIONColumn.ColumnName, locationSystem.AppSchema.ASSETLocationDetails.LOCATION_IDColumn.ColumnName);
                    }
                }
                else
                    ShowMessageBox(resultArgs.Message);

                //  glkLocation.EditValue = 1;
            }
        }

        private void LoadManufacturers()
        {
            using (ManufactureInfoSystem manufacturerSystem = new ManufactureInfoSystem())
            {
                resultArgs = manufacturerSystem.FetchDetails();
                if (resultArgs != null && resultArgs.Success)
                {
                    using (CommonMethod SelectAll = new CommonMethod())
                    {
                        DataTable dtManufacturerList = SelectAll.AddHeaderColumn(resultArgs.DataSource.Table.Copy(), "ID", "NAME", HeaderCaption);
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkManufacturer, dtManufacturerList, "NAME", "ID");
                        glkManufacturer.EditValue = glkManufacturer.Properties.GetKeyValue(0);
                        this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkManufacturer, resultArgs.DataSource.Table, "NAME", "ID");
                    }
                }
                else
                    ShowMessageBox(resultArgs.Message);
            }
        }

        private void LoadCustodians()
        {
            using (CustodiansSystem custodianSystem = new CustodiansSystem())
            {
                resultArgs = custodianSystem.FetchAllCustodiansDetails();
                if (resultArgs != null && resultArgs.Success)
                    this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkCustodian, resultArgs.DataSource.Table, custodianSystem.AppSchema.AssetCustodians.CUSTODIANColumn.ColumnName, custodianSystem.AppSchema.AssetCustodians.CUSTODIAN_IDColumn.ColumnName);
                else
                    ShowMessageBox(resultArgs.Message);
                GetMappedCustodianId = LoadMappedCustodian();
            }
        }

        private int LoadMappedCustodian()
        {
            int result = 0;
            using (CustodiansSystem custodianSystem = new CustodiansSystem())
            {
                custodianSystem.LocationID = glkLocation.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkLocation.EditValue.ToString()) : 0;
                resultArgs = custodianSystem.FetchCustodianNameByLocationID();
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null)
                {
                    result = resultArgs.DataSource.Table.Rows.Count > 0 ? UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][custodianSystem.AppSchema.AssetCustodians.CUSTODIAN_IDColumn.ColumnName].ToString()) : 0;
                }
            }
            return result;
        }

        private void LoadAssetItemDetails()
        {
            using (AssetItemSystem assetItem = new AssetItemSystem(AssetItemId))
            {
                Prefix = assetItem.Prefix;
                Sufix = assetItem.Suffix;
                Width = assetItem.Width;
                string TempID = GetLatestAssetItemRunnningNo();
                string ID = TempID;
                int NoExists = UtilityMember.NumberSet.ToInteger(Regex.Replace(TempID, @"\D+", ""));
                if (NoExists > 0)
                {
                    if (Prefix.Length > 0 && TempID.Length > Prefix.Length)
                        ID = TempID.Remove(0, Prefix.Length);
                    if (Sufix.Length > 0 && ID.Contains(Sufix) && ID.Length > Sufix.Length)
                        ID = ID.Remove(ID.Length - Sufix.Length);
                }
                StartingNo = tmpLastAssetNo = utilityMember.NumberSet.ToInteger(ID) > 0 ? utilityMember.NumberSet.ToInteger(ID) + 1 : assetItem.StartingNo;


                //                 StartingNo = tmpLastAssetNo = assetItem.StartingNo + GetLasterAssetItem();
                AssetMode = assetItem.AssetItemMode;
            }
        }

        private int GetLasterAssetItem()
        {
            //int LatestAssetId = 0;
            //using (AssetInwardOutwardSystem assetItem = new AssetInwardOutwardSystem())
            //{
            //    LatestAssetId = assetItem.FetchLatestAssetIdCount(AssetItemId);
            //}
            //return LatestAssetId;

            int LatestAssetId = 0;
            using (AssetInwardOutwardSystem assetItem = new AssetInwardOutwardSystem())
            {
                assetItem.ProjectId = this.ProjectId;
                resultArgs = assetItem.FetchLatestAssetIdCount(AssetItemId);
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    LatestAssetId = UtilityMember.NumberSet.ToInteger(Regex.Replace(resultArgs.DataSource.Table.Rows[0]["ASSET_ID"].ToString(), @"\D+", ""));
                }
            }
            return LatestAssetId;

        }

        private string GetLatestAssetItemRunnningNo()
        {
            string LatestAssetId = string.Empty;
            using (AssetInwardOutwardSystem assetItem = new AssetInwardOutwardSystem())
            {
                assetItem.ProjectId = this.ProjectId;
                resultArgs = assetItem.FetchLatestAssetIdCount(AssetItemId);
                if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    LatestAssetId = resultArgs.DataSource.Table.Rows[0]["ASSET_ID"].ToString();
                }
            }
            return LatestAssetId;
        }

        private CommonMember UtilityMember
        {
            get
            {
                if (utilityMember == null) { utilityMember = new CommonMember(); }
                return utilityMember;
            }
        }

        private Bosco.DAO.Schema.AppSchemaSet.ApplicationSchemaSet appSchema = null;
        private Bosco.DAO.Schema.AppSchemaSet.ApplicationSchemaSet AppSchemas
        {
            get { return appSchema = new Bosco.DAO.Schema.AppSchemaSet().AppSchema; }
        }

        private void ShowMessageBox(string Message)
        {
            //XtraMessageBox.Show(Message, "Acme.erp", MessageBoxButtons.OK, MessageBoxIcon.Information);
            XtraMessageBox.Show(Message, MessageRender.GetMessage(MessageCatalog.Common.COMMON_MESSAGE_TITLE), MessageBoxButtons.OK, MessageBoxIcon.Information);
        }
        #endregion

        /// <summary>
        /// 
        /// </summary>
        private void BindAssetItemList()
        {
            try
            {
                if (NoOfItems > 0)
                {
                    DataTable dtAssetList = null;
                    //Func Delegate to generate empty structure for the Asset list
                    //and Create n number of empty rows based on the value of NoOfItems variable
                    //Finaly return n number of empty rows 
                    Func<DataTable> GenerateAssetItem = () =>
                    {
                        dtAssetList = GenerateAssetItemStructure();
                        return dtAssetList = AssetMode == 1 ? Enumerable.Range(1, NoOfItems).Select(dr => dtAssetList.Rows.Add()).CopyToDataTable() : dtAssetList;
                    };

                    //Fecth the records from database and add it to the Dictionary.
                    //If the dtAssetList is null or row count of dtAssetList less than zero than it should be add mode
                    dtAssetList = LoadAssetListFromDB();
                    if (dtAssetList != null && dtAssetList.Rows.Count > 0)
                    {
                        //Adding Item to the collection
                        if (!SettingProperty.AssetListCollection.ContainsKey(RowNo))
                            SettingProperty.AssetListCollection.Add(RowNo, dtAssetList);
                        else
                            SettingProperty.AssetListCollection[RowNo] = dtAssetList;
                        //Update the temp variable and use when user closes the form without saving
                        dtTempAssetList = SettingProperty.AssetListCollection[RowNo];
                    }

                    if (SettingProperty.AssetListCollection.Count > 0)
                    {
                        dtAssetList = AnalyseAssetItemDeletion();
                        if (!SettingProperty.AssetListCollection.ContainsKey(RowNo))
                        {
                            dtAssetList = GenerateAssetItem();
                            dtAssetList = UpdateDataTableValues(dtAssetList);
                        }
                    }
                    else if (AssetInOut.PU == AssetInOutType || AssetInOut.IK == AssetInOutType || AssetInOut.OP == AssetInOutType)
                    {
                        //Generate new structure for new Asset Item and Initialize with default value 
                        dtAssetList = GenerateAssetItem();
                        dtAssetList = UpdateDataTableValues(dtAssetList);
                    }
                    if (AssetInOut.PU != AssetInOutType || AssetInOut.IK != AssetInOutType || AssetInOut.OP != AssetInOutType)
                    {
                        if (!dtAssetList.Columns.Contains("TEMP_AMOUNT"))
                        {
                            dtAssetList.Columns.Add("TEMP_AMOUNT", typeof(decimal));
                            dtAssetList.AsEnumerable().ToList<DataRow>().ForEach(dr =>
                            {
                                if (dr.RowState != DataRowState.Deleted)
                                {
                                    dr["TEMP_AMOUNT"] = dr["AMOUNT"];
                                }
                            });
                        }
                        SettingProperty.AssetListCollection[RowNo] = dtAssetList;
                    }
                    DataTable dtFinalSouce = dtAssetList.Rows.Count < NoOfItems ? MergeEmptyRows(dtAssetList) : dtAssetList;
                    gcItem.DataSource = dtFinalSouce;
                    IsItemGenerated = true; //Used for avoiding recreating of Asset Item Id while updating
                }
                else
                {
                    //ShowMessageBox("No of item should be greater than zero");
                    ShowMessageBox(MessageRender.GetMessage(MessageCatalog.Master.FinanceUIControls.UCASSET_LIST_NOITEM_SHOULDBE_GREATETHAN_ZERO));
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }

        }

        /// <summary>
        /// Fetch the records from database and store it in offline Dictionary collection,
        /// so that next time value can be retrieved from offline collection
        /// </summary>
        /// <returns></returns>
        private DataTable LoadAssetListFromDB()
        {
            DataTable dtAssetList = null;
            //Get records from Collection if it is available if not fetch the record from database
            if ((SettingProperty.AssetListCollection.ContainsKey(RowNo))) // && (AssetInOutType == AssetInOut.PU || AssetInOutType == AssetInOut.IK || AssetInOutType == AssetInOut.OP)))
            {
                dtAssetList = SettingProperty.AssetListCollection[RowNo];

                if (dtAssetList != null)
                {
                    if (AssetInOut.SL == AssetInOutType || AssetInOut.DS == AssetInOutType && AssetInOut.DN == AssetInOutType)
                    {
                        if (!dtAssetList.Columns.Contains("TEMP_AMOUNT"))
                            dtAssetList.Columns.Add("TEMP_AMOUNT", typeof(decimal));
                        if (!dtAssetList.Columns.Contains("GAIN_AMOUNT"))
                            dtAssetList.Columns.Add("GAIN_AMOUNT", typeof(decimal));
                        if (!dtAssetList.Columns.Contains("LOSS_AMOUNT"))
                            dtAssetList.Columns.Add("LOSS_AMOUNT", typeof(decimal));

                        dtAssetList.AsEnumerable().ToList<DataRow>().ForEach(dr =>
                        {
                            if (dr.RowState != DataRowState.Deleted)
                            {
                                double PurchaseAmount = 0;
                                double SalesAmountAmount = 0;
                                double GainAmount = 0;
                                double LossAmount = 0;

                                PurchaseAmount = this.UtilityMember.NumberSet.ToDouble(dr["TEMP_AMOUNT"].ToString());
                                SalesAmountAmount = this.UtilityMember.NumberSet.ToDouble(dr["AMOUNT"].ToString());
                                GainAmount = this.UtilityMember.NumberSet.ToDouble(dr["GAIN_AMOUNT"].ToString());
                                LossAmount = this.UtilityMember.NumberSet.ToDouble(dr["LOSS_AMOUNT"].ToString());

                                if (SalesAmountAmount > PurchaseAmount)  // Gain 
                                {
                                    // dr["AMOUNT"] = this.UtilityMember.NumberSet.ToDecimal((PurchaseAmount + GainAmount).ToString());
                                }
                                else if (PurchaseAmount > SalesAmountAmount)
                                {
                                    //  dr["AMOUNT"] = this.UtilityMember.NumberSet.ToDecimal((PurchaseAmount + LossAmount).ToString());
                                }
                            }
                        });
                    }
                }
            }
            else
            {
                using (AssetInwardOutwardSystem fetchAssetList = new AssetInwardOutwardSystem())
                {
                    fetchAssetList.ProjectId = ProjectId;
                    fetchAssetList.InOutDate = InoutDate;
                    fetchAssetList.InoutId = InOutDetailId; //InoutDetailId means that InOutId
                    //When Asset Type is PU or IK or OP than InoutDetailId =InOutDetailId
                    //Else Assign AssetItemId to InOutDetailId Field inorder to have a same column name in the quey
                    fetchAssetList.InoutDetailId = (AssetInOutType == AssetInOut.PU || AssetInOut.IK == AssetInOutType || AssetInOutType == AssetInOut.OP) ?
                                                    InOutDetailId : AssetItemId;
                    resultArgs = fetchAssetList.FetchAssetList(AssetInOutType);
                    if (resultArgs != null && resultArgs.Success)
                    {
                        dtAssetList = resultArgs.DataSource.Table;
                        if (!dtAssetList.Columns.Contains(SELECT))
                        {
                            dtAssetList.Columns.Add(SELECT, typeof(Int32));
                            if (!dtAssetList.Columns.Contains(AppSchemas.ASSETItem.ITEM_IDColumn.ColumnName))
                            {
                                dtAssetList.Columns.Add(AppSchemas.ASSETItem.ITEM_IDColumn.ColumnName, typeof(Int32));
                                dtAssetList.AsEnumerable().ToList<DataRow>().ForEach(dr =>
                                {
                                    if (dr.RowState != DataRowState.Deleted)
                                        dr[AppSchemas.ASSETItem.ITEM_IDColumn.ColumnName] = AssetItemId;
                                });
                            }
                        }
                        //Previously generated Item count should be greater than new n of Items to be created newly
                        if (dtAssetList.Rows.Count < NoOfItems)
                        {
                            MergeEmptyRows(dtAssetList);
                        }
                    }
                    else ShowMessageBox(resultArgs.Message);
                }
            }
            return dtAssetList;
        }

        /// <summary>
        /// Create n number of new extra items and merge the new records with the Previously collection of the same AssetId
        /// </summary>
        /// <param name="dtAssetList"></param>
        /// <returns></returns>
        private DataTable MergeEmptyRows(DataTable dtAssetList)
        {
            if (AssetInOutType == AssetInOut.PU || AssetInOut.IK == AssetInOutType || AssetInOutType == AssetInOut.OP)
            {
                // dtAssetList.Rows.Count == 0 ||
                StartingNo = StartingNo > dtAssetList.Rows.Count ? StartingNo : dtAssetList.Rows.Count + 1;
                // To Find the Last Asset Id No from the Temp Table Starts
                if (dtAssetList != null && dtAssetList.Rows.Count > 0)
                {
                    if (dtAssetList.AsEnumerable().Take(1).FirstOrDefault().RowState != DataRowState.Deleted)
                    {
                        DataRow drItem = dtAssetList.AsEnumerable().LastOrDefault();
                        if (drItem.RowState != DataRowState.Deleted)
                            tmpLastAssetNo = UtilityMember.NumberSet.ToInteger(Regex.Replace(drItem["ASSET_ID"].ToString(), @"\D+", ""));
                    }
                }
                if (StartingNo <= tmpLastAssetNo)
                {
                    //  StartingNo = ++tmpLastAssetNo;
                }
                // To Find the Last Asset Id No from the Temp Table ends

                //string FormatedNo = StartingNo.ToString().PadLeft(Width, '0').ToString();
                //int no = int.Parse(FormatedNo);
                Func<string> GenereateAssetId = () => Prefix + GenerateAssetId(StartingNo++, Width) + Sufix;
                DataTable dtnewAssetEmptyList = dtAssetList.Clone();
                //Create n number of empty Items
                //Formula,Let new no of Items be n,then
                //n = NoOfItems - PreviouslyTotalNoOfItems
                //Ex : n =25 - 20 
                //     n =5 so 5 Items will be created 
                dtnewAssetEmptyList = Enumerable.Range(1, NoOfItems - dtAssetList.Rows.Count).Select(dr => dtnewAssetEmptyList.Rows.Add()).CopyToDataTable();
                //Update the n number of empty
                dtnewAssetEmptyList.AsEnumerable().ToList<DataRow>().ForEach(dr =>
                {
                    if (dr.RowState != DataRowState.Deleted)
                    {
                        //AssetMode 1 =Auto Generate AssetId
                        if (AssetMode == 1 && string.IsNullOrEmpty(dr[AppSchemas.ASSETItem.ASSET_IDColumn.ColumnName].ToString()))
                            dr[AppSchemas.ASSETItem.ASSET_IDColumn.ColumnName] = GenereateAssetId.Invoke();
                        if (InOutDetailId == 0)
                        {
                            dr[AppSchemas.ASSETItem.ITEM_IDColumn.ColumnName] = AssetItemId;
                            dr[AppSchemas.ASSETItem.STATUSColumn.ColumnName] = 1; // Status 1 by default 1= Purchase in the add mode
                            dr[AppSchemas.AssetCustodians.CUSTODIAN_IDColumn.ColumnName] = GetMappedCustodianId;
                            dr[AppSchemas.ASSETLocationDetails.LOCATION_IDColumn.ColumnName] = glkLocation.Text == HeaderCaption ? 0 : GetLocationId;
                            dr["ID"] = glkManufacturer.Text == HeaderCaption ? 0 : GetManufacturerId;
                            dr[AppSchemas.AssetPurchaseDetail.AMOUNTColumn.ColumnName] = UtilityMember.NumberSet.ToDecimal(txtRatePerItem.Text);
                            dr[AppSchemas.AssetInOut.SALVAGE_VALUEColumn.ColumnName] = 0;

                        }
                        else
                        {
                            dr[AppSchemas.ASSETItem.ITEM_IDColumn.ColumnName] = AssetItemId;
                            dr[AppSchemas.ASSETItem.STATUSColumn.ColumnName] = 1; // Status 1 by default 1= Purchase in the add mode
                            dr["SELECT"] = 0; // SELECT 0 by default for validation
                        }
                    }
                });
                //Merge the newly created Items with Previous Items
                dtAssetList.Merge(dtnewAssetEmptyList);
            }
            return dtAssetList;
        }

        private string GenerateAssetId(int startNo, int width)
        {
            string no;
            no = startNo.ToString().PadLeft(width, '0');
            return no;
        }

        /// <summary>
        /// Find out if NoOfItems which are modified less than the available no of Items and mark last n of modified items to be selected
        /// Ex : Available Quantity =20 and Changed into 15 
        ///      Modified Quantity  = 20 -15 =5 
        ///      Mark last 5 Items to be checked
        /// </summary>
        /// <returns></returns>
        private DataTable AnalyseAssetItemDeletion()
        {
            //DataTable dtUnselectedAssetList = null;
            //try
            //{
            //    if (SettingProperty.AssetListCollection.ContainsKey(RowNo))
            //    {
            //        dtUnselectedAssetList = SettingProperty.AssetListCollection[RowNo];

            //        DataTable dtDeletedRecords = dtUnselectedAssetList.Clone();
            //        //Getting only the unselected Items
            //        var UndeletedRecords = from asset in dtUnselectedAssetList.AsEnumerable()
            //                               where asset.RowState != DataRowState.Deleted ? asset.Field<Int32?>(SELECT) != 1 : false
            //                               select asset;
            //        //Getting only the selected Items
            //        var DeletedRecords = from asset in dtUnselectedAssetList.AsEnumerable()
            //                             where asset.RowState != DataRowState.Deleted ? asset.Field<Int32?>(SELECT) == 1 : false
            //                             select asset;

            //        if (UndeletedRecords.Count() > 0) dtUnselectedAssetList = UndeletedRecords.CopyToDataTable();
            //        if (DeletedRecords.Count() > 0) dtDeletedRecords = DeletedRecords.CopyToDataTable();

            //        if (AssetInOutType == AssetInOut.PU || AssetInOut.IK == AssetInOutType || AssetInOutType == AssetInOut.OP)
            //        {
            //            if (dtUnselectedAssetList != null && dtUnselectedAssetList.Rows.Count > NoOfItems)
            //            {
            //                //Mark the Select column as 1 . 1= Checked ,0 or null = UnChecked
            //                int DeleteRowCount = dtUnselectedAssetList.Rows.Count - NoOfItems;
            //                //Mark last n Number of Item to be selected
            //                // SELECT =1 =>DELETE THE ITEM
            //                // SELECT =0 =>KEEP THE ITEM
            //                dtUnselectedAssetList.AsEnumerable().Reverse().ToList<DataRow>().ForEach(dr =>
            //                {
            //                    if (dr.RowState != DataRowState.Deleted)
            //                    {
            //                        if (UtilityMember.NumberSet.ToInteger(dr["STATUS"].ToString()) != 0)
            //                            if (DeleteRowCount != 0) { dr[SELECT] = 1; DeleteRowCount--; } else dr[SELECT] = 0;
            //                        else
            //                            dr[SELECT] = 1;
            //                    }
            //                });


            //                //Updating the Collection with the modified source
            //                if (dtDeletedRecords.Rows.Count > 0)
            //                {
            //                    dtDeletedRecords.Merge(dtUnselectedAssetList);
            //                }
            //                else dtDeletedRecords = dtUnselectedAssetList;
            //                                            SettingProperty.AssetListCollection[RowNo] = dtDeletedRecords;
            //                colRemove.Visible = true;
            //                lblDeleteStatusNote.Visibility = LayoutVisibility.Always;
            //                colRemove.VisibleIndex = 0;

            //            }
            //        }
            //        else //All asset Out Logics while editing
            //        {
            //            int TotalCount = dtUnselectedAssetList.AsEnumerable().Count(r => r.RowState != DataRowState.Deleted ? true : false);
            //            int SoldCount = dtUnselectedAssetList.AsEnumerable().Count(r => r.RowState != DataRowState.Deleted ? UtilityMember.NumberSet.ToInteger(r["STATUS"].ToString()) == 0 : false);
            //            if (TotalCount == SoldCount) NoOfItems = TotalCount;
            //            //Mark first n Number of Item to be selected other than Purchase
            //            // SELECT =1 =>Selected items
            //            // SELECT =0 =>Unselected items
            //            if (NoOfItems > SoldCount)
            //            {
            //                dtUnselectedAssetList.Select().Take(NoOfItems).ToList().ForEach(d =>
            //                {
            //                    if (d.RowState != DataRowState.Deleted)
            //                        d[SELECT] = 1;
            //                });
            //            }
            //            else
            //            {
            //                dtUnselectedAssetList.Select().ToList().ForEach(d =>
            //                {
            //                    if (d.RowState != DataRowState.Deleted)
            //                        if (UtilityMember.NumberSet.ToInteger(d["STATUS"].ToString()) == 0) d[SELECT] = 1;
            //                });
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    ShowMessageBox(ex.Message);
            //}
            //return dtUnselectedAssetList;
            DataTable dtUnselectedAssetList = null;
            try
            {
                if (SettingProperty.AssetListCollection.ContainsKey(RowNo))
                {
                    DataTable dttmpAssetList = new DataTable();
                    dtUnselectedAssetList = dttmpAssetList = SettingProperty.AssetListCollection[RowNo];
                    DataTable dtDeletedRecords = dtUnselectedAssetList.Clone();
                    //Getting only the unselected Items
                    var UndeletedRecords = from asset in dtUnselectedAssetList.AsEnumerable()
                                           where asset.RowState != DataRowState.Deleted ? asset.Field<Int32?>(SELECT) != 1 : false
                                           select asset;
                    //Getting only the selected Items
                    var DeletedRecords = from asset in dtUnselectedAssetList.AsEnumerable()
                                         where asset.RowState != DataRowState.Deleted ? asset.Field<Int32?>(SELECT) == 1 : false
                                         select asset;

                    if (UndeletedRecords.Count() > 0) dtUnselectedAssetList = UndeletedRecords.CopyToDataTable();
                    if (DeletedRecords.Count() > 0) dtDeletedRecords = DeletedRecords.CopyToDataTable();

                    if (AssetInOutType == AssetInOut.PU || AssetInOut.IK == AssetInOutType || AssetInOutType == AssetInOut.OP)
                    {
                        int Newvalue = 0;
                        int Oldvalue = 0;
                        int DeleteRowCount = 0;
                        int StsCount = 0;

                        if (dttmpAssetList != null)
                        {
                            foreach (DataRow dr in dtTempAssetList.Rows)
                            {
                                if (dr.RowState != DataRowState.Deleted)
                                    Oldvalue++;
                            }
                        }


                        if (dttmpAssetList != null && dttmpAssetList.Rows.Count > 0)
                        {
                            Newvalue = NoOfItems;

                            //Mark the Select column as 1 . 1= Checked ,0 or null = UnChecked
                            var StatusCount = from asset in dttmpAssetList.AsEnumerable()
                                              where asset.RowState != DataRowState.Deleted ? asset.Field<Int64?>("STATUS") == 0 : false
                                              select asset;
                            StsCount = StatusCount.Count();
                            if (Newvalue < Oldvalue)
                            {
                                //colRemove.Visible = true;
                                //lblDeleteStatusNote.Visibility = LayoutVisibility.Always;
                                //colRemove.VisibleIndex = 0;
                                EnableRemoveColumn = true;
                                DeleteRowCount = Oldvalue - Newvalue;
                                dttmpAssetList.AsEnumerable().ToList<DataRow>().ForEach(dr =>
                                {
                                    if (dr.RowState != DataRowState.Deleted)
                                    {
                                        if (UtilityMember.NumberSet.ToInteger(dr["STATUS"].ToString()) != 0)
                                        {
                                            dr[SELECT] = 0;
                                        }
                                        else
                                        {
                                            dr[SELECT] = 1;
                                        }
                                    }
                                });

                                if (StsCount == 0)
                                {
                                    dttmpAssetList.AsEnumerable().Reverse().ToList<DataRow>().ForEach(dr =>
                                    {
                                        if (dr.RowState != DataRowState.Deleted)
                                        {
                                            if (UtilityMember.NumberSet.ToInteger(dr["STATUS"].ToString()) != 0)
                                            {
                                                if (DeleteRowCount != 0)
                                                {
                                                    dr[SELECT] = 1;
                                                    DeleteRowCount--;
                                                }
                                                else dr[SELECT] = 0;
                                            }
                                            else
                                                dr[SELECT] = 1;
                                        }
                                    });
                                }
                                else
                                {
                                    int delcalc = Oldvalue - Newvalue;
                                    dttmpAssetList.AsEnumerable().Reverse().ToList<DataRow>().ForEach(dr =>
                                    {
                                        if (dr.RowState != DataRowState.Deleted)
                                        {
                                            if (UtilityMember.NumberSet.ToInteger(dr["STATUS"].ToString()) != 0)
                                            {
                                                if (delcalc != 0)
                                                {
                                                    dr[SELECT] = 1;
                                                    delcalc--;
                                                }
                                                else dr[SELECT] = 0;
                                            }
                                            else
                                                dr[SELECT] = 1;
                                        }
                                    });
                                }
                            }
                            else if (Oldvalue < Newvalue)
                            {
                                DeleteRowCount = Newvalue - Oldvalue;
                                dttmpAssetList.AsEnumerable().ToList<DataRow>().ForEach(dr =>
                                {
                                    if (dr.RowState != DataRowState.Deleted)
                                    {
                                        if (UtilityMember.NumberSet.ToInteger(dr["STATUS"].ToString()) != 0)
                                        {
                                            dr[SELECT] = 0;
                                        }
                                        else
                                        {
                                            dr[SELECT] = 1;
                                        }
                                    }
                                });
                            }
                            else
                            {
                                dttmpAssetList.AsEnumerable().ToList<DataRow>().ForEach(dr =>
                                {
                                    if (dr.RowState != DataRowState.Deleted)
                                    {
                                        if (UtilityMember.NumberSet.ToInteger(dr["STATUS"].ToString()) != 0)
                                        {
                                            dr[SELECT] = 0;
                                        }
                                        else
                                        {
                                            dr[SELECT] = 1;
                                        }
                                    }
                                });
                            }

                            dtUnselectedAssetList = dttmpAssetList;
                        }
                    }
                    else //All asset Out Logics while editing
                    {
                        int TotalCount = dttmpAssetList.AsEnumerable().Count(r => r.RowState != DataRowState.Deleted ? true : false);
                        int SoldCount = dttmpAssetList.AsEnumerable().Count(r => r.RowState != DataRowState.Deleted ? UtilityMember.NumberSet.ToInteger(r["STATUS"].ToString()) == 0 : false);
                        if (TotalCount == SoldCount) NoOfItems = TotalCount;
                        //Mark first n Number of Item to be selected other than Purchase
                        // SELECT =1 =>Selected items
                        // SELECT =0 =>Unselected items
                        if (NoOfItems > SoldCount)
                        {
                            dttmpAssetList.Select().Take(NoOfItems).ToList().ForEach(d =>
                                {
                                    if (d.RowState != DataRowState.Deleted)
                                        d[SELECT] = 1;
                                });
                        }
                        else
                        {
                            dttmpAssetList.Select().ToList().ForEach(d =>
                            {
                                if (d.RowState != DataRowState.Deleted)
                                    if (UtilityMember.NumberSet.ToInteger(d["STATUS"].ToString()) == 0) d[SELECT] = 1;
                            });
                        }
                        dtUnselectedAssetList = dttmpAssetList;
                    }
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
            return dtUnselectedAssetList;
        }

        /// <summary>
        /// Update value for the generated Items based on the selection of the users
        /// </summary>
        /// <param name="dtAssetList"></param>
        /// <returns></returns>
        private DataTable UpdateDataTableValues(DataTable dtAssetList)
        {
            try
            {
                if (dtAssetList != default(DataTable))
                {
                    //Using func delete to generate Asset Id which returns a genereted string based on Prefix and Sufix and Starting no
                    Func<string> GenereateAssetId = () => Prefix + StartingNo++ + Sufix;
                    dtAssetList.AsEnumerable().ToList<DataRow>().ForEach(dr =>
                        {
                            if (dr.RowState != DataRowState.Deleted)
                            {
                                if (!IsItemGenerated) { dr[AppSchemas.ASSETItem.ASSET_IDColumn.ColumnName] = GenereateAssetId.Invoke(); }
                                dr[AppSchemas.ASSETItem.ITEM_IDColumn.ColumnName] = AssetItemId;
                                dr[AppSchemas.AssetCustodians.CUSTODIAN_IDColumn.ColumnName] = LoadMappedCustodian();
                                dr[AppSchemas.ASSETLocationDetails.LOCATION_IDColumn.ColumnName] = glkLocation.Text == HeaderCaption ? 0 : GetLocationId;
                                dr["ID"] = glkManufacturer.Text == HeaderCaption ? 0 : GetManufacturerId;
                                if (dr["STATUS"].ToString() != "0")
                                {
                                    dr[AppSchemas.AssetPurchaseDetail.AMOUNTColumn.ColumnName] = UtilityMember.NumberSet.ToDecimal(txtRatePerItem.Text);
                                    dr[AppSchemas.AssetInOut.SALVAGE_VALUEColumn.ColumnName] = 0;
                                    dr["DEPRECIATION_AMOUNT"] = UtilityMember.NumberSet.ToDecimal(txtDepreciation.Text);

                                    dr["BALANCE"] = this.utilityMember.NumberSet.ToDouble(dr[AppSchemas.AssetPurchaseDetail.AMOUNTColumn.ColumnName].ToString()) - this.utilityMember.NumberSet.ToDouble(dr["DEPRECIATION_AMOUNT"].ToString());
                                }
                            }
                        });
                    //Updating Dictionary collection
                    SettingProperty.AssetListCollection[RowNo] = dtAssetList;
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
            return dtAssetList;
        }

        /// <summary>
        /// Create empty structure of Asset Items to be generated
        /// </summary>
        /// <returns></returns>
        private DataTable GenerateAssetItemStructure()
        {
            if (dtAssetItemStructure == default(DataTable))
            {
                dtAssetItemStructure = new DataTable(ASSET_TABLE_NAME);
                dtAssetItemStructure.Columns.Add(AppSchemas.ASSETItem.ITEM_IDColumn.ColumnName, typeof(Int32));
                dtAssetItemStructure.Columns.Add(AppSchemas.ASSETItem.ITEM_DETAIL_IDColumn.ColumnName, typeof(Int32));
                dtAssetItemStructure.Columns.Add(SELECT, typeof(Int32));
                dtAssetItemStructure.Columns.Add(AppSchemas.ASSETItem.ASSET_IDColumn.ColumnName, typeof(string));
                dtAssetItemStructure.Columns.Add(AppSchemas.ASSETLocationDetails.LOCATION_IDColumn.ColumnName, typeof(Int32));
                dtAssetItemStructure.Columns.Add(AppSchemas.AssetCustodians.CUSTODIAN_IDColumn.ColumnName, typeof(Int32));
                dtAssetItemStructure.Columns.Add("ID", typeof(Int32)); //manufacture Id
                dtAssetItemStructure.Columns.Add("DEPRECIATION_AMOUNT", typeof(decimal));
                dtAssetItemStructure.Columns.Add(AppSchemas.AssetPurchaseDetail.AMOUNTColumn.ColumnName, typeof(decimal));
                dtAssetItemStructure.Columns.Add("SALVAGE_VALUE", typeof(decimal));
                dtAssetItemStructure.Columns.Add(AppSchemas.AssetInOut.GAIN_AMOUNTColumn.ColumnName, typeof(decimal));
                dtAssetItemStructure.Columns.Add(AppSchemas.AssetInOut.LOSS_AMOUNTColumn.ColumnName, typeof(decimal));
                dtAssetItemStructure.Columns.Add("TEMP_AMOUNT", typeof(decimal));
                dtAssetItemStructure.Rows.Add();
            }
            return dtAssetItemStructure;
        }

        public void DeleteAssetList()
        {
            DataTable dtDeleteAssetList = null;
            if (SettingProperty.AssetListCollection.ContainsKey(RowNo))
            {
                dtDeleteAssetList = SettingProperty.AssetListCollection[RowNo];
                dtDeleteAssetList.Select().ToList().ForEach(dr => { if (dr.RowState != DataRowState.Deleted) dr[SELECT] = 1; });
                //Updating the Collection with the modified source
                SettingProperty.AssetListCollection.Remove(RowNo);
                //SettingProperty.AssetListCollection[RowNo] = dtDeleteAssetList;
                SettingProperty.AssetDeletedInoutIds += InOutDetailId.ToString() + ",";
            }
        }

        public ResultArgs DeleteInsuranceDetails(string AssettId)
        {
            resultArgs = new ResultArgs();
            int AssetItemDetailId = default(int);
            DataTable dtAssetDetail = gcItem.DataSource as DataTable;
            var ItemDetailId = dtAssetDetail.AsEnumerable().Where(dr => dr.Field<string>("ASSET_ID") == AssettId);
            if (ItemDetailId.Count() > 0)
            {
                dtAssetDetail = ItemDetailId.CopyToDataTable();
                if (dtAssetDetail != null && dtAssetDetail.Rows.Count > 0)
                    AssetItemDetailId = UtilityMember.NumberSet.ToInteger(dtAssetDetail.Rows[0]["ITEM_DETAIL_ID"].ToString());
                using (InsuranceRenewSystem insurancesystem = new InsuranceRenewSystem())
                {
                    resultArgs = insurancesystem.DeleteInsuranceDetailsByItemDetail(AssetItemDetailId.ToString());
                }
            }
            return resultArgs;
        }

        public bool CheckInsuranceDetailsExists(string AssettId)
        {
            bool isInsExists = false;
            int AssetItemDetailId = default(int);
            DataTable dtAssetDetail = gcItem.DataSource as DataTable;
            var ItemDetailId = dtAssetDetail.AsEnumerable().Where(dr => dr.Field<string>("ASSET_ID") == AssettId);
            if (ItemDetailId.Count() > 0)
            {
                dtAssetDetail = ItemDetailId.CopyToDataTable();
                if (dtAssetDetail != null && dtAssetDetail.Rows.Count > 0)
                    AssetItemDetailId = UtilityMember.NumberSet.ToInteger(dtAssetDetail.Rows[0]["ITEM_DETAIL_ID"].ToString());
                using (InsuranceRenewSystem insurancesystem = new InsuranceRenewSystem())
                {
                    isInsExists = (insurancesystem.IsInsuranceExists(AssetItemDetailId)) > 0 ? true : false;
                }
            }
            return isInsExists;
        }

        public ResultArgs ImportAssetItemOPBalance()
        {
            ResultArgs resultArgs = new ResultArgs();
            try
            {
                if (AssetItemId > 0)
                {
                    if (SettingProperty.AssetListCollection.ContainsKey(RowNo))
                    {
                        DataTable dt = SettingProperty.AssetListCollection[RowNo];
                        if (dt != null)
                        {
                            if (dt.AsEnumerable().Take(1).FirstOrDefault().RowState != DataRowState.Deleted)
                            {
                                DataRow drItem = dt.AsEnumerable().Take(1).FirstOrDefault();
                                int PrvItemId = UtilityMember.NumberSet.ToInteger(drItem["ITEM_ID"].ToString());
                                if (AssetItemId != PrvItemId)
                                {
                                    SettingProperty.AssetListCollection.Remove(RowNo);
                                }
                            }
                            else
                            {
                                SettingProperty.AssetListCollection.Remove(RowNo);
                            }
                        }
                    }
                    LoadAssetItemDetails();
                    txtRatePerItem.Text = Amount.ToString();
                    BindAssetItemList();
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
            return resultArgs;
        }

        public void setCursorPosition()
        {
            if (AssetInOutType == AssetInOut.SL)
            {
                colAssetId.OptionsColumn.AllowEdit = colAssetId.OptionsColumn.AllowFocus = false;
                gcItem.Select();
                gvItem.MoveFirst();
                gvItem.FocusedColumn = colAmount;
                // SendKeys.Send("{F2}");
            }
            else if (AssetInOutType == AssetInOut.DN || AssetInOutType == AssetInOut.DS)
            {
                // colAssetId.OptionsColumn.AllowEdit = colAssetId.OptionsColumn.AllowFocus = false;
                // colAmount.OptionsColumn.AllowEdit = colAmount.OptionsColumn.AllowFocus = false;
                //gcItem.Select();
                //gvItem.MoveFirst();
                // gvItem.FocusedColumn = colRemove;              

                //Enable column
                // colAssetId.OptionsColumn.AllowEdit = colAssetId.OptionsColumn.AllowFocus = false;
                gcItem.Select();
                gvItem.MoveFirst();
                // gvItem.FocusedColumn = colAmount;
                //gcItem.Select();
                //gvItem.MoveLast();
            }
            else
            {
                if (SettingProperty.AssetListCollection.ContainsKey(RowNo))
                {
                    DataTable dtItems = SettingProperty.AssetListCollection[RowNo];
                    if (dtItems != null && dtItems.Rows.Count > 0)
                    {
                        // var locCount = dtItems.AsEnumerable().Where(c => c.Field<UInt32?>("LOCATION_ID").Equals(0)); //.Count();
                        int sumvalue = utilityMember.NumberSet.ToInteger(dtItems.Compute("COUNT(LOCATION_ID)", "LOCATION_ID <> 0").ToString());
                        if (sumvalue > 0)
                        {
                            if (dtItems.Rows.Count == sumvalue)
                            {
                                gcItem.Select();
                                gvItem.MoveFirst();
                                gvItem.FocusedColumn = colAssetId;
                                gvItem.ShowEditor();
                                glkLocation.Select();
                                SendKeys.Send("{F2}");

                            }
                            else
                            {
                                glkLocation.Select();
                            }
                        }
                        else
                        {
                            //gcItem.Select();
                            //gvItem.MoveFirst();
                            //gvItem.FocusedColumn = colAssetId;
                            glkLocation.Select();
                        }
                    }
                }
            }
            SendKeys.Send("{F2}");
        }

        public void CheckLoactionExists()
        {
            if (SettingProperty.AssetListCollection.ContainsKey(RowNo))
            {
                DataTable dtItems = SettingProperty.AssetListCollection[RowNo];
                if (dtItems != null && dtItems.Rows.Count > 0)
                {
                    //  var locCount = dtItems.AsEnumerable().Where(c => !c.IsNull("LOCATION_ID") ? c.Field<UInt32>("LOCATION_ID").Equals(0) : false);
                    int sumvalue = utilityMember.NumberSet.ToInteger(dtItems.Compute("COUNT(LOCATION_ID)", "LOCATION_ID <> 0").ToString());
                    double SumItemAmount = utilityMember.NumberSet.ToDouble(dtItems.Compute("SUM(AMOUNT)", string.Empty).ToString());
                    if ((sumvalue != 0 && GetLocationId == 0 && GetManufacturerId == 0) || SumItemAmount > 0)
                    {
                        //if (this.ShowConfirmationMessage("Locations / Manufacturer is already filled below.Do you want to empty all the Location and Manufacturer", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        if (this.ShowConfirmationMessage(MessageRender.GetMessage(MessageCatalog.Master.FinanceUIControls.UCASSET_LIST_LOCATINS_ALREADY_FILLED_INFO), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            gcItem.DataSource = UpdateDataTableValues((gcItem.DataSource as DataTable));
                        }
                        else
                        {
                            gcItem.Select();
                            gvItem.MoveFirst();
                            gvItem.FocusedColumn = colAssetId;
                        }
                    }
                    else
                    {
                        gcItem.DataSource = UpdateDataTableValues((gcItem.DataSource as DataTable));
                        if (AssetInOutType != AssetInOut.OP)
                        {
                            if (AssetMode == 1)
                                colAssetId.OptionsColumn.AllowEdit = colAssetId.OptionsColumn.AllowFocus = false;
                            gcItem.Select();
                            gvItem.MoveFirst();
                            gvItem.FocusedColumn = colAmount;
                            gvItem.ShowEditor();
                            SendKeys.Send("{F2}");
                        }
                        else
                        {
                            colAssetId.OptionsColumn.AllowEdit = colAssetId.OptionsColumn.AllowFocus = true;
                            gcItem.Select();
                            gvItem.MoveFirst();
                            gvItem.FocusedColumn = colAssetId;
                            gvItem.ShowEditor();
                            SendKeys.Send("{F2}");
                        }
                    }
                }
            }
        }

        private DataTable ConstructEmptyOPStructure()
        {
            DataTable dtPurchaseVouhcerDetail = new DataTable();
            dtPurchaseVouhcerDetail.Columns.Add("ITEM_ID", typeof(string));
            dtPurchaseVouhcerDetail.Columns.Add("QUANTITY", typeof(int));
            dtPurchaseVouhcerDetail.Columns.Add("AVAILABLE_QUANTITY", typeof(Int32));
            dtPurchaseVouhcerDetail.Columns.Add("RATE", typeof(decimal));
            dtPurchaseVouhcerDetail.Columns.Add("AMOUNT", typeof(decimal));
            dtPurchaseVouhcerDetail.Columns.Add("IN_OUT_DETAIL_ID", typeof(int));
            //dtPurchaseVouhcerDetail.Columns.Add("LEDGER_ID", typeof(int));
            //dtPurchaseVouhcerDetail.Columns.Add("CHEQUE_NO", typeof(string));
            //dtPurchaseVouhcerDetail.Columns.Add("MATERIALIZED_ON", typeof(DateTime));
            return dtPurchaseVouhcerDetail;
        }

        public void AssignItemDetails()
        {
            BindAssetItemList();
        }

        public bool CheckAssetIDExistsDB()
        {
            bool isvalid = true;
            ResultArgs rArgs = new ResultArgs();
            if (SettingProperty.AssetListCollection.ContainsKey(RowNo))
            {
                DataTable dtAssetDetails = SettingProperty.AssetListCollection[RowNo];
                using (AssetInwardOutwardSystem inwardoutward = new AssetInwardOutwardSystem())
                {
                    int ItemDetailID = 0;
                    foreach (DataRow drItem in dtAssetDetails.Rows)
                    {
                        if (drItem.RowState != DataRowState.Deleted)
                        {
                            ItemDetailID = drItem["ITEM_DETAIL_ID"] != null ? UtilityMember.NumberSet.ToInteger(drItem["ITEM_DETAIL_ID"].ToString()) : 0;
                            if (!string.IsNullOrEmpty(drItem["ITEM_ID"].ToString().Trim()) && !string.IsNullOrEmpty(drItem["ASSET_ID"].ToString().Trim()))
                            {
                                rArgs = inwardoutward.CheckAssetItemIdExists(UtilityMember.NumberSet.ToInteger(drItem["ITEM_ID"].ToString().Trim()), drItem["ASSET_ID"].ToString().Trim(), ProjectId, ItemDetailID);

                                if (rArgs != null && !string.IsNullOrEmpty(rArgs.DataSource.Sclar.ToString))
                                {
                                    isvalid = false;
                                    this.ShowMessageBox("Asset Id [ " + drItem["ASSET_ID"].ToString().Trim() + " ] exists already for this Item.");

                                    int rowHandle = GetRowHandleByColumnValue(gvItem, "ASSET_ID", drItem["ASSET_ID"].ToString());
                                    if (rowHandle != GridControl.InvalidRowHandle)
                                    {
                                        gvItem.FocusedColumn = gvItem.Columns.ColumnByFieldName("ASSET_ID");
                                        gvItem.FocusedRowHandle = rowHandle;
                                        gvItem.ShowEditor();
                                    }
                                    break;
                                }
                            }
                            else
                            {
                                isvalid = false;
                                //this.ShowMessageBox("Asset Id is empty.");
                                this.ShowMessageBox(MessageRender.GetMessage(MessageCatalog.Master.FinanceUIControls.UCASSET_LIST_ASSETID_EMPTY));
                            }
                        }
                    }
                }
            }
            return isvalid;
        }

        private int GetRowHandleByColumnValue(GridView view, string ColumnFieldName, string value)
        {
            int result = GridControl.InvalidRowHandle;
            for (int i = 0; i < view.RowCount; i++)
                if (view.GetDataRow(i)[ColumnFieldName].Equals(value))
                    return i;
            return result;
        }

        private void RealColumnEditAssetAmount()
        {
            colAmount.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditAssetAmount_EditValueChanged);
            this.gvItem.MouseDown += (object sender, MouseEventArgs e) =>
            {
                DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hitInfo = gvItem.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colAmount)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvItem.ShowEditorByMouse();
                    }));
                }
            };
        }

        void RealColumnEditAssetAmount_EditValueChanged(object sender, System.EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null) return;
            gvItem.PostEditor();
            gvItem.UpdateCurrentRow();
            if (gvItem.ActiveEditor == null)
                gvItem.ShowEditor();

            int FocusedColumn = gvItem.FocusedRowHandle;
            if (FocusedColumn >= 0)
            {
                double balance = ItemAmount - ItemDepAmt;
                if (balance.ToString() != string.Empty) { gvItem.SetRowCellValue(gvItem.FocusedRowHandle, colBalance, balance); }
            }
        }

        private void RealColumnEditDepAssetAmount()
        {
            colDepreciation.RealColumnEdit.EditValueChanged += new System.EventHandler(RealColumnEditAssetDepAmount_EditValueChanged);
            this.gvItem.MouseDown += (object sender, MouseEventArgs e) =>
            {
                DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hitInfo = gvItem.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colDepreciation)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvItem.ShowEditorByMouse();
                    }));
                }
            };
        }

        void RealColumnEditAssetDepAmount_EditValueChanged(object sender, System.EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null) return;
            gvItem.PostEditor();
            gvItem.UpdateCurrentRow();
            if (gvItem.ActiveEditor == null)
                gvItem.ShowEditor();

            int FocusedColumn = gvItem.FocusedRowHandle;
            if (FocusedColumn >= 0)
            {
                double balance = ItemAmount - ItemDepAmt;
                if (balance.ToString() != string.Empty) { gvItem.SetRowCellValue(gvItem.FocusedRowHandle, colBalance, balance); }
            }
        }
        #endregion
    }
}
