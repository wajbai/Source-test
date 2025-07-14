namespace ACPP.Modules.Inventory
{
    partial class frmCustodiansView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCustodiansView));
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lblCount = new DevExpress.XtraEditors.LabelControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.ucCustodiansView = new Bosco.Utility.Controls.ucToolBar();
            this.gcCustodiansView = new DevExpress.XtraGrid.GridControl();
            this.gvCustodiansView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcColName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gccolRole = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ricType = new DevExpress.XtraEditors.Repository.RepositoryItemComboBox();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblRowCount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.colCustodiansId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStartDate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEndDate = new DevExpress.XtraGrid.Columns.GridColumn();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCustodiansView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCustodiansView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ricType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRowCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lblCount);
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.ucCustodiansView);
            this.layoutControl1.Controls.Add(this.gcCustodiansView);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(297, 233, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // lblCount
            // 
            this.lblCount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblCount.Appearance.Font")));
            resources.ApplyResources(this.lblCount, "lblCount");
            this.lblCount.Name = "lblCount";
            this.lblCount.StyleController = this.layoutControl1;
            // 
            // chkShowFilter
            // 
            resources.ApplyResources(this.chkShowFilter, "chkShowFilter");
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = resources.GetString("chkShowFilter.Properties.Caption");
            this.chkShowFilter.StyleController = this.layoutControl1;
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // ucCustodiansView
            // 
            this.ucCustodiansView.ChangeAddCaption = "&Add";
            this.ucCustodiansView.ChangeCaption = "&Edit";
            this.ucCustodiansView.ChangeDeleteCaption = "&Delete";
            this.ucCustodiansView.ChangeMoveVoucherCaption = "&Move Voucher";
            resources.ApplyResources(toolTipTitleItem1, "toolTipTitleItem1");
            toolTipItem1.LeftIndent = 6;
            resources.ApplyResources(toolTipItem1, "toolTipItem1");
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.ucCustodiansView.ChangeMoveVoucherTooltip = superToolTip1;
            this.ucCustodiansView.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucCustodiansView.ChangePostInterestCaption = "P&ost Interest";
            this.ucCustodiansView.ChangePrintCaption = "&Print";
            this.ucCustodiansView.ChnageRenewCaption = "Re<u>n</u>ew";
            this.ucCustodiansView.DisableAddButton = true;
            this.ucCustodiansView.DisableAMCRenew = true;
            this.ucCustodiansView.DisableCloseButton = true;
            this.ucCustodiansView.DisableDeleteButton = true;
            this.ucCustodiansView.DisableDownloadExcel = true;
            this.ucCustodiansView.DisableEditButton = true;
            this.ucCustodiansView.DisableMoveTransaction = true;
            this.ucCustodiansView.DisableNatureofPayments = true;
            this.ucCustodiansView.DisablePostInterest = true;
            this.ucCustodiansView.DisablePrintButton = true;
            this.ucCustodiansView.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucCustodiansView, "ucCustodiansView");
            this.ucCustodiansView.Name = "ucCustodiansView";
            this.ucCustodiansView.ShowHTML = true;
            this.ucCustodiansView.ShowMMT = true;
            this.ucCustodiansView.ShowPDF = true;
            this.ucCustodiansView.ShowRTF = true;
            this.ucCustodiansView.ShowText = true;
            this.ucCustodiansView.ShowXLS = true;
            this.ucCustodiansView.ShowXLSX = true;
            this.ucCustodiansView.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucCustodiansView.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucCustodiansView.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucCustodiansView.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucCustodiansView.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucCustodiansView.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucCustodiansView.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucCustodiansView.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucCustodiansView.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucCustodiansView.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucCustodiansView.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucCustodiansView.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucCustodiansView.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucCustodiansView.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucCustodiansView.AddClicked += new System.EventHandler(this.ucCustodiansView_AddClicked);
            this.ucCustodiansView.EditClicked += new System.EventHandler(this.ucCustodiansView_EditClicked);
            this.ucCustodiansView.DeleteClicked += new System.EventHandler(this.ucCustodiansView_DeleteClicked);
            this.ucCustodiansView.PrintClicked += new System.EventHandler(this.ucCustodiansView_PrintClicked);
            this.ucCustodiansView.CloseClicked += new System.EventHandler(this.ucCustodiansView_CloseClicked);
            this.ucCustodiansView.RefreshClicked += new System.EventHandler(this.ucCustodiansView_RefreshClicked);
            this.ucCustodiansView.DownloadExcel += new System.EventHandler(this.ucCustodiansView_DownloadExcel);
            // 
            // gcCustodiansView
            // 
            resources.ApplyResources(this.gcCustodiansView, "gcCustodiansView");
            this.gcCustodiansView.MainView = this.gvCustodiansView;
            this.gcCustodiansView.Name = "gcCustodiansView";
            this.gcCustodiansView.RepositoryItems.AddRange(new DevExpress.XtraEditors.Repository.RepositoryItem[] {
            this.ricType});
            this.gcCustodiansView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvCustodiansView});
            this.gcCustodiansView.DoubleClick += new System.EventHandler(this.gcCustodiansView_DoubleClick);
            // 
            // gvCustodiansView
            // 
            this.gvCustodiansView.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvCustodiansView.Appearance.FocusedRow.Font")));
            this.gvCustodiansView.Appearance.FocusedRow.Options.UseFont = true;
            this.gvCustodiansView.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvCustodiansView.Appearance.HeaderPanel.Font")));
            this.gvCustodiansView.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvCustodiansView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.gcColName,
            this.gccolRole});
            this.gvCustodiansView.GridControl = this.gcCustodiansView;
            this.gvCustodiansView.Name = "gvCustodiansView";
            this.gvCustodiansView.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvCustodiansView.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvCustodiansView.OptionsBehavior.Editable = false;
            this.gvCustodiansView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvCustodiansView.OptionsView.ShowGroupPanel = false;
            this.gvCustodiansView.RowCountChanged += new System.EventHandler(this.gvCustodiansView_RowCountChanged);
            // 
            // colId
            // 
            resources.ApplyResources(this.colId, "colId");
            this.colId.FieldName = "CUSTODIAN_ID";
            this.colId.Name = "colId";
            this.colId.OptionsColumn.AllowEdit = false;
            // 
            // gcColName
            // 
            this.gcColName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("gcColName.AppearanceHeader.Font")));
            this.gcColName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.gcColName, "gcColName");
            this.gcColName.FieldName = "CUSTODIAN";
            this.gcColName.Name = "gcColName";
            this.gcColName.OptionsColumn.AllowEdit = false;
            this.gcColName.OptionsColumn.AllowFocus = false;
            this.gcColName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // gccolRole
            // 
            this.gccolRole.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("gccolRole.AppearanceHeader.Font")));
            this.gccolRole.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.gccolRole, "gccolRole");
            this.gccolRole.FieldName = "ROLE";
            this.gccolRole.Name = "gccolRole";
            this.gccolRole.OptionsColumn.AllowEdit = false;
            this.gccolRole.OptionsColumn.AllowFocus = false;
            this.gccolRole.OptionsFilter.AllowAutoFilter = false;
            this.gccolRole.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // ricType
            // 
            resources.ApplyResources(this.ricType, "ricType");
            this.ricType.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("ricType.Buttons"))))});
            this.ricType.Items.AddRange(new object[] {
            resources.GetString("ricType.Items"),
            resources.GetString("ricType.Items1")});
            this.ricType.Name = "ricType";
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1,
            this.layoutControlItem3,
            this.lblRowCount,
            this.layoutControlItem4});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(817, 397);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcCustodiansView;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 32);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(196, 20);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem1.Size = new System.Drawing.Size(817, 341);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.ucCustodiansView;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(0, 32);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(196, 32);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem2.Size = new System.Drawing.Size(817, 32);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(79, 373);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(104, 24);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(689, 24);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkShowFilter;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 373);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(79, 24);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // lblRowCount
            // 
            this.lblRowCount.AllowHotTrack = false;
            this.lblRowCount.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblRowCount.AppearanceItemCaption.Font")));
            this.lblRowCount.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblRowCount, "lblRowCount");
            this.lblRowCount.Location = new System.Drawing.Point(785, 373);
            this.lblRowCount.Name = "lblRowCount";
            this.lblRowCount.Size = new System.Drawing.Size(32, 24);
            this.lblRowCount.TextSize = new System.Drawing.Size(7, 13);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.lblCount;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(768, 373);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(13, 17);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(17, 24);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // colCustodiansId
            // 
            this.colCustodiansId.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colCustodiansId.AppearanceHeader.Font")));
            this.colCustodiansId.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colCustodiansId, "colCustodiansId");
            this.colCustodiansId.FieldName = "CUSTODIANS_ID";
            this.colCustodiansId.Name = "colCustodiansId";
            // 
            // colName
            // 
            this.colName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colName.AppearanceHeader.Font")));
            this.colName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colName, "colName");
            this.colName.FieldName = "NAME";
            this.colName.Name = "colName";
            // 
            // colType
            // 
            this.colType.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colType.AppearanceHeader.Font")));
            this.colType.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colType, "colType");
            this.colType.FieldName = "TYPE";
            this.colType.Name = "colType";
            // 
            // colStartDate
            // 
            this.colStartDate.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colStartDate.AppearanceHeader.Font")));
            this.colStartDate.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colStartDate, "colStartDate");
            this.colStartDate.FieldName = "START_DATE";
            this.colStartDate.GroupFormat.FormatString = "d";
            this.colStartDate.GroupFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colStartDate.Name = "colStartDate";
            // 
            // colEndDate
            // 
            this.colEndDate.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colEndDate.AppearanceHeader.Font")));
            this.colEndDate.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colEndDate, "colEndDate");
            this.colEndDate.FieldName = "END_DATE";
            this.colEndDate.GroupFormat.FormatString = "d";
            this.colEndDate.GroupFormat.FormatType = DevExpress.Utils.FormatType.DateTime;
            this.colEndDate.Name = "colEndDate";
            // 
            // frmCustodiansView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmCustodiansView";
            this.ShowIcon = false;
            this.ShowFilterClicked += new System.EventHandler(this.frmCustodiansView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmCustodiansView_EnterClicked);
            this.Activated += new System.EventHandler(this.frmCustodiansView_Activated);
            this.Load += new System.EventHandler(this.frmCustodiansView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcCustodiansView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvCustodiansView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ricType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRowCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private Bosco.Utility.Controls.ucToolBar ucCustodiansView;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.LabelControl lblCount;
        private DevExpress.XtraGrid.GridControl gcCustodiansView;
        private DevExpress.XtraGrid.Views.Grid.GridView gvCustodiansView;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Columns.GridColumn colCustodiansId;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colStartDate;
        private DevExpress.XtraGrid.Columns.GridColumn colEndDate;
        private DevExpress.XtraLayout.SimpleLabelItem lblRowCount;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn gcColName;
        private DevExpress.XtraGrid.Columns.GridColumn gccolRole;
        private DevExpress.XtraEditors.Repository.RepositoryItemComboBox ricType;
    }
}