namespace ACPP.Modules.ProspectsDonor
{
    partial class frmDonorViewRegistrationType
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmDonorViewRegistrationType));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.ucRegistrationToolBar = new Bosco.Utility.Controls.ucToolBar();
            this.lblNo = new DevExpress.XtraEditors.LabelControl();
            this.lblCount = new DevExpress.XtraEditors.LabelControl();
            this.chkShowFilet = new DevExpress.XtraEditors.CheckEdit();
            this.gcRegistrationType = new DevExpress.XtraGrid.GridControl();
            this.gvRegistrationtype = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colRegistrationTypeID = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRegistrationType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilet.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcRegistrationType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRegistrationtype)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ucRegistrationToolBar);
            this.layoutControl1.Controls.Add(this.lblNo);
            this.layoutControl1.Controls.Add(this.lblCount);
            this.layoutControl1.Controls.Add(this.chkShowFilet);
            this.layoutControl1.Controls.Add(this.gcRegistrationType);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(325, 314, 391, 383);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // ucRegistrationToolBar
            // 
            this.ucRegistrationToolBar.ChangeAddCaption = "&Add";
            this.ucRegistrationToolBar.ChangeCaption = "&Edit";
            this.ucRegistrationToolBar.ChangeDeleteCaption = "&Delete";
            this.ucRegistrationToolBar.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucRegistrationToolBar.ChangePostInterestCaption = "P&ost Interest";
            this.ucRegistrationToolBar.ChangePrintCaption = "&Print";
            this.ucRegistrationToolBar.ChnageRenewCaption = "Re<u>n</u>ew";
            this.ucRegistrationToolBar.DisableAddButton = true;
            this.ucRegistrationToolBar.DisableAMCRenew = false;
            this.ucRegistrationToolBar.DisableCloseButton = true;
            this.ucRegistrationToolBar.DisableDeleteButton = true;
            this.ucRegistrationToolBar.DisableDownloadExcel = false;
            this.ucRegistrationToolBar.DisableEditButton = true;
            this.ucRegistrationToolBar.DisableMoveTransaction = false;
            this.ucRegistrationToolBar.DisableNatureofPayments = false;
            this.ucRegistrationToolBar.DisablePostInterest = false;
            this.ucRegistrationToolBar.DisablePrintButton = true;
            this.ucRegistrationToolBar.DisableRestoreVoucher = false;
            resources.ApplyResources(this.ucRegistrationToolBar, "ucRegistrationToolBar");
            this.ucRegistrationToolBar.Name = "ucRegistrationToolBar";
            this.ucRegistrationToolBar.ShowHTML = true;
            this.ucRegistrationToolBar.ShowMMT = true;
            this.ucRegistrationToolBar.ShowPDF = true;
            this.ucRegistrationToolBar.ShowRTF = true;
            this.ucRegistrationToolBar.ShowText = true;
            this.ucRegistrationToolBar.ShowXLS = true;
            this.ucRegistrationToolBar.ShowXLSX = true;
            this.ucRegistrationToolBar.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucRegistrationToolBar.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucRegistrationToolBar.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucRegistrationToolBar.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucRegistrationToolBar.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucRegistrationToolBar.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucRegistrationToolBar.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucRegistrationToolBar.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucRegistrationToolBar.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucRegistrationToolBar.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucRegistrationToolBar.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucRegistrationToolBar.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucRegistrationToolBar.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucRegistrationToolBar.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucRegistrationToolBar.AddClicked += new System.EventHandler(this.ucRegistrationToolBar_AddClicked);
            this.ucRegistrationToolBar.EditClicked += new System.EventHandler(this.ucRegistrationToolBar_EditClicked);
            this.ucRegistrationToolBar.DeleteClicked += new System.EventHandler(this.ucRegistrationToolBar_DeleteClicked);
            this.ucRegistrationToolBar.PrintClicked += new System.EventHandler(this.ucRegistrationToolBar_PrintClicked);
            this.ucRegistrationToolBar.CloseClicked += new System.EventHandler(this.ucRegistrationToolBar_CloseClicked);
            this.ucRegistrationToolBar.RefreshClicked += new System.EventHandler(this.ucRegistrationToolBar_RefreshClicked);
            // 
            // lblNo
            // 
            this.lblNo.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblNo.Appearance.Font")));
            resources.ApplyResources(this.lblNo, "lblNo");
            this.lblNo.Name = "lblNo";
            this.lblNo.StyleController = this.layoutControl1;
            // 
            // lblCount
            // 
            this.lblCount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblCount.Appearance.Font")));
            resources.ApplyResources(this.lblCount, "lblCount");
            this.lblCount.Name = "lblCount";
            this.lblCount.StyleController = this.layoutControl1;
            // 
            // chkShowFilet
            // 
            resources.ApplyResources(this.chkShowFilet, "chkShowFilet");
            this.chkShowFilet.Name = "chkShowFilet";
            this.chkShowFilet.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilet.Properties.Caption = resources.GetString("chkShowFilet.Properties.Caption");
            this.chkShowFilet.StyleController = this.layoutControl1;
            this.chkShowFilet.CheckedChanged += new System.EventHandler(this.chkShowFilet_CheckedChanged);
            // 
            // gcRegistrationType
            // 
            resources.ApplyResources(this.gcRegistrationType, "gcRegistrationType");
            this.gcRegistrationType.MainView = this.gvRegistrationtype;
            this.gcRegistrationType.Name = "gcRegistrationType";
            this.gcRegistrationType.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvRegistrationtype});
            // 
            // gvRegistrationtype
            // 
            this.gvRegistrationtype.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvRegistrationtype.Appearance.FocusedRow.Font")));
            this.gvRegistrationtype.Appearance.FocusedRow.Options.UseFont = true;
            this.gvRegistrationtype.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colRegistrationTypeID,
            this.colRegistrationType});
            this.gvRegistrationtype.GridControl = this.gcRegistrationType;
            this.gvRegistrationtype.Name = "gvRegistrationtype";
            this.gvRegistrationtype.OptionsView.ShowGroupPanel = false;
            this.gvRegistrationtype.DoubleClick += new System.EventHandler(this.gvRegistrationtype_DoubleClick);
            this.gvRegistrationtype.RowCountChanged += new System.EventHandler(this.gvRegistrationtype_RowCountChanged);
            // 
            // colRegistrationTypeID
            // 
            resources.ApplyResources(this.colRegistrationTypeID, "colRegistrationTypeID");
            this.colRegistrationTypeID.FieldName = "REGISTRATION_TYPE_ID";
            this.colRegistrationTypeID.Name = "colRegistrationTypeID";
            // 
            // colRegistrationType
            // 
            this.colRegistrationType.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colRegistrationType.AppearanceHeader.Font")));
            this.colRegistrationType.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colRegistrationType, "colRegistrationType");
            this.colRegistrationType.FieldName = "REGISTRATION_TYPE";
            this.colRegistrationType.Name = "colRegistrationType";
            this.colRegistrationType.OptionsColumn.AllowEdit = false;
            this.colRegistrationType.OptionsColumn.AllowFocus = false;
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.emptySpaceItem2,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem1,
            this.layoutControlItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(808, 446);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(213, 423);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(557, 23);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.chkShowFilet;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 423);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(213, 23);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.lblCount;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(783, 423);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(11, 17);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(25, 23);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.lblNo;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(770, 423);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(13, 17);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(13, 23);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcRegistrationType;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 27);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(808, 396);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.ucRegistrationToolBar;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(0, 27);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(197, 27);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem5.Size = new System.Drawing.Size(808, 27);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // frmDonorViewRegistrationType
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmDonorViewRegistrationType";
            this.Activated += new System.EventHandler(this.frmDonorViewRegistrationType_Activated);
            this.Load += new System.EventHandler(this.frmDonorViewRegistrationType_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilet.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcRegistrationType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvRegistrationtype)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.CheckEdit chkShowFilet;
        private DevExpress.XtraGrid.GridControl gcRegistrationType;
        private DevExpress.XtraGrid.Views.Grid.GridView gvRegistrationtype;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.LabelControl lblNo;
        private DevExpress.XtraEditors.LabelControl lblCount;
        private DevExpress.XtraGrid.Columns.GridColumn colRegistrationTypeID;
        private DevExpress.XtraGrid.Columns.GridColumn colRegistrationType;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private Bosco.Utility.Controls.ucToolBar ucRegistrationToolBar;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
    }
}