namespace ACPP.Modules.TDS
{
    partial class frmDutyTaxView
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
            this.ucDutyTax = new Bosco.Utility.Controls.ucToolBar();
            this.lblRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.labelControl1 = new DevExpress.XtraEditors.LabelControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.gcDutyTax = new DevExpress.XtraGrid.GridControl();
            this.gvTax = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.gcolDutyTax = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolDutyTaxId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolTaxRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcolExcemptionLimit = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colStatus = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDutyTax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTax)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ucDutyTax);
            this.layoutControl1.Controls.Add(this.lblRecordCount);
            this.layoutControl1.Controls.Add(this.labelControl1);
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.gcDutyTax);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(347, 182, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(852, 387);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // ucDutyTax
            // 
            this.ucDutyTax.ChangeAddCaption = "&Add";
            this.ucDutyTax.ChangeCaption = "&Edit";
            this.ucDutyTax.ChangeDeleteCaption = "&Delete";
            this.ucDutyTax.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucDutyTax.ChangePrintCaption = "&Print";
            this.ucDutyTax.DisableAddButton = false;
            this.ucDutyTax.DisableCloseButton = true;
            this.ucDutyTax.DisableDeleteButton = false;
            this.ucDutyTax.DisableDownloadExcel = true;
            this.ucDutyTax.DisableEditButton = true;
            this.ucDutyTax.DisableMoveTransaction = true;
            this.ucDutyTax.DisableNatureofPayments = true;
            this.ucDutyTax.DisablePrintButton = true;
            this.ucDutyTax.DisableRestoreVoucher = true;
            this.ucDutyTax.Location = new System.Drawing.Point(2, 2);
            this.ucDutyTax.Name = "ucDutyTax";
            this.ucDutyTax.Padding = new System.Windows.Forms.Padding(0, 5, 0, 0);
            this.ucDutyTax.ShowHTML = true;
            this.ucDutyTax.ShowMMT = true;
            this.ucDutyTax.ShowPDF = true;
            this.ucDutyTax.ShowRTF = true;
            this.ucDutyTax.ShowText = true;
            this.ucDutyTax.ShowXLS = true;
            this.ucDutyTax.ShowXLSX = true;
            this.ucDutyTax.Size = new System.Drawing.Size(848, 31);
            this.ucDutyTax.TabIndex = 9;
            this.ucDutyTax.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucDutyTax.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucDutyTax.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucDutyTax.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucDutyTax.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucDutyTax.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucDutyTax.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucDutyTax.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucDutyTax.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucDutyTax.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucDutyTax.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucDutyTax.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucDutyTax.AddClicked += new System.EventHandler(this.ucDutyTax_AddClicked);
            this.ucDutyTax.EditClicked += new System.EventHandler(this.ucDutyTax_EditClicked);
            this.ucDutyTax.DeleteClicked += new System.EventHandler(this.ucDutyTax_DeleteClicked);
            this.ucDutyTax.PrintClicked += new System.EventHandler(this.ucDutyTax_PrintClicked);
            this.ucDutyTax.CloseClicked += new System.EventHandler(this.ucDutyTax_CloseClicked);
            this.ucDutyTax.RefreshClicked += new System.EventHandler(this.ucDutyTax_RefreshClicked);
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblRecordCount.Location = new System.Drawing.Point(815, 365);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(35, 20);
            this.lblRecordCount.StyleController = this.layoutControl1;
            this.lblRecordCount.TabIndex = 7;
            this.lblRecordCount.Text = "0";
            // 
            // labelControl1
            // 
            this.labelControl1.Appearance.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.labelControl1.Location = new System.Drawing.Point(797, 365);
            this.labelControl1.Name = "labelControl1";
            this.labelControl1.Size = new System.Drawing.Size(14, 20);
            this.labelControl1.StyleController = this.layoutControl1;
            this.labelControl1.TabIndex = 6;
            this.labelControl1.Text = "#";
            // 
            // chkShowFilter
            // 
            this.chkShowFilter.AllowHtmlTextInToolTip = DevExpress.Utils.DefaultBoolean.False;
            this.chkShowFilter.Location = new System.Drawing.Point(2, 365);
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = "Show <b>F</b>ilter";
            this.chkShowFilter.Size = new System.Drawing.Size(105, 19);
            this.chkShowFilter.StyleController = this.layoutControl1;
            this.chkShowFilter.TabIndex = 5;
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // gcDutyTax
            // 
            this.gcDutyTax.Location = new System.Drawing.Point(2, 37);
            this.gcDutyTax.MainView = this.gvTax;
            this.gcDutyTax.Name = "gcDutyTax";
            this.gcDutyTax.Size = new System.Drawing.Size(848, 324);
            this.gcDutyTax.TabIndex = 4;
            this.gcDutyTax.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvTax});
            // 
            // gvTax
            // 
            this.gvTax.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvTax.Appearance.FocusedRow.Options.UseFont = true;
            this.gvTax.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvTax.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvTax.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.gcolDutyTax,
            this.gcolDutyTaxId,
            this.gcolTaxRate,
            this.gcolExcemptionLimit,
            this.colStatus});
            this.gvTax.GridControl = this.gcDutyTax;
            this.gvTax.Name = "gvTax";
            this.gvTax.OptionsBehavior.Editable = false;
            this.gvTax.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvTax.OptionsView.ShowGroupPanel = false;
            this.gvTax.DoubleClick += new System.EventHandler(this.gvTax_DoubleClick);
            this.gvTax.RowCountChanged += new System.EventHandler(this.gvTax_RowCountChanged);
            // 
            // gcolDutyTax
            // 
            this.gcolDutyTax.Caption = "Name";
            this.gcolDutyTax.FieldName = "TAX_TYPE_NAME";
            this.gcolDutyTax.Name = "gcolDutyTax";
            this.gcolDutyTax.OptionsColumn.AllowEdit = false;
            this.gcolDutyTax.OptionsColumn.AllowFocus = false;
            this.gcolDutyTax.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            this.gcolDutyTax.Visible = true;
            this.gcolDutyTax.VisibleIndex = 0;
            this.gcolDutyTax.Width = 714;
            // 
            // gcolDutyTaxId
            // 
            this.gcolDutyTaxId.Caption = "DutyTaxId";
            this.gcolDutyTaxId.FieldName = "TDS_DUTY_TAXTYPE_ID";
            this.gcolDutyTaxId.Name = "gcolDutyTaxId";
            this.gcolDutyTaxId.OptionsColumn.AllowEdit = false;
            this.gcolDutyTaxId.OptionsColumn.AllowFocus = false;
            // 
            // gcolTaxRate
            // 
            this.gcolTaxRate.Caption = "Tax Rate";
            this.gcolTaxRate.FieldName = "TDS_RATE";
            this.gcolTaxRate.Name = "gcolTaxRate";
            this.gcolTaxRate.OptionsColumn.AllowEdit = false;
            this.gcolTaxRate.OptionsColumn.AllowFocus = false;
            this.gcolTaxRate.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // gcolExcemptionLimit
            // 
            this.gcolExcemptionLimit.Caption = "Excemption Limit";
            this.gcolExcemptionLimit.FieldName = "TDS_EXEMPTION_LIMIT";
            this.gcolExcemptionLimit.Name = "gcolExcemptionLimit";
            this.gcolExcemptionLimit.OptionsColumn.AllowEdit = false;
            this.gcolExcemptionLimit.OptionsColumn.AllowFocus = false;
            this.gcolExcemptionLimit.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colStatus
            // 
            this.colStatus.Caption = "Status";
            this.colStatus.FieldName = "STATUS";
            this.colStatus.Name = "colStatus";
            this.colStatus.Visible = true;
            this.colStatus.VisibleIndex = 1;
            this.colStatus.Width = 116;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(852, 387);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcDutyTax;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 35);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(104, 24);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(852, 328);
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
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 363);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(109, 24);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(109, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(109, 24);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(109, 363);
            this.emptySpaceItem2.MinSize = new System.Drawing.Size(104, 24);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(686, 24);
            this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.labelControl1;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(795, 363);
            this.layoutControlItem3.MaxSize = new System.Drawing.Size(18, 24);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(18, 24);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(18, 24);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.lblRecordCount;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(813, 363);
            this.layoutControlItem4.MaxSize = new System.Drawing.Size(39, 24);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(39, 24);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(39, 24);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.ucDutyTax;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(0, 35);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(104, 35);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(852, 35);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // frmDutyTaxView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(852, 387);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmDutyTaxView";
            this.ShowIcon = false;
            this.Text = "Duty Tax";
            this.ShowFilterClicked += new System.EventHandler(this.frmDutyTaxView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmDutyTaxView_EnterClicked);
            this.Activated += new System.EventHandler(this.frmDutyTaxView_Activated);
            this.Load += new System.EventHandler(this.frmDutyTaxView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDutyTax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvTax)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraGrid.GridControl gcDutyTax;
        private DevExpress.XtraGrid.Views.Grid.GridView gvTax;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.LabelControl lblRecordCount;
        private DevExpress.XtraEditors.LabelControl labelControl1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraGrid.Columns.GridColumn gcolDutyTax;
        private DevExpress.XtraGrid.Columns.GridColumn gcolDutyTaxId;
        private DevExpress.XtraGrid.Columns.GridColumn gcolTaxRate;
        private DevExpress.XtraGrid.Columns.GridColumn gcolExcemptionLimit;
        private Bosco.Utility.Controls.ucToolBar ucDutyTax;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraGrid.Columns.GridColumn colStatus;
    }
}