namespace ACPP.Modules.TDS
{
    partial class frmPaymentsParty
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPaymentsParty));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.UcPendingDetails = new ACPP.Modules.UIControls.UCTDSPayments();
            this.ucPartyPaymentSummary = new ACPP.Modules.UIControls.UcTDSSummary();
            this.glkpCashOrBank = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridView1 = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCashBankLedger = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCashBankledgerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.lblNetAmountType = new DevExpress.XtraEditors.LabelControl();
            this.lblNetAmount = new DevExpress.XtraEditors.LabelControl();
            this.lblNetPayableAmtToParty = new DevExpress.XtraEditors.LabelControl();
            this.gcPaymentsParty = new DevExpress.XtraGrid.GridControl();
            this.gvPaymentsParty = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.ColVoucherNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColPaymentAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtAmount = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colActualAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ColVoucherMode = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rcboVoucherMode = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.lcPaymentsParty = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem9 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem15 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblCashOrBank = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblPartyLedgerNameCaption = new DevExpress.XtraLayout.SimpleLabelItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblPartyLedgerName = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblCapBalance = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblCashBankAmount = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.glkpCashOrBank.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPaymentsParty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPaymentsParty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcboVoucherMode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPaymentsParty)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCashOrBank)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPartyLedgerNameCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPartyLedgerName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCapBalance)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCashBankAmount)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.UcPendingDetails);
            this.layoutControl1.Controls.Add(this.ucPartyPaymentSummary);
            this.layoutControl1.Controls.Add(this.glkpCashOrBank);
            this.layoutControl1.Controls.Add(this.lblNetAmountType);
            this.layoutControl1.Controls.Add(this.lblNetAmount);
            this.layoutControl1.Controls.Add(this.lblNetPayableAmtToParty);
            this.layoutControl1.Controls.Add(this.gcPaymentsParty);
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.btnSave);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(472, 270, 250, 350);
            this.layoutControl1.Root = this.lcPaymentsParty;
            // 
            // UcPendingDetails
            // 
            this.UcPendingDetails.ChangeCaptionName = "<b>Get Pending <color=blue><u>P</u></color>ayment Details</b>";
            resources.ApplyResources(this.UcPendingDetails, "UcPendingDetails");
            this.UcPendingDetails.Name = "UcPendingDetails";
            this.UcPendingDetails.GetPendingDetails += new System.EventHandler(this.UcPendingDetails_GetPendingDetails);
            // 
            // ucPartyPaymentSummary
            // 
            resources.ApplyResources(this.ucPartyPaymentSummary, "ucPartyPaymentSummary");
            this.ucPartyPaymentSummary.Name = "ucPartyPaymentSummary";
            this.ucPartyPaymentSummary.UpdateTDSSummary = null;
            // 
            // glkpCashOrBank
            // 
            resources.ApplyResources(this.glkpCashOrBank, "glkpCashOrBank");
            this.glkpCashOrBank.Name = "glkpCashOrBank";
            this.glkpCashOrBank.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpCashOrBank.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpCashOrBank.Properties.Buttons"))))});
            this.glkpCashOrBank.Properties.ImmediatePopup = true;
            this.glkpCashOrBank.Properties.NullText = resources.GetString("glkpCashOrBank.Properties.NullText");
            this.glkpCashOrBank.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.glkpCashOrBank.Properties.PopupFormSize = new System.Drawing.Size(365, 0);
            this.glkpCashOrBank.Properties.View = this.gridView1;
            this.glkpCashOrBank.StyleController = this.layoutControl1;
            this.glkpCashOrBank.EditValueChanged += new System.EventHandler(this.glkpCashOrBank_EditValueChanged);
            this.glkpCashOrBank.KeyDown += new System.Windows.Forms.KeyEventHandler(this.glkpCashOrBank_KeyDown);
            this.glkpCashOrBank.Leave += new System.EventHandler(this.glkpCashOrBank_Leave);
            this.glkpCashOrBank.Validating += new System.ComponentModel.CancelEventHandler(this.glkpCashOrBank_Validating);
            // 
            // gridView1
            // 
            this.gridView1.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridView1.Appearance.FocusedRow.Font")));
            this.gridView1.Appearance.FocusedRow.Options.UseFont = true;
            this.gridView1.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCashBankLedger,
            this.colCashBankledgerName});
            this.gridView1.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridView1.Name = "gridView1";
            this.gridView1.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridView1.OptionsView.ShowColumnHeaders = false;
            this.gridView1.OptionsView.ShowGroupPanel = false;
            this.gridView1.OptionsView.ShowIndicator = false;
            // 
            // colCashBankLedger
            // 
            resources.ApplyResources(this.colCashBankLedger, "colCashBankLedger");
            this.colCashBankLedger.FieldName = "LEDGER_ID";
            this.colCashBankLedger.Name = "colCashBankLedger";
            // 
            // colCashBankledgerName
            // 
            resources.ApplyResources(this.colCashBankledgerName, "colCashBankledgerName");
            this.colCashBankledgerName.FieldName = "LEDGER_NAME";
            this.colCashBankledgerName.Name = "colCashBankledgerName";
            // 
            // lblNetAmountType
            // 
            this.lblNetAmountType.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblNetAmountType.Appearance.Font")));
            this.lblNetAmountType.Appearance.ForeColor = ((System.Drawing.Color)(resources.GetObject("lblNetAmountType.Appearance.ForeColor")));
            resources.ApplyResources(this.lblNetAmountType, "lblNetAmountType");
            this.lblNetAmountType.Name = "lblNetAmountType";
            this.lblNetAmountType.StyleController = this.layoutControl1;
            // 
            // lblNetAmount
            // 
            this.lblNetAmount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblNetAmount.Appearance.Font")));
            this.lblNetAmount.Appearance.ForeColor = ((System.Drawing.Color)(resources.GetObject("lblNetAmount.Appearance.ForeColor")));
            resources.ApplyResources(this.lblNetAmount, "lblNetAmount");
            this.lblNetAmount.Name = "lblNetAmount";
            this.lblNetAmount.StyleController = this.layoutControl1;
            // 
            // lblNetPayableAmtToParty
            // 
            this.lblNetPayableAmtToParty.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblNetPayableAmtToParty.Appearance.Font")));
            resources.ApplyResources(this.lblNetPayableAmtToParty, "lblNetPayableAmtToParty");
            this.lblNetPayableAmtToParty.Name = "lblNetPayableAmtToParty";
            this.lblNetPayableAmtToParty.StyleController = this.layoutControl1;
            // 
            // gcPaymentsParty
            // 
            resources.ApplyResources(this.gcPaymentsParty, "gcPaymentsParty");
            this.gcPaymentsParty.MainView = this.gvPaymentsParty;
            this.gcPaymentsParty.Name = "gcPaymentsParty";
            this.gcPaymentsParty.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rtxtAmount,
            this.rcboVoucherMode});
            this.gcPaymentsParty.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPaymentsParty});
            this.gcPaymentsParty.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.gcPaymentsParty_ProcessGridKey);
            this.gcPaymentsParty.Enter += new System.EventHandler(this.gcPaymentsParty_Enter);
            this.gcPaymentsParty.KeyDown += new System.Windows.Forms.KeyEventHandler(this.gcPaymentsParty_KeyDown);
            this.gcPaymentsParty.Leave += new System.EventHandler(this.gcPaymentsParty_Leave);
            this.gcPaymentsParty.PreviewKeyDown += new System.Windows.Forms.PreviewKeyDownEventHandler(this.gcPaymentsParty_PreviewKeyDown);
            // 
            // gvPaymentsParty
            // 
            this.gvPaymentsParty.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvPaymentsParty.Appearance.FocusedRow.Font")));
            this.gvPaymentsParty.Appearance.FocusedRow.Options.UseFont = true;
            this.gvPaymentsParty.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvPaymentsParty.Appearance.HeaderPanel.Font")));
            this.gvPaymentsParty.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvPaymentsParty.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.ColVoucherNo,
            this.ColDate,
            this.ColPaymentAmount,
            this.colActualAmount,
            this.ColVoucherMode});
            this.gvPaymentsParty.GridControl = this.gcPaymentsParty;
            this.gvPaymentsParty.Name = "gvPaymentsParty";
            this.gvPaymentsParty.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvPaymentsParty.OptionsView.ShowGroupPanel = false;
            this.gvPaymentsParty.OptionsView.ShowIndicator = false;
            this.gvPaymentsParty.ShowingEditor += new System.ComponentModel.CancelEventHandler(this.gvPaymentsParty_ShowingEditor);
            this.gvPaymentsParty.InvalidRowException += new DevExpress.XtraGrid.Views.Base.InvalidRowExceptionEventHandler(this.gvPaymentsParty_InvalidRowException);
            this.gvPaymentsParty.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gvPaymentsParty_ValidateRow);
            // 
            // ColVoucherNo
            // 
            resources.ApplyResources(this.ColVoucherNo, "ColVoucherNo");
            this.ColVoucherNo.FieldName = "VOUCHER_NO";
            this.ColVoucherNo.Name = "ColVoucherNo";
            this.ColVoucherNo.OptionsColumn.AllowEdit = false;
            this.ColVoucherNo.OptionsColumn.AllowFocus = false;
            this.ColVoucherNo.OptionsColumn.FixedWidth = true;
            this.ColVoucherNo.OptionsFilter.AllowAutoFilter = false;
            this.ColVoucherNo.OptionsFilter.AllowFilter = false;
            this.ColVoucherNo.OptionsFilter.FilterBySortField = DevExpress.Utils.DefaultBoolean.False;
            // 
            // ColDate
            // 
            resources.ApplyResources(this.ColDate, "ColDate");
            this.ColDate.FieldName = "VOUCHER_DATE";
            this.ColDate.Name = "ColDate";
            this.ColDate.OptionsColumn.AllowEdit = false;
            this.ColDate.OptionsColumn.AllowFocus = false;
            this.ColDate.OptionsFilter.AllowAutoFilter = false;
            this.ColDate.OptionsFilter.AllowFilter = false;
            this.ColDate.OptionsFilter.FilterBySortField = DevExpress.Utils.DefaultBoolean.False;
            // 
            // ColPaymentAmount
            // 
            this.ColPaymentAmount.AppearanceCell.Font = ((System.Drawing.Font)(resources.GetObject("ColPaymentAmount.AppearanceCell.Font")));
            this.ColPaymentAmount.AppearanceCell.ForeColor = ((System.Drawing.Color)(resources.GetObject("ColPaymentAmount.AppearanceCell.ForeColor")));
            this.ColPaymentAmount.AppearanceCell.Options.UseFont = true;
            this.ColPaymentAmount.AppearanceCell.Options.UseForeColor = true;
            resources.ApplyResources(this.ColPaymentAmount, "ColPaymentAmount");
            this.ColPaymentAmount.ColumnEdit = this.rtxtAmount;
            this.ColPaymentAmount.DisplayFormat.FormatString = "N";
            this.ColPaymentAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.ColPaymentAmount.FieldName = "AMOUNT";
            this.ColPaymentAmount.Name = "ColPaymentAmount";
            this.ColPaymentAmount.OptionsFilter.AllowAutoFilter = false;
            this.ColPaymentAmount.OptionsFilter.AllowFilter = false;
            this.ColPaymentAmount.OptionsFilter.FilterBySortField = DevExpress.Utils.DefaultBoolean.False;
            // 
            // rtxtAmount
            // 
            this.rtxtAmount.AppearanceFocused.BackColor = ((System.Drawing.Color)(resources.GetObject("rtxtAmount.AppearanceFocused.BackColor")));
            this.rtxtAmount.AppearanceFocused.Options.UseBackColor = true;
            resources.ApplyResources(this.rtxtAmount, "rtxtAmount");
            this.rtxtAmount.DisplayFormat.FormatString = "n";
            this.rtxtAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.rtxtAmount.Mask.EditMask = resources.GetString("rtxtAmount.Mask.EditMask");
            this.rtxtAmount.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("rtxtAmount.Mask.MaskType")));
            this.rtxtAmount.Name = "rtxtAmount";
            // 
            // colActualAmount
            // 
            this.colActualAmount.AppearanceCell.Font = ((System.Drawing.Font)(resources.GetObject("colActualAmount.AppearanceCell.Font")));
            this.colActualAmount.AppearanceCell.Options.UseFont = true;
            resources.ApplyResources(this.colActualAmount, "colActualAmount");
            this.colActualAmount.DisplayFormat.FormatString = "N";
            this.colActualAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colActualAmount.FieldName = "REMAINING_BALANCE";
            this.colActualAmount.Name = "colActualAmount";
            this.colActualAmount.OptionsColumn.AllowEdit = false;
            this.colActualAmount.OptionsColumn.AllowFocus = false;
            this.colActualAmount.OptionsColumn.ReadOnly = true;
            // 
            // ColVoucherMode
            // 
            this.ColVoucherMode.AppearanceCell.BackColor = ((System.Drawing.Color)(resources.GetObject("ColVoucherMode.AppearanceCell.BackColor")));
            this.ColVoucherMode.AppearanceCell.Options.UseBackColor = true;
            resources.ApplyResources(this.ColVoucherMode, "ColVoucherMode");
            this.ColVoucherMode.FieldName = "TRANS_MODE";
            this.ColVoucherMode.Name = "ColVoucherMode";
            this.ColVoucherMode.OptionsColumn.AllowEdit = false;
            this.ColVoucherMode.OptionsColumn.AllowFocus = false;
            this.ColVoucherMode.OptionsColumn.FixedWidth = true;
            this.ColVoucherMode.OptionsColumn.ShowCaption = false;
            this.ColVoucherMode.OptionsFilter.AllowAutoFilter = false;
            this.ColVoucherMode.OptionsFilter.AllowFilter = false;
            this.ColVoucherMode.OptionsFilter.FilterBySortField = DevExpress.Utils.DefaultBoolean.False;
            // 
            // rcboVoucherMode
            // 
            this.rcboVoucherMode.AppearanceFocused.BackColor = ((System.Drawing.Color)(resources.GetObject("rcboVoucherMode.AppearanceFocused.BackColor")));
            this.rcboVoucherMode.AppearanceFocused.Options.UseBackColor = true;
            resources.ApplyResources(this.rcboVoucherMode, "rcboVoucherMode");
            this.rcboVoucherMode.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.rcboVoucherMode.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rcboVoucherMode.Buttons"))))});
            this.rcboVoucherMode.Items.AddRange(new object[] {
            resources.GetString("rcboVoucherMode.Items"),
            resources.GetString("rcboVoucherMode.Items1")});
            this.rcboVoucherMode.Name = "rcboVoucherMode";
            this.rcboVoucherMode.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            // 
            // btnClose
            // 
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
            // lcPaymentsParty
            // 
            resources.ApplyResources(this.lcPaymentsParty, "lcPaymentsParty");
            this.lcPaymentsParty.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcPaymentsParty.GroupBordersVisible = false;
            this.lcPaymentsParty.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.layoutControlItem9,
            this.emptySpaceItem5,
            this.layoutControlGroup1,
            this.layoutControlItem6,
            this.lblCashOrBank,
            this.lblPartyLedgerNameCaption,
            this.emptySpaceItem1,
            this.lblPartyLedgerName,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.emptySpaceItem2,
            this.lblCapBalance,
            this.lblCashBankAmount});
            this.lcPaymentsParty.Location = new System.Drawing.Point(0, 0);
            this.lcPaymentsParty.Name = "Root";
            this.lcPaymentsParty.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcPaymentsParty.Size = new System.Drawing.Size(894, 487);
            this.lcPaymentsParty.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.gcPaymentsParty;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 52);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(593, 409);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.lblNetPayableAmtToParty;
            resources.ApplyResources(this.layoutControlItem7, "layoutControlItem7");
            this.layoutControlItem7.Location = new System.Drawing.Point(417, 461);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(176, 26);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlItem8.AppearanceItemCaption.Font")));
            this.layoutControlItem8.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlItem8.Control = this.lblNetAmount;
            resources.ApplyResources(this.layoutControlItem8, "layoutControlItem8");
            this.layoutControlItem8.Location = new System.Drawing.Point(593, 461);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(32, 26);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // layoutControlItem9
            // 
            this.layoutControlItem9.Control = this.lblNetAmountType;
            resources.ApplyResources(this.layoutControlItem9, "layoutControlItem9");
            this.layoutControlItem9.Location = new System.Drawing.Point(625, 461);
            this.layoutControlItem9.Name = "layoutControlItem9";
            this.layoutControlItem9.Size = new System.Drawing.Size(18, 26);
            this.layoutControlItem9.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem9.TextToControlDistance = 0;
            this.layoutControlItem9.TextVisible = false;
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem5, "emptySpaceItem5");
            this.emptySpaceItem5.Location = new System.Drawing.Point(643, 461);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(105, 26);
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlGroup1.AppearanceGroup.Font")));
            this.layoutControlGroup1.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem15});
            this.layoutControlGroup1.Location = new System.Drawing.Point(593, 52);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(301, 409);
            // 
            // layoutControlItem15
            // 
            this.layoutControlItem15.Control = this.ucPartyPaymentSummary;
            resources.ApplyResources(this.layoutControlItem15, "layoutControlItem15");
            this.layoutControlItem15.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem15.Name = "layoutControlItem15";
            this.layoutControlItem15.Size = new System.Drawing.Size(295, 384);
            this.layoutControlItem15.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem15.TextToControlDistance = 0;
            this.layoutControlItem15.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.UcPendingDetails;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(638, 28);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 0, 2);
            this.layoutControlItem6.Size = new System.Drawing.Size(256, 24);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // lblCashOrBank
            // 
            this.lblCashOrBank.AllowHtmlStringInCaption = true;
            this.lblCashOrBank.Control = this.glkpCashOrBank;
            resources.ApplyResources(this.lblCashOrBank, "lblCashOrBank");
            this.lblCashOrBank.Location = new System.Drawing.Point(0, 28);
            this.lblCashOrBank.Name = "lblCashOrBank";
            this.lblCashOrBank.Size = new System.Drawing.Size(445, 24);
            this.lblCashOrBank.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lblCashOrBank.TextSize = new System.Drawing.Size(71, 13);
            this.lblCashOrBank.TextToControlDistance = 5;
            // 
            // lblPartyLedgerNameCaption
            // 
            this.lblPartyLedgerNameCaption.AllowHotTrack = false;
            this.lblPartyLedgerNameCaption.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblPartyLedgerNameCaption.AppearanceItemCaption.Font")));
            this.lblPartyLedgerNameCaption.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblPartyLedgerNameCaption, "lblPartyLedgerNameCaption");
            this.lblPartyLedgerNameCaption.Location = new System.Drawing.Point(310, 0);
            this.lblPartyLedgerNameCaption.Name = "lblPartyLedgerNameCaption";
            this.lblPartyLedgerNameCaption.Size = new System.Drawing.Size(158, 28);
            this.lblPartyLedgerNameCaption.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblPartyLedgerNameCaption.TextSize = new System.Drawing.Size(144, 24);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(310, 28);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblPartyLedgerName
            // 
            this.lblPartyLedgerName.AllowHotTrack = false;
            this.lblPartyLedgerName.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblPartyLedgerName.AppearanceItemCaption.Font")));
            this.lblPartyLedgerName.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblPartyLedgerName, "lblPartyLedgerName");
            this.lblPartyLedgerName.Location = new System.Drawing.Point(468, 0);
            this.lblPartyLedgerName.Name = "lblPartyLedgerName";
            this.lblPartyLedgerName.Size = new System.Drawing.Size(426, 28);
            this.lblPartyLedgerName.TextSize = new System.Drawing.Size(6, 24);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnSave;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(748, 461);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(73, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(73, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(73, 26);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnClose;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(821, 461);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(73, 26);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(73, 26);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(73, 26);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 461);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(417, 26);
            this.emptySpaceItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblCapBalance
            // 
            this.lblCapBalance.AllowHotTrack = false;
            this.lblCapBalance.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblCapBalance.AppearanceItemCaption.Font")));
            this.lblCapBalance.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblCapBalance, "lblCapBalance");
            this.lblCapBalance.Location = new System.Drawing.Point(445, 28);
            this.lblCapBalance.Name = "lblCapBalance";
            this.lblCapBalance.Size = new System.Drawing.Size(37, 24);
            this.lblCapBalance.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblCapBalance.TextSize = new System.Drawing.Size(20, 13);
            // 
            // lblCashBankAmount
            // 
            this.lblCashBankAmount.AllowHotTrack = false;
            this.lblCashBankAmount.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblCashBankAmount.AppearanceItemCaption.Font")));
            this.lblCashBankAmount.AppearanceItemCaption.ForeColor = ((System.Drawing.Color)(resources.GetObject("lblCashBankAmount.AppearanceItemCaption.ForeColor")));
            this.lblCashBankAmount.AppearanceItemCaption.Options.UseFont = true;
            this.lblCashBankAmount.AppearanceItemCaption.Options.UseForeColor = true;
            resources.ApplyResources(this.lblCashBankAmount, "lblCashBankAmount");
            this.lblCashBankAmount.Location = new System.Drawing.Point(482, 28);
            this.lblCashBankAmount.Name = "lblCashBankAmount";
            this.lblCashBankAmount.Size = new System.Drawing.Size(156, 24);
            this.lblCashBankAmount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblCashBankAmount.TextSize = new System.Drawing.Size(28, 16);
            // 
            // frmPaymentsParty
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmPaymentsParty";
            this.Load += new System.EventHandler(this.frmPaymentsParty_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmPaymentsParty_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.glkpCashOrBank.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridView1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPaymentsParty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPaymentsParty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rcboVoucherMode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPaymentsParty)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem15)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCashOrBank)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPartyLedgerNameCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblPartyLedgerName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCapBalance)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCashBankAmount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup lcPaymentsParty;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.GridControl gcPaymentsParty;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPaymentsParty;
        private DevExpress.XtraEditors.LabelControl lblNetAmountType;
        private DevExpress.XtraEditors.LabelControl lblNetAmount;
        private DevExpress.XtraEditors.LabelControl lblNetPayableAmtToParty;
        private DevExpress.XtraGrid.Columns.GridColumn ColVoucherNo;
        private DevExpress.XtraGrid.Columns.GridColumn ColDate;
        private DevExpress.XtraGrid.Columns.GridColumn ColPaymentAmount;
        private DevExpress.XtraGrid.Columns.GridColumn ColVoucherMode;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtAmount;
        private DevExpress.XtraEditors.GridLookUpEdit glkpCashOrBank;
        private DevExpress.XtraGrid.Views.Grid.GridView gridView1;
        private DevExpress.XtraGrid.Columns.GridColumn colCashBankLedger;
        private DevExpress.XtraGrid.Columns.GridColumn colCashBankledgerName;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox rcboVoucherMode;
        private DevExpress.XtraLayout.LayoutControlItem lblCashOrBank;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem9;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
        private UIControls.UcTDSSummary ucPartyPaymentSummary;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem15;
        private UIControls.UCTDSPayments UcPendingDetails;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.SimpleLabelItem lblPartyLedgerNameCaption;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.SimpleLabelItem lblPartyLedgerName;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.SimpleLabelItem lblCapBalance;
        private DevExpress.XtraLayout.SimpleLabelItem lblCashBankAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colActualAmount;
    }
}