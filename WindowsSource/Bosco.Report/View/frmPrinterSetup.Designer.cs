namespace Bosco.Report.View
{
    partial class frmPrinterSetup
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
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnPrint = new DevExpress.XtraEditors.SimpleButton();
            this.cbPrinterNames = new DevExpress.XtraEditors.ComboBoxEdit();
            this.cboOddEven = new DevExpress.XtraEditors.ComboBoxEdit();
            this.txtPages = new DevExpress.XtraEditors.TextEdit();
            this.chkPages = new DevExpress.XtraEditors.CheckEdit();
            this.chkOddEven = new DevExpress.XtraEditors.CheckEdit();
            this.chkAllPages = new DevExpress.XtraEditors.CheckEdit();
            this.chkCurrentPage = new DevExpress.XtraEditors.CheckEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcPageRange = new DevExpress.XtraLayout.SimpleLabelItem();
            this.lcPrinterName = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem7 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem8 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem4 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcPagestext = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcOddevencombo = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblExamplePages = new DevExpress.XtraLayout.SimpleLabelItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.cbPrinterNames.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboOddEven.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPages.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPages.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkOddEven.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAllPages.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCurrentPage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPageRange)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPrinterName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPagestext)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOddevencombo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblExamplePages)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnPrint);
            this.layoutControl1.Controls.Add(this.cbPrinterNames);
            this.layoutControl1.Controls.Add(this.cboOddEven);
            this.layoutControl1.Controls.Add(this.txtPages);
            this.layoutControl1.Controls.Add(this.chkPages);
            this.layoutControl1.Controls.Add(this.chkOddEven);
            this.layoutControl1.Controls.Add(this.chkAllPages);
            this.layoutControl1.Controls.Add(this.chkCurrentPage);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(0, 0);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(728, 7, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(377, 176);
            this.layoutControl1.TabIndex = 2;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(310, 147);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(60, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnPrint
            // 
            this.btnPrint.Location = new System.Drawing.Point(246, 147);
            this.btnPrint.Name = "btnPrint";
            this.btnPrint.Size = new System.Drawing.Size(60, 22);
            this.btnPrint.StyleController = this.layoutControl1;
            this.btnPrint.TabIndex = 13;
            this.btnPrint.Text = "Print";
            this.btnPrint.Click += new System.EventHandler(this.btnPrint_Click);
            // 
            // cbPrinterNames
            // 
            this.cbPrinterNames.Location = new System.Drawing.Point(80, 7);
            this.cbPrinterNames.Name = "cbPrinterNames";
            this.cbPrinterNames.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.cbPrinterNames.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cbPrinterNames.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cbPrinterNames.Size = new System.Drawing.Size(290, 20);
            this.cbPrinterNames.StyleController = this.layoutControl1;
            this.cbPrinterNames.TabIndex = 12;
            // 
            // cboOddEven
            // 
            this.cboOddEven.Location = new System.Drawing.Point(184, 79);
            this.cboOddEven.Name = "cboOddEven";
            this.cboOddEven.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.cboOddEven.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cboOddEven.Properties.Items.AddRange(new object[] {
            "Only Print Odd Pages",
            "Only Print Even Pages"});
            this.cboOddEven.Properties.TextEditStyle = DevExpress.XtraEditors.Controls.TextEditStyles.DisableTextEditor;
            this.cboOddEven.Size = new System.Drawing.Size(186, 20);
            this.cboOddEven.StyleController = this.layoutControl1;
            this.cboOddEven.TabIndex = 9;
            // 
            // txtPages
            // 
            this.txtPages.Location = new System.Drawing.Point(184, 103);
            this.txtPages.Name = "txtPages";
            this.txtPages.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtPages.Size = new System.Drawing.Size(186, 20);
            this.txtPages.StyleController = this.layoutControl1;
            this.txtPages.TabIndex = 8;
            // 
            // chkPages
            // 
            this.chkPages.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chkPages.Location = new System.Drawing.Point(80, 103);
            this.chkPages.Name = "chkPages";
            this.chkPages.Properties.Caption = "Pages";
            this.chkPages.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.chkPages.Properties.RadioGroupIndex = 0;
            this.chkPages.Size = new System.Drawing.Size(102, 19);
            this.chkPages.StyleController = this.layoutControl1;
            this.chkPages.TabIndex = 7;
            this.chkPages.TabStop = false;
            this.chkPages.CheckedChanged += new System.EventHandler(this.chkPages_CheckedChanged);
            // 
            // chkOddEven
            // 
            this.chkOddEven.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chkOddEven.Location = new System.Drawing.Point(80, 79);
            this.chkOddEven.Name = "chkOddEven";
            this.chkOddEven.Properties.Caption = "Odd/Even Pages";
            this.chkOddEven.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.chkOddEven.Properties.RadioGroupIndex = 0;
            this.chkOddEven.Size = new System.Drawing.Size(102, 19);
            this.chkOddEven.StyleController = this.layoutControl1;
            this.chkOddEven.TabIndex = 7;
            this.chkOddEven.TabStop = false;
            this.chkOddEven.CheckedChanged += new System.EventHandler(this.chkOddEven_CheckedChanged);
            // 
            // chkAllPages
            // 
            this.chkAllPages.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chkAllPages.Location = new System.Drawing.Point(80, 31);
            this.chkAllPages.Name = "chkAllPages";
            this.chkAllPages.Properties.Caption = "All";
            this.chkAllPages.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.chkAllPages.Properties.RadioGroupIndex = 0;
            this.chkAllPages.Size = new System.Drawing.Size(292, 19);
            this.chkAllPages.StyleController = this.layoutControl1;
            this.chkAllPages.TabIndex = 6;
            this.chkAllPages.TabStop = false;
            // 
            // chkCurrentPage
            // 
            this.chkCurrentPage.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.chkCurrentPage.Location = new System.Drawing.Point(80, 55);
            this.chkCurrentPage.Name = "chkCurrentPage";
            this.chkCurrentPage.Properties.Caption = "Current Page";
            this.chkCurrentPage.Properties.CheckStyle = DevExpress.XtraEditors.Controls.CheckStyles.Radio;
            this.chkCurrentPage.Properties.RadioGroupIndex = 0;
            this.chkCurrentPage.Size = new System.Drawing.Size(292, 19);
            this.chkCurrentPage.StyleController = this.layoutControl1;
            this.chkCurrentPage.TabIndex = 6;
            this.chkCurrentPage.TabStop = false;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcPageRange,
            this.lcPrinterName,
            this.layoutControlItem7,
            this.layoutControlItem8,
            this.emptySpaceItem1,
            this.layoutControlItem3,
            this.layoutControlItem4,
            this.layoutControlItem5,
            this.lcPagestext,
            this.lcOddevencombo,
            this.lblExamplePages,
            this.layoutControlItem2});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup1.Size = new System.Drawing.Size(377, 176);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcPageRange
            // 
            this.lcPageRange.AllowHotTrack = false;
            this.lcPageRange.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lcPageRange.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.lcPageRange.CustomizationFormText = "lcPrintRange";
            this.lcPageRange.Location = new System.Drawing.Point(0, 24);
            this.lcPageRange.Name = "lcPageRange";
            this.lcPageRange.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 5, 2, 2);
            this.lcPageRange.Size = new System.Drawing.Size(75, 113);
            this.lcPageRange.Text = "Page Range";
            this.lcPageRange.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcPageRange.TextSize = new System.Drawing.Size(70, 13);
            // 
            // lcPrinterName
            // 
            this.lcPrinterName.Control = this.cbPrinterNames;
            this.lcPrinterName.CustomizationFormText = "Printer Name";
            this.lcPrinterName.Location = new System.Drawing.Point(0, 0);
            this.lcPrinterName.Name = "lcPrinterName";
            this.lcPrinterName.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 2, 2, 2);
            this.lcPrinterName.Size = new System.Drawing.Size(367, 24);
            this.lcPrinterName.Text = "Printer Name";
            this.lcPrinterName.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.CustomSize;
            this.lcPrinterName.TextSize = new System.Drawing.Size(70, 13);
            this.lcPrinterName.TextToControlDistance = 5;
            // 
            // layoutControlItem7
            // 
            this.layoutControlItem7.Control = this.btnPrint;
            this.layoutControlItem7.CustomizationFormText = "layoutControlItem7";
            this.layoutControlItem7.Location = new System.Drawing.Point(239, 137);
            this.layoutControlItem7.Name = "layoutControlItem7";
            this.layoutControlItem7.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 5, 2);
            this.layoutControlItem7.Size = new System.Drawing.Size(64, 29);
            this.layoutControlItem7.Text = "layoutControlItem7";
            this.layoutControlItem7.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem7.TextToControlDistance = 0;
            this.layoutControlItem7.TextVisible = false;
            // 
            // layoutControlItem8
            // 
            this.layoutControlItem8.Control = this.btnCancel;
            this.layoutControlItem8.CustomizationFormText = "layoutControlItem8";
            this.layoutControlItem8.Location = new System.Drawing.Point(303, 137);
            this.layoutControlItem8.Name = "layoutControlItem8";
            this.layoutControlItem8.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 5, 2);
            this.layoutControlItem8.Size = new System.Drawing.Size(64, 29);
            this.layoutControlItem8.Text = "layoutControlItem8";
            this.layoutControlItem8.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem8.TextToControlDistance = 0;
            this.layoutControlItem8.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 137);
            this.emptySpaceItem1.MinSize = new System.Drawing.Size(104, 24);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 5, 2);
            this.emptySpaceItem1.Size = new System.Drawing.Size(239, 29);
            this.emptySpaceItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.chkCurrentPage;
            this.layoutControlItem3.CustomizationFormText = "layoutControlItem3";
            this.layoutControlItem3.Location = new System.Drawing.Point(75, 48);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 2, 3);
            this.layoutControlItem3.Size = new System.Drawing.Size(292, 24);
            this.layoutControlItem3.Text = "layoutControlItem3";
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // layoutControlItem4
            // 
            this.layoutControlItem4.Control = this.chkOddEven;
            this.layoutControlItem4.CustomizationFormText = "layoutControlItem4";
            this.layoutControlItem4.Location = new System.Drawing.Point(75, 72);
            this.layoutControlItem4.Name = "layoutControlItem4";
            this.layoutControlItem4.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 2, 3);
            this.layoutControlItem4.Size = new System.Drawing.Size(102, 24);
            this.layoutControlItem4.Text = "layoutControlItem4";
            this.layoutControlItem4.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem4.TextToControlDistance = 0;
            this.layoutControlItem4.TextVisible = false;
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.chkPages;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(75, 96);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 2, 3);
            this.layoutControlItem5.Size = new System.Drawing.Size(102, 41);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // lcPagestext
            // 
            this.lcPagestext.Control = this.txtPages;
            this.lcPagestext.CustomizationFormText = "lcPagestext";
            this.lcPagestext.Location = new System.Drawing.Point(177, 96);
            this.lcPagestext.Name = "lcPagestext";
            this.lcPagestext.Size = new System.Drawing.Size(190, 24);
            this.lcPagestext.Text = "lcPagestext";
            this.lcPagestext.TextSize = new System.Drawing.Size(0, 0);
            this.lcPagestext.TextToControlDistance = 0;
            this.lcPagestext.TextVisible = false;
            this.lcPagestext.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // lcOddevencombo
            // 
            this.lcOddevencombo.Control = this.cboOddEven;
            this.lcOddevencombo.CustomizationFormText = "lcOddevencombo";
            this.lcOddevencombo.Location = new System.Drawing.Point(177, 72);
            this.lcOddevencombo.Name = "lcOddevencombo";
            this.lcOddevencombo.Size = new System.Drawing.Size(190, 24);
            this.lcOddevencombo.Text = "lcOddevencombo";
            this.lcOddevencombo.TextSize = new System.Drawing.Size(0, 0);
            this.lcOddevencombo.TextToControlDistance = 0;
            this.lcOddevencombo.TextVisible = false;
            this.lcOddevencombo.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // lblExamplePages
            // 
            this.lblExamplePages.AllowHotTrack = false;
            this.lblExamplePages.AppearanceItemCaption.Options.UseTextOptions = true;
            this.lblExamplePages.AppearanceItemCaption.TextOptions.VAlignment = DevExpress.Utils.VertAlignment.Top;
            this.lblExamplePages.CustomizationFormText = "For Example, 5-12";
            this.lblExamplePages.Location = new System.Drawing.Point(177, 120);
            this.lblExamplePages.Name = "lblExamplePages";
            this.lblExamplePages.Size = new System.Drawing.Size(190, 17);
            this.lblExamplePages.Text = "For Example, 5-12";
            this.lblExamplePages.TextSize = new System.Drawing.Size(88, 13);
            this.lblExamplePages.Visibility = DevExpress.XtraLayout.Utils.LayoutVisibility.Never;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.chkAllPages;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(75, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 2, 3);
            this.layoutControlItem2.Size = new System.Drawing.Size(292, 24);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // frmPrinterSetup
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(377, 176);
            this.Controls.Add(this.layoutControl1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmPrinterSetup";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Print Setup";
            this.Load += new System.EventHandler(this.frmPrinterSetup_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.cbPrinterNames.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cboOddEven.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtPages.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkPages.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkOddEven.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkAllPages.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkCurrentPage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPageRange)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPrinterName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcPagestext)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcOddevencombo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblExamplePages)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.CheckEdit chkCurrentPage;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.SimpleLabelItem lcPageRange;
        private DevExpress.XtraEditors.CheckEdit chkAllPages;
        private DevExpress.XtraEditors.CheckEdit chkPages;
        private DevExpress.XtraEditors.CheckEdit chkOddEven;
        private DevExpress.XtraEditors.TextEdit txtPages;
        private DevExpress.XtraEditors.ComboBoxEdit cboOddEven;
        private DevExpress.XtraEditors.ComboBoxEdit cbPrinterNames;
        private DevExpress.XtraLayout.LayoutControlItem lcPrinterName;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraEditors.SimpleButton btnPrint;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem7;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem8;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem4;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem lcPagestext;
        private DevExpress.XtraLayout.LayoutControlItem lcOddevencombo;
        private DevExpress.XtraLayout.SimpleLabelItem lblExamplePages;

    }
}