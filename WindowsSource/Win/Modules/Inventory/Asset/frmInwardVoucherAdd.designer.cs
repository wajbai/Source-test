namespace ACPP.Modules.Asset
{
    partial class frmInwardVoucherAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInwardVoucherAdd));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject3 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject4 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtActualAmt = new DevExpress.XtraEditors.TextEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.bbiProject = new DevExpress.XtraBars.BarButtonItem();
            this.bbiDate = new DevExpress.XtraBars.BarButtonItem();
            this.txtExchangeRate = new DevExpress.XtraEditors.TextEdit();
            this.lblCalAmt = new DevExpress.XtraEditors.LabelControl();
            this.lblAvgRate = new DevExpress.XtraEditors.LabelControl();
            this.txtCurrencyAmount = new DevExpress.XtraEditors.TextEdit();
            this.glkpCurrencyCountry = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcCountryId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCurrencyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcCurrencySymbol = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ucAssetJournal1 = new ACPP.Modules.UIControls.UcAssetJournal();
            this.glkpPurpose = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView4 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colPurposeId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPurpose = new DevExpress.XtraGrid.Columns.GridColumn();
            this.glkpDonor = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView2 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDonoudId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDonorName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.glkVendor = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colVendorId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnNew = new DevExpress.XtraEditors.SimpleButton();
            this.ucAssetVoucherShortcut = new ACPP.Modules.UIControls.UCAssetVoucherShortcuts();
            this.lblVoucherType = new DevExpress.XtraEditors.LabelControl();
            this.rgVoucherType = new DevExpress.XtraEditors.RadioGroup();
            this.txtNarration = new DevExpress.XtraEditors.TextEdit();
            this.txtOtherCharges = new DevExpress.XtraEditors.TextEdit();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.gcPurchase = new DevExpress.XtraGrid.GridControl();
            this.gvPurchase = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSource = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rglkpSource = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSourceName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAssetName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rglkpAssetName = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.gridView3 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colAssItemID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAssName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccountLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtQuantity = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtAmount = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colViewDetails = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rbtnViewDetails = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colInoutDetailId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAvailableQty = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDeleteAssetItem = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rbtnPurchaseDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colcostcnetre = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rbtnCostcentre = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colSalvagevalue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtSalavageValue = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gcExchangeRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcLiveExchangeRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ucCaptionProject = new ACPP.Modules.UIControls.UcCaptionPanel();
            this.deInwardDate = new DevExpress.XtraEditors.DateEdit();
            this.txtVoucherNo = new DevExpress.XtraEditors.TextEdit();
            this.txtReceiptBillNo = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lctgPurchase = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblNarration = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblVoucherNo = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblBillno = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblVendor = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblDonor = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciBank = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciCurrencyGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lciCurrency = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciAmount = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblDonorCurrency = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblExchangeRate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblliveAvgRate = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblCalcAmount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciActualAmt = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblPurpose = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblTransactionType = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.barManager2 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar3 = new DevExpress.XtraBars.Bar();
            this.bbiDeletePurchase = new DevExpress.XtraBars.BarButtonItem();
            this.bbiDeleteCashBankRow = new DevExpress.XtraBars.BarButtonItem();
            this.bbiMovetoAsset = new DevExpress.XtraBars.BarButtonItem();
            this.bbiMoveToCashBank = new DevExpress.XtraBars.BarButtonItem();
            this.bbiAssetGenerationList = new DevExpress.XtraBars.BarButtonItem();
            this.bbiSave = new DevExpress.XtraBars.BarButtonItem();
            this.bbiNew = new DevExpress.XtraBars.BarButtonItem();
            this.bbiClose = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControl1 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl2 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl3 = new DevExpress.XtraBars.BarDockControl();
            this.barDockControl4 = new DevExpress.XtraBars.BarDockControl();
            this.bbiDelete = new DevExpress.XtraBars.BarButtonItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtActualAmt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExchangeRate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCurrencyAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpCurrencyCountry.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpPurpose.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpDonor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkVendor.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgVoucherType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNarration.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOtherCharges.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPurchase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPurchase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpAssetName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtQuantity)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnViewDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnPurchaseDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnCostcentre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtSalavageValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deInwardDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deInwardDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVoucherNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReceiptBillNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lctgPurchase)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNarration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblVoucherNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBillno)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblVendor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDonor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBank)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCurrencyGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCurrency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDonorCurrency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblExchangeRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblliveAvgRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCalcAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciActualAmt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPurpose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTransactionType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtActualAmt);
            this.layoutControl1.Controls.Add(this.txtExchangeRate);
            this.layoutControl1.Controls.Add(this.lblCalAmt);
            this.layoutControl1.Controls.Add(this.lblAvgRate);
            this.layoutControl1.Controls.Add(this.txtCurrencyAmount);
            this.layoutControl1.Controls.Add(this.glkpCurrencyCountry);
            this.layoutControl1.Controls.Add(this.ucAssetJournal1);
            this.layoutControl1.Controls.Add(this.glkpPurpose);
            this.layoutControl1.Controls.Add(this.glkpDonor);
            this.layoutControl1.Controls.Add(this.glkVendor);
            this.layoutControl1.Controls.Add(this.btnNew);
            this.layoutControl1.Controls.Add(this.ucAssetVoucherShortcut);
            this.layoutControl1.Controls.Add(this.lblVoucherType);
            this.layoutControl1.Controls.Add(this.rgVoucherType);
            this.layoutControl1.Controls.Add(this.txtNarration);
            this.layoutControl1.Controls.Add(this.txtOtherCharges);
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.gcPurchase);
            this.layoutControl1.Controls.Add(this.ucCaptionProject);
            this.layoutControl1.Controls.Add(this.deInwardDate);
            this.layoutControl1.Controls.Add(this.txtVoucherNo);
            this.layoutControl1.Controls.Add(this.txtReceiptBillNo);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(1119, 168, 250, 573);
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
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiProject,
            this.bbiDate});
            this.barManager1.MaxItemId = 7;
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
            // bbiProject
            // 
            this.bbiProject.Id = 5;
            this.bbiProject.Name = "bbiProject";
            // 
            // bbiDate
            // 
            this.bbiDate.Id = 6;
            this.bbiDate.Name = "bbiDate";
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
            this.glkpCurrencyCountry.Properties.View = this.gridView1;
            this.glkpCurrencyCountry.StyleController = this.layoutControl1;
            this.glkpCurrencyCountry.EditValueChanged += new System.EventHandler(this.glkpCurrencyCountry_EditValueChanged);
            // 
            // gridView1
            // 
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcCountryId,
            this.gcCurrency,
            this.gcCurrencyName,
            this.gcCurrencySymbol});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowColumnHeaders = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
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
            this.gcCurrencySymbol.FieldName = "CURRENCY_SYMBOL";
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
            // glkpPurpose
            // 
            resources.ApplyResources(this.glkpPurpose, "glkpPurpose");
            this.glkpPurpose.MenuManager = this.barManager1;
            this.glkpPurpose.Name = "glkpPurpose";
            this.glkpPurpose.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpPurpose.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpPurpose.Properties.Buttons"))))});
            this.glkpPurpose.Properties.NullText = resources.GetString("glkpPurpose.Properties.NullText");
            this.glkpPurpose.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.glkpPurpose.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.glkpPurpose.Properties.View = this.gridView4;
            this.glkpPurpose.StyleController = this.layoutControl1;
            this.glkpPurpose.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glkpPurpose_KeyDown);
            this.glkpPurpose.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.glkpPurpose_PreviewKeyDown);
            // 
            // gridView4
            // 
            this.gridView4.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridView4.Appearance.FocusedRow.Font")));
            this.gridView4.Appearance.FocusedRow.Options.UseFont = true;
            this.gridView4.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colPurposeId,
            this.colPurpose});
            this.gridView4.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView4.Name = "gridView4";
            this.gridView4.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView4.OptionsView.ShowColumnHeaders = false;
            this.gridView4.OptionsView.ShowGroupPanel = false;
            this.gridView4.OptionsView.ShowIndicator = false;
            // 
            // colPurposeId
            // 
            this.colPurposeId.FieldName = "CONTRIBUTION_ID";
            this.colPurposeId.Name = "colPurposeId";
            // 
            // colPurpose
            // 
            this.colPurpose.FieldName = "FC_PURPOSE";
            this.colPurpose.Name = "colPurpose";
            this.colPurpose.OptionsColumn.AllowEdit = false;
            this.colPurpose.OptionsColumn.ShowCaption = false;
            this.colPurpose.OptionsFilter.AllowAutoFilter = false;
            resources.ApplyResources(this.colPurpose, "colPurpose");
            // 
            // glkpDonor
            // 
            resources.ApplyResources(this.glkpDonor, "glkpDonor");
            this.glkpDonor.MenuManager = this.barManager1;
            this.glkpDonor.Name = "glkpDonor";
            this.glkpDonor.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpDonor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpDonor.Properties.Buttons")))),
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpDonor.Properties.Buttons1"))), resources.GetString("glkpDonor.Properties.Buttons2"), ((int)(resources.GetObject("glkpDonor.Properties.Buttons3"))), ((bool)(resources.GetObject("glkpDonor.Properties.Buttons4"))), ((bool)(resources.GetObject("glkpDonor.Properties.Buttons5"))), ((bool)(resources.GetObject("glkpDonor.Properties.Buttons6"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("glkpDonor.Properties.Buttons7"))), ((System.Drawing.Image)(resources.GetObject("glkpDonor.Properties.Buttons8"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, resources.GetString("glkpDonor.Properties.Buttons9"), ((object)(resources.GetObject("glkpDonor.Properties.Buttons10"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("glkpDonor.Properties.Buttons11"))), ((bool)(resources.GetObject("glkpDonor.Properties.Buttons12")))),
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpDonor.Properties.Buttons13"))), resources.GetString("glkpDonor.Properties.Buttons14"), ((int)(resources.GetObject("glkpDonor.Properties.Buttons15"))), ((bool)(resources.GetObject("glkpDonor.Properties.Buttons16"))), ((bool)(resources.GetObject("glkpDonor.Properties.Buttons17"))), ((bool)(resources.GetObject("glkpDonor.Properties.Buttons18"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("glkpDonor.Properties.Buttons19"))), ((System.Drawing.Image)(resources.GetObject("glkpDonor.Properties.Buttons20"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, resources.GetString("glkpDonor.Properties.Buttons21"), ((object)(resources.GetObject("glkpDonor.Properties.Buttons22"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("glkpDonor.Properties.Buttons23"))), ((bool)(resources.GetObject("glkpDonor.Properties.Buttons24"))))});
            this.glkpDonor.Properties.NullText = resources.GetString("glkpDonor.Properties.NullText");
            this.glkpDonor.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.glkpDonor.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.glkpDonor.Properties.View = this.gridView2;
            this.glkpDonor.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.glkpDonor_Properties_ButtonClick);
            this.glkpDonor.StyleController = this.layoutControl1;
            this.glkpDonor.EditValueChanged += new System.EventHandler(this.glkpDonor_EditValueChanged);
            this.glkpDonor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glkpDonor_KeyDown);
            this.glkpDonor.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.glkpDonor_PreviewKeyDown);
            // 
            // gridView2
            // 
            this.gridView2.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridView2.Appearance.FocusedRow.Font")));
            this.gridView2.Appearance.FocusedRow.Options.UseFont = true;
            this.gridView2.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDonoudId,
            this.colDonorName});
            this.gridView2.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView2.Name = "gridView2";
            this.gridView2.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView2.OptionsView.ShowColumnHeaders = false;
            this.gridView2.OptionsView.ShowGroupPanel = false;
            this.gridView2.OptionsView.ShowIndicator = false;
            this.gridView2.SortInfo.AddRange(new DevExpress.XtraGrid.Columns.GridColumnSortInfo[] {
            new DevExpress.XtraGrid.Columns.GridColumnSortInfo(this.colDonorName, DevExpress.Data.ColumnSortOrder.Ascending)});
            // 
            // colDonoudId
            // 
            this.colDonoudId.FieldName = "DONAUD_ID";
            this.colDonoudId.Name = "colDonoudId";
            // 
            // colDonorName
            // 
            resources.ApplyResources(this.colDonorName, "colDonorName");
            this.colDonorName.FieldName = "NAME";
            this.colDonorName.Name = "colDonorName";
            this.colDonorName.OptionsColumn.AllowEdit = false;
            this.colDonorName.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colDonorName.OptionsColumn.ShowCaption = false;
            this.colDonorName.OptionsFilter.AllowAutoFilter = false;
            this.colDonorName.OptionsFilter.AllowFilter = false;
            this.colDonorName.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            // 
            // glkVendor
            // 
            resources.ApplyResources(this.glkVendor, "glkVendor");
            this.glkVendor.EnterMoveNextControl = true;
            this.glkVendor.Name = "glkVendor";
            this.glkVendor.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkVendor.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkVendor.Properties.Buttons")))),
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkVendor.Properties.Buttons1"))), resources.GetString("glkVendor.Properties.Buttons2"), ((int)(resources.GetObject("glkVendor.Properties.Buttons3"))), ((bool)(resources.GetObject("glkVendor.Properties.Buttons4"))), ((bool)(resources.GetObject("glkVendor.Properties.Buttons5"))), ((bool)(resources.GetObject("glkVendor.Properties.Buttons6"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("glkVendor.Properties.Buttons7"))), ((System.Drawing.Image)(resources.GetObject("glkVendor.Properties.Buttons8"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject3, resources.GetString("glkVendor.Properties.Buttons9"), ((object)(resources.GetObject("glkVendor.Properties.Buttons10"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("glkVendor.Properties.Buttons11"))), ((bool)(resources.GetObject("glkVendor.Properties.Buttons12"))))});
            this.glkVendor.Properties.ImmediatePopup = true;
            this.glkVendor.Properties.NullText = resources.GetString("glkVendor.Properties.NullText");
            this.glkVendor.Properties.ShowFooter = false;
            this.glkVendor.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.glkVendor.Properties.View = this.gridLookUpEdit1View;
            this.glkVendor.Properties.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.glkVendor_Properties_ButtonClick);
            this.glkVendor.StyleController = this.layoutControl1;
            this.glkVendor.EditValueChanged += new System.EventHandler(this.glkVendor_EditValueChanged);
            this.glkVendor.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glkVendor_KeyDown);
            this.glkVendor.Leave += new System.EventHandler(this.glkVendor_Leave);
            this.glkVendor.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.glkVendor_PreviewKeyDown);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridLookUpEdit1View.Appearance.FocusedRow.Font")));
            this.gridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = true;
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colVendorId,
            this.colName});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsBehavior.Editable = false;
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // colVendorId
            // 
            resources.ApplyResources(this.colVendorId, "colVendorId");
            this.colVendorId.FieldName = "VENDOR_ID";
            this.colVendorId.Name = "colVendorId";
            // 
            // colName
            // 
            resources.ApplyResources(this.colName, "colName");
            this.colName.FieldName = "NAME";
            this.colName.Name = "colName";
            this.colName.OptionsColumn.AllowEdit = false;
            this.colName.OptionsFilter.AllowAutoFilter = false;
            // 
            // btnNew
            // 
            this.btnNew.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.btnNew, "btnNew");
            this.btnNew.Name = "btnNew";
            this.btnNew.StyleController = this.layoutControl1;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // ucAssetVoucherShortcut
            // 
            this.ucAssetVoucherShortcut.DisableAssetItem = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetVoucherShortcut.DisableAssetVoucherView = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetVoucherShortcut.DisableBankAccount = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetVoucherShortcut.DisableConfigure = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetVoucherShortcut.DisableCostCentre = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetVoucherShortcut.DisableCustodian = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetVoucherShortcut.DisableDate = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetVoucherShortcut.DisableDispose = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetVoucherShortcut.DisableDonation = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetVoucherShortcut.DisableDonor = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetVoucherShortcut.DisableInkind = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetVoucherShortcut.DisableLedger = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetVoucherShortcut.DisableLedgerOption = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetVoucherShortcut.DisableLocation = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetVoucherShortcut.DisableManufacturer = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetVoucherShortcut.DisableMapping = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetVoucherShortcut.DisableMappLocation = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetVoucherShortcut.DisableNextDate = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetVoucherShortcut.DisableProject = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAssetVoucherShortcut.DisablePUrchase = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetVoucherShortcut.DisableSales = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAssetVoucherShortcut.DisableVendor = DevExpress.XtraBars.BarItemVisibility.Always;
            resources.ApplyResources(this.ucAssetVoucherShortcut, "ucAssetVoucherShortcut");
            this.ucAssetVoucherShortcut.Name = "ucAssetVoucherShortcut";
            this.ucAssetVoucherShortcut.BankAccountClicked += new System.EventHandler(this.ucAssetVoucherShortcut_BankAccountClicked);
            this.ucAssetVoucherShortcut.LocationMappingClicked += new System.EventHandler(this.ucAssetVoucherShortcut_LocationMappingClicked);
            this.ucAssetVoucherShortcut.ConfigureClicked += new System.EventHandler(this.ucAssetVoucherShortcut_ConfigureClicked);
            this.ucAssetVoucherShortcut.DateClicked += new System.EventHandler(this.ucAssetVoucherShortcut_DateClicked);
            this.ucAssetVoucherShortcut.NextDateClicked += new System.EventHandler(this.ucAssetVoucherShortcut_NextDateClicked);
            this.ucAssetVoucherShortcut.ProjectClicked += new System.EventHandler(this.ucAssetVoucherShortcut_ProjectClicked);
            this.ucAssetVoucherShortcut.LocationClicked += new System.EventHandler(this.ucAssetVoucherShortcut_LocationClicked);
            this.ucAssetVoucherShortcut.CustodianClicked += new System.EventHandler(this.ucAssetVoucherShortcut_CustodianClicked);
            this.ucAssetVoucherShortcut.VendorClicked += new System.EventHandler(this.ucAssetVoucherShortcut_VendorClicked);
            this.ucAssetVoucherShortcut.AssetItemClicked += new System.EventHandler(this.ucAssetVoucherShortcut_AssetItemClicked);
            this.ucAssetVoucherShortcut.ManufacturerClicked += new System.EventHandler(this.ucAssetVoucherShortcut_ManufacturerClicked);
            this.ucAssetVoucherShortcut.DonorClicked += new System.EventHandler(this.ucAssetVoucherShortcut_DonorClicked);
            this.ucAssetVoucherShortcut.AccountMappingClicked += new System.EventHandler(this.ucAssetVoucherShortcut_AccountMappingClicked);
            this.ucAssetVoucherShortcut.PurchaseClicked += new System.EventHandler(this.ucAssetVoucherShortcut_PurchaseClicked);
            this.ucAssetVoucherShortcut.InkindClicked += new System.EventHandler(this.ucAssetVoucherShortcut_InkindClicked);
            this.ucAssetVoucherShortcut.CostCentreClicked += new System.EventHandler(this.ucAssetVoucherShortcut_CostCentreClicked);
            this.ucAssetVoucherShortcut.LedgerClicked += new System.EventHandler(this.ucAssetVoucherShortcut_LedgerClicked);
            this.ucAssetVoucherShortcut.LedgerOptionClicked += new System.EventHandler(this.ucAssetVoucherShortcut_LedgerOptionClicked);
            this.ucAssetVoucherShortcut.AssetVoucherViewClicked += new System.EventHandler(this.ucAssetVoucherShortcut_AssetVoucherView);
            // 
            // lblVoucherType
            // 
            this.lblVoucherType.Appearance.BackColor = ((System.Drawing.Color)(resources.GetObject("lblVoucherType.Appearance.BackColor")));
            this.lblVoucherType.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblVoucherType.Appearance.Font")));
            this.lblVoucherType.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            resources.ApplyResources(this.lblVoucherType, "lblVoucherType");
            this.lblVoucherType.Name = "lblVoucherType";
            this.lblVoucherType.StyleController = this.layoutControl1;
            // 
            // rgVoucherType
            // 
            resources.ApplyResources(this.rgVoucherType, "rgVoucherType");
            this.rgVoucherType.Name = "rgVoucherType";
            this.rgVoucherType.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("rgVoucherType.Properties.Appearance.Font")));
            this.rgVoucherType.Properties.Appearance.Options.UseFont = true;
            this.rgVoucherType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.rgVoucherType.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("rgVoucherType.Properties.Items"))), resources.GetString("rgVoucherType.Properties.Items1")),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("rgVoucherType.Properties.Items2"))), resources.GetString("rgVoucherType.Properties.Items3"))});
            this.rgVoucherType.StyleController = this.layoutControl1;
            this.rgVoucherType.TabStop = false;
            this.rgVoucherType.SelectedIndexChanged += new System.EventHandler(this.rgVoucherType_SelectedIndexChanged);
            // 
            // txtNarration
            // 
            resources.ApplyResources(this.txtNarration, "txtNarration");
            this.txtNarration.MenuManager = this.barManager1;
            this.txtNarration.Name = "txtNarration";
            this.txtNarration.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtNarration.StyleController = this.layoutControl1;
            this.txtNarration.Enter += new System.EventHandler(this.txtNarration_Enter);
            this.txtNarration.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtNarration_KeyDown);
            this.txtNarration.Leave += new System.EventHandler(this.txtNarration_Leave);
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
            // btnSave
            // 
            this.btnSave.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.btnSave.Cursor = System.Windows.Forms.Cursors.Default;
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // gcPurchase
            // 
            resources.ApplyResources(this.gcPurchase, "gcPurchase");
            this.gcPurchase.MainView = this.gvPurchase;
            this.gcPurchase.Name = "gcPurchase";
            this.gcPurchase.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rglkpAssetName,
            this.rbtnPurchaseDelete,
            this.rtxtQuantity,
            this.rtxtAmount,
            this.rbtnViewDetails,
            this.rbtnCostcentre,
            this.rglkpSource,
            this.rtxtSalavageValue});
            this.gcPurchase.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPurchase});
            this.gcPurchase.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.gcPurchase_ProcessGridKey);
            this.gcPurchase.Enter += new System.EventHandler(this.gcPurchasePrticulars_Enter);
            // 
            // gvPurchase
            // 
            this.gvPurchase.Appearance.FocusedCell.BackColor = ((System.Drawing.Color)(resources.GetObject("gvPurchase.Appearance.FocusedCell.BackColor")));
            this.gvPurchase.Appearance.FocusedCell.Font = ((System.Drawing.Font)(resources.GetObject("gvPurchase.Appearance.FocusedCell.Font")));
            this.gvPurchase.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvPurchase.Appearance.FocusedCell.Options.UseFont = true;
            this.gvPurchase.Appearance.FocusedRow.BackColor = ((System.Drawing.Color)(resources.GetObject("gvPurchase.Appearance.FocusedRow.BackColor")));
            this.gvPurchase.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvPurchase.Appearance.FocusedRow.Font")));
            this.gvPurchase.Appearance.FocusedRow.Options.UseBackColor = true;
            this.gvPurchase.Appearance.FocusedRow.Options.UseFont = true;
            this.gvPurchase.Appearance.FooterPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvPurchase.Appearance.FooterPanel.Font")));
            this.gvPurchase.Appearance.FooterPanel.ForeColor = ((System.Drawing.Color)(resources.GetObject("gvPurchase.Appearance.FooterPanel.ForeColor")));
            this.gvPurchase.Appearance.FooterPanel.Options.UseBackColor = true;
            this.gvPurchase.Appearance.FooterPanel.Options.UseFont = true;
            this.gvPurchase.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gvPurchase.Appearance.GroupFooter.BackColor = ((System.Drawing.Color)(resources.GetObject("gvPurchase.Appearance.GroupFooter.BackColor")));
            this.gvPurchase.Appearance.GroupFooter.Font = ((System.Drawing.Font)(resources.GetObject("gvPurchase.Appearance.GroupFooter.Font")));
            this.gvPurchase.Appearance.GroupFooter.Options.UseBackColor = true;
            this.gvPurchase.Appearance.GroupFooter.Options.UseFont = true;
            this.gvPurchase.Appearance.TopNewRow.Font = ((System.Drawing.Font)(resources.GetObject("gvPurchase.Appearance.TopNewRow.Font")));
            this.gvPurchase.Appearance.TopNewRow.Options.UseFont = true;
            this.gvPurchase.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gvPurchase.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSource,
            this.colAssetName,
            this.colQuantity,
            this.colAmount,
            this.colViewDetails,
            this.colInoutDetailId,
            this.colLedgerId,
            this.colAvailableQty,
            this.colDeleteAssetItem,
            this.colcostcnetre,
            this.colSalvagevalue,
            this.gcExchangeRate,
            this.gcLiveExchangeRate});
            this.gvPurchase.GridControl = this.gcPurchase;
            this.gvPurchase.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("gvPurchase.GroupSummary"))), resources.GetString("gvPurchase.GroupSummary1"), ((DevExpress.XtraGrid.Columns.GridColumn)(resources.GetObject("gvPurchase.GroupSummary2"))), resources.GetString("gvPurchase.GroupSummary3")),
            new DevExpress.XtraGrid.GridGroupSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("gvPurchase.GroupSummary4"))), resources.GetString("gvPurchase.GroupSummary5"), ((DevExpress.XtraGrid.Columns.GridColumn)(resources.GetObject("gvPurchase.GroupSummary6"))), resources.GetString("gvPurchase.GroupSummary7"))});
            this.gvPurchase.Name = "gvPurchase";
            this.gvPurchase.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gvPurchase.OptionsBehavior.EditorShowMode = DevExpress.Utils.EditorShowMode.MouseUp;
            this.gvPurchase.OptionsBehavior.ImmediateUpdateRowPosition = false;
            this.gvPurchase.OptionsBehavior.SummariesIgnoreNullValues = true;
            this.gvPurchase.OptionsCustomization.AllowColumnMoving = false;
            this.gvPurchase.OptionsCustomization.AllowFilter = false;
            this.gvPurchase.OptionsCustomization.AllowGroup = false;
            this.gvPurchase.OptionsCustomization.AllowSort = false;
            this.gvPurchase.OptionsFilter.AllowFilterEditor = false;
            this.gvPurchase.OptionsFind.AllowFindPanel = false;
            this.gvPurchase.OptionsNavigation.EnterMoveNextColumn = true;
            this.gvPurchase.OptionsView.BestFitMaxRowCount = 2;
            this.gvPurchase.OptionsView.ShowFooter = true;
            this.gvPurchase.OptionsView.ShowGroupPanel = false;
            this.gvPurchase.OptionsView.ShowIndicator = false;
            this.gvPurchase.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvPurchase_RowCellStyle);
            this.gvPurchase.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gvPurchase_ShowingEditor);
            this.gvPurchase.CellValueChanging += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvPurchase_CellValueChanging);
            // 
            // colSource
            // 
            this.colSource.AppearanceCell.Font = ((System.Drawing.Font)(resources.GetObject("colSource.AppearanceCell.Font")));
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
            this.colSource.OptionsFilter.ImmediateUpdateAutoFilter = false;
            resources.ApplyResources(this.colSource, "colSource");
            // 
            // rglkpSource
            // 
            resources.ApplyResources(this.rglkpSource, "rglkpSource");
            this.rglkpSource.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.rglkpSource.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rglkpSource.Buttons"))))});
            this.rglkpSource.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F2);
            this.rglkpSource.Name = "rglkpSource";
            this.rglkpSource.PopupFormMinSize = new System.Drawing.Size(10, 10);
            this.rglkpSource.PopupFormSize = new System.Drawing.Size(25, 45);
            this.rglkpSource.ReadOnly = true;
            this.rglkpSource.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.rglkpSource.View = this.repositoryItemGridLookUpEdit1View;
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colSourceName});
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            // 
            // colId
            // 
            resources.ApplyResources(this.colId, "colId");
            this.colId.FieldName = "Id";
            this.colId.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText;
            this.colId.Name = "colId";
            // 
            // colSourceName
            // 
            this.colSourceName.FieldName = "Name";
            this.colSourceName.Name = "colSourceName";
            resources.ApplyResources(this.colSourceName, "colSourceName");
            // 
            // colAssetName
            // 
            this.colAssetName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colAssetName.AppearanceHeader.Font")));
            this.colAssetName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colAssetName, "colAssetName");
            this.colAssetName.ColumnEdit = this.rglkpAssetName;
            this.colAssetName.FieldName = "ITEM_ID";
            this.colAssetName.Name = "colAssetName";
            this.colAssetName.OptionsColumn.AllowMove = false;
            this.colAssetName.OptionsColumn.AllowShowHide = false;
            this.colAssetName.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colAssetName.OptionsFilter.AllowAutoFilter = false;
            this.colAssetName.OptionsFilter.AllowFilter = false;
            this.colAssetName.OptionsFilter.FilterBySortField = DevExpress.Utils.DefaultBoolean.False;
            this.colAssetName.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colAssetName.SortMode = DevExpress.XtraGrid.ColumnSortMode.DisplayText;
            // 
            // rglkpAssetName
            // 
            this.rglkpAssetName.AppearanceFocused.Font = ((System.Drawing.Font)(resources.GetObject("rglkpAssetName.AppearanceFocused.Font")));
            this.rglkpAssetName.AppearanceFocused.Options.UseFont = true;
            resources.ApplyResources(this.rglkpAssetName, "rglkpAssetName");
            this.rglkpAssetName.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rglkpAssetName.Buttons"))))});
            this.rglkpAssetName.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F2);
            this.rglkpAssetName.ImmediatePopup = true;
            this.rglkpAssetName.Name = "rglkpAssetName";
            this.rglkpAssetName.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.rglkpAssetName.PopupFormMinSize = new System.Drawing.Size(270, 0);
            this.rglkpAssetName.PopupFormSize = new System.Drawing.Size(270, 0);
            this.rglkpAssetName.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.rglkpAssetName.View = this.gridView3;
            this.rglkpAssetName.EditValueChanged += new System.EventHandler(this.rglkpAssetName_EditValueChanged);
            this.rglkpAssetName.Enter += new System.EventHandler(this.rglkpAssetName_Enter);
            this.rglkpAssetName.Leave += new System.EventHandler(this.rglkpAssetName_Leave);
            this.rglkpAssetName.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rglkpAssetName_MouseDown);
            this.rglkpAssetName.Validating += new System.ComponentModel.CancelEventHandler(this.rglkpAssName_Validating);
            // 
            // gridView3
            // 
            this.gridView3.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridView3.Appearance.FocusedRow.Font")));
            this.gridView3.Appearance.FocusedRow.Options.UseFont = true;
            this.gridView3.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAssItemID,
            this.colAssName,
            this.colAccountLedgerId});
            this.gridView3.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView3.Name = "gridView3";
            this.gridView3.OptionsBehavior.AllowIncrementalSearch = true;
            this.gridView3.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView3.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gridView3.OptionsView.ShowColumnHeaders = false;
            this.gridView3.OptionsView.ShowFilterPanelMode = DevExpress.XtraGrid.Views.Base.ShowFilterPanelMode.ShowAlways;
            this.gridView3.OptionsView.ShowGroupPanel = false;
            this.gridView3.OptionsView.ShowIndicator = false;
            // 
            // colAssItemID
            // 
            resources.ApplyResources(this.colAssItemID, "colAssItemID");
            this.colAssItemID.FieldName = "ITEM_ID";
            this.colAssItemID.Name = "colAssItemID";
            // 
            // colAssName
            // 
            resources.ApplyResources(this.colAssName, "colAssName");
            this.colAssName.FieldName = "ASSET_ITEM";
            this.colAssName.Name = "colAssName";
            this.colAssName.OptionsColumn.ShowCaption = false;
            // 
            // colAccountLedgerId
            // 
            resources.ApplyResources(this.colAccountLedgerId, "colAccountLedgerId");
            this.colAccountLedgerId.FieldName = "ACCOUNT_LEDGER_ID";
            this.colAccountLedgerId.Name = "colAccountLedgerId";
            // 
            // colQuantity
            // 
            this.colQuantity.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colQuantity.AppearanceHeader.Font")));
            this.colQuantity.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colQuantity, "colQuantity");
            this.colQuantity.ColumnEdit = this.rtxtQuantity;
            this.colQuantity.FieldName = "QUANTITY";
            this.colQuantity.Name = "colQuantity";
            // 
            // rtxtQuantity
            // 
            resources.ApplyResources(this.rtxtQuantity, "rtxtQuantity");
            this.rtxtQuantity.Mask.EditMask = resources.GetString("rtxtQuantity.Mask.EditMask");
            this.rtxtQuantity.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("rtxtQuantity.Mask.MaskType")));
            this.rtxtQuantity.Mask.ShowPlaceHolders = ((bool)(resources.GetObject("rtxtQuantity.Mask.ShowPlaceHolders")));
            this.rtxtQuantity.MaxLength = 4;
            this.rtxtQuantity.Name = "rtxtQuantity";
            this.rtxtQuantity.EditValueChanged += new System.EventHandler(this.rtxtQuantity_EditValueChanged);
            this.rtxtQuantity.Enter += new System.EventHandler(this.rtxtQuantity_Enter);
            this.rtxtQuantity.Leave += new System.EventHandler(this.rtxtQuantity_Leave);
            // 
            // colAmount
            // 
            this.colAmount.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colAmount.AppearanceHeader.Font")));
            this.colAmount.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colAmount, "colAmount");
            this.colAmount.ColumnEdit = this.rtxtAmount;
            this.colAmount.FieldName = "AMOUNT";
            this.colAmount.Name = "colAmount";
            this.colAmount.OptionsColumn.AllowEdit = false;
            this.colAmount.OptionsColumn.AllowFocus = false;
            this.colAmount.OptionsColumn.AllowMove = false;
            this.colAmount.OptionsColumn.AllowShowHide = false;
            this.colAmount.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colAmount.OptionsFilter.AllowAutoFilter = false;
            this.colAmount.OptionsFilter.AllowFilter = false;
            this.colAmount.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colAmount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("colAmount.Summary"))), resources.GetString("colAmount.Summary1"), resources.GetString("colAmount.Summary2"))});
            // 
            // rtxtAmount
            // 
            resources.ApplyResources(this.rtxtAmount, "rtxtAmount");
            this.rtxtAmount.Mask.EditMask = resources.GetString("rtxtAmount.Mask.EditMask");
            this.rtxtAmount.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("rtxtAmount.Mask.MaskType")));
            this.rtxtAmount.MaxLength = 10;
            this.rtxtAmount.Name = "rtxtAmount";
            this.rtxtAmount.EditValueChanged += new System.EventHandler(this.rtxtAmount_EditValueChanged);
            this.rtxtAmount.Validating += new System.ComponentModel.CancelEventHandler(this.rtxtAmount_Validating);
            // 
            // colViewDetails
            // 
            this.colViewDetails.ColumnEdit = this.rbtnViewDetails;
            this.colViewDetails.Name = "colViewDetails";
            this.colViewDetails.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colViewDetails.OptionsColumn.FixedWidth = true;
            this.colViewDetails.OptionsColumn.TabStop = false;
            this.colViewDetails.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            resources.ApplyResources(this.colViewDetails, "colViewDetails");
            // 
            // rbtnViewDetails
            // 
            resources.ApplyResources(this.rbtnViewDetails, "rbtnViewDetails");
            this.rbtnViewDetails.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rbtnViewDetails.Buttons"))), resources.GetString("rbtnViewDetails.Buttons1"), ((int)(resources.GetObject("rbtnViewDetails.Buttons2"))), ((bool)(resources.GetObject("rbtnViewDetails.Buttons3"))), ((bool)(resources.GetObject("rbtnViewDetails.Buttons4"))), ((bool)(resources.GetObject("rbtnViewDetails.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("rbtnViewDetails.Buttons6"))), global::ACPP.Properties.Resources.Used_Codes, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject4, resources.GetString("rbtnViewDetails.Buttons7"), ((object)(resources.GetObject("rbtnViewDetails.Buttons8"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("rbtnViewDetails.Buttons9"))), ((bool)(resources.GetObject("rbtnViewDetails.Buttons10"))))});
            this.rbtnViewDetails.Name = "rbtnViewDetails";
            this.rbtnViewDetails.Click += new System.EventHandler(this.rbtnViewDetails_Click);
            // 
            // colInoutDetailId
            // 
            resources.ApplyResources(this.colInoutDetailId, "colInoutDetailId");
            this.colInoutDetailId.FieldName = "IN_OUT_DETAIL_ID";
            this.colInoutDetailId.Name = "colInoutDetailId";
            // 
            // colLedgerId
            // 
            resources.ApplyResources(this.colLedgerId, "colLedgerId");
            this.colLedgerId.FieldName = "LEDGER_ID";
            this.colLedgerId.Name = "colLedgerId";
            // 
            // colAvailableQty
            // 
            this.colAvailableQty.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colAvailableQty.AppearanceHeader.Font")));
            this.colAvailableQty.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colAvailableQty, "colAvailableQty");
            this.colAvailableQty.FieldName = "AVAILABLE_QUANTITY";
            this.colAvailableQty.Name = "colAvailableQty";
            this.colAvailableQty.OptionsColumn.AllowFocus = false;
            // 
            // colDeleteAssetItem
            // 
            resources.ApplyResources(this.colDeleteAssetItem, "colDeleteAssetItem");
            this.colDeleteAssetItem.ColumnEdit = this.rbtnPurchaseDelete;
            this.colDeleteAssetItem.Name = "colDeleteAssetItem";
            this.colDeleteAssetItem.OptionsColumn.AllowMove = false;
            this.colDeleteAssetItem.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colDeleteAssetItem.OptionsColumn.FixedWidth = true;
            this.colDeleteAssetItem.OptionsColumn.ShowCaption = false;
            this.colDeleteAssetItem.OptionsColumn.TabStop = false;
            this.colDeleteAssetItem.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            // 
            // rbtnPurchaseDelete
            // 
            resources.ApplyResources(this.rbtnPurchaseDelete, "rbtnPurchaseDelete");
            this.rbtnPurchaseDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rbtnPurchaseDelete.Buttons"))), resources.GetString("rbtnPurchaseDelete.Buttons1"), ((int)(resources.GetObject("rbtnPurchaseDelete.Buttons2"))), ((bool)(resources.GetObject("rbtnPurchaseDelete.Buttons3"))), ((bool)(resources.GetObject("rbtnPurchaseDelete.Buttons4"))), ((bool)(resources.GetObject("rbtnPurchaseDelete.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("rbtnPurchaseDelete.Buttons6"))), global::ACPP.Properties.Resources.Delete_Mob, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject5, resources.GetString("rbtnPurchaseDelete.Buttons7"), ((object)(resources.GetObject("rbtnPurchaseDelete.Buttons8"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("rbtnPurchaseDelete.Buttons9"))), ((bool)(resources.GetObject("rbtnPurchaseDelete.Buttons10"))))});
            this.rbtnPurchaseDelete.Name = "rbtnPurchaseDelete";
            this.rbtnPurchaseDelete.Click += new System.EventHandler(this.rbtnPurchaseDelete_Click);
            // 
            // colcostcnetre
            // 
            resources.ApplyResources(this.colcostcnetre, "colcostcnetre");
            this.colcostcnetre.ColumnEdit = this.rbtnCostcentre;
            this.colcostcnetre.Name = "colcostcnetre";
            this.colcostcnetre.OptionsColumn.AllowMove = false;
            this.colcostcnetre.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colcostcnetre.OptionsColumn.FixedWidth = true;
            this.colcostcnetre.OptionsColumn.ShowCaption = false;
            this.colcostcnetre.OptionsColumn.TabStop = false;
            this.colcostcnetre.OptionsFilter.AllowAutoFilter = false;
            this.colcostcnetre.OptionsFilter.AllowFilter = false;
            this.colcostcnetre.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colcostcnetre.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            // 
            // rbtnCostcentre
            // 
            resources.ApplyResources(this.rbtnCostcentre, "rbtnCostcentre");
            this.rbtnCostcentre.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rbtnCostcentre.Buttons"))), resources.GetString("rbtnCostcentre.Buttons1"), ((int)(resources.GetObject("rbtnCostcentre.Buttons2"))), ((bool)(resources.GetObject("rbtnCostcentre.Buttons3"))), ((bool)(resources.GetObject("rbtnCostcentre.Buttons4"))), ((bool)(resources.GetObject("rbtnCostcentre.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("rbtnCostcentre.Buttons6"))), global::ACPP.Properties.Resources.Donor_mapping, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject6, resources.GetString("rbtnCostcentre.Buttons7"), ((object)(resources.GetObject("rbtnCostcentre.Buttons8"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("rbtnCostcentre.Buttons9"))), ((bool)(resources.GetObject("rbtnCostcentre.Buttons10"))))});
            this.rbtnCostcentre.Name = "rbtnCostcentre";
            this.rbtnCostcentre.Click += new System.EventHandler(this.rbtnCostcentre_Click);
            // 
            // colSalvagevalue
            // 
            this.colSalvagevalue.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colSalvagevalue.AppearanceHeader.Font")));
            this.colSalvagevalue.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colSalvagevalue, "colSalvagevalue");
            this.colSalvagevalue.ColumnEdit = this.rtxtSalavageValue;
            this.colSalvagevalue.FieldName = "SALVAGE_VALUE";
            this.colSalvagevalue.Name = "colSalvagevalue";
            // 
            // rtxtSalavageValue
            // 
            resources.ApplyResources(this.rtxtSalavageValue, "rtxtSalavageValue");
            this.rtxtSalavageValue.Mask.EditMask = resources.GetString("rtxtSalavageValue.Mask.EditMask");
            this.rtxtSalavageValue.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("rtxtSalavageValue.Mask.MaskType")));
            this.rtxtSalavageValue.MaxLength = 10;
            this.rtxtSalavageValue.Name = "rtxtSalavageValue";
            // 
            // gcExchangeRate
            // 
            resources.ApplyResources(this.gcExchangeRate, "gcExchangeRate");
            this.gcExchangeRate.FieldName = "EXCHANGE_RATE";
            this.gcExchangeRate.Name = "gcExchangeRate";
            // 
            // gcLiveExchangeRate
            // 
            resources.ApplyResources(this.gcLiveExchangeRate, "gcLiveExchangeRate");
            this.gcLiveExchangeRate.FieldName = "LIVE_EXCHANGE_RATE";
            this.gcLiveExchangeRate.Name = "gcLiveExchangeRate";
            // 
            // ucCaptionProject
            // 
            this.ucCaptionProject.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            resources.ApplyResources(this.ucCaptionProject, "ucCaptionProject");
            this.ucCaptionProject.Name = "ucCaptionProject";
            // 
            // deInwardDate
            // 
            resources.ApplyResources(this.deInwardDate, "deInwardDate");
            this.deInwardDate.EnterMoveNextControl = true;
            this.deInwardDate.MenuManager = this.barManager1;
            this.deInwardDate.Name = "deInwardDate";
            this.deInwardDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deInwardDate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.deInwardDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("deInwardDate.Properties.Buttons"))))});
            this.deInwardDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("deInwardDate.Properties.CalendarTimeProperties.Buttons"))))});
            this.deInwardDate.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("deInwardDate.Properties.Mask.MaskType")));
            this.deInwardDate.StyleController = this.layoutControl1;
            this.deInwardDate.TabStop = false;
            this.deInwardDate.EditValueChanged += new System.EventHandler(this.dePurchaseDate_EditValueChanged);
            this.deInwardDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.deInwardDate_KeyDown);
            this.deInwardDate.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.deInwardDate_PreviewKeyDown);
            // 
            // txtVoucherNo
            // 
            resources.ApplyResources(this.txtVoucherNo, "txtVoucherNo");
            this.txtVoucherNo.Name = "txtVoucherNo";
            this.txtVoucherNo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtVoucherNo.StyleController = this.layoutControl1;
            this.txtVoucherNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtVoucherNo_KeyDown);
            this.txtVoucherNo.Leave += new System.EventHandler(this.txtVoucherNo_Leave);
            this.txtVoucherNo.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtVoucherNo_PreviewKeyDown);
            // 
            // txtReceiptBillNo
            // 
            this.txtReceiptBillNo.EnterMoveNextControl = true;
            resources.ApplyResources(this.txtReceiptBillNo, "txtReceiptBillNo");
            this.txtReceiptBillNo.Name = "txtReceiptBillNo";
            this.txtReceiptBillNo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtReceiptBillNo.Properties.MaxLength = 20;
            this.txtReceiptBillNo.StyleController = this.layoutControl1;
            this.txtReceiptBillNo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtInvoiceBillNo_KeyDown);
            this.txtReceiptBillNo.Leave += new System.EventHandler(this.txtBillNo_Leave);
            this.txtReceiptBillNo.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.txtInvoiceBillNo_PreviewKeyDown);
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.lctgPurchase,
            this.layoutControlGroup3,
            this.lblTransactionType,
            this.layoutControlItem7});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.layoutControlGroup1.Size = new System.Drawing.Size(988, 725);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ucCaptionProject;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 39);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(104, 39);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(726, 39);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // lctgPurchase
            // 
            this.lctgPurchase.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lctgPurchase.AppearanceGroup.Font")));
            this.lctgPurchase.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.lctgPurchase, "lctgPurchase");
            this.lctgPurchase.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3,
            this.layoutControlGroup2,
            this.lblDate,
            this.lblVoucherNo,
            this.lblBillno,
            this.lblVendor,
            this.lblDonor,
            this.lciBank,
            this.lciCurrencyGroup,
            this.lblPurpose});
            this.lctgPurchase.Location = new System.Drawing.Point(0, 83);
            this.lctgPurchase.Name = "lctgPurchase";
            this.lctgPurchase.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.lctgPurchase.Size = new System.Drawing.Size(878, 636);
            this.lctgPurchase.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gcPurchase;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 76);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(100, 23);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 0);
            this.layoutControlItem3.Size = new System.Drawing.Size(866, 272);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            resources.ApplyResources(this.layoutControlGroup2, "layoutControlGroup2");
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem10,
            this.layoutControlItem6,
            this.lblNarration,
            this.layoutControlItem8,
            this.layoutControlItem9});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 571);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutControlGroup2.Size = new System.Drawing.Size(866, 34);
            this.layoutControlGroup2.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.txtOtherCharges;
            resources.ApplyResources(this.layoutControlItem10, "layoutControlItem10");
            this.layoutControlItem10.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem10.MinSize = new System.Drawing.Size(130, 24);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 4, 4);
            this.layoutControlItem10.Size = new System.Drawing.Size(136, 28);
            this.layoutControlItem10.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem10.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem10.TextSize = new System.Drawing.Size(71, 13);
            this.layoutControlItem10.TextToControlDistance = 5;
            this.layoutControlItem10.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnSave;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(667, 0);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(63, 28);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(63, 28);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(63, 28);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // lblNarration
            // 
            this.lblNarration.Control = this.txtNarration;
            resources.ApplyResources(this.lblNarration, "lblNarration");
            this.lblNarration.Location = new System.Drawing.Point(136, 0);
            this.lblNarration.MinSize = new System.Drawing.Size(109, 28);
            this.lblNarration.Name = "lblNarration";
            this.lblNarration.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 4, 4);
            this.lblNarration.Size = new System.Drawing.Size(531, 28);
            this.lblNarration.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblNarration.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lblNarration.TextSize = new System.Drawing.Size(50, 13);
            this.lblNarration.TextToControlDistance = 5;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.btnClose;
            resources.ApplyResources(this.layoutControlItem8, "layoutControlItem8");
            this.layoutControlItem8.Location = new System.Drawing.Point(796, 0);
            this.layoutControlItem8.MaxSize = new System.Drawing.Size(64, 28);
            this.layoutControlItem8.MinSize = new System.Drawing.Size(64, 28);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(64, 28);
            this.layoutControlItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.btnNew;
            resources.ApplyResources(this.layoutControlItem9, "layoutControlItem9");
            this.layoutControlItem9.Location = new System.Drawing.Point(730, 0);
            this.layoutControlItem9.MaxSize = new System.Drawing.Size(66, 28);
            this.layoutControlItem9.MinSize = new System.Drawing.Size(66, 28);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(66, 28);
            this.layoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // lblDate
            // 
            this.lblDate.AllowHtmlStringInCaption = true;
            this.lblDate.Control = this.deInwardDate;
            resources.ApplyResources(this.lblDate, "lblDate");
            this.lblDate.Location = new System.Drawing.Point(0, 0);
            this.lblDate.MaxSize = new System.Drawing.Size(143, 26);
            this.lblDate.MinSize = new System.Drawing.Size(143, 26);
            this.lblDate.Name = "lblDate";
            this.lblDate.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 3, 3);
            this.lblDate.Size = new System.Drawing.Size(143, 26);
            this.lblDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblDate.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 2, 0, 0);
            this.lblDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lblDate.TextSize = new System.Drawing.Size(43, 13);
            this.lblDate.TextToControlDistance = 5;
            // 
            // lblVoucherNo
            // 
            this.lblVoucherNo.AllowHtmlStringInCaption = true;
            this.lblVoucherNo.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lblVoucherNo.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblVoucherNo.Control = this.txtVoucherNo;
            resources.ApplyResources(this.lblVoucherNo, "lblVoucherNo");
            this.lblVoucherNo.Location = new System.Drawing.Point(143, 0);
            this.lblVoucherNo.MaxSize = new System.Drawing.Size(169, 26);
            this.lblVoucherNo.MinSize = new System.Drawing.Size(169, 26);
            this.lblVoucherNo.Name = "lblVoucherNo";
            this.lblVoucherNo.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 3);
            this.lblVoucherNo.Size = new System.Drawing.Size(169, 26);
            this.lblVoucherNo.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblVoucherNo.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 2, 0, 0);
            this.lblVoucherNo.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lblVoucherNo.TextSize = new System.Drawing.Size(66, 13);
            this.lblVoucherNo.TextToControlDistance = 5;
            // 
            // lblBillno
            // 
            this.lblBillno.AllowHtmlStringInCaption = true;
            this.lblBillno.Control = this.txtReceiptBillNo;
            resources.ApplyResources(this.lblBillno, "lblBillno");
            this.lblBillno.Location = new System.Drawing.Point(312, 0);
            this.lblBillno.MaxSize = new System.Drawing.Size(0, 26);
            this.lblBillno.MinSize = new System.Drawing.Size(146, 26);
            this.lblBillno.Name = "lblBillno";
            this.lblBillno.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 2, 3, 3);
            this.lblBillno.Size = new System.Drawing.Size(219, 26);
            this.lblBillno.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblBillno.Spacing = new DevExpress.XtraLayout.Utils.Padding(6, 0, 0, 0);
            this.lblBillno.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lblBillno.TextSize = new System.Drawing.Size(83, 13);
            this.lblBillno.TextToControlDistance = 5;
            // 
            // lblVendor
            // 
            this.lblVendor.AllowHtmlStringInCaption = true;
            this.lblVendor.Control = this.glkVendor;
            resources.ApplyResources(this.lblVendor, "lblVendor");
            this.lblVendor.Location = new System.Drawing.Point(531, 0);
            this.lblVendor.MinSize = new System.Drawing.Size(104, 26);
            this.lblVendor.Name = "lblVendor";
            this.lblVendor.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 2, 3, 3);
            this.lblVendor.Size = new System.Drawing.Size(335, 26);
            this.lblVendor.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblVendor.Spacing = new DevExpress.XtraLayout.Utils.Padding(3, 1, 0, 0);
            this.lblVendor.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblVendor.TextSize = new System.Drawing.Size(43, 13);
            this.lblVendor.TextToControlDistance = 5;
            // 
            // lblDonor
            // 
            this.lblDonor.Control = this.glkpDonor;
            resources.ApplyResources(this.lblDonor, "lblDonor");
            this.lblDonor.Location = new System.Drawing.Point(0, 52);
            this.lblDonor.Name = "lblDonor";
            this.lblDonor.Size = new System.Drawing.Size(433, 24);
            this.lblDonor.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lblDonor.TextSize = new System.Drawing.Size(42, 13);
            this.lblDonor.TextToControlDistance = 5;
            // 
            // lciBank
            // 
            this.lciBank.Control = this.ucAssetJournal1;
            resources.ApplyResources(this.lciBank, "lciBank");
            this.lciBank.Location = new System.Drawing.Point(0, 348);
            this.lciBank.Name = "lciBank";
            this.lciBank.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciBank.Size = new System.Drawing.Size(866, 223);
            this.lciBank.TextSize = new System.Drawing.Size(0, 0);
            this.lciBank.TextToControlDistance = 0;
            this.lciBank.TextVisible = false;
            // 
            // lciCurrencyGroup
            // 
            resources.ApplyResources(this.lciCurrencyGroup, "lciCurrencyGroup");
            this.lciCurrencyGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lciCurrency,
            this.lciAmount,
            this.lblDonorCurrency,
            this.lblExchangeRate,
            this.lblliveAvgRate,
            this.layoutControlItem4,
            this.lblCalcAmount,
            this.layoutControlItem11,
            this.lciActualAmt});
            this.lciCurrencyGroup.Location = new System.Drawing.Point(0, 26);
            this.lciCurrencyGroup.Name = "lciCurrencyGroup";
            this.lciCurrencyGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciCurrencyGroup.Size = new System.Drawing.Size(866, 26);
            this.lciCurrencyGroup.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lciCurrencyGroup.TextVisible = false;
            // 
            // lciCurrency
            // 
            this.lciCurrency.Control = this.glkpCurrencyCountry;
            resources.ApplyResources(this.lciCurrency, "lciCurrency");
            this.lciCurrency.Location = new System.Drawing.Point(0, 0);
            this.lciCurrency.MaxSize = new System.Drawing.Size(158, 24);
            this.lciCurrency.MinSize = new System.Drawing.Size(158, 24);
            this.lciCurrency.Name = "lciCurrency";
            this.lciCurrency.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 2, 2);
            this.lciCurrency.Size = new System.Drawing.Size(158, 24);
            this.lciCurrency.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciCurrency.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lciCurrency.TextSize = new System.Drawing.Size(44, 13);
            this.lciCurrency.TextToControlDistance = 5;
            // 
            // lciAmount
            // 
            this.lciAmount.Control = this.txtCurrencyAmount;
            resources.ApplyResources(this.lciAmount, "lciAmount");
            this.lciAmount.Location = new System.Drawing.Point(158, 0);
            this.lciAmount.MaxSize = new System.Drawing.Size(141, 24);
            this.lciAmount.MinSize = new System.Drawing.Size(141, 24);
            this.lciAmount.Name = "lciAmount";
            this.lciAmount.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 2, 2);
            this.lciAmount.Size = new System.Drawing.Size(141, 24);
            this.lciAmount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciAmount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lciAmount.TextSize = new System.Drawing.Size(37, 13);
            this.lciAmount.TextToControlDistance = 5;
            // 
            // lblDonorCurrency
            // 
            this.lblDonorCurrency.AllowHotTrack = false;
            resources.ApplyResources(this.lblDonorCurrency, "lblDonorCurrency");
            this.lblDonorCurrency.Location = new System.Drawing.Point(299, 0);
            this.lblDonorCurrency.MinSize = new System.Drawing.Size(18, 15);
            this.lblDonorCurrency.Name = "lblDonorCurrency";
            this.lblDonorCurrency.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 0, 2, 0);
            this.lblDonorCurrency.Size = new System.Drawing.Size(18, 24);
            this.lblDonorCurrency.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblDonorCurrency.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblDonorCurrency.TextSize = new System.Drawing.Size(14, 13);
            // 
            // lblExchangeRate
            // 
            this.lblExchangeRate.Control = this.txtExchangeRate;
            resources.ApplyResources(this.lblExchangeRate, "lblExchangeRate");
            this.lblExchangeRate.Location = new System.Drawing.Point(317, 0);
            this.lblExchangeRate.MaxSize = new System.Drawing.Size(133, 24);
            this.lblExchangeRate.MinSize = new System.Drawing.Size(133, 24);
            this.lblExchangeRate.Name = "lblExchangeRate";
            this.lblExchangeRate.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 2, 2);
            this.lblExchangeRate.Size = new System.Drawing.Size(133, 24);
            this.lblExchangeRate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblExchangeRate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblExchangeRate.TextSize = new System.Drawing.Size(73, 13);
            this.lblExchangeRate.TextToControlDistance = 5;
            // 
            // lblliveAvgRate
            // 
            this.lblliveAvgRate.AllowHotTrack = false;
            resources.ApplyResources(this.lblliveAvgRate, "lblliveAvgRate");
            this.lblliveAvgRate.Location = new System.Drawing.Point(450, 0);
            this.lblliveAvgRate.Name = "lblliveAvgRate";
            this.lblliveAvgRate.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 2, 2);
            this.lblliveAvgRate.Size = new System.Drawing.Size(106, 24);
            this.lblliveAvgRate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblliveAvgRate.TextSize = new System.Drawing.Size(96, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.lblAvgRate;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(556, 0);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(26, 17);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 2, 2);
            this.layoutControlItem4.Size = new System.Drawing.Size(26, 24);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // lblCalcAmount
            // 
            this.lblCalcAmount.AllowHotTrack = false;
            resources.ApplyResources(this.lblCalcAmount, "lblCalcAmount");
            this.lblCalcAmount.Location = new System.Drawing.Point(582, 0);
            this.lblCalcAmount.Name = "lblCalcAmount";
            this.lblCalcAmount.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 2, 2);
            this.lblCalcAmount.Size = new System.Drawing.Size(99, 24);
            this.lblCalcAmount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblCalcAmount.TextSize = new System.Drawing.Size(90, 13);
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.lblCalAmt;
            resources.ApplyResources(this.layoutControlItem11, "layoutControlItem11");
            this.layoutControlItem11.Location = new System.Drawing.Point(681, 0);
            this.layoutControlItem11.MinSize = new System.Drawing.Size(26, 17);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 2, 2);
            this.layoutControlItem11.Size = new System.Drawing.Size(26, 24);
            this.layoutControlItem11.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem11.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextToControlDistance = 0;
            this.layoutControlItem11.TextVisible = false;
            // 
            // lciActualAmt
            // 
            this.lciActualAmt.Control = this.txtActualAmt;
            resources.ApplyResources(this.lciActualAmt, "lciActualAmt");
            this.lciActualAmt.Location = new System.Drawing.Point(707, 0);
            this.lciActualAmt.MaxSize = new System.Drawing.Size(157, 24);
            this.lciActualAmt.MinSize = new System.Drawing.Size(157, 24);
            this.lciActualAmt.Name = "lciActualAmt";
            this.lciActualAmt.Size = new System.Drawing.Size(157, 24);
            this.lciActualAmt.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lciActualAmt.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lciActualAmt.TextSize = new System.Drawing.Size(52, 13);
            this.lciActualAmt.TextToControlDistance = 5;
            // 
            // lblPurpose
            // 
            this.lblPurpose.Control = this.glkpPurpose;
            resources.ApplyResources(this.lblPurpose, "lblPurpose");
            this.lblPurpose.Location = new System.Drawing.Point(433, 52);
            this.lblPurpose.MaxSize = new System.Drawing.Size(0, 24);
            this.lblPurpose.MinSize = new System.Drawing.Size(98, 24);
            this.lblPurpose.Name = "lblPurpose";
            this.lblPurpose.Size = new System.Drawing.Size(433, 24);
            this.lblPurpose.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblPurpose.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lblPurpose.TextSize = new System.Drawing.Size(39, 13);
            this.lblPurpose.TextToControlDistance = 5;
            // 
            // layoutControlGroup3
            // 
            resources.ApplyResources(this.layoutControlGroup3, "layoutControlGroup3");
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5});
            this.layoutControlGroup3.Location = new System.Drawing.Point(726, 0);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup3.Size = new System.Drawing.Size(256, 83);
            this.layoutControlGroup3.Spacing = new DevExpress.XtraLayout.Utils.Padding(10, 0, 0, 0);
            this.layoutControlGroup3.TextVisible = false;
            this.layoutControlGroup3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.lblVoucherType;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(244, 81);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // lblTransactionType
            // 
            this.lblTransactionType.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblTransactionType.AppearanceItemCaption.Font")));
            this.lblTransactionType.AppearanceItemCaption.Options.UseFont = true;
            this.lblTransactionType.Control = this.rgVoucherType;
            resources.ApplyResources(this.lblTransactionType, "lblTransactionType");
            this.lblTransactionType.Location = new System.Drawing.Point(0, 39);
            this.lblTransactionType.MinSize = new System.Drawing.Size(174, 39);
            this.lblTransactionType.Name = "lblTransactionType";
            this.lblTransactionType.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 0);
            this.lblTransactionType.Size = new System.Drawing.Size(726, 44);
            this.lblTransactionType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblTransactionType.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 3);
            this.lblTransactionType.TextSize = new System.Drawing.Size(116, 23);
            this.lblTransactionType.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.ucAssetVoucherShortcut;
            resources.ApplyResources(this.layoutControlItem7, "layoutControlItem7");
            this.layoutControlItem7.Location = new System.Drawing.Point(878, 83);
            this.layoutControlItem7.MaxSize = new System.Drawing.Size(104, 636);
            this.layoutControlItem7.MinSize = new System.Drawing.Size(104, 636);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlItem7.Size = new System.Drawing.Size(104, 636);
            this.layoutControlItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // barManager2
            // 
            this.barManager2.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar3});
            this.barManager2.DockControls.Add(this.barDockControl1);
            this.barManager2.DockControls.Add(this.barDockControl2);
            this.barManager2.DockControls.Add(this.barDockControl3);
            this.barManager2.DockControls.Add(this.barDockControl4);
            this.barManager2.Form = this;
            this.barManager2.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiDelete,
            this.bbiDeleteCashBankRow,
            this.bbiMovetoAsset,
            this.bbiMoveToCashBank,
            this.bbiSave,
            this.bbiNew,
            this.bbiClose,
            this.bbiDeletePurchase,
            this.bbiAssetGenerationList});
            this.barManager2.MaxItemId = 10;
            this.barManager2.StatusBar = this.bar3;
            // 
            // bar3
            // 
            this.bar3.BarName = "Status bar";
            this.bar3.CanDockStyle = DevExpress.XtraBars.BarCanDockStyle.Bottom;
            this.bar3.DockCol = 0;
            this.bar3.DockRow = 0;
            this.bar3.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar3.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiDeletePurchase, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiDeleteCashBankRow),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiMovetoAsset, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiMoveToCashBank, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiAssetGenerationList, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiSave),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiNew, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiClose, true)});
            this.bar3.OptionsBar.AllowCollapse = true;
            this.bar3.OptionsBar.AllowQuickCustomization = false;
            this.bar3.OptionsBar.DisableClose = true;
            this.bar3.OptionsBar.DisableCustomization = true;
            this.bar3.OptionsBar.DrawBorder = false;
            this.bar3.OptionsBar.MultiLine = true;
            resources.ApplyResources(this.bar3, "bar3");
            // 
            // bbiDeletePurchase
            // 
            this.bbiDeletePurchase.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiDeletePurchase, "bbiDeletePurchase");
            this.bbiDeletePurchase.Id = 8;
            this.bbiDeletePurchase.Name = "bbiDeletePurchase";
            this.bbiDeletePurchase.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiDeletePurchase_ItemClick);
            // 
            // bbiDeleteCashBankRow
            // 
            this.bbiDeleteCashBankRow.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiDeleteCashBankRow, "bbiDeleteCashBankRow");
            this.bbiDeleteCashBankRow.Id = 1;
            this.bbiDeleteCashBankRow.Name = "bbiDeleteCashBankRow";
            this.bbiDeleteCashBankRow.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiDeleteCashBankRow_ItemClick);
            // 
            // bbiMovetoAsset
            // 
            this.bbiMovetoAsset.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiMovetoAsset, "bbiMovetoAsset");
            this.bbiMovetoAsset.Id = 2;
            this.bbiMovetoAsset.Name = "bbiMovetoAsset";
            this.bbiMovetoAsset.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiMovetoAsset_ItemClick);
            // 
            // bbiMoveToCashBank
            // 
            this.bbiMoveToCashBank.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiMoveToCashBank, "bbiMoveToCashBank");
            this.bbiMoveToCashBank.Id = 4;
            this.bbiMoveToCashBank.Name = "bbiMoveToCashBank";
            this.bbiMoveToCashBank.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiMoveToCashBank_ItemClick);
            // 
            // bbiAssetGenerationList
            // 
            this.bbiAssetGenerationList.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiAssetGenerationList, "bbiAssetGenerationList");
            this.bbiAssetGenerationList.Id = 9;
            this.bbiAssetGenerationList.Name = "bbiAssetGenerationList";
            this.bbiAssetGenerationList.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiAssetGenerationList_ItemClick);
            // 
            // bbiSave
            // 
            this.bbiSave.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiSave, "bbiSave");
            this.bbiSave.Id = 5;
            this.bbiSave.Name = "bbiSave";
            // 
            // bbiNew
            // 
            this.bbiNew.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiNew, "bbiNew");
            this.bbiNew.Id = 6;
            this.bbiNew.Name = "bbiNew";
            this.bbiNew.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiNew_ItemClick);
            // 
            // bbiClose
            // 
            this.bbiClose.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiClose, "bbiClose");
            this.bbiClose.Id = 7;
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
            // bbiDelete
            // 
            this.bbiDelete.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiDelete, "bbiDelete");
            this.bbiDelete.Id = 0;
            this.bbiDelete.Name = "bbiDelete";
            this.bbiDelete.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiDelete_ItemClick);
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.txtOtherCharges;
            resources.ApplyResources(this.layoutControlItem14, "layoutControlItem14");
            this.layoutControlItem14.Location = new System.Drawing.Point(477, 0);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 3, 2);
            this.layoutControlItem14.Size = new System.Drawing.Size(309, 25);
            this.layoutControlItem14.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.layoutControlItem14.TextSize = new System.Drawing.Size(71, 13);
            this.layoutControlItem14.TextToControlDistance = 5;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(398, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(10, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmInwardVoucherAdd
            // 
            this.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("frmInwardVoucherAdd.Appearance.Font")));
            this.Appearance.Options.UseFont = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.btnClose;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.layoutControl1);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Controls.Add(this.barDockControl3);
            this.Controls.Add(this.barDockControl4);
            this.Controls.Add(this.barDockControl2);
            this.Controls.Add(this.barDockControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Sizable;
            this.Name = "frmInwardVoucherAdd";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmInwardVoucherAdd_FormClosing);
            this.Load += new System.EventHandler(this.frmInwardVoucherAdd_Load);
            this.Shown += new System.EventHandler(this.frmInwardVoucherAdd_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmInwardVoucherAdd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtActualAmt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExchangeRate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCurrencyAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpCurrencyCountry.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpPurpose.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpDonor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkVendor.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgVoucherType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNarration.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtOtherCharges.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPurchase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPurchase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpAssetName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtQuantity)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnViewDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnPurchaseDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnCostcentre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtSalavageValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deInwardDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deInwardDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVoucherNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtReceiptBillNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lctgPurchase)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNarration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblVoucherNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBillno)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblVendor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDonor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciBank)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCurrencyGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciCurrency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDonorCurrency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblExchangeRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblliveAvgRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCalcAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciActualAmt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPurpose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTransactionType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private UIControls.UcCaptionPanel ucCaptionProject;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlGroup lctgPurchase;
        private DevExpress.XtraEditors.DateEdit deInwardDate;
        private DevExpress.XtraLayout.LayoutControlItem lblDate;
        private DevExpress.XtraEditors.TextEdit txtVoucherNo;
        private DevExpress.XtraLayout.LayoutControlItem lblVoucherNo;
        private DevExpress.XtraEditors.TextEdit txtReceiptBillNo;
        private DevExpress.XtraLayout.LayoutControlItem lblBillno;
        private DevExpress.XtraGrid.GridControl gcPurchase;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPurchase;
        private DevExpress.XtraGrid.Columns.GridColumn colAssetName;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.BarButtonItem bbiProject;
        private DevExpress.XtraBars.BarButtonItem bbiDate;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit rglkpAssetName;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView3;
        private DevExpress.XtraGrid.Columns.GridColumn colAssItemID;
        private DevExpress.XtraGrid.Columns.GridColumn colAssName;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbtnPurchaseDelete;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtQuantity;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtAmount;
        private DevExpress.XtraEditors.TextEdit txtOtherCharges;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraBars.BarDockControl barDockControl3;
        private DevExpress.XtraBars.BarDockControl barDockControl4;
        private DevExpress.XtraBars.BarDockControl barDockControl2;
        private DevExpress.XtraBars.BarDockControl barDockControl1;
        private DevExpress.XtraBars.BarManager barManager2;
        private DevExpress.XtraBars.Bar bar3;
        private DevExpress.XtraBars.BarButtonItem bbiDelete;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraGrid.Columns.GridColumn colViewDetails;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbtnViewDetails;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraBars.BarButtonItem bbiDeleteCashBankRow;
        private DevExpress.XtraBars.BarButtonItem bbiMovetoAsset;
        private DevExpress.XtraBars.BarButtonItem bbiMoveToCashBank;
        private DevExpress.XtraBars.BarButtonItem bbiSave;
        private DevExpress.XtraBars.BarButtonItem bbiNew;
        private DevExpress.XtraBars.BarButtonItem bbiClose;
        private DevExpress.XtraEditors.TextEdit txtNarration;
        private DevExpress.XtraLayout.LayoutControlItem lblNarration;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraEditors.RadioGroup rgVoucherType;
        private DevExpress.XtraLayout.LayoutControlItem lblTransactionType;
        private DevExpress.XtraEditors.LabelControl lblVoucherType;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraGrid.Columns.GridColumn colQuantity;
        private UIControls.UCAssetVoucherShortcuts ucAssetVoucherShortcut;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraEditors.SimpleButton btnNew;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraGrid.Columns.GridColumn colInoutDetailId;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountLedgerId;
        private DevExpress.XtraEditors.GridLookUpEdit glkVendor;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem lblVendor;
        private DevExpress.XtraGrid.Columns.GridColumn colVendorId;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colAvailableQty;
        private DevExpress.XtraEditors.GridLookUpEdit glkpDonor;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView2;
        private DevExpress.XtraLayout.LayoutControlItem lblDonor;
        private DevExpress.XtraEditors.GridLookUpEdit glkpPurpose;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView4;
        private DevExpress.XtraLayout.LayoutControlItem lblPurpose;
        private DevExpress.XtraGrid.Columns.GridColumn colDonoudId;
        private DevExpress.XtraGrid.Columns.GridColumn colDonorName;
        private DevExpress.XtraGrid.Columns.GridColumn colPurposeId;
        private DevExpress.XtraGrid.Columns.GridColumn colPurpose;
        private DevExpress.XtraGrid.Columns.GridColumn colDeleteAssetItem;
        private DevExpress.XtraBars.BarButtonItem bbiDeletePurchase;
        private DevExpress.XtraBars.BarButtonItem bbiAssetGenerationList;
        private UIControls.UcAssetJournal ucAssetJournal1;
        private DevExpress.XtraLayout.LayoutControlItem lciBank;
        private DevExpress.XtraGrid.Columns.GridColumn colcostcnetre;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbtnCostcentre;
        private DevExpress.XtraGrid.Columns.GridColumn colSource;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit rglkpSource;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colSourceName;
        private DevExpress.XtraGrid.Columns.GridColumn colSalvagevalue;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtSalavageValue;
        private DevExpress.XtraEditors.GridLookUpEdit glkpCurrencyCountry;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraLayout.LayoutControlItem lciCurrency;
        private DevExpress.XtraEditors.TextEdit txtCurrencyAmount;
        private DevExpress.XtraLayout.LayoutControlItem lciAmount;
        private DevExpress.XtraLayout.SimpleLabelItem lblliveAvgRate;
        private DevExpress.XtraEditors.LabelControl lblCalAmt;
        private DevExpress.XtraEditors.LabelControl lblAvgRate;
        private DevExpress.XtraLayout.SimpleLabelItem lblCalcAmount;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private DevExpress.XtraLayout.SimpleLabelItem lblDonorCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn gcCountryId;
        private DevExpress.XtraGrid.Columns.GridColumn gcCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn gcCurrencyName;
        private DevExpress.XtraGrid.Columns.GridColumn gcCurrencySymbol;
        private DevExpress.XtraEditors.TextEdit txtExchangeRate;
        private DevExpress.XtraLayout.LayoutControlItem lblExchangeRate;
        private DevExpress.XtraGrid.Columns.GridColumn gcExchangeRate;
        private DevExpress.XtraGrid.Columns.GridColumn gcLiveExchangeRate;
        private DevExpress.XtraEditors.TextEdit txtActualAmt;
        private DevExpress.XtraLayout.LayoutControlItem lciActualAmt;
        private DevExpress.XtraLayout.LayoutControlGroup lciCurrencyGroup;
    }
}