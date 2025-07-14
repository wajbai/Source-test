namespace ACPP.Modules.Master
{
    partial class frmAuditTypeView
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
            this.lblBankRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.ucToolBar1 = new Bosco.Utility.Controls.ucToolBar();
            this.gcAudit = new DevExpress.XtraGrid.GridControl();
            this.gvAudit = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colAuditTypeId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAuditType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblAuditRowCount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem2 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAudit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAudit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAuditRowCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.lblBankRecordCount);
            this.layoutControl1.Controls.Add(this.ucToolBar1);
            this.layoutControl1.Controls.Add(this.gcAudit);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(561, 237, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(882, 424);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // chkShowFilter
            // 
            this.chkShowFilter.Location = new System.Drawing.Point(2, 403);
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = "Show <b>F</b>ilter";
            this.chkShowFilter.Size = new System.Drawing.Size(793, 19);
            this.chkShowFilter.StyleController = this.layoutControl1;
            this.chkShowFilter.TabIndex = 6;
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // lblBankRecordCount
            // 
            this.lblBankRecordCount.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.lblBankRecordCount.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblBankRecordCount.Location = new System.Drawing.Point(833, 41);
            this.lblBankRecordCount.Name = "lblBankRecordCount";
            this.lblBankRecordCount.Size = new System.Drawing.Size(0, 13);
            this.lblBankRecordCount.StyleController = this.layoutControl1;
            this.lblBankRecordCount.TabIndex = 3;
            // 
            // ucToolBar1
            // 
            this.ucToolBar1.ChangeAddCaption = "&Add";
            this.ucToolBar1.ChangeCaption = "&Edit";
            this.ucToolBar1.ChangeDeleteCaption = "&Delete";
            this.ucToolBar1.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucToolBar1.ChangePrintCaption = "&Print";
            this.ucToolBar1.DisableAddButton = true;
            this.ucToolBar1.DisableCloseButton = true;
            this.ucToolBar1.DisableDeleteButton = true;
            this.ucToolBar1.DisableDownloadExcel = true;
            this.ucToolBar1.DisableEditButton = true;
            this.ucToolBar1.DisableMoveTransaction = true;
            this.ucToolBar1.DisableNatureofPayments = true;
            this.ucToolBar1.DisablePrintButton = true;
            this.ucToolBar1.DisableRestoreVoucher = true;
            this.ucToolBar1.Location = new System.Drawing.Point(0, 0);
            this.ucToolBar1.Name = "ucToolBar1";
            this.ucToolBar1.Padding = new System.Windows.Forms.Padding(0, 1, 0, 1);
            this.ucToolBar1.ShowHTML = true;
            this.ucToolBar1.ShowMMT = true;
            this.ucToolBar1.ShowPDF = true;
            this.ucToolBar1.ShowRTF = true;
            this.ucToolBar1.ShowText = true;
            this.ucToolBar1.ShowXLS = true;
            this.ucToolBar1.ShowXLSX = true;
            this.ucToolBar1.Size = new System.Drawing.Size(882, 30);
            this.ucToolBar1.TabIndex = 4;
            this.ucToolBar1.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar1.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar1.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar1.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar1.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar1.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar1.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar1.AddClicked += new System.EventHandler(this.ucToolBar1_AddClicked);
            this.ucToolBar1.EditClicked += new System.EventHandler(this.ucToolBar1_EditClicked);
            this.ucToolBar1.DeleteClicked += new System.EventHandler(this.ucToolBar1_DeleteClicked);
            this.ucToolBar1.PrintClicked += new System.EventHandler(this.ucToolBar1_PrintClicked);
            this.ucToolBar1.CloseClicked += new System.EventHandler(this.ucToolBar1_CloseClicked);
            this.ucToolBar1.RefreshClicked += new System.EventHandler(this.ucToolBar1_RefreshClicked);
            // 
            // gcAudit
            // 
            this.gcAudit.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.gcAudit.Location = new System.Drawing.Point(0, 30);
            this.gcAudit.MainView = this.gvAudit;
            this.gcAudit.Margin = new System.Windows.Forms.Padding(0);
            this.gcAudit.Name = "gcAudit";
            this.gcAudit.Size = new System.Drawing.Size(882, 371);
            this.gcAudit.TabIndex = 5;
            this.gcAudit.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvAudit});
            // 
            // gvAudit
            // 
            this.gvAudit.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvAudit.Appearance.FocusedRow.Options.UseFont = true;
            this.gvAudit.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvAudit.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvAudit.AppearancePrint.HeaderPanel.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvAudit.AppearancePrint.HeaderPanel.Options.UseFont = true;
            this.gvAudit.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAuditTypeId,
            this.colAuditType});
            this.gvAudit.GridControl = this.gcAudit;
            this.gvAudit.Name = "gvAudit";
            this.gvAudit.OptionsBehavior.Editable = false;
            this.gvAudit.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvAudit.OptionsView.ShowGroupPanel = false;
            this.gvAudit.DoubleClick += new System.EventHandler(this.gvAudit_DoubleClick);
            this.gvAudit.RowCountChanged += new System.EventHandler(this.gvAudit_RowCountChanged);
            // 
            // colAuditTypeId
            // 
            this.colAuditTypeId.AppearanceCell.Options.UseFont = true;
            this.colAuditTypeId.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colAuditTypeId.AppearanceHeader.Options.UseFont = true;
            this.colAuditTypeId.Caption = "Audit TypeId";
            this.colAuditTypeId.FieldName = "AUDIT_TYPE_ID";
            this.colAuditTypeId.Name = "colAuditTypeId";
            // 
            // colAuditType
            // 
            this.colAuditType.AppearanceCell.Options.UseFont = true;
            this.colAuditType.AppearanceHeader.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.colAuditType.AppearanceHeader.Options.UseFont = true;
            this.colAuditType.Caption = "Audit Type";
            this.colAuditType.FieldName = "AUDIT_TYPE";
            this.colAuditType.Name = "colAuditType";
            this.colAuditType.Visible = true;
            this.colAuditType.VisibleIndex = 0;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.lblAuditRowCount,
            this.simpleLabelItem2,
            this.emptySpaceItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(882, 424);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ucToolBar1;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 30);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(104, 30);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem1.Size = new System.Drawing.Size(882, 30);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcAudit;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 30);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem2.Size = new System.Drawing.Size(882, 371);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkShowFilter;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 401);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(797, 23);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // lblAuditRowCount
            // 
            this.lblAuditRowCount.AllowHotTrack = false;
            this.lblAuditRowCount.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblAuditRowCount.AppearanceItemCaption.Options.UseFont = true;
            this.lblAuditRowCount.CustomizationFormText = "0";
            this.lblAuditRowCount.Location = new System.Drawing.Point(817, 401);
            this.lblAuditRowCount.MinSize = new System.Drawing.Size(13, 17);
            this.lblAuditRowCount.Name = "lblAuditRowCount";
            this.lblAuditRowCount.Size = new System.Drawing.Size(18, 23);
            this.lblAuditRowCount.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblAuditRowCount.Text = "0";
            this.lblAuditRowCount.TextSize = new System.Drawing.Size(9, 13);
            // 
            // simpleLabelItem2
            // 
            this.simpleLabelItem2.AllowHotTrack = false;
            this.simpleLabelItem2.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.simpleLabelItem2.AppearanceItemCaption.Options.UseFont = true;
            this.simpleLabelItem2.CustomizationFormText = "#";
            this.simpleLabelItem2.Location = new System.Drawing.Point(797, 401);
            this.simpleLabelItem2.MinSize = new System.Drawing.Size(13, 17);
            this.simpleLabelItem2.Name = "simpleLabelItem2";
            this.simpleLabelItem2.Size = new System.Drawing.Size(20, 23);
            this.simpleLabelItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.simpleLabelItem2.Text = "#";
            this.simpleLabelItem2.TextSize = new System.Drawing.Size(9, 13);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(835, 401);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(47, 23);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmAuditTypeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(882, 424);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmAuditTypeView";
            this.Text = "Audit Type ";
            this.ShowFilterClicked += new System.EventHandler(this.frmAuditTypeView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmAuditTypeView_EnterClicked);
            this.Activated += new System.EventHandler(this.frmAuditTypeView_Activated);
            this.Load += new System.EventHandler(this.frmAuditTypeView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            this.layoutControl1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcAudit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvAudit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAuditRowCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gcAudit;
        private DevExpress.XtraGrid.Views.Grid.GridView gvAudit;
        private DevExpress.XtraGrid.Columns.GridColumn colAuditTypeId;
        private DevExpress.XtraGrid.Columns.GridColumn colAuditType;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.LabelControl lblBankRecordCount;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem2;
        private Bosco.Utility.Controls.ucToolBar ucToolBar1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.SimpleLabelItem lblAuditRowCount;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
    }
}