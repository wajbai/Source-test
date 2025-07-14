namespace ACPP.Modules.Asset.Masters
{
    partial class frmUnitofMeasure
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
            this.txtFormalName = new DevExpress.XtraEditors.TextEdit();
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.txtSymbol = new DevExpress.XtraEditors.TextEdit();
            this.txtCode = new DevExpress.XtraEditors.TextEdit();
            this.txtNoDecimalPlace = new DevExpress.XtraEditors.TextEdit();
            this.cmbType = new DevExpress.XtraEditors.ComboBoxEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblNumberofdecimalplace = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblCode = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblSymbol = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblType = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem2 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem3 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblFormalName = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFormalName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSymbol.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNoDecimalPlace.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNumberofdecimalplace)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCode)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSymbol)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblType)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFormalName)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtFormalName);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.txtSymbol);
            this.layoutControl1.Controls.Add(this.txtCode);
            this.layoutControl1.Controls.Add(this.txtNoDecimalPlace);
            this.layoutControl1.Controls.Add(this.cmbType);
            this.layoutControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.layoutControl1.Location = new System.Drawing.Point(5, 5);
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(145, 503, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            this.layoutControl1.Size = new System.Drawing.Size(271, 148);
            this.layoutControl1.TabIndex = 0;
            this.layoutControl1.Text = "layoutControl1";
            // 
            // txtFormalName
            // 
            this.txtFormalName.Location = new System.Drawing.Point(76, 71);
            this.txtFormalName.Name = "txtFormalName";
            this.txtFormalName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtFormalName.Size = new System.Drawing.Size(195, 20);
            this.txtFormalName.StyleController = this.layoutControl1;
            this.txtFormalName.TabIndex = 11;
            // 
            // btnSave
            // 
            this.btnSave.Location = new System.Drawing.Point(135, 119);
            this.btnSave.Name = "btnSave";
            this.btnSave.Size = new System.Drawing.Size(65, 22);
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.TabIndex = 10;
            this.btnSave.Text = "&Save";
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnClose.Location = new System.Drawing.Point(204, 119);
            this.btnClose.Name = "btnClose";
            this.btnClose.Size = new System.Drawing.Size(65, 22);
            this.btnClose.StyleController = this.layoutControl1;
            this.btnClose.TabIndex = 9;
            this.btnClose.Text = "&Close";
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtSymbol
            // 
            this.txtSymbol.Location = new System.Drawing.Point(76, 48);
            this.txtSymbol.Name = "txtSymbol";
            this.txtSymbol.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtSymbol.Size = new System.Drawing.Size(195, 20);
            this.txtSymbol.StyleController = this.layoutControl1;
            this.txtSymbol.TabIndex = 7;
            // 
            // txtCode
            // 
            this.txtCode.Location = new System.Drawing.Point(76, 2);
            this.txtCode.Name = "txtCode";
            this.txtCode.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtCode.Size = new System.Drawing.Size(58, 20);
            this.txtCode.StyleController = this.layoutControl1;
            this.txtCode.TabIndex = 6;
            // 
            // txtNoDecimalPlace
            // 
            this.txtNoDecimalPlace.Location = new System.Drawing.Point(76, 94);
            this.txtNoDecimalPlace.Name = "txtNoDecimalPlace";
            this.txtNoDecimalPlace.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtNoDecimalPlace.Size = new System.Drawing.Size(60, 20);
            this.txtNoDecimalPlace.StyleController = this.layoutControl1;
            this.txtNoDecimalPlace.TabIndex = 4;
            // 
            // cmbType
            // 
            this.cmbType.Location = new System.Drawing.Point(76, 25);
            this.cmbType.Name = "cmbType";
            this.cmbType.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.cmbType.Properties.Buttons.AddRange(new DevExpress.XtraEditors.Controls.EditorButton[] {
            new DevExpress.XtraEditors.Controls.EditorButton(DevExpress.XtraEditors.Controls.ButtonPredefines.Combo)});
            this.cmbType.Size = new System.Drawing.Size(195, 20);
            this.cmbType.StyleController = this.layoutControl1;
            this.cmbType.TabIndex = 5;
            // 
            // layoutControlGroup1
            // 
            this.layoutControlGroup1.CustomizationFormText = "layoutControlGroup1";
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblNumberofdecimalplace,
            this.lblCode,
            this.lblSymbol,
            this.lblType,
            this.layoutControlItem1,
            this.emptySpaceItem2,
            this.emptySpaceItem1,
            this.layoutControlItem2,
            this.emptySpaceItem3,
            this.lblFormalName});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "layoutControlGroup1";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(271, 148);
            this.layoutControlGroup1.Text = "layoutControlGroup1";
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lblNumberofdecimalplace
            // 
            this.lblNumberofdecimalplace.AllowHtmlStringInCaption = true;
            this.lblNumberofdecimalplace.Control = this.txtNoDecimalPlace;
            this.lblNumberofdecimalplace.CustomizationFormText = "Decimal Place ";
            this.lblNumberofdecimalplace.Location = new System.Drawing.Point(0, 94);
            this.lblNumberofdecimalplace.Name = "lblNumberofdecimalplace";
            this.lblNumberofdecimalplace.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblNumberofdecimalplace.Size = new System.Drawing.Size(136, 23);
            this.lblNumberofdecimalplace.Text = "Decimal Place <color=red>*";
            this.lblNumberofdecimalplace.TextSize = new System.Drawing.Size(73, 13);
            // 
            // lblCode
            // 
            this.lblCode.AllowHtmlStringInCaption = true;
            this.lblCode.Control = this.txtCode;
            this.lblCode.CustomizationFormText = "Code <color=red>*";
            this.lblCode.Location = new System.Drawing.Point(0, 0);
            this.lblCode.Name = "lblCode";
            this.lblCode.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 2, 0, 1);
            this.lblCode.Size = new System.Drawing.Size(136, 25);
            this.lblCode.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 2, 2);
            this.lblCode.Text = "Code ";
            this.lblCode.TextSize = new System.Drawing.Size(73, 13);
            // 
            // lblSymbol
            // 
            this.lblSymbol.AllowHtmlStringInCaption = true;
            this.lblSymbol.Control = this.txtSymbol;
            this.lblSymbol.CustomizationFormText = "Symbol <color=red>*";
            this.lblSymbol.Location = new System.Drawing.Point(0, 48);
            this.lblSymbol.Name = "lblSymbol";
            this.lblSymbol.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblSymbol.Size = new System.Drawing.Size(271, 23);
            this.lblSymbol.Text = "Symbol <color=red>*";
            this.lblSymbol.TextSize = new System.Drawing.Size(73, 13);
            // 
            // lblType
            // 
            this.lblType.AllowHtmlStringInCaption = true;
            this.lblType.Control = this.cmbType;
            this.lblType.CustomizationFormText = "Type <color=red>*";
            this.lblType.Location = new System.Drawing.Point(0, 25);
            this.lblType.Name = "lblType";
            this.lblType.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblType.Size = new System.Drawing.Size(271, 23);
            this.lblType.Text = "Type <color=red>*";
            this.lblType.TextSize = new System.Drawing.Size(73, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnClose;
            this.layoutControlItem1.CustomizationFormText = "layoutControlItem1";
            this.layoutControlItem1.Location = new System.Drawing.Point(202, 117);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(69, 26);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(69, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(69, 31);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.Text = "layoutControlItem1";
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem2
            // 
            this.emptySpaceItem2.AllowHotTrack = false;
            this.emptySpaceItem2.CustomizationFormText = "emptySpaceItem2";
            this.emptySpaceItem2.Location = new System.Drawing.Point(0, 117);
            this.emptySpaceItem2.Name = "emptySpaceItem2";
            this.emptySpaceItem2.Size = new System.Drawing.Size(133, 31);
            this.emptySpaceItem2.Text = "emptySpaceItem2";
            this.emptySpaceItem2.TextSize = new System.Drawing.Size(0, 0);
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            this.emptySpaceItem1.CustomizationFormText = "emptySpaceItem1";
            this.emptySpaceItem1.Location = new System.Drawing.Point(136, 0);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(135, 25);
            this.emptySpaceItem1.Text = "emptySpaceItem1";
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnSave;
            this.layoutControlItem2.CustomizationFormText = "layoutControlItem2";
            this.layoutControlItem2.Location = new System.Drawing.Point(133, 117);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(69, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(69, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(69, 31);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.Text = "layoutControlItem2";
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // emptySpaceItem3
            // 
            this.emptySpaceItem3.AllowHotTrack = false;
            this.emptySpaceItem3.CustomizationFormText = "emptySpaceItem3";
            this.emptySpaceItem3.Location = new System.Drawing.Point(136, 94);
            this.emptySpaceItem3.Name = "emptySpaceItem3";
            this.emptySpaceItem3.Size = new System.Drawing.Size(135, 23);
            this.emptySpaceItem3.Text = "emptySpaceItem3";
            this.emptySpaceItem3.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblFormalName
            // 
            this.lblFormalName.AllowHtmlStringInCaption = true;
            this.lblFormalName.Control = this.txtFormalName;
            this.lblFormalName.CustomizationFormText = "Formal Name";
            this.lblFormalName.Location = new System.Drawing.Point(0, 71);
            this.lblFormalName.Name = "lblFormalName";
            this.lblFormalName.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblFormalName.Size = new System.Drawing.Size(271, 23);
            this.lblFormalName.Text = "Formal Name";
            this.lblFormalName.TextSize = new System.Drawing.Size(73, 13);
            // 
            // frmUnitofMeasure
            // 
            this.AcceptButton = this.btnSave;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.ClientSize = new System.Drawing.Size(281, 158);
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmUnitofMeasure";
            this.Text = "Unit of Measure";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtFormalName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSymbol.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtCode.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtNoDecimalPlace.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.cmbType.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblNumberofdecimalplace)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCode)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblSymbol)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblType)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFormalName)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.TextEdit txtSymbol;
        private DevExpress.XtraEditors.TextEdit txtCode;
        private DevExpress.XtraEditors.TextEdit txtNoDecimalPlace;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem lblNumberofdecimalplace;
        private DevExpress.XtraLayout.LayoutControlItem lblCode;
        private DevExpress.XtraLayout.LayoutControlItem lblSymbol;
        private DevExpress.XtraLayout.LayoutControlItem lblType;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem3;
        private DevExpress.XtraEditors.TextEdit txtFormalName;
        private DevExpress.XtraLayout.LayoutControlItem lblFormalName;
        private DevExpress.XtraEditors.ComboBoxEdit cmbType;
    }
}