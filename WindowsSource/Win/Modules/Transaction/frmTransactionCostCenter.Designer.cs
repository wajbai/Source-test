namespace ACPP.Modules.Transaction
{
    partial class frmTransactionCostCenter
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmTransactionCostCenter));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.pnlCostCenter = new DevExpress.XtraEditors.PanelControl();
            this.lctCostCenter = new DevExpress.XtraLayout.LayoutControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.lblCostCentre = new DevExpress.XtraEditors.LabelControl();
            this.gcCostCenter = new DevExpress.XtraGrid.GridControl();
            this.cmsCostCentre = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.DeleteToolStripMenuCostCentre = new System.Windows.Forms.ToolStripMenuItem();
            this.gvCostCenter = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCostCentreName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rglkpCostCenter = new DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit();
            this.repositoryItemGridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colCostCenterID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCostCentre = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rtxtAmount = new DevExpress.XtraEditors.Repository.RepositoryItemTextEdit();
            this.colDelete = new DevExpress.XtraGrid.Columns.GridColumn();
            this.rbeDeleteCostCentre = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.rbtn = new DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblLedgerAmount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblLedgerName = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem2 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.pnlCostCenter)).BeginInit();
            this.pnlCostCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lctCostCenter)).BeginInit();
            this.lctCostCenter.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcCostCenter)).BeginInit();
            this.cmsCostCentre.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gvCostCenter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpCostCenter)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbeDeleteCostCentre)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtn)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLedgerAmount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLedgerName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
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
            this.lctCostCenter.Controls.Add(this.lblCostCentre);
            this.lctCostCenter.Controls.Add(this.gcCostCenter);
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
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.StyleController = this.lctCostCenter;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // lblCostCentre
            // 
            this.lblCostCentre.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblCostCentre.Appearance.Font")));
            resources.ApplyResources(this.lblCostCentre, "lblCostCentre");
            this.lblCostCentre.Name = "lblCostCentre";
            this.lblCostCentre.StyleController = this.lctCostCenter;
            // 
            // gcCostCenter
            // 
            this.gcCostCenter.ContextMenuStrip = this.cmsCostCentre;
            resources.ApplyResources(this.gcCostCenter, "gcCostCenter");
            this.gcCostCenter.MainView = this.gvCostCenter;
            this.gcCostCenter.Name = "gcCostCenter";
            this.gcCostCenter.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.rglkpCostCenter,
            this.rtxtAmount,
            this.rbtn,
            this.rbeDeleteCostCentre});
            this.gcCostCenter.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCostCenter});
            this.gcCostCenter.ProcessGridKey += new System.Windows.Forms.KeyEventHandler(this.gcCostCenter_ProcessGridKey);
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
            // gvCostCenter
            // 
            this.gvCostCenter.Appearance.FocusedCell.BackColor = ((System.Drawing.Color)(resources.GetObject("gvCostCenter.Appearance.FocusedCell.BackColor")));
            this.gvCostCenter.Appearance.FocusedCell.Font = ((System.Drawing.Font)(resources.GetObject("gvCostCenter.Appearance.FocusedCell.Font")));
            this.gvCostCenter.Appearance.FocusedCell.Options.UseBackColor = true;
            this.gvCostCenter.Appearance.FocusedCell.Options.UseFont = true;
            this.gvCostCenter.Appearance.FocusedRow.BackColor2 = ((System.Drawing.Color)(resources.GetObject("gvCostCenter.Appearance.FocusedRow.BackColor2")));
            this.gvCostCenter.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvCostCenter.Appearance.FocusedRow.Font")));
            this.gvCostCenter.Appearance.FocusedRow.Options.UseFont = true;
            this.gvCostCenter.Appearance.FooterPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvCostCenter.Appearance.FooterPanel.Font")));
            this.gvCostCenter.Appearance.FooterPanel.ForeColor = ((System.Drawing.Color)(resources.GetObject("gvCostCenter.Appearance.FooterPanel.ForeColor")));
            this.gvCostCenter.Appearance.FooterPanel.Options.UseFont = true;
            this.gvCostCenter.Appearance.FooterPanel.Options.UseForeColor = true;
            this.gvCostCenter.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvCostCenter.Appearance.HeaderPanel.Font")));
            this.gvCostCenter.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvCostCenter.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCostCentreName,
            this.colAmount,
            this.colDelete});
            this.gvCostCenter.GridControl = this.gcCostCenter;
            this.gvCostCenter.Name = "gvCostCenter";
            this.gvCostCenter.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvCostCenter.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvCostCenter.OptionsBehavior.ImmediateUpdateRowPosition = false;
            this.gvCostCenter.OptionsNavigation.EnterMoveNextColumn = true;
            this.gvCostCenter.OptionsView.ShowButtonMode = DevExpress.XtraGrid.Views.Base.ShowButtonModeEnum.ShowAlways;
            this.gvCostCenter.OptionsView.ShowFooter = true;
            this.gvCostCenter.OptionsView.ShowGroupPanel = false;
            this.gvCostCenter.OptionsView.ShowIndicator = false;
            // 
            // colCostCentreName
            // 
            this.colCostCentreName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colCostCentreName.AppearanceHeader.Font")));
            this.colCostCentreName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colCostCentreName, "colCostCentreName");
            this.colCostCentreName.ColumnEdit = this.rglkpCostCenter;
            this.colCostCentreName.FieldName = "COST_CENTRE_ID";
            this.colCostCentreName.Name = "colCostCentreName";
            // 
            // rglkpCostCenter
            // 
            resources.ApplyResources(this.rglkpCostCenter, "rglkpCostCenter");
            this.rglkpCostCenter.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rglkpCostCenter.Buttons"))))});
            this.rglkpCostCenter.DisplayMember = "COST_CENTRE_NAME";
            this.rglkpCostCenter.ImmediatePopup = true;
            this.rglkpCostCenter.Name = "rglkpCostCenter";
            this.rglkpCostCenter.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.rglkpCostCenter.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.rglkpCostCenter.ValueMember = "COST_CENTRE_ID";
            this.rglkpCostCenter.View = this.repositoryItemGridLookUpEdit1View;
            this.rglkpCostCenter.Leave += new System.EventHandler(this.rglkpCostCenter_Leave);
            // 
            // repositoryItemGridLookUpEdit1View
            // 
            this.repositoryItemGridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colCostCenterID,
            this.colCostCentre});
            this.repositoryItemGridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.repositoryItemGridLookUpEdit1View.Name = "repositoryItemGridLookUpEdit1View";
            this.repositoryItemGridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.repositoryItemGridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // colCostCenterID
            // 
            resources.ApplyResources(this.colCostCenterID, "colCostCenterID");
            this.colCostCenterID.FieldName = "COST_CENTRE_ID";
            this.colCostCenterID.Name = "colCostCenterID";
            // 
            // colCostCentre
            // 
            resources.ApplyResources(this.colCostCentre, "colCostCentre");
            this.colCostCentre.FieldName = "COST_CENTRE_NAME";
            this.colCostCentre.Name = "colCostCentre";
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
            // 
            // colDelete
            // 
            resources.ApplyResources(this.colDelete, "colDelete");
            this.colDelete.ColumnEdit = this.rbeDeleteCostCentre;
            this.colDelete.Name = "colDelete";
            this.colDelete.OptionsColumn.FixedWidth = true;
            this.colDelete.OptionsColumn.ShowCaption = false;
            // 
            // rbeDeleteCostCentre
            // 
            resources.ApplyResources(this.rbeDeleteCostCentre, "rbeDeleteCostCentre");
            this.rbeDeleteCostCentre.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("rbeDeleteCostCentre.Buttons"))), resources.GetString("rbeDeleteCostCentre.Buttons1"), ((int)(resources.GetObject("rbeDeleteCostCentre.Buttons2"))), ((bool)(resources.GetObject("rbeDeleteCostCentre.Buttons3"))), ((bool)(resources.GetObject("rbeDeleteCostCentre.Buttons4"))), ((bool)(resources.GetObject("rbeDeleteCostCentre.Buttons5"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("rbeDeleteCostCentre.Buttons6"))), global::ACPP.Properties.Resources.Delete_Mob, new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, resources.GetString("rbeDeleteCostCentre.Buttons7"), ((object)(resources.GetObject("rbeDeleteCostCentre.Buttons8"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("rbeDeleteCostCentre.Buttons9"))), ((bool)(resources.GetObject("rbeDeleteCostCentre.Buttons10"))))});
            this.rbeDeleteCostCentre.Name = "rbeDeleteCostCentre";
            this.rbeDeleteCostCentre.Click += new System.EventHandler(this.rbeDeleteCostCentre_Click);
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
            this.emptySpaceItem2,
            this.emptySpaceItem3,
            this.lblLedgerAmount,
            this.layoutControlItem3,
            this.layoutControlItem6,
            this.lblLedgerName,
            this.simpleLabelItem1,
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
            this.layoutControlItem4.Control = this.lblCostCentre;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(97, 23);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(41, 22);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(41, 22);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(41, 22);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcCostCenter;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 45);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(409, 352);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 397);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(54, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem3, "emptySpaceItem3");
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 23);
            this.emptySpaceItem3.MaxSize = new System.Drawing.Size(97, 22);
            this.emptySpaceItem3.MinSize = new System.Drawing.Size(97, 22);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(97, 22);
            this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblLedgerAmount
            // 
            this.lblLedgerAmount.AllowHotTrack = false;
            this.lblLedgerAmount.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblLedgerAmount.AppearanceItemCaption.Font")));
            this.lblLedgerAmount.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblLedgerAmount, "lblLedgerAmount");
            this.lblLedgerAmount.Location = new System.Drawing.Point(138, 23);
            this.lblLedgerAmount.Name = "lblLedgerAmount";
            this.lblLedgerAmount.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblLedgerAmount.Size = new System.Drawing.Size(271, 22);
            this.lblLedgerAmount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblLedgerAmount.TextSize = new System.Drawing.Size(61, 19);
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
            // lblLedgerName
            // 
            this.lblLedgerName.AllowHotTrack = false;
            this.lblLedgerName.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblLedgerName.AppearanceItemCaption.Font")));
            this.lblLedgerName.AppearanceItemCaption.Options.UseFont = true;
            this.lblLedgerName.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lblLedgerName.AppearanceItemCaption.TextOptions.WordWrap = DevExpress.Utils.WordWrap.Wrap;
            resources.ApplyResources(this.lblLedgerName, "lblLedgerName");
            this.lblLedgerName.Location = new System.Drawing.Point(146, 0);
            this.lblLedgerName.Name = "lblLedgerName";
            this.lblLedgerName.Size = new System.Drawing.Size(263, 23);
            this.lblLedgerName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lblLedgerName.TextSize = new System.Drawing.Size(158, 19);
            // 
            // simpleLabelItem1
            // 
            this.simpleLabelItem1.AllowHotTrack = false;
            this.simpleLabelItem1.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("simpleLabelItem1.AppearanceItemCaption.Font")));
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.simpleLabelItem1, "simpleLabelItem1");
            this.simpleLabelItem1.Location = new System.Drawing.Point(0, 0);
            this.simpleLabelItem1.MaxSize = new System.Drawing.Size(146, 23);
            this.simpleLabelItem1.MinSize = new System.Drawing.Size(146, 23);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Size = new System.Drawing.Size(146, 23);
            this.simpleLabelItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.simpleLabelItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(135, 18);
            // 
            // simpleLabelItem2
            // 
            this.simpleLabelItem2.AllowHotTrack = false;
            this.simpleLabelItem2.AllowHtmlStringInCaption = true;
            resources.ApplyResources(this.simpleLabelItem2, "simpleLabelItem2");
            this.simpleLabelItem2.Location = new System.Drawing.Point(54, 397);
            this.simpleLabelItem2.Name = "simpleLabelItem2";
            this.simpleLabelItem2.Size = new System.Drawing.Size(162, 26);
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
            // frmTransactionCostCenter
            // 
            this.Appearance.BorderColor = ((System.Drawing.Color)(resources.GetObject("frmTransactionCostCenter.Appearance.BorderColor")));
            this.Appearance.Options.UseBorderColor = true;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.pnlCostCenter);
            this.Name = "frmTransactionCostCenter";
            this.Load += new System.EventHandler(this.frmTransactionCostCenter_Load);
            this.Shown += new System.EventHandler(this.frmTransactionCostCenter_Shown);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.frmTransactionCostCenter_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pnlCostCenter)).EndInit();
            this.pnlCostCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lctCostCenter)).EndInit();
            this.lctCostCenter.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcCostCenter)).EndInit();
            this.cmsCostCentre.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gvCostCenter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rglkpCostCenter)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.repositoryItemGridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rtxtAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbeDeleteCostCentre)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rbtn)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLedgerAmount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLedgerName)).EndInit();
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
        private DevExpress.XtraGrid.GridControl gcCostCenter;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCostCenter;
        private DevExpress.XtraGrid.Columns.GridColumn colCostCentreName;
        private DevExpress.XtraGrid.Columns.GridColumn colAmount;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.Repository.RepositoryItemGridLookUpEdit rglkpCostCenter;
        private DevExpress.XtraGrid.Views.Grid.GridView repositoryItemGridLookUpEdit1View;
        private DevExpress.XtraEditors.Repository.RepositoryItemTextEdit rtxtAmount;
        private System.Windows.Forms.ContextMenuStrip cmsCostCentre;
        private System.Windows.Forms.ToolStripMenuItem DeleteToolStripMenuCostCentre;
        private DevExpress.XtraGrid.Columns.GridColumn colCostCenterID;
        private DevExpress.XtraGrid.Columns.GridColumn colCostCentre;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.SimpleLabelItem lblLedgerAmount;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraGrid.Columns.GridColumn colDelete;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbeDeleteCostCentre;
        private DevExpress.XtraEditors.Repository.RepositoryItemButtonEdit rbtn;
        private DevExpress.XtraLayout.SimpleLabelItem lblLedgerName;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}