namespace ACPP.Modules.Master
{
    partial class frmLedgerAdd
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
            DevExpress.Utils.SuperToolTip superToolTip1 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem1 = new DevExpress.Utils.ToolTipTitleItem();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLedgerAdd));
            DevExpress.Utils.ToolTipItem toolTipItem1 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip2 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem2 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem2 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip3 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem3 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem3 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip4 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem4 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem4 = new DevExpress.Utils.ToolTipItem();
            DevExpress.Utils.SuperToolTip superToolTip5 = new DevExpress.Utils.SuperToolTip();
            DevExpress.Utils.ToolTipTitleItem toolTipTitleItem5 = new DevExpress.Utils.ToolTipTitleItem();
            DevExpress.Utils.ToolTipItem toolTipItem5 = new DevExpress.Utils.ToolTipItem();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.txtLedgerCode = new DevExpress.XtraEditors.TextEdit();
            this.lkpLedgerGroup = new DevExpress.XtraEditors.LookUpEdit();
            this.txtLedgerName = new DevExpress.XtraEditors.TextEdit();
            this.lcgLedgerGroup = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblCode = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblName = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblLedgerGroup = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtLedgerCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpLedgerGroup.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLedgerName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgLedgerGroup)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLedgerGroup)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.txtLedgerCode);
            this.layoutControl1.Controls.Add(this.lkpLedgerGroup);
            this.layoutControl1.Controls.Add(this.txtLedgerName);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(5, 5);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(311, 219, 250, 350);
            this.layoutControl1.Root = this.lcgLedgerGroup;
            this.layoutControl1.Size = new System.Drawing.Size(307, 102);
            this.layoutControl1.TabIndex = 1;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Image = global::ACPP.Properties.Resources.Close;
            this.btnClose.Location = new System.Drawing.Point(240, 76);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(65, 22);
            this.btnClose.StyleController = this.layoutControl1;
            toolTipTitleItem1.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image")));
            toolTipTitleItem1.Appearance.Options.UseImage = true;
            toolTipTitleItem1.Image = ((System.Drawing.Image)(resources.GetObject("toolTipTitleItem1.Image")));
            toolTipTitleItem1.Text = "Close (Clrt+C)";
            toolTipItem1.LeftIndent = 6;
            toolTipItem1.Text = "        Click on this to close Ledger Group";
            superToolTip1.Items.Add(toolTipTitleItem1);
            superToolTip1.Items.Add(toolTipItem1);
            this.btnClose.SuperTip = superToolTip1;
            this.btnClose.TabIndex = 7;
            this.btnClose.Text = "&Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // btnSave
            // 
            this.btnSave.Image = global::ACPP.Properties.Resources.TickMark1;
            this.btnSave.Location = new System.Drawing.Point(171, 76);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(65, 22);
            this.btnSave.StyleController = this.layoutControl1;
            toolTipTitleItem2.Appearance.Image = ((System.Drawing.Image)(resources.GetObject("resource.Image1")));
            toolTipTitleItem2.Appearance.Options.UseImage = true;
            toolTipTitleItem2.Image = ((System.Drawing.Image)(resources.GetObject("toolTipTitleItem2.Image")));
            toolTipTitleItem2.Text = "Save (Ctrl+S)";
            toolTipItem2.LeftIndent = 6;
            toolTipItem2.Text = "        Click on this to save Ledger Group Details";
            superToolTip2.Items.Add(toolTipTitleItem2);
            superToolTip2.Items.Add(toolTipItem2);
            this.btnSave.SuperTip = superToolTip2;
            this.btnSave.TabIndex = 8;
            this.btnSave.Text = "&Save";
            // 
            // txtLedgerCode
            // 
            this.txtLedgerCode.Location = new System.Drawing.Point(45, 5);
            this.txtLedgerCode.Name = "txtLedgerCode";
            this.txtLedgerCode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtLedgerCode.Properties.MaxLength = 5;
            this.txtLedgerCode.Size = new System.Drawing.Size(91, 20);
            this.txtLedgerCode.StyleController = this.layoutControl1;
            toolTipTitleItem3.Text = "Code";
            toolTipItem3.LeftIndent = 6;
            toolTipItem3.Text = "Sub Group Code";
            superToolTip3.Items.Add(toolTipTitleItem3);
            superToolTip3.Items.Add(toolTipItem3);
            this.txtLedgerCode.SuperTip = superToolTip3;
            this.txtLedgerCode.TabIndex = 4;
            // 
            // lkpLedgerGroup
            // 
            this.lkpLedgerGroup.Location = new System.Drawing.Point(45, 51);
            this.lkpLedgerGroup.Name = "lkpLedgerGroup";
            this.lkpLedgerGroup.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.lkpLedgerGroup.Properties.NullText = "";
            this.lkpLedgerGroup.Properties.PopupFormMinSize = new System.Drawing.Size(10, 0);
            this.lkpLedgerGroup.Properties.ShowFooter = false;
            this.lkpLedgerGroup.Properties.ShowHeader = false;
            this.lkpLedgerGroup.Size = new System.Drawing.Size(262, 20);
            this.lkpLedgerGroup.StyleController = this.layoutControl1;
            toolTipTitleItem4.Text = "Group";
            toolTipItem4.LeftIndent = 6;
            toolTipItem4.Text = "  Parent Group";
            superToolTip4.Items.Add(toolTipTitleItem4);
            superToolTip4.Items.Add(toolTipItem4);
            this.lkpLedgerGroup.SuperTip = superToolTip4;
            this.lkpLedgerGroup.TabIndex = 9;
            this.lkpLedgerGroup.EditValueChanged += new System.EventHandler(this.lkpLedgerGroup_EditValueChanged);
            // 
            // txtLedgerName
            // 
            this.txtLedgerName.Location = new System.Drawing.Point(45, 28);
            this.txtLedgerName.Name = "txtLedgerName";
            this.txtLedgerName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtLedgerName.Properties.MaxLength = 30;
            this.txtLedgerName.Size = new System.Drawing.Size(262, 20);
            this.txtLedgerName.StyleController = this.layoutControl1;
            toolTipTitleItem5.Text = "Name";
            toolTipItem5.LeftIndent = 6;
            toolTipItem5.Text = "  Sub Group Name";
            superToolTip5.Items.Add(toolTipTitleItem5);
            superToolTip5.Items.Add(toolTipItem5);
            this.txtLedgerName.SuperTip = superToolTip5;
            this.txtLedgerName.TabIndex = 5;
            // 
            // lcgLedgerGroup
            // 
            this.lcgLedgerGroup.CustomizationFormText = "lcgLedgerGroup";
            this.lcgLedgerGroup.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgLedgerGroup.GroupBordersVisible = false;
            this.lcgLedgerGroup.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblCode,
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.lblName,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.lblLedgerGroup});
            this.lcgLedgerGroup.Location = new System.Drawing.Point(0, 0);
            this.lcgLedgerGroup.Name = "lcgLedgerGroup";
            this.lcgLedgerGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgLedgerGroup.Size = new System.Drawing.Size(307, 102);
            this.lcgLedgerGroup.Text = "lcgLedgerGroup";
            this.lcgLedgerGroup.TextVisible = false;
            // 
            // lblCode
            // 
            this.lblCode.AllowHtmlStringInCaption = true;
            this.lblCode.Control = this.txtLedgerCode;
            this.lblCode.CustomizationFormText = "Code <Color=Red><b>*</b>";
            this.lblCode.Location = new System.Drawing.Point(0, 0);
            this.lblCode.Name = "lblCode";
            this.lblCode.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblCode.Size = new System.Drawing.Size(136, 28);
            this.lblCode.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 5, 3);
            this.lblCode.Text = "Code <Color=Red>*";
            this.lblCode.TextSize = new System.Drawing.Size(41, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 74);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(169, 28);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(136, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(171, 28);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblName
            // 
            this.lblName.AllowHtmlStringInCaption = true;
            this.lblName.Control = this.txtLedgerName;
            this.lblName.CustomizationFormText = "Name <Color=Red>*";
            this.lblName.Location = new System.Drawing.Point(0, 28);
            this.lblName.Name = "lblName";
            this.lblName.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblName.Size = new System.Drawing.Size(307, 23);
            this.lblName.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblName.Text = "Name <Color=Red>*";
            this.lblName.TextSize = new System.Drawing.Size(41, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnClose;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(238, 74);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(69, 28);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnSave;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(169, 74);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(69, 28);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // lblLedgerGroup
            // 
            this.lblLedgerGroup.AllowHtmlStringInCaption = true;
            this.lblLedgerGroup.Control = this.lkpLedgerGroup;
            this.lblLedgerGroup.CustomizationFormText = "Under the Group  <Color=Red>*";
            this.lblLedgerGroup.Location = new System.Drawing.Point(0, 51);
            this.lblLedgerGroup.Name = "lblLedgerGroup";
            this.lblLedgerGroup.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblLedgerGroup.Size = new System.Drawing.Size(307, 23);
            this.lblLedgerGroup.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblLedgerGroup.Text = "Group  <Color=Red>*";
            this.lblLedgerGroup.TextSize = new System.Drawing.Size(41, 13);
            // 
            // frmLedgerAdd
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(317, 112);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmLedgerAdd";
            this.Text = "A/C Ledger ";
            this.Load += new System.EventHandler(this.frmLedgerAdd_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtLedgerCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lkpLedgerGroup.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtLedgerName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgLedgerGroup)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblLedgerGroup)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.LookUpEdit lkpLedgerGroup;
        private DevExpress.XtraEditors.TextEdit txtLedgerName;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.TextEdit txtLedgerCode;
        private DevExpress.XtraLayout.LayoutControlGroup lcgLedgerGroup;
        private DevExpress.XtraLayout.LayoutControlItem lblCode;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem lblName;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.LayoutControlItem lblLedgerGroup;
    }
}