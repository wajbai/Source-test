namespace ACPP.Modules.Master
{
	partial class frmCountry
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmCountry));
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.lcCountry = new DevExpress.XtraLayout.LayoutControl();
            this.txtExchangeRate = new DevExpress.XtraEditors.TextEdit();
            this.txtName = new DevExpress.XtraEditors.TextEdit();
            this.txtSymbols = new DevExpress.XtraEditors.TextEdit();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.txtCountryCode = new DevExpress.XtraEditors.TextEdit();
            this.txtCountry = new DevExpress.XtraEditors.TextEdit();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlGroup2 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblCode = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblCountry = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblCountryCode = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lcgExchangeRate = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcExchangeRate = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.lcCountry)).BeginInit();
            this.lcCountry.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtExchangeRate.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSymbols.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCountryCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCountry.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCountry)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCountryCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgExchangeRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcExchangeRate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.StyleController = this.lcCountry;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // lcCountry
            // 
            this.lcCountry.AllowCustomizationMenu = false;
            this.lcCountry.Controls.Add(this.txtExchangeRate);
            this.lcCountry.Controls.Add(this.txtName);
            this.lcCountry.Controls.Add(this.txtSymbols);
            this.lcCountry.Controls.Add(this.txtCode);
            this.lcCountry.Controls.Add(this.txtCountryCode);
            this.lcCountry.Controls.Add(this.txtCountry);
            this.lcCountry.Controls.Add(this.btnClose);
            this.lcCountry.Controls.Add(this.btnSave);
            resources.ApplyResources(this.lcCountry, "lcCountry");
            this.lcCountry.Name = "lcCountry";
            this.lcCountry.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(451, 39, 306, 543);
            this.lcCountry.OptionsView.UseDefaultDragAndDropRendering = false;
            this.lcCountry.Root = this.layoutControlGroup1;
            // 
            // txtExchangeRate
            // 
            resources.ApplyResources(this.txtExchangeRate, "txtExchangeRate");
            this.txtExchangeRate.EnterMoveNextControl = true;
            this.txtExchangeRate.Name = "txtExchangeRate";
            this.txtExchangeRate.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtExchangeRate.Properties.Mask.EditMask = resources.GetString("txtExchangeRate.Properties.Mask.EditMask");
            this.txtExchangeRate.Properties.Mask.MaskType = ((DevExpress.XtraEditors.Mask.MaskType)(resources.GetObject("txtExchangeRate.Properties.Mask.MaskType")));
            this.txtExchangeRate.StyleController = this.lcCountry;
            // 
            // txtName
            // 
            resources.ApplyResources(this.txtName, "txtName");
            this.txtName.Name = "txtName";
            this.txtName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtName.Properties.MaxLength = 40;
            this.txtName.StyleController = this.lcCountry;
            this.txtName.Leave += new System.EventHandler(this.txtName_Leave_1);
            // 
            // txtSymbols
            // 
            resources.ApplyResources(this.txtSymbols, "txtSymbols");
            this.txtSymbols.Name = "txtSymbols";
            this.txtSymbols.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtSymbols.Properties.MaxLength = 7;
            this.txtSymbols.StyleController = this.lcCountry;
            this.txtSymbols.Leave += new System.EventHandler(this.txtSymbols_Leave);
            // 
            // txtCode
            // 
            resources.ApplyResources(this.txtCode, "txtCode");
            this.txtCode.Name = "txtCode";
            this.txtCode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtCode.Properties.MaxLength = 8;
            this.txtCode.StyleController = this.lcCountry;
            this.txtCode.Leave += new System.EventHandler(this.txtCode_Leave);
            // 
            // txtCountryCode
            // 
            resources.ApplyResources(this.txtCountryCode, "txtCountryCode");
            this.txtCountryCode.Name = "txtCountryCode";
            this.txtCountryCode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtCountryCode.Properties.MaxLength = 5;
            this.txtCountryCode.StyleController = this.lcCountry;
            this.txtCountryCode.Leave += new System.EventHandler(this.txtCountryCode_Leave);
            // 
            // txtCountry
            // 
            resources.ApplyResources(this.txtCountry, "txtCountry");
            this.txtCountry.Name = "txtCountry";
            this.txtCountry.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtCountry.Properties.MaxLength = 50;
            this.txtCountry.StyleController = this.lcCountry;
            this.txtCountry.Leave += new System.EventHandler(this.txtCountry_Leave);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.lcCountry;
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.layoutControlGroup2,
            this.lblCountry,
            this.lblCountryCode,
            this.emptySpaceItem2,
            this.emptySpaceItem3,
            this.lcgExchangeRate});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(413, 202);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnClose;
            resources.ApplyResources(this.layoutControlItem5, "layoutControlItem5");
            this.layoutControlItem5.Location = new System.Drawing.Point(346, 176);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 0, 2, 2);
            this.layoutControlItem5.Size = new System.Drawing.Size(67, 26);
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnSave;
            resources.ApplyResources(this.layoutControlItem6, "layoutControlItem6");
            this.layoutControlItem6.Location = new System.Drawing.Point(277, 176);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(69, 26);
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // layoutControlGroup2
            // 
            this.layoutControlGroup2.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("layoutControlGroup2.AppearanceGroup.Font")));
            this.layoutControlGroup2.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.layoutControlGroup2, "layoutControlGroup2");
            this.layoutControlGroup2.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblCode,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem1});
            this.layoutControlGroup2.Location = new System.Drawing.Point(0, 50);
            this.layoutControlGroup2.Name = "layoutControlGroup2";
            this.layoutControlGroup2.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.layoutControlGroup2.Size = new System.Drawing.Size(413, 73);
            this.layoutControlGroup2.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            // 
            // lblCode
            // 
            this.lblCode.AllowHtmlStringInCaption = true;
            this.lblCode.Control = this.txtCode;
            resources.ApplyResources(this.lblCode, "lblCode");
            this.lblCode.Location = new System.Drawing.Point(239, 0);
            this.lblCode.Name = "lblCode";
            this.lblCode.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 0, 2, 2);
            this.lblCode.Size = new System.Drawing.Size(168, 24);
            this.lblCode.TextAlignMode = DevExpress.XtraLayout.TextAlignModeItem.AutoSize;
            this.lblCode.TextSize = new System.Drawing.Size(34, 13);
            this.lblCode.TextToControlDistance = 7;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.AllowHtmlStringInCaption = true;
            this.layoutControlItem1.Control = this.txtSymbols;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 0, 2, 2);
            this.layoutControlItem1.Size = new System.Drawing.Size(226, 24);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(79, 13);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.AllowHtmlStringInCaption = true;
            this.layoutControlItem2.Control = this.txtName;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(0, 24);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Padding = new DevExpress.XtraLayout.Utils.Padding(3, 0, 2, 2);
            this.layoutControlItem2.Size = new System.Drawing.Size(407, 24);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(79, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(226, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(13, 24);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblCountry
            // 
            this.lblCountry.AllowHtmlStringInCaption = true;
            this.lblCountry.Control = this.txtCountry;
            resources.ApplyResources(this.lblCountry, "lblCountry");
            this.lblCountry.Location = new System.Drawing.Point(0, 0);
            this.lblCountry.Name = "lblCountry";
            this.lblCountry.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 0, 0, 0);
            this.lblCountry.Size = new System.Drawing.Size(413, 25);
            this.lblCountry.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 2, 3);
            this.lblCountry.TextSize = new System.Drawing.Size(79, 13);
            // 
            // lblCountryCode
            // 
            this.lblCountryCode.AllowHtmlStringInCaption = true;
            this.lblCountryCode.Control = this.txtCountryCode;
            resources.ApplyResources(this.lblCountryCode, "lblCountryCode");
            this.lblCountryCode.Location = new System.Drawing.Point(0, 25);
            this.lblCountryCode.Name = "lblCountryCode";
            this.lblCountryCode.Padding = new DevExpress.XtraLayout.Utils.Padding(4, 0, 0, 0);
            this.lblCountryCode.Size = new System.Drawing.Size(227, 25);
            this.lblCountryCode.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 2, 3);
            this.lblCountryCode.TextSize = new System.Drawing.Size(79, 13);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem2, "emptySpaceItem2");
            this.emptySpaceItem2.Location = new System.Drawing.Point(227, 25);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(186, 25);
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem3, "emptySpaceItem3");
            this.emptySpaceItem3.Location = new System.Drawing.Point(0, 176);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(277, 26);
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lcgExchangeRate
            // 
            this.lcgExchangeRate.AppearanceGroup.Font = ((System.Drawing.Font)(resources.GetObject("lcgExchangeRate.AppearanceGroup.Font")));
            this.lcgExchangeRate.AppearanceGroup.Options.UseFont = true;
            resources.ApplyResources(this.lcgExchangeRate, "lcgExchangeRate");
            this.lcgExchangeRate.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcExchangeRate,
            this.emptySpaceItem4});
            this.lcgExchangeRate.Location = new System.Drawing.Point(0, 123);
            this.lcgExchangeRate.Name = "lcgExchangeRate";
            this.lcgExchangeRate.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 2);
            this.lcgExchangeRate.Size = new System.Drawing.Size(413, 53);
            // 
            // lcExchangeRate
            // 
            this.lcExchangeRate.AllowHtmlStringInCaption = true;
            this.lcExchangeRate.Control = this.txtExchangeRate;
            resources.ApplyResources(this.lcExchangeRate, "lcExchangeRate");
            this.lcExchangeRate.Location = new System.Drawing.Point(0, 0);
            this.lcExchangeRate.MaxSize = new System.Drawing.Size(226, 24);
            this.lcExchangeRate.MinSize = new System.Drawing.Size(226, 24);
            this.lcExchangeRate.Name = "lcExchangeRate";
            this.lcExchangeRate.Size = new System.Drawing.Size(226, 24);
            this.lcExchangeRate.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcExchangeRate.TextSize = new System.Drawing.Size(79, 13);
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem4, "emptySpaceItem4");
            this.emptySpaceItem4.Location = new System.Drawing.Point(226, 0);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(177, 24);
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmCountry
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.lcCountry);
            this.MaximizeBox = false;
            this.Name = "frmCountry";
            this.Load += new System.EventHandler(this.frmCountry_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lcCountry)).EndInit();
            this.lcCountry.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtExchangeRate.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSymbols.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCountryCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCountry.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCountry)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCountryCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgExchangeRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcExchangeRate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            this.ResumeLayout(false);

		}

		#endregion

        private DevExpress.XtraLayout.LayoutControl lcCountry;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup2;
        private DevExpress.XtraEditors.TextEdit txtCountryCode;
        private DevExpress.XtraEditors.TextEdit txtCountry;
        private DevExpress.XtraLayout.LayoutControlItem lblCountry;
        private DevExpress.XtraLayout.LayoutControlItem lblCountryCode;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraLayout.LayoutControlItem lblCode;
        private DevExpress.XtraEditors.TextEdit txtSymbols;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraEditors.TextEdit txtName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.TextEdit txtExchangeRate;
        private DevExpress.XtraLayout.LayoutControlItem lcExchangeRate;
        private DevExpress.XtraLayout.LayoutControlGroup lcgExchangeRate;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
	}
}