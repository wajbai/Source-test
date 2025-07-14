namespace ACPP.Modules.Inventory.Stock
{
    partial class frmStockJournalAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmStockJournalAdd));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.txtNarration = new DevExpress.XtraEditors.TextEdit();
            this.txtVoucherNo = new DevExpress.XtraEditors.TextEdit();
            this.deTransactopnDate = new DevExpress.XtraEditors.DateEdit();
            this.lblDestination = new DevExpress.XtraEditors.LabelControl();
            this.lblSource = new DevExpress.XtraEditors.LabelControl();
            this.gcDestination = new DevExpress.XtraGrid.GridControl();
            this.gvDestination = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colDNameOfItem = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colDAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.gcSource = new DevExpress.XtraGrid.GridControl();
            this.gvSource = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colSNameOfItem = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSQuantity = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSRate = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colSAmount = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblNarration = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblVoucherno = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtNarration.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVoucherNo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTransactopnDate.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTransactopnDate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDestination)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDestination)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNarration)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblVoucherno)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.txtNarration);
            this.layoutControl1.Controls.Add(this.txtVoucherNo);
            this.layoutControl1.Controls.Add(this.deTransactopnDate);
            this.layoutControl1.Controls.Add(this.lblDestination);
            this.layoutControl1.Controls.Add(this.lblSource);
            this.layoutControl1.Controls.Add(this.gcDestination);
            this.layoutControl1.Controls.Add(this.gcSource);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(225, 261, 218, 350);
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
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // txtNarration
            // 
            resources.ApplyResources(this.txtNarration, "txtNarration");
            this.txtNarration.Name = "txtNarration";
            this.txtNarration.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtNarration.StyleController = this.layoutControl1;
            // 
            // txtVoucherNo
            // 
            resources.ApplyResources(this.txtVoucherNo, "txtVoucherNo");
            this.txtVoucherNo.Name = "txtVoucherNo";
            this.txtVoucherNo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtVoucherNo.StyleController = this.layoutControl1;
            // 
            // deTransactopnDate
            // 
            resources.ApplyResources(this.deTransactopnDate, "deTransactopnDate");
            this.deTransactopnDate.Name = "deTransactopnDate";
            this.deTransactopnDate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.deTransactopnDate.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("deTransactopnDate.Properties.Buttons"))))});
            this.deTransactopnDate.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("deTransactopnDate.Properties.CalendarTimeProperties.Buttons"))))});
            this.deTransactopnDate.StyleController = this.layoutControl1;
            // 
            // lblDestination
            // 
            this.lblDestination.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblDestination.Appearance.Font")));
            resources.ApplyResources(this.lblDestination, "lblDestination");
            this.lblDestination.Name = "lblDestination";
            this.lblDestination.StyleController = this.layoutControl1;
            // 
            // lblSource
            // 
            this.lblSource.Appearance.Font = ((System.Drawing.Font)(resources.GetObject("lblSource.Appearance.Font")));
            resources.ApplyResources(this.lblSource, "lblSource");
            this.lblSource.Name = "lblSource";
            this.lblSource.StyleController = this.layoutControl1;
            // 
            // gcDestination
            // 
            resources.ApplyResources(this.gcDestination, "gcDestination");
            this.gcDestination.MainView = this.gvDestination;
            this.gcDestination.Name = "gcDestination";
            this.gcDestination.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvDestination});
            // 
            // gvDestination
            // 
            this.gvDestination.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colDNameOfItem,
            this.colDQuantity,
            this.colDRate,
            this.colDAmount});
            this.gvDestination.GridControl = this.gcDestination;
            this.gvDestination.Name = "gvDestination";
            this.gvDestination.OptionsView.ShowGroupPanel = false;
            // 
            // colDNameOfItem
            // 
            this.colDNameOfItem.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colDNameOfItem.AppearanceHeader.Font")));
            this.colDNameOfItem.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colDNameOfItem, "colDNameOfItem");
            this.colDNameOfItem.Name = "colDNameOfItem";
            // 
            // colDQuantity
            // 
            this.colDQuantity.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colDQuantity.AppearanceHeader.Font")));
            this.colDQuantity.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colDQuantity, "colDQuantity");
            this.colDQuantity.Name = "colDQuantity";
            // 
            // colDRate
            // 
            this.colDRate.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colDRate.AppearanceHeader.Font")));
            this.colDRate.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colDRate, "colDRate");
            this.colDRate.Name = "colDRate";
            // 
            // colDAmount
            // 
            this.colDAmount.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colDAmount.AppearanceHeader.Font")));
            this.colDAmount.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colDAmount, "colDAmount");
            this.colDAmount.Name = "colDAmount";
            // 
            // gcSource
            // 
            resources.ApplyResources(this.gcSource, "gcSource");
            this.gcSource.MainView = this.gvSource;
            this.gcSource.Name = "gcSource";
            this.gcSource.ViewCollection.AddRange(new DevExpress.XtraGrid.Views.Base.BaseView[] {
            this.gvSource});
            // 
            // gvSource
            // 
            this.gvSource.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colSNameOfItem,
            this.colSQuantity,
            this.colSRate,
            this.colSAmount});
            this.gvSource.GridControl = this.gcSource;
            this.gvSource.Name = "gvSource";
            this.gvSource.OptionsView.ShowGroupPanel = false;
            // 
            // colSNameOfItem
            // 
            this.colSNameOfItem.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colSNameOfItem.AppearanceHeader.Font")));
            this.colSNameOfItem.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colSNameOfItem, "colSNameOfItem");
            this.colSNameOfItem.Name = "colSNameOfItem";
            // 
            // colSQuantity
            // 
            this.colSQuantity.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colSQuantity.AppearanceHeader.Font")));
            this.colSQuantity.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colSQuantity, "colSQuantity");
            this.colSQuantity.Name = "colSQuantity";
            // 
            // colSRate
            // 
            this.colSRate.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colSRate.AppearanceHeader.Font")));
            this.colSRate.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colSRate, "colSRate");
            this.colSRate.Name = "colSRate";
            // 
            // colSAmount
            // 
            this.colSAmount.AppearanceHeader.Font = ((System.Drawing.Font)(resources.GetObject("colSAmount.AppearanceHeader.Font")));
            this.colSAmount.AppearanceHeader.Options.UseFont = true;
            resources.ApplyResources(this.colSAmount, "colSAmount");
            this.colSAmount.Name = "colSAmount";
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup2,
            this.emptySpaceItem3,
            this.lblDate,
            this.lblVoucherno});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(1040, 469);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlGroup2.AppearanceGroup.Font")));
            this.layoutControlGroup2.AppearanceGroup.Options.UseFont = true;
            this.layoutControlGroup2.AppearanceGroup.Options.UseTextOptions = true;
            this.layoutControlGroup2.AppearanceGroup.TextOptions.HAlignment = DevExpress.Utils.HorzAlignment.Center;
            this.layoutControlGroup2.AppearanceItemCaption.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlGroup2.AppearanceItemCaption.Font")));
            this.layoutControlGroup2.AppearanceItemCaption.Options.UseFont = true;
            this.layoutControlGroup2.AppearanceTabPage.Header.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlGroup2.AppearanceTabPage.Header.Font")));
            this.layoutControlGroup2.AppearanceTabPage.Header.Options.UseFont = true;
            resources.ApplyResources(this.layoutControlGroup2, "layoutControlGroup2");
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem2,
            this.layoutControlItem4,
            this.emptySpaceItem1,
            this.lblNarration,
            this.layoutControlItem5,
            this.layoutControlItem6});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 30);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup2.Size = new System.Drawing.Size(1040, 439);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.gcSource;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(518, 17);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem1.Size = new System.Drawing.Size(506, 357);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.gcDestination;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 17);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlItem2.Size = new System.Drawing.Size(518, 357);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.lblSource;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(142, 0);
            this.layoutControlItem3.MinSize = new System.Drawing.Size(67, 17);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(375, 17);
            this.layoutControlItem3.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.emptySpaceItem2.Size = new System.Drawing.Size(142, 17);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.lblDestination;
            resources.ApplyResources(this.layoutControlItem4, "layoutControlItem4");
            this.layoutControlItem4.Location = new System.Drawing.Point(667, 0);
            this.layoutControlItem4.MinSize = new System.Drawing.Size(67, 17);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Size = new System.Drawing.Size(357, 17);
            this.layoutControlItem4.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(517, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.emptySpaceItem1.Size = new System.Drawing.Size(150, 17);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblNarration
            // 
            this.lblNarration.Control = this.txtNarration;
            resources.ApplyResources(this.lblNarration, "lblNarration");
            this.lblNarration.Location = new System.Drawing.Point(0, 374);
            this.lblNarration.Name = "lblNarration";
            this.lblNarration.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 2, 5, 5);
            this.lblNarration.Size = new System.Drawing.Size(882, 30);
            this.lblNarration.TextSize = new System.Drawing.Size(53, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnSave;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(882, 374);
            this.layoutControlItem5.MaxSize = new System.Drawing.Size(70, 30);
            this.layoutControlItem5.MinSize = new System.Drawing.Size(70, 30);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 3, 3);
            this.layoutControlItem5.Size = new System.Drawing.Size(70, 30);
            this.layoutControlItem5.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnClose;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(952, 374);
            this.layoutControlItem6.MaxSize = new System.Drawing.Size(72, 30);
            this.layoutControlItem6.MinSize = new System.Drawing.Size(72, 30);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 5, 3, 3);
            this.layoutControlItem6.Size = new System.Drawing.Size(72, 30);
            this.layoutControlItem6.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem3, "emptySpaceItem3");
            this.emptySpaceItem3.Location = new System.Drawing.Point(134, 0);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(742, 30);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblDate
            // 
            this.lblDate.Control = this.deTransactopnDate;
            resources.ApplyResources(this.lblDate, "lblDate");
            this.lblDate.Location = new System.Drawing.Point(0, 0);
            this.lblDate.Name = "lblDate";
            this.lblDate.Padding = new DevExpress.XtraLayout.Utils.Padding(10, 5, 5, 5);
            this.lblDate.Size = new System.Drawing.Size(134, 30);
            this.lblDate.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblDate.TextSize = new System.Drawing.Size(23, 13);
            this.lblDate.TextToControlDistance = 5;
            // 
            // lblVoucherno
            // 
            this.lblVoucherno.Control = this.txtVoucherNo;
            resources.ApplyResources(this.lblVoucherno, "lblVoucherno");
            this.lblVoucherno.Location = new System.Drawing.Point(876, 0);
            this.lblVoucherno.Name = "lblVoucherno";
            this.lblVoucherno.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 3, 5, 5);
            this.lblVoucherno.Size = new System.Drawing.Size(164, 30);
            this.lblVoucherno.TextSize = new System.Drawing.Size(53, 13);
            // 
            // frmStockJournalAdd
            // 
            this.AcceptButton = this.btnSave;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmStockJournalAdd";
            this.Load += new System.EventHandler(this.frmStockJournalAdd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtNarration.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtVoucherNo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTransactopnDate.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.deTransactopnDate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcDestination)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvDestination)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gcSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gvSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNarration)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblVoucherno)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraGrid.GridControl gcDestination;
        private DevExpress.XtraGrid.Views.Grid.GridView gvDestination;
        private DevExpress.XtraGrid.GridControl gcSource;
        private DevExpress.XtraGrid.Views.Grid.GridView gvSource;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraGrid.Columns.GridColumn colSNameOfItem;
        private DevExpress.XtraGrid.Columns.GridColumn colSQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colSRate;
        private DevExpress.XtraGrid.Columns.GridColumn colSAmount;
        private DevExpress.XtraGrid.Columns.GridColumn colDNameOfItem;
        private DevExpress.XtraGrid.Columns.GridColumn colDQuantity;
        private DevExpress.XtraGrid.Columns.GridColumn colDRate;
        private DevExpress.XtraGrid.Columns.GridColumn colDAmount;
        private DevExpress.XtraEditors.LabelControl lblSource;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraEditors.LabelControl lblDestination;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraEditors.DateEdit deTransactopnDate;
        private DevExpress.XtraLayout.LayoutControlItem lblDate;
        private DevExpress.XtraEditors.TextEdit txtVoucherNo;
        private DevExpress.XtraLayout.LayoutControlItem lblVoucherno;
        private DevExpress.XtraEditors.TextEdit txtNarration;
        private DevExpress.XtraLayout.LayoutControlItem lblNarration;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
    }
}