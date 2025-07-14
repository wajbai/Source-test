namespace ACPP.Modules.Inventory
{
    partial class frmVendorInfoView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmVendorInfoView));
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.lblCount = new DevExpress.XtraEditors.LabelControl();
            this.lblRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.gcVendorInfo = new DevExpress.XtraGrid.GridControl();
            this.gvVendorInfo = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPanNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colGstNo = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colTelephoneNO = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colemailId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAddress = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ucVendorInfo = new Bosco.Utility.Controls.ucToolBar();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcVendorInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvVendorInfo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.lblCount);
            this.layoutControl1.Controls.Add(this.lblRecordCount);
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.gcVendorInfo);
            this.layoutControl1.Controls.Add(this.ucVendorInfo);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(439, 281, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // lblCount
            // 
            this.lblCount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblCount.Appearance.Font")));
            resources.ApplyResources(this.lblCount, "lblCount");
            this.lblCount.Name = "lblCount";
            this.lblCount.StyleController = this.layoutControl1;
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblRecordCount.Appearance.Font")));
            resources.ApplyResources(this.lblRecordCount, "lblRecordCount");
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.StyleController = this.layoutControl1;
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
            // gcVendorInfo
            // 
            resources.ApplyResources(this.gcVendorInfo, "gcVendorInfo");
            this.gcVendorInfo.MainView = this.gvVendorInfo;
            this.gcVendorInfo.Name = "gcVendorInfo";
            this.gcVendorInfo.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvVendorInfo});
            // 
            // gvVendorInfo
            // 
            this.gvVendorInfo.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvVendorInfo.Appearance.FocusedRow.Font")));
            this.gvVendorInfo.Appearance.FocusedRow.Options.UseFont = true;
            this.gvVendorInfo.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvVendorInfo.Appearance.HeaderPanel.Font")));
            this.gvVendorInfo.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvVendorInfo.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colName,
            this.colPanNo,
            this.colGstNo,
            this.colTelephoneNO,
            this.colemailId,
            this.colAddress});
            this.gvVendorInfo.GridControl = this.gcVendorInfo;
            this.gvVendorInfo.Name = "gvVendorInfo";
            this.gvVendorInfo.OptionsBehavior.Editable = false;
            this.gvVendorInfo.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvVendorInfo.OptionsView.ShowGroupPanel = false;
            this.gvVendorInfo.DoubleClick += new System.EventHandler(this.gvVendorInfo_DoubleClick);
            this.gvVendorInfo.RowCountChanged += new System.EventHandler(this.gvVendorInfo_RowCountChanged);
            // 
            // colId
            // 
            resources.ApplyResources(this.colId, "colId");
            this.colId.FieldName = "ID";
            this.colId.Name = "colId";
            // 
            // colName
            // 
            this.colName.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colName.AppearanceHeader.Font")));
            this.colName.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colName, "colName");
            this.colName.FieldName = "NAME";
            this.colName.Name = "colName";
            this.colName.OptionsColumn.AllowEdit = false;
            this.colName.OptionsColumn.AllowFocus = false;
            this.colName.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colPanNo
            // 
            resources.ApplyResources(this.colPanNo, "colPanNo");
            this.colPanNo.FieldName = "PAN_NO";
            this.colPanNo.Name = "colPanNo";
            this.colPanNo.OptionsColumn.AllowEdit = false;
            this.colPanNo.OptionsColumn.AllowFocus = false;
            this.colPanNo.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colGstNo
            // 
            resources.ApplyResources(this.colGstNo, "colGstNo");
            this.colGstNo.FieldName = "GST_NO";
            this.colGstNo.Name = "colGstNo";
            this.colGstNo.OptionsColumn.AllowEdit = false;
            this.colGstNo.OptionsColumn.AllowFocus = false;
            this.colGstNo.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colTelephoneNO
            // 
            this.colTelephoneNO.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colTelephoneNO.AppearanceHeader.Font")));
            this.colTelephoneNO.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colTelephoneNO, "colTelephoneNO");
            this.colTelephoneNO.FieldName = "CONTACT_NO";
            this.colTelephoneNO.Name = "colTelephoneNO";
            this.colTelephoneNO.OptionsColumn.AllowEdit = false;
            this.colTelephoneNO.OptionsColumn.AllowFocus = false;
            this.colTelephoneNO.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colemailId
            // 
            this.colemailId.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colemailId.AppearanceHeader.Font")));
            this.colemailId.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colemailId, "colemailId");
            this.colemailId.FieldName = "EMAIL_ID";
            this.colemailId.Name = "colemailId";
            this.colemailId.OptionsColumn.AllowEdit = false;
            this.colemailId.OptionsColumn.AllowFocus = false;
            this.colemailId.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colAddress
            // 
            this.colAddress.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colAddress.AppearanceHeader.Font")));
            this.colAddress.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colAddress, "colAddress");
            this.colAddress.FieldName = "ADDRESS";
            this.colAddress.Name = "colAddress";
            this.colAddress.OptionsColumn.AllowEdit = false;
            this.colAddress.OptionsColumn.AllowFocus = false;
            this.colAddress.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // ucVendorInfo
            // 
            this.ucVendorInfo.ChangeAddCaption = "&Add";
            this.ucVendorInfo.ChangeCaption = "&Edit";
            this.ucVendorInfo.ChangeDeleteCaption = "&Delete";
            this.ucVendorInfo.ChangeMoveVoucherCaption = "&Move Voucher";
            resources.ApplyResources(toolTipTitleItem1, "toolTipTitleItem1");
            toolTipItem1.LeftIndent = 6;
            resources.ApplyResources(toolTipItem1, "toolTipItem1");
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.ucVendorInfo.ChangeMoveVoucherTooltip = superToolTip1;
            this.ucVendorInfo.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucVendorInfo.ChangePostInterestCaption = "P&ost Interest";
            this.ucVendorInfo.ChangePrintCaption = "&Print";
            this.ucVendorInfo.ChnageRenewCaption = "Re<u>n</u>ew";
            this.ucVendorInfo.DisableAddButton = true;
            this.ucVendorInfo.DisableAMCRenew = true;
            this.ucVendorInfo.DisableCloseButton = true;
            this.ucVendorInfo.DisableDeleteButton = true;
            this.ucVendorInfo.DisableDownloadExcel = true;
            this.ucVendorInfo.DisableEditButton = true;
            this.ucVendorInfo.DisableMoveTransaction = true;
            this.ucVendorInfo.DisableNatureofPayments = true;
            this.ucVendorInfo.DisablePostInterest = true;
            this.ucVendorInfo.DisablePrintButton = true;
            this.ucVendorInfo.DisableRestoreVoucher = true;
            resources.ApplyResources(this.ucVendorInfo, "ucVendorInfo");
            this.ucVendorInfo.Name = "ucVendorInfo";
            this.ucVendorInfo.ShowHTML = true;
            this.ucVendorInfo.ShowMMT = true;
            this.ucVendorInfo.ShowPDF = true;
            this.ucVendorInfo.ShowRTF = true;
            this.ucVendorInfo.ShowText = true;
            this.ucVendorInfo.ShowXLS = true;
            this.ucVendorInfo.ShowXLSX = true;
            this.ucVendorInfo.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucVendorInfo.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucVendorInfo.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucVendorInfo.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucVendorInfo.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucVendorInfo.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucVendorInfo.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucVendorInfo.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucVendorInfo.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucVendorInfo.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucVendorInfo.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucVendorInfo.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucVendorInfo.VisibleRenew = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucVendorInfo.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucVendorInfo.AddClicked += new System.EventHandler(this.ucVendorInfo_AddClicked);
            this.ucVendorInfo.EditClicked += new System.EventHandler(this.ucVendorInfo_EditClicked);
            this.ucVendorInfo.DeleteClicked += new System.EventHandler(this.ucVendorInfo_DeleteClicked);
            this.ucVendorInfo.PrintClicked += new System.EventHandler(this.ucVendorInfo_PrintClicked);
            this.ucVendorInfo.CloseClicked += new System.EventHandler(this.ucVendorInfo_CloseClicked);
            this.ucVendorInfo.RefreshClicked += new System.EventHandler(this.ucVendorInfo_RefreshClicked);
            this.ucVendorInfo.DownloadExcel += new System.EventHandler(this.ucVendorInfo_DownloadExcel);
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
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(954, 471);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ucVendorInfo;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 29);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(196, 29);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem1.Size = new System.Drawing.Size(954, 29);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcVendorInfo;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 29);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(200, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem2.Size = new System.Drawing.Size(954, 419);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(120, 448);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(782, 23);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkShowFilter;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 448);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(120, 23);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.lblRecordCount;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(918, 448);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(11, 17);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(36, 23);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.lblCount;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(902, 448);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(13, 17);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(16, 23);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // frmVendorInfoView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmVendorInfoView";
            this.ShowFilterClicked += new System.EventHandler(this.frmVendorInfoView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmVendorInfoView_EnterClicked);
            this.Activated += new System.EventHandler(this.frmVendorInfoView_Activated);
            this.Load += new System.EventHandler(this.frmVendorInfoView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcVendorInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvVendorInfo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gcVendorInfo;
        private DevExpress.XtraGrid.Views.Grid.GridView gvVendorInfo;
        private Bosco.Utility.Controls.ucToolBar ucVendorInfo;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private DevExpress.XtraGrid.Columns.GridColumn colAddress;
        private DevExpress.XtraGrid.Columns.GridColumn colPanNo;
        private DevExpress.XtraGrid.Columns.GridColumn colTelephoneNO;
        private DevExpress.XtraGrid.Columns.GridColumn colemailId;
        private DevExpress.XtraEditors.LabelControl lblCount;
        private DevExpress.XtraEditors.LabelControl lblRecordCount;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraGrid.Columns.GridColumn colGstNo;
    }
}