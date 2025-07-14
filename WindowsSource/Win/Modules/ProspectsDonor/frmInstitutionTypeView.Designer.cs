namespace ACPP.Modules.ProspectsDonor
{
    partial class frmInstitutionTypeView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmInstitutionTypeView));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.gcInstitutionType = new DevExpress.XtraGrid.GridControl();
            this.gvInstitutionType = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colINSTITUTIONALTYPE = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ucToolBar1 = new Bosco.Utility.Controls.ucToolBar();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblRowCount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblCountSymbol = new DevExpress.XtraLayout.SimpleLabelItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcInstitutionType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInstitutionType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRowCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCountSymbol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.gcInstitutionType);
            this.layoutControl1.Controls.Add(this.ucToolBar1);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(149, 173, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
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
            // gcInstitutionType
            // 
            resources.ApplyResources(this.gcInstitutionType, "gcInstitutionType");
            this.gcInstitutionType.MainView = this.gvInstitutionType;
            this.gcInstitutionType.Name = "gcInstitutionType";
            this.gcInstitutionType.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvInstitutionType});
            this.gcInstitutionType.DoubleClick += new System.EventHandler(this.gcInstitutionType_DoubleClick);
            // 
            // gvInstitutionType
            // 
            this.gvInstitutionType.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvInstitutionType.Appearance.FocusedRow.Font")));
            this.gvInstitutionType.Appearance.FocusedRow.Options.UseFont = true;
            this.gvInstitutionType.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvInstitutionType.Appearance.HeaderPanel.Font")));
            this.gvInstitutionType.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvInstitutionType.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colINSTITUTIONALTYPE});
            this.gvInstitutionType.GridControl = this.gcInstitutionType;
            this.gvInstitutionType.Name = "gvInstitutionType";
            this.gvInstitutionType.OptionsBehavior.AllowAddRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvInstitutionType.OptionsBehavior.AllowDeleteRows = DevExpress.Utils.DefaultBoolean.False;
            this.gvInstitutionType.OptionsBehavior.Editable = false;
            this.gvInstitutionType.OptionsCustomization.AllowGroup = false;
            this.gvInstitutionType.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvInstitutionType.OptionsView.ShowGroupPanel = false;
            this.gvInstitutionType.RowCountChanged += new System.EventHandler(this.gvInstitutionType_RowCountChanged);
            // 
            // colId
            // 
            this.colId.FieldName = "INSTITUTIONAL_TYPE_ID";
            this.colId.Name = "colId";
            // 
            // colINSTITUTIONALTYPE
            // 
            this.colINSTITUTIONALTYPE.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colINSTITUTIONALTYPE.AppearanceHeader.Font")));
            resources.ApplyResources(this.colINSTITUTIONALTYPE, "colINSTITUTIONALTYPE");
            this.colINSTITUTIONALTYPE.FieldName = "INSTITUTIONAL_TYPE";
            this.colINSTITUTIONALTYPE.Name = "colINSTITUTIONALTYPE";
            this.colINSTITUTIONALTYPE.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // ucToolBar1
            // 
            this.ucToolBar1.ChangeAddCaption = "&Add";
            this.ucToolBar1.ChangeCaption = "&Edit";
            this.ucToolBar1.ChangeDeleteCaption = "&Delete";
            this.ucToolBar1.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucToolBar1.ChangePostInterestCaption = "P&ost Interest";
            this.ucToolBar1.ChangePrintCaption = "&Print";
            this.ucToolBar1.ChnageRenewCaption = "Re<u>n</u>ew";
            this.ucToolBar1.DisableAddButton = true;
            this.ucToolBar1.DisableAMCRenew = true;
            this.ucToolBar1.DisableCloseButton = true;
            this.ucToolBar1.DisableDeleteButton = true;
            this.ucToolBar1.DisableDownloadExcel = true;
            this.ucToolBar1.DisableEditButton = true;
            this.ucToolBar1.DisableMoveTransaction = true;
            this.ucToolBar1.DisableNatureofPayments = true;
            this.ucToolBar1.DisablePostInterest = true;
            this.ucToolBar1.DisablePrintButton = true;
            this.ucToolBar1.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucToolBar1, "ucToolBar1");
            this.ucToolBar1.Name = "ucToolBar1";
            this.ucToolBar1.ShowHTML = true;
            this.ucToolBar1.ShowMMT = true;
            this.ucToolBar1.ShowPDF = true;
            this.ucToolBar1.ShowRTF = true;
            this.ucToolBar1.ShowText = true;
            this.ucToolBar1.ShowXLS = true;
            this.ucToolBar1.ShowXLSX = true;
            this.ucToolBar1.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar1.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar1.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar1.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar1.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar1.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar1.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.AddClicked += new System.EventHandler(this.ucToolBar1_AddClicked);
            this.ucToolBar1.EditClicked += new System.EventHandler(this.ucToolBar1_EditClicked);
            this.ucToolBar1.DeleteClicked += new System.EventHandler(this.ucToolBar1_DeleteClicked);
            this.ucToolBar1.PrintClicked += new System.EventHandler(this.ucToolBar1_PrintClicked);
            this.ucToolBar1.CloseClicked += new System.EventHandler(this.ucToolBar1_CloseClicked);
            this.ucToolBar1.RefreshClicked += new System.EventHandler(this.ucToolBar1_RefreshClicked);
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
            this.lblRowCount,
            this.lblCountSymbol,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(875, 357);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ucToolBar1;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 31);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(196, 31);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem1.Size = new System.Drawing.Size(875, 31);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcInstitutionType;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 31);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem2.Size = new System.Drawing.Size(875, 303);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkShowFilter;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 334);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(91, 23);
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
            this.lblRowCount.Location = new System.Drawing.Point(852, 334);
            this.lblRowCount.MaxSize = new System.Drawing.Size(23, 23);
            this.lblRowCount.MinSize = new System.Drawing.Size(23, 23);
            this.lblRowCount.Name = "lblRowCount";
            this.lblRowCount.Size = new System.Drawing.Size(23, 23);
            this.lblRowCount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblRowCount.TextSize = new System.Drawing.Size(9, 13);
            // 
            // lblCountSymbol
            // 
            this.lblCountSymbol.AllowHotTrack = false;
            this.lblCountSymbol.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblCountSymbol.AppearanceItemCaption.Font")));
            this.lblCountSymbol.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblCountSymbol, "lblCountSymbol");
            this.lblCountSymbol.Location = new System.Drawing.Point(839, 334);
            this.lblCountSymbol.MaxSize = new System.Drawing.Size(13, 23);
            this.lblCountSymbol.MinSize = new System.Drawing.Size(13, 23);
            this.lblCountSymbol.Name = "lblCountSymbol";
            this.lblCountSymbol.Size = new System.Drawing.Size(13, 23);
            this.lblCountSymbol.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblCountSymbol.TextSize = new System.Drawing.Size(9, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(91, 334);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(748, 23);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmInstitutionTypeView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmInstitutionTypeView";
            this.ShowFilterClicked += new System.EventHandler(this.frmInstitutionTypeView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmInstitutionTypeView_EnterClicked);
            this.Activated += new System.EventHandler(this.frmInstitutionTypeView_Activated);
            this.Load += new System.EventHandler(this.frmInstitutionTypeView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcInstitutionType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInstitutionType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRowCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCountSymbol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private Bosco.Utility.Controls.ucToolBar ucToolBar1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.GridControl gcInstitutionType;
        private DevExpress.XtraGrid.Views.Grid.GridView gvInstitutionType;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.SimpleLabelItem lblRowCount;
        private DevExpress.XtraLayout.SimpleLabelItem lblCountSymbol;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colINSTITUTIONALTYPE;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}