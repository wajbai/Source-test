namespace ACPP.Modules.Transaction
{
    partial class JournalAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(JournalAdd));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject5 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject6 = new DevExpress.Utils.SerializableAppearanceObject();
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject7 = new DevExpress.Utils.SerializableAppearanceObject();
            this.pnlFill = new DevExpress.XtraEditors.PanelControl();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lblLiveExchangeRate = new DevExpress.XtraEditors.LabelControl();
            this.glkpCurrencyCountry = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gvCurrencyCountry = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCurrencyCountryId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrencyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrencySymbol = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtActualAmount = new DevExpress.XtraEditors.TextEdit();
            this.txtExchangeRate = new DevExpress.XtraEditors.TextEdit();
            this.txtCurrencyAmount = new DevExpress.XtraEditors.TextEdit();
            this.barManager1 = new DevExpress.XtraBars.BarManager(this.components);
            this.bar2 = new DevExpress.XtraBars.Bar();
            this.bbiDeleteTrans = new DevExpress.XtraBars.BarButtonItem();
            this.bbiMoveTransaction = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem3 = new DevExpress.XtraBars.BarButtonItem();
            this.bbiNewTrans = new DevExpress.XtraBars.BarButtonItem();
            this.barButtonItem5 = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            this.ucAdditionalInfo = new ACPP.Modules.UIControls.ucAdditionalInfoMenu();
            this.btnRemoveVendorGSTInvoce = new DevExpress.XtraEditors.SimpleButton();
            this.btnVendor = new DevExpress.XtraEditors.SimpleButton();
            this.glkpVoucherType = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit2View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colVoucherId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVOUCHERTYPE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtJNarration = new DevExpress.XtraEditors.TextEdit();
            this.ucJournalShortcut = new ACPP.Modules.UIControls.ucVoucherShortcut();
            this.ucCaptionPanel = new ACPP.Modules.UIControls.UcCaptionPanel();
            this.gcTransaction = new DevExpress.XtraGrid.GridControl();
            this.gvTransaction = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colLedger = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rglkpLedger = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedCode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedgerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDebit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtDebit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colCredit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtCredit = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colLedgerBal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAction = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rbtnDelete = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colCostcentre = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rCostcentre = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colIdentification = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBookingId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTDSBookingView = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rTDSBookingView = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.colReferenceNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtRefferenceNo = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colChequeNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtChequeNo = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colMaterilizedOn = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rdtMaterilizsed = new DevExpress.XtraEditors.Repository.RepositoryItemDateEdit();
            this.colGSTLedgerClass = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rglkpLedgerGST = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.gvGSTClassLeddetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colgstId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colgstPer = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGStAmt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtGST = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colGST = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCGST = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSGST = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIGST = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsGSTLedger = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNarration = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtNarration = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.gccolVoucherDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colReferedAmt = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colOldLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rglkpSource = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.gridView4 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName1 = new DevExpress.XtraGrid.Columns.GridColumn();
            this.txtNarration = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.txtVoucher = new DevExpress.XtraEditors.TextEdit();
            this.deDate = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.gbMasterInfo = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcSave = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcClose = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcVendor = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcRemoveVendorGSTInvoice = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblTransactionDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcItemVoucerType = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcAdditionalLinks = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcCurrencyAmount = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblDonorCurrency = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lcCurrencyEmptySpace = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcExchangeRate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblCalculatedAmtCaption = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblCalculatedAmt = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lcActualAmount = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem7 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblVoucher = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcCurrency = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcLiveExchangeRate = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem8 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.barBottomShortcut = new DevExpress.XtraBars.Bar();
            this.bar1 = new DevExpress.XtraBars.Bar();
            ((System.ComponentModel.ISupportInitialize)(this.pnlFill)).BeginInit();
            this.pnlFill.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glkpCurrencyCountry.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCurrencyCountry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtActualAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExchangeRate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCurrencyAmount.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpVoucherType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit2View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtJNarration.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTransaction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTransaction)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtDebit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCredit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnDelete)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rCostcentre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rTDSBookingView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRefferenceNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtChequeNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdtMaterilizsed)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdtMaterilizsed.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpLedgerGST)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvGSTClassLeddetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtGST)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtNarration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNarration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVoucher.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbMasterInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcSave)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcClose)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcVendor)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcRemoveVendorGSTInvoice)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTransactionDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemVoucerType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcAdditionalLinks)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCurrencyAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDonorCurrency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCurrencyEmptySpace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcExchangeRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCalculatedAmtCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCalculatedAmt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcActualAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblVoucher)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCurrency)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcLiveExchangeRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem8)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlFill
            // 
            this.pnlFill.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlFill.Controls.Add(this.layoutControl1);
            resources.ApplyResources(this.pnlFill, "pnlFill");
            this.pnlFill.Name = "pnlFill";
            // 
            // layoutControl1
            // 
            this.layoutControl1.AllowCustomizationMenu = false;
            this.layoutControl1.Controls.Add(this.lblLiveExchangeRate);
            this.layoutControl1.Controls.Add(this.glkpCurrencyCountry);
            this.layoutControl1.Controls.Add(this.txtActualAmount);
            this.layoutControl1.Controls.Add(this.txtExchangeRate);
            this.layoutControl1.Controls.Add(this.txtCurrencyAmount);
            this.layoutControl1.Controls.Add(this.ucAdditionalInfo);
            this.layoutControl1.Controls.Add(this.btnRemoveVendorGSTInvoce);
            this.layoutControl1.Controls.Add(this.btnVendor);
            this.layoutControl1.Controls.Add(this.glkpVoucherType);
            this.layoutControl1.Controls.Add(this.txtJNarration);
            this.layoutControl1.Controls.Add(this.ucJournalShortcut);
            this.layoutControl1.Controls.Add(this.ucCaptionPanel);
            this.layoutControl1.Controls.Add(this.gcTransaction);
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.txtVoucher);
            this.layoutControl1.Controls.Add(this.deDate);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(787, 255, 394, 382);
            this.layoutControl1.OptionsView.UseDefaultDragAndDropRendering = false;
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // lblLiveExchangeRate
            // 
            this.lblLiveExchangeRate.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblLiveExchangeRate.Appearance.Font")));
            resources.ApplyResources(this.lblLiveExchangeRate, "lblLiveExchangeRate");
            this.lblLiveExchangeRate.Name = "lblLiveExchangeRate";
            this.lblLiveExchangeRate.StyleController = this.layoutControl1;
            // 
            // glkpCurrencyCountry
            // 
            resources.ApplyResources(this.glkpCurrencyCountry, "glkpCurrencyCountry");
            this.glkpCurrencyCountry.Name = "glkpCurrencyCountry";
            this.glkpCurrencyCountry.Properties.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFitResizePopup;
            this.glkpCurrencyCountry.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpCurrencyCountry.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpCurrencyCountry.Properties.Buttons"))))});
            this.glkpCurrencyCountry.Properties.NullText = resources.GetString("glkpCurrencyCountry.Properties.NullText");
            this.glkpCurrencyCountry.Properties.PopupFormMinSize = new System.Drawing.Size(207, 0);
            this.glkpCurrencyCountry.Properties.PopupFormSize = new System.Drawing.Size(0, 50);
            this.glkpCurrencyCountry.Properties.View = this.gvCurrencyCountry;
            this.glkpCurrencyCountry.StyleController = this.layoutControl1;
            this.glkpCurrencyCountry.EditValueChanged += new System.EventHandler(this.lkpCountry_EditValueChanged);
            // 
            // gvCurrencyCountry
            // 
            this.gvCurrencyCountry.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvCurrencyCountry.Appearance.FocusedRow.Font")));
            this.gvCurrencyCountry.Appearance.FocusedRow.Options.UseFont = true;
            this.gvCurrencyCountry.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCurrencyCountryId,
            this.colCurrency,
            this.colCurrencyName,
            this.colCurrencySymbol});
            this.gvCurrencyCountry.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gvCurrencyCountry.Name = "gvCurrencyCountry";
            this.gvCurrencyCountry.OptionsBehavior.Editable = false;
            this.gvCurrencyCountry.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvCurrencyCountry.OptionsView.RowAutoHeight = true;
            this.gvCurrencyCountry.OptionsView.ShowColumnHeaders = false;
            this.gvCurrencyCountry.OptionsView.ShowGroupPanel = false;
            this.gvCurrencyCountry.OptionsView.ShowIndicator = false;
            // 
            // colCurrencyCountryId
            // 
            resources.ApplyResources(this.colCurrencyCountryId, "colCurrencyCountryId");
            this.colCurrencyCountryId.FieldName = "COUNTRY_ID";
            this.colCurrencyCountryId.Name = "colCurrencyCountryId";
            // 
            // colCurrency
            // 
            resources.ApplyResources(this.colCurrency, "colCurrency");
            this.colCurrency.FieldName = "CURRENCY";
            this.colCurrency.Name = "colCurrency";
            // 
            // colCurrencyName
            // 
            resources.ApplyResources(this.colCurrencyName, "colCurrencyName");
            this.colCurrencyName.FieldName = "CURRENCY_NAME";
            this.colCurrencyName.Name = "colCurrencyName";
            // 
            // colCurrencySymbol
            // 
            resources.ApplyResources(this.colCurrencySymbol, "colCurrencySymbol");
            this.colCurrencySymbol.FieldName = "CURRENCY_SYMBOL";
            this.colCurrencySymbol.Name = "colCurrencySymbol";
            // 
            // txtActualAmount
            // 
            resources.ApplyResources(this.txtActualAmount, "txtActualAmount");
            this.txtActualAmount.EnterMoveNextControl = true;
            this.txtActualAmount.Name = "txtActualAmount";
            this.txtActualAmount.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtActualAmount.Properties.DisplayFormat.FormatString = "n";
            this.txtActualAmount.Properties.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.txtActualAmount.Properties.Mask.EditMask = resources.GetString("txtActualAmount.Properties.Mask.EditMask");
            this.txtActualAmount.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("txtActualAmount.Properties.Mask.MaskType")));
            this.txtActualAmount.StyleController = this.layoutControl1;
            // 
            // txtExchangeRate
            // 
            resources.ApplyResources(this.txtExchangeRate, "txtExchangeRate");
            this.txtExchangeRate.EnterMoveNextControl = true;
            this.txtExchangeRate.Name = "txtExchangeRate";
            this.txtExchangeRate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtExchangeRate.Properties.Mask.EditMask = resources.GetString("txtExchangeRate.Properties.Mask.EditMask");
            this.txtExchangeRate.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("txtExchangeRate.Properties.Mask.MaskType")));
            this.txtExchangeRate.StyleController = this.layoutControl1;
            this.txtExchangeRate.EditValueChanged += new System.EventHandler(this.txtExchangeRate_EditValueChanged);
            // 
            // txtCurrencyAmount
            // 
            resources.ApplyResources(this.txtCurrencyAmount, "txtCurrencyAmount");
            this.txtCurrencyAmount.EnterMoveNextControl = true;
            this.txtCurrencyAmount.MenuManager = this.barManager1;
            this.txtCurrencyAmount.Name = "txtCurrencyAmount";
            this.txtCurrencyAmount.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtCurrencyAmount.Properties.Mask.EditMask = resources.GetString("txtCurrencyAmount.Properties.Mask.EditMask");
            this.txtCurrencyAmount.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("txtCurrencyAmount.Properties.Mask.MaskType")));
            this.txtCurrencyAmount.StyleController = this.layoutControl1;
            this.txtCurrencyAmount.EditValueChanged += new System.EventHandler(this.txtCurrencyAmount_EditValueChanged);
            // 
            // barManager1
            // 
            this.barManager1.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar2});
            this.barManager1.DockControls.Add(this.barDockControlTop);
            this.barManager1.DockControls.Add(this.barDockControlBottom);
            this.barManager1.DockControls.Add(this.barDockControlLeft);
            this.barManager1.DockControls.Add(this.barDockControlRight);
            this.barManager1.Form = this;
            this.barManager1.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiDeleteTrans,
            this.bbiMoveTransaction,
            this.barButtonItem3,
            this.bbiNewTrans,
            this.barButtonItem5});
            this.barManager1.MaxItemId = 5;
            // 
            // bar2
            // 
            this.bar2.BarName = "Tools";
            this.bar2.DockCol = 0;
            this.bar2.DockRow = 0;
            this.bar2.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar2.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiDeleteTrans),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiMoveTransaction, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem3, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiNewTrans, true),
            new DevExpress.XtraBars.LinkPersistInfo(this.barButtonItem5, true)});
            this.bar2.OptionsBar.AllowQuickCustomization = false;
            this.bar2.OptionsBar.DrawBorder = false;
            this.bar2.OptionsBar.DrawDragBorder = false;
            resources.ApplyResources(this.bar2, "bar2");
            // 
            // bbiDeleteTrans
            // 
            this.bbiDeleteTrans.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiDeleteTrans, "bbiDeleteTrans");
            this.bbiDeleteTrans.Id = 0;
            this.bbiDeleteTrans.Name = "bbiDeleteTrans";
            this.bbiDeleteTrans.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiDeleteTrans_ItemClick);
            // 
            // bbiMoveTransaction
            // 
            this.bbiMoveTransaction.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiMoveTransaction, "bbiMoveTransaction");
            this.bbiMoveTransaction.Id = 1;
            this.bbiMoveTransaction.Name = "bbiMoveTransaction";
            this.bbiMoveTransaction.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiMoveTransaction_ItemClick);
            // 
            // barButtonItem3
            // 
            this.barButtonItem3.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.barButtonItem3, "barButtonItem3");
            this.barButtonItem3.Id = 2;
            this.barButtonItem3.Name = "barButtonItem3";
            // 
            // bbiNewTrans
            // 
            this.bbiNewTrans.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.bbiNewTrans, "bbiNewTrans");
            this.bbiNewTrans.Id = 3;
            this.bbiNewTrans.Name = "bbiNewTrans";
            this.bbiNewTrans.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiNewTrans_ItemClick);
            // 
            // barButtonItem5
            // 
            this.barButtonItem5.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.barButtonItem5, "barButtonItem5");
            this.barButtonItem5.Id = 4;
            this.barButtonItem5.Name = "barButtonItem5";
            this.barButtonItem5.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.barButtonItem5_ItemClick);
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
            // ucAdditionalInfo
            // 
            this.ucAdditionalInfo.DiableDonor = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAdditionalInfo.DiableVoucherBills = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAdditionalInfo.DisableDeleteVocuher = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAdditionalInfo.DisableEntryMethod = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucAdditionalInfo.DisablePrintVoucher = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucAdditionalInfo.EntryCaption = "<b>Press <color=blue>F11</color> to Single Entry</b>";
            resources.ApplyResources(this.ucAdditionalInfo, "ucAdditionalInfo");
            this.ucAdditionalInfo.LockDeleteVocuher = true;
            this.ucAdditionalInfo.LockPrintVoucher = true;
            this.ucAdditionalInfo.Name = "ucAdditionalInfo";
            this.ucAdditionalInfo.PrintVoucherCaption = "Print Voucher";
            this.ucAdditionalInfo.DeleteVoucherClicked += new System.EventHandler(this.ucAdditionalInfo_DeleteVoucherClicked);
            this.ucAdditionalInfo.PrintVoucherClicked += new System.EventHandler(this.ucAdditionalInfo_PrintVoucherClicked);
            // 
            // btnRemoveVendorGSTInvoce
            // 
            resources.ApplyResources(this.btnRemoveVendorGSTInvoce, "btnRemoveVendorGSTInvoce");
            this.btnRemoveVendorGSTInvoce.Name = "btnRemoveVendorGSTInvoce";
            this.btnRemoveVendorGSTInvoce.StyleController = this.layoutControl1;
            this.btnRemoveVendorGSTInvoce.Click += new System.EventHandler(this.btnRemoveVendorGSTInvoce_Click);
            // 
            // btnVendor
            // 
            this.btnVendor.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("btnVendor.Appearance.Font")));
            this.btnVendor.Appearance.Options.UseFont = true;
            resources.ApplyResources(this.btnVendor, "btnVendor");
            this.btnVendor.Name = "btnVendor";
            this.btnVendor.StyleController = this.layoutControl1;
            this.btnVendor.Click += new System.EventHandler(this.btnVendor_Click);
            // 
            // glkpVoucherType
            // 
            this.glkpVoucherType.EnterMoveNextControl = true;
            resources.ApplyResources(this.glkpVoucherType, "glkpVoucherType");
            this.glkpVoucherType.Name = "glkpVoucherType";
            this.glkpVoucherType.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("glkpVoucherType.Properties.Appearance.Font")));
            this.glkpVoucherType.Properties.Appearance.Options.UseFont = true;
            this.glkpVoucherType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpVoucherType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpVoucherType.Properties.Buttons"))))});
            this.glkpVoucherType.Properties.ImmediatePopup = true;
            this.glkpVoucherType.Properties.NullText = resources.GetString("glkpVoucherType.Properties.NullText");
            this.glkpVoucherType.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.glkpVoucherType.Properties.PopupFormSize = new System.Drawing.Size(279, 50);
            this.glkpVoucherType.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.glkpVoucherType.Properties.View = this.gridLookUpEdit2View;
            this.glkpVoucherType.StyleController = this.layoutControl1;
            // 
            // gridLookUpEdit2View
            // 
            this.gridLookUpEdit2View.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridLookUpEdit2View.Appearance.FocusedRow.Font")));
            this.gridLookUpEdit2View.Appearance.FocusedRow.Options.UseFont = true;
            this.gridLookUpEdit2View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colVoucherId,
            this.colVOUCHERTYPE});
            this.gridLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit2View.Name = "gridLookUpEdit2View";
            this.gridLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit2View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit2View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit2View.OptionsView.ShowIndicator = false;
            // 
            // colVoucherId
            // 
            resources.ApplyResources(this.colVoucherId, "colVoucherId");
            this.colVoucherId.FieldName = "VOUCHER_ID";
            this.colVoucherId.Name = "colVoucherId";
            // 
            // colVOUCHERTYPE
            // 
            resources.ApplyResources(this.colVOUCHERTYPE, "colVOUCHERTYPE");
            this.colVOUCHERTYPE.FieldName = "VOUCHER_NAME";
            this.colVOUCHERTYPE.Name = "colVOUCHERTYPE";
            // 
            // txtJNarration
            // 
            resources.ApplyResources(this.txtJNarration, "txtJNarration");
            this.txtJNarration.MenuManager = this.barManager1;
            this.txtJNarration.Name = "txtJNarration";
            this.txtJNarration.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtJNarration.Properties.MaxLength = 300;
            this.txtJNarration.StyleController = this.layoutControl1;
            this.txtJNarration.Enter += new System.EventHandler(this.txtJNarration_Enter);
            this.txtJNarration.Leave += new System.EventHandler(this.txtJNarration_Leave);
            // 
            // ucJournalShortcut
            // 
            this.ucJournalShortcut.DisableBankAccount = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucJournalShortcut.DisableConfigure = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucJournalShortcut.DisableContra = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucJournalShortcut.DisableCostCentre = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucJournalShortcut.DisableDate = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucJournalShortcut.DisableDeleteVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucJournalShortcut.DisableDonor = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucJournalShortcut.DisableJournal = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucJournalShortcut.DisableLedgerAdd = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucJournalShortcut.DisableLedgerOption = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucJournalShortcut.DisableMapping = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucJournalShortcut.DisableNextVoucherDate = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucJournalShortcut.DisablePayment = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucJournalShortcut.DisableProject = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucJournalShortcut.DisableReceipt = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucJournalShortcut.DisableTransView = DevExpress.XtraBars.BarItemVisibility.Always;
            resources.ApplyResources(this.ucJournalShortcut, "ucJournalShortcut");
            this.ucJournalShortcut.LockContra = true;
            this.ucJournalShortcut.LockDate = true;
            this.ucJournalShortcut.LockDeleteVoucher = true;
            this.ucJournalShortcut.LockNextVoucherDate = true;
            this.ucJournalShortcut.LockPayment = true;
            this.ucJournalShortcut.LockProject = true;
            this.ucJournalShortcut.LockReceipt = true;
            this.ucJournalShortcut.Name = "ucJournalShortcut";
            this.ucJournalShortcut.DateClicked += new System.EventHandler(this.ucJournalShortcut_DateClicked);
            this.ucJournalShortcut.ProjectClicked += new System.EventHandler(this.ucJournalShortcut_ProjectClicked);
            this.ucJournalShortcut.CostCentreClicked += new System.EventHandler(this.ucJournalShortcut_CostCentreClicked);
            this.ucJournalShortcut.MappingClicked += new System.EventHandler(this.ucJournalShortcut_MappingClicked);
            this.ucJournalShortcut.ConfigureClicked += new System.EventHandler(this.ucJournalShortcut_ConfigureClicked);
            this.ucJournalShortcut.TransactionVoucherViewClicked += new System.EventHandler(this.ucJournalShortcut_TransactionVoucherViewClicked);
            this.ucJournalShortcut.LedgerAddClicked += new System.EventHandler(this.ucJournalShortcut_LedgerAddClicked);
            this.ucJournalShortcut.LedgerOptionsClicked += new System.EventHandler(this.ucJournalShortcut_LedgerOptionsClicked);
            this.ucJournalShortcut.NextVoucherDateClicked += new System.EventHandler(this.ucJournalShortcut_NextVoucherDateClicked);
            // 
            // ucCaptionPanel
            // 
            resources.ApplyResources(this.ucCaptionPanel, "ucCaptionPanel");
            this.ucCaptionPanel.Name = "ucCaptionPanel";
            // 
            // gcTransaction
            // 
            resources.ApplyResources(this.gcTransaction, "gcTransaction");
            this.gcTransaction.MainView = this.gvTransaction;
            this.gcTransaction.Name = "gcTransaction";
            this.gcTransaction.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rglkpSource,
            this.rglkpLedger,
            this.rtxtDebit,
            this.txtCredit,
            this.txtNarration,
            this.rbtnDelete,
            this.rCostcentre,
            this.rTDSBookingView,
            this.rtxtNarration,
            this.txtRefferenceNo,
            this.rglkpLedgerGST,
            this.rtxtGST,
            this.rtxtChequeNo,
            this.rdtMaterilizsed});
            this.gcTransaction.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTransaction});
            this.gcTransaction.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.gcTransaction_ProcessGridKey);
            // 
            // gvTransaction
            // 
            this.gvTransaction.Appearance.FilterPanel.BackColor = ((System.Drawing.Color)(resources.GetObject("gvTransaction.Appearance.FilterPanel.BackColor")));
            this.gvTransaction.Appearance.FilterPanel.Options.UseBackColor = true;
            this.gvTransaction.Appearance.FocusedCell.BackColor = ((System.Drawing.Color)(resources.GetObject("gvTransaction.Appearance.FocusedCell.BackColor")));
            this.gvTransaction.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvTransaction.Appearance.FooterPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvTransaction.Appearance.FooterPanel.Font")));
            this.gvTransaction.Appearance.FooterPanel.Options.UseFont = true;
            this.gvTransaction.Appearance.GroupFooter.Font = ((System.Drawing.Font)(resources.GetObject("gvTransaction.Appearance.GroupFooter.Font")));
            this.gvTransaction.Appearance.GroupFooter.Options.UseFont = true;
            this.gvTransaction.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvTransaction.Appearance.HeaderPanel.Font")));
            this.gvTransaction.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvTransaction.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gvTransaction.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colLedger,
            this.colDebit,
            this.colCredit,
            this.colLedgerBal,
            this.colAction,
            this.colCostcentre,
            this.colIdentification,
            this.colBookingId,
            this.colTDSBookingView,
            this.colReferenceNumber,
            this.colChequeNo,
            this.colMaterilizedOn,
            this.colGSTLedgerClass,
            this.colGStAmt,
            this.colGST,
            this.colCGST,
            this.colSGST,
            this.colIGST,
            this.colIsGSTLedger,
            this.colNarration,
            this.gccolVoucherDate,
            this.colReferedAmt,
            this.colOldLedgerId});
            this.gvTransaction.GridControl = this.gcTransaction;
            this.gvTransaction.GroupSummary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridGroupSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("gvTransaction.GroupSummary"))), resources.GetString("gvTransaction.GroupSummary1"), ((DevExpress.XtraGrid.Columns.GridColumn)(resources.GetObject("gvTransaction.GroupSummary2"))), resources.GetString("gvTransaction.GroupSummary3"))});
            this.gvTransaction.Name = "gvTransaction";
            this.gvTransaction.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.True;
            this.gvTransaction.OptionsBehavior.FocusLeaveOnTab = true;
            this.gvTransaction.OptionsBehavior.SummariesIgnoreNullValues = true;
            this.gvTransaction.OptionsFilter.AllowColumnMRUFilterList = false;
            this.gvTransaction.OptionsFilter.AllowFilterEditor = false;
            this.gvTransaction.OptionsFilter.AllowFilterIncrementalSearch = false;
            this.gvTransaction.OptionsFilter.AllowMRUFilterList = false;
            this.gvTransaction.OptionsFilter.FilterEditorUseMenuForOperandsAndOperators = false;
            this.gvTransaction.OptionsFilter.ShowAllTableValuesInCheckedFilterPopup = false;
            this.gvTransaction.OptionsMenu.ShowAddNewSummaryItem = DevExpress.Utils.DefaultBoolean.True;
            this.gvTransaction.OptionsNavigation.AutoFocusNewRow = true;
            this.gvTransaction.OptionsNavigation.EnterMoveNextColumn = true;
            this.gvTransaction.OptionsSelection.EnableAppearanceFocusedRow = false;
            this.gvTransaction.OptionsSelection.MultiSelectMode = DevExpress.XtraGrid.Views.Grid.GridMultiSelectMode.CellSelect;
            this.gvTransaction.OptionsView.BestFitMaxRowCount = 2;
            this.gvTransaction.OptionsView.GroupFooterShowMode = DevExpress.XtraGrid.Views.Grid.GroupFooterShowMode.VisibleAlways;
            this.gvTransaction.OptionsView.NewItemRowPosition = DevExpress.XtraGrid.Views.Grid.NewItemRowPosition.Bottom;
            this.gvTransaction.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gvTransaction.OptionsView.ShowFooter = true;
            this.gvTransaction.OptionsView.ShowGroupPanel = false;
            this.gvTransaction.OptionsView.ShowIndicator = false;
            this.gvTransaction.RowCellStyle += new DevExpress.XtraGrid.Views.Grid.RowCellStyleEventHandler(this.gvTransaction_RowCellStyle);
            this.gvTransaction.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gvTransaction_ShowingEditor);
            this.gvTransaction.ColumnChanged += new System.EventHandler(this.gvTransaction_ColumnChanged);
            this.gvTransaction.CellValueChanged += new DevExpress.XtraGrid.Views.Base.CellValueChangedEventHandler(this.gvTransaction_CellValueChanged);
            this.gvTransaction.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.gvTransaction_InvalidRowException);
            this.gvTransaction.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gvTransaction_ValidateRow);
            // 
            // colLedger
            // 
            this.colLedger.AppearanceCell.BackColor = ((System.Drawing.Color)(resources.GetObject("colLedger.AppearanceCell.BackColor")));
            this.colLedger.AppearanceCell.Options.UseBackColor = true;
            resources.ApplyResources(this.colLedger, "colLedger");
            this.colLedger.ColumnEdit = this.rglkpLedger;
            this.colLedger.FieldName = "LEDGER_ID";
            this.colLedger.FilterMode = DevExpress.XtraGrid.ColumnFilterMode.DisplayText;
            this.colLedger.Name = "colLedger";
            this.colLedger.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colLedger.OptionsFilter.AllowAutoFilter = false;
            this.colLedger.OptionsFilter.AllowFilter = false;
            this.colLedger.OptionsFilter.AllowFilterModeChanging = DevExpress.Utils.DefaultBoolean.False;
            this.colLedger.OptionsFilter.FilterBySortField = DevExpress.Utils.DefaultBoolean.False;
            this.colLedger.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colLedger.SortMode = DevExpress.XtraGrid.ColumnSortMode.DisplayText;
            // 
            // rglkpLedger
            // 
            resources.ApplyResources(this.rglkpLedger, "rglkpLedger");
            this.rglkpLedger.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.rglkpLedger.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.rglkpLedger.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rglkpLedger.Buttons"))), resources.GetString("rglkpLedger.Buttons1"), ((int)(resources.GetObject("rglkpLedger.Buttons2"))), ((bool)(resources.GetObject("rglkpLedger.Buttons3"))), ((bool)(resources.GetObject("rglkpLedger.Buttons4"))), ((bool)(resources.GetObject("rglkpLedger.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("rglkpLedger.Buttons6"))), ((System.Drawing.Image)(resources.GetObject("rglkpLedger.Buttons7"))), new DevExpress.Utils.KeyShortcut((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.A)), serializableAppearanceObject5, resources.GetString("rglkpLedger.Buttons8"), ((object)(resources.GetObject("rglkpLedger.Buttons9"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("rglkpLedger.Buttons10"))), ((bool)(resources.GetObject("rglkpLedger.Buttons11"))))});
            this.rglkpLedger.CloseUpKey = new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.F2);
            this.rglkpLedger.ImmediatePopup = true;
            this.rglkpLedger.Name = "rglkpLedger";
            this.rglkpLedger.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.rglkpLedger.PopupFormSize = new System.Drawing.Size(287, 0);
            this.rglkpLedger.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.rglkpLedger.View = this.repositoryItemGridLookUpEdit1View;
            this.rglkpLedger.QueryPopUp += new System.ComponentModel.CancelEventHandler(this.rglkpLedger_QueryPopUp);
            this.rglkpLedger.EditValueChanged += new System.EventHandler(this.rglkpLedger_EditValueChanged);
            this.rglkpLedger.Leave += new System.EventHandler(this.rglkpLedger_Leave);
            this.rglkpLedger.MouseDown += new System.Windows.Forms.MouseEventHandler(this.rglkpLedger_MouseDown);
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.Appearance.FilterPanel.BackColor = ((System.Drawing.Color)(resources.GetObject("repositoryItemGridLookUpEdit1View.Appearance.FilterPanel.BackColor")));
            this.repositoryItemGridLookUpEdit1View.Appearance.FilterPanel.Options.UseBackColor = true;
            this.repositoryItemGridLookUpEdit1View.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("repositoryItemGridLookUpEdit1View.Appearance.FocusedRow.Font")));
            this.repositoryItemGridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = true;
            this.repositoryItemGridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colLedgerId,
            this.colLedCode,
            this.colLedgerName});
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.BestFitMaxRowCount = 0;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // colLedgerId
            // 
            resources.ApplyResources(this.colLedgerId, "colLedgerId");
            this.colLedgerId.FieldName = "LEDGER_ID";
            this.colLedgerId.Name = "colLedgerId";
            // 
            // colLedCode
            // 
            this.colLedCode.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colLedCode.AppearanceHeader.Font")));
            this.colLedCode.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colLedCode, "colLedCode");
            this.colLedCode.FieldName = "LEDGER_CODE";
            this.colLedCode.Name = "colLedCode";
            // 
            // colLedgerName
            // 
            this.colLedgerName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colLedgerName.AppearanceHeader.Font")));
            this.colLedgerName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colLedgerName, "colLedgerName");
            this.colLedgerName.FieldName = "LEDGER_NAME";
            this.colLedgerName.Name = "colLedgerName";
            // 
            // colDebit
            // 
            this.colDebit.AppearanceCell.BackColor = ((System.Drawing.Color)(resources.GetObject("colDebit.AppearanceCell.BackColor")));
            this.colDebit.AppearanceCell.Options.UseBackColor = true;
            this.colDebit.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colDebit.AppearanceHeader.Font")));
            this.colDebit.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colDebit, "colDebit");
            this.colDebit.ColumnEdit = this.rtxtDebit;
            this.colDebit.FieldName = "DEBIT";
            this.colDebit.GroupFormat.FormatString = "C";
            this.colDebit.Name = "colDebit";
            this.colDebit.OptionsColumn.AllowMove = false;
            this.colDebit.OptionsColumn.AllowSize = false;
            this.colDebit.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colDebit.OptionsColumn.FixedWidth = true;
            this.colDebit.OptionsFilter.AllowAutoFilter = false;
            this.colDebit.OptionsFilter.AllowFilter = false;
            this.colDebit.OptionsFilter.AllowFilterModeChanging = DevExpress.Utils.DefaultBoolean.False;
            this.colDebit.OptionsFilter.FilterBySortField = DevExpress.Utils.DefaultBoolean.False;
            this.colDebit.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colDebit.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("colDebit.Summary"))), resources.GetString("colDebit.Summary1"), resources.GetString("colDebit.Summary2"))});
            // 
            // rtxtDebit
            // 
            resources.ApplyResources(this.rtxtDebit, "rtxtDebit");
            this.rtxtDebit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.rtxtDebit.DisplayFormat.FormatString = "C";
            this.rtxtDebit.Mask.EditMask = resources.GetString("rtxtDebit.Mask.EditMask");
            this.rtxtDebit.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("rtxtDebit.Mask.MaskType")));
            this.rtxtDebit.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("rtxtDebit.Mask.UseMaskAsDisplayFormat")));
            this.rtxtDebit.MaxLength = 14;
            this.rtxtDebit.Name = "rtxtDebit";
            // 
            // colCredit
            // 
            this.colCredit.AppearanceCell.BackColor = ((System.Drawing.Color)(resources.GetObject("colCredit.AppearanceCell.BackColor")));
            this.colCredit.AppearanceCell.Options.UseBackColor = true;
            resources.ApplyResources(this.colCredit, "colCredit");
            this.colCredit.ColumnEdit = this.txtCredit;
            this.colCredit.FieldName = "CREDIT";
            this.colCredit.Name = "colCredit";
            this.colCredit.OptionsColumn.AllowMove = false;
            this.colCredit.OptionsColumn.AllowSize = false;
            this.colCredit.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colCredit.OptionsColumn.FixedWidth = true;
            this.colCredit.OptionsFilter.AllowAutoFilter = false;
            this.colCredit.OptionsFilter.AllowFilter = false;
            this.colCredit.OptionsFilter.AllowFilterModeChanging = DevExpress.Utils.DefaultBoolean.False;
            this.colCredit.OptionsFilter.FilterBySortField = DevExpress.Utils.DefaultBoolean.False;
            this.colCredit.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colCredit.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("colCredit.Summary"))), resources.GetString("colCredit.Summary1"), resources.GetString("colCredit.Summary2"))});
            // 
            // txtCredit
            // 
            resources.ApplyResources(this.txtCredit, "txtCredit");
            this.txtCredit.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtCredit.Mask.EditMask = resources.GetString("txtCredit.Mask.EditMask");
            this.txtCredit.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("txtCredit.Mask.MaskType")));
            this.txtCredit.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("txtCredit.Mask.UseMaskAsDisplayFormat")));
            this.txtCredit.MaxLength = 14;
            this.txtCredit.Name = "txtCredit";
            // 
            // colLedgerBal
            // 
            this.colLedgerBal.AppearanceCell.Font = ((System.Drawing.Font)(resources.GetObject("colLedgerBal.AppearanceCell.Font")));
            this.colLedgerBal.AppearanceCell.Options.UseFont = true;
            resources.ApplyResources(this.colLedgerBal, "colLedgerBal");
            this.colLedgerBal.DisplayFormat.FormatString = "n";
            this.colLedgerBal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colLedgerBal.FieldName = "LEDGER_BALANCE";
            this.colLedgerBal.Name = "colLedgerBal";
            this.colLedgerBal.OptionsColumn.AllowEdit = false;
            this.colLedgerBal.OptionsColumn.AllowFocus = false;
            this.colLedgerBal.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colLedgerBal.OptionsColumn.FixedWidth = true;
            this.colLedgerBal.OptionsFilter.AllowAutoFilter = false;
            this.colLedgerBal.OptionsFilter.AllowFilter = false;
            this.colLedgerBal.OptionsFilter.AllowFilterModeChanging = DevExpress.Utils.DefaultBoolean.False;
            this.colLedgerBal.OptionsFilter.FilterBySortField = DevExpress.Utils.DefaultBoolean.False;
            this.colLedgerBal.OptionsFilter.ImmediateUpdateAutoFilter = false;
            // 
            // colAction
            // 
            this.colAction.ColumnEdit = this.rbtnDelete;
            this.colAction.Name = "colAction";
            this.colAction.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colAction.OptionsColumn.FixedWidth = true;
            this.colAction.OptionsColumn.TabStop = false;
            this.colAction.OptionsFilter.AllowAutoFilter = false;
            this.colAction.OptionsFilter.AllowFilter = false;
            this.colAction.OptionsFilter.ImmediateUpdateAutoFilter = false;
            resources.ApplyResources(this.colAction, "colAction");
            // 
            // rbtnDelete
            // 
            resources.ApplyResources(this.rbtnDelete, "rbtnDelete");
            this.rbtnDelete.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rbtnDelete.Buttons"))), resources.GetString("rbtnDelete.Buttons1"), ((int)(resources.GetObject("rbtnDelete.Buttons2"))), ((bool)(resources.GetObject("rbtnDelete.Buttons3"))), ((bool)(resources.GetObject("rbtnDelete.Buttons4"))), ((bool)(resources.GetObject("rbtnDelete.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("rbtnDelete.Buttons6"))), global::ACPP.Properties.Resources.Delete_Mob, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, resources.GetString("rbtnDelete.Buttons7"), ((object)(resources.GetObject("rbtnDelete.Buttons8"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("rbtnDelete.Buttons9"))), ((bool)(resources.GetObject("rbtnDelete.Buttons10"))))});
            this.rbtnDelete.Name = "rbtnDelete";
            this.rbtnDelete.Click += new System.EventHandler(this.rbtnDelete_Click);
            // 
            // colCostcentre
            // 
            this.colCostcentre.ColumnEdit = this.rCostcentre;
            this.colCostcentre.Name = "colCostcentre";
            this.colCostcentre.OptionsColumn.AllowSize = false;
            this.colCostcentre.OptionsColumn.FixedWidth = true;
            this.colCostcentre.OptionsColumn.ShowCaption = false;
            this.colCostcentre.OptionsColumn.TabStop = false;
            resources.ApplyResources(this.colCostcentre, "colCostcentre");
            // 
            // rCostcentre
            // 
            resources.ApplyResources(this.rCostcentre, "rCostcentre");
            this.rCostcentre.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rCostcentre.Buttons"))), resources.GetString("rCostcentre.Buttons1"), ((int)(resources.GetObject("rCostcentre.Buttons2"))), ((bool)(resources.GetObject("rCostcentre.Buttons3"))), ((bool)(resources.GetObject("rCostcentre.Buttons4"))), ((bool)(resources.GetObject("rCostcentre.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("rCostcentre.Buttons6"))), global::ACPP.Properties.Resources.Donor_mapping, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject6, resources.GetString("rCostcentre.Buttons7"), ((object)(resources.GetObject("rCostcentre.Buttons8"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("rCostcentre.Buttons9"))), ((bool)(resources.GetObject("rCostcentre.Buttons10"))))});
            this.rCostcentre.Name = "rCostcentre";
            this.rCostcentre.Click += new System.EventHandler(this.rCostcentre_Click);
            // 
            // colIdentification
            // 
            resources.ApplyResources(this.colIdentification, "colIdentification");
            this.colIdentification.FieldName = "VALUE";
            this.colIdentification.Name = "colIdentification";
            this.colIdentification.OptionsColumn.FixedWidth = true;
            // 
            // colBookingId
            // 
            resources.ApplyResources(this.colBookingId, "colBookingId");
            this.colBookingId.FieldName = "BOOKING_ID";
            this.colBookingId.Name = "colBookingId";
            // 
            // colTDSBookingView
            // 
            resources.ApplyResources(this.colTDSBookingView, "colTDSBookingView");
            this.colTDSBookingView.ColumnEdit = this.rTDSBookingView;
            this.colTDSBookingView.Name = "colTDSBookingView";
            this.colTDSBookingView.OptionsColumn.FixedWidth = true;
            this.colTDSBookingView.OptionsColumn.ShowCaption = false;
            this.colTDSBookingView.OptionsColumn.TabStop = false;
            // 
            // rTDSBookingView
            // 
            resources.ApplyResources(this.rTDSBookingView, "rTDSBookingView");
            this.rTDSBookingView.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rTDSBookingView.Buttons"))), resources.GetString("rTDSBookingView.Buttons1"), ((int)(resources.GetObject("rTDSBookingView.Buttons2"))), ((bool)(resources.GetObject("rTDSBookingView.Buttons3"))), ((bool)(resources.GetObject("rTDSBookingView.Buttons4"))), ((bool)(resources.GetObject("rTDSBookingView.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("rTDSBookingView.Buttons6"))), global::ACPP.Properties.Resources.payment_16, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject7, resources.GetString("rTDSBookingView.Buttons7"), ((object)(resources.GetObject("rTDSBookingView.Buttons8"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("rTDSBookingView.Buttons9"))), ((bool)(resources.GetObject("rTDSBookingView.Buttons10"))))});
            this.rTDSBookingView.Name = "rTDSBookingView";
            this.rTDSBookingView.Click += new System.EventHandler(this.rTDSBookingView_Click);
            // 
            // colReferenceNumber
            // 
            this.colReferenceNumber.AppearanceCell.BackColor = ((System.Drawing.Color)(resources.GetObject("colReferenceNumber.AppearanceCell.BackColor")));
            this.colReferenceNumber.AppearanceCell.Options.UseBackColor = true;
            resources.ApplyResources(this.colReferenceNumber, "colReferenceNumber");
            this.colReferenceNumber.ColumnEdit = this.txtRefferenceNo;
            this.colReferenceNumber.FieldName = "REFERENCE_NUMBER";
            this.colReferenceNumber.Name = "colReferenceNumber";
            // 
            // txtRefferenceNo
            // 
            resources.ApplyResources(this.txtRefferenceNo, "txtRefferenceNo");
            this.txtRefferenceNo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtRefferenceNo.Name = "txtRefferenceNo";
            // 
            // colChequeNo
            // 
            this.colChequeNo.AppearanceCell.BackColor = ((System.Drawing.Color)(resources.GetObject("colChequeNo.AppearanceCell.BackColor")));
            this.colChequeNo.AppearanceCell.Options.UseBackColor = true;
            resources.ApplyResources(this.colChequeNo, "colChequeNo");
            this.colChequeNo.ColumnEdit = this.rtxtChequeNo;
            this.colChequeNo.FieldName = "CHEQUE_NO";
            this.colChequeNo.Name = "colChequeNo";
            // 
            // rtxtChequeNo
            // 
            resources.ApplyResources(this.rtxtChequeNo, "rtxtChequeNo");
            this.rtxtChequeNo.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.rtxtChequeNo.Name = "rtxtChequeNo";
            // 
            // colMaterilizedOn
            // 
            this.colMaterilizedOn.AppearanceCell.BackColor = ((System.Drawing.Color)(resources.GetObject("colMaterilizedOn.AppearanceCell.BackColor")));
            this.colMaterilizedOn.AppearanceCell.Options.UseBackColor = true;
            resources.ApplyResources(this.colMaterilizedOn, "colMaterilizedOn");
            this.colMaterilizedOn.ColumnEdit = this.rdtMaterilizsed;
            this.colMaterilizedOn.FieldName = "MATERIALIZED_ON";
            this.colMaterilizedOn.Name = "colMaterilizedOn";
            this.colMaterilizedOn.UnboundType = DevExpress.Data.UnboundColumnType.DateTime;
            // 
            // rdtMaterilizsed
            // 
            resources.ApplyResources(this.rdtMaterilizsed, "rdtMaterilizsed");
            this.rdtMaterilizsed.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.rdtMaterilizsed.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rdtMaterilizsed.Buttons"))))});
            this.rdtMaterilizsed.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rdtMaterilizsed.CalendarTimeProperties.Buttons"))))});
            this.rdtMaterilizsed.CalendarTimeProperties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("rdtMaterilizsed.CalendarTimeProperties.Mask.MaskType")));
            this.rdtMaterilizsed.CalendarTimeProperties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("rdtMaterilizsed.CalendarTimeProperties.Mask.UseMaskAsDisplayFormat")));
            this.rdtMaterilizsed.Name = "rdtMaterilizsed";
            // 
            // colGSTLedgerClass
            // 
            this.colGSTLedgerClass.AppearanceCell.BackColor = ((System.Drawing.Color)(resources.GetObject("colGSTLedgerClass.AppearanceCell.BackColor")));
            this.colGSTLedgerClass.AppearanceCell.Options.UseBackColor = true;
            resources.ApplyResources(this.colGSTLedgerClass, "colGSTLedgerClass");
            this.colGSTLedgerClass.ColumnEdit = this.rglkpLedgerGST;
            this.colGSTLedgerClass.FieldName = "LEDGER_GST_CLASS_ID";
            this.colGSTLedgerClass.Name = "colGSTLedgerClass";
            this.colGSTLedgerClass.OptionsColumn.AllowMove = false;
            this.colGSTLedgerClass.OptionsColumn.AllowShowHide = false;
            this.colGSTLedgerClass.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colGSTLedgerClass.OptionsColumn.FixedWidth = true;
            this.colGSTLedgerClass.OptionsFilter.AllowAutoFilter = false;
            this.colGSTLedgerClass.OptionsFilter.AllowFilter = false;
            this.colGSTLedgerClass.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colGSTLedgerClass.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colGSTLedgerClass.SortMode = DevExpress.XtraGrid.ColumnSortMode.DisplayText;
            // 
            // rglkpLedgerGST
            // 
            this.rglkpLedgerGST.AppearanceFocused.BackColor2 = ((System.Drawing.Color)(resources.GetObject("rglkpLedgerGST.AppearanceFocused.BackColor2")));
            this.rglkpLedgerGST.AppearanceFocused.BorderColor = ((System.Drawing.Color)(resources.GetObject("rglkpLedgerGST.AppearanceFocused.BorderColor")));
            this.rglkpLedgerGST.AppearanceFocused.Font = ((System.Drawing.Font)(resources.GetObject("rglkpLedgerGST.AppearanceFocused.Font")));
            this.rglkpLedgerGST.AppearanceFocused.Options.UseBorderColor = true;
            this.rglkpLedgerGST.AppearanceFocused.Options.UseFont = true;
            resources.ApplyResources(this.rglkpLedgerGST, "rglkpLedgerGST");
            this.rglkpLedgerGST.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.rglkpLedgerGST.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.rglkpLedgerGST.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rglkpLedgerGST.Buttons"))))});
            this.rglkpLedgerGST.Name = "rglkpLedgerGST";
            this.rglkpLedgerGST.PopupBorderStyle = DevExpress.XtraEditors.Controls.PopupBorderStyles.Style3D;
            this.rglkpLedgerGST.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.rglkpLedgerGST.PopupFormMinSize = new System.Drawing.Size(60, 108);
            this.rglkpLedgerGST.PopupFormSize = new System.Drawing.Size(60, 108);
            this.rglkpLedgerGST.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.rglkpLedgerGST.View = this.gvGSTClassLeddetails;
            this.rglkpLedgerGST.EditValueChanged += new System.EventHandler(this.rglkpLedgerGST_EditValueChanged);
            // 
            // gvGSTClassLeddetails
            // 
            this.gvGSTClassLeddetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colgstId,
            this.colgstPer});
            this.gvGSTClassLeddetails.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gvGSTClassLeddetails.Name = "gvGSTClassLeddetails";
            this.gvGSTClassLeddetails.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvGSTClassLeddetails.OptionsView.ShowColumnHeaders = false;
            this.gvGSTClassLeddetails.OptionsView.ShowGroupPanel = false;
            this.gvGSTClassLeddetails.OptionsView.ShowIndicator = false;
            // 
            // colgstId
            // 
            resources.ApplyResources(this.colgstId, "colgstId");
            this.colgstId.FieldName = "GST_Id";
            this.colgstId.Name = "colgstId";
            // 
            // colgstPer
            // 
            resources.ApplyResources(this.colgstPer, "colgstPer");
            this.colgstPer.FieldName = "GST_NAME";
            this.colgstPer.Name = "colgstPer";
            // 
            // colGStAmt
            // 
            this.colGStAmt.AppearanceCell.BackColor = ((System.Drawing.Color)(resources.GetObject("colGStAmt.AppearanceCell.BackColor")));
            this.colGStAmt.AppearanceCell.Options.UseBackColor = true;
            resources.ApplyResources(this.colGStAmt, "colGStAmt");
            this.colGStAmt.ColumnEdit = this.rtxtGST;
            this.colGStAmt.FieldName = "GST_AMOUNT";
            this.colGStAmt.Name = "colGStAmt";
            this.colGStAmt.OptionsColumn.AllowEdit = false;
            this.colGStAmt.OptionsColumn.AllowFocus = false;
            this.colGStAmt.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colGStAmt.OptionsColumn.AllowMove = false;
            this.colGStAmt.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colGStAmt.OptionsColumn.FixedWidth = true;
            this.colGStAmt.OptionsColumn.ReadOnly = true;
            this.colGStAmt.OptionsFilter.AllowAutoFilter = false;
            this.colGStAmt.OptionsFilter.AllowFilter = false;
            this.colGStAmt.OptionsFilter.ImmediateUpdateAutoFilter = false;
            this.colGStAmt.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowForFocusedCell;
            this.colGStAmt.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("colGStAmt.Summary"))), resources.GetString("colGStAmt.Summary1"), resources.GetString("colGStAmt.Summary2"))});
            // 
            // rtxtGST
            // 
            this.rtxtGST.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("rtxtGST.Appearance.Font")));
            this.rtxtGST.Appearance.ForeColor = ((System.Drawing.Color)(resources.GetObject("rtxtGST.Appearance.ForeColor")));
            this.rtxtGST.Appearance.Options.UseFont = true;
            this.rtxtGST.Appearance.Options.UseForeColor = true;
            this.rtxtGST.AppearanceFocused.BackColor2 = ((System.Drawing.Color)(resources.GetObject("rtxtGST.AppearanceFocused.BackColor2")));
            this.rtxtGST.AppearanceFocused.BorderColor = ((System.Drawing.Color)(resources.GetObject("rtxtGST.AppearanceFocused.BorderColor")));
            this.rtxtGST.AppearanceFocused.Font = ((System.Drawing.Font)(resources.GetObject("rtxtGST.AppearanceFocused.Font")));
            this.rtxtGST.AppearanceFocused.Options.UseBorderColor = true;
            this.rtxtGST.AppearanceFocused.Options.UseFont = true;
            resources.ApplyResources(this.rtxtGST, "rtxtGST");
            this.rtxtGST.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.rtxtGST.Name = "rtxtGST";
            // 
            // colGST
            // 
            resources.ApplyResources(this.colGST, "colGST");
            this.colGST.FieldName = "GST";
            this.colGST.Name = "colGST";
            // 
            // colCGST
            // 
            resources.ApplyResources(this.colCGST, "colCGST");
            this.colCGST.FieldName = "CGST";
            this.colCGST.Name = "colCGST";
            // 
            // colSGST
            // 
            resources.ApplyResources(this.colSGST, "colSGST");
            this.colSGST.FieldName = "SGST";
            this.colSGST.Name = "colSGST";
            // 
            // colIGST
            // 
            resources.ApplyResources(this.colIGST, "colIGST");
            this.colIGST.FieldName = "IGST";
            this.colIGST.Name = "colIGST";
            // 
            // colIsGSTLedger
            // 
            resources.ApplyResources(this.colIsGSTLedger, "colIsGSTLedger");
            this.colIsGSTLedger.FieldName = "IS_GST_LEDGERS";
            this.colIsGSTLedger.Name = "colIsGSTLedger";
            // 
            // colNarration
            // 
            this.colNarration.AppearanceCell.BackColor = ((System.Drawing.Color)(resources.GetObject("colNarration.AppearanceCell.BackColor")));
            this.colNarration.AppearanceCell.Options.UseBackColor = true;
            resources.ApplyResources(this.colNarration, "colNarration");
            this.colNarration.ColumnEdit = this.rtxtNarration;
            this.colNarration.FieldName = "NARRATION";
            this.colNarration.Name = "colNarration";
            // 
            // rtxtNarration
            // 
            resources.ApplyResources(this.rtxtNarration, "rtxtNarration");
            this.rtxtNarration.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.rtxtNarration.MaxLength = 400;
            this.rtxtNarration.Name = "rtxtNarration";
            // 
            // gccolVoucherDate
            // 
            resources.ApplyResources(this.gccolVoucherDate, "gccolVoucherDate");
            this.gccolVoucherDate.FieldName = "VOUCHER_DATE";
            this.gccolVoucherDate.Name = "gccolVoucherDate";
            // 
            // colReferedAmt
            // 
            this.colReferedAmt.AppearanceCell.BackColor = ((System.Drawing.Color)(resources.GetObject("colReferedAmt.AppearanceCell.BackColor")));
            this.colReferedAmt.AppearanceCell.Options.UseBackColor = true;
            resources.ApplyResources(this.colReferedAmt, "colReferedAmt");
            this.colReferedAmt.FieldName = "REF_AMOUNT";
            this.colReferedAmt.Name = "colReferedAmt";
            // 
            // colOldLedgerId
            // 
            this.colOldLedgerId.AppearanceCell.BackColor = ((System.Drawing.Color)(resources.GetObject("colOldLedgerId.AppearanceCell.BackColor")));
            this.colOldLedgerId.AppearanceCell.Options.UseBackColor = true;
            resources.ApplyResources(this.colOldLedgerId, "colOldLedgerId");
            this.colOldLedgerId.FieldName = "TEMP_LEDGER_ID";
            this.colOldLedgerId.Name = "colOldLedgerId";
            // 
            // rglkpSource
            // 
            resources.ApplyResources(this.rglkpSource, "rglkpSource");
            this.rglkpSource.BestFitMode = DevExpress.XtraEditors.Controls.BestFitMode.BestFit;
            this.rglkpSource.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rglkpSource.Buttons"))))});
            this.rglkpSource.DisplayFormat.FormatString = "C2";
            this.rglkpSource.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rglkpSource.Name = "rglkpSource";
            this.rglkpSource.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.rglkpSource.PopupFormSize = new System.Drawing.Size(44, 10);
            this.rglkpSource.ReadOnly = true;
            this.rglkpSource.View = this.gridView4;
            // 
            // gridView4
            // 
            this.gridView4.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colName1});
            this.gridView4.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView4.Name = "gridView4";
            this.gridView4.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView4.OptionsView.ShowColumnHeaders = false;
            this.gridView4.OptionsView.ShowGroupPanel = false;
            this.gridView4.OptionsView.ShowIndicator = false;
            // 
            // colId
            // 
            resources.ApplyResources(this.colId, "colId");
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            // 
            // colName1
            // 
            resources.ApplyResources(this.colName1, "colName1");
            this.colName1.FieldName = "Name";
            this.colName1.Name = "colName1";
            // 
            // txtNarration
            // 
            resources.ApplyResources(this.txtNarration, "txtNarration");
            this.txtNarration.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtNarration.Name = "txtNarration";
            // 
            // btnClose
            // 
            this.btnClose.CausesValidation = false;
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.layoutControl1;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtVoucher
            // 
            resources.ApplyResources(this.txtVoucher, "txtVoucher");
            this.txtVoucher.Name = "txtVoucher";
            this.txtVoucher.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtVoucher.StyleController = this.layoutControl1;
            // 
            // deDate
            // 
            resources.ApplyResources(this.deDate, "deDate");
            this.deDate.EnterMoveNextControl = true;
            this.deDate.Name = "deDate";
            this.deDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deDate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.deDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("deDate.Properties.Buttons"))))});
            this.deDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.deDate.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("deDate.Properties.Mask.MaskType")));
            this.deDate.StyleController = this.layoutControl1;
            this.deDate.EditValueChanged += new System.EventHandler(this.deDate_EditValueChanged);
            this.deDate.Enter += new System.EventHandler(this.deDate_Enter);
            this.deDate.KeyDown += new System.Windows.Forms.KeyEventHandler(this.deDate_KeyDown);
            this.deDate.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.deDate_PreviewKeyDown);
            this.deDate.Validating += new System.ComponentModel.CancelEventHandler(this.deDate_Validating);
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.gbMasterInfo,
            this.layoutControlGroup2,
            this.lblTransactionDate,
            this.emptySpaceItem5,
            this.layoutControlItem5,
            this.lcItemVoucerType,
            this.lcAdditionalLinks,
            this.emptySpaceItem4,
            this.lcCurrencyAmount,
            this.lblDonorCurrency,
            this.lcCurrencyEmptySpace,
            this.lcExchangeRate,
            this.lblCalculatedAmtCaption,
            this.lblCalculatedAmt,
            this.lcActualAmount,
            this.emptySpaceItem7,
            this.lblVoucher,
            this.emptySpaceItem6,
            this.lcCurrency,
            this.lcLiveExchangeRate,
            this.emptySpaceItem8});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1153, 455);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // gbMasterInfo
            // 
            this.gbMasterInfo.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("gbMasterInfo.AppearanceGroup.Font")));
            this.gbMasterInfo.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.gbMasterInfo, "gbMasterInfo");
            this.gbMasterInfo.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.lcSave,
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.lcClose,
            this.layoutControlItem6,
            this.lcVendor,
            this.emptySpaceItem3,
            this.lcRemoveVendorGSTInvoice});
            this.gbMasterInfo.Location = new System.Drawing.Point(0, 89);
            this.gbMasterInfo.Name = "gbMasterInfo";
            this.gbMasterInfo.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.gbMasterInfo.Size = new System.Drawing.Size(1049, 366);
            this.gbMasterInfo.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 5, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcTransaction;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(1037, 284);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // lcSave
            // 
            this.lcSave.Control = this.btnSave;
            resources.ApplyResources(this.lcSave, "lcSave");
            this.lcSave.Location = new System.Drawing.Point(857, 294);
            this.lcSave.MaxSize = new System.Drawing.Size(90, 26);
            this.lcSave.MinSize = new System.Drawing.Size(90, 26);
            this.lcSave.Name = "lcSave";
            this.lcSave.Size = new System.Drawing.Size(90, 26);
            this.lcSave.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcSave.TextSize = new System.Drawing.Size(0, 0);
            this.lcSave.TextToControlDistance = 0;
            this.lcSave.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(469, 284);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(568, 10);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(469, 320);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(568, 10);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcClose
            // 
            this.lcClose.Control = this.btnClose;
            this.lcClose.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            resources.ApplyResources(this.lcClose, "lcClose");
            this.lcClose.Location = new System.Drawing.Point(947, 294);
            this.lcClose.MaxSize = new System.Drawing.Size(90, 26);
            this.lcClose.MinSize = new System.Drawing.Size(90, 26);
            this.lcClose.Name = "lcClose";
            this.lcClose.Size = new System.Drawing.Size(90, 26);
            this.lcClose.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcClose.TextSize = new System.Drawing.Size(0, 0);
            this.lcClose.TextToControlDistance = 0;
            this.lcClose.TextVisible = false;
            this.lcClose.TrimClientAreaToControl = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.txtJNarration;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 284);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 12, 2);
            this.layoutControlItem6.Size = new System.Drawing.Size(469, 46);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(99, 13);
            // 
            // lcVendor
            // 
            this.lcVendor.Control = this.btnVendor;
            resources.ApplyResources(this.lcVendor, "lcVendor");
            this.lcVendor.Location = new System.Drawing.Point(469, 294);
            this.lcVendor.MaxSize = new System.Drawing.Size(166, 26);
            this.lcVendor.MinSize = new System.Drawing.Size(166, 26);
            this.lcVendor.Name = "lcVendor";
            this.lcVendor.Size = new System.Drawing.Size(166, 26);
            this.lcVendor.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcVendor.TextSize = new System.Drawing.Size(0, 0);
            this.lcVendor.TextToControlDistance = 0;
            this.lcVendor.TextVisible = false;
            this.lcVendor.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem3, "emptySpaceItem3");
            this.emptySpaceItem3.Location = new System.Drawing.Point(810, 294);
            this.emptySpaceItem3.MaxSize = new System.Drawing.Size(47, 26);
            this.emptySpaceItem3.MinSize = new System.Drawing.Size(47, 26);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(47, 26);
            this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcRemoveVendorGSTInvoice
            // 
            this.lcRemoveVendorGSTInvoice.Control = this.btnRemoveVendorGSTInvoce;
            resources.ApplyResources(this.lcRemoveVendorGSTInvoice, "lcRemoveVendorGSTInvoice");
            this.lcRemoveVendorGSTInvoice.Location = new System.Drawing.Point(635, 294);
            this.lcRemoveVendorGSTInvoice.MaxSize = new System.Drawing.Size(175, 26);
            this.lcRemoveVendorGSTInvoice.MinSize = new System.Drawing.Size(175, 26);
            this.lcRemoveVendorGSTInvoice.Name = "lcRemoveVendorGSTInvoice";
            this.lcRemoveVendorGSTInvoice.Size = new System.Drawing.Size(175, 26);
            this.lcRemoveVendorGSTInvoice.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcRemoveVendorGSTInvoice.TextSize = new System.Drawing.Size(0, 0);
            this.lcRemoveVendorGSTInvoice.TextToControlDistance = 0;
            this.lcRemoveVendorGSTInvoice.TextVisible = false;
            this.lcRemoveVendorGSTInvoice.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlGroup2
            // 
            resources.ApplyResources(this.layoutControlGroup2, "layoutControlGroup2");
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem3});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup2.Size = new System.Drawing.Size(1049, 35);
            this.layoutControlGroup2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.ucCaptionPanel;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(1043, 29);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // lblTransactionDate
            // 
            this.lblTransactionDate.Control = this.deDate;
            this.lblTransactionDate.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            resources.ApplyResources(this.lblTransactionDate, "lblTransactionDate");
            this.lblTransactionDate.Location = new System.Drawing.Point(0, 35);
            this.lblTransactionDate.MaxSize = new System.Drawing.Size(119, 30);
            this.lblTransactionDate.MinSize = new System.Drawing.Size(119, 30);
            this.lblTransactionDate.Name = "lblTransactionDate";
            this.lblTransactionDate.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 3, 2);
            this.lblTransactionDate.Size = new System.Drawing.Size(119, 30);
            this.lblTransactionDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblTransactionDate.Spacing = new DevExpress.XtraLayout.Utils.Padding(4, 0, 0, 3);
            this.lblTransactionDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblTransactionDate.TextSize = new System.Drawing.Size(23, 13);
            this.lblTransactionDate.TextToControlDistance = 5;
            this.lblTransactionDate.TrimClientAreaToControl = false;
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem5, "emptySpaceItem5");
            this.emptySpaceItem5.Location = new System.Drawing.Point(806, 35);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(24, 30);
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.ucJournalShortcut;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(1049, 0);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(104, 455);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // lcItemVoucerType
            // 
            this.lcItemVoucerType.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lcItemVoucerType.AppearanceItemCaption.Font")));
            this.lcItemVoucerType.AppearanceItemCaption.Options.UseFont = true;
            this.lcItemVoucerType.Control = this.glkpVoucherType;
            this.lcItemVoucerType.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            resources.ApplyResources(this.lcItemVoucerType, "lcItemVoucerType");
            this.lcItemVoucerType.Location = new System.Drawing.Point(119, 35);
            this.lcItemVoucerType.MaxSize = new System.Drawing.Size(251, 30);
            this.lcItemVoucerType.MinSize = new System.Drawing.Size(251, 30);
            this.lcItemVoucerType.Name = "lcItemVoucerType";
            this.lcItemVoucerType.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 2, 2, 2);
            this.lcItemVoucerType.Size = new System.Drawing.Size(251, 30);
            this.lcItemVoucerType.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcItemVoucerType.TextSize = new System.Drawing.Size(99, 13);
            this.lcItemVoucerType.TrimClientAreaToControl = false;
            // 
            // lcAdditionalLinks
            // 
            this.lcAdditionalLinks.Control = this.ucAdditionalInfo;
            resources.ApplyResources(this.lcAdditionalLinks, "lcAdditionalLinks");
            this.lcAdditionalLinks.Location = new System.Drawing.Point(583, 35);
            this.lcAdditionalLinks.MaxSize = new System.Drawing.Size(223, 30);
            this.lcAdditionalLinks.MinSize = new System.Drawing.Size(223, 30);
            this.lcAdditionalLinks.Name = "lcAdditionalLinks";
            this.lcAdditionalLinks.Size = new System.Drawing.Size(223, 30);
            this.lcAdditionalLinks.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcAdditionalLinks.TextSize = new System.Drawing.Size(0, 0);
            this.lcAdditionalLinks.TextToControlDistance = 0;
            this.lcAdditionalLinks.TextVisible = false;
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem4, "emptySpaceItem4");
            this.emptySpaceItem4.Location = new System.Drawing.Point(548, 35);
            this.emptySpaceItem4.MaxSize = new System.Drawing.Size(35, 30);
            this.emptySpaceItem4.MinSize = new System.Drawing.Size(35, 30);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(35, 30);
            this.emptySpaceItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcCurrencyAmount
            // 
            this.lcCurrencyAmount.Control = this.txtCurrencyAmount;
            resources.ApplyResources(this.lcCurrencyAmount, "lcCurrencyAmount");
            this.lcCurrencyAmount.Location = new System.Drawing.Point(200, 65);
            this.lcCurrencyAmount.MaxSize = new System.Drawing.Size(123, 24);
            this.lcCurrencyAmount.MinSize = new System.Drawing.Size(123, 24);
            this.lcCurrencyAmount.Name = "lcCurrencyAmount";
            this.lcCurrencyAmount.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 2, 2, 2);
            this.lcCurrencyAmount.Size = new System.Drawing.Size(123, 24);
            this.lcCurrencyAmount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcCurrencyAmount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lcCurrencyAmount.TextSize = new System.Drawing.Size(37, 13);
            this.lcCurrencyAmount.TextToControlDistance = 5;
            // 
            // lblDonorCurrency
            // 
            this.lblDonorCurrency.AllowHotTrack = false;
            resources.ApplyResources(this.lblDonorCurrency, "lblDonorCurrency");
            this.lblDonorCurrency.Location = new System.Drawing.Point(323, 65);
            this.lblDonorCurrency.MaxSize = new System.Drawing.Size(46, 24);
            this.lblDonorCurrency.MinSize = new System.Drawing.Size(46, 24);
            this.lblDonorCurrency.Name = "lblDonorCurrency";
            this.lblDonorCurrency.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 2, 2, 2);
            this.lblDonorCurrency.Size = new System.Drawing.Size(46, 24);
            this.lblDonorCurrency.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblDonorCurrency.TextSize = new System.Drawing.Size(99, 13);
            // 
            // lcCurrencyEmptySpace
            // 
            this.lcCurrencyEmptySpace.AllowHotTrack = false;
            resources.ApplyResources(this.lcCurrencyEmptySpace, "lcCurrencyEmptySpace");
            this.lcCurrencyEmptySpace.Location = new System.Drawing.Point(1029, 65);
            this.lcCurrencyEmptySpace.Name = "lcCurrencyEmptySpace";
            this.lcCurrencyEmptySpace.Size = new System.Drawing.Size(20, 24);
            this.lcCurrencyEmptySpace.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcExchangeRate
            // 
            this.lcExchangeRate.Control = this.txtExchangeRate;
            resources.ApplyResources(this.lcExchangeRate, "lcExchangeRate");
            this.lcExchangeRate.Location = new System.Drawing.Point(369, 65);
            this.lcExchangeRate.MaxSize = new System.Drawing.Size(135, 24);
            this.lcExchangeRate.MinSize = new System.Drawing.Size(135, 24);
            this.lcExchangeRate.Name = "lcExchangeRate";
            this.lcExchangeRate.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 2, 2, 2);
            this.lcExchangeRate.Size = new System.Drawing.Size(135, 24);
            this.lcExchangeRate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcExchangeRate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lcExchangeRate.TextSize = new System.Drawing.Size(73, 13);
            this.lcExchangeRate.TextToControlDistance = 5;
            // 
            // lblCalculatedAmtCaption
            // 
            this.lblCalculatedAmtCaption.AllowHotTrack = false;
            resources.ApplyResources(this.lblCalculatedAmtCaption, "lblCalculatedAmtCaption");
            this.lblCalculatedAmtCaption.Location = new System.Drawing.Point(642, 65);
            this.lblCalculatedAmtCaption.MaxSize = new System.Drawing.Size(99, 24);
            this.lblCalculatedAmtCaption.MinSize = new System.Drawing.Size(99, 24);
            this.lblCalculatedAmtCaption.Name = "lblCalculatedAmtCaption";
            this.lblCalculatedAmtCaption.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 2, 2, 2);
            this.lblCalculatedAmtCaption.Size = new System.Drawing.Size(99, 24);
            this.lblCalculatedAmtCaption.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblCalculatedAmtCaption.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblCalculatedAmtCaption.TextSize = new System.Drawing.Size(90, 13);
            // 
            // lblCalculatedAmt
            // 
            this.lblCalculatedAmt.AllowHotTrack = false;
            this.lblCalculatedAmt.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblCalculatedAmt.AppearanceItemCaption.Font")));
            this.lblCalculatedAmt.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblCalculatedAmt, "lblCalculatedAmt");
            this.lblCalculatedAmt.Location = new System.Drawing.Point(741, 65);
            this.lblCalculatedAmt.MinSize = new System.Drawing.Size(50, 17);
            this.lblCalculatedAmt.Name = "lblCalculatedAmt";
            this.lblCalculatedAmt.Size = new System.Drawing.Size(89, 24);
            this.lblCalculatedAmt.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblCalculatedAmt.TextSize = new System.Drawing.Size(99, 13);
            // 
            // lcActualAmount
            // 
            this.lcActualAmount.Control = this.txtActualAmount;
            resources.ApplyResources(this.lcActualAmount, "lcActualAmount");
            this.lcActualAmount.Location = new System.Drawing.Point(830, 65);
            this.lcActualAmount.MaxSize = new System.Drawing.Size(174, 24);
            this.lcActualAmount.MinSize = new System.Drawing.Size(174, 24);
            this.lcActualAmount.Name = "lcActualAmount";
            this.lcActualAmount.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 2, 2);
            this.lcActualAmount.Size = new System.Drawing.Size(199, 24);
            this.lcActualAmount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcActualAmount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lcActualAmount.TextSize = new System.Drawing.Size(70, 13);
            this.lcActualAmount.TextToControlDistance = 5;
            // 
            // emptySpaceItem7
            // 
            this.emptySpaceItem7.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem7, "emptySpaceItem7");
            this.emptySpaceItem7.Location = new System.Drawing.Point(1029, 35);
            this.emptySpaceItem7.MinSize = new System.Drawing.Size(20, 24);
            this.emptySpaceItem7.Name = "emptySpaceItem7";
            this.emptySpaceItem7.Size = new System.Drawing.Size(20, 30);
            this.emptySpaceItem7.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem7.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblVoucher
            // 
            this.lblVoucher.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lblVoucher.AppearanceItemCaption.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Far;
            this.lblVoucher.Control = this.txtVoucher;
            this.lblVoucher.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            resources.ApplyResources(this.lblVoucher, "lblVoucher");
            this.lblVoucher.Location = new System.Drawing.Point(370, 35);
            this.lblVoucher.MaxSize = new System.Drawing.Size(178, 30);
            this.lblVoucher.MinSize = new System.Drawing.Size(178, 30);
            this.lblVoucher.Name = "lblVoucher";
            this.lblVoucher.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 2, 2, 2);
            this.lblVoucher.Size = new System.Drawing.Size(178, 30);
            this.lblVoucher.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblVoucher.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 4, 0, 3);
            this.lblVoucher.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblVoucher.TextSize = new System.Drawing.Size(50, 13);
            this.lblVoucher.TextToControlDistance = 5;
            this.lblVoucher.TrimClientAreaToControl = false;
            // 
            // emptySpaceItem6
            // 
            this.emptySpaceItem6.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem6, "emptySpaceItem6");
            this.emptySpaceItem6.Location = new System.Drawing.Point(830, 35);
            this.emptySpaceItem6.MaxSize = new System.Drawing.Size(199, 30);
            this.emptySpaceItem6.MinSize = new System.Drawing.Size(199, 30);
            this.emptySpaceItem6.Name = "emptySpaceItem6";
            this.emptySpaceItem6.Size = new System.Drawing.Size(199, 30);
            this.emptySpaceItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem6.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcCurrency
            // 
            this.lcCurrency.Control = this.glkpCurrencyCountry;
            resources.ApplyResources(this.lcCurrency, "lcCurrency");
            this.lcCurrency.Location = new System.Drawing.Point(0, 65);
            this.lcCurrency.MaxSize = new System.Drawing.Size(200, 24);
            this.lcCurrency.MinSize = new System.Drawing.Size(200, 24);
            this.lcCurrency.Name = "lcCurrency";
            this.lcCurrency.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 2, 2, 2);
            this.lcCurrency.Size = new System.Drawing.Size(200, 24);
            this.lcCurrency.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcCurrency.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lcCurrency.TextSize = new System.Drawing.Size(44, 13);
            this.lcCurrency.TextToControlDistance = 5;
            // 
            // lcLiveExchangeRate
            // 
            this.lcLiveExchangeRate.Control = this.lblLiveExchangeRate;
            this.lcLiveExchangeRate.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            resources.ApplyResources(this.lcLiveExchangeRate, "lcLiveExchangeRate");
            this.lcLiveExchangeRate.Location = new System.Drawing.Point(504, 65);
            this.lcLiveExchangeRate.Name = "lcLiveExchangeRate";
            this.lcLiveExchangeRate.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 2, 2, 2);
            this.lcLiveExchangeRate.Size = new System.Drawing.Size(133, 24);
            this.lcLiveExchangeRate.TextSize = new System.Drawing.Size(99, 13);
            this.lcLiveExchangeRate.TrimClientAreaToControl = false;
            // 
            // emptySpaceItem8
            // 
            this.emptySpaceItem8.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem8, "emptySpaceItem8");
            this.emptySpaceItem8.Location = new System.Drawing.Point(637, 65);
            this.emptySpaceItem8.MinSize = new System.Drawing.Size(5, 24);
            this.emptySpaceItem8.Name = "emptySpaceItem8";
            this.emptySpaceItem8.Size = new System.Drawing.Size(5, 24);
            this.emptySpaceItem8.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem8.TextSize = new System.Drawing.Size(0, 0);
            // 
            // barBottomShortcut
            // 
            this.barBottomShortcut.BarName = "Bottom ShortCut";
            this.barBottomShortcut.DockCol = 0;
            this.barBottomShortcut.DockRow = 0;
            this.barBottomShortcut.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.barBottomShortcut.FloatLocation = new System.Drawing.Point(4, 573);
            this.barBottomShortcut.OptionsBar.AllowCollapse = true;
            this.barBottomShortcut.OptionsBar.AllowQuickCustomization = false;
            this.barBottomShortcut.OptionsBar.DisableClose = true;
            this.barBottomShortcut.OptionsBar.DisableCustomization = true;
            this.barBottomShortcut.OptionsBar.DrawBorder = false;
            this.barBottomShortcut.OptionsBar.MultiLine = true;
            this.barBottomShortcut.OptionsBar.UseWholeRow = true;
            resources.ApplyResources(this.barBottomShortcut, "barBottomShortcut");
            // 
            // bar1
            // 
            this.bar1.BarName = "Bottom ShortCut";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Bottom;
            this.bar1.FloatLocation = new System.Drawing.Point(4, 573);
            this.bar1.OptionsBar.AllowCollapse = true;
            this.bar1.OptionsBar.AllowQuickCustomization = false;
            this.bar1.OptionsBar.DisableClose = true;
            this.bar1.OptionsBar.DisableCustomization = true;
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.OptionsBar.MultiLine = true;
            this.bar1.OptionsBar.UseWholeRow = true;
            resources.ApplyResources(this.bar1, "bar1");
            // 
            // JournalAdd
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.CancelButton = this.btnClose;
            resources.ApplyResources(this, "$this");
            this.Controls.Add(this.pnlFill);
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "JournalAdd";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.JournalAdd_FormClosing);
            this.Load += new System.EventHandler(this.frmTransactionMultiAdd_Load);
            this.Shown += new System.EventHandler(this.JournalAdd_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.JournalAdd_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pnlFill)).EndInit();
            this.pnlFill.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.glkpCurrencyCountry.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCurrencyCountry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtActualAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtExchangeRate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCurrencyAmount.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.barManager1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpVoucherType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit2View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtJNarration.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTransaction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTransaction)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtDebit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCredit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtnDelete)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rCostcentre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rTDSBookingView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtRefferenceNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtChequeNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdtMaterilizsed.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rdtMaterilizsed)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpLedgerGST)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvGSTClassLeddetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtGST)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtNarration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNarration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVoucher.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gbMasterInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcSave)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcClose)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcVendor)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcRemoveVendorGSTInvoice)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblTransactionDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcItemVoucerType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcAdditionalLinks)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCurrencyAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDonorCurrency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCurrencyEmptySpace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcExchangeRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCalculatedAmtCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCalculatedAmt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcActualAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblVoucher)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCurrency)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcLiveExchangeRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem8)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlFill;
        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.TextEdit txtVoucher;
        private DevExpress.XtraEditors.DateEdit deDate;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup gbMasterInfo;
        private DevExpress.XtraLayout.LayoutControlItem lblTransactionDate;
        private DevExpress.XtraLayout.LayoutControlItem lblVoucher;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraLayout.LayoutControlItem lcSave;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraLayout.LayoutControlItem lcClose;
        private DevExpress.XtraGrid.GridControl gcTransaction;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTransaction;
        private DevExpress.XtraGrid.Columns.GridColumn colDebit;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit rglkpSource;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView4;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colName1;
        private DevExpress.XtraGrid.Columns.GridColumn colLedger;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit rglkpLedger;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerName;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtDebit;
        private DevExpress.XtraGrid.Columns.GridColumn colLedCode;
        private DevExpress.XtraGrid.Columns.GridColumn colCredit;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit txtCredit;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit txtNarration;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraGrid.Columns.GridColumn colAction;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbtnDelete;
        private UIControls.UcCaptionPanel ucCaptionPanel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private UIControls.ucVoucherShortcut ucJournalShortcut;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraBars.Bar barBottomShortcut;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraBars.BarManager barManager1;
        private DevExpress.XtraBars.Bar bar2;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
        private DevExpress.XtraBars.BarButtonItem bbiDeleteTrans;
        private DevExpress.XtraBars.BarButtonItem bbiMoveTransaction;
        private DevExpress.XtraBars.BarButtonItem barButtonItem3;
        private DevExpress.XtraBars.BarButtonItem bbiNewTrans;
        private DevExpress.XtraBars.BarButtonItem barButtonItem5;
        private DevExpress.XtraEditors.TextEdit txtJNarration;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraGrid.Columns.GridColumn colCostcentre;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rCostcentre;
        private DevExpress.XtraGrid.Columns.GridColumn colIdentification;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerBal;
        private DevExpress.XtraGrid.Columns.GridColumn colBookingId;
        private DevExpress.XtraGrid.Columns.GridColumn colTDSBookingView;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rTDSBookingView;
        private DevExpress.XtraGrid.Columns.GridColumn colNarration;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtNarration;
        private DevExpress.XtraGrid.Columns.GridColumn colReferenceNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colReferedAmt;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit txtRefferenceNo;
        private DevExpress.XtraGrid.Columns.GridColumn colOldLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn gccolVoucherDate;
        private DevExpress.XtraEditors.GridLookUpEdit glkpVoucherType;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit2View;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherId;
        private DevExpress.XtraGrid.Columns.GridColumn colVOUCHERTYPE;
        private DevExpress.XtraLayout.LayoutControlItem lcItemVoucerType;
        private DevExpress.XtraGrid.Columns.GridColumn colGSTLedgerClass;
        private DevExpress.XtraGrid.Views.Grid.GridView gvGSTClassLeddetails;
        public DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit rglkpLedgerGST;
        private DevExpress.XtraGrid.Columns.GridColumn colgstId;
        private DevExpress.XtraGrid.Columns.GridColumn colgstPer;
        private DevExpress.XtraGrid.Columns.GridColumn colGStAmt;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtGST;
        private DevExpress.XtraGrid.Columns.GridColumn colGST;
        private DevExpress.XtraGrid.Columns.GridColumn colCGST;
        private DevExpress.XtraGrid.Columns.GridColumn colSGST;
        private DevExpress.XtraGrid.Columns.GridColumn colIGST;
        private DevExpress.XtraEditors.SimpleButton btnVendor;
        private DevExpress.XtraLayout.LayoutControlItem lcVendor;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraEditors.SimpleButton btnRemoveVendorGSTInvoce;
        private DevExpress.XtraLayout.LayoutControlItem lcRemoveVendorGSTInvoice;
        private DevExpress.XtraGrid.Columns.GridColumn colIsGSTLedger;
        private UIControls.ucAdditionalInfoMenu ucAdditionalInfo;
        private DevExpress.XtraLayout.LayoutControlItem lcAdditionalLinks;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraEditors.TextEdit txtCurrencyAmount;
        private DevExpress.XtraLayout.LayoutControlItem lcCurrencyAmount;
        private DevExpress.XtraLayout.SimpleLabelItem lblDonorCurrency;
        private DevExpress.XtraLayout.EmptySpaceItem lcCurrencyEmptySpace;
        private DevExpress.XtraEditors.TextEdit txtExchangeRate;
        private DevExpress.XtraLayout.LayoutControlItem lcExchangeRate;
        private DevExpress.XtraLayout.SimpleLabelItem lblCalculatedAmtCaption;
        private DevExpress.XtraLayout.SimpleLabelItem lblCalculatedAmt;
        private DevExpress.XtraEditors.TextEdit txtActualAmount;
        private DevExpress.XtraLayout.LayoutControlItem lcActualAmount;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem7;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem6;
        private DevExpress.XtraEditors.GridLookUpEdit glkpCurrencyCountry;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCurrencyCountry;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencyCountryId;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencyName;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencySymbol;
        private DevExpress.XtraLayout.LayoutControlItem lcCurrency;
        private DevExpress.XtraEditors.LabelControl lblLiveExchangeRate;
        private DevExpress.XtraLayout.LayoutControlItem lcLiveExchangeRate;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem8;
        private DevExpress.XtraGrid.Columns.GridColumn colChequeNo;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtChequeNo;
        private DevExpress.XtraGrid.Columns.GridColumn colMaterilizedOn;
        private DevExpress.XtraEditors.Repository.RepositoryItemDateEdit rdtMaterilizsed;
    }
}