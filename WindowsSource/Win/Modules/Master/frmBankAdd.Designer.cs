namespace ACPP
{
    partial class frmBankAdd
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
            this.lcBankAdd = new DevExpress.XtraLayout.LayoutControl();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.meAddress = new DevExpress.XtraEditors.MemoEdit();
            this.txtBranch = new DevExpress.XtraEditors.TextEdit();
            this.txtBankName = new DevExpress.XtraEditors.TextEdit();
            this.txtBankCode = new DevExpress.XtraEditors.TextEdit();
            this.lcgBankAdd = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblBankCode = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblBankName = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblBankBranch = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblAddress = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.lcBankAdd)).BeginInit();
            this.lcBankAdd.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.meAddress.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBranch.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBankName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBankCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgBankAdd)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBankCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBankName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBankBranch)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAddress)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            this.SuspendLayout();
            // 
            // lblResponseMsg
            // 
            //this.lblResponseMsg.Appearance.ForeColor = System.Drawing.Color.Red;
            this.lblResponseMsg.Location = new System.Drawing.Point(280, 6);
            // 
            // lcBankAdd
            // 
            this.lcBankAdd.Controls.Add(this.btnSave);
            this.lcBankAdd.Controls.Add(this.btnClose);
            this.lcBankAdd.Controls.Add(this.meAddress);
            this.lcBankAdd.Controls.Add(this.txtBranch);
            this.lcBankAdd.Controls.Add(this.txtBankName);
            this.lcBankAdd.Controls.Add(this.txtBankCode);
            this.lcBankAdd.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lcBankAdd.Location = new System.Drawing.Point(5, 26);
            this.lcBankAdd.Name = "lcBankAdd";
            this.lcBankAdd.Root = this.lcgBankAdd;
            this.lcBankAdd.Size = new System.Drawing.Size(337, 159);
            this.lcBankAdd.TabIndex = 0;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(184, 135);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(69, 22);
            this.btnSave.StyleController = this.lcBankAdd;
            this.btnSave.TabIndex = 9;
            this.btnSave.Text = "&Save";
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(267, 135);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(68, 22);
            this.btnClose.StyleController = this.lcBankAdd;
            this.btnClose.TabIndex = 8;
            this.btnClose.Text = "&Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // meAddress
            // 
            this.meAddress.Location = new System.Drawing.Point(69, 72);
            this.meAddress.Name = "meAddress";
            this.meAddress.Properties.MaxLength = 100;
            this.meAddress.Properties.ScrollBars = System.Windows.Forms.ScrollBars.None;
            this.meAddress.Size = new System.Drawing.Size(268, 58);
            this.meAddress.StyleController = this.lcBankAdd;
            this.meAddress.TabIndex = 7;
            // 
            // txtBranch
            // 
            this.txtBranch.Location = new System.Drawing.Point(69, 49);
            this.txtBranch.Name = "txtBranch";
            this.txtBranch.Properties.MaxLength = 50;
            this.txtBranch.Size = new System.Drawing.Size(268, 20);
            this.txtBranch.StyleController = this.lcBankAdd;
            this.txtBranch.TabIndex = 6;
            // 
            // txtBankName
            // 
            this.txtBankName.Location = new System.Drawing.Point(69, 26);
            this.txtBankName.Name = "txtBankName";
            this.txtBankName.Properties.MaxLength = 50;
            this.txtBankName.Size = new System.Drawing.Size(268, 20);
            this.txtBankName.StyleController = this.lcBankAdd;
            this.txtBankName.TabIndex = 5;
            // 
            // txtBankCode
            // 
            this.txtBankCode.Location = new System.Drawing.Point(69, 3);
            this.txtBankCode.Name = "txtBankCode";
            this.txtBankCode.Properties.MaxLength = 15;
            this.txtBankCode.Size = new System.Drawing.Size(83, 20);
            this.txtBankCode.StyleController = this.lcBankAdd;
            this.txtBankCode.TabIndex = 4;
            // 
            // lcgBankAdd
            // 
            this.lcgBankAdd.CustomizationFormText = "layoutControlGroup1";
            this.lcgBankAdd.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgBankAdd.GroupBordersVisible = false;
            this.lcgBankAdd.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblBankCode,
            this.emptySpaceItem1,
            this.lblBankName,
            this.lblBankBranch,
            this.lblAddress,
            this.emptySpaceItem2,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.emptySpaceItem3});
            this.lcgBankAdd.Location = new System.Drawing.Point(0, 0);
            this.lcgBankAdd.Name = "lcgBankAdd";
            this.lcgBankAdd.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgBankAdd.Size = new System.Drawing.Size(337, 159);
            this.lcgBankAdd.Text = "lcgBankAdd";
            this.lcgBankAdd.TextVisible = false;
            // 
            // lblBankCode
            // 
            this.lblBankCode.AllowHtmlStringInCaption = true;
            this.lblBankCode.Control = this.txtBankCode;
            this.lblBankCode.CustomizationFormText = "layoutControlItem1";
            this.lblBankCode.Location = new System.Drawing.Point(0, 0);
            this.lblBankCode.Name = "lblBankCode";
            this.lblBankCode.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblBankCode.Size = new System.Drawing.Size(152, 26);
            this.lblBankCode.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 3, 3);
            this.lblBankCode.Text = "Code <Color=Red>*";
            this.lblBankCode.TextSize = new System.Drawing.Size(65, 13);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 133);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(182, 26);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblBankName
            // 
            this.lblBankName.AllowHtmlStringInCaption = true;
            this.lblBankName.Control = this.txtBankName;
            this.lblBankName.CustomizationFormText = "layoutControlItem2";
            this.lblBankName.Location = new System.Drawing.Point(0, 26);
            this.lblBankName.Name = "lblBankName";
            this.lblBankName.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblBankName.Size = new System.Drawing.Size(337, 23);
            this.lblBankName.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblBankName.Text = "Bank Name  <Color=Red>*";
            this.lblBankName.TextSize = new System.Drawing.Size(65, 13);
            // 
            // lblBankBranch
            // 
            this.lblBankBranch.AllowHtmlStringInCaption = true;
            this.lblBankBranch.Control = this.txtBranch;
            this.lblBankBranch.CustomizationFormText = "Branch  <Color=Red>*";
            this.lblBankBranch.Location = new System.Drawing.Point(0, 49);
            this.lblBankBranch.Name = "lblBankBranch";
            this.lblBankBranch.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblBankBranch.Size = new System.Drawing.Size(337, 23);
            this.lblBankBranch.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblBankBranch.Text = "Branch  <Color=Red>*";
            this.lblBankBranch.TextSize = new System.Drawing.Size(65, 13);
            // 
            // lblAddress
            // 
            this.lblAddress.Control = this.meAddress;
            this.lblAddress.CustomizationFormText = "Address";
            this.lblAddress.Location = new System.Drawing.Point(0, 72);
            this.lblAddress.Name = "lblAddress";
            this.lblAddress.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lblAddress.Size = new System.Drawing.Size(337, 61);
            this.lblAddress.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblAddress.Text = "Address";
            this.lblAddress.TextSize = new System.Drawing.Size(65, 13);
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(152, 0);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(185, 26);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnClose;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(265, 133);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(72, 26);
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnSave;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(182, 133);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(73, 26);
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(255, 133);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(10, 26);
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmBankAdd
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(347, 190);
            this.Controls.Add(this.lcBankAdd);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.MaximizeBox = false;
            this.Name = "frmBankAdd";
            this.Padding = new System.Windows.Forms.Padding(5);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Bank Add";
            this.Load += new System.EventHandler(this.frmBankAdd_Load);
            this.Controls.SetChildIndex(this.lcBankAdd, 0);
            ((System.ComponentModel.ISupportInitialize)(this.lcBankAdd)).EndInit();
            this.lcBankAdd.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.meAddress.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBranch.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBankName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtBankCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcgBankAdd)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBankCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBankName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblBankBranch)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblAddress)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lcBankAdd;
        private DevExpress.XtraLayout.LayoutControlGroup lcgBankAdd;
        private DevExpress.XtraEditors.TextEdit txtBranch;
        private DevExpress.XtraEditors.TextEdit txtBankName;
        private DevExpress.XtraEditors.TextEdit txtBankCode;
        private DevExpress.XtraLayout.LayoutControlItem lblBankCode;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem lblBankName;
        private DevExpress.XtraLayout.LayoutControlItem lblBankBranch;
        private DevExpress.XtraEditors.MemoEdit meAddress;
        private DevExpress.XtraLayout.LayoutControlItem lblAddress;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
    }
}