using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Model;
using Bosco.Utility;
using Bosco.Model.Inventory;
using Bosco.Model.UIModel;
using Bosco.Utility.CommonMemberSet;
using System.Text.RegularExpressions;
using System.Reflection;
using Bosco.DAO.Schema;
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;

namespace ACPP.Modules.Inventory.Asset
{
    public partial class frmUpdateAssetDetails : frmFinanceBase
    {
        #region Variable Decelaration

        ResultArgs resultArgs = new ResultArgs();
        private AssetStockProduct.IVendorManufacture IVendor = null;
        FinanceModule FrmModule;
        private int Rowindex = 0;
        ApplicationSchema appSchema = new ApplicationSchema();
        List<int> selectedRows = new List<int>();
        const string HeaderCaption = "";
        #endregion

        #region Constructor
        public frmUpdateAssetDetails()
        {
            InitializeComponent();
        }

        #endregion

        #region Properties
        private int ItemId = 0;
        private int ItemID
        {
            get
            {
                Rowindex = gvUpdateAssetDetails.FocusedRowHandle;
                ItemId = gvUpdateAssetDetails.GetFocusedRowCellValue(colItemDetailId) != null ? this.UtilityMember.NumberSet.ToInteger(gvUpdateAssetDetails.GetFocusedRowCellValue(colItemDetailId).ToString()) : 0;
                return ItemId;
            }
            set
            {
                ItemID = value;
            }
        }

        private int InsuranceId = 0;
        private int InsuranceID
        {
            get
            {
                Rowindex = gvUpdateAssetDetails.FocusedRowHandle;
                InsuranceId = gvUpdateAssetDetails.GetFocusedRowCellValue(colInsuranceID) != null ? this.UtilityMember.NumberSet.ToInteger(gvUpdateAssetDetails.GetFocusedRowCellValue(colInsuranceID).ToString()) : 0;
                return InsuranceId;
            }
            set
            {
                InsuranceID = value;
            }
        }

        private int Insuranceapplicable = 0;
        private int InsuranceApplicable
        {
            get
            {
                Rowindex = gvUpdateAssetDetails.FocusedRowHandle;
                Insuranceapplicable = gvUpdateAssetDetails.GetFocusedRowCellValue(colInsuranceApplicable) != null ? this.UtilityMember.NumberSet.ToInteger(gvUpdateAssetDetails.GetFocusedRowCellValue(colInsuranceApplicable).ToString()) : 0;
                return Insuranceapplicable;
            }
            set
            {
                InsuranceApplicable = value;
            }
        }

        private string Assetid = string.Empty;
        private string AssetID
        {
            get
            {
                Rowindex = gvUpdateAssetDetails.FocusedRowHandle;
                Assetid = gvUpdateAssetDetails.GetFocusedRowCellValue(colAssetID).ToString();
                return Assetid;
            }
            set
            {
                AssetID = value;
            }
        }

        private int LocationId = 0;
        private int LocationID
        {
            get
            {
                Rowindex = gvUpdateAssetDetails.FocusedRowHandle;
                LocationId = gvUpdateAssetDetails.GetFocusedRowCellValue(colLocationID) != null ? this.UtilityMember.NumberSet.ToInteger(gvUpdateAssetDetails.GetFocusedRowCellValue(colLocationID).ToString()) : 0;
                return LocationId;
            }
            set
            {
                LocationID = value;
            }
        }
       
        private string Assetitem = string.Empty;
        private string AssetItem
        {
            get
            {
                Rowindex = gvUpdateAssetDetails.FocusedRowHandle;
                Assetitem = gvUpdateAssetDetails.GetFocusedRowCellValue(colAssetItem).ToString();
                return Assetitem;
            }
            set
            {
                AssetItem = value;
            }
        }

        public int InOutDetailId
        {
            get { return gvUpdateAssetDetails.GetFocusedRowCellValue(colInoutDetailID) != null ? this.UtilityMember.NumberSet.ToInteger(gvUpdateAssetDetails.GetFocusedRowCellValue(colInoutDetailID).ToString()) : 0; }
        }
        public int ManufactureId { get; set; }
        private int GetManufacturerId { get { return glkManufacturer.EditValue != null ? UtilityMember.NumberSet.ToInteger(glkManufacturer.EditValue.ToString()) : 0; } }
        #endregion

