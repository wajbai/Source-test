namespace ACPP.Modules.Dsync
{
    partial class frmMismatchedLedgerList
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMismatchedLedgerList));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.gcMismatchedLedgers = new DevExpress.XtraGrid.GridControl();
            this.gvMismatchedLedgers = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colLedgerName = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.simpleLabelItem1 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.simpleLabelItem2 = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lblRecordCount = new DevExpress.XtraLayout.SimpleLabelItem();
            this.simpleLabelItem3 = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.gcMismatchedLedgers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMismatchedLedgers)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRecordCount)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.gcMismatchedLedgers);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(834, 306, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.layoutControl1;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // gcMismatchedLedgers
            // 
            resources.ApplyResources(this.gcMismatchedLedgers, "gcMismatchedLedgers");
            this.gcMismatchedLedgers.MainView = this.gvMismatchedLedgers;
            this.gcMismatchedLedgers.Name = "gcMismatchedLedgers";
            this.gcMismatchedLedgers.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvMismatchedLedgers});
            // 
            // gvMismatchedLedgers
            // 
            this.gvMismatchedLedgers.Appearance.HeaderPanel.Font = ((System.Drawing.Font)(resources.GetObject("gvMismatchedLedgers.Appearance.HeaderPanel.Font")));
            this.gvMismatchedLedgers.Appearance.HeaderPanel.Options.UseFont = true;
            this.gvMismatchedLedgers.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colLedgerName});
            this.gvMismatchedLedgers.GridControl = this.gcMismatchedLedgers;
            this.gvMismatchedLedgers.Name = "gvMismatchedLedgers";
            this.gvMismatchedLedgers.OptionsView.ShowGroupPanel = false;
            this.gvMismatchedLedgers.OptionsView.ShowIndicator = false;
            this.gvMismatchedLedgers.RowCountChanged += new System.EventHandler(this.gvMismatchedLedgers_RowCountChanged);
            // 
            // colLedgerName
            // 
            resources.ApplyResources(this.colLedgerName, "colLedgerName");
            this.colLedgerName.FieldName = "LEDGER_NAME";
            this.colLedgerName.Name = "colLedgerName";
            this.colLedgerName.OptionsColumn.AllowEdit = false;
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.simpleLabelItem1,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1,
            this.simpleLabelItem2,
            this.lblRecordCount,
            this.simpleLabelItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(403, 304);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // simpleLabelItem1
            // 
            this.simpleLabelItem1.AllowHotTrack = false;
            this.simpleLabelItem1.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("simpleLabelItem1.AppearanceItemCaption.Font")));
            this.simpleLabelItem1.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.simpleLabelItem1, "simpleLabelItem1");
            this.simpleLabelItem1.Image = global::ACPP.Properties.Resources.info;
            this.simpleLabelItem1.Location = new System.Drawing.Point(0, 0);
            this.simpleLabelItem1.Name = "simpleLabelItem1";
            this.simpleLabelItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 0);
            this.simpleLabelItem1.Size = new System.Drawing.Size(403, 26);
            this.simpleLabelItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.simpleLabelItem1.TextSize = new System.Drawing.Size(358, 24);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcMismatchedLedgers;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 42);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(403, 236);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnClose;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(355, 278);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(48, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(343, 278);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(12, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // simpleLabelItem2
            // 
            this.simpleLabelItem2.AllowHotTrack = false;
            this.simpleLabelItem2.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("simpleLabelItem2.AppearanceItemCaption.Font")));
            this.simpleLabelItem2.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.simpleLabelItem2, "simpleLabelItem2");
            this.simpleLabelItem2.Location = new System.Drawing.Point(0, 278);
            this.simpleLabelItem2.Name = "simpleLabelItem2";
            this.simpleLabelItem2.Size = new System.Drawing.Size(16, 26);
            this.simpleLabelItem2.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.simpleLabelItem2.TextSize = new System.Drawing.Size(12, 13);
            // 
            // lblRecordCount
            // 
            this.lblRecordCount.AllowHotTrack = false;
            this.lblRecordCount.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("lblRecordCount.AppearanceItemCaption.Font")));
            this.lblRecordCount.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.lblRecordCount, "lblRecordCount");
            this.lblRecordCount.Location = new System.Drawing.Point(16, 278);
            this.lblRecordCount.Name = "lblRecordCount";
            this.lblRecordCount.Size = new System.Drawing.Size(327, 26);
            this.lblRecordCount.TextSize = new System.Drawing.Size(265, 13);
            // 
            // simpleLabelItem3
            // 
            this.simpleLabelItem3.AllowHotTrack = false;
            this.simpleLabelItem3.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("simpleLabelItem3.AppearanceItemCaption.Font")));
            this.simpleLabelItem3.AppearanceItemCaption.Options.UseFont = true;
            resources.ApplyResources(this.simpleLabelItem3, "simpleLabelItem3");
            this.simpleLabelItem3.Location = new System.Drawing.Point(0, 26);
            this.simpleLabelItem3.Name = "simpleLabelItem3";
            this.simpleLabelItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(33, 2, 0, 2);
            this.simpleLabelItem3.Size = new System.Drawing.Size(403, 16);
            this.simpleLabelItem3.TextSize = new System.Drawing.Size(265, 14);
            // 
            // frmMismatchedLedgerList
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.layoutControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMismatchedLedgerList";
            this.Load += new System.EventHandler(this.frmMismatchedLedgerList_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.gcMismatchedLedgers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvMismatchedLedgers)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblRecordCount)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.simpleLabelItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraGrid.GridControl gcMismatchedLedgers;
        private DevExpress.XtraGrid.Views.Grid.GridView gvMismatchedLedgers;
        private DevExpress.XtraGrid.Columns.GridColumn colLedgerName;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem2;
        private DevExpress.XtraLayout.SimpleLabelItem lblRecordCount;
        private DevExpress.XtraLayout.SimpleLabelItem simpleLabelItem3;

    }
}