namespace PAYROLL.Modules.Payroll_app
{
    partial class frmPayrollLoanView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPayrollLoanView));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.gcPayrollLoanView = new DevExpress.XtraGrid.GridControl();
            this.gvPayrollLoanView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colPayrollLoanId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLoanAbbreviation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLoanName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ucToolBar1 = new PAYROLL.UserControl.UcToolBar();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblRecordCount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPayrollLoanView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPayrollLoanView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRecordCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.gcPayrollLoanView);
            this.layoutControl1.Controls.Add(this.ucToolBar1);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(819, 233, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // chkShowFilter
            // 
            resources.ApplyResources(this.chkShowFilter, "chkShowFilter");
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = resources.GetString("chkShowFilter.Properties.Caption");
            this.chkShowFilter.StyleController = this.layoutControl1;
            // 
            // gcPayrollLoanView
            // 
            resources.ApplyResources(this.gcPayrollLoanView, "gcPayrollLoanView");
            this.gcPayrollLoanView.MainView = this.gvPayrollLoanView;
            this.gcPayrollLoanView.Name = "gcPayrollLoanView";
            this.gcPayrollLoanView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPayrollLoanView});
            // 
            // gvPayrollLoanView
            // 
            this.gvPayrollLoanView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colPayrollLoanId,
            this.colLoanAbbreviation,
            this.colLoanName});
            this.gvPayrollLoanView.GridControl = this.gcPayrollLoanView;
            this.gvPayrollLoanView.Name = "gvPayrollLoanView";
            this.gvPayrollLoanView.OptionsBehavior.Editable = false;
            this.gvPayrollLoanView.OptionsView.ShowGroupPanel = false;
            // 
            // colPayrollLoanId
            // 
            resources.ApplyResources(this.colPayrollLoanId, "colPayrollLoanId");
            this.colPayrollLoanId.Name = "colPayrollLoanId";
            // 
            // colLoanAbbreviation
            // 
            this.colLoanAbbreviation.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colLoanAbbreviation.AppearanceHeader.Font")));
            this.colLoanAbbreviation.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colLoanAbbreviation, "colLoanAbbreviation");
            this.colLoanAbbreviation.Name = "colLoanAbbreviation";
            // 
            // colLoanName
            // 
            this.colLoanName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colLoanName.AppearanceHeader.Font")));
            this.colLoanName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colLoanName, "colLoanName");
            this.colLoanName.Name = "colLoanName";
            // 
            // ucToolBar1
            // 
            this.ucToolBar1.ChangeAddCaption = "<u>&A</u>dd";
            this.ucToolBar1.DisableAddButton = true;
            this.ucToolBar1.DisableCloseButton = true;
            this.ucToolBar1.DisableDeleteButton = true;
            this.ucToolBar1.DisableEditButton = true;
            this.ucToolBar1.DisableImportButton = true;
            this.ucToolBar1.DisablePrintButton = true;
            this.ucToolBar1.DisableRefreshButton = true;
            resources.ApplyResources(this.ucToolBar1, "ucToolBar1");
            this.ucToolBar1.Name = "ucToolBar1";
            this.ucToolBar1.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar1.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar1.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar1.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar1.VisibleImport = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar1.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.simpleLabelItem1,
            this.lblRecordCount,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(677, 367);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ucToolBar1;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(677, 32);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcPayrollLoanView;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 32);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(677, 295);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkShowFilter;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 327);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(677, 23);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // simpleLabelItem1
            // 
            this.simpleLabelItem1.AllowHotTrack = false;
            this.simpleLabelItem1.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("simpleLabelItem1.AppearanceItemCaption.Font")));
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.simpleLabelItem1, "simpleLabelItem1");
            this.simpleLabelItem1.Location = new System.Drawing.Point(623, 350);
            this.simpleLabelItem1.MaxSize = new System.Drawing.Size(13, 17);
            this.simpleLabelItem1.MinSize = new System.Drawing.Size(13, 17);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Size = new System.Drawing.Size(13, 17);
            this.simpleLabelItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(9, 13);
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.AllowHotTrack = false;
            this.lblRecordCount.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblRecordCount.AppearanceItemCaption.Font")));
            this.lblRecordCount.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblRecordCount, "lblRecordCount");
            this.lblRecordCount.Location = new System.Drawing.Point(636, 350);
            this.lblRecordCount.MaxSize = new System.Drawing.Size(41, 17);
            this.lblRecordCount.MinSize = new System.Drawing.Size(41, 17);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(41, 17);
            this.lblRecordCount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblRecordCount.TextSize = new System.Drawing.Size(9, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 350);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(623, 17);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmPayrollLoanView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPayrollLoanView";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPayrollLoanView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPayrollLoanView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRecordCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private UserControl.UcToolBar ucToolBar1;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraGrid.GridControl gcPayrollLoanView;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPayrollLoanView;
        private DevExpress.XtraGrid.Columns.GridColumn colPayrollLoanId;
        private DevExpress.XtraGrid.Columns.GridColumn colLoanAbbreviation;
        private DevExpress.XtraGrid.Columns.GridColumn colLoanName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraLayout.SimpleLabelItem lblRecordCount;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;

    }
}