        #region Events
        /// <summary>
        /// Load Asset Details and Load the Deafualt Values
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmUpdateAssetDetails_Load(object sender, EventArgs e)
        {
            ConstructAssetUpdate();
            ProjectDetials();
            LoadLocationDetails();
          //  LoadManufacturerDetails();
            LoadCustodianDetails();
            LoadAssetConditon();
            FetchAssetItemDetails();
            LoadManufacturers();
          //  glkManufacturer.EditValue = ManufactureId;
        }

        /// <summary>
        /// Load asset details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DataTable dt = null;
            try
            {
                using (AssetItemSystem assetItemSystem = new AssetItemSystem())
                {
                    ShowWaitDialog();
                    assetItemSystem.ProjectId = this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString());
                    dt = gcUpdateAssetDetails.DataSource as DataTable;
                    assetItemSystem.dtUpdateAssetDetails= dt.GetChanges(DataRowState.Modified);
                    resultArgs = assetItemSystem.UpdateAssetDetails();
                    CloseWaitDialog();
                    if (resultArgs.Success)
                    {
                        this.ShowSuccessMessage(this.GetMessage(MessageCatalog.Asset.AssetItem.ASSET_UPDATE_DETAILS));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
        }
        /// <summary>
        /// Load Update Asset Details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBar1_RefreshClicked(object sender, EventArgs e)
        {
            FetchAssetItemDetails();
        }

        /// <summary>
        /// Close the form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ucToolBar1_CloseClicked(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Load Asset Item add form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void rbtiInsurance_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        //{
        //    frmAssetItemAdd assetItemAdd = new frmAssetItemAdd(ItemID);
        //    assetItemAdd.ShowDialog();
        //    FetchAssetItemDetails();
        //}

        /// <summary>
        /// Load update asset items
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void OnUpdateHeld(object sender, EventArgs e)
        {
            FetchAssetItemDetails();
        }

        /// <summary>
        /// Load AMC form 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        //private void rbiAMC_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        //{
        //    frmAssetItemAdd frmAssetItemAdd = new frmAssetItemAdd(ItemID);
        //    frmAssetItemAdd.UpdateHeld += new EventHandler(OnUpdateHeld);
        //    frmAssetItemAdd.ShowDialog();
        //}

        //private void rbiLocation_ButtonClick(object sender, DevExpress.XtraEditors.Controls.ButtonPressedEventArgs e)
        //{
        //    frmLocationsAdd Location = new frmLocationsAdd(LocationID, FormMode.Edit, FrmModule);
        //    Location.UpdateHeld += new EventHandler(OnUpdateHeld);
        //    Location.ShowDialog();
        //}

        /// <summary>
        /// Load asset detail by project ID
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnApply_Click(object sender, EventArgs e)
        {
            UpdateAccountLedgers();
        }


        /// <summary>
        /// close this form
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        /// <summary>
        /// Add Insurance Detials 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void rbtiInsurance_Click(object sender, EventArgs e)
        {
            try
            {
                int mode = 0;
                if (InsuranceApplicable != 0)
                {
                    if (InsuranceID != 0)
                    {
                        mode = 1;
                    }
                    frmRenewInsuranceVoucherAdd insuranceAdd = new frmRenewInsuranceVoucherAdd(InsuranceID, ItemID, AssetItem, AssetID, mode, null, null);
                    insuranceAdd.UpdateHeld += new EventHandler(OnUpdateHeld);
                    insuranceAdd.ShowDialog();
                }
                else
                {
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }

        }

        /// <summary>
        /// shortcut keys
        /// </summary>
        /// <param name="e"></param>
        private void ProcessShortcutKeys(KeyEventArgs e)
        {
            if (e.KeyData == (Keys.Alt | Keys.U))
            {
            }
            else if (e.KeyData == (Keys.Alt | Keys.U))
            {

            }
        }

        /// <summary>
        /// Load asset details
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void frmUpdateAssetDetails_KeyDown(object sender, KeyEventArgs e)
        {
            ProcessShortcutKeys(e);
        }

        /// <summary>
        /// Show Filter in grid
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void chkShowFilter_CheckedChanged(object sender, EventArgs e)
        {
            gvUpdateAssetDetails.OptionsView.ShowAutoFilterRow = chkShowFilter.Checked;
            if (chkShowFilter.Checked)
            {
                this.SetFocusRowFilter(gvUpdateAssetDetails, colAssetClass);
            }
        }

        /// <summary>
        /// Sold items are Higlight in color
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gvUpdateAssetDetails_RowStyle(object sender, DevExpress.XtraGrid.Views.Grid.RowStyleEventArgs e)
        {                
            if (gvUpdateAssetDetails.GetRowCellValue(e.RowHandle, colFlag) != null)
            {
                if (gvUpdateAssetDetails.GetRowCellValue(e.RowHandle, colFlag).ToString() != string.Empty)
                                                                                     {
                    string Status = gvUpdateAssetDetails.GetRowCellValue(e.RowHandle, colFlag).ToString();                                                                                                                                                    
                    if (Status == "0")
                    {
                        e.Appearance.BackColor = Color.Red;
                    }
                }
            }
        }
        //private void gvUpdateAssetDetails_RowCountChanged(object sender, EventArgs e)
        //{
        //    lblCount.Text = gvUpdateAssetDetails.RowCount.ToString();
        //}

        #endregion

        #region Methods
        /// <summary>
        /// Construct grid values
        /// </summary>
        private void ConstructAssetUpdate()
        {
            try
            {
                DataTable dtUpdateAsset = new DataTable();
                dtUpdateAsset.Columns.Add("CLASS_ID", typeof(Int32));
                dtUpdateAsset.Columns.Add("ITEM_DETAIL_ID", typeof(Int32));
                dtUpdateAsset.Columns.Add("AESST_ID", typeof(string));
                dtUpdateAsset.Columns.Add("LOCAITON_ID", typeof(Int32));
                dtUpdateAsset.Columns.Add("MANUFACTURER_ID", typeof(Int32));
                dtUpdateAsset.Columns.Add("CUSTODIAN_ID", typeof(Int32));
                dtUpdateAsset.Columns.Add("CONDITIONS", typeof(string));
                dtUpdateAsset.Columns.Add("ASSET_ID", typeof(string));
                dtUpdateAsset.Columns.Add("INSURANCE_ID", typeof(Int32));
                dtUpdateAsset.Columns.Add("IS_INSURANCE", typeof(Int32));
                gcUpdateAssetDetails.DataSource = dtUpdateAsset;
                gvUpdateAssetDetails.AddNewRow();
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message + Environment.NewLine + ex.Source, true);
            }
            finally { }
        }

        /// <summary>
        /// Load Custodians Details
        /// </summary>
        private void LoadCustodianDetails()
        {
            try
            {
                using (CustodiansSystem custodianSystem = new CustodiansSystem())
                {
                    resultArgs = custodianSystem.FetchAllCustodiansDetails();
                    if (resultArgs.DataSource.Table.Rows.Count > 0 && resultArgs != null)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpCustodian, resultArgs.DataSource.Table, custodianSystem.AppSchema.AssetCustodians.CUSTODIANColumn.ColumnName, custodianSystem.AppSchema.AssetCustodians.CUSTODIAN_IDColumn.ColumnName);

                        this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpCustodian, resultArgs.DataSource.Table, custodianSystem.AppSchema.AssetCustodians.CUSTODIANColumn.ColumnName, custodianSystem.AppSchema.AssetCustodians.CUSTODIAN_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
            }
        }

        /// <summary>
        /// Load Asset condition from enum command
        /// </summary>
        private void LoadAssetConditon()
        {
            try
            {
                AssetCondition assetCondition = new AssetCondition();
                DataTable dtLedgerComponent = GetDescriptionfromEnumType(assetCondition);
                if (dtLedgerComponent.Rows.Count > 0 && resultArgs != null)
                {
                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpCondition, dtLedgerComponent, "CONDITIONS", "CONDITIONS");

                    this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpCondition, dtLedgerComponent, "CONDITIONS", "CONDITIONS");
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
            }
        }

