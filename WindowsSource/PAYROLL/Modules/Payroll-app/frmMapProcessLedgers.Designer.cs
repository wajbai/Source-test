namespace PAYROLL.Modules.Payroll_app
{
    partial class frmMapProcessLedgers
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMapProcessLedgers));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.gcMapProcessTypeLedgers = new DevExpress.XtraGrid.GridControl();
            this.gvMapProcessTypeLedgers = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProcessTypeId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colProcessType = new DevExpress.XtraGrid.Columns.GridColumn();
            this.coLedgerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.glkDeductionLedger = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit2View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDedLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDedLedgerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.glkIncomeLedger = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colIncomeLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colIncomeLedgerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblCaption = new DevExpress.XtraLayout.SimpleLabelItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcMapProcessTypeLedgers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMapProcessTypeLedgers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkDeductionLedger.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit2View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkIncomeLedger.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCaption)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.gcMapProcessTypeLedgers);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.glkDeductionLedger);
            this.layoutControl1.Controls.Add(this.glkIncomeLedger);
            this.layoutControl1.Controls.Add(this.btnClose);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(444, 122, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // gcMapProcessTypeLedgers
            // 
            resources.ApplyResources(this.gcMapProcessTypeLedgers, "gcMapProcessTypeLedgers");
            this.gcMapProcessTypeLedgers.MainView = this.gvMapProcessTypeLedgers;
            this.gcMapProcessTypeLedgers.Name = "gcMapProcessTypeLedgers";
            this.gcMapProcessTypeLedgers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMapProcessTypeLedgers});
            // 
            // gvMapProcessTypeLedgers
            // 
            this.gvMapProcessTypeLedgers.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gvMapProcessTypeLedgers.Appearance.FocusedRow.Font")));
            this.gvMapProcessTypeLedgers.Appearance.FocusedRow.Options.UseFont = true;
            this.gvMapProcessTypeLedgers.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvMapProcessTypeLedgers.Appearance.HeaderPanel.Font")));
            this.gvMapProcessTypeLedgers.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvMapProcessTypeLedgers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colLedgerId,
            this.colProcessTypeId,
            this.colProcessType,
            this.coLedgerName});
            this.gvMapProcessTypeLedgers.GridControl = this.gcMapProcessTypeLedgers;
            this.gvMapProcessTypeLedgers.Name = "gvMapProcessTypeLedgers";
            this.gvMapProcessTypeLedgers.OptionsBehavior.Editable = false;
            this.gvMapProcessTypeLedgers.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gvMapProcessTypeLedgers.OptionsView.ShowGroupPanel = false;
            this.gvMapProcessTypeLedgers.OptionsView.ShowIndicator = false;
            // 
            // colLedgerId
            // 
            resources.ApplyResources(this.colLedgerId, "colLedgerId");
            this.colLedgerId.FieldName = "LEDGER_ID";
            this.colLedgerId.Name = "colLedgerId";
            // 
            // colProcessTypeId
            // 
            resources.ApplyResources(this.colProcessTypeId, "colProcessTypeId");
            this.colProcessTypeId.FieldName = "TYPE_ID";
            this.colProcessTypeId.Name = "colProcessTypeId";
            // 
            // colProcessType
            // 
            resources.ApplyResources(this.colProcessType, "colProcessType");
            this.colProcessType.FieldName = "PROCESS_TYPE";
            this.colProcessType.Name = "colProcessType";
            // 
            // coLedgerName
            // 
            resources.ApplyResources(this.coLedgerName, "coLedgerName");
            this.coLedgerName.FieldName = "LEDGER_NAME";
            this.coLedgerName.Name = "coLedgerName";
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // glkDeductionLedger
            // 
            resources.ApplyResources(this.glkDeductionLedger, "glkDeductionLedger");
            this.glkDeductionLedger.Name = "glkDeductionLedger";
            this.glkDeductionLedger.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkDeductionLedger.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkDeductionLedger.Properties.Buttons"))))});
            this.glkDeductionLedger.Properties.ImmediatePopup = true;
            this.glkDeductionLedger.Properties.NullText = resources.GetString("glkDeductionLedger.Properties.NullText");
            this.glkDeductionLedger.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.glkDeductionLedger.Properties.PopupFormSize = new System.Drawing.Size(286, 120);
            this.glkDeductionLedger.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.glkDeductionLedger.Properties.View = this.gridLookUpEdit2View;
            this.glkDeductionLedger.StyleController = this.layoutControl1;
            // 
            // gridLookUpEdit2View
            // 
            this.gridLookUpEdit2View.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridLookUpEdit2View.Appearance.FocusedRow.Font")));
            this.gridLookUpEdit2View.Appearance.FocusedRow.Options.UseFont = true;
            this.gridLookUpEdit2View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDedLedgerId,
            this.colDedLedgerName});
            this.gridLookUpEdit2View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit2View.Name = "gridLookUpEdit2View";
            this.gridLookUpEdit2View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit2View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit2View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit2View.OptionsView.ShowIndicator = false;
            // 
            // colDedLedgerId
            // 
            resources.ApplyResources(this.colDedLedgerId, "colDedLedgerId");
            this.colDedLedgerId.FieldName = "LEDGER_ID";
            this.colDedLedgerId.Name = "colDedLedgerId";
            this.colDedLedgerId.OptionsColumn.AllowEdit = false;
            // 
            // colDedLedgerName
            // 
            resources.ApplyResources(this.colDedLedgerName, "colDedLedgerName");
            this.colDedLedgerName.FieldName = "LEDGER_NAME";
            this.colDedLedgerName.Name = "colDedLedgerName";
            // 
            // glkIncomeLedger
            // 
            resources.ApplyResources(this.glkIncomeLedger, "glkIncomeLedger");
            this.glkIncomeLedger.Name = "glkIncomeLedger";
            this.glkIncomeLedger.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkIncomeLedger.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkIncomeLedger.Properties.Buttons"))))});
            this.glkIncomeLedger.Properties.ImmediatePopup = true;
            this.glkIncomeLedger.Properties.NullText = resources.GetString("glkIncomeLedger.Properties.NullText");
            this.glkIncomeLedger.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.glkIncomeLedger.Properties.PopupFormMinSize = new System.Drawing.Size(246, 100);
            this.glkIncomeLedger.Properties.PopupFormSize = new System.Drawing.Size(286, 120);
            this.glkIncomeLedger.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.glkIncomeLedger.Properties.View = this.gridLookUpEdit1View;
            this.glkIncomeLedger.StyleController = this.layoutControl1;
            this.glkIncomeLedger.EditValueChanged += new System.EventHandler(this.glkIncomeLedger_EditValueChanged);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridLookUpEdit1View.Appearance.FocusedRow.Font")));
            this.gridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = true;
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colIncomeLedgerId,
            this.colIncomeLedgerName});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // colIncomeLedgerId
            // 
            resources.ApplyResources(this.colIncomeLedgerId, "colIncomeLedgerId");
            this.colIncomeLedgerId.FieldName = "Id";
            this.colIncomeLedgerId.Name = "colIncomeLedgerId";
            // 
            // colIncomeLedgerName
            // 
            resources.ApplyResources(this.colIncomeLedgerName, "colIncomeLedgerName");
            this.colIncomeLedgerName.FieldName = "Name";
            this.colIncomeLedgerName.Name = "colIncomeLedgerName";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.layoutControl1;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click_1);
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem2,
            this.layoutControlItem5,
            this.layoutControlItem4,
            this.layoutControlItem3,
            this.lblCaption,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup1.Size = new System.Drawing.Size(399, 239);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AllowHtmlStringInCaption = true;
            this.layoutControlItem1.Control = this.glkIncomeLedger;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.layoutControlItem1.Size = new System.Drawing.Size(389, 26);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(94, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.AllowHtmlStringInCaption = true;
            this.layoutControlItem2.Control = this.glkDeductionLedger;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 3, 3, 3);
            this.layoutControlItem2.Size = new System.Drawing.Size(389, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(94, 13);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 52);
            this.emptySpaceItem2.MaxSize = new System.Drawing.Size(269, 26);
            this.emptySpaceItem2.MinSize = new System.Drawing.Size(269, 26);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(269, 26);
            this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.gcMapProcessTypeLedgers;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(0, 94);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(389, 135);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.btnClose;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(329, 52);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(60, 26);
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnSave;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(269, 52);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(60, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // lblCaption
            // 
            this.lblCaption.AllowHotTrack = false;
            this.lblCaption.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblCaption.AppearanceItemCaption.Font")));
            this.lblCaption.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblCaption, "lblCaption");
            this.lblCaption.Location = new System.Drawing.Point(0, 78);
            this.lblCaption.Name = "lblCaption";
            this.lblCaption.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblCaption.Size = new System.Drawing.Size(103, 16);
            this.lblCaption.TextSize = new System.Drawing.Size(94, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(103, 78);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(286, 16);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmMapProcessLedgers
            // 
            this.AcceptButton = this.btnSave;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMapProcessLedgers";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.frmMapProcessLedgers_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcMapProcessTypeLedgers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMapProcessTypeLedgers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkDeductionLedger.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit2View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkIncomeLedger.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCaption)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.GridLookUpEdit glkDeductionLedger;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit2View;
        private DevExpress.XtraEditors.GridLookUpEdit glkIncomeLedger;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraGrid.Columns.GridColumn colDedLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn colDedLedgerName;
        private DevExpress.XtraGrid.Columns.GridColumn colIncomeLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn colIncomeLedgerName;
        private DevExpress.XtraGrid.GridControl gcMapProcessTypeLedgers;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMapProcessTypeLedgers;
        private DevExpress.XtraGrid.Columns.GridColumn colProcessType;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn coLedgerName;
        private DevExpress.XtraGrid.Columns.GridColumn colProcessTypeId;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.SimpleLabelItem lblCaption;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}