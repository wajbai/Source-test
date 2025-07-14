namespace ACPP.Modules.Inventory.Asset
{
    partial class frmDepreciationView
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
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.gcDepreciationView = new DevExpress.XtraGrid.GridControl();
            this.gvDepreciationView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ucDepriciationView = new Bosco.Utility.Controls.ucToolBar();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblCountNumber = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblCount = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDepreciationView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDepreciationView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCountNumber)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCount)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.gcDepreciationView);
            this.layoutControl1.Controls.Add(this.ucDepriciationView);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(958, 241, 250, 297);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(1138, 448);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // chkShowFilter
            // 
            this.chkShowFilter.Location = new System.Drawing.Point(2, 426);
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = "Show <b>F</b>ilter";
            this.chkShowFilter.Size = new System.Drawing.Size(122, 19);
            this.chkShowFilter.StyleController = this.layoutControl1;
            this.chkShowFilter.TabIndex = 6;
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // gcDepreciationView
            // 
            this.gcDepreciationView.Location = new System.Drawing.Point(0, 32);
            this.gcDepreciationView.MainView = this.gvDepreciationView;
            this.gcDepreciationView.Margin = new System.Windows.Forms.Padding(0);
            this.gcDepreciationView.Name = "gcDepreciationView";
            this.gcDepreciationView.Padding = new System.Windows.Forms.Padding(2);
            this.gcDepreciationView.Size = new System.Drawing.Size(1138, 392);
            this.gcDepreciationView.TabIndex = 5;
            this.gcDepreciationView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDepreciationView});
            this.gcDepreciationView.DoubleClick += new System.EventHandler(this.gcDepreciationView_DoubleClick);
            // 
            // gvDepreciationView
            // 
            this.gvDepreciationView.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvDepreciationView.Appearance.FocusedRow.Options.UseFont = true;
            this.gvDepreciationView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvDepreciationView.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvDepreciationView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colName,
            this.colDescription});
            this.gvDepreciationView.GridControl = this.gcDepreciationView;
            this.gvDepreciationView.Name = "gvDepreciationView";
            this.gvDepreciationView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvDepreciationView.OptionsView.ShowGroupPanel = false;
            this.gvDepreciationView.RowCountChanged += new System.EventHandler(this.gvDepreciationView_RowCountChanged);
            // 
            // colId
            // 
            this.colId.Caption = "Id";
            this.colId.FieldName = "DEP_ID";
            this.colId.Name = "colId";
            this.colId.OptionsColumn.AllowEdit = false;
            // 
            // colName
            // 
            this.colName.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colName.AppearanceHeader.Options.UseFont = true;
            this.colName.Caption = "Name";
            this.colName.FieldName = "NAME";
            this.colName.Name = "colName";
            this.colName.OptionsColumn.AllowEdit = false;
            this.colName.OptionsColumn.AllowFocus = false;
            this.colName.Visible = true;
            this.colName.VisibleIndex = 0;
            // 
            // colDescription
            // 
            this.colDescription.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colDescription.AppearanceHeader.Options.UseFont = true;
            this.colDescription.Caption = "Description";
            this.colDescription.FieldName = "DESCRIPTION";
            this.colDescription.Name = "colDescription";
            this.colDescription.OptionsColumn.AllowEdit = false;
            this.colDescription.OptionsColumn.AllowFocus = false;
            this.colDescription.Visible = true;
            this.colDescription.VisibleIndex = 1;
            // 
            // ucDepriciationView
            // 
            this.ucDepriciationView.ChangeAddCaption = "&Add";
            this.ucDepriciationView.ChangeCaption = "&Edit";
            this.ucDepriciationView.ChangeDeleteCaption = "&Delete";
            this.ucDepriciationView.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucDepriciationView.ChangePrintCaption = "&Print";
            this.ucDepriciationView.DisableAddButton = true;
            this.ucDepriciationView.DisableCloseButton = true;
            this.ucDepriciationView.DisableDeleteButton = true;
            this.ucDepriciationView.DisableDownloadExcel = true;
            this.ucDepriciationView.DisableEditButton = true;
            this.ucDepriciationView.DisableMoveTransaction = true;
            this.ucDepriciationView.DisableNatureofPayments = true;
            this.ucDepriciationView.DisablePrintButton = true;
            this.ucDepriciationView.DisableRestoreVoucher = true;
            this.ucDepriciationView.Location = new System.Drawing.Point(0, 0);
            this.ucDepriciationView.Name = "ucDepriciationView";
            this.ucDepriciationView.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.ucDepriciationView.ShowHTML = true;
            this.ucDepriciationView.ShowMMT = true;
            this.ucDepriciationView.ShowPDF = true;
            this.ucDepriciationView.ShowRTF = true;
            this.ucDepriciationView.ShowText = true;
            this.ucDepriciationView.ShowXLS = true;
            this.ucDepriciationView.ShowXLSX = true;
            this.ucDepriciationView.Size = new System.Drawing.Size(1138, 32);
            this.ucDepriciationView.TabIndex = 4;
            this.ucDepriciationView.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucDepriciationView.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucDepriciationView.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucDepriciationView.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucDepriciationView.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucDepriciationView.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucDepriciationView.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucDepriciationView.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucDepriciationView.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucDepriciationView.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucDepriciationView.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucDepriciationView.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucDepriciationView.AddClicked += new System.EventHandler(this.ucDepriciationView_AddClicked);
            this.ucDepriciationView.EditClicked += new System.EventHandler(this.ucDepriciationView_EditClicked);
            this.ucDepriciationView.DeleteClicked += new System.EventHandler(this.ucDepriciationView_DeleteClicked);
            this.ucDepriciationView.PrintClicked += new System.EventHandler(this.ucDepriciationView_PrintClicked);
            this.ucDepriciationView.CloseClicked += new System.EventHandler(this.ucDepriciationView_CloseClicked);
            this.ucDepriciationView.RefreshClicked += new System.EventHandler(this.ucDepriciationView_RefreshClicked);
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
            this.lblCountNumber,
            this.lblCount});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1138, 448);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(126, 424);
            this.emptySpaceItem2.MaxSize = new System.Drawing.Size(952, 24);
            this.emptySpaceItem2.MinSize = new System.Drawing.Size(952, 24);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(952, 24);
            this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ucDepriciationView;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 32);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(196, 32);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem1.Size = new System.Drawing.Size(1138, 32);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcDepreciationView;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 32);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(196, 20);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem2.Size = new System.Drawing.Size(1138, 392);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkShowFilter;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 424);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(126, 23);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(126, 23);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(126, 24);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // lblCountNumber
            // 
            this.lblCountNumber.AllowHotTrack = false;
            this.lblCountNumber.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblCountNumber.AppearanceItemCaption.Options.UseFont = true;
            this.lblCountNumber.CustomizationFormText = "0";
            this.lblCountNumber.Location = new System.Drawing.Point(1091, 424);
            this.lblCountNumber.MaxSize = new System.Drawing.Size(47, 24);
            this.lblCountNumber.MinSize = new System.Drawing.Size(47, 24);
            this.lblCountNumber.Name = "lblCountNumber";
            this.lblCountNumber.Size = new System.Drawing.Size(47, 24);
            this.lblCountNumber.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblCountNumber.Text = "0";
            this.lblCountNumber.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblCountNumber.TextSize = new System.Drawing.Size(7, 13);
            // 
            // lblCount
            // 
            this.lblCount.AllowHotTrack = false;
            this.lblCount.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblCount.AppearanceItemCaption.Options.UseFont = true;
            this.lblCount.CustomizationFormText = "#";
            this.lblCount.Location = new System.Drawing.Point(1078, 424);
            this.lblCount.MaxSize = new System.Drawing.Size(13, 24);
            this.lblCount.MinSize = new System.Drawing.Size(13, 24);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(13, 24);
            this.lblCount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblCount.Text = "#";
            this.lblCount.TextSize = new System.Drawing.Size(9, 13);
            // 
            // frmDepreciationView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1138, 448);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmDepreciationView";
            this.ShowIcon = false;
            this.Text = "Depreciation Method";
            this.ShowFilterClicked += new System.EventHandler(this.frmDepreciationView_ShowFilterClicked);
            this.Load += new System.EventHandler(this.frmDepreciationView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDepreciationView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDepreciationView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCountNumber)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCount)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gcDepreciationView;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDepreciationView;
        private Bosco.Utility.Controls.ucToolBar ucDepriciationView;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraLayout.SimpleLabelItem lblCountNumber;
        private DevExpress.XtraLayout.SimpleLabelItem lblCount;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription;

    }
}