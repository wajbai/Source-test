namespace ACPP.Modules.Inventory.Asset
{
    partial class frmInsuranceTypeView
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
            this.gcInsuranceTypeView = new DevExpress.XtraGrid.GridControl();
            this.gvInsuranceTypeView = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCompany = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProduct = new DevExpress.XtraGrid.Columns.GridColumn();
            this.ucInsuranceType = new ACPP.Modules.UIControls.ucToolBar();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblCount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblCountNumber = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcInsuranceTypeView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInsuranceTypeView)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCountNumber)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.gcInsuranceTypeView);
            this.layoutControl1.Controls.Add(this.ucInsuranceType);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(407, 267, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(863, 423);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // chkShowFilter
            // 
            this.chkShowFilter.Location = new System.Drawing.Point(0, 399);
            this.chkShowFilter.Name = "chkShowFilter";
            this.chkShowFilter.Properties.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            this.chkShowFilter.Properties.Caption = "Show <b>F</b>ilter";
            this.chkShowFilter.Size = new System.Drawing.Size(76, 19);
            this.chkShowFilter.StyleController = this.layoutControl1;
            this.chkShowFilter.TabIndex = 6;
            this.chkShowFilter.CheckedChanged += new System.EventHandler(this.chkShowFilter_CheckedChanged);
            // 
            // gcInsuranceTypeView
            // 
            this.gcInsuranceTypeView.Location = new System.Drawing.Point(0, 30);
            this.gcInsuranceTypeView.MainView = this.gvInsuranceTypeView;
            this.gcInsuranceTypeView.Name = "gcInsuranceTypeView";
            this.gcInsuranceTypeView.Size = new System.Drawing.Size(863, 369);
            this.gcInsuranceTypeView.TabIndex = 5;
            this.gcInsuranceTypeView.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvInsuranceTypeView});
            this.gcInsuranceTypeView.DoubleClick += new System.EventHandler(this.gcInsuranceTypeView_DoubleClick);
            // 
            // gvInsuranceTypeView
            // 
            this.gvInsuranceTypeView.Appearance.FocusedRow.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvInsuranceTypeView.Appearance.FocusedRow.Options.UseFont = true;
            this.gvInsuranceTypeView.Appearance.HeaderPanel.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Bold);
            this.gvInsuranceTypeView.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvInsuranceTypeView.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colId,
            this.colName,
            this.colDescription,
            this.colCompany,
            this.colProduct});
            this.gvInsuranceTypeView.GridControl = this.gcInsuranceTypeView;
            this.gvInsuranceTypeView.Name = "gvInsuranceTypeView";
            this.gvInsuranceTypeView.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvInsuranceTypeView.OptionsView.ShowGroupPanel = false;
            this.gvInsuranceTypeView.RowCountChanged += new System.EventHandler(this.gvInsuranceTypeView_RowCountChanged);
            // 
            // colId
            // 
            this.colId.Caption = "Id";
            this.colId.FieldName = "INSURANCE_TYPE_ID";
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
            // 
            // colCompany
            // 
            this.colCompany.Caption = "Company";
            this.colCompany.FieldName = "COMPANY";
            this.colCompany.Name = "colCompany";
            this.colCompany.OptionsColumn.AllowEdit = false;
            this.colCompany.OptionsColumn.AllowFocus = false;
            this.colCompany.Visible = true;
            this.colCompany.VisibleIndex = 1;
            // 
            // colProduct
            // 
            this.colProduct.Caption = "Product";
            this.colProduct.FieldName = "PRODUCT";
            this.colProduct.Name = "colProduct";
            this.colProduct.OptionsColumn.AllowEdit = false;
            this.colProduct.OptionsColumn.AllowFocus = false;
            this.colProduct.Visible = true;
            this.colProduct.VisibleIndex = 2;
            // 
            // ucInsuranceType
            // 
            this.ucInsuranceType.ChangeAddCaption = "&Add";
            this.ucInsuranceType.ChangeCaption = "&Edit";
            this.ucInsuranceType.ChangeDeleteCaption = "&Delete";
            this.ucInsuranceType.ChangeNatureOfPaymentCaption = "&Nature of Payments";
            this.ucInsuranceType.ChangePostInterestCaption = "P&ost Interest";
            this.ucInsuranceType.ChangePrintCaption = "&Print";
            this.ucInsuranceType.DisableAddButton = true;
            this.ucInsuranceType.DisableCloseButton = true;
            this.ucInsuranceType.DisableDeleteButton = true;
            this.ucInsuranceType.DisableDownloadExcel = true;
            this.ucInsuranceType.DisableEditButton = true;
            this.ucInsuranceType.DisableMoveTransaction = true;
            this.ucInsuranceType.DisableNatureofPayments = true;
            this.ucInsuranceType.DisablePostInterest = true;
            this.ucInsuranceType.DisablePrintButton = true;
            this.ucInsuranceType.DisableRestoreVoucher = true;
            this.ucInsuranceType.Location = new System.Drawing.Point(0, 0);
            this.ucInsuranceType.Name = "ucInsuranceType";
            this.ucInsuranceType.Padding = new System.Windows.Forms.Padding(0, 2, 0, 0);
            this.ucInsuranceType.ShowHTML = true;
            this.ucInsuranceType.ShowMMT = true;
            this.ucInsuranceType.ShowPDF = true;
            this.ucInsuranceType.ShowRTF = true;
            this.ucInsuranceType.ShowText = true;
            this.ucInsuranceType.ShowXLS = true;
            this.ucInsuranceType.ShowXLSX = true;
            this.ucInsuranceType.Size = new System.Drawing.Size(863, 30);
            this.ucInsuranceType.TabIndex = 4;
            this.ucInsuranceType.VisbleInsertVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucInsuranceType.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucInsuranceType.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucInsuranceType.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucInsuranceType.VisibleDownloadExcel = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucInsuranceType.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucInsuranceType.VisibleMoveTrans = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucInsuranceType.VisibleNatureofPayments = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucInsuranceType.VisibleNegativeBalance = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucInsuranceType.VisiblePostInterest = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucInsuranceType.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucInsuranceType.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucInsuranceType.VisibleRestoreVoucher = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucInsuranceType.AddClicked += new System.EventHandler(this.ucInsuranceType_AddClicked);
            this.ucInsuranceType.EditClicked += new System.EventHandler(this.ucInsuranceType_EditClicked);
            this.ucInsuranceType.DeleteClicked += new System.EventHandler(this.ucInsuranceType_DeleteClicked);
            this.ucInsuranceType.PrintClicked += new System.EventHandler(this.ucInsuranceType_PrintClicked);
            this.ucInsuranceType.CloseClicked += new System.EventHandler(this.ucInsuranceType_CloseClicked);
            this.ucInsuranceType.RefreshClicked += new System.EventHandler(this.ucInsuranceType_RefreshClicked);
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
            this.lblCount,
            this.lblCountNumber});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(863, 423);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(76, 399);
            this.emptySpaceItem2.MinSize = new System.Drawing.Size(104, 24);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(747, 24);
            this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ucInsuranceType;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 30);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(196, 30);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem1.Size = new System.Drawing.Size(863, 30);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcInsuranceTypeView;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 30);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(100, 20);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem2.Size = new System.Drawing.Size(863, 369);
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
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 399);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(75, 19);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem3.Size = new System.Drawing.Size(76, 24);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // lblCount
            // 
            this.lblCount.AllowHotTrack = false;
            this.lblCount.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblCount.AppearanceItemCaption.Options.UseFont = true;
            this.lblCount.CustomizationFormText = "#";
            this.lblCount.Location = new System.Drawing.Point(823, 399);
            this.lblCount.Name = "lblCount";
            this.lblCount.Size = new System.Drawing.Size(13, 24);
            this.lblCount.Text = "#";
            this.lblCount.TextSize = new System.Drawing.Size(9, 13);
            // 
            // lblCountNumber
            // 
            this.lblCountNumber.AllowHotTrack = false;
            this.lblCountNumber.AppearanceItemCaption.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Bold);
            this.lblCountNumber.AppearanceItemCaption.Options.UseFont = true;
            this.lblCountNumber.CustomizationFormText = "0";
            this.lblCountNumber.Location = new System.Drawing.Point(836, 399);
            this.lblCountNumber.Name = "lblCountNumber";
            this.lblCountNumber.Size = new System.Drawing.Size(27, 24);
            this.lblCountNumber.Text = "0";
            this.lblCountNumber.TextSize = new System.Drawing.Size(9, 13);
            // 
            // frmInsuranceTypeView
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(863, 423);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "frmInsuranceTypeView";
            this.Text = "Insurance Plans";
            this.ShowFilterClicked += new System.EventHandler(this.frmInsuranceTypeView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmInsuranceTypeView_EnterClicked);
            this.Load += new System.EventHandler(this.frmInsuranceTypeView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcInsuranceTypeView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvInsuranceTypeView)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCountNumber)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraGrid.GridControl gcInsuranceTypeView;
        private DevExpress.XtraGrid.Views.Grid.GridView gvInsuranceTypeView;
        private DevExpress.XtraGrid.Columns.GridColumn colId;
        private DevExpress.XtraGrid.Columns.GridColumn colName;
        private UIControls.ucToolBar ucInsuranceType;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.SimpleLabelItem lblCount;
        private DevExpress.XtraLayout.SimpleLabelItem lblCountNumber;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colCompany;
        private DevExpress.XtraGrid.Columns.GridColumn colProduct;
    }
}