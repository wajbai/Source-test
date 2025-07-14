﻿namespace ACPP.Modules.Inventory.Asset
{
    partial class frmMaintenanceServiceView
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
            this.gcMaintenanceorServiceView = new DevExpress.XtraGrid.GridControl();
            this.gvMaintenanceorService = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ucMaintananceOrServive = new Bosco.Utility.Controls.ucToolBar();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblCount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblCountNumber = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMaintenanceorServiceView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMaintenanceorService)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCountNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.gcMaintenanceorServiceView);
            this.layoutControl1.Controls.Add(this.ucMaintananceOrServive);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(350, 185, 240, 295);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(935, 485);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // chkShowFilter
            // 
            this.chkShowFilter.Location = new System.Drawing.Point(2, 464);
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = "Show <b>F</b>ilter";
            this.chkShowFilter.Size = new System.Drawing.Size(75, 19);
            this.chkShowFilter.StyleController = this.layoutControl1;
            this.chkShowFilter.TabIndex = 6;
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // gcMaintenanceorServiceView
            // 
            this.gcMaintenanceorServiceView.Location = new System.Drawing.Point(0, 31);
            this.gcMaintenanceorServiceView.MainView = this.gvMaintenanceorService;
            this.gcMaintenanceorServiceView.Name = "gcMaintenanceorServiceView";
            this.gcMaintenanceorServiceView.Size = new System.Drawing.Size(935, 431);
            this.gcMaintenanceorServiceView.TabIndex = 5;
            this.gcMaintenanceorServiceView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMaintenanceorService});
            // 
            // gvMaintenanceorService
            // 
            this.gvMaintenanceorService.Appearance.FocusedRow.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvMaintenanceorService.Appearance.FocusedRow.Options.UseFont = true;
            this.gvMaintenanceorService.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvMaintenanceorService.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvMaintenanceorService.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colName,
            this.colDescription});
            this.gvMaintenanceorService.GridControl = this.gcMaintenanceorServiceView;
            this.gvMaintenanceorService.Name = "gvMaintenanceorService";
            this.gvMaintenanceorService.OptionsBehavior.Editable = false;
            this.gvMaintenanceorService.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvMaintenanceorService.OptionsView.ShowGroupPanel = false;
            this.gvMaintenanceorService.DoubleClick += new System.EventHandler(this.gvMaintenanceorService_DoubleClick);
            this.gvMaintenanceorService.RowCountChanged += new System.EventHandler(this.gvMaintenanceorService_RowCountChanged);
            // 
            // colId
            // 
            this.colId.Caption = "Id";
            this.colId.FieldName = "SERVICE_ID";
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
            this.colName.Width = 189;
            // 
            // colDescription
            // 
            this.colDescription.AppearanceCell.Font = new System.Drawing.Font("Arial", 8.25F);
            this.colDescription.AppearanceCell.Options.UseFont = true;
            this.colDescription.AppearanceHeader.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.colDescription.AppearanceHeader.Options.UseFont = true;
            this.colDescription.Caption = "Description";
            this.colDescription.FieldName = "DESCRIPTION";
            this.colDescription.Name = "colDescription";
            this.colDescription.OptionsColumn.AllowEdit = false;
            this.colDescription.OptionsColumn.AllowGroup = DevExpress.Utils.DefaultBoolean.False;
            this.colDescription.Visible = true;
            this.colDescription.VisibleIndex = 1;
            this.colDescription.Width = 398;
            // 
            // ucMaintananceOrServive
            // 
            this.ucMaintananceOrServive.ChangeAddCaption = "&Add";
            this.ucMaintananceOrServive.ChangeCaption = "&Edit";
            this.ucMaintananceOrServive.ChangeDeleteCaption = "&Delete";
            this.ucMaintananceOrServive.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucMaintananceOrServive.ChangePrintCaption = "&Print";
            this.ucMaintananceOrServive.DisableAddButton = true;
            this.ucMaintananceOrServive.DisableCloseButton = true;
            this.ucMaintananceOrServive.DisableDeleteButton = true;
            this.ucMaintananceOrServive.DisableDownloadExcel = true;
            this.ucMaintananceOrServive.DisableEditButton = true;
            this.ucMaintananceOrServive.DisableMoveTransaction = true;
            this.ucMaintananceOrServive.DisableNatureofPayments = true;
            this.ucMaintananceOrServive.DisablePrintButton = true;
            this.ucMaintananceOrServive.DisableRestoreVoucher = true;
            this.ucMaintananceOrServive.Location = new System.Drawing.Point(0, 0);
            this.ucMaintananceOrServive.Name = "ucMaintananceOrServive";
            this.ucMaintananceOrServive.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.ucMaintananceOrServive.ShowHTML = true;
            this.ucMaintananceOrServive.ShowMMT = true;
            this.ucMaintananceOrServive.ShowPDF = true;
            this.ucMaintananceOrServive.ShowRTF = true;
            this.ucMaintananceOrServive.ShowText = true;
            this.ucMaintananceOrServive.ShowXLS = true;
            this.ucMaintananceOrServive.ShowXLSX = true;
            this.ucMaintananceOrServive.Size = new System.Drawing.Size(935, 31);
            this.ucMaintananceOrServive.TabIndex = 4;
            this.ucMaintananceOrServive.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucMaintananceOrServive.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucMaintananceOrServive.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucMaintananceOrServive.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucMaintananceOrServive.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucMaintananceOrServive.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucMaintananceOrServive.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucMaintananceOrServive.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucMaintananceOrServive.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucMaintananceOrServive.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucMaintananceOrServive.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucMaintananceOrServive.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucMaintananceOrServive.AddClicked += new System.EventHandler(this.ucMaintananceOrServive_AddClicked);
            this.ucMaintananceOrServive.EditClicked += new System.EventHandler(this.ucMaintananceOrServive_EditClicked);
            this.ucMaintananceOrServive.DeleteClicked += new System.EventHandler(this.ucMaintananceOrServive_DeleteClicked);
            this.ucMaintananceOrServive.PrintClicked += new System.EventHandler(this.ucMaintananceOrServive_PrintClicked);
            this.ucMaintananceOrServive.CloseClicked += new System.EventHandler(this.ucMaintananceOrServive_CloseClicked);
            this.ucMaintananceOrServive.RefreshClicked += new System.EventHandler(this.ucMaintananceOrServive_RefreshClicked);
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
            this.emptySpaceItem1,
            this.lblCount,
            this.lblCountNumber});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(935, 485);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(79, 462);
            this.emptySpaceItem2.MinSize = new System.Drawing.Size(100, 20);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.emptySpaceItem2.Size = new System.Drawing.Size(762, 23);
            this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ucMaintananceOrServive;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 31);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(100, 31);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem1.Size = new System.Drawing.Size(935, 31);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcMaintenanceorServiceView;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 31);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(100, 20);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem2.Size = new System.Drawing.Size(935, 431);
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
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 462);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(79, 23);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(79, 23);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(79, 23);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(895, 462);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(40, 23);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(40, 23);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(40, 23);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblCount
            // 
            this.lblCount.AllowHotTrack = false;
            this.lblCount.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblCount.AppearanceItemCaption.Options.UseFont = true;
            this.lblCount.CustomizationFormText = "#";
            this.lblCount.Location = new System.Drawing.Point(841, 462);
            this.lblCount.MaxSize = new System.Drawing.Size(13, 23);
            this.lblCount.MinSize = new System.Drawing.Size(13, 23);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(13, 23);
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
            this.lblCountNumber.Location = new System.Drawing.Point(854, 462);
            this.lblCountNumber.MinSize = new System.Drawing.Size(13, 17);
            this.lblCountNumber.Name = "lblCountNumber";
            this.lblCountNumber.Size = new System.Drawing.Size(41, 23);
            this.lblCountNumber.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblCountNumber.Text = "0";
            this.lblCountNumber.TextSize = new System.Drawing.Size(9, 13);
            // 
            // frmMaintenanceServiceView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(935, 485);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmMaintenanceServiceView";
            this.Text = "Maintenance/Service";
            this.ShowFilterClicked += new System.EventHandler(this.frmMaintenanceServiceView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmMaintenanceServiceView_EnterClicked);
            this.Load += new System.EventHandler(this.frmMaintananceServiceView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcMaintenanceorServiceView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMaintenanceorService)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCountNumber)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraGrid.GridControl gcMaintenanceorServiceView;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMaintenanceorService;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private Bosco.Utility.Controls.ucToolBar ucMaintananceOrServive;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.SimpleLabelItem lblCount;
        private DevExpress.XtraLayout.SimpleLabelItem lblCountNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription;
    }
}