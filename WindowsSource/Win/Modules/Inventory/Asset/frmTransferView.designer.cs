namespace ACPP.Modules.Asset.Transactions
{
    partial class frmTransferView
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
            DevExpress.XtraGrid.GridLevelNode gridLevelNode2 = new DevExpress.XtraGrid.GridLevelNode();
            this.gvTransferSubView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSubTransferId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSubASSETID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFromLocation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTooLocation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcTransferView = new DevExpress.XtraGrid.GridControl();
            this.gvTransferView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colTransferId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRefrenceId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAssetGroup = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTolocation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAssetName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colNameAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRemarks = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colItemid = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFromLocationId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnApply = new DevExpress.XtraEditors.SimpleButton();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.glkpProjects = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.deTodate = new DevExpress.XtraEditors.DateEdit();
            this.deFromDate = new DevExpress.XtraEditors.DateEdit();
            this.ucTransfer = new Bosco.Utility.Controls.ucToolBar();
            this.lblProject = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem6 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblFromDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblToDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblCountSimbol = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblNumbercount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.ucTransviewClosingBalance1 = new ACPP.Modules.UIControls.ucTransviewClosingBalance();
            this.ucTransViewOpeningBalDetails1 = new ACPP.Modules.UIControls.ucTransViewOpeningBalDetails();
            ((System.ComponentModel.ISupportInitialize)(this.gvTransferSubView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTransferView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTransferView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpProjects.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTodate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTodate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFromDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFromDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblProject)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFromDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblToDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCountSimbol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNumbercount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // gvTransferSubView
            // 
            this.gvTransferSubView.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvTransferSubView.Appearance.FocusedRow.Options.UseFont = true;
            this.gvTransferSubView.Appearance.SelectedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvTransferSubView.Appearance.SelectedRow.Options.UseFont = true;
            this.gvTransferSubView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSubTransferId,
            this.colSubASSETID,
            this.colFromLocation,
            this.colTooLocation});
            this.gvTransferSubView.GridControl = this.gcTransferView;
            this.gvTransferSubView.Name = "gvTransferSubView";
            this.gvTransferSubView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvTransferSubView.OptionsView.ShowGroupPanel = false;
            // 
            // colSubTransferId
            // 
            this.colSubTransferId.Caption = "TransferId";
            this.colSubTransferId.FieldName = "TRANSFER_ID";
            this.colSubTransferId.Name = "colSubTransferId";
            this.colSubTransferId.OptionsColumn.AllowEdit = false;
            this.colSubTransferId.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colSubTransferId.OptionsFilter.AllowFilter = false;
            // 
            // colSubASSETID
            // 
            this.colSubASSETID.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colSubASSETID.AppearanceHeader.Options.UseFont = true;
            this.colSubASSETID.Caption = "Asset Id";
            this.colSubASSETID.FieldName = "ASSET_ID";
            this.colSubASSETID.Name = "colSubASSETID";
            this.colSubASSETID.OptionsColumn.AllowEdit = false;
            this.colSubASSETID.OptionsColumn.AllowFocus = false;
            this.colSubASSETID.Visible = true;
            this.colSubASSETID.VisibleIndex = 0;
            // 
            // colFromLocation
            // 
            this.colFromLocation.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colFromLocation.AppearanceHeader.Options.UseFont = true;
            this.colFromLocation.Caption = "From Location";
            this.colFromLocation.FieldName = "FROM_LOCATION";
            this.colFromLocation.Name = "colFromLocation";
            this.colFromLocation.OptionsColumn.AllowEdit = false;
            this.colFromLocation.OptionsColumn.AllowFocus = false;
            this.colFromLocation.Visible = true;
            this.colFromLocation.VisibleIndex = 1;
            // 
            // colTooLocation
            // 
            this.colTooLocation.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colTooLocation.AppearanceHeader.Options.UseFont = true;
            this.colTooLocation.Caption = "To Location";
            this.colTooLocation.FieldName = "TO_LOCATION";
            this.colTooLocation.Name = "colTooLocation";
            this.colTooLocation.OptionsColumn.AllowEdit = false;
            this.colTooLocation.OptionsColumn.AllowFocus = false;
            this.colTooLocation.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colTooLocation.Visible = true;
            this.colTooLocation.VisibleIndex = 2;
            // 
            // gcTransferView
            // 
            gridLevelNode2.LevelTemplate = this.gvTransferSubView;
            gridLevelNode2.RelationName = "Transfer";
            this.gcTransferView.LevelTree.Nodes.AddRange(new DevExpress.XtraGrid.GridLevelNode[] {
            gridLevelNode2});
            this.gcTransferView.Location = new System.Drawing.Point(1, 60);
            this.gcTransferView.MainView = this.gvTransferView;
            this.gcTransferView.Margin = new System.Windows.Forms.Padding(0);
            this.gcTransferView.Name = "gcTransferView";
            this.gcTransferView.Size = new System.Drawing.Size(1059, 386);
            this.gcTransferView.TabIndex = 6;
            this.gcTransferView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTransferView,
            this.gvTransferSubView});
            this.gcTransferView.Click += new System.EventHandler(this.gcTransferView_Click);
            // 
            // gvTransferView
            // 
            this.gvTransferView.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvTransferView.Appearance.FocusedRow.Options.UseFont = true;
            this.gvTransferView.Appearance.SelectedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvTransferView.Appearance.SelectedRow.Options.UseFont = true;
            this.gvTransferView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colTransferId,
            this.colRefrenceId,
            this.colAssetGroup,
            this.colTolocation,
            this.colAssetName,
            this.colNameAddress,
            this.colDate,
            this.colRemarks,
            this.colItemid,
            this.colFromLocationId});
            this.gvTransferView.GridControl = this.gcTransferView;
            this.gvTransferView.Name = "gvTransferView";
            this.gvTransferView.OptionsBehavior.Editable = false;
            this.gvTransferView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvTransferView.OptionsView.ShowGroupPanel = false;
            this.gvTransferView.RowCountChanged += new System.EventHandler(this.gvTransferView_RowCountChanged);
            // 
            // colTransferId
            // 
            this.colTransferId.Caption = "Id";
            this.colTransferId.FieldName = "TRANSFER_ID";
            this.colTransferId.Name = "colTransferId";
            // 
            // colRefrenceId
            // 
            this.colRefrenceId.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colRefrenceId.AppearanceHeader.Options.UseFont = true;
            this.colRefrenceId.Caption = "Ref.No";
            this.colRefrenceId.FieldName = "REFRENCE_ID";
            this.colRefrenceId.Name = "colRefrenceId";
            this.colRefrenceId.OptionsColumn.AllowEdit = false;
            this.colRefrenceId.OptionsColumn.AllowFocus = false;
            this.colRefrenceId.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colRefrenceId.Visible = true;
            this.colRefrenceId.VisibleIndex = 1;
            this.colRefrenceId.Width = 92;
            // 
            // colAssetGroup
            // 
            this.colAssetGroup.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colAssetGroup.AppearanceHeader.Options.UseFont = true;
            this.colAssetGroup.Caption = "Group";
            this.colAssetGroup.FieldName = "GROUP_NAME";
            this.colAssetGroup.Name = "colAssetGroup";
            this.colAssetGroup.OptionsColumn.AllowEdit = false;
            this.colAssetGroup.OptionsColumn.AllowFocus = false;
            this.colAssetGroup.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colAssetGroup.Visible = true;
            this.colAssetGroup.VisibleIndex = 3;
            this.colAssetGroup.Width = 200;
            // 
            // colTolocation
            // 
            this.colTolocation.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colTolocation.AppearanceHeader.Options.UseFont = true;
            this.colTolocation.Caption = "To Location";
            this.colTolocation.FieldName = "TO_LOCATION";
            this.colTolocation.Name = "colTolocation";
            this.colTolocation.OptionsColumn.AllowEdit = false;
            this.colTolocation.OptionsColumn.AllowFocus = false;
            this.colTolocation.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colTolocation.Visible = true;
            this.colTolocation.VisibleIndex = 5;
            this.colTolocation.Width = 129;
            // 
            // colAssetName
            // 
            this.colAssetName.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colAssetName.AppearanceHeader.Options.UseFont = true;
            this.colAssetName.Caption = "Asset";
            this.colAssetName.FieldName = "ITEM_NAME";
            this.colAssetName.Name = "colAssetName";
            this.colAssetName.OptionsColumn.AllowEdit = false;
            this.colAssetName.OptionsColumn.AllowFocus = false;
            this.colAssetName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colAssetName.Visible = true;
            this.colAssetName.VisibleIndex = 2;
            this.colAssetName.Width = 230;
            // 
            // colNameAddress
            // 
            this.colNameAddress.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colNameAddress.AppearanceHeader.Options.UseFont = true;
            this.colNameAddress.Caption = "Name/Address";
            this.colNameAddress.FieldName = "NAME_ADDRESS";
            this.colNameAddress.GroupFormat.FormatType = DevExpress.Utils.FormatType.Numeric;
            this.colNameAddress.Name = "colNameAddress";
            this.colNameAddress.OptionsColumn.AllowEdit = false;
            this.colNameAddress.OptionsColumn.AllowFocus = false;
            this.colNameAddress.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colNameAddress.Width = 135;
            // 
            // colDate
            // 
            this.colDate.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colDate.AppearanceHeader.Options.UseFont = true;
            this.colDate.Caption = "Date";
            this.colDate.DisplayFormat.FormatString = "d";
            this.colDate.DisplayFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colDate.FieldName = "TRANSFER_DATE";
            this.colDate.Name = "colDate";
            this.colDate.OptionsColumn.AllowEdit = false;
            this.colDate.OptionsColumn.AllowFocus = false;
            this.colDate.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colDate.Visible = true;
            this.colDate.VisibleIndex = 0;
            this.colDate.Width = 89;
            // 
            // colRemarks
            // 
            this.colRemarks.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colRemarks.AppearanceHeader.Options.UseFont = true;
            this.colRemarks.Caption = "Remarks";
            this.colRemarks.FieldName = "NARRATION";
            this.colRemarks.Name = "colRemarks";
            this.colRemarks.OptionsColumn.AllowEdit = false;
            this.colRemarks.OptionsColumn.AllowFocus = false;
            this.colRemarks.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colRemarks.Visible = true;
            this.colRemarks.VisibleIndex = 6;
            this.colRemarks.Width = 173;
            // 
            // colItemid
            // 
            this.colItemid.Caption = "itemId";
            this.colItemid.FieldName = "ITEM_ID";
            this.colItemid.Name = "colItemid";
            // 
            // colFromLocationId
            // 
            this.colFromLocationId.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colFromLocationId.AppearanceHeader.Options.UseFont = true;
            this.colFromLocationId.Caption = "From Location";
            this.colFromLocationId.FieldName = "FROM_LOCATION";
            this.colFromLocationId.Name = "colFromLocationId";
            this.colFromLocationId.OptionsColumn.AllowEdit = false;
            this.colFromLocationId.OptionsColumn.AllowFocus = false;
            this.colFromLocationId.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.colFromLocationId.Visible = true;
            this.colFromLocationId.VisibleIndex = 4;
            this.colFromLocationId.Width = 128;
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnApply);
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.glkpProjects);
            this.layoutControl1.Controls.Add(this.deTodate);
            this.layoutControl1.Controls.Add(this.deFromDate);
            this.layoutControl1.Controls.Add(this.ucTransfer);
            this.layoutControl1.Controls.Add(this.gcTransferView);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.HiddenItems.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblProject});
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(219, 210, 250, 321);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1061, 471);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnApply
            // 
            this.btnApply.Location = new System.Drawing.Point(997, 35);
            this.btnApply.MaximumSize = new System.Drawing.Size(54, 21);
            this.btnApply.MinimumSize = new System.Drawing.Size(54, 21);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(54, 21);
            this.btnApply.StyleController = this.layoutControl1;
            this.btnApply.TabIndex = 12;
            this.btnApply.Text = "Apply";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // chkShowFilter
            // 
            this.chkShowFilter.Location = new System.Drawing.Point(2, 449);
            this.chkShowFilter.Margin = new System.Windows.Forms.Padding(0);
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = "Show <b>F</b>ilter";
            this.chkShowFilter.Size = new System.Drawing.Size(75, 19);
            this.chkShowFilter.StyleController = this.layoutControl1;
            this.chkShowFilter.TabIndex = 8;
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // glkpProjects
            // 
            this.glkpProjects.Location = new System.Drawing.Point(54, 26);
            this.glkpProjects.Margin = new System.Windows.Forms.Padding(0);
            this.glkpProjects.Name = "glkpProjects";
            this.glkpProjects.Properties.AppearanceFocused.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.glkpProjects.Properties.AppearanceFocused.Options.UseFont = true;
            this.glkpProjects.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpProjects.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.glkpProjects.Properties.NullText = "";
            this.glkpProjects.Properties.PopupFormSize = new System.Drawing.Size(382, 50);
            this.glkpProjects.Properties.View = this.gridLookUpEdit1View;
            this.glkpProjects.Size = new System.Drawing.Size(382, 20);
            this.glkpProjects.StyleController = this.layoutControl1;
            this.glkpProjects.TabIndex = 1;
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colID,
            this.colName});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsBehavior.Editable = false;
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // colID
            // 
            this.colID.Caption = "Id";
            this.colID.FieldName = "PROJECT_ID";
            this.colID.Name = "colID";
            // 
            // colName
            // 
            this.colName.Caption = "PROJECT";
            this.colName.FieldName = "PROJECT";
            this.colName.Name = "colName";
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            // 
            // deTodate
            // 
            this.deTodate.EditValue = null;
            this.deTodate.EnterMoveNextControl = true;
            this.deTodate.Location = new System.Drawing.Point(910, 36);
            this.deTodate.Margin = new System.Windows.Forms.Padding(0);
            this.deTodate.Name = "deTodate";
            this.deTodate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deTodate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.deTodate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deTodate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deTodate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.deTodate.Size = new System.Drawing.Size(83, 20);
            this.deTodate.StyleController = this.layoutControl1;
            this.deTodate.TabIndex = 3;
            // 
            // deFromDate
            // 
            this.deFromDate.EditValue = null;
            this.deFromDate.EnterMoveNextControl = true;
            this.deFromDate.Location = new System.Drawing.Point(806, 36);
            this.deFromDate.Margin = new System.Windows.Forms.Padding(0);
            this.deFromDate.Name = "deFromDate";
            this.deFromDate.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.deFromDate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.deFromDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deFromDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.deFromDate.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.deFromDate.Size = new System.Drawing.Size(83, 20);
            this.deFromDate.StyleController = this.layoutControl1;
            this.deFromDate.TabIndex = 2;
            // 
            // ucTransfer
            // 
            this.ucTransfer.ChangeAddCaption = "&Add";
            this.ucTransfer.ChangeCaption = "&Edit";
            this.ucTransfer.ChangeDeleteCaption = "&Delete";
            this.ucTransfer.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucTransfer.ChangePostInterestCaption = "P&ost Interest";
            this.ucTransfer.ChangePrintCaption = "&Print";
            this.ucTransfer.DisableAddButton = true;
            this.ucTransfer.DisableCloseButton = true;
            this.ucTransfer.DisableDeleteButton = true;
            this.ucTransfer.DisableDownloadExcel = true;
            this.ucTransfer.DisableEditButton = true;
            this.ucTransfer.DisableMoveTransaction = true;
            this.ucTransfer.DisableNatureofPayments = true;
            this.ucTransfer.DisablePostInterest = true;
            this.ucTransfer.DisablePrintButton = true;
            this.ucTransfer.DisableRestoreVoucher = true;
            this.ucTransfer.Location = new System.Drawing.Point(0, 0);
            this.ucTransfer.Name = "ucTransfer";
            this.ucTransfer.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.ucTransfer.ShowHTML = true;
            this.ucTransfer.ShowMMT = true;
            this.ucTransfer.ShowPDF = true;
            this.ucTransfer.ShowRTF = true;
            this.ucTransfer.ShowText = true;
            this.ucTransfer.ShowXLS = true;
            this.ucTransfer.ShowXLSX = true;
            this.ucTransfer.Size = new System.Drawing.Size(1061, 33);
            this.ucTransfer.TabIndex = 4;
            this.ucTransfer.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucTransfer.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucTransfer.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucTransfer.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucTransfer.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucTransfer.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucTransfer.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucTransfer.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucTransfer.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucTransfer.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucTransfer.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucTransfer.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucTransfer.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucTransfer.AddClicked += new System.EventHandler(this.ucTransfer_AddClicked);
            this.ucTransfer.PrintClicked += new System.EventHandler(this.ucTransfer_PrintClicked);
            this.ucTransfer.CloseClicked += new System.EventHandler(this.ucTransfer_CloseClicked);
            this.ucTransfer.RefreshClicked += new System.EventHandler(this.ucTransfer_RefreshClicked);
            // 
            // lblProject
            // 
            this.lblProject.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblProject.AppearanceItemCaption.Options.UseFont = true;
            this.lblProject.Control = this.glkpProjects;
            this.lblProject.CustomizationFormText = "Project";
            this.lblProject.Location = new System.Drawing.Point(0, 23);
            this.lblProject.MaxSize = new System.Drawing.Size(438, 26);
            this.lblProject.MinSize = new System.Drawing.Size(438, 26);
            this.lblProject.Name = "lblProject";
            this.lblProject.Padding = new DevExpress.XtraLayout.Utils.Padding(8, 2, 3, 3);
            this.lblProject.Size = new System.Drawing.Size(438, 26);
            this.lblProject.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblProject.Text = "Project";
            this.lblProject.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblProject.TextSize = new System.Drawing.Size(41, 13);
            this.lblProject.TextToControlDistance = 5;
            this.lblProject.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem6,
            this.layoutControlItem1,
            this.lblFromDate,
            this.emptySpaceItem3,
            this.lblToDate,
            this.layoutControlItem3,
            this.layoutControlItem5,
            this.lblCountSimbol,
            this.lblNumbercount,
            this.layoutControlItem6,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1061, 471);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem6
            // 
            this.emptySpaceItem6.AllowHotTrack = false;
            this.emptySpaceItem6.CustomizationFormText = "emptySpaceItem6";
            this.emptySpaceItem6.Location = new System.Drawing.Point(79, 447);
            this.emptySpaceItem6.Name = "emptySpaceItem6";
            this.emptySpaceItem6.Size = new System.Drawing.Size(937, 24);
            this.emptySpaceItem6.Text = "emptySpaceItem6";
            this.emptySpaceItem6.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ucTransfer;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(100, 20);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem1.Size = new System.Drawing.Size(1061, 33);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // lblFromDate
            // 
            this.lblFromDate.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblFromDate.AppearanceItemCaption.Options.UseFont = true;
            this.lblFromDate.Control = this.deFromDate;
            this.lblFromDate.CustomizationFormText = "From";
            this.lblFromDate.Location = new System.Drawing.Point(772, 33);
            this.lblFromDate.MaxSize = new System.Drawing.Size(119, 26);
            this.lblFromDate.MinSize = new System.Drawing.Size(119, 26);
            this.lblFromDate.Name = "lblFromDate";
            this.lblFromDate.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 2, 3, 3);
            this.lblFromDate.Size = new System.Drawing.Size(119, 26);
            this.lblFromDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblFromDate.Text = "From";
            this.lblFromDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblFromDate.TextSize = new System.Drawing.Size(29, 13);
            this.lblFromDate.TextToControlDistance = 5;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 33);
            this.emptySpaceItem3.MaxSize = new System.Drawing.Size(21, 26);
            this.emptySpaceItem3.MinSize = new System.Drawing.Size(21, 26);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(21, 26);
            this.emptySpaceItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblToDate
            // 
            this.lblToDate.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblToDate.AppearanceItemCaption.Options.UseFont = true;
            this.lblToDate.Control = this.deTodate;
            this.lblToDate.CustomizationFormText = "To";
            this.lblToDate.Location = new System.Drawing.Point(891, 33);
            this.lblToDate.MaxSize = new System.Drawing.Size(104, 26);
            this.lblToDate.MinSize = new System.Drawing.Size(104, 26);
            this.lblToDate.Name = "lblToDate";
            this.lblToDate.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 2, 3, 3);
            this.lblToDate.Size = new System.Drawing.Size(104, 26);
            this.lblToDate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblToDate.Text = "To";
            this.lblToDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblToDate.TextSize = new System.Drawing.Size(14, 13);
            this.lblToDate.TextToControlDistance = 5;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gcTransferView;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 59);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(100, 20);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutControlItem3.Size = new System.Drawing.Size(1061, 388);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.chkShowFilter;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 447);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(79, 24);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(79, 24);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(79, 24);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // lblCountSimbol
            // 
            this.lblCountSimbol.AllowHotTrack = false;
            this.lblCountSimbol.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblCountSimbol.AppearanceItemCaption.Options.UseFont = true;
            this.lblCountSimbol.CustomizationFormText = "#";
            this.lblCountSimbol.Location = new System.Drawing.Point(1016, 447);
            this.lblCountSimbol.MinSize = new System.Drawing.Size(9, 13);
            this.lblCountSimbol.Name = "lblCountSimbol";
            this.lblCountSimbol.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblCountSimbol.Size = new System.Drawing.Size(12, 24);
            this.lblCountSimbol.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblCountSimbol.Text = "#";
            this.lblCountSimbol.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblCountSimbol.TextSize = new System.Drawing.Size(9, 13);
            // 
            // lblNumbercount
            // 
            this.lblNumbercount.AllowHotTrack = false;
            this.lblNumbercount.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblNumbercount.AppearanceItemCaption.Options.UseFont = true;
            this.lblNumbercount.CustomizationFormText = "0";
            this.lblNumbercount.Location = new System.Drawing.Point(1028, 447);
            this.lblNumbercount.MinSize = new System.Drawing.Size(7, 13);
            this.lblNumbercount.Name = "lblNumbercount";
            this.lblNumbercount.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblNumbercount.Size = new System.Drawing.Size(33, 24);
            this.lblNumbercount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblNumbercount.Text = "0";
            this.lblNumbercount.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblNumbercount.TextSize = new System.Drawing.Size(7, 13);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnApply;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(995, 33);
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
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(21, 33);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(0, 26);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(104, 26);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(751, 26);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // ucTransviewClosingBalance1
            // 
            this.ucTransviewClosingBalance1.ClosingDateFrom = "";
            this.ucTransviewClosingBalance1.ClosingDateTo = "";
            this.ucTransviewClosingBalance1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTransviewClosingBalance1.Location = new System.Drawing.Point(2, 2);
            this.ucTransviewClosingBalance1.Name = "ucTransviewClosingBalance1";
            this.ucTransviewClosingBalance1.ProjectId = 0;
            this.ucTransviewClosingBalance1.Size = new System.Drawing.Size(1057, 25);
            this.ucTransviewClosingBalance1.TabIndex = 0;
            // 
            // ucTransViewOpeningBalDetails1
            // 
            this.ucTransViewOpeningBalDetails1.AssignCash = null;
            this.ucTransViewOpeningBalDetails1.AssingBank = null;
            this.ucTransViewOpeningBalDetails1.AssingFD = null;
            this.ucTransViewOpeningBalDetails1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.ucTransViewOpeningBalDetails1.Location = new System.Drawing.Point(2, 2);
            this.ucTransViewOpeningBalDetails1.Margin = new System.Windows.Forms.Padding(0);
            this.ucTransViewOpeningBalDetails1.Name = "ucTransViewOpeningBalDetails1";
            this.ucTransViewOpeningBalDetails1.OpeningDateFrom = "";
            this.ucTransViewOpeningBalDetails1.OpeningDateTo = "";
            this.ucTransViewOpeningBalDetails1.ProjectId = 0;
            this.ucTransViewOpeningBalDetails1.Size = new System.Drawing.Size(1057, 26);
            this.ucTransViewOpeningBalDetails1.TabIndex = 0;
            // 
            // frmTransferView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1061, 471);
            this.Controls.Add(this.layoutControl1);
            this.MaximizeBox = false;
            this.Name = "frmTransferView";
            this.ShowIcon = false;
            this.Text = "Transfer";
            this.ShowFilterClicked += new System.EventHandler(this.frmTransferView_ShowFilterClicked);
            this.Load += new System.EventHandler(this.frmTransferView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.gvTransferSubView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcTransferView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTransferView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpProjects.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTodate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTodate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFromDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deFromDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblProject)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFromDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblToDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCountSimbol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNumbercount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private UIControls.ucTransviewClosingBalance ucTransviewClosingBalance1;
        private DevExpress.XtraGrid.GridControl gcTransferView;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTransferView;
        private DevExpress.XtraGrid.Columns.GridColumn colRefrenceId;
        private DevExpress.XtraGrid.Columns.GridColumn colAssetGroup;
        private DevExpress.XtraGrid.Columns.GridColumn colTolocation;
        private DevExpress.XtraGrid.Columns.GridColumn colAssetName;
        private DevExpress.XtraGrid.Columns.GridColumn colNameAddress;
        private UIControls.ucTransViewOpeningBalDetails ucTransViewOpeningBalDetails1;
        private DevExpress.XtraEditors.GridLookUpEdit glkpProjects;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraEditors.DateEdit deTodate;
        private DevExpress.XtraEditors.DateEdit deFromDate;
        private Bosco.Utility.Controls.ucToolBar ucTransfer;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem lblProject;
        private DevExpress.XtraLayout.LayoutControlItem lblFromDate;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.LayoutControlItem lblToDate;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.SimpleLabelItem lblCountSimbol;
        private DevExpress.XtraLayout.SimpleLabelItem lblNumbercount;
        private DevExpress.XtraGrid.Columns.GridColumn colTransferId;
        private DevExpress.XtraGrid.Columns.GridColumn colDate;
        private DevExpress.XtraGrid.Columns.GridColumn colRemarks;
        private DevExpress.XtraGrid.Columns.GridColumn colID;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraEditors.SimpleButton btnApply;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTransferSubView;
        private DevExpress.XtraGrid.Columns.GridColumn colSubTransferId;
        private DevExpress.XtraGrid.Columns.GridColumn colSubASSETID;
        private DevExpress.XtraGrid.Columns.GridColumn colFromLocation;
        private DevExpress.XtraGrid.Columns.GridColumn colTooLocation;
        private DevExpress.XtraGrid.Columns.GridColumn colItemid;
        private DevExpress.XtraGrid.Columns.GridColumn colFromLocationId;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}