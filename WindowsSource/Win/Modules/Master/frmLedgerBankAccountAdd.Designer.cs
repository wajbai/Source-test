namespace ACPP.Modules.Master
{
    partial class frmLedgerBankAccountAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLedgerBankAccountAdd));
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.lcLedgerAccount = new DevExpress.XtraLayout.LayoutControl();
            this.txtOPExchangeRate = new DevExpress.XtraEditors.TextEdit();
            this.lblCurrencyName = new System.Windows.Forms.Label();
            this.lblCurrencySymbol = new DevExpress.XtraEditors.LabelControl();
            this.lblSymbol = new DevExpress.XtraEditors.LabelControl();
            this.glkpCurrencyCountry = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gvCurrencyCountry = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCurrencyCountryId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrency = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrencyName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCurrencySymbol = new DevExpress.XtraGrid.Columns.GridColumn();
            this.glkpInvestmentType = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gvInvestmentType = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcInvestmentTypeId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcInvestmentType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.glkpHOLedger = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gvHO = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colHeadOfficeLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colHOLedgerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.deLedgerDateClosed = new DevExpress.XtraEditors.DateEdit();
            this.ucLedgerUsedCodes = new ACPP.Modules.UIControls.UcUsedCodesIcon();
            this.btnTDSLedger = new DevExpress.XtraEditors.SimpleButton();
            this.chkTDSLedgers = new DevExpress.XtraEditors.CheckEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.UcMappingLedger = new ACPP.Modules.UIControls.UcAccountMapping();
            this.chkBankInterestLedger = new DevExpress.XtraEditors.CheckEdit();
            this.grdLedgerType = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.mtxtNotes = new DevExpress.XtraEditors.MemoEdit();
            this.glkpLedgerCodes = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit2View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.chkIncludeCostCenter = new DevExpress.XtraEditors.CheckEdit();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.lkpGroup = new DevExpress.XtraEditors.LookUpEdit();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem13 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem14 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblGroup = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblCode = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcgNotes = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcgLedgerType = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcLedgerCloseDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lciHOLedger = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcInvestmentType = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcgCurrencyDetails = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem10 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem12 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem11 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcOpeningAvgExchangeRateCaption = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lcCurrencyNote = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lcExchangeRate = new DevExpress.XtraLayout.LayoutControlItem();
            this.LayGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            ((System.ComponentModel.ISupportInitialize)(this.lcLedgerAccount)).BeginInit();
            this.lcLedgerAccount.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtOPExchangeRate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpCurrencyCountry.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCurrencyCountry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpInvestmentType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInvestmentType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpHOLedger.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHO)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deLedgerDateClosed.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deLedgerDateClosed.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTDSLedgers.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBankInterestLedger.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLedgerType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtxtNotes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpLedgerCodes.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit2View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIncludeCostCenter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgNotes)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgLedgerType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcLedgerCloseDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciHOLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcInvestmentType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgCurrencyDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOpeningAvgExchangeRateCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCurrencyNote)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcExchangeRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.LayGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.StyleController = this.lcLedgerAccount;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lcLedgerAccount
            // 
            this.lcLedgerAccount.AllowCustomizationMenu = false;
            this.lcLedgerAccount.Controls.Add(this.txtOPExchangeRate);
            this.lcLedgerAccount.Controls.Add(this.lblCurrencyName);
            this.lcLedgerAccount.Controls.Add(this.lblCurrencySymbol);
            this.lcLedgerAccount.Controls.Add(this.lblSymbol);
            this.lcLedgerAccount.Controls.Add(this.glkpCurrencyCountry);
            this.lcLedgerAccount.Controls.Add(this.glkpInvestmentType);
            this.lcLedgerAccount.Controls.Add(this.glkpHOLedger);
            this.lcLedgerAccount.Controls.Add(this.deLedgerDateClosed);
            this.lcLedgerAccount.Controls.Add(this.ucLedgerUsedCodes);
            this.lcLedgerAccount.Controls.Add(this.btnTDSLedger);
            this.lcLedgerAccount.Controls.Add(this.chkTDSLedgers);
            this.lcLedgerAccount.Controls.Add(this.txtName);
            this.lcLedgerAccount.Controls.Add(this.UcMappingLedger);
            this.lcLedgerAccount.Controls.Add(this.chkBankInterestLedger);
            this.lcLedgerAccount.Controls.Add(this.grdLedgerType);
            this.lcLedgerAccount.Controls.Add(this.mtxtNotes);
            this.lcLedgerAccount.Controls.Add(this.glkpLedgerCodes);
            this.lcLedgerAccount.Controls.Add(this.btnSave);
            this.lcLedgerAccount.Controls.Add(this.chkIncludeCostCenter);
            this.lcLedgerAccount.Controls.Add(this.btnClose);
            this.lcLedgerAccount.Controls.Add(this.lkpGroup);
            this.lcLedgerAccount.Controls.Add(this.txtCode);
            resources.ApplyResources(this.lcLedgerAccount, "lcLedgerAccount");
            this.lcLedgerAccount.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5});
            this.lcLedgerAccount.Name = "lcLedgerAccount";
            this.lcLedgerAccount.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(660, 222, 298, 409);
            this.lcLedgerAccount.OptionsView.UseDefaultDragAndDropRendering = false;
            this.lcLedgerAccount.Root = this.layoutControlGroup1;
            // 
            // txtOPExchangeRate
            // 
            resources.ApplyResources(this.txtOPExchangeRate, "txtOPExchangeRate");
            this.txtOPExchangeRate.EnterMoveNextControl = true;
            this.txtOPExchangeRate.Name = "txtOPExchangeRate";
            this.txtOPExchangeRate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtOPExchangeRate.Properties.Mask.EditMask = resources.GetString("txtOPExchangeRate.Properties.Mask.EditMask");
            this.txtOPExchangeRate.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("txtOPExchangeRate.Properties.Mask.MaskType")));
            this.txtOPExchangeRate.StyleController = this.lcLedgerAccount;
            // 
            // lblCurrencyName
            // 
            resources.ApplyResources(this.lblCurrencyName, "lblCurrencyName");
            this.lblCurrencyName.Name = "lblCurrencyName";
            // 
            // lblCurrencySymbol
            // 
            this.lblCurrencySymbol.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblCurrencySymbol.Appearance.Font")));
            this.lblCurrencySymbol.Appearance.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Near;
            resources.ApplyResources(this.lblCurrencySymbol, "lblCurrencySymbol");
            this.lblCurrencySymbol.Name = "lblCurrencySymbol";
            this.lblCurrencySymbol.StyleController = this.lcLedgerAccount;
            // 
            // lblSymbol
            // 
            this.lblSymbol.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblSymbol.Appearance.Font")));
            resources.ApplyResources(this.lblSymbol, "lblSymbol");
            this.lblSymbol.Name = "lblSymbol";
            this.lblSymbol.StyleController = this.lcLedgerAccount;
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
            this.glkpCurrencyCountry.Properties.PopupFormMinSize = new System.Drawing.Size(170, 0);
            this.glkpCurrencyCountry.Properties.PopupFormSize = new System.Drawing.Size(170, 50);
            this.glkpCurrencyCountry.Properties.View = this.gvCurrencyCountry;
            this.glkpCurrencyCountry.StyleController = this.lcLedgerAccount;
            this.glkpCurrencyCountry.EditValueChanged += new System.EventHandler(this.glkpCurrencyCountry_EditValueChanged);
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
            // glkpInvestmentType
            // 
            resources.ApplyResources(this.glkpInvestmentType, "glkpInvestmentType");
            this.glkpInvestmentType.Name = "glkpInvestmentType";
            this.glkpInvestmentType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpInvestmentType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpInvestmentType.Properties.Buttons"))))});
            this.glkpInvestmentType.Properties.DisplayMember = "HEADOFFICELEDGER";
            this.glkpInvestmentType.Properties.NullText = resources.GetString("glkpInvestmentType.Properties.NullText");
            this.glkpInvestmentType.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.glkpInvestmentType.Properties.PopupFormSize = new System.Drawing.Size(320, 180);
            this.glkpInvestmentType.Properties.ValueMember = "HEADOFFICE_LEDGER_ID";
            this.glkpInvestmentType.Properties.View = this.gvInvestmentType;
            this.glkpInvestmentType.StyleController = this.lcLedgerAccount;
            // 
            // gvInvestmentType
            // 
            this.gvInvestmentType.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcInvestmentTypeId,
            this.gcInvestmentType});
            this.gvInvestmentType.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gvInvestmentType.Name = "gvInvestmentType";
            this.gvInvestmentType.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvInvestmentType.OptionsView.ShowColumnHeaders = false;
            this.gvInvestmentType.OptionsView.ShowGroupPanel = false;
            this.gvInvestmentType.OptionsView.ShowIndicator = false;
            // 
            // gcInvestmentTypeId
            // 
            resources.ApplyResources(this.gcInvestmentTypeId, "gcInvestmentTypeId");
            this.gcInvestmentTypeId.FieldName = "INVESTMENT_ID";
            this.gcInvestmentTypeId.Name = "gcInvestmentTypeId";
            // 
            // gcInvestmentType
            // 
            resources.ApplyResources(this.gcInvestmentType, "gcInvestmentType");
            this.gcInvestmentType.FieldName = "INVESTMENT_TYPE";
            this.gcInvestmentType.Name = "gcInvestmentType";
            // 
            // glkpHOLedger
            // 
            resources.ApplyResources(this.glkpHOLedger, "glkpHOLedger");
            this.glkpHOLedger.Name = "glkpHOLedger";
            this.glkpHOLedger.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpHOLedger.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpHOLedger.Properties.Buttons"))))});
            this.glkpHOLedger.Properties.DisplayMember = "HEADOFFICELEDGER";
            this.glkpHOLedger.Properties.NullText = resources.GetString("glkpHOLedger.Properties.NullText");
            this.glkpHOLedger.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.glkpHOLedger.Properties.PopupFormSize = new System.Drawing.Size(320, 180);
            this.glkpHOLedger.Properties.ValueMember = "HEADOFFICE_LEDGER_ID";
            this.glkpHOLedger.Properties.View = this.gvHO;
            this.glkpHOLedger.StyleController = this.lcLedgerAccount;
            // 
            // gvHO
            // 
            this.gvHO.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colHeadOfficeLedgerId,
            this.colHOLedgerName});
            this.gvHO.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gvHO.Name = "gvHO";
            this.gvHO.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvHO.OptionsView.ShowColumnHeaders = false;
            this.gvHO.OptionsView.ShowGroupPanel = false;
            this.gvHO.OptionsView.ShowIndicator = false;
            // 
            // colHeadOfficeLedgerId
            // 
            resources.ApplyResources(this.colHeadOfficeLedgerId, "colHeadOfficeLedgerId");
            this.colHeadOfficeLedgerId.FieldName = "HEADOFFICE_LEDGER_ID";
            this.colHeadOfficeLedgerId.Name = "colHeadOfficeLedgerId";
            // 
            // colHOLedgerName
            // 
            resources.ApplyResources(this.colHOLedgerName, "colHOLedgerName");
            this.colHOLedgerName.FieldName = "HEADOFFICELEDGER";
            this.colHOLedgerName.Name = "colHOLedgerName";
            // 
            // deLedgerDateClosed
            // 
            resources.ApplyResources(this.deLedgerDateClosed, "deLedgerDateClosed");
            this.deLedgerDateClosed.Name = "deLedgerDateClosed";
            this.deLedgerDateClosed.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.deLedgerDateClosed.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("deLedgerDateClosed.Properties.Buttons"))))});
            this.deLedgerDateClosed.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("deLedgerDateClosed.Properties.CalendarTimeProperties.Buttons"))))});
            this.deLedgerDateClosed.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("deLedgerDateClosed.Properties.Mask.MaskType")));
            this.deLedgerDateClosed.StyleController = this.lcLedgerAccount;
            // 
            // ucLedgerUsedCodes
            // 
            this.ucLedgerUsedCodes.ExistUsedCode = "";
            resources.ApplyResources(this.ucLedgerUsedCodes, "ucLedgerUsedCodes");
            this.ucLedgerUsedCodes.Name = "ucLedgerUsedCodes";
            this.ucLedgerUsedCodes.Iconclicked += new System.EventHandler(this.ucLedgerUsedCodes_Iconclicked);
            this.ucLedgerUsedCodes.Click += new System.EventHandler(this.ucLedgerUsedCodes_Click);
            // 
            // btnTDSLedger
            // 
            resources.ApplyResources(this.btnTDSLedger, "btnTDSLedger");
            this.btnTDSLedger.Image = global::ACPP.Properties.Resources.TaxPolicy;
            this.btnTDSLedger.Name = "btnTDSLedger";
            this.btnTDSLedger.StyleController = this.lcLedgerAccount;
            this.btnTDSLedger.Click += new System.EventHandler(this.btnTDSLedger_Click);
            // 
            // chkTDSLedgers
            // 
            resources.ApplyResources(this.chkTDSLedgers, "chkTDSLedgers");
            this.chkTDSLedgers.Name = "chkTDSLedgers";
            this.chkTDSLedgers.Properties.Caption = resources.GetString("chkTDSLedgers.Properties.Caption");
            this.chkTDSLedgers.StyleController = this.lcLedgerAccount;
            this.chkTDSLedgers.CheckedChanged += new System.EventHandler(this.chkTDSLedgers_CheckedChanged);
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            this.txtName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtName.Properties.MaxLength = 100;
            this.txtName.StyleController = this.lcLedgerAccount;
            this.txtName.Leave += new System.EventHandler(this.txtName_Leave);
            // 
            // UcMappingLedger
            // 
            this.UcMappingLedger.ActiveMappId = 0;
            this.UcMappingLedger.ActiveMappName = "";
            this.UcMappingLedger.FDAccountID = 0;
            this.UcMappingLedger.FDLedgerSubType = Bosco.Utility.ledgerSubType.CA;
            this.UcMappingLedger.FormType = Bosco.Utility.MapForm.Ledger;
            this.UcMappingLedger.Id = 0;
            resources.ApplyResources(this.UcMappingLedger, "UcMappingLedger");
            this.UcMappingLedger.Name = "UcMappingLedger";
            this.UcMappingLedger.ProjectId = 0;
            this.UcMappingLedger.RefreshGrid = false;
            this.UcMappingLedger.ShowApplicableOption = false;
            this.UcMappingLedger.ProcessGridKey += new System.EventHandler(this.UcMappingLedger_ProcessGridKey);
            // 
            // chkBankInterestLedger
            // 
            resources.ApplyResources(this.chkBankInterestLedger, "chkBankInterestLedger");
            this.chkBankInterestLedger.Name = "chkBankInterestLedger";
            this.chkBankInterestLedger.Properties.Caption = resources.GetString("chkBankInterestLedger.Properties.Caption");
            this.chkBankInterestLedger.StyleController = this.lcLedgerAccount;
            // 
            // grdLedgerType
            // 
            resources.ApplyResources(this.grdLedgerType, "grdLedgerType");
            this.grdLedgerType.Name = "grdLedgerType";
            this.grdLedgerType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.grdLedgerType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("grdLedgerType.Properties.Buttons"))))});
            this.grdLedgerType.Properties.NullText = resources.GetString("grdLedgerType.Properties.NullText");
            this.grdLedgerType.Properties.NullValuePromptShowForEmptyValue = ((bool)(resources.GetObject("grdLedgerType.Properties.NullValuePromptShowForEmptyValue")));
            this.grdLedgerType.Properties.PopupFormSize = new System.Drawing.Size(338, 50);
            this.grdLedgerType.Properties.View = this.gridLookUpEdit1View;
            this.grdLedgerType.StyleController = this.lcLedgerAccount;
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colName});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // colId
            // 
            resources.ApplyResources(this.colId, "colId");
            this.colId.FieldName = "Id";
            this.colId.Name = "colId";
            // 
            // colName
            // 
            resources.ApplyResources(this.colName, "colName");
            this.colName.FieldName = "Name";
            this.colName.Name = "colName";
            // 
            // mtxtNotes
            // 
            resources.ApplyResources(this.mtxtNotes, "mtxtNotes");
            this.mtxtNotes.Name = "mtxtNotes";
            this.mtxtNotes.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.mtxtNotes.Properties.MaxLength = 500;
            this.mtxtNotes.StyleController = this.lcLedgerAccount;
            this.mtxtNotes.UseOptimizedRendering = true;
            // 
            // glkpLedgerCodes
            // 
            resources.ApplyResources(this.glkpLedgerCodes, "glkpLedgerCodes");
            this.glkpLedgerCodes.Name = "glkpLedgerCodes";
            this.glkpLedgerCodes.Properties.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("glkpLedgerCodes.Properties.Appearance.Font")));
            this.glkpLedgerCodes.Properties.Appearance.Options.UseFont = true;
            this.glkpLedgerCodes.Properties.AppearanceFocused.Font = ((System.Drawing.Font)(resources.GetObject("glkpLedgerCodes.Properties.AppearanceFocused.Font")));
            this.glkpLedgerCodes.Properties.AppearanceFocused.Options.UseFont = true;
            this.glkpLedgerCodes.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpLedgerCodes.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpLedgerCodes.Properties.Buttons"))))});
            this.glkpLedgerCodes.Properties.NullText = resources.GetString("glkpLedgerCodes.Properties.NullText");
            this.glkpLedgerCodes.Properties.PopupFormMinSize = new System.Drawing.Size(108, 0);
            this.glkpLedgerCodes.Properties.PopupFormSize = new System.Drawing.Size(50, 40);
            this.glkpLedgerCodes.Properties.View = this.gridLookUpEdit2View;
            this.glkpLedgerCodes.StyleController = this.lcLedgerAccount;
            this.glkpLedgerCodes.TabStop = false;
            // 
            // gridLookUpEdit2View
            // 
            this.gridLookUpEdit2View.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridLookUpEdit2View.Appearance.FocusedRow.Font")));
            this.gridLookUpEdit2View.Appearance.FocusedRow.Options.UseFont = true;
            this.gridLookUpEdit2View.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gridLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit2View.Name = "gridLookUpEdit2View";
            this.gridLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit2View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit2View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit2View.OptionsView.ShowIndicator = false;
            // 
            // chkIncludeCostCenter
            // 
            resources.ApplyResources(this.chkIncludeCostCenter, "chkIncludeCostCenter");
            this.chkIncludeCostCenter.Name = "chkIncludeCostCenter";
            this.chkIncludeCostCenter.Properties.Caption = resources.GetString("chkIncludeCostCenter.Properties.Caption");
            this.chkIncludeCostCenter.StyleController = this.lcLedgerAccount;
            this.chkIncludeCostCenter.CheckedChanged += new System.EventHandler(this.chkIncludeCostCenter_CheckedChanged);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.lcLedgerAccount;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // lkpGroup
            // 
            resources.ApplyResources(this.lkpGroup, "lkpGroup");
            this.lkpGroup.Name = "lkpGroup";
            this.lkpGroup.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.lkpGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("lkpGroup.Properties.Buttons"))))});
            this.lkpGroup.Properties.Columns.AddRange(new DevExpress.XtraEditors.Controls.LookUpColumnInfo[] {
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("lkpGroup.Properties.Columns"), resources.GetString("lkpGroup.Properties.Columns1"), ((int)(resources.GetObject("lkpGroup.Properties.Columns2"))), ((DevExpress.Utils.FormatType)(resources.GetObject("lkpGroup.Properties.Columns3"))), resources.GetString("lkpGroup.Properties.Columns4"), ((bool)(resources.GetObject("lkpGroup.Properties.Columns5"))), ((DevExpress.Utils.HorzAlignment)(resources.GetObject("lkpGroup.Properties.Columns6")))),
            new DevExpress.XtraEditors.Controls.LookUpColumnInfo(resources.GetString("lkpGroup.Properties.Columns7"), resources.GetString("lkpGroup.Properties.Columns8"))});
            this.lkpGroup.Properties.ImmediatePopup = true;
            this.lkpGroup.Properties.NullText = resources.GetString("lkpGroup.Properties.NullText");
            this.lkpGroup.Properties.ShowHeader = false;
            this.lkpGroup.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.lkpGroup.StyleController = this.lcLedgerAccount;
            this.lkpGroup.EditValueChanged += new System.EventHandler(this.lkpGroup_EditValueChanged);
            this.lkpGroup.Leave += new System.EventHandler(this.lkpGroup_Leave);
            // 
            // txtCode
            // 
            resources.ApplyResources(this.txtCode, "txtCode");
            this.txtCode.Name = "txtCode";
            this.txtCode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtCode.Properties.MaxLength = 5;
            this.txtCode.StyleController = this.lcLedgerAccount;
            this.txtCode.Leave += new System.EventHandler(this.txtCode_Leave);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.glkpLedgerCodes;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(322, 28);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem5.Size = new System.Drawing.Size(111, 24);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem1,
            this.layoutControlItem13,
            this.layoutControlItem14,
            this.layoutControlItem2,
            this.lblGroup,
            this.layoutControlItem3,
            this.lblCode,
            this.layoutControlItem6,
            this.emptySpaceItem4,
            this.emptySpaceItem5,
            this.layoutControlItem7,
            this.lcgNotes,
            this.lcgLedgerType,
            this.layoutControlItem1,
            this.emptySpaceItem3,
            this.layoutControlItem4,
            this.emptySpaceItem6,
            this.layoutControlItem8,
            this.emptySpaceItem2,
            this.lcLedgerCloseDate,
            this.lciHOLedger,
            this.lcInvestmentType,
            this.lcgCurrencyDetails});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(433, 482);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 453);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(340, 29);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem13
            // 
            this.layoutControlItem13.Control = this.btnClose;
            resources.ApplyResources(this.layoutControlItem13, "layoutControlItem13");
            this.layoutControlItem13.Location = new System.Drawing.Point(386, 453);
            this.layoutControlItem13.Name = "layoutControlItem13";
            this.layoutControlItem13.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 2, 2);
            this.layoutControlItem13.Size = new System.Drawing.Size(47, 29);
            this.layoutControlItem13.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 0);
            this.layoutControlItem13.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem13.TextToControlDistance = 0;
            this.layoutControlItem13.TextVisible = false;
            // 
            // layoutControlItem14
            // 
            this.layoutControlItem14.Control = this.btnSave;
            resources.ApplyResources(this.layoutControlItem14, "layoutControlItem14");
            this.layoutControlItem14.Location = new System.Drawing.Point(340, 453);
            this.layoutControlItem14.Name = "layoutControlItem14";
            this.layoutControlItem14.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 2, 2);
            this.layoutControlItem14.Size = new System.Drawing.Size(46, 29);
            this.layoutControlItem14.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 2, 3, 0);
            this.layoutControlItem14.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem14.TextToControlDistance = 0;
            this.layoutControlItem14.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.UcMappingLedger;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 314);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 2, 0);
            this.layoutControlItem2.Size = new System.Drawing.Size(433, 105);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // lblGroup
            // 
            this.lblGroup.AllowHtmlStringInCaption = true;
            this.lblGroup.Control = this.lkpGroup;
            resources.ApplyResources(this.lblGroup, "lblGroup");
            this.lblGroup.Location = new System.Drawing.Point(0, 0);
            this.lblGroup.Name = "lblGroup";
            this.lblGroup.Size = new System.Drawing.Size(433, 24);
            this.lblGroup.TextSize = new System.Drawing.Size(97, 13);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkIncludeCostCenter;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(108, 242);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(325, 23);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // lblCode
            // 
            this.lblCode.AllowHtmlStringInCaption = true;
            this.lblCode.Control = this.txtCode;
            resources.ApplyResources(this.lblCode, "lblCode");
            this.lblCode.Location = new System.Drawing.Point(0, 24);
            this.lblCode.Name = "lblCode";
            this.lblCode.Size = new System.Drawing.Size(225, 24);
            this.lblCode.TextSize = new System.Drawing.Size(97, 13);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.chkBankInterestLedger;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(108, 265);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(325, 23);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem4, "emptySpaceItem4");
            this.emptySpaceItem4.Location = new System.Drawing.Point(0, 265);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(108, 23);
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem5, "emptySpaceItem5");
            this.emptySpaceItem5.Location = new System.Drawing.Point(0, 242);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(108, 23);
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.AllowHtmlStringInCaption = true;
            this.layoutControlItem7.Control = this.txtName;
            resources.ApplyResources(this.layoutControlItem7, "layoutControlItem7");
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 98);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(433, 24);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(97, 13);
            // 
            // lcgNotes
            // 
            this.lcgNotes.Control = this.mtxtNotes;
            resources.ApplyResources(this.lcgNotes, "lcgNotes");
            this.lcgNotes.Location = new System.Drawing.Point(0, 419);
            this.lcgNotes.Name = "lcgNotes";
            this.lcgNotes.Size = new System.Drawing.Size(433, 34);
            this.lcgNotes.Spacing = new DevExpress.XtraLayout.Utils.Padding(2, 0, 0, 3);
            this.lcgNotes.TextSize = new System.Drawing.Size(97, 13);
            // 
            // lcgLedgerType
            // 
            this.lcgLedgerType.AllowHtmlStringInCaption = true;
            this.lcgLedgerType.Control = this.grdLedgerType;
            resources.ApplyResources(this.lcgLedgerType, "lcgLedgerType");
            this.lcgLedgerType.Location = new System.Drawing.Point(0, 72);
            this.lcgLedgerType.Name = "lcgLedgerType";
            this.lcgLedgerType.Size = new System.Drawing.Size(274, 26);
            this.lcgLedgerType.TextSize = new System.Drawing.Size(97, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.chkTDSLedgers;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(108, 288);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(233, 26);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            this.layoutControlItem1.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem3, "emptySpaceItem3");
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 288);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(108, 26);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            this.emptySpaceItem3.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnTDSLedger;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(341, 288);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(44, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            this.layoutControlItem4.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // emptySpaceItem6
            // 
            this.emptySpaceItem6.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem6, "emptySpaceItem6");
            this.emptySpaceItem6.Location = new System.Drawing.Point(385, 288);
            this.emptySpaceItem6.Name = "emptySpaceItem6";
            this.emptySpaceItem6.Size = new System.Drawing.Size(48, 26);
            this.emptySpaceItem6.TextSize = new System.Drawing.Size(0, 0);
            this.emptySpaceItem6.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.ucLedgerUsedCodes;
            resources.ApplyResources(this.layoutControlItem8, "layoutControlItem8");
            this.layoutControlItem8.Location = new System.Drawing.Point(255, 24);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem8.Size = new System.Drawing.Size(178, 24);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(225, 24);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(30, 24);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcLedgerCloseDate
            // 
            this.lcLedgerCloseDate.Control = this.deLedgerDateClosed;
            resources.ApplyResources(this.lcLedgerCloseDate, "lcLedgerCloseDate");
            this.lcLedgerCloseDate.Location = new System.Drawing.Point(274, 72);
            this.lcLedgerCloseDate.MaxSize = new System.Drawing.Size(159, 26);
            this.lcLedgerCloseDate.MinSize = new System.Drawing.Size(159, 26);
            this.lcLedgerCloseDate.Name = "lcLedgerCloseDate";
            this.lcLedgerCloseDate.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.lcLedgerCloseDate.Size = new System.Drawing.Size(159, 26);
            this.lcLedgerCloseDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcLedgerCloseDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lcLedgerCloseDate.TextSize = new System.Drawing.Size(58, 13);
            this.lcLedgerCloseDate.TextToControlDistance = 5;
            // 
            // lciHOLedger
            // 
            this.lciHOLedger.Control = this.glkpHOLedger;
            resources.ApplyResources(this.lciHOLedger, "lciHOLedger");
            this.lciHOLedger.Location = new System.Drawing.Point(0, 122);
            this.lciHOLedger.Name = "lciHOLedger";
            this.lciHOLedger.Size = new System.Drawing.Size(433, 24);
            this.lciHOLedger.TextSize = new System.Drawing.Size(97, 13);
            // 
            // lcInvestmentType
            // 
            this.lcInvestmentType.Control = this.glkpInvestmentType;
            resources.ApplyResources(this.lcInvestmentType, "lcInvestmentType");
            this.lcInvestmentType.Location = new System.Drawing.Point(0, 48);
            this.lcInvestmentType.Name = "lcInvestmentType";
            this.lcInvestmentType.Size = new System.Drawing.Size(433, 24);
            this.lcInvestmentType.TextSize = new System.Drawing.Size(97, 13);
            // 
            // lcgCurrencyDetails
            // 
            this.lcgCurrencyDetails.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lcgCurrencyDetails.AppearanceGroup.Font")));
            this.lcgCurrencyDetails.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.lcgCurrencyDetails, "lcgCurrencyDetails");
            this.lcgCurrencyDetails.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem9,
            this.layoutControlItem10,
            this.layoutControlItem12,
            this.layoutControlItem11,
            this.lcOpeningAvgExchangeRateCaption,
            this.lcCurrencyNote,
            this.lcExchangeRate});
            this.lcgCurrencyDetails.Location = new System.Drawing.Point(0, 146);
            this.lcgCurrencyDetails.Name = "lcgCurrencyDetails";
            this.lcgCurrencyDetails.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.lcgCurrencyDetails.Size = new System.Drawing.Size(433, 96);
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.AllowHtmlStringInCaption = true;
            this.layoutControlItem9.Control = this.glkpCurrencyCountry;
            this.layoutControlItem9.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            resources.ApplyResources(this.layoutControlItem9, "layoutControlItem9");
            this.layoutControlItem9.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem9.MaxSize = new System.Drawing.Size(274, 26);
            this.layoutControlItem9.MinSize = new System.Drawing.Size(274, 26);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(274, 26);
            this.layoutControlItem9.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem9.TextSize = new System.Drawing.Size(97, 13);
            // 
            // layoutControlItem10
            // 
            this.layoutControlItem10.Control = this.lblSymbol;
            this.layoutControlItem10.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            resources.ApplyResources(this.layoutControlItem10, "layoutControlItem10");
            this.layoutControlItem10.Location = new System.Drawing.Point(274, 0);
            this.layoutControlItem10.Name = "layoutControlItem10";
            this.layoutControlItem10.Size = new System.Drawing.Size(48, 26);
            this.layoutControlItem10.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem10.TextToControlDistance = 0;
            this.layoutControlItem10.TextVisible = false;
            this.layoutControlItem10.TrimClientAreaToControl = false;
            // 
            // layoutControlItem12
            // 
            this.layoutControlItem12.Control = this.lblCurrencyName;
            resources.ApplyResources(this.layoutControlItem12, "layoutControlItem12");
            this.layoutControlItem12.Location = new System.Drawing.Point(350, 0);
            this.layoutControlItem12.Name = "layoutControlItem12";
            this.layoutControlItem12.Size = new System.Drawing.Size(73, 26);
            this.layoutControlItem12.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem12.TextToControlDistance = 0;
            this.layoutControlItem12.TextVisible = false;
            // 
            // layoutControlItem11
            // 
            this.layoutControlItem11.Control = this.lblCurrencySymbol;
            this.layoutControlItem11.ControlAlignment = System.Drawing.ContentAlignment.MiddleLeft;
            resources.ApplyResources(this.layoutControlItem11, "layoutControlItem11");
            this.layoutControlItem11.Location = new System.Drawing.Point(322, 0);
            this.layoutControlItem11.Name = "layoutControlItem11";
            this.layoutControlItem11.Size = new System.Drawing.Size(28, 26);
            this.layoutControlItem11.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem11.TextToControlDistance = 0;
            this.layoutControlItem11.TextVisible = false;
            this.layoutControlItem11.TrimClientAreaToControl = false;
            // 
            // lcOpeningAvgExchangeRateCaption
            // 
            this.lcOpeningAvgExchangeRateCaption.AllowHotTrack = false;
            this.lcOpeningAvgExchangeRateCaption.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lcOpeningAvgExchangeRateCaption.AppearanceItemCaption.Font")));
            this.lcOpeningAvgExchangeRateCaption.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lcOpeningAvgExchangeRateCaption, "lcOpeningAvgExchangeRateCaption");
            this.lcOpeningAvgExchangeRateCaption.Location = new System.Drawing.Point(0, 26);
            this.lcOpeningAvgExchangeRateCaption.Name = "lcOpeningAvgExchangeRateCaption";
            this.lcOpeningAvgExchangeRateCaption.Size = new System.Drawing.Size(322, 24);
            this.lcOpeningAvgExchangeRateCaption.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lcOpeningAvgExchangeRateCaption.TextSize = new System.Drawing.Size(220, 13);
            // 
            // lcCurrencyNote
            // 
            this.lcCurrencyNote.AllowHotTrack = false;
            this.lcCurrencyNote.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lcCurrencyNote.AppearanceItemCaption.Font")));
            this.lcCurrencyNote.AppearanceItemCaption.ForeColor = ((System.Drawing.Color)(resources.GetObject("lcCurrencyNote.AppearanceItemCaption.ForeColor")));
            this.lcCurrencyNote.AppearanceItemCaption.Options.UseFont = true;
            this.lcCurrencyNote.AppearanceItemCaption.Options.UseForeColor = true;
            resources.ApplyResources(this.lcCurrencyNote, "lcCurrencyNote");
            this.lcCurrencyNote.Location = new System.Drawing.Point(0, 50);
            this.lcCurrencyNote.Name = "lcCurrencyNote";
            this.lcCurrencyNote.Size = new System.Drawing.Size(423, 17);
            this.lcCurrencyNote.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lcCurrencyNote.TextSize = new System.Drawing.Size(367, 13);
            // 
            // lcExchangeRate
            // 
            this.lcExchangeRate.Control = this.txtOPExchangeRate;
            resources.ApplyResources(this.lcExchangeRate, "lcExchangeRate");
            this.lcExchangeRate.Location = new System.Drawing.Point(322, 26);
            this.lcExchangeRate.Name = "lcExchangeRate";
            this.lcExchangeRate.Size = new System.Drawing.Size(101, 24);
            this.lcExchangeRate.TextSize = new System.Drawing.Size(0, 0);
            this.lcExchangeRate.TextToControlDistance = 0;
            this.lcExchangeRate.TextVisible = false;
            // 
            // LayGroup
            // 
            this.LayGroup.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("LayGroup.AppearanceGroup.Font")));
            this.LayGroup.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.LayGroup, "LayGroup");
            this.LayGroup.Location = new System.Drawing.Point(0, 0);
            this.LayGroup.Name = "LayGroup";
            this.LayGroup.OptionsItemText.TextToControlDistance = 5;
            this.LayGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.LayGroup.Size = new System.Drawing.Size(436, 295);
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlGroup2.AppearanceGroup.Font")));
            this.layoutControlGroup2.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.layoutControlGroup2, "layoutControlGroup2");
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup2.Name = "LayGroup";
            this.layoutControlGroup2.OptionsItemText.TextToControlDistance = 5;
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup2.Size = new System.Drawing.Size(436, 295);
            // 
            // frmLedgerBankAccountAdd
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.lcLedgerAccount);
            this.Name = "frmLedgerBankAccountAdd";
            this.Load += new System.EventHandler(this.frmLedgerBankAccountAdd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lcLedgerAccount)).EndInit();
            this.lcLedgerAccount.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtOPExchangeRate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpCurrencyCountry.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCurrencyCountry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpInvestmentType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInvestmentType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpHOLedger.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvHO)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deLedgerDateClosed.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deLedgerDateClosed.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkTDSLedgers.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkBankInterestLedger.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.grdLedgerType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.mtxtNotes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpLedgerCodes.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit2View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkIncludeCostCenter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem13)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem14)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgNotes)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgLedgerType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcLedgerCloseDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lciHOLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcInvestmentType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgCurrencyDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOpeningAvgExchangeRateCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcCurrencyNote)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcExchangeRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.LayGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lcLedgerAccount;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem13;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem14;
        private DevExpress.XtraEditors.LookUpEdit lkpGroup;
        //  private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.MemoEdit mtxtNotes;
        private DevExpress.XtraEditors.CheckEdit chkIncludeCostCenter;
        private DevExpress.XtraEditors.GridLookUpEdit grdLedgerType;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraEditors.GridLookUpEdit glkpLedgerCodes;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit2View;
        private DevExpress.XtraEditors.CheckEdit chkBankInterestLedger;
        private DevExpress.XtraLayout.LayoutControlGroup LayGroup;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private UIControls.UcAccountMapping UcMappingLedger;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraLayout.LayoutControlItem lblGroup;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem lblCode;
        private DevExpress.XtraLayout.LayoutControlItem lcgLedgerType;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem lcgNotes;
        private DevExpress.XtraEditors.CheckEdit chkTDSLedgers;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraEditors.SimpleButton btnTDSLedger;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem6;
        private UIControls.UcUsedCodesIcon ucLedgerUsedCodes;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraEditors.DateEdit deLedgerDateClosed;
        private DevExpress.XtraLayout.LayoutControlItem lcLedgerCloseDate;
        private DevExpress.XtraEditors.GridLookUpEdit glkpHOLedger;
        private DevExpress.XtraGrid.Views.Grid.GridView gvHO;
        private DevExpress.XtraGrid.Columns.GridColumn colHeadOfficeLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn colHOLedgerName;
        private DevExpress.XtraLayout.LayoutControlItem lciHOLedger;
        private DevExpress.XtraEditors.GridLookUpEdit glkpInvestmentType;
        private DevExpress.XtraGrid.Views.Grid.GridView gvInvestmentType;
        private DevExpress.XtraGrid.Columns.GridColumn gcInvestmentTypeId;
        private DevExpress.XtraGrid.Columns.GridColumn gcInvestmentType;
        private DevExpress.XtraLayout.LayoutControlItem lcInvestmentType;
        private DevExpress.XtraEditors.GridLookUpEdit glkpCurrencyCountry;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCurrencyCountry;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencyCountryId;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrency;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencyName;
        private DevExpress.XtraGrid.Columns.GridColumn colCurrencySymbol;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraEditors.LabelControl lblSymbol;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem10;
        private DevExpress.XtraEditors.LabelControl lblCurrencySymbol;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem11;
        private System.Windows.Forms.Label lblCurrencyName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem12;
        private DevExpress.XtraLayout.SimpleLabelItem lcOpeningAvgExchangeRateCaption;
        private DevExpress.XtraLayout.SimpleLabelItem lcCurrencyNote;
        private DevExpress.XtraLayout.LayoutControlGroup lcgCurrencyDetails;
        private DevExpress.XtraEditors.TextEdit txtOPExchangeRate;
        private DevExpress.XtraLayout.LayoutControlItem lcExchangeRate;
    }
}