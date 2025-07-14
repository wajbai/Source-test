namespace PAYROLL.Modules.Payroll_app
{
    partial class frmComponentView
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmComponentView));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.ucToolBar2 = new PAYROLL.UserControl.UcToolBar();
            this.lblRecord = new DevExpress.XtraEditors.LabelControl();
            this.lblRecordCount = new DevExpress.XtraEditors.LabelControl();
            this.chkShowFilter = new DevExpress.XtraEditors.CheckEdit();
            this.gcComponentDetails = new DevExpress.XtraGrid.GridControl();
            this.gvComponentDetails = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colComponentId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colComponent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDescription = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLedgerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProcesstype = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colFixedValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colLinkValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEquation = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colMaxSlap = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProcessComponent = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colPayable = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIsEditable = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colImportValue = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colShowInBrowse = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colEquationId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colCompround = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIfCondition = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colRelatedComponents = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccessFlag = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcComponentDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvComponentDetails)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.ucToolBar2);
            this.layoutControl1.Controls.Add(this.lblRecord);
            this.layoutControl1.Controls.Add(this.lblRecordCount);
            this.layoutControl1.Controls.Add(this.chkShowFilter);
            this.layoutControl1.Controls.Add(this.gcComponentDetails);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(376, -3, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // ucToolBar2
            // 
            this.ucToolBar2.ChangeAddCaption = "<u>&A</u>dd";
            this.ucToolBar2.DisableAddButton = true;
            this.ucToolBar2.DisableCloseButton = true;
            this.ucToolBar2.DisableDeleteButton = true;
            this.ucToolBar2.DisableEditButton = true;
            this.ucToolBar2.DisableImportButton = true;
            this.ucToolBar2.DisablePrintButton = true;
            this.ucToolBar2.DisableRefreshButton = true;
            resources.ApplyResources(this.ucToolBar2, "ucToolBar2");
            this.ucToolBar2.Name = "ucToolBar2";
            this.ucToolBar2.VisibleAddButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar2.VisibleClose = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar2.VisibleDeleteButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar2.VisibleEditButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar2.VisibleImport = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar2.VisiblePrintButton = DevExpress.XtraBars.BarItemVisibility.Never;
            this.ucToolBar2.VisibleRefresh = DevExpress.XtraBars.BarItemVisibility.Always;
            this.ucToolBar2.AddClicked += new System.EventHandler(this.ucToolBar1_AddClicked);
            this.ucToolBar2.EditClicked += new System.EventHandler(this.ucToolBar1_EditClicked);
            this.ucToolBar2.DeleteClicked += new System.EventHandler(this.ucToolBar2_DeleteClicked);
            this.ucToolBar2.PrintClicked += new System.EventHandler(this.ucToolBar2_PrintClicked);
            this.ucToolBar2.CloseClicked += new System.EventHandler(this.ucToolBar2_CloseClicked);
            this.ucToolBar2.RefreshClicked += new System.EventHandler(this.ucToolBar2_RefreshClicked);
            // 
            // lblRecord
            // 
            resources.ApplyResources(this.lblRecord, "lblRecord");
            this.lblRecord.Name = "lblRecord";
            this.lblRecord.StyleController = this.layoutControl1;
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
            // gcComponentDetails
            // 
            resources.ApplyResources(this.gcComponentDetails, "gcComponentDetails");
            this.gcComponentDetails.MainView = this.gvComponentDetails;
            this.gcComponentDetails.Name = "gcComponentDetails";
            this.gcComponentDetails.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvComponentDetails});
            // 
            // gvComponentDetails
            // 
            this.gvComponentDetails.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvComponentDetails.Appearance.FocusedRow.Font")));
            this.gvComponentDetails.Appearance.FocusedRow.Options.UseFont = true;
            this.gvComponentDetails.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvComponentDetails.Appearance.HeaderPanel.Font")));
            this.gvComponentDetails.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvComponentDetails.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colComponentId,
            this.colComponent,
            this.colDescription,
            this.colType,
            this.colLedgerName,
            this.colProcesstype,
            this.colFixedValue,
            this.colLinkValue,
            this.colEquation,
            this.colMaxSlap,
            this.colProcessComponent,
            this.colPayable,
            this.colIsEditable,
            this.colImportValue,
            this.colShowInBrowse,
            this.colEquationId,
            this.colCompround,
            this.colIfCondition,
            this.colRelatedComponents,
            this.colAccessFlag});
            this.gvComponentDetails.GridControl = this.gcComponentDetails;
            this.gvComponentDetails.Name = "gvComponentDetails";
            this.gvComponentDetails.OptionsBehavior.Editable = false;
            this.gvComponentDetails.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvComponentDetails.OptionsView.ShowGroupPanel = false;
            this.gvComponentDetails.OptionsView.ShowIndicator = false;
            this.gvComponentDetails.DoubleClick += new System.EventHandler(this.gvComponentDetails_DoubleClick);
            this.gvComponentDetails.RowCountChanged += new System.EventHandler(this.gvComponentDetails_RowCountChanged);
            // 
            // colComponentId
            // 
            resources.ApplyResources(this.colComponentId, "colComponentId");
            this.colComponentId.FieldName = "COMPONENTID";
            this.colComponentId.Name = "colComponentId";
            this.colComponentId.OptionsColumn.AllowEdit = false;
            // 
            // colComponent
            // 
            resources.ApplyResources(this.colComponent, "colComponent");
            this.colComponent.FieldName = "COMPONENT";
            this.colComponent.Name = "colComponent";
            this.colComponent.OptionsColumn.AllowEdit = false;
            this.colComponent.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colDescription
            // 
            resources.ApplyResources(this.colDescription, "colDescription");
            this.colDescription.FieldName = "DESCRIPTION";
            this.colDescription.Name = "colDescription";
            this.colDescription.OptionsColumn.AllowEdit = false;
            this.colDescription.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colType
            // 
            resources.ApplyResources(this.colType, "colType");
            this.colType.FieldName = "Type";
            this.colType.Name = "colType";
            this.colType.OptionsColumn.AllowEdit = false;
            this.colType.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colLedgerName
            // 
            resources.ApplyResources(this.colLedgerName, "colLedgerName");
            this.colLedgerName.FieldName = "LEDGER_NAME";
            this.colLedgerName.Name = "colLedgerName";
            // 
            // colProcesstype
            // 
            resources.ApplyResources(this.colProcesstype, "colProcesstype");
            this.colProcesstype.FieldName = "PROCESS_TYPE";
            this.colProcesstype.Name = "colProcesstype";
            // 
            // colFixedValue
            // 
            resources.ApplyResources(this.colFixedValue, "colFixedValue");
            this.colFixedValue.FieldName = "DEFVALUE";
            this.colFixedValue.Name = "colFixedValue";
            this.colFixedValue.OptionsColumn.AllowEdit = false;
            this.colFixedValue.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colLinkValue
            // 
            resources.ApplyResources(this.colLinkValue, "colLinkValue");
            this.colLinkValue.FieldName = "LINKVALUE";
            this.colLinkValue.Name = "colLinkValue";
            this.colLinkValue.OptionsColumn.AllowEdit = false;
            this.colLinkValue.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colEquation
            // 
            resources.ApplyResources(this.colEquation, "colEquation");
            this.colEquation.FieldName = "EQUATION";
            this.colEquation.Name = "colEquation";
            this.colEquation.OptionsColumn.AllowEdit = false;
            // 
            // colMaxSlap
            // 
            resources.ApplyResources(this.colMaxSlap, "colMaxSlap");
            this.colMaxSlap.FieldName = "MAXSLAP";
            this.colMaxSlap.Name = "colMaxSlap";
            this.colMaxSlap.OptionsColumn.AllowEdit = false;
            this.colMaxSlap.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colProcessComponent
            // 
            resources.ApplyResources(this.colProcessComponent, "colProcessComponent");
            this.colProcessComponent.FieldName = "PROCESS_COMPONENT_TYPE";
            this.colProcessComponent.Name = "colProcessComponent";
            // 
            // colPayable
            // 
            resources.ApplyResources(this.colPayable, "colPayable");
            this.colPayable.FieldName = "PAYABLE";
            this.colPayable.Name = "colPayable";
            // 
            // colIsEditable
            // 
            resources.ApplyResources(this.colIsEditable, "colIsEditable");
            this.colIsEditable.FieldName = "ISEDITABLE";
            this.colIsEditable.Name = "colIsEditable";
            this.colIsEditable.OptionsColumn.AllowSize = false;
            this.colIsEditable.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colImportValue
            // 
            resources.ApplyResources(this.colImportValue, "colImportValue");
            this.colImportValue.FieldName = "DONT_IMPORT_MODIFIED_VALUE_PREV_PR";
            this.colImportValue.Name = "colImportValue";
            this.colImportValue.OptionsColumn.AllowSize = false;
            this.colImportValue.OptionsFilter.AutoFilterCondition = DevExpress.XtraGrid.Columns.AutoFilterCondition.Contains;
            // 
            // colShowInBrowse
            // 
            resources.ApplyResources(this.colShowInBrowse, "colShowInBrowse");
            this.colShowInBrowse.FieldName = "DONT_SHOWINBROWSE";
            this.colShowInBrowse.Name = "colShowInBrowse";
            this.colShowInBrowse.OptionsColumn.AllowEdit = false;
            // 
            // colEquationId
            // 
            resources.ApplyResources(this.colEquationId, "colEquationId");
            this.colEquationId.FieldName = "EQUATIONID";
            this.colEquationId.Name = "colEquationId";
            this.colEquationId.OptionsColumn.AllowEdit = false;
            // 
            // colCompround
            // 
            resources.ApplyResources(this.colCompround, "colCompround");
            this.colCompround.FieldName = "COMPROUND";
            this.colCompround.Name = "colCompround";
            this.colCompround.OptionsColumn.AllowEdit = false;
            // 
            // colIfCondition
            // 
            resources.ApplyResources(this.colIfCondition, "colIfCondition");
            this.colIfCondition.FieldName = "IFCONDITION";
            this.colIfCondition.Name = "colIfCondition";
            this.colIfCondition.OptionsColumn.AllowEdit = false;
            // 
            // colRelatedComponents
            // 
            resources.ApplyResources(this.colRelatedComponents, "colRelatedComponents");
            this.colRelatedComponents.FieldName = "RELATEDCOMPONENTS";
            this.colRelatedComponents.Name = "colRelatedComponents";
            this.colRelatedComponents.OptionsColumn.AllowEdit = false;
            // 
            // colAccessFlag
            // 
            resources.ApplyResources(this.colAccessFlag, "colAccessFlag");
            this.colAccessFlag.FieldName = "ACCESS_FLAG";
            this.colAccessFlag.Name = "colAccessFlag";
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.layoutControlItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(856, 353);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcComponentDetails;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 30);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(104, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(-1, -1, -1, -1);
            this.layoutControlItem2.Size = new System.Drawing.Size(854, 298);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkShowFilter;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(0, 328);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(819, 23);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.lblRecordCount;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(819, 328);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(13, 23);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.lblRecord;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(832, 328);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(22, 23);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ucToolBar2;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(0, 30);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(98, 30);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(-1, -1, -1, -1);
            this.layoutControlItem1.Size = new System.Drawing.Size(854, 30);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // frmComponentView
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmComponentView";
            this.ShowFilterClicked += new System.EventHandler(this.frmComponentView_ShowFilterClicked);
            this.EnterClicked += new System.EventHandler(this.frmComponentView_EnterClicked);
            this.Load += new System.EventHandler(this.frmComponentView_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.chkShowFilter.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcComponentDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvComponentDetails)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private UserControl.UcToolBar ucToolBar1;
        private DevExpress.XtraGrid.GridControl gcComponentDetails;
        private DevExpress.XtraGrid.Views.Grid.GridView gvComponentDetails;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraGrid.Columns.GridColumn colComponent;
        private DevExpress.XtraGrid.Columns.GridColumn colDescription;
        private DevExpress.XtraGrid.Columns.GridColumn colType;
        private DevExpress.XtraGrid.Columns.GridColumn colFixedValue;
        private DevExpress.XtraGrid.Columns.GridColumn colLinkValue;
        private DevExpress.XtraGrid.Columns.GridColumn colMaxSlap;
        private DevExpress.XtraGrid.Columns.GridColumn colIsEditable;
        private DevExpress.XtraGrid.Columns.GridColumn colComponentId;
        private DevExpress.XtraGrid.Columns.GridColumn colEquation;
        private DevExpress.XtraGrid.Columns.GridColumn colEquationId;
        private DevExpress.XtraGrid.Columns.GridColumn colCompround;
        private DevExpress.XtraGrid.Columns.GridColumn colIfCondition;
        private DevExpress.XtraGrid.Columns.GridColumn colShowInBrowse;
        private DevExpress.XtraGrid.Columns.GridColumn colRelatedComponents;
        private DevExpress.XtraEditors.CheckEdit chkShowFilter;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.LabelControl lblRecord;
        private DevExpress.XtraEditors.LabelControl lblRecordCount;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private UserControl.UcToolBar ucToolBar2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerName;
        private DevExpress.XtraGrid.Columns.GridColumn colAccessFlag;
        private DevExpress.XtraGrid.Columns.GridColumn colProcesstype;
        private DevExpress.XtraGrid.Columns.GridColumn colProcessComponent;
        private DevExpress.XtraGrid.Columns.GridColumn colPayable;
        private DevExpress.XtraGrid.Columns.GridColumn colImportValue;
    }
}