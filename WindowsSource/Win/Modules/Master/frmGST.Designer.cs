namespace ACPP.Modules.Master
{
    partial class frmGST
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
            this.txtSGSt = new DevExpress.XtraEditors.TextEdit();
            this.chkGSTActive = new DevExpress.XtraEditors.CheckEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.dtApplicableFrom = new DevExpress.XtraEditors.DateEdit();
            this.txtCGSt = new DevExpress.XtraEditors.TextEdit();
            this.txtGSt = new DevExpress.XtraEditors.TextEdit();
            this.txtGStSlab = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblSlab = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblGSt = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblCGST = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem5 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem6 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblApplicableFrom = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem4 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblSGST = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem5 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtSGSt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkGSTActive.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtApplicableFrom.Properties.CalendarTimeProperties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtApplicableFrom.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCGSt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGSt.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGStSlab.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSlab)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblGSt)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCGST)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblApplicableFrom)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSGST)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtSGSt);
            this.layoutControl1.Controls.Add(this.chkGSTActive);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.dtApplicableFrom);
            this.layoutControl1.Controls.Add(this.txtCGSt);
            this.layoutControl1.Controls.Add(this.txtGSt);
            this.layoutControl1.Controls.Add(this.txtGStSlab);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(5, 5);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(642, 65, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(344, 179);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtSGSt
            // 
            this.txtSGSt.Location = new System.Drawing.Point(90, 81);
            this.txtSGSt.Name = "txtSGSt";
            this.txtSGSt.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtSGSt.Properties.MaxLength = 13;
            this.txtSGSt.Size = new System.Drawing.Size(127, 20);
            this.txtSGSt.StyleController = this.layoutControl1;
            this.txtSGSt.TabIndex = 11;
            // 
            // chkGSTActive
            // 
            this.chkGSTActive.EditValue = true;
            this.chkGSTActive.Location = new System.Drawing.Point(2, 132);
            this.chkGSTActive.Name = "chkGSTActive";
            this.chkGSTActive.Properties.Caption = "Active";
            this.chkGSTActive.Size = new System.Drawing.Size(340, 19);
            this.chkGSTActive.StyleController = this.layoutControl1;
            this.chkGSTActive.TabIndex = 10;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(179, 155);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(80, 22);
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Location = new System.Drawing.Point(263, 155);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(79, 22);
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.TabIndex = 8;
            this.btnCancel.Text = "&Cancel";
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // dtApplicableFrom
            // 
            this.dtApplicableFrom.EditValue = null;
            this.dtApplicableFrom.Location = new System.Drawing.Point(90, 107);
            this.dtApplicableFrom.Name = "dtApplicableFrom";
            this.dtApplicableFrom.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.dtApplicableFrom.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtApplicableFrom.Properties.CalendarTimeProperties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.dtApplicableFrom.Properties.CalendarTimeProperties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.dtApplicableFrom.Properties.Mask.MaskType = DevExpress.XtraEditors.Mask.MaskType.DateTimeAdvancingCaret;
            this.dtApplicableFrom.Size = new System.Drawing.Size(127, 20);
            this.dtApplicableFrom.StyleController = this.layoutControl1;
            this.dtApplicableFrom.TabIndex = 7;
            // 
            // txtCGSt
            // 
            this.txtCGSt.Location = new System.Drawing.Point(90, 55);
            this.txtCGSt.Name = "txtCGSt";
            this.txtCGSt.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtCGSt.Properties.MaxLength = 13;
            this.txtCGSt.Size = new System.Drawing.Size(126, 20);
            this.txtCGSt.StyleController = this.layoutControl1;
            this.txtCGSt.TabIndex = 6;
            // 
            // txtGSt
            // 
            this.txtGSt.Location = new System.Drawing.Point(90, 29);
            this.txtGSt.Name = "txtGSt";
            this.txtGSt.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtGSt.Properties.MaxLength = 13;
            this.txtGSt.Size = new System.Drawing.Size(127, 20);
            this.txtGSt.StyleController = this.layoutControl1;
            this.txtGSt.TabIndex = 5;
            // 
            // txtGStSlab
            // 
            this.txtGStSlab.Location = new System.Drawing.Point(90, 3);
            this.txtGStSlab.Name = "txtGStSlab";
            this.txtGStSlab.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtGStSlab.Properties.MaxLength = 50;
            this.txtGStSlab.Size = new System.Drawing.Size(254, 20);
            this.txtGStSlab.StyleController = this.layoutControl1;
            this.txtGStSlab.TabIndex = 4;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "Root";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblSlab,
            this.lblGSt,
            this.lblCGST,
            this.layoutControlItem5,
            this.layoutControlItem6,
            this.emptySpaceItem1,
            this.emptySpaceItem2,
            this.emptySpaceItem3,
            this.lblApplicableFrom,
            this.emptySpaceItem4,
            this.layoutControlItem1,
            this.lblSGST,
            this.emptySpaceItem5});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(344, 179);
            this.layoutControlGroup1.Text = "Root";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lblSlab
            // 
            this.lblSlab.Control = this.txtGStSlab;
            this.lblSlab.CustomizationFormText = "Slab";
            this.lblSlab.Location = new System.Drawing.Point(0, 0);
            this.lblSlab.Name = "lblSlab";
            this.lblSlab.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblSlab.Size = new System.Drawing.Size(344, 26);
            this.lblSlab.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 3);
            this.lblSlab.Text = "Slab";
            this.lblSlab.TextSize = new System.Drawing.Size(87, 13);
            // 
            // lblGSt
            // 
            this.lblGSt.AllowHtmlStringInCaption = true;
            this.lblGSt.Control = this.txtGSt;
            this.lblGSt.CustomizationFormText = "GST";
            this.lblGSt.Location = new System.Drawing.Point(0, 26);
            this.lblGSt.Name = "lblGSt";
            this.lblGSt.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblGSt.Size = new System.Drawing.Size(217, 26);
            this.lblGSt.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 3);
            this.lblGSt.Text = "GST <color=\"red\"> *";
            this.lblGSt.TextSize = new System.Drawing.Size(87, 13);
            // 
            // lblCGST
            // 
            this.lblCGST.Control = this.txtCGSt;
            this.lblCGST.CustomizationFormText = "CGST";
            this.lblCGST.Location = new System.Drawing.Point(0, 52);
            this.lblCGST.Name = "lblCGST";
            this.lblCGST.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblCGST.Size = new System.Drawing.Size(216, 26);
            this.lblCGST.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 3);
            this.lblCGST.Text = "CGST";
            this.lblCGST.TextSize = new System.Drawing.Size(87, 13);
            // 
            // layoutControlItem5
            // 
            this.layoutControlItem5.Control = this.btnCancel;
            this.layoutControlItem5.CustomizationFormText = "layoutControlItem5";
            this.layoutControlItem5.Location = new System.Drawing.Point(261, 153);
            this.layoutControlItem5.Name = "layoutControlItem5";
            this.layoutControlItem5.Size = new System.Drawing.Size(83, 26);
            this.layoutControlItem5.Text = "layoutControlItem5";
            this.layoutControlItem5.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem5.TextToControlDistance = 0;
            this.layoutControlItem5.TextVisible = false;
            // 
            // layoutControlItem6
            // 
            this.layoutControlItem6.Control = this.btnSave;
            this.layoutControlItem6.CustomizationFormText = "layoutControlItem6";
            this.layoutControlItem6.Location = new System.Drawing.Point(177, 153);
            this.layoutControlItem6.Name = "layoutControlItem6";
            this.layoutControlItem6.Size = new System.Drawing.Size(84, 26);
            this.layoutControlItem6.Text = "layoutControlItem6";
            this.layoutControlItem6.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem6.TextToControlDistance = 0;
            this.layoutControlItem6.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 153);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(177, 26);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(217, 26);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(127, 26);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(216, 52);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(128, 26);
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblApplicableFrom
            // 
            this.lblApplicableFrom.AllowHtmlStringInCaption = true;
            this.lblApplicableFrom.Control = this.dtApplicableFrom;
            this.lblApplicableFrom.CustomizationFormText = "SGST";
            this.lblApplicableFrom.Location = new System.Drawing.Point(0, 104);
            this.lblApplicableFrom.Name = "lblApplicableFrom";
            this.lblApplicableFrom.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 3);
            this.lblApplicableFrom.Size = new System.Drawing.Size(217, 26);
            this.lblApplicableFrom.Text = "Applicable From <color=\"red\"> *";
            this.lblApplicableFrom.TextSize = new System.Drawing.Size(87, 13);
            // 
            // emptySpaceItem4
            // 
            this.emptySpaceItem4.AllowHotTrack = false;
            this.emptySpaceItem4.CustomizationFormText = "emptySpaceItem4";
            this.emptySpaceItem4.Location = new System.Drawing.Point(217, 104);
            this.emptySpaceItem4.Name = "emptySpaceItem4";
            this.emptySpaceItem4.Size = new System.Drawing.Size(127, 26);
            this.emptySpaceItem4.Text = "emptySpaceItem4";
            this.emptySpaceItem4.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.chkGSTActive;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 130);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(344, 23);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // lblSGST
            // 
            this.lblSGST.AllowHtmlStringInCaption = true;
            this.lblSGST.Control = this.txtSGSt;
            this.lblSGST.CustomizationFormText = "Applicable From <color=\"red\"> *";
            this.lblSGST.Location = new System.Drawing.Point(0, 78);
            this.lblSGST.Name = "lblSGST";
            this.lblSGST.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblSGST.Size = new System.Drawing.Size(217, 26);
            this.lblSGST.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 3);
            this.lblSGST.Text = "SGST";
            this.lblSGST.TextSize = new System.Drawing.Size(87, 13);
            // 
            // emptySpaceItem5
            // 
            this.emptySpaceItem5.AllowHotTrack = false;
            this.emptySpaceItem5.CustomizationFormText = "emptySpaceItem5";
            this.emptySpaceItem5.Location = new System.Drawing.Point(217, 78);
            this.emptySpaceItem5.Name = "emptySpaceItem5";
            this.emptySpaceItem5.Size = new System.Drawing.Size(127, 26);
            this.emptySpaceItem5.Text = "emptySpaceItem5";
            this.emptySpaceItem5.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmGST
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.ClientSize = new System.Drawing.Size(354, 189);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmGST";
            this.Text = "GST";
            this.Load += new System.EventHandler(this.frmGST_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtSGSt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chkGSTActive.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtApplicableFrom.Properties.CalendarTimeProperties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dtApplicableFrom.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCGSt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGSt.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtGStSlab.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSlab)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblGSt)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCGST)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblApplicableFrom)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSGST)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem5)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.DateEdit dtApplicableFrom;
        private DevExpress.XtraEditors.TextEdit txtCGSt;
        private DevExpress.XtraEditors.TextEdit txtGSt;
        private DevExpress.XtraEditors.TextEdit txtGStSlab;
        private DevExpress.XtraLayout.LayoutControlItem lblSlab;
        private DevExpress.XtraLayout.LayoutControlItem lblGSt;
        private DevExpress.XtraLayout.LayoutControlItem lblCGST;
        private DevExpress.XtraLayout.LayoutControlItem lblApplicableFrom;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem5;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem6;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem4;
        private DevExpress.XtraEditors.CheckEdit chkGSTActive;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.TextEdit txtSGSt;
        private DevExpress.XtraLayout.LayoutControlItem lblSGST;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem5;
    }
}