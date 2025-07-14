namespace ACPP.Modules.Transaction
{
    partial class frmTransactionReference
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
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
                if (resultArgs != null)
                {
                    resultArgs.Dispose();
                    resultArgs = null;
                }
                if (dtVoucherLedgerReferenceDetails != null)
                {
                    dtVoucherLedgerReferenceDetails.Dispose();
                    dtVoucherLedgerReferenceDetails = null;
                }
                
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTransactionReference));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.cmsCostCentre = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteToolStripMenuCostCentre = new System.Windows.Forms.ToolStripMenuItem();
            this.colcbRefNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlCostCenter = new DevExpress.XtraEditors.PanelControl();
            this.lctCostCenter = new DevExpress.XtraLayout.LayoutControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.lblAmtCaption = new DevExpress.XtraEditors.LabelControl();
            this.gcReferenceNo = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.gvReferenceNo = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colRefNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rglkpRefNumber = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repglkView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colcbRefVoucherId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcbBalance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcbTotal = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colcbDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtAmount = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colValidationRefAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRecPayVoucherId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colVoucherDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rbeDeleteReference = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.rbtn = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblLedgerAmount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblLedgerNameValue = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblLedgerName = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem2 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.cmsCostCentre.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCostCenter)).BeginInit();
            this.pnlCostCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lctCostCenter)).BeginInit();
            this.lctCostCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcReferenceNo)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvReferenceNo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpRefNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repglkView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbeDeleteReference)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLedgerAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLedgerNameValue)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLedgerName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // cmsCostCentre
            // 
            this.cmsCostCentre.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteToolStripMenuCostCentre});
            this.cmsCostCentre.Name = "cmsCostCentre";
            resources.ApplyResources(this.cmsCostCentre, "cmsCostCentre");
            // 
            // DeleteToolStripMenuCostCentre
            // 
            this.DeleteToolStripMenuCostCentre.Image = global::ACPP.Properties.Resources.Delete;
            this.DeleteToolStripMenuCostCentre.Name = "DeleteToolStripMenuCostCentre";
            resources.ApplyResources(this.DeleteToolStripMenuCostCentre, "DeleteToolStripMenuCostCentre");
            // 
            // colcbRefNumber
            // 
            resources.ApplyResources(this.colcbRefNumber, "colcbRefNumber");
            this.colcbRefNumber.FieldName = "REFERENCE_NUMBER";
            this.colcbRefNumber.Name = "colcbRefNumber";
            // 
            // pnlCostCenter
            // 
            this.pnlCostCenter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlCostCenter.Controls.Add(this.lctCostCenter);
            resources.ApplyResources(this.pnlCostCenter, "pnlCostCenter");
            this.pnlCostCenter.Name = "pnlCostCenter";
            // 
            // lctCostCenter
            // 
            this.lctCostCenter.AllowCustomizationMenu = false;
            this.lctCostCenter.Controls.Add(this.btnCancel);
            this.lctCostCenter.Controls.Add(this.btnOk);
            this.lctCostCenter.Controls.Add(this.lblAmtCaption);
            this.lctCostCenter.Controls.Add(this.gcReferenceNo);
            resources.ApplyResources(this.lctCostCenter, "lctCostCenter");
            this.lctCostCenter.Name = "lctCostCenter";
            this.lctCostCenter.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(487, 109, 449, 469);
            this.lctCostCenter.Root = this.layoutControlGroup1;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.StyleController = this.lctCostCenter;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click_1);
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.StyleController = this.lctCostCenter;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblAmtCaption
            // 
            this.lblAmtCaption.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblAmtCaption.Appearance.Font")));
            resources.ApplyResources(this.lblAmtCaption, "lblAmtCaption");
            this.lblAmtCaption.Name = "lblAmtCaption";
            this.lblAmtCaption.StyleController = this.lctCostCenter;
            // 
            // gcReferenceNo
            // 
            this.gcReferenceNo.ContextMenuStrip = this.contextMenuStrip1;
            resources.ApplyResources(this.gcReferenceNo, "gcReferenceNo");
            this.gcReferenceNo.MainView = this.gvReferenceNo;
            this.gcReferenceNo.Name = "gcReferenceNo";
            this.gcReferenceNo.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rglkpRefNumber,
            this.rtxtAmount,
            this.rbtn,
            this.rbeDeleteReference});
            this.gcReferenceNo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvReferenceNo});
            this.gcReferenceNo.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.gcReferenceNo_ProcessGridKey);
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1});
            this.contextMenuStrip1.Name = "cmsCostCentre";
            resources.ApplyResources(this.contextMenuStrip1, "contextMenuStrip1");
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.Image = global::ACPP.Properties.Resources.Delete;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            resources.ApplyResources(this.toolStripMenuItem1, "toolStripMenuItem1");
            // 
            // gvReferenceNo
            // 
            this.gvReferenceNo.Appearance.FocusedCell.BackColor = ((System.Drawing.Color)(resources.GetObject("gvReferenceNo.Appearance.FocusedCell.BackColor")));
            this.gvReferenceNo.Appearance.FocusedCell.Font = ((System.Drawing.Font)(resources.GetObject("gvReferenceNo.Appearance.FocusedCell.Font")));
            this.gvReferenceNo.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvReferenceNo.Appearance.FocusedCell.Options.UseFont = true;
            this.gvReferenceNo.Appearance.FocusedRow.BackColor2 = ((System.Drawing.Color)(resources.GetObject("gvReferenceNo.Appearance.FocusedRow.BackColor2")));
            this.gvReferenceNo.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvReferenceNo.Appearance.FocusedRow.Font")));
            this.gvReferenceNo.Appearance.FocusedRow.Options.UseFont = true;
            this.gvReferenceNo.Appearance.FooterPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvReferenceNo.Appearance.FooterPanel.Font")));
            this.gvReferenceNo.Appearance.FooterPanel.ForeColor = ((System.Drawing.Color)(resources.GetObject("gvReferenceNo.Appearance.FooterPanel.ForeColor")));
            this.gvReferenceNo.Appearance.FooterPanel.Options.UseFont = true;
            this.gvReferenceNo.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gvReferenceNo.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvReferenceNo.Appearance.HeaderPanel.Font")));
            this.gvReferenceNo.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvReferenceNo.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colRefNumber,
            this.colAmount,
            this.colValidationRefAmount,
            this.colLedgerId,
            this.colRecPayVoucherId,
            this.colVoucherDate,
            this.colDelete});
            this.gvReferenceNo.GridControl = this.gcReferenceNo;
            this.gvReferenceNo.Name = "gvReferenceNo";
            this.gvReferenceNo.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvReferenceNo.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvReferenceNo.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.Inplace;
            this.gvReferenceNo.OptionsBehavior.ImmediateUpdateRowPosition = false;
            this.gvReferenceNo.OptionsCustomization.AllowColumnMoving = false;
            this.gvReferenceNo.OptionsCustomization.AllowFilter = false;
            this.gvReferenceNo.OptionsCustomization.AllowGroup = false;
            this.gvReferenceNo.OptionsCustomization.AllowSort = false;
            this.gvReferenceNo.OptionsNavigation.EnterMoveNextColumn = true;
            this.gvReferenceNo.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gvReferenceNo.OptionsView.ShowFooter = true;
            this.gvReferenceNo.OptionsView.ShowGroupPanel = false;
            this.gvReferenceNo.OptionsView.ShowIndicator = false;
            this.gvReferenceNo.ShownEditor += new System.EventHandler(this.gvReferenceNo_ShownEditor);
            this.gvReferenceNo.FocusedColumnChanged += new DevExpress.XtraGrid.Views.Base.FocusedColumnChangedEventHandler(this.gvReferenceNo_FocusedColumnChanged);
            this.gvReferenceNo.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gvReferenceNo_ValidateRow);
            // 
            // colRefNumber
            // 
            resources.ApplyResources(this.colRefNumber, "colRefNumber");
            this.colRefNumber.ColumnEdit = this.rglkpRefNumber;
            this.colRefNumber.FieldName = "REF_VOUCHER_ID";
            this.colRefNumber.Name = "colRefNumber";
            // 
            // rglkpRefNumber
            // 
            this.rglkpRefNumber.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rglkpRefNumber.Buttons"))))});
            this.rglkpRefNumber.DisplayMember = "REFERENCE_NUMBER";
            this.rglkpRefNumber.ImmediatePopup = true;
            this.rglkpRefNumber.Name = "rglkpRefNumber";
            resources.ApplyResources(this.rglkpRefNumber, "rglkpRefNumber");
            this.rglkpRefNumber.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.rglkpRefNumber.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.rglkpRefNumber.ValueMember = "REF_VOUCHER_ID";
            this.rglkpRefNumber.View = this.repglkView;
            this.rglkpRefNumber.EditValueChanged += new System.EventHandler(this.rglkpRefNumber_EditValueChanged);
            // 
            // repglkView
            // 
            this.repglkView.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("repglkView.Appearance.HeaderPanel.Font")));
            this.repglkView.Appearance.HeaderPanel.Options.UseFont = true;
            this.repglkView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colcbRefVoucherId,
            this.colcbBalance,
            this.colcbTotal,
            this.colcbDate});
            this.repglkView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repglkView.Name = "repglkView";
            this.repglkView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repglkView.OptionsView.ShowGroupPanel = false;
            this.repglkView.OptionsView.ShowIndicator = false;
            // 
            // colcbRefVoucherId
            // 
            resources.ApplyResources(this.colcbRefVoucherId, "colcbRefVoucherId");
            this.colcbRefVoucherId.FieldName = "REFERENCE_NUMBER";
            this.colcbRefVoucherId.Name = "colcbRefVoucherId";
            // 
            // colcbBalance
            // 
            resources.ApplyResources(this.colcbBalance, "colcbBalance");
            this.colcbBalance.DisplayFormat.FormatString = "n";
            this.colcbBalance.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colcbBalance.FieldName = "BALANCE";
            this.colcbBalance.Name = "colcbBalance";
            // 
            // colcbTotal
            // 
            resources.ApplyResources(this.colcbTotal, "colcbTotal");
            this.colcbTotal.DisplayFormat.FormatString = "n";
            this.colcbTotal.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Custom;
            this.colcbTotal.FieldName = "TOTAL_REF_AMOUNT";
            this.colcbTotal.Name = "colcbTotal";
            // 
            // colcbDate
            // 
            resources.ApplyResources(this.colcbDate, "colcbDate");
            this.colcbDate.FieldName = "VOUCHER_DATE";
            this.colcbDate.Name = "colcbDate";
            // 
            // colAmount
            // 
            this.colAmount.AppearanceCell.BackColor = ((System.Drawing.Color)(resources.GetObject("colAmount.AppearanceCell.BackColor")));
            this.colAmount.AppearanceCell.Options.UseBackColor = true;
            this.colAmount.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colAmount.AppearanceHeader.Font")));
            this.colAmount.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colAmount, "colAmount");
            this.colAmount.ColumnEdit = this.rtxtAmount;
            this.colAmount.FieldName = "AMOUNT";
            this.colAmount.Name = "colAmount";
            this.colAmount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(((DevExpress.Data.SummaryItemType)(resources.GetObject("colAmount.Summary"))), resources.GetString("colAmount.Summary1"), resources.GetString("colAmount.Summary2"))});
            // 
            // rtxtAmount
            // 
            resources.ApplyResources(this.rtxtAmount, "rtxtAmount");
            this.rtxtAmount.Mask.EditMask = resources.GetString("rtxtAmount.Mask.EditMask");
            this.rtxtAmount.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("rtxtAmount.Mask.MaskType")));
            this.rtxtAmount.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("rtxtAmount.Mask.UseMaskAsDisplayFormat")));
            this.rtxtAmount.Name = "rtxtAmount";
            this.rtxtAmount.Enter += new System.EventHandler(this.rtxtAmount_Enter);
            this.rtxtAmount.Validating += new System.ComponentModel.CancelEventHandler(this.rtxtAmount_Validating);
            // 
            // colValidationRefAmount
            // 
            resources.ApplyResources(this.colValidationRefAmount, "colValidationRefAmount");
            this.colValidationRefAmount.DisplayFormat.FormatString = "{0:C}";
            this.colValidationRefAmount.DisplayFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colValidationRefAmount.FieldName = "REF_VALIDATION_AMOUNT";
            this.colValidationRefAmount.Name = "colValidationRefAmount";
            this.colValidationRefAmount.OptionsColumn.AllowEdit = false;
            this.colValidationRefAmount.OptionsColumn.AllowFocus = false;
            this.colValidationRefAmount.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colValidationRefAmount.OptionsColumn.AllowMove = false;
            this.colValidationRefAmount.OptionsColumn.AllowSize = false;
            this.colValidationRefAmount.OptionsColumn.TabStop = false;
            // 
            // colLedgerId
            // 
            resources.ApplyResources(this.colLedgerId, "colLedgerId");
            this.colLedgerId.FieldName = "LEDGER_ID";
            this.colLedgerId.Name = "colLedgerId";
            // 
            // colRecPayVoucherId
            // 
            resources.ApplyResources(this.colRecPayVoucherId, "colRecPayVoucherId");
            this.colRecPayVoucherId.FieldName = "REC_PAY_VOUCHER_ID";
            this.colRecPayVoucherId.Name = "colRecPayVoucherId";
            // 
            // colVoucherDate
            // 
            this.colVoucherDate.FieldName = "VOUCHER_DATE";
            this.colVoucherDate.Name = "colVoucherDate";
            // 
            // colDelete
            // 
            resources.ApplyResources(this.colDelete, "colDelete");
            this.colDelete.ColumnEdit = this.rbeDeleteReference;
            this.colDelete.Name = "colDelete";
            this.colDelete.OptionsColumn.AllowMove = false;
            this.colDelete.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colDelete.OptionsColumn.FixedWidth = true;
            this.colDelete.OptionsColumn.ShowCaption = false;
            this.colDelete.OptionsColumn.TabStop = false;
            // 
            // rbeDeleteReference
            // 
            resources.ApplyResources(this.rbeDeleteReference, "rbeDeleteReference");
            this.rbeDeleteReference.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rbeDeleteReference.Buttons"))), resources.GetString("rbeDeleteReference.Buttons1"), ((int)(resources.GetObject("rbeDeleteReference.Buttons2"))), ((bool)(resources.GetObject("rbeDeleteReference.Buttons3"))), ((bool)(resources.GetObject("rbeDeleteReference.Buttons4"))), ((bool)(resources.GetObject("rbeDeleteReference.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("rbeDeleteReference.Buttons6"))), global::ACPP.Properties.Resources.Delete_Mob, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, resources.GetString("rbeDeleteReference.Buttons7"), ((object)(resources.GetObject("rbeDeleteReference.Buttons8"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("rbeDeleteReference.Buttons9"))), ((bool)(resources.GetObject("rbeDeleteReference.Buttons10"))))});
            this.rbeDeleteReference.Name = "rbeDeleteReference";
            this.rbeDeleteReference.Click += new System.EventHandler(this.rbeDeleteReference_Click);
            // 
            // rbtn
            // 
            resources.ApplyResources(this.rbtn, "rbtn");
            this.rbtn.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.rbtn.Name = "rbtn";
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.layoutControlItem1,
            this.lblLedgerAmount,
            this.layoutControlItem3,
            this.layoutControlItem6,
            this.lblLedgerNameValue,
            this.lblLedgerName,
            this.simpleLabelItem2,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(409, 423);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.lblAmtCaption;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 23);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(127, 22);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcReferenceNo;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 45);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(409, 352);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // lblLedgerAmount
            // 
            this.lblLedgerAmount.AllowHotTrack = false;
            this.lblLedgerAmount.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblLedgerAmount.AppearanceItemCaption.Font")));
            this.lblLedgerAmount.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblLedgerAmount, "lblLedgerAmount");
            this.lblLedgerAmount.Location = new System.Drawing.Point(127, 23);
            this.lblLedgerAmount.Name = "lblLedgerAmount";
            this.lblLedgerAmount.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblLedgerAmount.Size = new System.Drawing.Size(282, 22);
            this.lblLedgerAmount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblLedgerAmount.TextSize = new System.Drawing.Size(50, 16);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnOk;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(279, 397);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(64, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(64, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(64, 26);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnCancel;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(343, 397);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(66, 26);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(66, 26);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(66, 26);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // lblLedgerNameValue
            // 
            this.lblLedgerNameValue.AllowHotTrack = false;
            this.lblLedgerNameValue.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblLedgerNameValue.AppearanceItemCaption.Font")));
            this.lblLedgerNameValue.AppearanceItemCaption.Options.UseFont = true;
            this.lblLedgerNameValue.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lblLedgerNameValue.AppearanceItemCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            resources.ApplyResources(this.lblLedgerNameValue, "lblLedgerNameValue");
            this.lblLedgerNameValue.Location = new System.Drawing.Point(126, 0);
            this.lblLedgerNameValue.MinSize = new System.Drawing.Size(162, 23);
            this.lblLedgerNameValue.Name = "lblLedgerNameValue";
            this.lblLedgerNameValue.Size = new System.Drawing.Size(283, 23);
            this.lblLedgerNameValue.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblLedgerNameValue.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lblLedgerNameValue.TextSize = new System.Drawing.Size(158, 19);
            // 
            // lblLedgerName
            // 
            this.lblLedgerName.AllowHotTrack = false;
            this.lblLedgerName.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblLedgerName.AppearanceItemCaption.Font")));
            this.lblLedgerName.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblLedgerName, "lblLedgerName");
            this.lblLedgerName.Location = new System.Drawing.Point(0, 0);
            this.lblLedgerName.MinSize = new System.Drawing.Size(126, 20);
            this.lblLedgerName.Name = "lblLedgerName";
            this.lblLedgerName.Size = new System.Drawing.Size(126, 23);
            this.lblLedgerName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblLedgerName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblLedgerName.TextSize = new System.Drawing.Size(122, 16);
            // 
            // simpleLabelItem2
            // 
            this.simpleLabelItem2.AllowHotTrack = false;
            this.simpleLabelItem2.AllowHtmlStringInCaption = true;
            resources.ApplyResources(this.simpleLabelItem2, "simpleLabelItem2");
            this.simpleLabelItem2.Location = new System.Drawing.Point(0, 397);
            this.simpleLabelItem2.Name = "simpleLabelItem2";
            this.simpleLabelItem2.Size = new System.Drawing.Size(216, 26);
            this.simpleLabelItem2.TextSize = new System.Drawing.Size(129, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(216, 397);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(63, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmTransactionReference
            // 
            this.Appearance.BorderColor = ((System.Drawing.Color)(resources.GetObject("frmTransactionReference.Appearance.BorderColor")));
            this.Appearance.Options.UseBorderColor = true;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.pnlCostCenter);
            this.MaximizeBox = false;
            this.Name = "frmTransactionReference";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTransactionReference_FormClosing);
            this.Load += new System.EventHandler(this.frmTransactionReference_Load);
            this.cmsCostCentre.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlCostCenter)).EndInit();
            this.pnlCostCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lctCostCenter)).EndInit();
            this.lctCostCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcReferenceNo)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvReferenceNo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpRefNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repglkView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbeDeleteReference)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLedgerAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLedgerNameValue)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLedgerName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ContextMenuStrip cmsCostCentre;
        private System.Windows.Forms.ToolStripMenuItem DeleteToolStripMenuCostCentre;
        private DevExpress.XtraGrid.Columns.GridColumn colcbRefNumber;        
        private DevExpress.XtraEditors.PanelControl pnlCostCenter;
        private DevExpress.XtraLayout.LayoutControl lctCostCenter;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.LabelControl lblAmtCaption;
        private DevExpress.XtraGrid.GridControl gcReferenceNo;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private DevExpress.XtraGrid.Views.Grid.GridView gvReferenceNo;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit rglkpRefNumber;
        private DevExpress.XtraGrid.Views.Grid.GridView repglkView;
        private DevExpress.XtraGrid.Columns.GridColumn colcbRefVoucherId;
        private DevExpress.XtraGrid.Columns.GridColumn colcbBalance;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colDelete;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbeDeleteReference;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbtn;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.SimpleLabelItem lblLedgerAmount;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.SimpleLabelItem lblLedgerNameValue;
        private DevExpress.XtraLayout.SimpleLabelItem lblLedgerName;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colcbTotal;
        private DevExpress.XtraGrid.Columns.GridColumn colcbDate;
        private DevExpress.XtraGrid.Columns.GridColumn colRefNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colValidationRefAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn colRecPayVoucherId;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherDate;
    }
}