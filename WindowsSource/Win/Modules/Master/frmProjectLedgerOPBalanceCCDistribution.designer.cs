namespace ACPP.Modules.Master
{
    partial class frmProjectLedgerOPBalanceCCDistribution
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
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject2 = new DevExpress.Utils.SerializableAppearanceObject();
            this.pnlCostCenter = new DevExpress.XtraEditors.PanelControl();
            this.lctCostCenter = new DevExpress.XtraLayout.LayoutControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.lblCostCentre = new DevExpress.XtraEditors.LabelControl();
            this.gcProjectLedgerCostCentre = new DevExpress.XtraGrid.GridControl();
            this.cmsCostCentre = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteToolStripMenuCostCentre = new System.Windows.Forms.ToolStripMenuItem();
            this.gvProjectLedgerCostCentre = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCCName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rglkpCC = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.rglkpView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colrepCCId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colrepCCName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtAmount = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rbeDeleteCC = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.rbtn = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblLedgerAmount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblCCName = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem2 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCostCenter)).BeginInit();
            this.pnlCostCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lctCostCenter)).BeginInit();
            this.lctCostCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcProjectLedgerCostCentre)).BeginInit();
            this.cmsCostCentre.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvProjectLedgerCostCentre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpCC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbeDeleteCC)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLedgerAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCCName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // pnlCostCenter
            // 
            this.pnlCostCenter.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.pnlCostCenter.Controls.Add(this.lctCostCenter);
            this.pnlCostCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlCostCenter.Location = new System.Drawing.Point(5, 5);
            this.pnlCostCenter.Name = "pnlCostCenter";
            this.pnlCostCenter.Size = new System.Drawing.Size(409, 423);
            this.pnlCostCenter.TabIndex = 0;
            // 
            // lctCostCenter
            // 
            this.lctCostCenter.AllowCustomizationMenu = false;
            this.lctCostCenter.Controls.Add(this.btnCancel);
            this.lctCostCenter.Controls.Add(this.btnOk);
            this.lctCostCenter.Controls.Add(this.lblCostCentre);
            this.lctCostCenter.Controls.Add(this.gcProjectLedgerCostCentre);
            this.lctCostCenter.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lctCostCenter.Location = new System.Drawing.Point(0, 0);
            this.lctCostCenter.Name = "lctCostCenter";
            this.lctCostCenter.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(487, 109, 449, 469);
            this.lctCostCenter.Root = this.layoutControlGroup1;
            this.lctCostCenter.Size = new System.Drawing.Size(409, 423);
            this.lctCostCenter.TabIndex = 0;
            this.lctCostCenter.Text = "layoutControl1";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(345, 399);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(62, 22);
            this.btnCancel.StyleController = this.lctCostCenter;
            this.btnCancel.TabIndex = 10;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            this.btnOk.Location = new System.Drawing.Point(281, 399);
            this.btnOk.Name = "btnOk";
            this.btnOk.Size = new System.Drawing.Size(60, 22);
            this.btnOk.StyleController = this.lctCostCenter;
            this.btnOk.TabIndex = 1;
            this.btnOk.Text = "&Ok";
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblCostCentre
            // 
            this.lblCostCentre.Appearance.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCostCentre.Location = new System.Drawing.Point(55, 25);
            this.lblCostCentre.Name = "lblCostCentre";
            this.lblCostCentre.Size = new System.Drawing.Size(45, 18);
            this.lblCostCentre.StyleController = this.lctCostCenter;
            this.lblCostCentre.TabIndex = 7;
            this.lblCostCentre.Text = "Upto : ";
            // 
            // gcProjectLedgerCostCentre
            // 
            this.gcProjectLedgerCostCentre.ContextMenuStrip = this.cmsCostCentre;
            this.gcProjectLedgerCostCentre.Location = new System.Drawing.Point(2, 47);
            this.gcProjectLedgerCostCentre.MainView = this.gvProjectLedgerCostCentre;
            this.gcProjectLedgerCostCentre.Name = "gcProjectLedgerCostCentre";
            this.gcProjectLedgerCostCentre.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rglkpCC,
            this.rtxtAmount,
            this.rbtn,
            this.rbeDeleteCC});
            this.gcProjectLedgerCostCentre.Size = new System.Drawing.Size(405, 348);
            this.gcProjectLedgerCostCentre.TabIndex = 0;
            this.gcProjectLedgerCostCentre.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvProjectLedgerCostCentre});
            this.gcProjectLedgerCostCentre.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.gcProjectLedgerCostCentre_ProcessGridKey);
            // 
            // cmsCostCentre
            // 
            this.cmsCostCentre.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DeleteToolStripMenuCostCentre});
            this.cmsCostCentre.Name = "cmsCostCentre";
            this.cmsCostCentre.Size = new System.Drawing.Size(108, 26);
            // 
            // DeleteToolStripMenuCostCentre
            // 
            this.DeleteToolStripMenuCostCentre.Image = global::ACPP.Properties.Resources.Delete;
            this.DeleteToolStripMenuCostCentre.Name = "DeleteToolStripMenuCostCentre";
            this.DeleteToolStripMenuCostCentre.Size = new System.Drawing.Size(107, 22);
            this.DeleteToolStripMenuCostCentre.Text = "Delete";
            // 
            // gvProjectLedgerCostCentre
            // 
            this.gvProjectLedgerCostCentre.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvProjectLedgerCostCentre.Appearance.FocusedCell.Options.UseFont = true;
            this.gvProjectLedgerCostCentre.Appearance.FocusedRow.Options.UseFont = true;
            this.gvProjectLedgerCostCentre.Appearance.FooterPanel.Options.UseFont = true;
            this.gvProjectLedgerCostCentre.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gvProjectLedgerCostCentre.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvProjectLedgerCostCentre.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCCName,
            this.colAmount,
            this.colDelete});
            this.gvProjectLedgerCostCentre.GridControl = this.gcProjectLedgerCostCentre;
            this.gvProjectLedgerCostCentre.Name = "gvProjectLedgerCostCentre";
            this.gvProjectLedgerCostCentre.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvProjectLedgerCostCentre.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvProjectLedgerCostCentre.OptionsBehavior.ImmediateUpdateRowPosition = false;
            this.gvProjectLedgerCostCentre.OptionsNavigation.EnterMoveNextColumn = true;
            this.gvProjectLedgerCostCentre.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gvProjectLedgerCostCentre.OptionsView.ShowFooter = true;
            this.gvProjectLedgerCostCentre.OptionsView.ShowGroupPanel = false;
            this.gvProjectLedgerCostCentre.OptionsView.ShowIndicator = false;
            // 
            // colCCName
            // 
            this.colCCName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colCCName.AppearanceHeader.Options.UseFont = true;
            this.colCCName.Caption = "Cost Centre";
            this.colCCName.ColumnEdit = this.rglkpCC;
            this.colCCName.FieldName = "COST_CENTRE_ID";
            this.colCCName.Name = "colCCName";
            this.colCCName.Visible = true;
            this.colCCName.VisibleIndex = 0;
            this.colCCName.Width = 267;
            // 
            // rglkpCC
            // 
            this.rglkpCC.AutoHeight = false;
            this.rglkpCC.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.rglkpCC.DisplayMember = "COST_CENTRE_NAME";
            this.rglkpCC.ImmediatePopup = true;
            this.rglkpCC.Name = "rglkpCC";
            this.rglkpCC.NullText = "";
            this.rglkpCC.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.rglkpCC.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.rglkpCC.ValueMember = "COST_CENTRE_Id";
            this.rglkpCC.View = this.rglkpView;
            this.rglkpCC.Leave += new System.EventHandler(this.rglkpCostCenter_Leave);
            // 
            // rglkpView
            // 
            this.rglkpView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colrepCCId,
            this.colrepCCName});
            this.rglkpView.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.rglkpView.Name = "rglkpView";
            this.rglkpView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.rglkpView.OptionsView.ShowColumnHeaders = false;
            this.rglkpView.OptionsView.ShowGroupPanel = false;
            this.rglkpView.OptionsView.ShowIndicator = false;
            // 
            // colrepCCId
            // 
            this.colrepCCId.Caption = "CostCentreId";
            this.colrepCCId.FieldName = "COST_CENTRE_ID";
            this.colrepCCId.Name = "colrepCCId";
            // 
            // colrepCCName
            // 
            this.colrepCCName.Caption = "Ledger Name";
            this.colrepCCName.FieldName = "COST_CENTRE_NAME";
            this.colrepCCName.Name = "colrepCCName";
            this.colrepCCName.Visible = true;
            this.colrepCCName.VisibleIndex = 0;
            // 
            // colAmount
            // 
            this.colAmount.AppearanceCell.BackColor = System.Drawing.Color.LightYellow;
            this.colAmount.AppearanceCell.Options.UseBackColor = true;
            this.colAmount.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colAmount.AppearanceHeader.Options.UseFont = true;
            this.colAmount.Caption = "Amount";
            this.colAmount.ColumnEdit = this.rtxtAmount;
            this.colAmount.FieldName = "AMOUNT";
            this.colAmount.Name = "colAmount";
            this.colAmount.Summary.AddRange(new DevExpress.XtraGrid.GridSummaryItem[] {
            new DevExpress.XtraGrid.GridColumnSummaryItem(DevExpress.Data.SummaryItemType.Sum, "AMOUNT", "{0:C}")});
            this.colAmount.Visible = true;
            this.colAmount.VisibleIndex = 1;
            this.colAmount.Width = 110;
            // 
            // rtxtAmount
            // 
            this.rtxtAmount.AutoHeight = false;
            this.rtxtAmount.Mask.EditMask = "n";
            this.rtxtAmount.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.Numeric;
            this.rtxtAmount.Mask.UseMaskAsDisplayFormat = true;
            this.rtxtAmount.Name = "rtxtAmount";
            // 
            // colDelete
            // 
            this.colDelete.Caption = "Delete";
            this.colDelete.ColumnEdit = this.rbeDeleteCC;
            this.colDelete.Name = "colDelete";
            this.colDelete.OptionsColumn.FixedWidth = true;
            this.colDelete.OptionsColumn.ShowCaption = false;
            this.colDelete.Visible = true;
            this.colDelete.VisibleIndex = 2;
            this.colDelete.Width = 26;
            // 
            // rbeDeleteCC
            // 
            this.rbeDeleteCC.AutoHeight = false;
            this.rbeDeleteCC.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Glyph, "", -1, true, true, false, DevExpress.XtraEditors.ImageLocation.MiddleCenter, global::ACPP.Properties.Resources.Delete_Mob, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject2, "", null, null, true)});
            this.rbeDeleteCC.Name = "rbeDeleteCC";
            this.rbeDeleteCC.Click += new System.EventHandler(this.rbeDeleteCostCentre_Click);
            // 
            // rbtn
            // 
            this.rbtn.AutoHeight = false;
            this.rbtn.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.rbtn.Name = "rbtn";
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem4,
            this.layoutControlItem1,
            this.emptySpaceItem2,
            this.emptySpaceItem3,
            this.lblLedgerAmount,
            this.layoutControlItem3,
            this.layoutControlItem6,
            this.lblCCName,
            this.simpleLabelItem1,
            this.simpleLabelItem2,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(409, 423);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.lblCostCentre;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(53, 23);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(49, 22);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(49, 22);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(49, 22);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcProjectLedgerCostCentre;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 45);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(409, 352);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 397);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(54, 26);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 23);
            this.emptySpaceItem3.MaxSize = new System.Drawing.Size(53, 22);
            this.emptySpaceItem3.MinSize = new System.Drawing.Size(53, 22);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(53, 22);
            this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblLedgerAmount
            // 
            this.lblLedgerAmount.AllowHotTrack = false;
            this.lblLedgerAmount.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblLedgerAmount.AppearanceItemCaption.Options.UseFont = true;
            this.lblLedgerAmount.CustomizationFormText = "Amount";
            this.lblLedgerAmount.Location = new System.Drawing.Point(102, 23);
            this.lblLedgerAmount.MinSize = new System.Drawing.Size(54, 20);
            this.lblLedgerAmount.Name = "lblLedgerAmount";
            this.lblLedgerAmount.Size = new System.Drawing.Size(307, 22);
            this.lblLedgerAmount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblLedgerAmount.Text = "Amount";
            this.lblLedgerAmount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblLedgerAmount.TextSize = new System.Drawing.Size(50, 14);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnOk;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(279, 397);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(64, 26);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(64, 26);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(64, 26);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnCancel;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(343, 397);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(66, 26);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(66, 26);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(66, 26);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // lblCCName
            // 
            this.lblCCName.AllowHotTrack = false;
            this.lblCCName.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCCName.AppearanceItemCaption.Options.UseFont = true;
            this.lblCCName.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lblCCName.AppearanceItemCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            this.lblCCName.CustomizationFormText = "Purpose";
            this.lblCCName.Location = new System.Drawing.Point(102, 0);
            this.lblCCName.Name = "lblCCName";
            this.lblCCName.Size = new System.Drawing.Size(307, 23);
            this.lblCCName.Text = "Purpose";
            this.lblCCName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lblCCName.TextSize = new System.Drawing.Size(158, 19);
            // 
            // simpleLabelItem1
            // 
            this.simpleLabelItem1.AllowHotTrack = false;
            this.simpleLabelItem1.AppearanceItemCaption.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem1.CustomizationFormText = "Amount Allocation for :";
            this.simpleLabelItem1.Location = new System.Drawing.Point(0, 0);
            this.simpleLabelItem1.MaxSize = new System.Drawing.Size(102, 23);
            this.simpleLabelItem1.MinSize = new System.Drawing.Size(102, 23);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Size = new System.Drawing.Size(102, 23);
            this.simpleLabelItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.simpleLabelItem1.Text = "Allocation for :";
            this.simpleLabelItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(98, 18);
            // 
            // simpleLabelItem2
            // 
            this.simpleLabelItem2.AllowHotTrack = false;
            this.simpleLabelItem2.AllowHtmlStringInCaption = true;
            this.simpleLabelItem2.CustomizationFormText = " <b><color=\"Blue\">  Alt + D </color> Delete Trans </b>";
            this.simpleLabelItem2.Location = new System.Drawing.Point(54, 397);
            this.simpleLabelItem2.Name = "simpleLabelItem2";
            this.simpleLabelItem2.Size = new System.Drawing.Size(162, 26);
            this.simpleLabelItem2.Text = " <b><color=\"Blue\">  Alt + D </color> Delete Trans </b>";
            this.simpleLabelItem2.TextSize = new System.Drawing.Size(129, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(216, 397);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(63, 26);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmProjectLedgerOPBalanceCCDistribution
            // 
            this.Appearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(192)))), ((int)(((byte)(192)))));
            this.Appearance.Options.UseBorderColor = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(419, 433);
            this.Controls.Add(this.pnlCostCenter);
            this.Location = new System.Drawing.Point(250, 600);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmProjectLedgerOPBalanceCCDistribution";
            this.Text = "Distribute Project Ledger Opening Balance";
            this.Load += new System.EventHandler(this.frmTransactionCostCenter_Load);
            this.Shown += new System.EventHandler(this.frmTransactionCostCenter_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmTransactionCostCenter_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pnlCostCenter)).EndInit();
            this.pnlCostCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lctCostCenter)).EndInit();
            this.lctCostCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcProjectLedgerCostCentre)).EndInit();
            this.cmsCostCentre.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvProjectLedgerCostCentre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpCC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbeDeleteCC)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLedgerAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCCName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraEditors.PanelControl pnlCostCenter;
        private DevExpress.XtraLayout.LayoutControl lctCostCenter;
        private DevExpress.XtraEditors.LabelControl lblCostCentre;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.GridControl gcProjectLedgerCostCentre;
        private DevExpress.XtraGrid.Views.Grid.GridView gvProjectLedgerCostCentre;
        private DevExpress.XtraGrid.Columns.GridColumn colCCName;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit rglkpCC;
        private DevExpress.XtraGrid.Views.Grid.GridView rglkpView;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtAmount;
        private System.Windows.Forms.ContextMenuStrip cmsCostCentre;
        private System.Windows.Forms.ToolStripMenuItem DeleteToolStripMenuCostCentre;
        private DevExpress.XtraGrid.Columns.GridColumn colrepCCId;
        private DevExpress.XtraGrid.Columns.GridColumn colrepCCName;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.SimpleLabelItem lblLedgerAmount;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraGrid.Columns.GridColumn colDelete;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbeDeleteCC;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbtn;
        private DevExpress.XtraLayout.SimpleLabelItem lblCCName;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}