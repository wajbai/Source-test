namespace PAYROLL.Modules.Payroll_app
{
    partial class frmPayrollWorkLocationView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPayrollWorkLocationView));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.ucToolBar1 = new PAYROLL.UserControl.UcToolBar();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.lblRowCount = new DevExpress.XtraEditors.LabelControl();
            this.gcPayrollWorkLocationView = new DevExpress.XtraGrid.GridControl();
            this.gvPayrollWorkLocationView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colPayrollWorkLocationId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPayrollWorkLocation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.labelControl2 = new DevExpress.XtraEditors.LabelControl();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPayrollWorkLocationView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPayrollWorkLocationView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ucToolBar1);
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.lblRowCount);
            this.layoutControl1.Controls.Add(this.gcPayrollWorkLocationView);
            this.layoutControl1.Controls.Add(this.labelControl2);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(82, 241, 250, 345);
            this.layoutControl1.Root = this.layoutControlGroup1;
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
            this.ucToolBar1.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar1.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleImport = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar1.AddClicked += new System.EventHandler(this.ucToolBar1_AddClicked);
            this.ucToolBar1.EditClicked += new System.EventHandler(this.ucToolBar1_EditClicked);
            this.ucToolBar1.DeleteClicked += new System.EventHandler(this.ucToolBar1_DeleteClicked);
            this.ucToolBar1.PrintClicked += new System.EventHandler(this.ucToolBar1_PrintClicked);
            this.ucToolBar1.CloseClicked += new System.EventHandler(this.ucToolBar1_CloseClicked);
            this.ucToolBar1.RefreshClicked += new System.EventHandler(this.ucToolBar1_RefreshClicked);
            this.ucToolBar1.DoubleClick += new System.EventHandler(this.ucToolBar1_DoubleClick);
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
            // lblRowCount
            // 
            resources.ApplyResources(this.lblRowCount, "lblRowCount");
            this.lblRowCount.Name = "lblRowCount";
            this.lblRowCount.StyleController = this.layoutControl1;
            // 
            // gcPayrollWorkLocationView
            // 
            resources.ApplyResources(this.gcPayrollWorkLocationView, "gcPayrollWorkLocationView");
            this.gcPayrollWorkLocationView.MainView = this.gvPayrollWorkLocationView;
            this.gcPayrollWorkLocationView.Name = "gcPayrollWorkLocationView";
            this.gcPayrollWorkLocationView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvPayrollWorkLocationView});
            // 
            // gvPayrollWorkLocationView
            // 
            this.gvPayrollWorkLocationView.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvPayrollDepartmentView.Appearance.FocusedRow.Font")));
            this.gvPayrollWorkLocationView.Appearance.FocusedRow.Options.UseFont = true;
            this.gvPayrollWorkLocationView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colPayrollWorkLocationId,
            this.colPayrollWorkLocation});
            this.gvPayrollWorkLocationView.GridControl = this.gcPayrollWorkLocationView;
            this.gvPayrollWorkLocationView.Name = "gvPayrollWorkLocationView";
            this.gvPayrollWorkLocationView.OptionsBehavior.Editable = false;
            this.gvPayrollWorkLocationView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvPayrollWorkLocationView.OptionsView.ShowGroupPanel = false;
            this.gvPayrollWorkLocationView.OptionsView.ShowIndicator = false;
            this.gvPayrollWorkLocationView.DoubleClick += new System.EventHandler(this.gvPayrollGroupView_DoubleClick);
            this.gvPayrollWorkLocationView.RowCountChanged += new System.EventHandler(this.gvPayrollGroupView_RowCountChanged);
            // 
            // colPayrollWorkLocationId
            // 
            this.colPayrollWorkLocationId.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colPaymonthDepartmentId.AppearanceHeader.Font")));
            this.colPayrollWorkLocationId.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colPayrollWorkLocationId, "colPayrollWorkLocationId");
            this.colPayrollWorkLocationId.FieldName = "WORK_LOCATION_ID";
            this.colPayrollWorkLocationId.Name = "colPayrollWorkLocationId";
            this.colPayrollWorkLocationId.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colPayrollWorkLocation
            // 
            this.colPayrollWorkLocation.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colPayrollDepartment.AppearanceHeader.Font")));
            this.colPayrollWorkLocation.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colPayrollWorkLocation, "colPayrollWorkLocation");
            this.colPayrollWorkLocation.FieldName = "WORK_LOCATION";
            this.colPayrollWorkLocation.Name = "colPayrollWorkLocation";
            this.colPayrollWorkLocation.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // labelControl2
            // 
            this.labelControl2.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl2.Appearance.Font")));
            resources.ApplyResources(this.labelControl2, "labelControl2");
            this.labelControl2.Name = "labelControl2";
            this.labelControl2.StyleController = this.layoutControl1;
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem1,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(-1, -1, -1, -1);
            this.layoutControlGroup1.Size = new System.Drawing.Size(894, 342);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcPayrollWorkLocationView;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 30);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(-1, -1, -1, -1);
            this.layoutControlItem2.Size = new System.Drawing.Size(894, 291);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.chkShowFilter;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 321);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutControlItem1.Size = new System.Drawing.Size(871, 21);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.lblRowCount;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(882, 321);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(8, 15);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutControlItem3.Size = new System.Drawing.Size(12, 21);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.labelControl2;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(871, 321);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(11, 15);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.layoutControlItem4.Size = new System.Drawing.Size(11, 21);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.ucToolBar1;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(0, 30);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(98, 30);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(-1, -1, -1, -1);
            this.layoutControlItem5.Size = new System.Drawing.Size(894, 30);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // frmPayrollWorkLocationView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPayrollWorkLocationView";
            this.ShowFilterClicked += new System.EventHandler(this.frmPayrollGroupView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmPayrollGroupView_EnterClicked);
            this.Load += new System.EventHandler(this.frmPayrollGroupView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcPayrollWorkLocationView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvPayrollWorkLocationView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraGrid.GridControl gcPayrollWorkLocationView;
        private DevExpress.XtraGrid.Views.Grid.GridView gvPayrollWorkLocationView;
        private DevExpress.XtraGrid.Columns.GridColumn colPayrollWorkLocationId;
        private DevExpress.XtraGrid.Columns.GridColumn colPayrollWorkLocation;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.LabelControl labelControl2;
        private DevExpress.XtraEditors.LabelControl lblRowCount;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private UserControl.UcToolBar ucToolBar1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    }
}

