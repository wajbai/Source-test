namespace ACPP.Modules.Inventory
{
    partial class frmUnitofMeasureView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUnitofMeasureView));
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcMeasureofunits = new DevExpress.XtraGrid.GridControl();
            this.gvMeasureofunits = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSymbol = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFormalName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDecimalPlace = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFirstUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSecondUnit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colConversionOf = new DevExpress.XtraGrid.Columns.GridColumn();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.ucMeasureofUnits = new Bosco.Utility.Controls.ucToolBar();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblCount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblCountNumber = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcMeasureofunits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMeasureofunits)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCountNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcMeasureofunits);
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.ucMeasureofUnits);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(429, 167, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // gcMeasureofunits
            // 
            resources.ApplyResources(this.gcMeasureofunits, "gcMeasureofunits");
            this.gcMeasureofunits.MainView = this.gvMeasureofunits;
            this.gcMeasureofunits.Name = "gcMeasureofunits";
            this.gcMeasureofunits.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMeasureofunits});
            // 
            // gvMeasureofunits
            // 
            this.gvMeasureofunits.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvMeasureofunits.Appearance.FocusedRow.Font")));
            this.gvMeasureofunits.Appearance.FocusedRow.Options.UseFont = true;
            this.gvMeasureofunits.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvMeasureofunits.Appearance.HeaderPanel.Font")));
            this.gvMeasureofunits.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvMeasureofunits.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colType,
            this.colSymbol,
            this.colFormalName,
            this.colDecimalPlace,
            this.colFirstUnit,
            this.colSecondUnit,
            this.colConversionOf});
            this.gvMeasureofunits.GridControl = this.gcMeasureofunits;
            this.gvMeasureofunits.Name = "gvMeasureofunits";
            this.gvMeasureofunits.OptionsBehavior.Editable = false;
            this.gvMeasureofunits.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvMeasureofunits.OptionsView.ShowGroupPanel = false;
            this.gvMeasureofunits.DoubleClick += new System.EventHandler(this.gvMeasureofunits_DoubleClick);
            this.gvMeasureofunits.RowCountChanged += new System.EventHandler(this.gvMeasureofunits_RowCountChanged);
            // 
            // colId
            // 
            resources.ApplyResources(this.colId, "colId");
            this.colId.FieldName = "UOM_ID";
            this.colId.Name = "colId";
            // 
            // colType
            // 
            this.colType.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colType.AppearanceHeader.Font")));
            this.colType.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colType, "colType");
            this.colType.FieldName = "TYPE";
            this.colType.Name = "colType";
            this.colType.OptionsColumn.AllowEdit = false;
            this.colType.OptionsColumn.AllowFocus = false;
            // 
            // colSymbol
            // 
            this.colSymbol.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colSymbol.AppearanceHeader.Font")));
            this.colSymbol.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colSymbol, "colSymbol");
            this.colSymbol.FieldName = "SYMBOL";
            this.colSymbol.Name = "colSymbol";
            this.colSymbol.OptionsColumn.AllowEdit = false;
            this.colSymbol.OptionsColumn.AllowFocus = false;
            // 
            // colFormalName
            // 
            this.colFormalName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colFormalName.AppearanceHeader.Font")));
            this.colFormalName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colFormalName, "colFormalName");
            this.colFormalName.FieldName = "NAME";
            this.colFormalName.Name = "colFormalName";
            this.colFormalName.OptionsColumn.AllowEdit = false;
            this.colFormalName.OptionsColumn.AllowFocus = false;
            // 
            // colDecimalPlace
            // 
            this.colDecimalPlace.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colDecimalPlace.AppearanceHeader.Font")));
            this.colDecimalPlace.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colDecimalPlace, "colDecimalPlace");
            this.colDecimalPlace.FieldName = "DECIMAL_PLACE";
            this.colDecimalPlace.Name = "colDecimalPlace";
            this.colDecimalPlace.OptionsColumn.AllowEdit = false;
            this.colDecimalPlace.OptionsColumn.AllowFocus = false;
            // 
            // colFirstUnit
            // 
            this.colFirstUnit.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colFirstUnit.AppearanceHeader.Font")));
            this.colFirstUnit.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colFirstUnit, "colFirstUnit");
            this.colFirstUnit.FieldName = "FIRST_UNIT_ID";
            this.colFirstUnit.Name = "colFirstUnit";
            this.colFirstUnit.OptionsColumn.AllowEdit = false;
            this.colFirstUnit.OptionsColumn.AllowFocus = false;
            // 
            // colSecondUnit
            // 
            this.colSecondUnit.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colSecondUnit.AppearanceHeader.Font")));
            this.colSecondUnit.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colSecondUnit, "colSecondUnit");
            this.colSecondUnit.FieldName = "SECOND_UNIT_ID";
            this.colSecondUnit.Name = "colSecondUnit";
            this.colSecondUnit.OptionsColumn.AllowEdit = false;
            this.colSecondUnit.OptionsColumn.AllowFocus = false;
            // 
            // colConversionOf
            // 
            this.colConversionOf.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colConversionOf.AppearanceHeader.Font")));
            this.colConversionOf.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colConversionOf, "colConversionOf");
            this.colConversionOf.FieldName = "CONVERSION_OF";
            this.colConversionOf.Name = "colConversionOf";
            this.colConversionOf.OptionsColumn.AllowEdit = false;
            this.colConversionOf.OptionsColumn.AllowFocus = false;
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
            // ucMeasureofUnits
            // 
            this.ucMeasureofUnits.ChangeAddCaption = "&Add";
            this.ucMeasureofUnits.ChangeCaption = "&Edit";
            this.ucMeasureofUnits.ChangeDeleteCaption = "&Delete";
            this.ucMeasureofUnits.ChangeMoveVoucherCaption = "&Move Voucher";
            resources.ApplyResources(toolTipTitleItem1, "toolTipTitleItem1");
            toolTipItem1.LeftIndent = 6;
            resources.ApplyResources(toolTipItem1, "toolTipItem1");
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.ucMeasureofUnits.ChangeMoveVoucherTooltip = superToolTip1;
            this.ucMeasureofUnits.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucMeasureofUnits.ChangePostInterestCaption = "P&ost Interest";
            this.ucMeasureofUnits.ChangePrintCaption = "&Print";
            this.ucMeasureofUnits.ChnageRenewCaption = "Re<u>n</u>ew";
            this.ucMeasureofUnits.DisableAddButton = true;
            this.ucMeasureofUnits.DisableAMCRenew = true;
            this.ucMeasureofUnits.DisableCloseButton = true;
            this.ucMeasureofUnits.DisableDeleteButton = true;
            this.ucMeasureofUnits.DisableDownloadExcel = true;
            this.ucMeasureofUnits.DisableEditButton = true;
            this.ucMeasureofUnits.DisableMoveTransaction = true;
            this.ucMeasureofUnits.DisableNatureofPayments = true;
            this.ucMeasureofUnits.DisablePostInterest = true;
            this.ucMeasureofUnits.DisablePrintButton = true;
            this.ucMeasureofUnits.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucMeasureofUnits, "ucMeasureofUnits");
            this.ucMeasureofUnits.Name = "ucMeasureofUnits";
            this.ucMeasureofUnits.ShowHTML = true;
            this.ucMeasureofUnits.ShowMMT = true;
            this.ucMeasureofUnits.ShowPDF = true;
            this.ucMeasureofUnits.ShowRTF = true;
            this.ucMeasureofUnits.ShowText = true;
            this.ucMeasureofUnits.ShowXLS = true;
            this.ucMeasureofUnits.ShowXLSX = true;
            this.ucMeasureofUnits.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucMeasureofUnits.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucMeasureofUnits.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucMeasureofUnits.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucMeasureofUnits.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucMeasureofUnits.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucMeasureofUnits.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucMeasureofUnits.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucMeasureofUnits.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucMeasureofUnits.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucMeasureofUnits.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucMeasureofUnits.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucMeasureofUnits.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucMeasureofUnits.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucMeasureofUnits.AddClicked += new System.EventHandler(this.ucMeasureofUnits_AddClicked);
            this.ucMeasureofUnits.EditClicked += new System.EventHandler(this.ucMeasureofUnits_EditClicked);
            this.ucMeasureofUnits.DeleteClicked += new System.EventHandler(this.ucMeasureofUnits_DeleteClicked);
            this.ucMeasureofUnits.PrintClicked += new System.EventHandler(this.ucMeasureofUnits_PrintClicked);
            this.ucMeasureofUnits.CloseClicked += new System.EventHandler(this.ucMeasureofUnits_CloseClicked);
            this.ucMeasureofUnits.RefreshClicked += new System.EventHandler(this.ucMeasureofUnits_RefreshClicked);
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem2,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.lblCount,
            this.lblCountNumber});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(976, 402);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(112, 379);
            this.emptySpaceItem2.MinSize = new System.Drawing.Size(100, 20);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.emptySpaceItem2.Size = new System.Drawing.Size(821, 23);
            this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ucMeasureofUnits;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 32);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(100, 32);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem1.Size = new System.Drawing.Size(976, 32);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.chkShowFilter;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 379);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(112, 23);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(112, 23);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(112, 23);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gcMeasureofunits;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 32);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(100, 20);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem3.Size = new System.Drawing.Size(976, 347);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // lblCount
            // 
            this.lblCount.AllowHotTrack = false;
            this.lblCount.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblCount.AppearanceItemCaption.Font")));
            this.lblCount.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblCount, "lblCount");
            this.lblCount.Location = new System.Drawing.Point(933, 379);
            this.lblCount.MinSize = new System.Drawing.Size(13, 17);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(13, 23);
            this.lblCount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblCount.TextSize = new System.Drawing.Size(9, 13);
            // 
            // lblCountNumber
            // 
            this.lblCountNumber.AllowHotTrack = false;
            this.lblCountNumber.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblCountNumber.AppearanceItemCaption.Font")));
            this.lblCountNumber.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblCountNumber, "lblCountNumber");
            this.lblCountNumber.Location = new System.Drawing.Point(946, 379);
            this.lblCountNumber.MinSize = new System.Drawing.Size(13, 17);
            this.lblCountNumber.Name = "lblCountNumber";
            this.lblCountNumber.Size = new System.Drawing.Size(30, 23);
            this.lblCountNumber.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblCountNumber.TextSize = new System.Drawing.Size(9, 13);
            // 
            // frmUnitofMeasureView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmUnitofMeasureView";
            this.ShowFilterClicked += new System.EventHandler(this.frmUnitofMeasureView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmUnitofMeasureView_EnterClicked);
            this.Activated += new System.EventHandler(this.frmUnitofMeasureView_Activated);
            this.Load += new System.EventHandler(this.frmUnitofMeasureView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcMeasureofunits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMeasureofunits)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCountNumber)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gcMeasureofunits;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMeasureofunits;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colSymbol;
        private DevExpress.XtraGrid.Columns.GridColumn colFormalName;
        private DevExpress.XtraGrid.Columns.GridColumn colDecimalPlace;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private Bosco.Utility.Controls.ucToolBar ucMeasureofUnits;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.SimpleLabelItem lblCount;
        private DevExpress.XtraLayout.SimpleLabelItem lblCountNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colFirstUnit;
        private DevExpress.XtraGrid.Columns.GridColumn colSecondUnit;
        private DevExpress.XtraGrid.Columns.GridColumn colConversionOf;
    }
}