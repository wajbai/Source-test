using System;
using System.Data;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Model;
using Bosco.Utility;
using Bosco.Model.Inventory.Stock;
using ACPP.Modules.Transaction;
using Bosco.Model.UIModel;


namespace ACPP.Modules.Inventory.Stock
{
    public partial class frmStockTransferAdd : frmFinanceBaseAdd
    {
        #region Declaration
        ResultArgs resultArgs = null;
        bool IsValid = true;
        public event EventHandler UpdateHeld;
        static string LOCATION = "LOCATION";
        static string LOCATION_ID = "LOCATION_ID";
        static string FROM_LOCATION_ID = "FROM_LOCATION_ID";
        static string TO_LOCATION_ID = "TO_LOCATION_ID";
        static string FROM_LOCATION = "FROM_LOCATION";
        static string TO_LOCATION = "TO_LOCATION";
        static string ITEM_NAME = "ITEM_NAME";
        static string QUANTITY = "QUANTITY";
        static string EDIT_ID = "EDIT_ID";
        static string AVAILABLE_QANTITY = "AVAIL_QUANTITY";
        DataView dv = new DataView();
        DataTable dt = new DataTable();
        DataTable dtTransfer = new DataTable();
        #endregion

        #region Properties
        private DataTable dtItemDetails { get; set; }
        private DataTable dtItemLocation { get; set; }
        public int ProjectId { get; set; }
        public int EditId { get; set; }
        public string ProjectName { get; set; }
        private DateTime deTransferDate { get; set; }
        private bool isMouseClicked { get; set; }
        public int VoucherId { get; set; }

        private int ItemId { get { return gvItemTransfer.GetFocusedRowCellValue(ColItems) != null ? this.UtilityMember.NumberSet.ToInteger(gvItemTransfer.GetFocusedRowCellValue(ColItems).ToString()) : 0; } }

        private int LocationId { get { return gvItemTransfer.GetFocusedRowCellValue(colFromLocation) != null ? this.UtilityMember.NumberSet.ToInteger(gvItemTransfer.GetFocusedRowCellValue(colFromLocation).ToString()) : 0; } }
        #endregion

        #region Constructor
        public frmStockTransferAdd()
        {
            InitializeComponent();
            RealColumnEditValidateAvailableQuantity();
            RealColumnEditLocation();
        }
        public frmStockTransferAdd(string ProjectName, DateTime deTransferDate)
            : this()
        {
            this.ProjectName = ProjectName;
            this.deTransferDate = deTransferDate;
        }
        #endregion

        #region Events

        protected override bool ProcessCmdKey(ref Message msg, Keys KeyData)
        {
            if (KeyData.Equals(Keys.F5))
            {
                ShowProjectSelectionWindow();
            }
            else if (KeyData.Equals(Keys.F3))
            {
                deDate.Focus();
            }
            return base.ProcessCmdKey(ref msg, KeyData);
        }

        private void frmStockTransferAdd_Load(object sender, EventArgs e)
        {
            LoadDefaultValues();
            SetToStockLocation();
            if (dtItemLocation != null && dtItemLocation.Rows.Count > 0)
            {
                dv = dtItemLocation.DefaultView;
                dv.RowFilter = "ITEM_ID=" + ItemId;
                dt = dv.ToTable();
                dt.Columns[LOCATION_ID].ColumnName = FROM_LOCATION_ID;
                dt.Columns[LOCATION].ColumnName = FROM_LOCATION;
                UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpFromLocation, dt, FROM_LOCATION, FROM_LOCATION_ID);
            }
        }

        private void deDate_Leave(object sender, EventArgs e)
        {
            this.SetBorderColorForDateTimeEdit(deDate);
        }

        private void gcItemTransfer_Enter(object sender, EventArgs e)
        {
            gvItemTransfer.OptionsSelection.EnableAppearanceFocusedCell = true;
        }

        private void gcItemTransfer_Leave(object sender, EventArgs e)
        {
            gvItemTransfer.OptionsSelection.EnableAppearanceFocusedCell = false;
        }

