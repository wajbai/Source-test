namespace ACPP.Modules.Master
{
    partial class frmAccountingPeriodAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmAccountingPeriodAdd));
            this.lgBeginingDate = new DevExpress.XtraLayout.LayoutControlItem();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.detBookbeginingFrom = new DevExpress.XtraEditors.DateEdit();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.detYearTo = new DevExpress.XtraEditors.DateEdit();
            this.detYearFrom = new DevExpress.XtraEditors.DateEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcgYearFrom = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcgYearto = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcgBooksBeginning = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.lgBeginingDate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.detBookbeginingFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detBookbeginingFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detYearTo.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detYearTo.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detYearFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.detYearFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgYearFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgYearto)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgBooksBeginning)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // lgBeginingDate
            // 
            this.lgBeginingDate.AllowHtmlStringInCaption = true;
            resources.ApplyResources(this.lgBeginingDate, "lgBeginingDate");
            this.lgBeginingDate.Location = new System.Drawing.Point(155, 257);
            this.lgBeginingDate.Name = "lgBeginingDate";
            this.lgBeginingDate.Size = new System.Drawing.Size(215, 24);
            this.lgBeginingDate.TextSize = new System.Drawing.Size(114, 13);
            this.lgBeginingDate.TextToControlDistance = 5;
            // 
            // btnSave
            // 
            this.btnSave.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Appearance.Options.UseImage = true;
            this.btnSave.MaximumSize = new System.Drawing.Size(65, 0);
            this.btnSave.MinimumSize = new System.Drawing.Size(65, 0);
            this.btnSave.Name = "btnSave";
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.AllowCustomizationMenu = false;
            this.layoutControl1.Controls.Add(this.detBookbeginingFrom);
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.detYearTo);
            this.layoutControl1.Controls.Add(this.detYearFrom);
            this.layoutControl1.Controls.Add(this.btnSave);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(513, 75, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // detBookbeginingFrom
            // 
            resources.ApplyResources(this.detBookbeginingFrom, "detBookbeginingFrom");
            this.detBookbeginingFrom.Name = "detBookbeginingFrom";
            this.detBookbeginingFrom.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.detBookbeginingFrom.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.detBookbeginingFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("detBookbeginingFrom.Properties.Buttons"))))});
            this.detBookbeginingFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.detBookbeginingFrom.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("detBookbeginingFrom.Properties.Mask.MaskType")));
            this.detBookbeginingFrom.Properties.Mask.UseMaskAsDisplayFormat = ((bool)(resources.GetObject("detBookbeginingFrom.Properties.Mask.UseMaskAsDisplayFormat")));
            this.detBookbeginingFrom.StyleController = this.layoutControl1;
            // 
            // btnClose
            // 
            this.btnClose.AllowGlyphSkinning = DevExpress.Utils.DefaultBoolean.True;
            this.btnClose.AllowHtmlDraw = DevExpress.Utils.DefaultBoolean.True;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.MaximumSize = new System.Drawing.Size(65, 0);
            this.btnClose.MinimumSize = new System.Drawing.Size(65, 0);
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.layoutControl1;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // detYearTo
            // 
            resources.ApplyResources(this.detYearTo, "detYearTo");
            this.detYearTo.Name = "detYearTo";
            this.detYearTo.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.detYearTo.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.detYearTo.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("detYearTo.Properties.Buttons"))))});
            this.detYearTo.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.detYearTo.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("detYearTo.Properties.Mask.MaskType")));
            this.detYearTo.StyleController = this.layoutControl1;
            this.detYearTo.Leave += new System.EventHandler(this.detYearTo_Leave);
            // 
            // detYearFrom
            // 
            resources.ApplyResources(this.detYearFrom, "detYearFrom");
            this.detYearFrom.Name = "detYearFrom";
            this.detYearFrom.Properties.AllowNullInput = DevExpress.Utils.DefaultBoolean.False;
            this.detYearFrom.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.detYearFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(((DevExpress.XtraEditors.Controls.ButtonPredefines)(resources.GetObject("detYearFrom.Properties.Buttons"))))});
            this.detYearFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton()});
            this.detYearFrom.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("detYearFrom.Properties.Mask.MaskType")));
            this.detYearFrom.StyleController = this.layoutControl1;
            this.detYearFrom.EditValueChanged += new System.EventHandler(this.detYearFrom_EditValueChanged);
            this.detYearFrom.Leave += new System.EventHandler(this.detYearFrom_Leave);
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcgYearFrom,
            this.lcgYearto,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem2,
            this.lcgBooksBeginning,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(314, 77);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcgYearFrom
            // 
            this.lcgYearFrom.AllowHtmlStringInCaption = true;
            this.lcgYearFrom.Control = this.detYearFrom;
            resources.ApplyResources(this.lcgYearFrom, "lcgYearFrom");
            this.lcgYearFrom.Location = new System.Drawing.Point(0, 0);
            this.lcgYearFrom.Name = "lcgYearFrom";
            this.lcgYearFrom.Size = new System.Drawing.Size(157, 24);
            this.lcgYearFrom.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lcgYearFrom.TextSize = new System.Drawing.Size(61, 13);
            this.lcgYearFrom.TextToControlDistance = 5;
            // 
            // lcgYearto
            // 
            this.lcgYearto.AllowHtmlStringInCaption = true;
            this.lcgYearto.Control = this.detYearTo;
            resources.ApplyResources(this.lcgYearto, "lcgYearto");
            this.lcgYearto.Location = new System.Drawing.Point(157, 0);
            this.lcgYearto.Name = "lcgYearto";
            this.lcgYearto.Size = new System.Drawing.Size(157, 24);
            this.lcgYearto.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lcgYearto.TextSize = new System.Drawing.Size(49, 13);
            this.lcgYearto.TextToControlDistance = 5;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnSave;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(172, 51);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(65, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(73, 26);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnClose;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(245, 51);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 24);
            this.emptySpaceItem2.MinSize = new System.Drawing.Size(104, 24);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(104, 27);
            this.emptySpaceItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcgBooksBeginning
            // 
            this.lcgBooksBeginning.AllowHtmlStringInCaption = true;
            this.lcgBooksBeginning.Control = this.detBookbeginingFrom;
            resources.ApplyResources(this.lcgBooksBeginning, "lcgBooksBeginning");
            this.lcgBooksBeginning.Location = new System.Drawing.Point(104, 24);
            this.lcgBooksBeginning.MinSize = new System.Drawing.Size(171, 24);
            this.lcgBooksBeginning.Name = "lcgBooksBeginning";
            this.lcgBooksBeginning.Size = new System.Drawing.Size(210, 27);
            this.lcgBooksBeginning.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcgBooksBeginning.TextSize = new System.Drawing.Size(113, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 51);
            this.emptySpaceItem1.MaxSize = new System.Drawing.Size(172, 26);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(172, 26);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(172, 26);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmAccountingPeriodAdd
            // 
            this.AcceptButton = this.btnSave;
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmAccountingPeriodAdd";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.frmAccountingPeriodAdd_FormClosed);
            this.Load += new System.EventHandler(this.frmAccountingPeriodAdd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lgBeginingDate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.detBookbeginingFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detBookbeginingFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detYearTo.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detYearTo.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detYearFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.detYearFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgYearFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgYearto)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgBooksBeginning)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.DateEdit detYearTo;
        private DevExpress.XtraEditors.DateEdit detYearFrom;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem lcgYearFrom;
        private DevExpress.XtraLayout.LayoutControlItem lcgYearto;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem lgBeginingDate;
        private DevExpress.XtraEditors.DateEdit detBookbeginingFrom;
        private DevExpress.XtraLayout.LayoutControlItem lcgBooksBeginning;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}