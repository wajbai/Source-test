namespace ACPP.Modules.Asset.Masters
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
            this.ucMeasureofUnits = new ACPP.Modules.UIControls.ucToolBar();
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
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(429, 167, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(870, 402);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // gcMeasureofunits
            // 
            this.gcMeasureofunits.Location = new System.Drawing.Point(0, 32);
            this.gcMeasureofunits.MainView = this.gvMeasureofunits;
            this.gcMeasureofunits.Name = "gcMeasureofunits";
            this.gcMeasureofunits.Size = new System.Drawing.Size(870, 347);
            this.gcMeasureofunits.TabIndex = 6;
            this.gcMeasureofunits.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMeasureofunits});
            // 
            // gvMeasureofunits
            // 
            this.gvMeasureofunits.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvMeasureofunits.Appearance.FocusedRow.Options.UseFont = true;
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
            this.colId.Caption = "Id";
            this.colId.FieldName = "UNIT_ID";
            this.colId.Name = "colId";
            // 
            // colType
            // 
            this.colType.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colType.AppearanceHeader.Options.UseFont = true;
            this.colType.Caption = "Type";
            this.colType.FieldName = "TYPE";
            this.colType.Name = "colType";
            this.colType.OptionsColumn.AllowEdit = false;
            this.colType.Visible = true;
            this.colType.VisibleIndex = 0;
            // 
            // colSymbol
            // 
            this.colSymbol.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colSymbol.AppearanceHeader.Options.UseFont = true;
            this.colSymbol.Caption = "Symbol";
            this.colSymbol.FieldName = "SYMBOL";
            this.colSymbol.Name = "colSymbol";
            this.colSymbol.OptionsColumn.AllowEdit = false;
            this.colSymbol.Visible = true;
            this.colSymbol.VisibleIndex = 1;
            // 
            // colFormalName
            // 
            this.colFormalName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colFormalName.AppearanceHeader.Options.UseFont = true;
            this.colFormalName.Caption = "Formal Name";
            this.colFormalName.FieldName = "NAME";
            this.colFormalName.Name = "colFormalName";
            this.colFormalName.OptionsColumn.AllowEdit = false;
            this.colFormalName.Visible = true;
            this.colFormalName.VisibleIndex = 2;
            // 
            // colDecimalPlace
            // 
            this.colDecimalPlace.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colDecimalPlace.AppearanceHeader.Options.UseFont = true;
            this.colDecimalPlace.Caption = "Decimal Place";
            this.colDecimalPlace.FieldName = "DECIMAL_PLACE";
            this.colDecimalPlace.Name = "colDecimalPlace";
            this.colDecimalPlace.OptionsColumn.AllowEdit = false;
            this.colDecimalPlace.Visible = true;
            this.colDecimalPlace.VisibleIndex = 3;
            // 
            // colFirstUnit
            // 
            this.colFirstUnit.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colFirstUnit.AppearanceHeader.Options.UseFont = true;
            this.colFirstUnit.Caption = "First Unit";
            this.colFirstUnit.FieldName = "FIRST_UNIT_ID";
            this.colFirstUnit.Name = "colFirstUnit";
            this.colFirstUnit.OptionsColumn.AllowEdit = false;
            // 
            // colSecondUnit
            // 
            this.colSecondUnit.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colSecondUnit.AppearanceHeader.Options.UseFont = true;
            this.colSecondUnit.Caption = "Second Unit";
            this.colSecondUnit.FieldName = "SECOND_UNIT_ID";
            this.colSecondUnit.Name = "colSecondUnit";
            this.colSecondUnit.OptionsColumn.AllowEdit = false;
            // 
            // colConversionOf
            // 
            this.colConversionOf.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colConversionOf.AppearanceHeader.Options.UseFont = true;
            this.colConversionOf.Caption = "Conversion Of";
            this.colConversionOf.FieldName = "CONVERSION_OF";
            this.colConversionOf.Name = "colConversionOf";
            this.colConversionOf.OptionsColumn.AllowEdit = false;
            // 
            // chkShowFilter
            // 
            this.chkShowFilter.Location = new System.Drawing.Point(2, 381);
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = "Show <b>F</b>ilter";
            this.chkShowFilter.Size = new System.Drawing.Size(108, 19);
            this.chkShowFilter.StyleController = this.layoutControl1;
            this.chkShowFilter.TabIndex = 5;
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // ucMeasureofUnits
            // 
            this.ucMeasureofUnits.ChangeAddCaption = "&Add";
            this.ucMeasureofUnits.ChangeCaption = "&Edit";
            this.ucMeasureofUnits.ChangeDeleteCaption = "&Delete";
            this.ucMeasureofUnits.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucMeasureofUnits.ChangePrintCaption = "&Print";
            this.ucMeasureofUnits.DisableAddButton = true;
            this.ucMeasureofUnits.DisableCloseButton = true;
            this.ucMeasureofUnits.DisableDeleteButton = true;
            this.ucMeasureofUnits.DisableDownloadExcel = true;
            this.ucMeasureofUnits.DisableEditButton = true;
            this.ucMeasureofUnits.DisableMoveTransaction = true;
            this.ucMeasureofUnits.DisableNatureofPayments = true;
            this.ucMeasureofUnits.DisablePrintButton = true;
            this.ucMeasureofUnits.DisableRestoreVoucher = true;
            this.ucMeasureofUnits.Location = new System.Drawing.Point(0, 0);
            this.ucMeasureofUnits.Name = "ucMeasureofUnits";
            this.ucMeasureofUnits.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.ucMeasureofUnits.ShowHTML = true;
            this.ucMeasureofUnits.ShowMMT = true;
            this.ucMeasureofUnits.ShowPDF = true;
            this.ucMeasureofUnits.ShowRTF = true;
            this.ucMeasureofUnits.ShowText = true;
            this.ucMeasureofUnits.ShowXLS = true;
            this.ucMeasureofUnits.ShowXLSX = true;
            this.ucMeasureofUnits.Size = new System.Drawing.Size(870, 32);
            this.ucMeasureofUnits.TabIndex = 4;
            this.ucMeasureofUnits.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucMeasureofUnits.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucMeasureofUnits.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucMeasureofUnits.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucMeasureofUnits.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucMeasureofUnits.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucMeasureofUnits.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucMeasureofUnits.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucMeasureofUnits.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucMeasureofUnits.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucMeasureofUnits.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
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
            this.layoutControlGroup1.CustomizationFormText = "Root";
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
            this.layoutControlGroup1.Size = new System.Drawing.Size(870, 402);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(112, 379);
            this.emptySpaceItem2.MinSize = new System.Drawing.Size(100, 20);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.emptySpaceItem2.Size = new System.Drawing.Size(692, 23);
            this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ucMeasureofUnits;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 32);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(100, 32);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem1.Size = new System.Drawing.Size(870, 32);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.chkShowFilter;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 379);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(112, 23);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(112, 23);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(112, 23);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.gcMeasureofunits;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 32);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(100, 20);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem3.Size = new System.Drawing.Size(870, 347);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // lblCount
            // 
            this.lblCount.AllowHotTrack = false;
            this.lblCount.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblCount.AppearanceItemCaption.Options.UseFont = true;
            this.lblCount.CustomizationFormText = "#";
            this.lblCount.Location = new System.Drawing.Point(804, 379);
            this.lblCount.MaxSize = new System.Drawing.Size(14, 23);
            this.lblCount.MinSize = new System.Drawing.Size(14, 23);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(14, 23);
            this.lblCount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblCount.Text = "#";
            this.lblCount.TextSize = new System.Drawing.Size(9, 13);
            // 
            // lblCountNumber
            // 
            this.lblCountNumber.AllowHotTrack = false;
            this.lblCountNumber.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblCountNumber.AppearanceItemCaption.Options.UseFont = true;
            this.lblCountNumber.CustomizationFormText = "0";
            this.lblCountNumber.Location = new System.Drawing.Point(818, 379);
            this.lblCountNumber.MaxSize = new System.Drawing.Size(52, 23);
            this.lblCountNumber.MinSize = new System.Drawing.Size(52, 23);
            this.lblCountNumber.Name = "lblCountNumber";
            this.lblCountNumber.Size = new System.Drawing.Size(52, 23);
            this.lblCountNumber.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblCountNumber.Text = "0";
            this.lblCountNumber.TextSize = new System.Drawing.Size(9, 13);
            // 
            // frmUnitofMeasureView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(870, 402);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmUnitofMeasureView";
            this.Text = "Unit of Messuare";
            this.ShowFilterClicked += new System.EventHandler(this.frmUnitofMeasureView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmUnitofMeasureView_EnterClicked);
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
        private UIControls.ucToolBar ucMeasureofUnits;
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