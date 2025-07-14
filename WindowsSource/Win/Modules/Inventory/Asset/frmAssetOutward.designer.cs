namespace ACPP.Modules.Asset.Transactions
{
    partial class frmAssetOutward
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAssetOutward));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtActualAmt = new DevExpress.XtraEditors.TextEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.lblCalAmt = new DevExpress.XtraEditors.LabelControl();
            this.lblAvgRate = new DevExpress.XtraEditors.LabelControl();
            this.txtExchangeRate = new DevExpress.XtraEditors.TextEdit();
            this.txtCurrencyAmount = new DevExpress.XtraEditors.TextEdit();
            this.glkpCurrencyCountry = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcCountryId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCurrencyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCurrencySymbol = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ucAssetJournal1 = new ACPP.Modules.UIControls.UcAssetJournal();
            this.txtBillInvoiceNo = new DevExpress.XtraEditors.TextEdit();
            this.ucAssetVoucherShortcuts = new ACPP.Modules.UIControls.UCAssetVoucherShortcuts();
            this.rgTransactionType = new DevExpress.XtraEditors.RadioGroup();
            this.btnNew = new DevExpress.XtraEditors.SimpleButton();
            this.txtOtherCharges = new DevExpress.XtraEditors.TextEdit();
            this.txtNarration = new DevExpress.XtraEditors.TextEdit();
            this.txtSoldTo = new DevExpress.XtraEditors.TextEdit();
            this.txtVoucherNo = new DevExpress.XtraEditors.TextEdit();
            this.dtSalesDate = new DevExpress.XtraEditors.DateEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.ucCaptionTitle = new ACPP.Modules.UIControls.UcCaptionPanel();
            this.gcInOut = new DevExpress.XtraGrid.GridControl();
            this.gvInOutAdd = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSource = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rglkpSource = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSalesId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLocation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rglkpLocation = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colLocation_Id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLocationName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAsset = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rglkpAssetName = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colItemId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAssetName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccountLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtQuantity = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtAmount = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colGroup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rglkpGroup = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colGroup_Id = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGroupName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColDiscount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtDiscount = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colAction = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rbiDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colTemp = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colInoutDetailId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAvailableQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colView = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rbtView = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colActualAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtActualAmount = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colDifference = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCosCentre = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rbtnCostCentre = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.rccmbAssetId = new DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcgSales = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblSalesDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblName = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblVoucherNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem8 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciCurrency = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem16 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblDonorCurrency = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblExchangeRate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblliveAvgRate = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem18 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblCalcAmount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem19 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciActualAmt = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblDisplayType = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem10 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.ColRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lblNameAddress = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblNarration = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.barManager2 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar4 = new DevExpress.XtraBars.Bar();
            this.bbiDeletePurchase = new DevExpress.XtraBars.BarButtonItem();
            this.bbiCashBank = new DevExpress.XtraBars.BarButtonItem();
            this.bbiAssetGenerationList = new DevExpress.XtraBars.BarButtonItem();
            this.bbiSave = new DevExpress.XtraBars.BarButtonItem();
            this.bbiNew = new DevExpress.XtraBars.BarButtonItem();
            this.bbiClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtActualAmt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExchangeRate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCurrencyAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpCurrencyCountry.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBillInvoiceNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgTransactionType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOtherCharges.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNarration.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoldTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVoucherNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtSalesDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtSalesDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcInOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInOutAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpLocation)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpAssetName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtDiscount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbiDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtActualAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnCostCentre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rccmbAssetId)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgSales)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSalesDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblVoucherNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCurrency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDonorCurrency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblExchangeRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblliveAvgRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCalcAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciActualAmt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDisplayType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNameAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNarration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).BeginInit();
            this.SuspendLayout();
            // 
            // panelControl1
            // 
            this.panelControl1.Controls.Add(this.layoutControl1);
            resources.ApplyResources(this.panelControl1, "panelControl1");
            this.panelControl1.Name = "panelControl1";
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtActualAmt);
            this.layoutControl1.Controls.Add(this.lblCalAmt);
            this.layoutControl1.Controls.Add(this.lblAvgRate);
            this.layoutControl1.Controls.Add(this.txtExchangeRate);
            this.layoutControl1.Controls.Add(this.txtCurrencyAmount);
            this.layoutControl1.Controls.Add(this.glkpCurrencyCountry);
            this.layoutControl1.Controls.Add(this.ucAssetJournal1);
            this.layoutControl1.Controls.Add(this.txtBillInvoiceNo);
            this.layoutControl1.Controls.Add(this.ucAssetVoucherShortcuts);
            this.layoutControl1.Controls.Add(this.rgTransactionType);
            this.layoutControl1.Controls.Add(this.btnNew);
            this.layoutControl1.Controls.Add(this.txtOtherCharges);
            this.layoutControl1.Controls.Add(this.txtNarration);
            this.layoutControl1.Controls.Add(this.txtSoldTo);
            this.layoutControl1.Controls.Add(this.txtVoucherNo);
            this.layoutControl1.Controls.Add(this.dtSalesDate);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.ucCaptionTitle);
            this.layoutControl1.Controls.Add(this.gcInOut);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2});
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(426, 106, 300, 686);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // txtActualAmt
            // 
            resources.ApplyResources(this.txtActualAmt, "txtActualAmt");
            this.txtActualAmt.MenuManager = this.barManager1;
            this.txtActualAmt.Name = "txtActualAmt";
            this.txtActualAmt.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtActualAmt.StyleController = this.layoutControl1;
            // 
            // barManager1
            // 
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.MaxItemId = 3;
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            resources.ApplyResources(this.barDockControlTop, "barDockControlTop");
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            resources.ApplyResources(this.barDockControlBottom, "barDockControlBottom");
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            resources.ApplyResources(this.barDockControlLeft, "barDockControlLeft");
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            resources.ApplyResources(this.barDockControlRight, "barDockControlRight");
            // 
            // lblCalAmt
            // 
            this.lblCalAmt.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblCalAmt.Appearance.Font")));
            resources.ApplyResources(this.lblCalAmt, "lblCalAmt");
            this.lblCalAmt.Name = "lblCalAmt";
            this.lblCalAmt.StyleController = this.layoutControl1;
            // 
            // lblAvgRate
            // 
            this.lblAvgRate.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblAvgRate.Appearance.Font")));
            resources.ApplyResources(this.lblAvgRate, "lblAvgRate");
            this.lblAvgRate.Name = "lblAvgRate";
            this.lblAvgRate.StyleController = this.layoutControl1;
            // 
            // txtExchangeRate
            // 
            resources.ApplyResources(this.txtExchangeRate, "txtExchangeRate");
            this.txtExchangeRate.MenuManager = this.barManager1;
            this.txtExchangeRate.Name = "txtExchangeRate";
            this.txtExchangeRate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtExchangeRate.StyleController = this.layoutControl1;
            this.txtExchangeRate.EditValueChanged += new System.EventHandler(this.txtExchangeRate_EditValueChanged);
            // 
            // txtCurrencyAmount
            // 
            resources.ApplyResources(this.txtCurrencyAmount, "txtCurrencyAmount");
            this.txtCurrencyAmount.MenuManager = this.barManager1;
            this.txtCurrencyAmount.Name = "txtCurrencyAmount";
            this.txtCurrencyAmount.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtCurrencyAmount.StyleController = this.layoutControl1;
            this.txtCurrencyAmount.EditValueChanged += new System.EventHandler(this.txtCurrencyAmount_EditValueChanged);
            // 
            // glkpCurrencyCountry
            // 
            resources.ApplyResources(this.glkpCurrencyCountry, "glkpCurrencyCountry");
            this.glkpCurrencyCountry.MenuManager = this.barManager1;
            this.glkpCurrencyCountry.Name = "glkpCurrencyCountry";
            this.glkpCurrencyCountry.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpCurrencyCountry.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpCurrencyCountry.Properties.Buttons"))))});
            this.glkpCurrencyCountry.Properties.NullText = resources.GetString("glkpCurrencyCountry.Properties.NullText");
            this.glkpCurrencyCountry.Properties.View = this.gridLookUpEdit1View;
            this.glkpCurrencyCountry.StyleController = this.layoutControl1;
            this.glkpCurrencyCountry.EditValueChanged += new System.EventHandler(this.glkpCurrencyCountry_EditValueChanged);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcCountryId,
            this.gcCurrency,
            this.gcCurrencyName,
            this.gcCurrencySymbol});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // gcCountryId
            // 
            resources.ApplyResources(this.gcCountryId, "gcCountryId");
            this.gcCountryId.FieldName = "COUNTRY_ID";
            this.gcCountryId.Name = "gcCountryId";
            // 
            // gcCurrency
            // 
            resources.ApplyResources(this.gcCurrency, "gcCurrency");
            this.gcCurrency.FieldName = "CURRENCY";
            this.gcCurrency.Name = "gcCurrency";
            // 
            // gcCurrencyName
            // 
            resources.ApplyResources(this.gcCurrencyName, "gcCurrencyName");
            this.gcCurrencyName.FieldName = "CURRENCY_NAME";
            this.gcCurrencyName.Name = "gcCurrencyName";
            // 
            // gcCurrencySymbol
            // 
            resources.ApplyResources(this.gcCurrencySymbol, "gcCurrencySymbol");
            this.gcCurrencySymbol.FieldNameSortGroup = "CURRENCY_SYMBOL";
            this.gcCurrencySymbol.Name = "gcCurrencySymbol";
            // 
            // ucAssetJournal1
            // 
            this.ucAssetJournal1.BankAmount = 0D;
            this.ucAssetJournal1.BeforeFocusControl = null;
            this.ucAssetJournal1.CrTotal = 0D;
            this.ucAssetJournal1.CurrencyCountryId = 0;
            this.ucAssetJournal1.DrTotal = 0D;
            this.ucAssetJournal1.EnableCashBankGrid = true;
            this.ucAssetJournal1.Flag = Bosco.Utility.AssetInOut.OP;
            resources.ApplyResources(this.ucAssetJournal1, "ucAssetJournal1");
            this.ucAssetJournal1.MinDate = null;
            this.ucAssetJournal1.Name = "ucAssetJournal1";
            this.ucAssetJournal1.NextFocusControl = null;
            this.ucAssetJournal1.ProjectId = 0;
            this.ucAssetJournal1.PurchaseTransSummary = 0D;
            this.ucAssetJournal1.ShowDeleteColumn = true;
            // 
            // txtBillInvoiceNo
            // 
            resources.ApplyResources(this.txtBillInvoiceNo, "txtBillInvoiceNo");
            this.txtBillInvoiceNo.MenuManager = this.barManager1;
            this.txtBillInvoiceNo.Name = "txtBillInvoiceNo";
            this.txtBillInvoiceNo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtBillInvoiceNo.Properties.MaxLength = 10;
            this.txtBillInvoiceNo.StyleController = this.layoutControl1;
            this.txtBillInvoiceNo.Leave += new System.EventHandler(this.txtBillInvoiceNo_Leave);
            // 
            // ucAssetVoucherShortcuts
            // 
            this.ucAssetVoucherShortcuts.DisableAssetItem = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetVoucherShortcuts.DisableAssetVoucherView = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetVoucherShortcuts.DisableBankAccount = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetVoucherShortcuts.DisableConfigure = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetVoucherShortcuts.DisableCostCentre = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetVoucherShortcuts.DisableCustodian = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetVoucherShortcuts.DisableDate = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetVoucherShortcuts.DisableDispose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetVoucherShortcuts.DisableDonation = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetVoucherShortcuts.DisableDonor = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetVoucherShortcuts.DisableInkind = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetVoucherShortcuts.DisableLedger = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetVoucherShortcuts.DisableLedgerOption = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetVoucherShortcuts.DisableLocation = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetVoucherShortcuts.DisableManufacturer = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetVoucherShortcuts.DisableMapping = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetVoucherShortcuts.DisableMappLocation = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetVoucherShortcuts.DisableNextDate = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetVoucherShortcuts.DisableProject = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetVoucherShortcuts.DisablePUrchase = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetVoucherShortcuts.DisableSales = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetVoucherShortcuts.DisableVendor = DevExpress.XtraBars.BarItemVisibility.Never;
            resources.ApplyResources(this.ucAssetVoucherShortcuts, "ucAssetVoucherShortcuts");
            this.ucAssetVoucherShortcuts.Name = "ucAssetVoucherShortcuts";
            this.ucAssetVoucherShortcuts.BankAccountClicked += new System.EventHandler(this.ucAssetVoucherShortcuts_BankAccountClicked);
            this.ucAssetVoucherShortcuts.LocationMappingClicked += new System.EventHandler(this.ucAssetVoucherShortcuts_LocationMappingClicked);
            this.ucAssetVoucherShortcuts.ConfigureClicked += new System.EventHandler(this.ucAssetVoucherShortcuts_ConfigureClicked);
            this.ucAssetVoucherShortcuts.DateClicked += new System.EventHandler(this.ucAssetVoucherShortcuts_DateClicked);
            this.ucAssetVoucherShortcuts.NextDateClicked += new System.EventHandler(this.ucAssetVoucherShortcuts_NextDateClicked);
            this.ucAssetVoucherShortcuts.ProjectClicked += new System.EventHandler(this.ucAssetVoucherShortcuts_ProjectClicked);
            this.ucAssetVoucherShortcuts.LocationClicked += new System.EventHandler(this.ucAssetVoucherShortcuts_LocationClicked);
            this.ucAssetVoucherShortcuts.CustodianClicked += new System.EventHandler(this.ucAssetVoucherShortcuts_CustodianClicked);
            this.ucAssetVoucherShortcuts.VendorClicked += new System.EventHandler(this.ucAssetVoucherShortcuts_VendorClicked);
            this.ucAssetVoucherShortcuts.AssetItemClicked += new System.EventHandler(this.ucAssetVoucherShortcuts_AssetItemClicked);
            this.ucAssetVoucherShortcuts.ManufacturerClicked += new System.EventHandler(this.ucAssetVoucherShortcuts_ManufacturerClicked);
            this.ucAssetVoucherShortcuts.DonorClicked += new System.EventHandler(this.ucAssetVoucherShortcuts_DonorClicked);
            this.ucAssetVoucherShortcuts.AccountMappingClicked += new System.EventHandler(this.ucAssetVoucherShortcuts_AccountMappingClicked);
            this.ucAssetVoucherShortcuts.SalesClicked += new System.EventHandler(this.ucAssetVoucherShortcuts_SalesClicked);
            this.ucAssetVoucherShortcuts.DonationClicked += new System.EventHandler(this.ucAssetVoucherShortcuts_DonationClicked);
            this.ucAssetVoucherShortcuts.DisposeClicked += new System.EventHandler(this.ucAssetVoucherShortcuts_DisposeClicked);
            this.ucAssetVoucherShortcuts.LedgerClicked += new System.EventHandler(this.ucAssetVoucherShortcuts_LedgerClicked);
            this.ucAssetVoucherShortcuts.LedgerOptionClicked += new System.EventHandler(this.ucAssetVoucherShortcuts_LedgerOptionClicked);
            this.ucAssetVoucherShortcuts.AssetVoucherViewClicked += new System.EventHandler(this.ucAssetVoucherShortcuts_AssetVoucherViewClicked);
            // 
            // rgTransactionType
            // 
            resources.ApplyResources(this.rgTransactionType, "rgTransactionType");
            this.rgTransactionType.MenuManager = this.barManager1;
            this.rgTransactionType.Name = "rgTransactionType";
            this.rgTransactionType.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("rgTransactionType.Properties.Appearance.Font")));
            this.rgTransactionType.Properties.Appearance.Options.UseFont = true;
            this.rgTransactionType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.rgTransactionType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("rgTransactionType.Properties.Items"))), resources.GetString("rgTransactionType.Properties.Items1")),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("rgTransactionType.Properties.Items2"))), resources.GetString("rgTransactionType.Properties.Items3")),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("rgTransactionType.Properties.Items4"))), resources.GetString("rgTransactionType.Properties.Items5"))});
            this.rgTransactionType.StyleController = this.layoutControl1;
            this.rgTransactionType.SelectedIndexChanged += new System.EventHandler(this.rgTransactionType_SelectedIndexChanged);
            // 
            // btnNew
            // 
            this.btnNew.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.btnNew, "btnNew");
            this.btnNew.Name = "btnNew";
            this.btnNew.StyleController = this.layoutControl1;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // txtOtherCharges
            // 
            resources.ApplyResources(this.txtOtherCharges, "txtOtherCharges");
            this.txtOtherCharges.MenuManager = this.barManager1;
            this.txtOtherCharges.Name = "txtOtherCharges";
            this.txtOtherCharges.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtOtherCharges.Properties.Mask.EditMask = resources.GetString("txtOtherCharges.Properties.Mask.EditMask");
            this.txtOtherCharges.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("txtOtherCharges.Properties.Mask.MaskType")));
            this.txtOtherCharges.Properties.MaxLength = 13;
            this.txtOtherCharges.StyleController = this.layoutControl1;
            this.txtOtherCharges.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtOtherCharges_KeyDown);
            // 
            // txtNarration
            // 
            resources.ApplyResources(this.txtNarration, "txtNarration");
            this.txtNarration.MenuManager = this.barManager1;
            this.txtNarration.Name = "txtNarration";
            this.txtNarration.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtNarration.StyleController = this.layoutControl1;
            // 
            // txtSoldTo
            // 
            this.txtSoldTo.EnterMoveNextControl = true;
            resources.ApplyResources(this.txtSoldTo, "txtSoldTo");
            this.txtSoldTo.MenuManager = this.barManager1;
            this.txtSoldTo.Name = "txtSoldTo";
            this.txtSoldTo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtSoldTo.Properties.MaxLength = 48;
            this.txtSoldTo.StyleController = this.layoutControl1;
            this.txtSoldTo.Leave += new System.EventHandler(this.txtSoldTo_Leave);
            // 
            // txtVoucherNo
            // 
            resources.ApplyResources(this.txtVoucherNo, "txtVoucherNo");
            this.txtVoucherNo.EnterMoveNextControl = true;
            this.txtVoucherNo.MenuManager = this.barManager1;
            this.txtVoucherNo.Name = "txtVoucherNo";
            this.txtVoucherNo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtVoucherNo.Properties.MaxLength = 20;
            this.txtVoucherNo.StyleController = this.layoutControl1;
            // 
            // dtSalesDate
            // 
            resources.ApplyResources(this.dtSalesDate, "dtSalesDate");
            this.dtSalesDate.EnterMoveNextControl = true;
            this.dtSalesDate.MenuManager = this.barManager1;
            this.dtSalesDate.Name = "dtSalesDate";
            this.dtSalesDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.dtSalesDate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.dtSalesDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dtSalesDate.Properties.Buttons"))))});
            this.dtSalesDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dtSalesDate.Properties.CalendarTimeProperties.Buttons"))))});
            this.dtSalesDate.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("dtSalesDate.Properties.Mask.MaskType")));
            this.dtSalesDate.StyleController = this.layoutControl1;
            this.dtSalesDate.EditValueChanged += new System.EventHandler(this.dtSalesDate_EditValueChanged);
            // 
            // btnSave
            // 
            this.btnSave.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.layoutControl1;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // ucCaptionTitle
            // 
            this.ucCaptionTitle.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.ucCaptionTitle, "ucCaptionTitle");
            this.ucCaptionTitle.Name = "ucCaptionTitle";
            // 
            // gcInOut
            // 
            resources.ApplyResources(this.gcInOut, "gcInOut");
            this.gcInOut.MainView = this.gvInOutAdd;
            this.gcInOut.MenuManager = this.barManager1;
            this.gcInOut.Name = "gcInOut";
            this.gcInOut.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rglkpGroup,
            this.rglkpLocation,
            this.rglkpAssetName,
            this.rccmbAssetId,
            this.rtxtDiscount,
            this.rtxtAmount,
            this.rbiDelete,
            this.rtxtQuantity,
            this.rbtView,
            this.rglkpSource,
            this.rtxtActualAmount,
            this.rbtnCostCentre});
            this.gcInOut.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvInOutAdd});
            this.gcInOut.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.gcSalesAdd_ProcessGridKey);
            this.gcInOut.Click += new System.EventHandler(this.gcInOut_Click);
            // 
            // gvInOutAdd
            // 
            this.gvInOutAdd.Appearance.FocusedCell.Font = ((System.Drawing.Font)(resources.GetObject("gvInOutAdd.Appearance.FocusedCell.Font")));
            this.gvInOutAdd.Appearance.FocusedCell.Options.UseFont = true;
            this.gvInOutAdd.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvInOutAdd.Appearance.FocusedRow.Font")));
            this.gvInOutAdd.Appearance.FocusedRow.Options.UseFont = true;
            this.gvInOutAdd.Appearance.FooterPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvInOutAdd.Appearance.FooterPanel.Font")));
            this.gvInOutAdd.Appearance.FooterPanel.ForeColor = ((System.Drawing.Color)(resources.GetObject("gvInOutAdd.Appearance.FooterPanel.ForeColor")));
            this.gvInOutAdd.Appearance.FooterPanel.Options.UseFont = true;
            this.gvInOutAdd.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gvInOutAdd.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvInOutAdd.Appearance.HeaderPanel.Font")));
            this.gvInOutAdd.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvInOutAdd.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gvInOutAdd.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSource,
            this.colSalesId,
            this.colLocation,
            this.colAsset,
            this.colQuantity,
            this.colAmount,
            this.colGroup,
            this.ColDiscount,
            this.colAction,
            this.colTemp,
            this.colInoutDetailId,
            this.colAvailableQuantity,
            this.colView,
            this.colLedgerId,
            this.colActualAmount,
            this.colDifference,
            this.colType,
            this.colCosCentre});
            this.gvInOutAdd.GridControl = this.gcInOut;
            this.gvInOutAdd.Name = "gvInOutAdd";
            this.gvInOutAdd.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gvInOutAdd.OptionsBehavior.ImmediateUpdateRowPosition = false;
            this.gvInOutAdd.OptionsBehavior.SummariesIgnoreNullValues = true;
            this.gvInOutAdd.OptionsCustomization.AllowSort = false;
            this.gvInOutAdd.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gvInOutAdd.OptionsFind.AllowFindPanel = false;
            this.gvInOutAdd.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.True;
            this.gvInOutAdd.OptionsNavigation.AutoFocusNewRow = true;
            this.gvInOutAdd.OptionsNavigation.EnterMoveNextColumn = true;
            this.gvInOutAdd.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gvInOutAdd.OptionsView.ShowFooter = true;
            this.gvInOutAdd.OptionsView.ShowGroupPanel = false;
            this.gvInOutAdd.OptionsView.ShowIndicator = false;
            this.gvInOutAdd.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvInOutAdd_RowCellStyle);
            this.gvInOutAdd.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gvInOutAdd_ShowingEditor);
            this.gvInOutAdd.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvInOutAdd_CellValueChanged);
            this.gvInOutAdd.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvInOutAdd_CellValueChanging);
            // 
            // colSource
            // 
            this.colSource.AppearanceCell.Options.UseFont = true;
            this.colSource.ColumnEdit = this.rglkpSource;
            this.colSource.FieldName = "SOURCE";
            this.colSource.Name = "colSource";
            this.colSource.OptionsColumn.AllowEdit = false;
            this.colSource.OptionsColumn.AllowFocus = false;
            this.colSource.OptionsColumn.AllowMove = false;
            this.colSource.OptionsColumn.AllowShowHide = false;
            this.colSource.OptionsColumn.AllowSize = false;
            this.colSource.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colSource.OptionsColumn.FixedWidth = true;
            this.colSource.OptionsColumn.ReadOnly = true;
            this.colSource.OptionsColumn.ShowCaption = false;
            this.colSource.OptionsColumn.TabStop = false;
            this.colSource.OptionsFilter.AllowAutoFilter = false;
            this.colSource.OptionsFilter.AllowFilter = false;
            resources.ApplyResources(this.colSource, "colSource");
            // 
            // rglkpSource
            // 
            resources.ApplyResources(this.rglkpSource, "rglkpSource");
            this.rglkpSource.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rglkpSource.Buttons"))))});
            this.rglkpSource.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F2);
            this.rglkpSource.Name = "rglkpSource";
            this.rglkpSource.PopupFormMinSize = new System.Drawing.Size(10, 10);
            this.rglkpSource.PopupFormSize = new System.Drawing.Size(25, 45);
            this.rglkpSource.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.rglkpSource.View = this.gridView3;
            // 
            // gridView3
            // 
            this.gridView3.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colName});
            this.gridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            // 
            // colId
            // 
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            // 
            // colName
            // 
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            resources.ApplyResources(this.colName, "colName");
            // 
            // colSalesId
            // 
            resources.ApplyResources(this.colSalesId, "colSalesId");
            this.colSalesId.FieldName = "SALES_ID";
            this.colSalesId.Name = "colSalesId";
            // 
            // colLocation
            // 
            resources.ApplyResources(this.colLocation, "colLocation");
            this.colLocation.ColumnEdit = this.rglkpLocation;
            this.colLocation.FieldName = "LOCATION_ID";
            this.colLocation.Name = "colLocation";
            this.colLocation.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedCell;
            // 
            // rglkpLocation
            // 
            resources.ApplyResources(this.rglkpLocation, "rglkpLocation");
            this.rglkpLocation.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rglkpLocation.Buttons"))))});
            this.rglkpLocation.ImmediatePopup = true;
            this.rglkpLocation.Name = "rglkpLocation";
            this.rglkpLocation.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.rglkpLocation.PopupFormMinSize = new System.Drawing.Size(200, 150);
            this.rglkpLocation.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.rglkpLocation.View = this.gridView1;
            // 
            // gridView1
            // 
            this.gridView1.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridView1.Appearance.FocusedRow.Font")));
            this.gridView1.Appearance.FocusedRow.Options.UseFont = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colLocation_Id,
            this.colLocationName});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowColumnHeaders = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            // 
            // colLocation_Id
            // 
            resources.ApplyResources(this.colLocation_Id, "colLocation_Id");
            this.colLocation_Id.FieldName = "LOCATION_ID";
            this.colLocation_Id.Name = "colLocation_Id";
            // 
            // colLocationName
            // 
            resources.ApplyResources(this.colLocationName, "colLocationName");
            this.colLocationName.FieldName = "LOCATION";
            this.colLocationName.Name = "colLocationName";
            // 
            // colAsset
            // 
            resources.ApplyResources(this.colAsset, "colAsset");
            this.colAsset.ColumnEdit = this.rglkpAssetName;
            this.colAsset.FieldName = "ITEM_ID";
            this.colAsset.Name = "colAsset";
            this.colAsset.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedCell;
            // 
            // rglkpAssetName
            // 
            this.rglkpAssetName.AppearanceFocused.Font = ((System.Drawing.Font)(resources.GetObject("rglkpAssetName.AppearanceFocused.Font")));
            this.rglkpAssetName.AppearanceFocused.Options.UseFont = true;
            resources.ApplyResources(this.rglkpAssetName, "rglkpAssetName");
            this.rglkpAssetName.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rglkpAssetName.Buttons"))))});
            this.rglkpAssetName.ImmediatePopup = true;
            this.rglkpAssetName.Name = "rglkpAssetName";
            this.rglkpAssetName.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.rglkpAssetName.PopupFormSize = new System.Drawing.Size(600, 100);
            this.rglkpAssetName.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.rglkpAssetName.View = this.gridView2;
            this.rglkpAssetName.Enter += new System.EventHandler(this.rglkpAssetName_Enter);
            // 
            // gridView2
            // 
            this.gridView2.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridView2.Appearance.FocusedRow.Font")));
            this.gridView2.Appearance.FocusedRow.Options.UseFont = true;
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colItemId,
            this.colAssetName,
            this.colAccountLedgerId});
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowColumnHeaders = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            this.gridView2.OptionsView.ShowIndicator = false;
            // 
            // colItemId
            // 
            resources.ApplyResources(this.colItemId, "colItemId");
            this.colItemId.FieldName = "ITEM_ID";
            this.colItemId.Name = "colItemId";
            // 
            // colAssetName
            // 
            resources.ApplyResources(this.colAssetName, "colAssetName");
            this.colAssetName.FieldName = "ASSET_ITEM";
            this.colAssetName.Name = "colAssetName";
            // 
            // colAccountLedgerId
            // 
            this.colAccountLedgerId.FieldName = "ACCOUNT_LEDGER_ID";
            this.colAccountLedgerId.Name = "colAccountLedgerId";
            // 
            // colQuantity
            // 
            resources.ApplyResources(this.colQuantity, "colQuantity");
            this.colQuantity.ColumnEdit = this.rtxtQuantity;
            this.colQuantity.FieldName = "QUANTITY";
            this.colQuantity.Name = "colQuantity";
            this.colQuantity.OptionsColumn.FixedWidth = true;
            // 
            // rtxtQuantity
            // 
            resources.ApplyResources(this.rtxtQuantity, "rtxtQuantity");
            this.rtxtQuantity.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rtxtQuantity.EditFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rtxtQuantity.Mask.EditMask = resources.GetString("rtxtQuantity.Mask.EditMask");
            this.rtxtQuantity.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("rtxtQuantity.Mask.MaskType")));
            this.rtxtQuantity.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("rtxtQuantity.Mask.ShowPlaceHolders")));
            this.rtxtQuantity.MaxLength = 4;
            this.rtxtQuantity.Name = "rtxtQuantity";
            this.rtxtQuantity.EditValueChanging += new DevExpress.XtraEditors.Controls.ChangingEventHandler(this.rtxtQuantity_EditValueChanging);
            this.rtxtQuantity.Enter += new System.EventHandler(this.rtxtQuantity_Enter);
            this.rtxtQuantity.Leave += new System.EventHandler(this.rtxtQuantity_Leave);
            // 
            // colAmount
            // 
            resources.ApplyResources(this.colAmount, "colAmount");
            this.colAmount.ColumnEdit = this.rtxtAmount;
            this.colAmount.DisplayFormat.FormatString = "n";
            this.colAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colAmount.FieldName = "AMOUNT";
            this.colAmount.Name = "colAmount";
            this.colAmount.OptionsColumn.AllowEdit = false;
            this.colAmount.OptionsColumn.AllowFocus = false;
            this.colAmount.OptionsColumn.FixedWidth = true;
            this.colAmount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("colAmount.Summary"))), resources.GetString("colAmount.Summary1"), resources.GetString("colAmount.Summary2"))});
            // 
            // rtxtAmount
            // 
            resources.ApplyResources(this.rtxtAmount, "rtxtAmount");
            this.rtxtAmount.Mask.EditMask = resources.GetString("rtxtAmount.Mask.EditMask");
            this.rtxtAmount.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("rtxtAmount.Mask.MaskType")));
            this.rtxtAmount.Name = "rtxtAmount";
            // 
            // colGroup
            // 
            resources.ApplyResources(this.colGroup, "colGroup");
            this.colGroup.ColumnEdit = this.rglkpGroup;
            this.colGroup.FieldName = "GROUP_ID";
            this.colGroup.Name = "colGroup";
            this.colGroup.OptionsColumn.AllowEdit = false;
            this.colGroup.OptionsColumn.AllowFocus = false;
            this.colGroup.OptionsColumn.ReadOnly = true;
            this.colGroup.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedCell;
            // 
            // rglkpGroup
            // 
            resources.ApplyResources(this.rglkpGroup, "rglkpGroup");
            this.rglkpGroup.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rglkpGroup.Buttons"))))});
            this.rglkpGroup.ImmediatePopup = true;
            this.rglkpGroup.Name = "rglkpGroup";
            this.rglkpGroup.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.rglkpGroup.PopupFormMinSize = new System.Drawing.Size(200, 150);
            this.rglkpGroup.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.rglkpGroup.View = this.repositoryItemGridLookUpEdit1View;
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colGroup_Id,
            this.colGroupName});
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // colGroup_Id
            // 
            resources.ApplyResources(this.colGroup_Id, "colGroup_Id");
            this.colGroup_Id.FieldName = "GROUP_ID";
            this.colGroup_Id.Name = "colGroup_Id";
            // 
            // colGroupName
            // 
            resources.ApplyResources(this.colGroupName, "colGroupName");
            this.colGroupName.FieldName = "GROUP_NAME";
            this.colGroupName.Name = "colGroupName";
            this.colGroupName.OptionsColumn.AllowFocus = false;
            this.colGroupName.OptionsColumn.ShowCaption = false;
            // 
            // ColDiscount
            // 
            resources.ApplyResources(this.ColDiscount, "ColDiscount");
            this.ColDiscount.ColumnEdit = this.rtxtDiscount;
            this.ColDiscount.DisplayFormat.FormatString = "n";
            this.ColDiscount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ColDiscount.FieldName = "PERCENTAGE";
            this.ColDiscount.Name = "ColDiscount";
            this.ColDiscount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("ColDiscount.Summary"))), resources.GetString("ColDiscount.Summary1"), resources.GetString("ColDiscount.Summary2"))});
            // 
            // rtxtDiscount
            // 
            resources.ApplyResources(this.rtxtDiscount, "rtxtDiscount");
            this.rtxtDiscount.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.rtxtDiscount.Mask.EditMask = resources.GetString("rtxtDiscount.Mask.EditMask");
            this.rtxtDiscount.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("rtxtDiscount.Mask.MaskType")));
            this.rtxtDiscount.Name = "rtxtDiscount";
            // 
            // colAction
            // 
            this.colAction.ColumnEdit = this.rbiDelete;
            this.colAction.Name = "colAction";
            this.colAction.OptionsColumn.FixedWidth = true;
            this.colAction.OptionsColumn.TabStop = false;
            resources.ApplyResources(this.colAction, "colAction");
            // 
            // rbiDelete
            // 
            resources.ApplyResources(this.rbiDelete, "rbiDelete");
            this.rbiDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rbiDelete.Buttons"))), resources.GetString("rbiDelete.Buttons1"), ((int)(resources.GetObject("rbiDelete.Buttons2"))), ((bool)(resources.GetObject("rbiDelete.Buttons3"))), ((bool)(resources.GetObject("rbiDelete.Buttons4"))), ((bool)(resources.GetObject("rbiDelete.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("rbiDelete.Buttons6"))), global::ACPP.Properties.Resources.Delete_Mob, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, resources.GetString("rbiDelete.Buttons7"), ((object)(resources.GetObject("rbiDelete.Buttons8"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("rbiDelete.Buttons9"))), ((bool)(resources.GetObject("rbiDelete.Buttons10"))))});
            this.rbiDelete.Name = "rbiDelete";
            this.rbiDelete.Click += new System.EventHandler(this.ribDelete_Click);
            // 
            // colTemp
            // 
            resources.ApplyResources(this.colTemp, "colTemp");
            this.colTemp.FieldName = "TEMP_AMOUNT";
            this.colTemp.Name = "colTemp";
            // 
            // colInoutDetailId
            // 
            resources.ApplyResources(this.colInoutDetailId, "colInoutDetailId");
            this.colInoutDetailId.FieldName = "IN_OUT_DETAIL_ID";
            this.colInoutDetailId.Name = "colInoutDetailId";
            // 
            // colAvailableQuantity
            // 
            resources.ApplyResources(this.colAvailableQuantity, "colAvailableQuantity");
            this.colAvailableQuantity.FieldName = "AVAILABLE_QUANTITY";
            this.colAvailableQuantity.Name = "colAvailableQuantity";
            this.colAvailableQuantity.OptionsColumn.AllowEdit = false;
            this.colAvailableQuantity.OptionsColumn.AllowFocus = false;
            this.colAvailableQuantity.OptionsColumn.AllowMove = false;
            this.colAvailableQuantity.OptionsColumn.AllowSize = false;
            this.colAvailableQuantity.OptionsColumn.FixedWidth = true;
            // 
            // colView
            // 
            this.colView.ColumnEdit = this.rbtView;
            this.colView.Name = "colView";
            this.colView.OptionsColumn.FixedWidth = true;
            this.colView.OptionsColumn.TabStop = false;
            resources.ApplyResources(this.colView, "colView");
            // 
            // rbtView
            // 
            resources.ApplyResources(this.rbtView, "rbtView");
            this.rbtView.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rbtView.Buttons"))), resources.GetString("rbtView.Buttons1"), ((int)(resources.GetObject("rbtView.Buttons2"))), ((bool)(resources.GetObject("rbtView.Buttons3"))), ((bool)(resources.GetObject("rbtView.Buttons4"))), ((bool)(resources.GetObject("rbtView.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("rbtView.Buttons6"))), global::ACPP.Properties.Resources.Used_Codes, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, resources.GetString("rbtView.Buttons7"), ((object)(resources.GetObject("rbtView.Buttons8"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("rbtView.Buttons9"))), ((bool)(resources.GetObject("rbtView.Buttons10"))))});
            this.rbtView.Name = "rbtView";
            this.rbtView.Click += new System.EventHandler(this.rbtView_Click);
            // 
            // colLedgerId
            // 
            this.colLedgerId.FieldName = "LEDGER_ID";
            this.colLedgerId.Name = "colLedgerId";
            // 
            // colActualAmount
            // 
            resources.ApplyResources(this.colActualAmount, "colActualAmount");
            this.colActualAmount.ColumnEdit = this.rtxtActualAmount;
            this.colActualAmount.DisplayFormat.FormatString = "n";
            this.colActualAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colActualAmount.FieldName = "TEMP_AMOUNT";
            this.colActualAmount.Name = "colActualAmount";
            this.colActualAmount.OptionsColumn.AllowEdit = false;
            this.colActualAmount.OptionsColumn.AllowFocus = false;
            this.colActualAmount.OptionsColumn.FixedWidth = true;
            this.colActualAmount.OptionsColumn.TabStop = false;
            this.colActualAmount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("colActualAmount.Summary"))), resources.GetString("colActualAmount.Summary1"), resources.GetString("colActualAmount.Summary2"))});
            // 
            // rtxtActualAmount
            // 
            resources.ApplyResources(this.rtxtActualAmount, "rtxtActualAmount");
            this.rtxtActualAmount.Mask.EditMask = resources.GetString("rtxtActualAmount.Mask.EditMask");
            this.rtxtActualAmount.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("rtxtActualAmount.Mask.MaskType")));
            this.rtxtActualAmount.Name = "rtxtActualAmount";
            // 
            // colDifference
            // 
            resources.ApplyResources(this.colDifference, "colDifference");
            this.colDifference.DisplayFormat.FormatString = "n";
            this.colDifference.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colDifference.FieldName = "DIFFERENCE";
            this.colDifference.Name = "colDifference";
            this.colDifference.OptionsColumn.AllowEdit = false;
            this.colDifference.OptionsColumn.AllowFocus = false;
            this.colDifference.OptionsColumn.AllowMove = false;
            this.colDifference.OptionsColumn.AllowSize = false;
            this.colDifference.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colDifference.OptionsColumn.FixedWidth = true;
            this.colDifference.OptionsColumn.TabStop = false;
            this.colDifference.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("colDifference.Summary"))), resources.GetString("colDifference.Summary1"), resources.GetString("colDifference.Summary2"))});
            // 
            // colType
            // 
            this.colType.AppearanceCell.Font = ((System.Drawing.Font)(resources.GetObject("colType.AppearanceCell.Font")));
            this.colType.AppearanceCell.Options.UseFont = true;
            resources.ApplyResources(this.colType, "colType");
            this.colType.FieldName = "TYPE";
            this.colType.Name = "colType";
            this.colType.OptionsColumn.AllowEdit = false;
            this.colType.OptionsColumn.AllowFocus = false;
            this.colType.OptionsColumn.AllowMove = false;
            this.colType.OptionsColumn.AllowSize = false;
            this.colType.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.True;
            this.colType.OptionsColumn.FixedWidth = true;
            this.colType.OptionsColumn.TabStop = false;
            // 
            // colCosCentre
            // 
            this.colCosCentre.ColumnEdit = this.rbtnCostCentre;
            this.colCosCentre.Name = "colCosCentre";
            this.colCosCentre.OptionsColumn.FixedWidth = true;
            this.colCosCentre.OptionsColumn.TabStop = false;
            this.colCosCentre.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            resources.ApplyResources(this.colCosCentre, "colCosCentre");
            // 
            // rbtnCostCentre
            // 
            resources.ApplyResources(this.rbtnCostCentre, "rbtnCostCentre");
            this.rbtnCostCentre.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rbtnCostCentre.Buttons"))), resources.GetString("rbtnCostCentre.Buttons1"), ((int)(resources.GetObject("rbtnCostCentre.Buttons2"))), ((bool)(resources.GetObject("rbtnCostCentre.Buttons3"))), ((bool)(resources.GetObject("rbtnCostCentre.Buttons4"))), ((bool)(resources.GetObject("rbtnCostCentre.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("rbtnCostCentre.Buttons6"))), global::ACPP.Properties.Resources.Donor_mapping, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject3, resources.GetString("rbtnCostCentre.Buttons7"), ((object)(resources.GetObject("rbtnCostCentre.Buttons8"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("rbtnCostCentre.Buttons9"))), ((bool)(resources.GetObject("rbtnCostCentre.Buttons10"))))});
            this.rbtnCostCentre.Name = "rbtnCostCentre";
            this.rbtnCostCentre.Click += new System.EventHandler(this.rbtnCostCentre_Click);
            // 
            // rccmbAssetId
            // 
            this.rccmbAssetId.AppearanceFocused.Font = ((System.Drawing.Font)(resources.GetObject("rccmbAssetId.AppearanceFocused.Font")));
            this.rccmbAssetId.AppearanceFocused.Options.UseFont = true;
            resources.ApplyResources(this.rccmbAssetId, "rccmbAssetId");
            this.rccmbAssetId.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rccmbAssetId.Buttons"))))});
            this.rccmbAssetId.DisplayMember = "ASSET_ID";
            this.rccmbAssetId.ForceUpdateEditValue = DevExpress.Utils.DefaultBoolean.True;
            this.rccmbAssetId.Name = "rccmbAssetId";
            this.rccmbAssetId.PopupFormMinSize = new System.Drawing.Size(179, 0);
            this.rccmbAssetId.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.rccmbAssetId.ValueMember = "ASSET_ID";
            // 
            // layoutControlItem2
            // 
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(884, 0);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(1, 1);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(104, 629);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.lcgSales,
            this.layoutControlItem12,
            this.layoutControlGroup2,
            this.layoutControlItem10});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1014, 414);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ucCaptionTitle;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 40);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(104, 40);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(808, 40);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // lcgSales
            // 
            this.lcgSales.AllowHtmlStringInCaption = true;
            this.lcgSales.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lcgSales.AppearanceGroup.Font")));
            this.lcgSales.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.lcgSales, "lcgSales");
            this.lcgSales.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem8,
            this.lblSalesDate,
            this.lblName,
            this.emptySpaceItem1,
            this.layoutControlItem9,
            this.layoutControlItem5,
            this.layoutControlItem7,
            this.layoutControlItem14,
            this.layoutControlItem6,
            this.lblVoucherNo,
            this.layoutControlItem13,
            this.emptySpaceItem2,
            this.emptySpaceItem4,
            this.emptySpaceItem8,
            this.layoutControlItem11,
            this.lciCurrency});
            this.lcgSales.Location = new System.Drawing.Point(0, 81);
            this.lcgSales.Name = "lcgSales";
            this.lcgSales.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lcgSales.Size = new System.Drawing.Size(910, 333);
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.gcInOut;
            resources.ApplyResources(this.layoutControlItem8, "layoutControlItem8");
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 56);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(894, 107);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // lblSalesDate
            // 
            this.lblSalesDate.AllowHtmlStringInCaption = true;
            this.lblSalesDate.Control = this.dtSalesDate;
            resources.ApplyResources(this.lblSalesDate, "lblSalesDate");
            this.lblSalesDate.Location = new System.Drawing.Point(0, 0);
            this.lblSalesDate.MaxSize = new System.Drawing.Size(146, 30);
            this.lblSalesDate.MinSize = new System.Drawing.Size(146, 30);
            this.lblSalesDate.Name = "lblSalesDate";
            this.lblSalesDate.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 2, 5, 5);
            this.lblSalesDate.Size = new System.Drawing.Size(146, 30);
            this.lblSalesDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblSalesDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblSalesDate.TextSize = new System.Drawing.Size(32, 13);
            this.lblSalesDate.TextToControlDistance = 5;
            // 
            // lblName
            // 
            this.lblName.AllowHtmlStringInCaption = true;
            this.lblName.Control = this.txtSoldTo;
            resources.ApplyResources(this.lblName, "lblName");
            this.lblName.Location = new System.Drawing.Point(528, 0);
            this.lblName.MaxSize = new System.Drawing.Size(0, 30);
            this.lblName.MinSize = new System.Drawing.Size(188, 30);
            this.lblName.Name = "lblName";
            this.lblName.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 2, 5, 5);
            this.lblName.Size = new System.Drawing.Size(250, 30);
            this.lblName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblName.TextSize = new System.Drawing.Size(55, 13);
            this.lblName.TextToControlDistance = 5;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(502, 0);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(26, 30);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(26, 30);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(26, 30);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.AllowHtmlStringInCaption = true;
            this.layoutControlItem9.Control = this.txtNarration;
            resources.ApplyResources(this.layoutControlItem9, "layoutControlItem9");
            this.layoutControlItem9.Location = new System.Drawing.Point(203, 272);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 4, 2);
            this.layoutControlItem9.Size = new System.Drawing.Size(483, 26);
            this.layoutControlItem9.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem9.TextSize = new System.Drawing.Size(45, 13);
            this.layoutControlItem9.TextToControlDistance = 5;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnClose;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(825, 272);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(69, 26);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(69, 26);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnSave;
            resources.ApplyResources(this.layoutControlItem7, "layoutControlItem7");
            this.layoutControlItem7.Location = new System.Drawing.Point(686, 272);
            this.layoutControlItem7.MaxSize = new System.Drawing.Size(70, 26);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(70, 26);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(70, 26);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.txtOtherCharges;
            resources.ApplyResources(this.layoutControlItem14, "layoutControlItem14");
            this.layoutControlItem14.Location = new System.Drawing.Point(0, 272);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 3, 2);
            this.layoutControlItem14.Size = new System.Drawing.Size(203, 26);
            this.layoutControlItem14.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem14.TextSize = new System.Drawing.Size(71, 13);
            this.layoutControlItem14.TextToControlDistance = 5;
            this.layoutControlItem14.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnNew;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(756, 272);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(69, 26);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(69, 26);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // lblVoucherNo
            // 
            this.lblVoucherNo.Control = this.txtVoucherNo;
            resources.ApplyResources(this.lblVoucherNo, "lblVoucherNo");
            this.lblVoucherNo.Location = new System.Drawing.Point(364, 0);
            this.lblVoucherNo.MaxSize = new System.Drawing.Size(138, 30);
            this.lblVoucherNo.MinSize = new System.Drawing.Size(138, 30);
            this.lblVoucherNo.Name = "lblVoucherNo";
            this.lblVoucherNo.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 2, 5, 5);
            this.lblVoucherNo.Size = new System.Drawing.Size(138, 30);
            this.lblVoucherNo.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblVoucherNo.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblVoucherNo.TextSize = new System.Drawing.Size(50, 13);
            this.lblVoucherNo.TextToControlDistance = 5;
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.AllowHtmlStringInCaption = true;
            this.layoutControlItem13.Control = this.txtBillInvoiceNo;
            resources.ApplyResources(this.layoutControlItem13, "layoutControlItem13");
            this.layoutControlItem13.Location = new System.Drawing.Point(168, 0);
            this.layoutControlItem13.MaxSize = new System.Drawing.Size(174, 30);
            this.layoutControlItem13.MinSize = new System.Drawing.Size(174, 30);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 2, 5, 5);
            this.layoutControlItem13.Size = new System.Drawing.Size(174, 30);
            this.layoutControlItem13.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem13.TextSize = new System.Drawing.Size(96, 13);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(146, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(22, 30);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem4, "emptySpaceItem4");
            this.emptySpaceItem4.Location = new System.Drawing.Point(342, 0);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(22, 30);
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem8
            // 
            this.emptySpaceItem8.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem8, "emptySpaceItem8");
            this.emptySpaceItem8.Location = new System.Drawing.Point(778, 0);
            this.emptySpaceItem8.Name = "emptySpaceItem8";
            this.emptySpaceItem8.Size = new System.Drawing.Size(116, 30);
            this.emptySpaceItem8.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.ucAssetJournal1;
            resources.ApplyResources(this.layoutControlItem11, "layoutControlItem11");
            this.layoutControlItem11.Location = new System.Drawing.Point(0, 163);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(894, 109);
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextToControlDistance = 0;
            this.layoutControlItem11.TextVisible = false;
            // 
            // lciCurrency
            // 
            resources.ApplyResources(this.lciCurrency, "lciCurrency");
            this.lciCurrency.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem15,
            this.layoutControlItem16,
            this.lblDonorCurrency,
            this.lblExchangeRate,
            this.lblliveAvgRate,
            this.layoutControlItem18,
            this.lblCalcAmount,
            this.layoutControlItem19,
            this.lciActualAmt});
            this.lciCurrency.Location = new System.Drawing.Point(0, 30);
            this.lciCurrency.Name = "lciCurrency";
            this.lciCurrency.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciCurrency.Size = new System.Drawing.Size(894, 26);
            this.lciCurrency.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciCurrency.TextVisible = false;
            // 
            // layoutControlItem15
            // 
            this.layoutControlItem15.Control = this.glkpCurrencyCountry;
            resources.ApplyResources(this.layoutControlItem15, "layoutControlItem15");
            this.layoutControlItem15.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem15.MaxSize = new System.Drawing.Size(241, 24);
            this.layoutControlItem15.MinSize = new System.Drawing.Size(241, 24);
            this.layoutControlItem15.Name = "layoutControlItem15";
            this.layoutControlItem15.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 2, 2);
            this.layoutControlItem15.Size = new System.Drawing.Size(241, 24);
            this.layoutControlItem15.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem15.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem15.TextSize = new System.Drawing.Size(44, 13);
            this.layoutControlItem15.TextToControlDistance = 5;
            // 
            // layoutControlItem16
            // 
            this.layoutControlItem16.Control = this.txtCurrencyAmount;
            resources.ApplyResources(this.layoutControlItem16, "layoutControlItem16");
            this.layoutControlItem16.Location = new System.Drawing.Point(241, 0);
            this.layoutControlItem16.MinSize = new System.Drawing.Size(94, 24);
            this.layoutControlItem16.Name = "layoutControlItem16";
            this.layoutControlItem16.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 2, 2);
            this.layoutControlItem16.Size = new System.Drawing.Size(94, 24);
            this.layoutControlItem16.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem16.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem16.TextSize = new System.Drawing.Size(37, 13);
            this.layoutControlItem16.TextToControlDistance = 5;
            // 
            // lblDonorCurrency
            // 
            this.lblDonorCurrency.AllowHotTrack = false;
            resources.ApplyResources(this.lblDonorCurrency, "lblDonorCurrency");
            this.lblDonorCurrency.Location = new System.Drawing.Point(335, 0);
            this.lblDonorCurrency.MinSize = new System.Drawing.Size(18, 17);
            this.lblDonorCurrency.Name = "lblDonorCurrency";
            this.lblDonorCurrency.Size = new System.Drawing.Size(18, 24);
            this.lblDonorCurrency.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblDonorCurrency.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblDonorCurrency.TextSize = new System.Drawing.Size(14, 13);
            // 
            // lblExchangeRate
            // 
            this.lblExchangeRate.Control = this.txtExchangeRate;
            resources.ApplyResources(this.lblExchangeRate, "lblExchangeRate");
            this.lblExchangeRate.Location = new System.Drawing.Point(353, 0);
            this.lblExchangeRate.MaxSize = new System.Drawing.Size(155, 24);
            this.lblExchangeRate.MinSize = new System.Drawing.Size(155, 24);
            this.lblExchangeRate.Name = "lblExchangeRate";
            this.lblExchangeRate.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 2, 2);
            this.lblExchangeRate.Size = new System.Drawing.Size(155, 24);
            this.lblExchangeRate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblExchangeRate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblExchangeRate.TextSize = new System.Drawing.Size(73, 13);
            this.lblExchangeRate.TextToControlDistance = 5;
            // 
            // lblliveAvgRate
            // 
            this.lblliveAvgRate.AllowHotTrack = false;
            resources.ApplyResources(this.lblliveAvgRate, "lblliveAvgRate");
            this.lblliveAvgRate.Location = new System.Drawing.Point(508, 0);
            this.lblliveAvgRate.MaxSize = new System.Drawing.Size(102, 24);
            this.lblliveAvgRate.MinSize = new System.Drawing.Size(102, 24);
            this.lblliveAvgRate.Name = "lblliveAvgRate";
            this.lblliveAvgRate.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 0, 2, 2);
            this.lblliveAvgRate.Size = new System.Drawing.Size(102, 24);
            this.lblliveAvgRate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblliveAvgRate.TextSize = new System.Drawing.Size(96, 13);
            // 
            // layoutControlItem18
            // 
            this.layoutControlItem18.Control = this.lblAvgRate;
            resources.ApplyResources(this.layoutControlItem18, "layoutControlItem18");
            this.layoutControlItem18.Location = new System.Drawing.Point(610, 0);
            this.layoutControlItem18.MinSize = new System.Drawing.Size(26, 15);
            this.layoutControlItem18.Name = "layoutControlItem18";
            this.layoutControlItem18.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 0, 2);
            this.layoutControlItem18.Size = new System.Drawing.Size(26, 24);
            this.layoutControlItem18.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem18.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem18.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem18.TextToControlDistance = 0;
            this.layoutControlItem18.TextVisible = false;
            // 
            // lblCalcAmount
            // 
            this.lblCalcAmount.AllowHotTrack = false;
            resources.ApplyResources(this.lblCalcAmount, "lblCalcAmount");
            this.lblCalcAmount.Location = new System.Drawing.Point(636, 0);
            this.lblCalcAmount.MaxSize = new System.Drawing.Size(102, 24);
            this.lblCalcAmount.MinSize = new System.Drawing.Size(102, 24);
            this.lblCalcAmount.Name = "lblCalcAmount";
            this.lblCalcAmount.Padding = new DevExpress.XtraLayout.Utils.Padding(6, 0, 2, 2);
            this.lblCalcAmount.Size = new System.Drawing.Size(102, 24);
            this.lblCalcAmount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblCalcAmount.TextSize = new System.Drawing.Size(96, 13);
            // 
            // layoutControlItem19
            // 
            this.layoutControlItem19.Control = this.lblCalAmt;
            resources.ApplyResources(this.layoutControlItem19, "layoutControlItem19");
            this.layoutControlItem19.Location = new System.Drawing.Point(738, 0);
            this.layoutControlItem19.MinSize = new System.Drawing.Size(26, 15);
            this.layoutControlItem19.Name = "layoutControlItem19";
            this.layoutControlItem19.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 0, 2);
            this.layoutControlItem19.Size = new System.Drawing.Size(26, 24);
            this.layoutControlItem19.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem19.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem19.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem19.TextToControlDistance = 0;
            this.layoutControlItem19.TextVisible = false;
            // 
            // lciActualAmt
            // 
            this.lciActualAmt.Control = this.txtActualAmt;
            resources.ApplyResources(this.lciActualAmt, "lciActualAmt");
            this.lciActualAmt.Location = new System.Drawing.Point(764, 0);
            this.lciActualAmt.Name = "lciActualAmt";
            this.lciActualAmt.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 2, 2);
            this.lciActualAmt.Size = new System.Drawing.Size(128, 24);
            this.lciActualAmt.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lciActualAmt.TextSize = new System.Drawing.Size(70, 13);
            this.lciActualAmt.TextToControlDistance = 5;
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlItem12.AppearanceItemCaption.Font")));
            this.layoutControlItem12.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem12.Control = this.rgTransactionType;
            resources.ApplyResources(this.layoutControlItem12, "layoutControlItem12");
            this.layoutControlItem12.Location = new System.Drawing.Point(0, 40);
            this.layoutControlItem12.MaxSize = new System.Drawing.Size(0, 41);
            this.layoutControlItem12.MinSize = new System.Drawing.Size(148, 41);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(808, 41);
            this.layoutControlItem12.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem12.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.layoutControlItem12.TextSize = new System.Drawing.Size(89, 16);
            this.layoutControlItem12.TextToControlDistance = 5;
            // 
            // layoutControlGroup2
            // 
            resources.ApplyResources(this.layoutControlGroup2, "layoutControlGroup2");
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblDisplayType});
            this.layoutControlGroup2.Location = new System.Drawing.Point(808, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(206, 81);
            this.layoutControlGroup2.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.TextVisible = false;
            // 
            // lblDisplayType
            // 
            this.lblDisplayType.AllowHotTrack = false;
            this.lblDisplayType.AppearanceItemCaption.BackColor = ((System.Drawing.Color)(resources.GetObject("lblDisplayType.AppearanceItemCaption.BackColor")));
            this.lblDisplayType.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblDisplayType.AppearanceItemCaption.Font")));
            this.lblDisplayType.AppearanceItemCaption.Options.UseBackColor = true;
            this.lblDisplayType.AppearanceItemCaption.Options.UseFont = true;
            this.lblDisplayType.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lblDisplayType.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.lblDisplayType, "lblDisplayType");
            this.lblDisplayType.Location = new System.Drawing.Point(0, 0);
            this.lblDisplayType.MaxSize = new System.Drawing.Size(204, 79);
            this.lblDisplayType.MinSize = new System.Drawing.Size(204, 79);
            this.lblDisplayType.Name = "lblDisplayType";
            this.lblDisplayType.Size = new System.Drawing.Size(204, 79);
            this.lblDisplayType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblDisplayType.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblDisplayType.TextSize = new System.Drawing.Size(165, 42);
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.ucAssetVoucherShortcuts;
            resources.ApplyResources(this.layoutControlItem10, "layoutControlItem10");
            this.layoutControlItem10.Location = new System.Drawing.Point(910, 81);
            this.layoutControlItem10.MaxSize = new System.Drawing.Size(104, 0);
            this.layoutControlItem10.MinSize = new System.Drawing.Size(104, 24);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(104, 333);
            this.layoutControlItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextToControlDistance = 0;
            this.layoutControlItem10.TextVisible = false;
            // 
            // emptySpaceItem10
            // 
            this.emptySpaceItem10.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem10, "emptySpaceItem10");
            this.emptySpaceItem10.Location = new System.Drawing.Point(0, 223);
            this.emptySpaceItem10.Name = "emptySpaceItem10";
            this.emptySpaceItem10.Size = new System.Drawing.Size(988, 23);
            this.emptySpaceItem10.TextSize = new System.Drawing.Size(0, 0);
            // 
            // ColRate
            // 
            resources.ApplyResources(this.ColRate, "ColRate");
            this.ColRate.DisplayFormat.FormatString = "n";
            this.ColRate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ColRate.FieldName = "RATE";
            this.ColRate.Name = "ColRate";
            // 
            // lblNameAddress
            // 
            this.lblNameAddress.AllowHtmlStringInCaption = true;
            this.lblNameAddress.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblNameAddress.AppearanceItemCaption.Font")));
            this.lblNameAddress.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblNameAddress, "lblNameAddress");
            this.lblNameAddress.Location = new System.Drawing.Point(0, 0);
            this.lblNameAddress.Name = "lblNameAddress";
            this.lblNameAddress.Padding = new DevExpress.XtraLayout.Utils.Padding(8, 2, 5, 3);
            this.lblNameAddress.Size = new System.Drawing.Size(872, 28);
            this.lblNameAddress.TextSize = new System.Drawing.Size(93, 13);
            this.lblNameAddress.TextToControlDistance = 5;
            // 
            // lblNarration
            // 
            this.lblNarration.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblNarration.AppearanceItemCaption.Font")));
            this.lblNarration.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblNarration, "lblNarration");
            this.lblNarration.Location = new System.Drawing.Point(0, 28);
            this.lblNarration.Name = "lblNarration";
            this.lblNarration.Padding = new DevExpress.XtraLayout.Utils.Padding(8, 2, 2, 8);
            this.lblNarration.Size = new System.Drawing.Size(872, 31);
            this.lblNarration.TextSize = new System.Drawing.Size(93, 13);
            this.lblNarration.TextToControlDistance = 5;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlItem3.AppearanceItemCaption.Font")));
            this.layoutControlItem3.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 28);
            this.layoutControlItem3.Name = "lblNarration";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(8, 2, 2, 8);
            this.layoutControlItem3.Size = new System.Drawing.Size(872, 31);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(93, 13);
            this.layoutControlItem3.TextToControlDistance = 5;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.AllowHtmlStringInCaption = true;
            this.layoutControlItem4.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlItem4.AppearanceItemCaption.Font")));
            this.layoutControlItem4.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem4.Name = "lblNameAddress";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(8, 2, 5, 3);
            this.layoutControlItem4.Size = new System.Drawing.Size(872, 28);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(93, 13);
            this.layoutControlItem4.TextToControlDistance = 5;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem3, "emptySpaceItem3");
            this.emptySpaceItem3.Location = new System.Drawing.Point(-2358, 26);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(107, 101);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem5, "emptySpaceItem5");
            this.emptySpaceItem5.Location = new System.Drawing.Point(-2774, -1);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(107, 101);
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem6
            // 
            this.emptySpaceItem6.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem6, "emptySpaceItem6");
            this.emptySpaceItem6.Location = new System.Drawing.Point(848, 0);
            this.emptySpaceItem6.Name = "emptySpaceItem6";
            this.emptySpaceItem6.Size = new System.Drawing.Size(10, 655);
            this.emptySpaceItem6.TextSize = new System.Drawing.Size(0, 0);
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DrawDragBorder = false;
            this.bar3.OptionsBar.UseWholeRow = true;
            resources.ApplyResources(this.bar3, "bar3");
            // 
            // barManager2
            // 
            this.barManager2.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar4});
            this.barManager2.DockControls.Add(this.barDockControl1);
            this.barManager2.DockControls.Add(this.barDockControl2);
            this.barManager2.DockControls.Add(this.barDockControl3);
            this.barManager2.DockControls.Add(this.barDockControl4);
            this.barManager2.Form = this;
            this.barManager2.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiDeletePurchase,
            this.bbiAssetGenerationList,
            this.bbiSave,
            this.bbiNew,
            this.bbiClose,
            this.bbiCashBank});
            this.barManager2.MaxItemId = 6;
            this.barManager2.StatusBar = this.bar4;
            // 
            // bar4
            // 
            this.bar4.BarName = "Status bar";
            this.bar4.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar4.DockCol = 0;
            this.bar4.DockRow = 0;
            this.bar4.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar4.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiDeletePurchase),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiCashBank, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiAssetGenerationList, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiSave),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiNew),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiClose)});
            this.bar4.OptionsBar.AllowQuickCustomization = false;
            this.bar4.OptionsBar.DrawDragBorder = false;
            this.bar4.OptionsBar.UseWholeRow = true;
            resources.ApplyResources(this.bar4, "bar4");
            // 
            // bbiDeletePurchase
            // 
            this.bbiDeletePurchase.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiDeletePurchase, "bbiDeletePurchase");
            this.bbiDeletePurchase.Id = 0;
            this.bbiDeletePurchase.Name = "bbiDeletePurchase";
            this.bbiDeletePurchase.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiDeletePurchase_ItemClick);
            // 
            // bbiCashBank
            // 
            this.bbiCashBank.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiCashBank, "bbiCashBank");
            this.bbiCashBank.Id = 5;
            this.bbiCashBank.Name = "bbiCashBank";
            this.bbiCashBank.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiCashBank_ItemClick);
            // 
            // bbiAssetGenerationList
            // 
            this.bbiAssetGenerationList.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiAssetGenerationList, "bbiAssetGenerationList");
            this.bbiAssetGenerationList.Id = 1;
            this.bbiAssetGenerationList.Name = "bbiAssetGenerationList";
            this.bbiAssetGenerationList.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiAssetGenerationList_ItemClick);
            // 
            // bbiSave
            // 
            this.bbiSave.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiSave, "bbiSave");
            this.bbiSave.Id = 2;
            this.bbiSave.Name = "bbiSave";
            // 
            // bbiNew
            // 
            this.bbiNew.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiNew, "bbiNew");
            this.bbiNew.Id = 3;
            this.bbiNew.Name = "bbiNew";
            this.bbiNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiNew_ItemClick);
            // 
            // bbiClose
            // 
            this.bbiClose.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiClose, "bbiClose");
            this.bbiClose.Id = 4;
            this.bbiClose.Name = "bbiClose";
            this.bbiClose.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiClose_ItemClick);
            // 
            // barDockControl1
            // 
            this.barDockControl1.CausesValidation = false;
            resources.ApplyResources(this.barDockControl1, "barDockControl1");
            // 
            // barDockControl2
            // 
            this.barDockControl2.CausesValidation = false;
            resources.ApplyResources(this.barDockControl2, "barDockControl2");
            // 
            // barDockControl3
            // 
            this.barDockControl3.CausesValidation = false;
            resources.ApplyResources(this.barDockControl3, "barDockControl3");
            // 
            // barDockControl4
            // 
            this.barDockControl4.CausesValidation = false;
            resources.ApplyResources(this.barDockControl4, "barDockControl4");
            // 
            // frmAssetOutward
            // 
            this.Appearance.BackColor = ((System.Drawing.Color)(resources.GetObject("frmAssetOutward.Appearance.BackColor")));
            this.Appearance.Options.UseBackColor = true;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.panelControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Controls.Add(this.barDockControl3);
            this.Controls.Add(this.barDockControl4);
            this.Controls.Add(this.barDockControl2);
            this.Controls.Add(this.barDockControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Name = "frmAssetOutward";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmAssetOutward_FormClosing);
            this.Load += new System.EventHandler(this.frmSalesVoucherAdd_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmAssetOutward_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtActualAmt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExchangeRate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCurrencyAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpCurrencyCountry.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBillInvoiceNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgTransactionType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOtherCharges.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNarration.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSoldTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVoucherNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtSalesDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtSalesDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcInOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInOutAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpLocation)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpAssetName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtDiscount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbiDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtActualAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnCostCentre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rccmbAssetId)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgSales)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSalesDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblVoucherNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCurrency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem16)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDonorCurrency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblExchangeRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblliveAvgRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem18)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCalcAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem19)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciActualAmt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDisplayType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNameAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNarration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl panelControl1;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlGroup lcgSales;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraGrid.GridControl gcInOut;
        private DevExpress.XtraGrid.Views.Grid.GridView gvInOutAdd;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraGrid.Columns.GridColumn colSalesId;
        private DevExpress.XtraGrid.Columns.GridColumn colGroup;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit rglkpGroup;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colGroup_Id;
        private DevExpress.XtraGrid.Columns.GridColumn colGroupName;
        private DevExpress.XtraGrid.Columns.GridColumn colLocation;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit rglkpLocation;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colLocation_Id;
        private DevExpress.XtraGrid.Columns.GridColumn colLocationName;
        private DevExpress.XtraGrid.Columns.GridColumn colAsset;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit rglkpAssetName;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraGrid.Columns.GridColumn colItemId;
        private DevExpress.XtraGrid.Columns.GridColumn colAssetName;
        private DevExpress.XtraGrid.Columns.GridColumn ColDiscount;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem10;
        private DevExpress.XtraGrid.Columns.GridColumn colTemp;
        private DevExpress.XtraEditors.TextEdit txtVoucherNo;
        private DevExpress.XtraEditors.DateEdit dtSalesDate;
        private DevExpress.XtraLayout.LayoutControlItem lblSalesDate;
        private DevExpress.XtraLayout.LayoutControlItem lblVoucherNo;
        private UIControls.UcCaptionPanel ucCaptionTitle;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.TextEdit txtSoldTo;
        private DevExpress.XtraLayout.LayoutControlItem lblName;
        private DevExpress.XtraEditors.Repository.RepositoryItemCheckedComboBoxEdit rccmbAssetId;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn ColRate;
        private DevExpress.XtraLayout.LayoutControlItem lblNameAddress;
        private DevExpress.XtraLayout.LayoutControlItem lblNarration;
        private DevExpress.XtraEditors.TextEdit txtNarration;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtDiscount;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtAmount;
        private DevExpress.XtraEditors.TextEdit txtOtherCharges;
        private DevExpress.XtraGrid.Columns.GridColumn colAction;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbiDelete;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraEditors.SimpleButton btnNew;
        private DevExpress.XtraEditors.RadioGroup rgTransactionType;
        private UIControls.UCAssetVoucherShortcuts ucAssetVoucherShortcuts;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraLayout.SimpleLabelItem lblDisplayType;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraEditors.TextEdit txtBillInvoiceNo;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraGrid.Columns.GridColumn colInoutDetailId;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colAvailableQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colView;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbtView;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem8;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarDockControl barDockControl3;
        private DevExpress.XtraBars.BarDockControl barDockControl4;
        private DevExpress.XtraBars.BarDockControl barDockControl2;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarManager barManager2;
        private DevExpress.XtraBars.Bar bar4;
        private DevExpress.XtraBars.BarButtonItem bbiDeletePurchase;
        private DevExpress.XtraBars.BarButtonItem bbiAssetGenerationList;
        private DevExpress.XtraBars.BarButtonItem bbiSave;
        private DevExpress.XtraBars.BarButtonItem bbiNew;
        private DevExpress.XtraBars.BarButtonItem bbiClose;
        private DevExpress.XtraGrid.Columns.GridColumn colSource;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit rglkpSource;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private UIControls.UcAssetJournal ucAssetJournal1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountLedgerId;
        private DevExpress.XtraBars.BarButtonItem bbiCashBank;
        private DevExpress.XtraGrid.Columns.GridColumn colActualAmount;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtActualAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colDifference;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colCosCentre;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbtnCostCentre;
        private DevExpress.XtraEditors.GridLookUpEdit glkpCurrencyCountry;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem15;
        private DevExpress.XtraEditors.TextEdit txtCurrencyAmount;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem16;
        private DevExpress.XtraEditors.TextEdit txtExchangeRate;
        private DevExpress.XtraLayout.LayoutControlItem lblExchangeRate;
        private DevExpress.XtraEditors.LabelControl lblAvgRate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem18;
        private DevExpress.XtraEditors.LabelControl lblCalAmt;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem19;
        private DevExpress.XtraEditors.TextEdit txtActualAmt;
        private DevExpress.XtraLayout.LayoutControlItem lciActualAmt;
        private DevExpress.XtraLayout.SimpleLabelItem lblliveAvgRate;
        private DevExpress.XtraLayout.SimpleLabelItem lblCalcAmount;
        private DevExpress.XtraLayout.SimpleLabelItem lblDonorCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn gcCountryId;
        private DevExpress.XtraGrid.Columns.GridColumn gcCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn gcCurrencyName;
        private DevExpress.XtraGrid.Columns.GridColumn gcCurrencySymbol;
        private DevExpress.XtraLayout.LayoutControlGroup lciCurrency;
    }
}