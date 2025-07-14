namespace ACPP.Modules.Transaction
{
    partial class frmSelectImage
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmSelectImage));
            this.layoutControl1 = new DevExpress.XtraLayout.LayoutControl();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.btnOK = new DevExpress.XtraEditors.SimpleButton();
            this.imgsliderVoucherImageSlider = new DevExpress.XtraEditors.Controls.ImageSlider();
            this.picRemoveCurrentImage = new DevExpress.XtraEditors.PictureEdit();
            this.picAddImage = new DevExpress.XtraEditors.PictureEdit();
            this.layoutControlGroup1 = new DevExpress.XtraLayout.LayoutControlGroup();
            this.lcAddImage = new DevExpress.XtraLayout.LayoutControlItem();
            this.lcRemoveCurrentImage = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem2 = new DevExpress.XtraLayout.LayoutControlItem();
            this.layoutControlItem3 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).BeginInit();
            this.layoutControl1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.imgsliderVoucherImageSlider)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRemoveCurrentImage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAddImage.Properties)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcAddImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcRemoveCurrentImage)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            this.SuspendLayout();
            // 
            // layoutControl1
            // 
            this.layoutControl1.AllowCustomizationMenu = false;
            this.layoutControl1.Controls.Add(this.btnCancel);
            this.layoutControl1.Controls.Add(this.btnOK);
            this.layoutControl1.Controls.Add(this.imgsliderVoucherImageSlider);
            this.layoutControl1.Controls.Add(this.picRemoveCurrentImage);
            this.layoutControl1.Controls.Add(this.picAddImage);
            resources.ApplyResources(this.layoutControl1, "layoutControl1");
            this.layoutControl1.Name = "layoutControl1";
            this.layoutControl1.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(593, 108, 250, 350);
            this.layoutControl1.Root = this.layoutControlGroup1;
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.StyleController = this.layoutControl1;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // btnOK
            // 
            resources.ApplyResources(this.btnOK, "btnOK");
            this.btnOK.Name = "btnOK";
            this.btnOK.StyleController = this.layoutControl1;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // imgsliderVoucherImageSlider
            // 
            this.imgsliderVoucherImageSlider.LayoutMode = DevExpress.Utils.Drawing.ImageLayoutMode.Stretch;
            resources.ApplyResources(this.imgsliderVoucherImageSlider, "imgsliderVoucherImageSlider");
            this.imgsliderVoucherImageSlider.Name = "imgsliderVoucherImageSlider";
            this.imgsliderVoucherImageSlider.StyleController = this.layoutControl1;
            // 
            // picRemoveCurrentImage
            // 
            this.picRemoveCurrentImage.EditValue = global::ACPP.Properties.Resources.Delete_Mob;
            resources.ApplyResources(this.picRemoveCurrentImage, "picRemoveCurrentImage");
            this.picRemoveCurrentImage.Name = "picRemoveCurrentImage";
            this.picRemoveCurrentImage.StyleController = this.layoutControl1;
            this.picRemoveCurrentImage.Click += new System.EventHandler(this.picRemoveCurrentImage_Click);
            // 
            // picAddImage
            // 
            this.picAddImage.EditValue = global::ACPP.Properties.Resources.edit_add;
            resources.ApplyResources(this.picAddImage, "picAddImage");
            this.picAddImage.Name = "picAddImage";
            this.picAddImage.Properties.InitialImage = global::ACPP.Properties.Resources._7_32;
            this.picAddImage.StyleController = this.layoutControl1;
            this.picAddImage.Click += new System.EventHandler(this.picAddImage_Click);
            // 
            // layoutControlGroup1
            // 
            resources.ApplyResources(this.layoutControlGroup1, "layoutControlGroup1");
            this.layoutControlGroup1.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.layoutControlGroup1.GroupBordersVisible = false;
            this.layoutControlGroup1.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.lcAddImage,
            this.lcRemoveCurrentImage,
            this.layoutControlItem1,
            this.layoutControlItem2,
            this.layoutControlItem3,
            this.emptySpaceItem1});
            this.layoutControlGroup1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlGroup1.Name = "Root";
            this.layoutControlGroup1.Padding = new DevExpress.XtraLayout.Utils.Padding(5, 5, 5, 5);
            this.layoutControlGroup1.Size = new System.Drawing.Size(773, 454);
            this.layoutControlGroup1.TextVisible = false;
            // 
            // lcAddImage
            // 
            this.lcAddImage.Control = this.picAddImage;
            resources.ApplyResources(this.lcAddImage, "lcAddImage");
            this.lcAddImage.Location = new System.Drawing.Point(734, 0);
            this.lcAddImage.MaxSize = new System.Drawing.Size(24, 24);
            this.lcAddImage.MinSize = new System.Drawing.Size(24, 24);
            this.lcAddImage.Name = "lcAddImage";
            this.lcAddImage.Size = new System.Drawing.Size(29, 24);
            this.lcAddImage.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcAddImage.TextSize = new System.Drawing.Size(0, 0);
            this.lcAddImage.TextToControlDistance = 0;
            this.lcAddImage.TextVisible = false;
            // 
            // lcRemoveCurrentImage
            // 
            this.lcRemoveCurrentImage.Control = this.picRemoveCurrentImage;
            resources.ApplyResources(this.lcRemoveCurrentImage, "lcRemoveCurrentImage");
            this.lcRemoveCurrentImage.Location = new System.Drawing.Point(709, 0);
            this.lcRemoveCurrentImage.MaxSize = new System.Drawing.Size(25, 24);
            this.lcRemoveCurrentImage.MinSize = new System.Drawing.Size(25, 24);
            this.lcRemoveCurrentImage.Name = "lcRemoveCurrentImage";
            this.lcRemoveCurrentImage.Size = new System.Drawing.Size(25, 24);
            this.lcRemoveCurrentImage.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lcRemoveCurrentImage.TextSize = new System.Drawing.Size(0, 0);
            this.lcRemoveCurrentImage.TextToControlDistance = 0;
            this.lcRemoveCurrentImage.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.imgsliderVoucherImageSlider;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.MinSize = new System.Drawing.Size(54, 20);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(709, 444);
            this.layoutControlItem1.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // layoutControlItem2
            // 
            this.layoutControlItem2.Control = this.btnOK;
            resources.ApplyResources(this.layoutControlItem2, "layoutControlItem2");
            this.layoutControlItem2.Location = new System.Drawing.Point(709, 392);
            this.layoutControlItem2.Name = "layoutControlItem2";
            this.layoutControlItem2.Size = new System.Drawing.Size(54, 26);
            this.layoutControlItem2.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem2.TextToControlDistance = 0;
            this.layoutControlItem2.TextVisible = false;
            // 
            // layoutControlItem3
            // 
            this.layoutControlItem3.Control = this.btnCancel;
            resources.ApplyResources(this.layoutControlItem3, "layoutControlItem3");
            this.layoutControlItem3.Location = new System.Drawing.Point(709, 418);
            this.layoutControlItem3.Name = "layoutControlItem3";
            this.layoutControlItem3.Size = new System.Drawing.Size(54, 26);
            this.layoutControlItem3.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem3.TextToControlDistance = 0;
            this.layoutControlItem3.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(709, 24);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(54, 368);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // frmSelectImage
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.layoutControl1);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSelectImage";
            ((System.ComponentModel.ISupportInitialize)(this.layoutControl1)).EndInit();
            this.layoutControl1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.imgsliderVoucherImageSlider)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picRemoveCurrentImage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.picAddImage.Properties)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlGroup1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcAddImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lcRemoveCurrentImage)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl layoutControl1;
        private DevExpress.XtraLayout.LayoutControlGroup layoutControlGroup1;
        private DevExpress.XtraEditors.PictureEdit picRemoveCurrentImage;
        private DevExpress.XtraEditors.PictureEdit picAddImage;
        private DevExpress.XtraLayout.LayoutControlItem lcAddImage;
        private DevExpress.XtraLayout.LayoutControlItem lcRemoveCurrentImage;
        private DevExpress.XtraEditors.Controls.ImageSlider imgsliderVoucherImageSlider;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton btnOK;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem2;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem3;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
    }
}