        /// <summary>
        /// Lock Insurance Button
        /// </summary>
        private void LockInsurance()
        {
            //if(in
        }

        /// <summary>
        /// Convert enum command to data table
        /// </summary>
        /// <param name="enumType"></param>
        /// <returns></returns>
        public DataTable GetDescriptionfromEnumType(Enum enumType)
        {
            DataTable dtConditions = new DataTable();
            dtConditions.Columns.Add("ID", typeof(int));
            dtConditions.Columns.Add("CONDITIONS", typeof(string));

            if (enumType != null)
            {
                int i = 0;
                string[] names = enumType.GetType().GetEnumNames();
                foreach (string name in names)
                {
                    FieldInfo fi = enumType.GetType().GetField(name);
                    object[] da = fi.GetCustomAttributes(typeof(DescriptionAttribute), true);
                    foreach (DescriptionAttribute ds in da)
                    {
                        dtConditions.Rows.Add(i, ds.Description.ToString());
                    }
                }
                dtConditions.AcceptChanges();
            }
            return dtConditions;
        }

        public void SetTitle()
        {
            this.Text = this.GetMessage(MessageCatalog.Asset.FixeAssetRegister.UPDATE_ASSET_DETAIL_TITLE);
        }

        /// <summary>
        /// Load Project Details 
        /// </summary>
        private void ProjectDetials()
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
                        glkpProject.EditValue = this.AppSetting.UserProjectId;
                    }
                    else
                    {
                        XtraMessageBox.Show(resultArgs.Message);
                    }
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
        }

        /// <summary>
        /// Load Location Details
        /// </summary>
        private void LoadLocationDetails()
        {
            try
            {
                using (LocationSystem locationSystem = new LocationSystem())
                {
                    ResultArgs resultArgs = new ResultArgs();
                    locationSystem.ProjectId = glkpProject.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;
                    resultArgs = locationSystem.FetchLocationByProject();
                    if (resultArgs.DataSource.Table.Rows.Count > 0 && resultArgs != null)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkpLocation, resultArgs.DataSource.Table, locationSystem.AppSchema.ASSETLocationDetails.LOCATIONColumn.ColumnName, locationSystem.AppSchema.ASSETLocationDetails.LOCATION_IDColumn.ColumnName);

                        this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpLocation, resultArgs.DataSource.Table, locationSystem.AppSchema.ASSETLocationDetails.LOCATIONColumn.ColumnName, locationSystem.AppSchema.ASSETLocationDetails.LOCATION_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.Message, true);
            }
        }
        /// <summary>
        /// Load Manufacturer
        /// </summary>
        //private void LoadManufacturers()
        //{
        //    using (ManufactureInfoSystem manufacturerSystem = new ManufactureInfoSystem())
        //    {
        //        resultArgs = manufacturerSystem.FetchDetails();
        //        if (resultArgs != null && resultArgs.Success)
        //        {
        //            using (CommonMethod SelectAll = new CommonMethod())
        //            {                        
        //                DataTable dtManufacturerList = SelectAll.AddHeaderColumn(resultArgs.DataSource.Table.Copy(), "ID", "NAME", HeaderCaption);
        //                this.UtilityMember.ComboSet.BindGridLookUpCombo(glkManufacturer, dtManufacturerList, "NAME", "ID");
        //                glkManufacturer.EditValue = glkManufacturer.Properties.GetKeyValue(0);
        //                this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpManufacturer, resultArgs.DataSource.Table, "NAME", "ID");
        //            }
        //        }
        //        else
        //            ShowMessageBox(resultArgs.Message);
        //    }
        //}

        private void LoadManufacturers()
        {
            try
            {
                using (ManufactureInfoSystem manufacturerSystem = new ManufactureInfoSystem())
                {
                    resultArgs = manufacturerSystem.FetchDetails();
                    if (resultArgs.DataSource.Table.Rows.Count > 0 && resultArgs != null)
                    {
                        this.UtilityMember.ComboSet.BindGridLookUpCombo(glkManufacturer, resultArgs.DataSource.Table, manufacturerSystem.AppSchema.Manufactures.MANUFACTURERColumn.ColumnName, manufacturerSystem.AppSchema.Manufactures.MANUFACTURER_IDColumn.ColumnName);

                        this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpManufacturer, resultArgs.DataSource.Table, manufacturerSystem.AppSchema.Manufactures.MANUFACTURERColumn.ColumnName, manufacturerSystem.AppSchema.Manufactures.MANUFACTURER_IDColumn.ColumnName);
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Source + Environment.NewLine + ex.Message);
            }
        }
        /// <summary>
        /// Load Manufacturer Details
        /// </summary>
        //private void LoadManufacturerDetails()
        //{
        //    try
        //    {
        //        using (ManufactureInfoSystem manufacturerSystem = new ManufactureInfoSystem())
        //        {
        //            resultArgs = manufacturerSystem.FetchDetails();
        //            if (resultArgs.DataSource.Table.Rows.Count > 0 && resultArgs != null)
        //            {
        //                using (CommonMethod SelectAll = new CommonMethod())
        //                {
        //                    DataTable dtManufacturerList = SelectAll.AddHeaderColumn(resultArgs.DataSource.Table.Copy(), "ID", "NAME", HeaderCaption);
        //                    this.UtilityMember.ComboSet.BindGridLookUpCombo(glkManufacturer, dtManufacturerList, "NAME", "ID");
        //                    glkManufacturer.EditValue = glkManufacturer.Properties.GetKeyValue(0);
        //                    this.UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpManufacturer, resultArgs.DataSource.Table, manufacturerSystem.AppSchema.Manufactures.MANUFACTURERColumn.ColumnName, manufacturerSystem.AppSchema.Manufactures.MANUFACTURER_IDColumn.ColumnName);
        //                }
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageRender.ShowMessage(ex.Message, true);
        //    }
        //}

        /// <summary>
        /// Load asset item details
        /// </summary>
        private void FetchAssetItemDetails()
        {
            using (AssetItemSystem assetItemSystem = new AssetItemSystem())
            {
                assetItemSystem.ProjectId = glkpProject.EditValue != null ? this.UtilityMember.NumberSet.ToInteger(glkpProject.EditValue.ToString()) : 0;
                resultArgs = assetItemSystem.FetchUpdateAssetDetails();
                if (resultArgs.Success && resultArgs.DataSource.Table.Rows.Count > 0)
                {
                    gcUpdateAssetDetails.DataSource = resultArgs.DataSource.Table;
                    gvUpdateAssetDetails.RefreshData();
                }
                else
                {
                    gcUpdateAssetDetails.DataSource = null;
                }
            }
        }
        #endregion

        private void gvUpdateAssetDetails_ShowingEditor(object sender, CancelEventArgs e)
        {
            if (gvUpdateAssetDetails.GetRowCellValue(gvUpdateAssetDetails.FocusedRowHandle, colFlag) != null)
            {
                if (gvUpdateAssetDetails.GetRowCellValue(gvUpdateAssetDetails.FocusedRowHandle, colFlag).ToString() != string.Empty)
                {
                    string Status = (string)gvUpdateAssetDetails.GetRowCellValue(gvUpdateAssetDetails.FocusedRowHandle, colFlag).ToString();
                    if (Status == "0" && gvUpdateAssetDetails.FocusedColumn == colrbiInsurance)
                    {
                        e.Cancel = true; //Disabling the editing of the cell 
                        //this.ShowMessageBox("This Item has been Sold or Diposed or Donated");
                        this.ShowMessageBox(MessageCatalog.Asset.SalesVoucher.UPDATE_SALES_DISPOSE_INFO);
                    }
                }
            }
        }

        private void glkpProject_EditValueChanged(object sender, EventArgs e)
        {
            FetchAssetItemDetails();
            LoadLocationDetails();
        }
        private DataTable UpdateAccountLedgers()
        {
            DataTable dtSelectedList = new DataTable();
            DataTable dtDataSource = gcUpdateAssetDetails.DataSource as DataTable;
            if (!string.IsNullOrEmpty(glkpLocation.Text) && !string.IsNullOrEmpty(glkpCustodian.Text) && !string.IsNullOrEmpty(glkpCondition.Text))
            {
                if (dtDataSource != null && dtDataSource.Rows.Count > 0)
                {
                    int[] SelectedIds = gvUpdateAssetDetails.GetSelectedRows();
                    if (gvUpdateAssetDetails.SelectedRowsCount > 0)
                    {
                        dtSelectedList = dtDataSource.Clone();
                        if (SelectedIds.Count() > 0)
                        {
                            foreach (int RowIndex in SelectedIds)
                            {
                                DataRow drUpdateAssetDetaisl = gvUpdateAssetDetails.GetDataRow(RowIndex);
                                if (drUpdateAssetDetaisl != null)
                                {
                                    drUpdateAssetDetaisl[appSchema.Manufacture.MANUFACTURER_IDColumn.ColumnName] = glkManufacturer.EditValue;
                                    drUpdateAssetDetaisl[appSchema.AssetLocation.LOCATION_IDColumn.ColumnName] = glkpLocation.EditValue;
                                    drUpdateAssetDetaisl[appSchema.AssetCustodians.CUSTODIAN_IDColumn.ColumnName] = glkpCustodian.EditValue;
                                    drUpdateAssetDetaisl["CONDITIONS"] = glkpCondition.EditValue;
                                }
                            }
                            dtSelectedList.DefaultView.Sort = dtDataSource.DefaultView.Sort;
                        }
                    }
                    else
                    {
                        UpdateDataTableValues(dtDataSource);
                    }
                }
            }
            return dtSelectedList;
        }
        private DataTable UpdateDataTableValues(DataTable dtAssetItemMapping)
        {
            try
            {
                if (dtAssetItemMapping != default(DataTable))
                {
                    dtAssetItemMapping.AsEnumerable().ToList<DataRow>().ForEach(dr =>
                    {
                        if (dr.RowState != DataRowState.Deleted)
                        {
                            dr[appSchema.AssetLocation.LOCATION_IDColumn.ColumnName] = glkpLocation.EditValue;
                            dr[appSchema.AssetCustodians.CUSTODIAN_IDColumn.ColumnName] = glkpCustodian.EditValue;
                            dr["CONDITIONS"] = glkpCondition.EditValue;
                            dr[appSchema.Manufacture.MANUFACTURER_IDColumn.ColumnName] = glkManufacturer.EditValue;
                        }
                    });
                }
            }
            catch (Exception ex)
            {
                ShowMessageBox(ex.Message);
            }
            return dtAssetItemMapping;
        }

        private void gvUpdateAssetDetails_SelectionChanged(object sender, DevExpress.Data.SelectionChangedEventArgs e)
         {
            if (e.Action == CollectionChangeAction.Refresh)
            {
                gvUpdateAssetDetails.UnselectRow(gvUpdateAssetDetails.FocusedRowHandle);
            }
        }


        private void gvUpdateAssetDetails_MouseDown(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;

            GridHitInfo hi = view.CalcHitInfo(e.Location);
            if (hi.Column != null && hi.Column.FieldName == "DX$CheckboxSelectorColumn")
            {
                if (!hi.InRow)
                {
                    //DataView dv = view.DataSource as DataView;
                    bool allSelected = view.DataController.Selection.Count - view.DataController.GroupRowCount == view.DataRowCount;
                    if (!allSelected)
                    {
                        for (int i = 0; i < view.RowCount; i++)
                        {
                            int sourceHandle = view.GetRowHandle(i);

                            if (view.IsDataRow(sourceHandle))
                            {
                                // string donoremail = view.GetDataRow(sourceHandle)["email"].ToString();
                                if (!selectedRows.Contains(sourceHandle))  //&& !string.IsNullOrEmpty(donoremail))
                                    selectedRows.Add(sourceHandle);
                            }
                        }
                    }
                    else selectedRows.Clear();
                }
                else
                {
                    int sourceHandle = view.GetDataSourceRowIndex(hi.RowHandle);
                    if (!selectedRows.Contains(sourceHandle))
                        selectedRows.Add(sourceHandle);
                    else
                        selectedRows.Remove(sourceHandle);
                }
            }
            RestoreSelection(view);
        }

        private void gvUpdateAssetDetails_ColumnFilterChanged(object sender, EventArgs e)
        {
            RestoreSelection(sender as GridView);
        }

        private void gvUpdateAssetDetails_MouseUp(object sender, MouseEventArgs e)
        {
            GridView view = sender as GridView;
            RestoreSelection(view);
        }
        private void RestoreSelection(GridView view)
        {
            BeginInvoke(new Action(() =>
            {
                view.ClearSelection();
                for (int i = 0; i < selectedRows.Count; i++)
                {
                    int rowhandle = view.GetRowHandle(selectedRows[i]);
                    view.SelectRow(rowhandle);
                }
            }));
        }
    }
}