namespace ACPP.Modules.Transaction
{
    partial class frmTransactionSubLedger
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
                if (SubLedgersVoucher != null)
                {
                    SubLedgersVoucher.Dispose();
                    SubLedgersVoucher = null;
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTransactionSubLedger));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.cmsCostCentre = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteToolStripMenuCostCentre = new System.Windows.Forms.ToolStripMenuItem();
            this.colcbRefNumber = new DevExpress.XtraGrid.Columns.GridColumn();
            this.pnlCostCenter = new DevExpress.XtraEditors.PanelControl();
            this.lctCostCenter = new DevExpress.XtraLayout.LayoutControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.lblAmtCaption = new DevExpress.XtraEditors.LabelControl();
            this.gcSubLedgerVouchers = new DevExpress.XtraGrid.GridControl();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.gvSubLedgerVouchers = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colVoucherId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubLedger = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtAmount = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colBudgetVariance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubLegerBalance = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colBudgetAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTmpAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rbeDeleteReference = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
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
            ((System.ComponentModel.ISupportInitialize)(this.gcSubLedgerVouchers)).BeginInit();
            this.contextMenuStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvSubLedgerVouchers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbeDeleteReference)).BeginInit();
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
            this.lctCostCenter.Controls.Add(this.gcSubLedgerVouchers);
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
            // gcSubLedgerVouchers
            // 
            this.gcSubLedgerVouchers.ContextMenuStrip = this.contextMenuStrip1;
            resources.ApplyResources(this.gcSubLedgerVouchers, "gcSubLedgerVouchers");
            this.gcSubLedgerVouchers.MainView = this.gvSubLedgerVouchers;
            this.gcSubLedgerVouchers.Name = "gcSubLedgerVouchers";
            this.gcSubLedgerVouchers.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rtxtAmount,
            this.rbeDeleteReference});
            this.gcSubLedgerVouchers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSubLedgerVouchers});
            this.gcSubLedgerVouchers.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.gcReferenceNo_ProcessGridKey);
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
            // gvSubLedgerVouchers
            // 
            this.gvSubLedgerVouchers.Appearance.FocusedCell.BackColor = ((System.Drawing.Color)(resources.GetObject("gvSubLedgerVouchers.Appearance.FocusedCell.BackColor")));
            this.gvSubLedgerVouchers.Appearance.FocusedCell.Font = ((System.Drawing.Font)(resources.GetObject("gvSubLedgerVouchers.Appearance.FocusedCell.Font")));
            this.gvSubLedgerVouchers.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvSubLedgerVouchers.Appearance.FocusedCell.Options.UseFont = true;
            this.gvSubLedgerVouchers.Appearance.FocusedRow.BackColor2 = ((System.Drawing.Color)(resources.GetObject("gvSubLedgerVouchers.Appearance.FocusedRow.BackColor2")));
            this.gvSubLedgerVouchers.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvSubLedgerVouchers.Appearance.FocusedRow.Font")));
            this.gvSubLedgerVouchers.Appearance.FocusedRow.Options.UseFont = true;
            this.gvSubLedgerVouchers.Appearance.FooterPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvSubLedgerVouchers.Appearance.FooterPanel.Font")));
            this.gvSubLedgerVouchers.Appearance.FooterPanel.ForeColor = ((System.Drawing.Color)(resources.GetObject("gvSubLedgerVouchers.Appearance.FooterPanel.ForeColor")));
            this.gvSubLedgerVouchers.Appearance.FooterPanel.Options.UseFont = true;
            this.gvSubLedgerVouchers.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gvSubLedgerVouchers.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvSubLedgerVouchers.Appearance.HeaderPanel.Font")));
            this.gvSubLedgerVouchers.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvSubLedgerVouchers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colVoucherId,
            this.colLedgerId,
            this.colSubLedgerId,
            this.colSubLedger,
            this.colAmount,
            this.colBudgetVariance,
            this.colSubLegerBalance,
            this.colBudgetAmount,
            this.colTmpAmount,
            this.colDelete});
            this.gvSubLedgerVouchers.GridControl = this.gcSubLedgerVouchers;
            this.gvSubLedgerVouchers.Name = "gvSubLedgerVouchers";
            this.gvSubLedgerVouchers.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvSubLedgerVouchers.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvSubLedgerVouchers.OptionsBehavior.EditingMode = DevExpress.XtraGrid.Views.Grid.GridEditingMode.Inplace;
            this.gvSubLedgerVouchers.OptionsBehavior.ImmediateUpdateRowPosition = false;
            this.gvSubLedgerVouchers.OptionsCustomization.AllowColumnMoving = false;
            this.gvSubLedgerVouchers.OptionsCustomization.AllowFilter = false;
            this.gvSubLedgerVouchers.OptionsCustomization.AllowGroup = false;
            this.gvSubLedgerVouchers.OptionsCustomization.AllowSort = false;
            this.gvSubLedgerVouchers.OptionsNavigation.EnterMoveNextColumn = true;
            this.gvSubLedgerVouchers.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gvSubLedgerVouchers.OptionsView.ShowFooter = true;
            this.gvSubLedgerVouchers.OptionsView.ShowGroupPanel = false;
            this.gvSubLedgerVouchers.OptionsView.ShowIndicator = false;
            this.gvSubLedgerVouchers.ShownEditor += new System.EventHandler(this.gvReferenceNo_ShownEditor);
            this.gvSubLedgerVouchers.ValidateRow += new DevExpress.XtraGrid.Views.Base.ValidateRowEventHandler(this.gvReferenceNo_ValidateRow);
            // 
            // colVoucherId
            // 
            resources.ApplyResources(this.colVoucherId, "colVoucherId");
            this.colVoucherId.FieldName = "VOUCHER_ID";
            this.colVoucherId.Name = "colVoucherId";
            this.colVoucherId.OptionsColumn.AllowEdit = false;
            this.colVoucherId.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colVoucherId.OptionsColumn.AllowIncrementalSearch = false;
            this.colVoucherId.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colVoucherId.OptionsColumn.AllowMove = false;
            this.colVoucherId.OptionsColumn.AllowSize = false;
            this.colVoucherId.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colVoucherId.OptionsColumn.FixedWidth = true;
            this.colVoucherId.OptionsColumn.ReadOnly = true;
            // 
            // colLedgerId
            // 
            resources.ApplyResources(this.colLedgerId, "colLedgerId");
            this.colLedgerId.FieldName = "LEDGER_ID";
            this.colLedgerId.Name = "colLedgerId";
            // 
            // colSubLedgerId
            // 
            resources.ApplyResources(this.colSubLedgerId, "colSubLedgerId");
            this.colSubLedgerId.FieldName = "SUB_LEDGER_ID";
            this.colSubLedgerId.Name = "colSubLedgerId";
            // 
            // colSubLedger
            // 
            resources.ApplyResources(this.colSubLedger, "colSubLedger");
            this.colSubLedger.FieldName = "SUB_LEDGER_NAME";
            this.colSubLedger.Name = "colSubLedger";
            this.colSubLedger.OptionsColumn.AllowEdit = false;
            this.colSubLedger.OptionsColumn.AllowFocus = false;
            this.colSubLedger.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colSubLedger.OptionsColumn.AllowIncrementalSearch = false;
            this.colSubLedger.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colSubLedger.OptionsColumn.AllowMove = false;
            this.colSubLedger.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colSubLedger.OptionsColumn.ReadOnly = true;
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
            this.colAmount.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colAmount.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colAmount.OptionsColumn.AllowSize = false;
            this.colAmount.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colAmount.OptionsColumn.FixedWidth = true;
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
            this.rtxtAmount.Leave += new System.EventHandler(this.rtxtAmount_Leave);
            // 
            // colBudgetVariance
            // 
            this.colBudgetVariance.AppearanceCell.Font = ((System.Drawing.Font)(resources.GetObject("colBudgetVariance.AppearanceCell.Font")));
            this.colBudgetVariance.AppearanceCell.Options.UseFont = true;
            resources.ApplyResources(this.colBudgetVariance, "colBudgetVariance");
            this.colBudgetVariance.FieldName = "BUDGET_VARIANCE";
            this.colBudgetVariance.Name = "colBudgetVariance";
            this.colBudgetVariance.OptionsColumn.AllowEdit = false;
            this.colBudgetVariance.OptionsColumn.AllowFocus = false;
            this.colBudgetVariance.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colBudgetVariance.OptionsColumn.AllowIncrementalSearch = false;
            this.colBudgetVariance.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colBudgetVariance.OptionsColumn.AllowMove = false;
            this.colBudgetVariance.OptionsColumn.AllowSize = false;
            this.colBudgetVariance.OptionsColumn.AllowSort = DevExpress.Utils.DefaultBoolean.False;
            this.colBudgetVariance.OptionsColumn.FixedWidth = true;
            this.colBudgetVariance.OptionsColumn.ReadOnly = true;
            // 
            // colSubLegerBalance
            // 
            resources.ApplyResources(this.colSubLegerBalance, "colSubLegerBalance");
            this.colSubLegerBalance.FieldName = "SUB_LEDGER_BALANCE";
            this.colSubLegerBalance.Name = "colSubLegerBalance";
            // 
            // colBudgetAmount
            // 
            resources.ApplyResources(this.colBudgetAmount, "colBudgetAmount");
            this.colBudgetAmount.FieldName = "BUDGET_AMOUNT";
            this.colBudgetAmount.Name = "colBudgetAmount";
            this.colBudgetAmount.OptionsColumn.AllowEdit = false;
            this.colBudgetAmount.OptionsColumn.AllowFocus = false;
            this.colBudgetAmount.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colBudgetAmount.OptionsColumn.AllowIncrementalSearch = false;
            this.colBudgetAmount.OptionsColumn.AllowMerge = DevExpress.Utils.DefaultBoolean.False;
            this.colBudgetAmount.OptionsColumn.AllowMove = false;
            this.colBudgetAmount.OptionsColumn.ReadOnly = true;
            // 
            // colTmpAmount
            // 
            resources.ApplyResources(this.colTmpAmount, "colTmpAmount");
            this.colTmpAmount.FieldName = "TMP_AMOUNT";
            this.colTmpAmount.Name = "colTmpAmount";
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
            this.layoutControlGroup1.Size = new System.Drawing.Size(392, 423);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.lblAmtCaption;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(0, 23);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(100, 22);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcSubLedgerVouchers;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 45);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(392, 352);
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
            this.lblLedgerAmount.Location = new System.Drawing.Point(100, 23);
            this.lblLedgerAmount.Name = "lblLedgerAmount";
            this.lblLedgerAmount.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 0, 0, 0);
            this.lblLedgerAmount.Size = new System.Drawing.Size(292, 22);
            this.lblLedgerAmount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblLedgerAmount.TextSize = new System.Drawing.Size(50, 16);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnOk;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(262, 397);
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
            this.layoutControlItem6.Location = new System.Drawing.Point(326, 397);
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
            this.lblLedgerNameValue.Location = new System.Drawing.Point(100, 0);
            this.lblLedgerNameValue.MinSize = new System.Drawing.Size(162, 23);
            this.lblLedgerNameValue.Name = "lblLedgerNameValue";
            this.lblLedgerNameValue.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 2, 2, 2);
            this.lblLedgerNameValue.Size = new System.Drawing.Size(292, 23);
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
            this.lblLedgerName.MinSize = new System.Drawing.Size(90, 20);
            this.lblLedgerName.Name = "lblLedgerName";
            this.lblLedgerName.Size = new System.Drawing.Size(100, 23);
            this.lblLedgerName.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblLedgerName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblLedgerName.TextSize = new System.Drawing.Size(95, 16);
            // 
            // simpleLabelItem2
            // 
            this.simpleLabelItem2.AllowHotTrack = false;
            this.simpleLabelItem2.AllowHtmlStringInCaption = true;
            resources.ApplyResources(this.simpleLabelItem2, "simpleLabelItem2");
            this.simpleLabelItem2.Location = new System.Drawing.Point(0, 397);
            this.simpleLabelItem2.Name = "simpleLabelItem2";
            this.simpleLabelItem2.Size = new System.Drawing.Size(207, 26);
            this.simpleLabelItem2.TextSize = new System.Drawing.Size(129, 13);
            this.simpleLabelItem2.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(207, 397);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(55, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmTransactionSubLedger
            // 
            this.Appearance.BorderColor = ((System.Drawing.Color)(resources.GetObject("frmTransactionSubLedger.Appearance.BorderColor")));
            this.Appearance.Options.UseBorderColor = true;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.pnlCostCenter);
            this.MaximizeBox = false;
            this.Name = "frmTransactionSubLedger";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmTransactionReference_FormClosing);
            this.Load += new System.EventHandler(this.frmTransactionReference_Load);
            this.cmsCostCentre.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.pnlCostCenter)).EndInit();
            this.pnlCostCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lctCostCenter)).EndInit();
            this.lctCostCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcSubLedgerVouchers)).EndInit();
            this.contextMenuStrip1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvSubLedgerVouchers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbeDeleteReference)).EndInit();
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
        private DevExpress.XtraGrid.GridControl gcSubLedgerVouchers;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSubLedgerVouchers;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colDelete;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbeDeleteReference;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.SimpleLabelItem lblLedgerAmount;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.SimpleLabelItem lblLedgerNameValue;
        private DevExpress.XtraLayout.SimpleLabelItem lblLedgerName;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colSubLedger;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn colVoucherId;
        private DevExpress.XtraGrid.Columns.GridColumn colBudgetAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colSubLedgerId;
        private DevExpress.XtraEditors.LabelControl lblAmtCaption;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Columns.GridColumn colSubLegerBalance;
        private DevExpress.XtraGrid.Columns.GridColumn colBudgetVariance;
        private DevExpress.XtraGrid.Columns.GridColumn colTmpAmount;
    }
}