        private void gcItemTransfer_ProcessGridKey(object sender, KeyEventArgs e)
        {
            try
            {
                if ((e.KeyCode == Keys.Enter || e.KeyCode == Keys.Return || e.KeyCode == Keys.Tab) && !e.Shift && !e.Alt && !e.Control)
                {
                    gvItemTransfer.PostEditor();
                    gvItemTransfer.UpdateCurrentRow();
                    string ItemName = gvItemTransfer.GetFocusedRowCellValue(ColItems) != null ? gvItemTransfer.GetFocusedRowCellValue(ColItems).ToString() : string.Empty;
                    string FromLocation = gvItemTransfer.GetFocusedRowCellValue(colFromLocation) != null ? gvItemTransfer.GetFocusedRowCellValue(colFromLocation).ToString() : string.Empty;
                    string ToLocation = gvItemTransfer.GetFocusedRowCellValue(colToLocation) != null ? gvItemTransfer.GetFocusedRowCellValue(colToLocation).ToString() : string.Empty;
                    int Quantity = gvItemTransfer.GetFocusedRowCellValue(colQuantity) != null ? UtilityMember.NumberSet.ToInteger(gvItemTransfer.GetFocusedRowCellValue(colQuantity).ToString()) : 0;

                    if (gvItemTransfer.FocusedColumn == ColItems)
                    {
                        if (dtItemLocation != null && dtItemLocation.Rows.Count > 0)
                        {
                            dv = dtItemLocation.DefaultView;
                            dv.RowFilter = "ITEM_ID=" + ItemId;
                            dt = dv.ToTable();
                            dt.Columns[LOCATION_ID].ColumnName = FROM_LOCATION_ID;
                            dt.Columns[LOCATION].ColumnName = FROM_LOCATION;
                            UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpFromLocation, dt, FROM_LOCATION, FROM_LOCATION_ID);
                        }
                        if (string.IsNullOrEmpty(ItemName))
                        {
                            DataTable dtRowCount = EliminateItemTransferDataRowEmpty();
                            if (dtRowCount.Rows.Count > 0 && string.IsNullOrEmpty(ItemName) && string.IsNullOrEmpty(FromLocation) && string.IsNullOrEmpty(ToLocation) && Quantity == 0)
                            {
                                gvItemTransfer.CloseEditor();
                                e.SuppressKeyPress = true;
                                e.Handled = true;
                                btnSave.Select();
                                btnSave.Focus();
                            }
                        }
                    }
                    else if (gvItemTransfer.FocusedColumn == colFromLocation)
                    {
                        SetItemAvailableQuantity();
                    }
                    if (gvItemTransfer.IsLastRow && (gvItemTransfer.FocusedColumn == colQuantity) && gvItemTransfer.GetFocusedRowCellValue(colQuantity) != null)
                    {
                        if (!string.IsNullOrEmpty(ItemName) && !string.IsNullOrEmpty(FromLocation) && !string.IsNullOrEmpty(ToLocation) && Quantity != 0)
                        {
                            FromLocation = gvItemTransfer.GetFocusedRowCellValue(colFromLocation) != null ? gvItemTransfer.GetFocusedRowCellValue(colFromLocation).ToString() : string.Empty;
                            ToLocation = gvItemTransfer.GetFocusedRowCellValue(colToLocation) != null ? gvItemTransfer.GetFocusedRowCellValue(colToLocation).ToString() : string.Empty;
                            //if (gvItemTransfer.FocusedColumn == colToLocation)
                            //{
                            //if (FromLocation.Equals(ToLocation))
                            //{
                            //    ShowMessageBox(GetMessage(MessageCatalog.Stock.StokItemTransfer.STOCK_TRANSFER_FROM_TO_LOCATION_SAME));
                            //    gvItemTransfer.FocusedColumn = colItemName;
                            //    gvItemTransfer.SetFocusedRowCellValue(colToLocation, String.Empty);
                            //    SetItemAvailableQuantity();
                            //}
                            //else
                            //{
                            //    gvItemTransfer.AddNewRow();
                            //    gvItemTransfer.FocusedColumn = gvItemTransfer.Columns[ColItems.Name];
                            //    gvItemTransfer.ShowEditor();
                            //}
                            //}
                            //else if (gvItemTransfer.FocusedColumn == colFromLocation)
                            //{
                            if (!string.IsNullOrEmpty(ToLocation))
                            {
                                if (FromLocation.Equals(ToLocation))
                                {
                                    ShowMessageBox(GetMessage(MessageCatalog.Stock.StokItemTransfer.STOCK_TRANSFER_FROM_TO_LOCATION_SAME));
                                    gvItemTransfer.FocusedColumn = gvItemTransfer.Columns[ColItems.Name];
                                    gvItemTransfer.SetFocusedRowCellValue(colFromLocation, string.Empty);

                                }
                                else
                                {
                                    SetItemAvailableQuantity();
                                    gvItemTransfer.AddNewRow();
                                    gvItemTransfer.FocusedColumn = gvItemTransfer.Columns[ColItems.Name];
                                    gvItemTransfer.ShowEditor();
                                }
                            }

                            //}

                        }
                        else if (gvItemTransfer.IsFirstRow)
                        {
                            gvItemTransfer.FocusedColumn = ColItems;
                        }
                        else
                        {
                            gvItemTransfer.CloseEditor();
                            e.SuppressKeyPress = true;
                            e.Handled = true;
                            btnSave.Select();
                            btnSave.Focus();
                        }
                    }

                }
                else if (gvItemTransfer.IsFirstRow && gvItemTransfer.FocusedColumn == ColItems && e.Shift && e.KeyCode == Keys.Tab)
                {
                    deDate.Focus();
                    deDate.Select();
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
        }

        private void rbtnDelete_Click(object sender, EventArgs e)
        {
            DeleteTransaction();
        }

        private void gvItemTransfer_ValidateRow(object sender, DevExpress.XtraGrid.Views.Base.ValidateRowEventArgs e)
        {
            //string FromLocation = gvItemTransfer.GetFocusedRowCellValue(colFromLocation) != null ? gvItemTransfer.GetFocusedRowCellValue(colFromLocation).ToString() : string.Empty;
            //string ToLocation = gvItemTransfer.GetFocusedRowCellValue(colToLocation) != null ? gvItemTransfer.GetFocusedRowCellValue(colToLocation).ToString() : string.Empty;
            //if (gvItemTransfer.FocusedColumn == colToLocation)
            //{
            //    if (FromLocation.Equals(ToLocation))
            //    {
            //        ShowMessageBox(GetMessage(MessageCatalog.Stock.StokItemTransfer.STOCK_TRANSFER_FROM_TO_LOCATION_SAME));
            //        gvItemTransfer.FocusedColumn = colItemName;
            //        gvItemTransfer.SetFocusedRowCellValue(colToLocation, String.Empty);
            //    }
            //}
            //else if (gvItemTransfer.FocusedColumn == colFromLocation)
            //{
            //    if (!string.IsNullOrEmpty(ToLocation))
            //    {
            //        if (FromLocation.Equals(ToLocation))
            //        {
            //            ShowMessageBox(GetMessage(MessageCatalog.Stock.StokItemTransfer.STOCK_TRANSFER_FROM_TO_LOCATION_SAME));
            //            gvItemTransfer.FocusedColumn = gvItemTransfer.Columns[ColItems.Name];
            //            gvItemTransfer.SetFocusedRowCellValue(colFromLocation, string.Empty);
            //        }
            //    }
            //    SetItemAvailableQuantity();
            //}
        }

        private void SetItemAvailableQuantity()
        {
            int LocationId = gvItemTransfer.GetFocusedRowCellValue(colFromLocation) != null ? this.UtilityMember.NumberSet.ToInteger(gvItemTransfer.GetFocusedRowCellValue(colFromLocation).ToString()) : 0;
            int ItemId = gvItemTransfer.GetFocusedRowCellValue(ColItems) != null ? this.UtilityMember.NumberSet.ToInteger(gvItemTransfer.GetFocusedRowCellValue(ColItems).ToString()) : 0;
            if (LocationId > 0 && ItemId > 0)
            {
                int AvailStockQuantity = GetAvailableQuantity(gcItemTransfer.DataSource as DataTable, ItemId, LocationId);
                gvItemTransfer.SetFocusedRowCellValue(colStockQuantity, AvailStockQuantity);
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            SaveTransferDetails(e);
        }

        private void SaveTransferDetails(EventArgs e)
        {
            if (ValidateItemTransfer())
            {
                resultArgs = SaveItemTransferDetails();
                if (resultArgs != null && resultArgs.Success)
                {
                    ShowSuccessMessage(this.GetMessage(MessageCatalog.Common.COMMON_SAVED_CONFIRMATION));
                    if (EditId == 0)
                        ClearControls();
                    if (UpdateHeld != null)
                    {
                        UpdateHeld(this, e);
                    }
                }
                else ShowMessageBox(resultArgs.Message);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void rglkpItems_MouseDown(object sender, MouseEventArgs e)
        {
            //To Identify the Mouse Click Event
            if (e.Button == MouseButtons.Left)
                isMouseClicked = true;
        }

        private void rglkpItems_EditValueChanged(object sender, EventArgs e)
        {
            //To select the Ledger Using Mouse Click
            if (isMouseClicked)
            {
                SendKeys.Send("{tab}"); isMouseClicked = false;
            }
        }
        #endregion

        #region Methods
        private void InitializeAddMode()
        {
            DataTable dtTransferItem = new DataTable();
            dtTransferItem.Columns.Add(ITEM_NAME, typeof(String));
            dtTransferItem.Columns.Add(FROM_LOCATION, typeof(String));
            dtTransferItem.Columns.Add(TO_LOCATION, typeof(String));
            dtTransferItem.Columns.Add(QUANTITY, typeof(Int32));
            dtTransferItem.Columns.Add(AVAILABLE_QANTITY, typeof(Int32));
            dtTransferItem.Columns.Add(EDIT_ID, typeof(Int32));
            dtTransferItem.Rows.Add(dtTransferItem.NewRow());
            gcItemTransfer.DataSource = dtTransferItem;
            gvItemTransfer.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.None;
        }

        private void LoadDefaultValues()
        {
            deDate.Properties.MinValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            deDate.Properties.MaxValue = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            SetTitle();
            uccpProject.Caption = ProjectName;
            deDate.DateTime = deTransferDate;
            SetStockItems();
            SetStockLocation();
            if (EditId > 0) AssignTranferredItemDetails();
            else InitializeAddMode();
        }

        private void SetTitle()
        {
            this.Text = EditId > 0 ? GetMessage(MessageCatalog.Stock.StokItemTransfer.STOCK_TRANSFER_EDIT_CAPTION) : GetMessage(MessageCatalog.Stock.StokItemTransfer.STOCK_TRANSFER_ADD_CAPTION);
        }

        private void AssignTranferredItemDetails()
        {
            using (StockItemTransferSystem TransferredSystem = new StockItemTransferSystem())
            {
                TransferredSystem.EditId = EditId;
                resultArgs = TransferredSystem.FetchTransferredItemByEditId();
                if (resultArgs != null && resultArgs.Success)
                {
                    if (resultArgs.DataSource.Table.Rows.Count > 0)
                        deDate.DateTime = UtilityMember.DateSet.ToDate(resultArgs.DataSource.Table.Rows[0][TransferredSystem.AppSchema.StockItemTransfer.TRANSFER_DATEColumn.ColumnName].ToString(), false);
                    gcItemTransfer.DataSource = resultArgs.DataSource.Table;
                }
                else
                {
                    ShowMessageBox(resultArgs.Message);
                }
            }
        }

        private void ClearControls()
        {
            InitializeAddMode();
            deDate.Focus();
        }

        /// <summary>
        /// Loading all the Item Name to the gridlookup
        /// </summary>
        private void SetStockItems()
        {
            using (StockPurchaseSalesSystem LoadStockItem = new StockPurchaseSalesSystem())
            {
                resultArgs = LoadStockItem.FetchStockItemDetails();
                if (resultArgs != null && resultArgs.DataSource.Table != null)
                {
                    dtItemDetails = resultArgs.DataSource.Table;
                    UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpItems, dtItemDetails, ITEM_NAME, LoadStockItem.AppSchema.StockItem.ITEM_IDColumn.ColumnName);
                }
            }
        }

        private void SetStockLocation()
        {
            using (StockPurchaseSalesSystem LoadStockLocation = new StockPurchaseSalesSystem())
            {
                resultArgs = LoadStockLocation.FetchStockItemLocationDetails();
                if (resultArgs != null && resultArgs.DataSource.Table != null)
                {
                    dtItemLocation = resultArgs.DataSource.Table;
                    SetFromLocation(dtItemLocation.Copy());
                }
            }
        }

        private void SetToStockLocation()
        {
            DataTable dt = new DataTable();
            using (LocationSystem locationsystem = new LocationSystem())
            {
                resultArgs = locationsystem.FetchLocationDetails();
                if (resultArgs != null && resultArgs.DataSource.Table != null)
                {
                    dt = resultArgs.DataSource.Table;
                    SetToLocation(dt.Copy());
                }
            }
        }

        private void SetFromLocation(DataTable dtFromLocation)
        {
            //Bind the Location from Column
            //Rename the Column name LOCATION_ID to FROM_LOCATION_ID and LOCATION to FROM_LOCATION.
            //dtFromLocation.Columns[LOCATION_ID].ColumnName = FROM_LOCATION_ID;
            //dtFromLocation.Columns[LOCATION_NAME].ColumnName = FROM_LOCATION;
            //UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpFromLocation, dtFromLocation, FROM_LOCATION, FROM_LOCATION_ID);
        }

        private void SetToLocation(DataTable dtToLocation)
        {
            //Bind the Location To Column
            //Rename FROM_LOCATION_ID to TO_LOCATION_ID and FROM_LOCATION to TO_LOCATION
            //dtToLocation.Columns[LOCATION_ID].ColumnName = TO_LOCATION_ID;
            //dtToLocation.Columns[LOCATION].ColumnName = TO_LOCATION;
            UtilityMember.ComboSet.BindRepositoryItemGridLookUpEdit(rglkpToLocation, dtToLocation, TO_LOCATION, TO_LOCATION_ID);
        }

        private void FilterLocationByItem()
        {
            DataView dvLocation = new DataView(dtItemLocation);
            if (gvItemTransfer.GetFocusedRowCellValue(colItemName) != null)
            {
                dvLocation.RowFilter = String.Format("ITEM_ID={0}", UtilityMember.NumberSet.ToInteger(gvItemTransfer.GetFocusedRowCellValue(colItemName).ToString()));
                DataTable dtLocationByItem = dvLocation.ToTable();
                SetAvailableQuantity(dtLocationByItem);
                SetFromLocation(dtLocationByItem);
            }
        }

        private void SetAvailableQuantity(DataTable dtItem)
        {
            int AvailalbleQuantity = UtilityMember.NumberSet.ToInteger(dtItem.Compute("SUM(AVAIL_QUANTITY)", String.Empty).ToString());
            gvItemTransfer.SetFocusedRowCellValue(colStockQuantity, AvailalbleQuantity);
        }

        private void RealColumnEditValidateAvailableQuantity()
        {
            colQuantity.RealColumnEdit.EditValueChanged += new EventHandler(RealColumnEdit_EditValueChanged);
            this.gvItemTransfer.MouseDown += (object sender, MouseEventArgs e) =>
            {
                DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hitInfo = gvItemTransfer.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colQuantity)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvItemTransfer.ShowEditorByMouse();
                    }));
                }
            };
        }

        private void RealColumnEdit_EditValueChanged(object sender, EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvItemTransfer.PostEditor();
            gvItemTransfer.UpdateCurrentRow();
            if (gvItemTransfer.ActiveEditor == null)
            {
                gvItemTransfer.ShowEditor();
            }
            if (gvItemTransfer.GetFocusedRowCellValue(colStockQuantity) != null && gvItemTransfer.GetFocusedRowCellValue(colQuantity) != null)
            {
                //int AvailableQuantity = UtilityMember.NumberSet.ToInteger(gvItemTransfer.GetFocusedRowCellValue(colStockQuantity).ToString());
                // int EnteredQuantity = UtilityMember.NumberSet.ToInteger(gvItemTransfer.GetFocusedRowCellValue(colQuantity).ToString());
                int EnteredQuantity = GetCalculatedQuantity(LocationId, ItemId, gcItemTransfer.DataSource as DataTable);
                int AvailableQuantity = FetchAvailableStock(LocationId, ItemId);

                if (EnteredQuantity > AvailableQuantity)
                {
                    ShowMessageBox(GetMessage(MessageCatalog.Stock.StokItemTransfer.STOCK_QUANTITY_EXCEEDS));
                    gvItemTransfer.SetFocusedRowCellValue(colQuantity, 0);
                    gvItemTransfer.FocusedColumn = colQuantity;
                }
                int Quantity = GetAvailableQuantity(gcItemTransfer.DataSource as DataTable, ItemId, LocationId);
                gvItemTransfer.SetFocusedRowCellValue(colStockQuantity, Quantity);
            }
        }

        private void RealColumnEditLocation()
        {
            colFromLocation.RealColumnEdit.EditValueChanged += new EventHandler(RealColumnEditLocation_EditValueChanged);
            this.gvItemTransfer.MouseDown += (object sender, MouseEventArgs e) =>
            {
                DevExpress.XtraGrid.Views.Grid.ViewInfo.GridHitInfo hitInfo = gvItemTransfer.CalcHitInfo(e.Location);
                if (hitInfo.Column != null && hitInfo.Column == colFromLocation)
                {
                    this.BeginInvoke(new MethodInvoker(delegate
                    {
                        gvItemTransfer.ShowEditorByMouse();
                    }));
                }
            };
        }

        private void RealColumnEditLocation_EditValueChanged(object sender, EventArgs e)
        {
            BaseEdit edit = sender as BaseEdit;
            if (edit.EditValue == null)
                return;
            gvItemTransfer.PostEditor();
            gvItemTransfer.UpdateCurrentRow();
            if (gvItemTransfer.ActiveEditor == null)
            {
                gvItemTransfer.ShowEditor();
            }
            if (gvItemTransfer.GetFocusedRowCellValue(colStockQuantity) != null && gvItemTransfer.GetFocusedRowCellValue(colQuantity) != null)
            {
                //int AvailableQuantity = UtilityMember.NumberSet.ToInteger(gvItemTransfer.GetFocusedRowCellValue(colStockQuantity).ToString());
                // int EnteredQuantity = UtilityMember.NumberSet.ToInteger(gvItemTransfer.GetFocusedRowCellValue(colQuantity).ToString());
                int EnteredQuantity = GetCalculatedQuantity(LocationId, ItemId, gcItemTransfer.DataSource as DataTable);
                int AvailableQuantity = FetchAvailableStock(LocationId, ItemId);

                if (EnteredQuantity > AvailableQuantity)
                {
                    ShowMessageBox(GetMessage(MessageCatalog.Stock.StokItemTransfer.STOCK_QUANTITY_EXCEEDS));
                    gvItemTransfer.SetFocusedRowCellValue(colQuantity, 0);
                    gvItemTransfer.FocusedColumn = colQuantity;
                }
                SetItemAvailableQuantity();
                int Quantity = GetAvailableQuantity(gcItemTransfer.DataSource as DataTable, ItemId, LocationId);
                gvItemTransfer.SetFocusedRowCellValue(colStockQuantity, Quantity);
            }
        }

        private ResultArgs SaveItemTransferDetails()
        {
            using (StockItemTransferSystem ItemTransfer = new StockItemTransferSystem(EditId))
            {
                ShowWaitDialog();
                ItemTransfer.TransferDate = deDate.DateTime;
                ItemTransfer.ProjectId = ProjectId;
                ItemTransfer.dtTransferItem = gcItemTransfer.DataSource as DataTable;
                resultArgs = ItemTransfer.SaveItemTransfer();
                CloseWaitDialog();
            }
            return resultArgs;
        }

        private bool ValidateItemTransfer()
        {
            DataTable dtItemDetails = EliminateItemTransferDataRowEmpty();
            IsValid = IsGridHasInvalidRecord(ref dtItemDetails);
            if (IsValid)
            {
                if (!IsValidTransactionDate())
                {
                    deDate.DateTime = this.UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom.ToString(), false);
                    IsValid = false;
                }
                else if (string.IsNullOrEmpty(deDate.Text))
                {
                    IsValid = false;
                    ShowMessageBox(GetMessage(MessageCatalog.Stock.StokItemTransfer.STOCK_TRANSFER_DATE_EMPTY));
                    deDate.Focus();
                }
                else if (!IsValidGridTransfer())
                {
                    IsValid = false;
                }
            }
            return IsValid;
        }

        private bool IsValidGridTransfer()
        {
            bool IsValid = true;
            DataTable dtItemDetails = EliminateItemTransferDataRowEmpty();
            if (dtItemDetails == null && dtItemDetails.Rows.Count > 0)
            {
                IsValid = false;
                ShowMessageBox(GetMessage(MessageCatalog.Common.COMMON_NO_RECORDS_TO_SAVE));
            }
            else
            {
                if (dtItemDetails.Rows.Count > 0)
                {
                    DataView dvFindEmptyCell = new DataView(dtItemDetails);
                    dvFindEmptyCell.RowFilter = String.Format("{0}>0 AND {1}>0 AND {2}>0 AND {3}>0", ITEM_NAME, FROM_LOCATION, TO_LOCATION, QUANTITY);
                    DataTable FilteredRow = dvFindEmptyCell.ToTable();
                    if (dtItemDetails.Rows.Count != FilteredRow.Rows.Count)
                    {
                        ShowMessageBox(GetMessage(MessageCatalog.Stock.StokItemTransfer.STOCK_INVALID_GRID_ENTRY));
                        IsValid = false;
                    }
                }
                else
                {
                    IsValid = false;
                    ShowMessageBox(GetMessage(MessageCatalog.Common.COMMON_NO_RECORDS_TO_SAVE));
                }
            }
            return IsValid;
        }

        private bool IsValidTransactionDate()
        {
            bool isValid = true;
            DateTime dtProjectFrom;
            DateTime dtProjectTo;
            DateTime dtyearfrom;
            DateTime dtbookbeginfrom;
            DateTime dtYearTo;
            DateTime dtRecentVoucherDate;
            dtyearfrom = UtilityMember.DateSet.ToDate(this.AppSetting.YearFrom, false);
            dtbookbeginfrom = UtilityMember.DateSet.ToDate(this.AppSetting.BookBeginFrom, false);
            dtYearTo = UtilityMember.DateSet.ToDate(this.AppSetting.YearTo, false);
            dtRecentVoucherDate = UtilityMember.DateSet.ToDate(deTransferDate.ToString(), false);

            ResultArgs result = FetchProjectDetails();

            DataView dvResult = result.DataSource.Table.DefaultView;
            dvResult.RowFilter = "PROJECT_ID=" + ProjectId;
            DataTable dtResult = dvResult.ToTable();
            dvResult.RowFilter = "";
            if (dtResult.Rows.Count > 0)
            {
                DataRow drProject = dtResult.Rows[0];

                string sProjectFrom = drProject["DATE_STARTED"].ToString();
                string sProjectTo = drProject["DATE_CLOSED"].ToString();

                dtProjectFrom = (!string.IsNullOrEmpty(sProjectFrom)) ? this.UtilityMember.DateSet.ToDate(sProjectFrom, false) : dtyearfrom;

                if (!string.IsNullOrEmpty(sProjectTo))
                {
                    dtProjectTo = this.UtilityMember.DateSet.ToDate(sProjectTo, false);
                }
                else
                {
                    dtProjectTo = dtProjectFrom > dtYearTo ? dtProjectFrom : dtYearTo;
                }

                if ((dtProjectFrom < dtyearfrom && dtProjectTo < dtyearfrom) || (dtProjectFrom > dtYearTo && dtProjectTo > dtYearTo))
                {
                    //this.ShowMessageBox(this.GetMessage(MessageCatalog.Stock.StockMasterPurchase.VALIDATE_ACCOUNTING_YEAR));
                     //this.ShowMessageBoxWarning("Project start date and closed date does not fall between transaction period." + Environment.NewLine + "Kindly change the Project start date and closed date and try again.");
                     this.ShowMessageBoxWarning(this.GetMessage(MessageCatalog.Stock.StockMasterPurchase.PROJECT_START_DATE_CLOSED_DATE_FALL_BETWEEN_TRANS_PEROID) + Environment.NewLine + this.GetMessage(MessageCatalog.Stock.StockMasterPurchase.CHANGE_PROJECT_START_DATE_CLOSED_DATE));
                    isValid = false;
                    //this.Close();
                }
            }
            return isValid;
        }

        private ResultArgs FetchProjectDetails()
        {
            try
            {
                using (MappingSystem mappingSystem = new MappingSystem())
                {
                    resultArgs = mappingSystem.FetchProjectsLookup();
                }
            }
            catch (Exception Ex)
            {
                MessageRender.ShowMessage(Ex.Message);
            }
            finally { }
            return resultArgs;
        }

        //private void DeleteTransaction()
        //{
        //    try
        //    {
        //        if (gvItemTransfer.RowCount == 1)
        //        {
        //            int itemId = gvItemTransfer.GetFocusedRowCellValue(ColItems) != null ? this.UtilityMember.NumberSet.ToInteger(gvItemTransfer.GetFocusedRowCellValue(ColItems).ToString()) : 0;
        //            int LocationId = gvItemTransfer.GetFocusedRowCellValue(colgFromLocation) != null ? this.UtilityMember.NumberSet.ToInteger(gvItemTransfer.GetFocusedRowCellValue(colgFromLocation).ToString()) : 0;
        //            if (itemId > 0 || LocationId > 0)
        //            {
        //                if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
        //                {
        //                    gvItemTransfer.DeleteRow(gvItemTransfer.FocusedRowHandle);
        //                    gvItemTransfer.UpdateCurrentRow();
        //                    gvItemTransfer.AddNewRow();
        //                }
        //            }
        //            else
        //            {
        //                gvItemTransfer.UpdateCurrentRow();
        //                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Bank.BANK_DELETE));
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        MessageRender.ShowMessage(ex.ToString(), true);
        //    }
        //    finally { }
        //}


        private void DeleteTransaction()
        {
            try
            {
                dtTransfer = gcItemTransfer.DataSource as DataTable;
                int itemId = gvItemTransfer.GetFocusedRowCellValue(colItemName) != null ? this.UtilityMember.NumberSet.ToInteger(gvItemTransfer.GetFocusedRowCellValue(colItemName).ToString()) : 0;
                int LocId = gvItemTransfer.GetFocusedRowCellValue(colgFromLocation) != null ? this.UtilityMember.NumberSet.ToInteger(gvItemTransfer.GetFocusedRowCellValue(colgFromLocation).ToString()) : 0;
                if (gvItemTransfer.RowCount > 1)
                {
                    if (EditId > 0)
                    {
                        if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                        {
                            gvItemTransfer.DeleteRow(gvItemTransfer.FocusedRowHandle);
                            gvItemTransfer.FocusedColumn = gvItemTransfer.Columns.ColumnByName(colItemName.Name);
                        }

                    }
                    else
                    {
                        if (dtTransfer != null)
                        {
                            if (ItemId > 0 || LocId > 0)
                            {
                                if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                                {
                                    gvItemTransfer.DeleteRow(gvItemTransfer.FocusedRowHandle);
                                    gvItemTransfer.FocusedColumn = gvItemTransfer.Columns.ColumnByName(colItemName.Name);
                                }
                            }
                            else
                            {
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Bank.BANK_DELETE));
                            }
                        }
                    }
                }
                else if (gvItemTransfer.RowCount == 1)
                {
                    if (ItemId > 0 || LocId > 0)
                    {
                        if (EditId > 0)
                        {
                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                InitializeAddMode();
                                gvItemTransfer.FocusedColumn = gvItemTransfer.Columns.ColumnByName(colItemName.Name);
                                //int sourceId = (rgTransactionType.SelectedIndex == 0 || rgTransactionType.SelectedIndex == 2) ? (int)Source.To : (int)Source.By;
                                //gvPurchaseItems.SetRowCellValue(gvTransaction.FocusedRowHandle, colSource, sourceId);
                            }
                        }
                        else
                        {

                            if (this.ShowConfirmationMessage(this.GetMessage(MessageCatalog.Common.COMMON_DELETE_CONFIRMATION), MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                            {
                                gvItemTransfer.DeleteRow(gvItemTransfer.FocusedRowHandle);
                                InitializeAddMode();
                                gvItemTransfer.FocusedColumn = gvItemTransfer.Columns.ColumnByName(colItemName.Name);
                            }
                            else
                            {
                                this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Bank.BANK_DELETE));
                            }
                        }
                    }
                    else
                    {
                        this.ShowMessageBox(this.GetMessage(MessageCatalog.Master.Bank.BANK_DELETE));
                    }
                }
            }
            catch (Exception ex)
            {
                MessageRender.ShowMessage(ex.ToString(), true);
            }
            finally { }
        }

        private void ShowProjectSelectionWindow()
        {
            frmProjectSelection projectSelection = new frmProjectSelection(Bosco.Utility.ProjectSelection.DisableVoucherSelectionMethod);
            projectSelection.ShowDialog();
            if (projectSelection.DialogResult == DialogResult.OK)
            {
                if (projectSelection.ProjectName != string.Empty)
                {
                    ProjectId = projectSelection.ProjectId;
                    uccpProject.Caption = ProjectName = projectSelection.ProjectName;
                }
            }
        }

        private DataTable EliminateItemTransferDataRowEmpty()
        {
            DataTable dtRowCount = gcItemTransfer.DataSource as DataTable;
            DataView dvRowCount = new DataView(dtRowCount);
            dvRowCount.RowFilter = String.Format("{0}>0", ITEM_NAME);
            dtRowCount = dvRowCount.ToTable();
            return dtRowCount;
        }

        private bool IsGridHasInvalidRecord(ref DataTable dtEmptyRowEliminated)
        {
            bool IsValid = true;
            if (dtEmptyRowEliminated != null)
            {
                foreach (DataRow drItem in dtEmptyRowEliminated.Rows)
                {
                    string ItemName = drItem[ITEM_NAME].ToString();
                    string FromLocation = drItem[FROM_LOCATION].ToString();
                    string ToLocation = drItem[TO_LOCATION].ToString();
                    int Quantity = UtilityMember.NumberSet.ToInteger(drItem[QUANTITY].ToString());

                    if (string.IsNullOrEmpty(ItemName) || string.IsNullOrEmpty(FromLocation) || string.IsNullOrEmpty(ToLocation))
                    {
                        IsValid = false;
                        ShowMessageBox(GetMessage(MessageCatalog.Stock.StokItemTransfer.STOCK_INVALID_GRID_ENTRY));
                        break;
                    }
                    else if (!(Quantity > 0))
                    {
                        IsValid = false;
                        ShowMessageBox(GetMessage(MessageCatalog.Stock.StokItemTransfer.STOCK_TRANSFER_QTY_ZERO));
                        gvItemTransfer.Focus();
                        gvItemTransfer.FocusedColumn = colQuantity;
                        break;
                    }
                }
            }
            return IsValid;
        }

        private int FetchAvailableStock(int LocationId, int ItemId)
        {
            int AvaliableStock = 0;
            try
            {
                using (StockBalanceSystem BalanceSystem = new StockBalanceSystem())
                {
                    resultArgs = BalanceSystem.GetCurrentBalance(this.ProjectId, ItemId, LocationId);
                    if (resultArgs != null && resultArgs.Success && resultArgs.DataSource.Table != null && resultArgs.DataSource.Table.Rows.Count > 0)
                    {
                        AvaliableStock = resultArgs.DataSource.Sclar.ToInteger;
                        AvaliableStock = this.UtilityMember.NumberSet.ToInteger(resultArgs.DataSource.Table.Rows[0][BalanceSystem.AppSchema.StockPurchaseDetails.QUANTITYColumn.ColumnName].ToString());

                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
            return AvaliableStock;
        }

        private int GetAvailableQuantity(DataTable dtTrans, int ItemId, int LocationId)
        {
            int AvailQuantity = 0;
            int OldValue = 0;
            int NewValue = 0;

            if (dtTrans != null)
            {
                //NewValue = FetchAvailableStock(LocationId, ItemId);
                OldValue = GetCalculatedQuantity(LocationId, ItemId, dtTrans);
                AvailQuantity = GetCurBalance(NewValue, OldValue);
            }
            return AvailQuantity;
        }

        private int GetCalculatedQuantity(int LocationId, int ItemId, DataTable dtTrans)
        {
            int Quantity = 0;
            try
            {
                Quantity = this.UtilityMember.NumberSet.ToInteger(dtTrans.Compute("SUM(QUANTITY)", "ITEM_NAME=" + ItemId + " AND FROM_LOCATION=" + LocationId).ToString());
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
            return Quantity;
        }

        private int GetCurBalance(int NewQuantity, int OldQuantity)
        {
            int AvailQuantity = 0;
            try
            {
                int CurrentAvail = FetchAvailableStock(LocationId, ItemId);

                if (this.EditId.Equals(0))
                {
                    if (CurrentAvail > 0 && OldQuantity > 0)
                    {
                        AvailQuantity = CurrentAvail - OldQuantity;
                    }
                    else if (CurrentAvail > 0 && OldQuantity <= 0)
                    {
                        AvailQuantity = CurrentAvail - OldQuantity;
                    }
                    else if (CurrentAvail <= 0 && OldQuantity > 0)
                    {
                        AvailQuantity = OldQuantity - CurrentAvail;
                    }
                    else if (CurrentAvail.Equals(0) && OldQuantity > 0)
                    {
                        AvailQuantity = CurrentAvail + OldQuantity;
                    }
                }
                else
                {
                    if (CurrentAvail.Equals(0) || CurrentAvail > 0)
                    {
                        AvailQuantity = CurrentAvail;
                    }
                    else
                    {
                        AvailQuantity = OldQuantity;
                    }
                    if ((gcItemTransfer.DataSource as DataTable) != null && (gcItemTransfer.DataSource as DataTable).Rows.Count > 0)
                    {
                        DataTable dtUpdateRows = gcItemTransfer.DataSource as DataTable;
                    }
                }
            }
            catch (Exception ex)
            {
                this.ShowMessageBox(ex.Message + Environment.NewLine + ex.Source);
            }
            finally { }
            return AvailQuantity;
        }
        #endregion

        private void bbiDeleteTransaction_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            DeleteTransaction();
        }

    }
}