namespace ACPP.Modules
{
    partial class frmAssetSettings
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAssetSettings));
            DevExpress.Utils.SerializableAppearanceObject serializableAppearanceObject1 = new DevExpress.Utils.SerializableAppearanceObject();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.dtOPApplyFrom = new DevExpress.XtraEditors.DateEdit();
            this.rgDepr = new DevExpress.XtraEditors.RadioGroup();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.glkpAccountLedger = new DevExpress.XtraEditors.GridLookUpEdit();
            this.gridLookUpEdit1View = new DevExpress.XtraGrid.Views.Grid.GridView();
            this.colAccountLedgerId = new DevExpress.XtraGrid.Columns.GridColumn();
            this.colAccountLedger = new DevExpress.XtraGrid.Columns.GridColumn();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlGroup3 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblAccountLedger = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblDepr = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblOpApplyFrom = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.btnDeleteAll = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dtOPApplyFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtOPApplyFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgDepr.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpAccountLedger.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAccountLedger)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDepr)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblOpApplyFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // btnOK
            // 
            this.btnOK.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.StyleController = this.layoutControl1;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnDeleteAll);
            this.layoutControl1.Controls.Add(this.dtOPApplyFrom);
            this.layoutControl1.Controls.Add(this.rgDepr);
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.glkpAccountLedger);
            this.layoutControl1.Controls.Add(this.btnOK);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(902, 249, 250, 343);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // dtOPApplyFrom
            // 
            resources.ApplyResources(this.dtOPApplyFrom, "dtOPApplyFrom");
            this.dtOPApplyFrom.Name = "dtOPApplyFrom";
            this.dtOPApplyFrom.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.dtOPApplyFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dtOPApplyFrom.Properties.Buttons"))))});
            this.dtOPApplyFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("dtOPApplyFrom.Properties.CalendarTimeProperties.Buttons"))))});
            this.dtOPApplyFrom.StyleController = this.layoutControl1;
            // 
            // rgDepr
            // 
            resources.ApplyResources(this.rgDepr, "rgDepr");
            this.rgDepr.Name = "rgDepr";
            this.rgDepr.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.rgDepr.Properties.Columns = 2;
            this.rgDepr.Properties.Items.AddRange(new DevExpress.XtraEditors.Controls.RadioGroupItem[] {
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("rgDepr.Properties.Items"))), resources.GetString("rgDepr.Properties.Items1")),
            new DevExpress.XtraEditors.Controls.RadioGroupItem(((object)(resources.GetObject("rgDepr.Properties.Items2"))), resources.GetString("rgDepr.Properties.Items3"))});
            this.rgDepr.StyleController = this.layoutControl1;
            // 
            // btnClose
            // 
            this.btnClose.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.layoutControl1;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // glkpAccountLedger
            // 
            resources.ApplyResources(this.glkpAccountLedger, "glkpAccountLedger");
            this.glkpAccountLedger.Name = "glkpAccountLedger";
            this.glkpAccountLedger.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.glkpAccountLedger.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpAccountLedger.Properties.Buttons")))),
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("glkpAccountLedger.Properties.Buttons1"))), resources.GetString("glkpAccountLedger.Properties.Buttons2"), ((int)(resources.GetObject("glkpAccountLedger.Properties.Buttons3"))), ((bool)(resources.GetObject("glkpAccountLedger.Properties.Buttons4"))), ((bool)(resources.GetObject("glkpAccountLedger.Properties.Buttons5"))), ((bool)(resources.GetObject("glkpAccountLedger.Properties.Buttons6"))), ((DevExpress.XtraEditors.ImageLocation)(resources.GetObject("glkpAccountLedger.Properties.Buttons7"))), ((System.Drawing.Image)(resources.GetObject("glkpAccountLedger.Properties.Buttons8"))), new DevExpress.Utils.KeyShortcut(System.Windows.Forms.Keys.None), serializableAppearanceObject1, resources.GetString("glkpAccountLedger.Properties.Buttons9"), ((object)(resources.GetObject("glkpAccountLedger.Properties.Buttons10"))), ((DevExpress.Utils.SuperToolTip)(resources.GetObject("glkpAccountLedger.Properties.Buttons11"))), ((bool)(resources.GetObject("glkpAccountLedger.Properties.Buttons12"))))});
            this.glkpAccountLedger.Properties.ImmediatePopup = true;
            this.glkpAccountLedger.Properties.NullText = resources.GetString("glkpAccountLedger.Properties.NullText");
            this.glkpAccountLedger.Properties.PopupFilterMode = DevExpress.XtraEditors.PopupFilterMode.Contains;
            this.glkpAccountLedger.Properties.PopupFormSize = new System.Drawing.Size(332, 0);
            this.glkpAccountLedger.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.Standard;
            this.glkpAccountLedger.Properties.View = this.gridLookUpEdit1View;
            this.glkpAccountLedger.StyleController = this.layoutControl1;
            this.glkpAccountLedger.ButtonClick += new DevExpress.XtraEditors.Controls.ButtonPressedEventHandler(this.glkpAccountLedger_ButtonClick);
            this.glkpAccountLedger.Leave += new System.EventHandler(this.glkpAccountLedger_Leave);
            // 
            // gridLookUpEdit1View
            // 
            this.gridLookUpEdit1View.Appearance.FocusedRow.Font = ((System.Drawing.Font)(resources.GetObject("gridLookUpEdit1View.Appearance.FocusedRow.Font")));
            this.gridLookUpEdit1View.Appearance.FocusedRow.Options.UseFont = true;
            this.gridLookUpEdit1View.Columns.AddRange(new DevExpress.XtraGrid.Columns.GridColumn[] {
            this.colAccountLedgerId,
            this.colAccountLedger});
            this.gridLookUpEdit1View.FocusRectStyle = DevExpress.XtraGrid.Views.Grid.DrawFocusRectStyle.RowFocus;
            this.gridLookUpEdit1View.Name = "gridLookUpEdit1View";
            this.gridLookUpEdit1View.OptionsSelection.EnableAppearanceFocusedCell = false;
            this.gridLookUpEdit1View.OptionsView.ShowColumnHeaders = false;
            this.gridLookUpEdit1View.OptionsView.ShowGroupPanel = false;
            this.gridLookUpEdit1View.OptionsView.ShowIndicator = false;
            // 
            // colAccountLedgerId
            // 
            resources.ApplyResources(this.colAccountLedgerId, "colAccountLedgerId");
            this.colAccountLedgerId.FieldName = "LEDGER_ID";
            this.colAccountLedgerId.Name = "colAccountLedgerId";
            // 
            // colAccountLedger
            // 
            resources.ApplyResources(this.colAccountLedger, "colAccountLedger");
            this.colAccountLedger.FieldName = "LEDGER_NAME";
            this.colAccountLedger.Name = "colAccountLedger";
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlGroup3,
            this.layoutControlItem6,
            this.emptySpaceItem2,
            this.layoutControlItem5,
            this.lblDepr,
            this.lblOpApplyFrom,
            this.emptySpaceItem1,
            this.layoutControlItem1,
            this.emptySpaceItem3});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutControlGroup1.Size = new System.Drawing.Size(529, 167);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // layoutControlGroup3
            // 
            this.layoutControlGroup3.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlGroup3.AppearanceGroup.Font")));
            this.layoutControlGroup3.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.layoutControlGroup3, "layoutControlGroup3");
            this.layoutControlGroup3.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblAccountLedger});
            this.layoutControlGroup3.Location = new System.Drawing.Point(0, 54);
            this.layoutControlGroup3.Name = "layoutControlGroup3";
            this.layoutControlGroup3.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutControlGroup3.Size = new System.Drawing.Size(525, 55);
            // 
            // lblAccountLedger
            // 
            this.lblAccountLedger.AllowHtmlStringInCaption = true;
            this.lblAccountLedger.Control = this.glkpAccountLedger;
            resources.ApplyResources(this.lblAccountLedger, "lblAccountLedger");
            this.lblAccountLedger.Location = new System.Drawing.Point(0, 0);
            this.lblAccountLedger.Name = "lblAccountLedger";
            this.lblAccountLedger.Size = new System.Drawing.Size(515, 26);
            this.lblAccountLedger.Spacing = new DevExpress.XtraLayout.Utils.Padding(1, 1, 1, 1);
            this.lblAccountLedger.TextSize = new System.Drawing.Size(99, 13);
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnClose;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(458, 109);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(67, 26);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 109);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(388, 26);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnOK;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(388, 109);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(70, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // lblDepr
            // 
            this.lblDepr.Control = this.rgDepr;
            resources.ApplyResources(this.lblDepr, "lblDepr");
            this.lblDepr.Location = new System.Drawing.Point(0, 0);
            this.lblDepr.MaxSize = new System.Drawing.Size(525, 30);
            this.lblDepr.MinSize = new System.Drawing.Size(525, 30);
            this.lblDepr.Name = "lblDepr";
            this.lblDepr.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 2, 2, 2);
            this.lblDepr.Size = new System.Drawing.Size(525, 30);
            this.lblDepr.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblDepr.TextSize = new System.Drawing.Size(99, 13);
            // 
            // lblOpApplyFrom
            // 
            this.lblOpApplyFrom.Control = this.dtOPApplyFrom;
            resources.ApplyResources(this.lblOpApplyFrom, "lblOpApplyFrom");
            this.lblOpApplyFrom.Location = new System.Drawing.Point(0, 30);
            this.lblOpApplyFrom.MaxSize = new System.Drawing.Size(0, 24);
            this.lblOpApplyFrom.MinSize = new System.Drawing.Size(158, 24);
            this.lblOpApplyFrom.Name = "lblOpApplyFrom";
            this.lblOpApplyFrom.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 2, 2, 2);
            this.lblOpApplyFrom.Size = new System.Drawing.Size(234, 24);
            this.lblOpApplyFrom.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblOpApplyFrom.TextSize = new System.Drawing.Size(99, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(234, 30);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(291, 24);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // btnDeleteAll
            // 
            resources.ApplyResources(this.btnDeleteAll, "btnDeleteAll");
            this.btnDeleteAll.Name = "btnDeleteAll";
            this.btnDeleteAll.StyleController = this.layoutControl1;
            this.btnDeleteAll.Click += new System.EventHandler(this.btnDeleteAll_Click);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnDeleteAll;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 135);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(194, 28);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem3, "emptySpaceItem3");
            this.emptySpaceItem3.Location = new System.Drawing.Point(194, 135);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(331, 28);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmAssetSettings
            // 
            this.AcceptButton = this.btnOK;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.layoutControl1);
            this.MinimizeBox = false;
            this.Name = "frmAssetSettings";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.frmAssetSettings_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dtOPApplyFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtOPApplyFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.rgDepr.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.glkpAccountLedger.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.gridLookUpEdit1View)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAccountLedger)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblDepr)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblOpApplyFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.GridLookUpEdit glkpAccountLedger;
        private DevExpress.XtraGrid.Views.Grid.GridView gridLookUpEdit1View;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountLedgerId;
        private DevExpress.XtraGrid.Columns.GridColumn colAccountLedger;
        private DevExpress.XtraLayout.LayoutControlItem lblAccountLedger;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraEditors.RadioGroup rgDepr;
        private DevExpress.XtraLayout.LayoutControlItem lblDepr;
        private DevExpress.XtraEditors.DateEdit dtOPApplyFrom;
        private DevExpress.XtraLayout.LayoutControlItem lblOpApplyFrom;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.SimpleButton btnDeleteAll;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
    }
}