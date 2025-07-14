namespace ACPP.Modules.Inventory
{
    partial class frmUnitofMeasureAdd
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmUnitofMeasureAdd));
            this.btnSave = new DevExpress.XtraEditors.SimpleButton();
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.txtFormalName = new DevExpress.XtraEditors.TextEdit();
            this.btnClose = new DevExpress.XtraEditors.SimpleButton();
            this.txtSymbol = new DevExpress.XtraEditors.TextEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lblUnit = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblFormalName = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblKg = new DevExpress.XtraLayout.SimpleLabelItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.txtFormalName.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSymbol.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblUnit)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFormalName)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblKg)).BeginInit();
            this.SuspendLayout();
            // 
            // btnSave
            // 
            resources.ApplyResources(this.btnSave, "btnSave");
            this.btnSave.Name = "btnSave";
            this.btnSave.StyleController = this.layoutControl1;
            this.btnSave.Click += new System.EventHandler(this.btnSave_Click);
            // 
            // layoutControl1
            // 
            this.layoutControl1.Controls.Add(this.txtFormalName);
            this.layoutControl1.Controls.Add(this.btnSave);
            this.layoutControl1.Controls.Add(this.btnClose);
            this.layoutControl1.Controls.Add(this.txtSymbol);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(749, 148, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // txtFormalName
            // 
            this.txtFormalName.EnterMoveNextControl = true;
            resources.ApplyResources(this.txtFormalName, "txtFormalName");
            this.txtFormalName.Name = "txtFormalName";
            this.txtFormalName.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtFormalName.Properties.MaxLength = 50;
            this.txtFormalName.StyleController = this.layoutControl1;
            this.txtFormalName.Leave += new System.EventHandler(this.txtFormalName_Leave);
            // 
            // btnClose
            // 
            this.btnClose.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnClose, "btnClose");
            this.btnClose.Name = "btnClose";
            this.btnClose.StyleController = this.layoutControl1;
            this.btnClose.Click += new System.EventHandler(this.btnClose_Click);
            // 
            // txtSymbol
            // 
            this.txtSymbol.EnterMoveNextControl = true;
            resources.ApplyResources(this.txtSymbol, "txtSymbol");
            this.txtSymbol.Name = "txtSymbol";
            this.txtSymbol.Properties.BorderStyle = DevExpress.XtraEditors.Controls.BorderStyles.Simple;
            this.txtSymbol.Properties.MaxLength = 5;
            this.txtSymbol.StyleController = this.layoutControl1;
            this.txtSymbol.Leave += new System.EventHandler(this.txtSymbol_Leave);
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lblUnit,
            this.lblFormalName,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.lblKg});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.layoutControlGroup1.Size = new System.Drawing.Size(337, 75);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lblUnit
            // 
            this.lblUnit.AllowHtmlStringInCaption = true;
            this.lblUnit.Control = this.txtSymbol;
            resources.ApplyResources(this.lblUnit, "lblUnit");
            this.lblUnit.Location = new System.Drawing.Point(0, 0);
            this.lblUnit.Name = "lblUnit";
            this.lblUnit.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 2, 0);
            this.lblUnit.Size = new System.Drawing.Size(337, 25);
            this.lblUnit.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblUnit.TextSize = new System.Drawing.Size(62, 13);
            // 
            // lblFormalName
            // 
            this.lblFormalName.AllowHtmlStringInCaption = true;
            this.lblFormalName.Control = this.txtFormalName;
            resources.ApplyResources(this.lblFormalName, "lblFormalName");
            this.lblFormalName.Location = new System.Drawing.Point(0, 25);
            this.lblFormalName.Name = "lblFormalName";
            this.lblFormalName.Padding = new DevExpress.XtraLayout.Utils.Padding(2, 2, 0, 0);
            this.lblFormalName.Size = new System.Drawing.Size(337, 23);
            this.lblFormalName.Spacing = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 3);
            this.lblFormalName.TextSize = new System.Drawing.Size(62, 13);
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.btnClose;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(268, 48);
            this.layoutControlItem1.MaxSize = new System.Drawing.Size(69, 26);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(69, 26);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(69, 27);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnSave;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(199, 48);
            this.layoutControlItem2.MaxSize = new System.Drawing.Size(69, 26);
            this.layoutControlItem2.MinSize = new System.Drawing.Size(69, 26);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(69, 27);
            this.layoutControlItem2.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // lblKg
            // 
            this.lblKg.AllowHotTrack = false;
            resources.ApplyResources(this.lblKg, "lblKg");
            this.lblKg.Location = new System.Drawing.Point(0, 48);
            this.lblKg.MinSize = new System.Drawing.Size(77, 17);
            this.lblKg.Name = "lblKg";
            this.lblKg.Size = new System.Drawing.Size(199, 27);
            this.lblKg.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblKg.TextSize = new System.Drawing.Size(62, 13);
            // 
            // frmUnitofMeasureAdd
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnClose;
            this.Controls.Add(this.layoutControl1);
            this.Name = "frmUnitofMeasureAdd";
            this.Load += new System.EventHandler(this.frmUnitofMeasure_Load);
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.txtFormalName.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.txtSymbol.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblUnit)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblFormalName)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblKg)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraEditors.SimpleButton btnClose;
        private DevExpress.XtraEditors.TextEdit txtSymbol;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton btnSave;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.TextEdit txtFormalName;
        private DevExpress.XtraLayout.LayoutControlItem lblFormalName;
        private DevExpress.XtraLayout.SimpleLabelItem lblKg;
        private DevExpress.XtraLayout.LayoutControlItem lblUnit;
    }
}