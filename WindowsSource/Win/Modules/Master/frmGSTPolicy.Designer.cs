namespace ACPP.Modules.Master
{
    partial class frmGSTPolicy
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmGSTPolicy));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.panelControl3 = new DevExpress.XtraEditors.PanelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.lblGSTRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.panelControl2 = new DevExpress.XtraEditors.PanelControl();
            this.gcGSTPolicy = new DevExpress.XtraGrid.GridControl();
            this.gvGSTPolicy = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colGSTId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSlab = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGST = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCGst = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSGST = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colApplicableFrom = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.panelControl1 = new DevExpress.XtraEditors.PanelControl();
            this.UcGSTPolicy = new Bosco.Utility.Controls.ucToolBar();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).BeginInit();
            this.panelControl3.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).BeginInit();
            this.panelControl2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcGSTPolicy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvGSTPolicy)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).BeginInit();
            this.panelControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.panelControl3);
            this.layoutControl1.Controls.Add(this.panelControl2);
            this.layoutControl1.Controls.Add(this.panelControl1);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(245, 138, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // panelControl3
            // 
            this.panelControl3.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl3.Controls.Add(this.labelControl1);
            this.panelControl3.Controls.Add(this.lblGSTRecordCount);
            this.panelControl3.Controls.Add(this.chkShowFilter);
            resources.ApplyResources(this.panelControl3, "panelControl3");
            this.panelControl3.Name = "panelControl3";
            // 
            // labelControl1
            // 
            resources.ApplyResources(this.labelControl1, "labelControl1");
            this.labelControl1.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("labelControl1.Appearance.Font")));
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.StyleController = this.layoutControl1;
            // 
            // lblGSTRecordCount
            // 
            resources.ApplyResources(this.lblGSTRecordCount, "lblGSTRecordCount");
            this.lblGSTRecordCount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblGSTRecordCount.Appearance.Font")));
            this.lblGSTRecordCount.Name = "lblGSTRecordCount";
            this.lblGSTRecordCount.StyleController = this.layoutControl1;
            // 
            // chkShowFilter
            // 
            resources.ApplyResources(this.chkShowFilter, "chkShowFilter");
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = resources.GetString("chkShowFilter.Properties.Caption");
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // panelControl2
            // 
            this.panelControl2.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl2.Controls.Add(this.gcGSTPolicy);
            resources.ApplyResources(this.panelControl2, "panelControl2");
            this.panelControl2.Name = "panelControl2";
            // 
            // gcGSTPolicy
            // 
            resources.ApplyResources(this.gcGSTPolicy, "gcGSTPolicy");
            this.gcGSTPolicy.MainView = this.gvGSTPolicy;
            this.gcGSTPolicy.Name = "gcGSTPolicy";
            this.gcGSTPolicy.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvGSTPolicy});
            // 
            // gvGSTPolicy
            // 
            this.gvGSTPolicy.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvGSTPolicy.Appearance.FocusedRow.Font")));
            this.gvGSTPolicy.Appearance.FocusedRow.Options.UseFont = true;
            this.gvGSTPolicy.AppearancePrint.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvGSTPolicy.AppearancePrint.HeaderPanel.Font")));
            this.gvGSTPolicy.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gvGSTPolicy.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.gvGSTPolicy.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colGSTId,
            this.colSlab,
            this.colGST,
            this.colCGst,
            this.colSGST,
            this.colApplicableFrom,
            this.colStatus});
            this.gvGSTPolicy.GridControl = this.gcGSTPolicy;
            this.gvGSTPolicy.Name = "gvGSTPolicy";
            this.gvGSTPolicy.OptionsBehavior.Editable = false;
            this.gvGSTPolicy.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvGSTPolicy.OptionsView.ShowGroupPanel = false;
            this.gvGSTPolicy.DoubleClick += new System.EventHandler(this.gvGSTPolicy_DoubleClick);
            this.gvGSTPolicy.RowCountChanged += new System.EventHandler(this.gvGSTPolicy_RowCountChanged);
            // 
            // colGSTId
            // 
            resources.ApplyResources(this.colGSTId, "colGSTId");
            this.colGSTId.FieldName = "GST_Id";
            this.colGSTId.Name = "colGSTId";
            // 
            // colSlab
            // 
            this.colSlab.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colSlab.AppearanceHeader.Font")));
            this.colSlab.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colSlab, "colSlab");
            this.colSlab.FieldName = "SLAB";
            this.colSlab.Name = "colSlab";
            this.colSlab.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colGST
            // 
            this.colGST.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colGST.AppearanceHeader.Font")));
            this.colGST.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colGST, "colGST");
            this.colGST.FieldName = "GST";
            this.colGST.Name = "colGST";
            // 
            // colCGst
            // 
            this.colCGst.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colCGst.AppearanceHeader.Font")));
            this.colCGst.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colCGst, "colCGst");
            this.colCGst.FieldName = "CGST";
            this.colCGst.Name = "colCGst";
            // 
            // colSGST
            // 
            this.colSGST.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colSGST.AppearanceHeader.Font")));
            this.colSGST.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colSGST, "colSGST");
            this.colSGST.FieldName = "SGST";
            this.colSGST.Name = "colSGST";
            // 
            // colApplicableFrom
            // 
            this.colApplicableFrom.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colApplicableFrom.AppearanceHeader.Font")));
            this.colApplicableFrom.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colApplicableFrom, "colApplicableFrom");
            this.colApplicableFrom.FieldName = "APPLICABLE_FROM";
            this.colApplicableFrom.Name = "colApplicableFrom";
            // 
            // colStatus
            // 
            this.colStatus.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colStatus.AppearanceHeader.Font")));
            this.colStatus.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colStatus, "colStatus");
            this.colStatus.FieldName = "STATUS";
            this.colStatus.Name = "colStatus";
            // 
            // panelControl1
            // 
            this.panelControl1.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.NoBorder;
            this.panelControl1.Controls.Add(this.UcGSTPolicy);
            resources.ApplyResources(this.panelControl1, "panelControl1");
            this.panelControl1.Name = "panelControl1";
            // 
            // UcGSTPolicy
            // 
            this.UcGSTPolicy.ChangeAddCaption = "&Add";
            this.UcGSTPolicy.ChangeCaption = "&Edit";
            this.UcGSTPolicy.ChangeDeleteCaption = "&Delete";
            this.UcGSTPolicy.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.UcGSTPolicy.ChangePostInterestCaption = "P&ost Interest";
            this.UcGSTPolicy.ChangePrintCaption = "&Print";
            this.UcGSTPolicy.ChnageRenewCaption = "Re<u>n</u>ew";
            this.UcGSTPolicy.DisableAddButton = true;
            this.UcGSTPolicy.DisableAMCRenew = true;
            this.UcGSTPolicy.DisableCloseButton = true;
            this.UcGSTPolicy.DisableDeleteButton = true;
            this.UcGSTPolicy.DisableDownloadExcel = true;
            this.UcGSTPolicy.DisableEditButton = true;
            this.UcGSTPolicy.DisableMoveTransaction = true;
            this.UcGSTPolicy.DisableNatureofPayments = true;
            this.UcGSTPolicy.DisablePostInterest = true;
            this.UcGSTPolicy.DisablePrintButton = true;
            this.UcGSTPolicy.DisableRestoreVoucher = true;
            resources.ApplyResources(this.UcGSTPolicy, "UcGSTPolicy");
            this.UcGSTPolicy.Name = "UcGSTPolicy";
            this.UcGSTPolicy.ShowHTML = true;
            this.UcGSTPolicy.ShowMMT = true;
            this.UcGSTPolicy.ShowPDF = true;
            this.UcGSTPolicy.ShowRTF = true;
            this.UcGSTPolicy.ShowText = true;
            this.UcGSTPolicy.ShowXLS = true;
            this.UcGSTPolicy.ShowXLSX = true;
            this.UcGSTPolicy.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.UcGSTPolicy.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.UcGSTPolicy.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.UcGSTPolicy.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.UcGSTPolicy.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.UcGSTPolicy.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.UcGSTPolicy.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.UcGSTPolicy.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.UcGSTPolicy.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.UcGSTPolicy.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.UcGSTPolicy.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.UcGSTPolicy.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.UcGSTPolicy.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.UcGSTPolicy.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.UcGSTPolicy.AddClicked += new System.EventHandler(this.UcGSTPolicy_AddClicked);
            this.UcGSTPolicy.EditClicked += new System.EventHandler(this.UcGSTPolicy_EditClicked);
            this.UcGSTPolicy.DeleteClicked += new System.EventHandler(this.UcGSTPolicy_DeleteClicked);
            this.UcGSTPolicy.PrintClicked += new System.EventHandler(this.UcGSTPolicy_PrintClicked);
            this.UcGSTPolicy.CloseClicked += new System.EventHandler(this.UcGSTPolicy_CloseClicked);
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem6,
            this.layoutControlItem7,
            this.layoutControlItem8});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(900, 428);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.panelControl1;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem6.Size = new System.Drawing.Size(900, 20);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.panelControl2;
            resources.ApplyResources(this.layoutControlItem7, "layoutControlItem7");
            this.layoutControlItem7.Location = new System.Drawing.Point(0, 20);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Size = new System.Drawing.Size(900, 384);
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.panelControl3;
            resources.ApplyResources(this.layoutControlItem8, "layoutControlItem8");
            this.layoutControlItem8.Location = new System.Drawing.Point(0, 404);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Size = new System.Drawing.Size(900, 24);
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // frmGSTPolicy
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "frmGSTPolicy";
            this.EnterClicked += new System.EventHandler(this.frmGSTPolicy_EnterClicked);
            this.Load += new System.EventHandler(this.frmGSTPolicy_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.panelControl3)).EndInit();
            this.panelControl3.ResumeLayout(false);
            this.panelControl3.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl2)).EndInit();
            this.panelControl2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcGSTPolicy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvGSTPolicy)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.panelControl1)).EndInit();
            this.panelControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraEditors.PanelControl panelControl3;
        private DevExpress.XtraEditors.LabelControl lblGSTRecordCount;
        private DevExpress.XtraEditors.PanelControl panelControl2;
        private DevExpress.XtraGrid.GridControl gcGSTPolicy;
        private DevExpress.XtraGrid.Views.Grid.GridView gvGSTPolicy;
        private DevExpress.XtraGrid.Columns.GridColumn colGSTId;
        private DevExpress.XtraGrid.Columns.GridColumn colSlab;
        private DevExpress.XtraGrid.Columns.GridColumn colGST;
        private DevExpress.XtraGrid.Columns.GridColumn colCGst;
        private DevExpress.XtraGrid.Columns.GridColumn colSGST;
        private DevExpress.XtraGrid.Columns.GridColumn colApplicableFrom;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
        private DevExpress.XtraEditors.PanelControl panelControl1;
        private Bosco.Utility.Controls.ucToolBar UcGSTPolicy;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraEditors.LabelControl labelControl1;
    }
}