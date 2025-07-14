namespace ACPP.Modules.Master
{
    partial class frmPendingTransaction
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmPendingTransaction));
            this.lcPendingTrans = new DevExpress.XtraLayout.LayoutControl();
            this.btnOk = new DevExpress.XtraEditors.SimpleButton();
            this.btnCancel = new DevExpress.XtraEditors.SimpleButton();
            this.ucPendingTransaction = new ACPP.Modules.UIControls.UcPendingTransaction();
            this.lcgPendingTrans = new DevExpress.XtraLayout.LayoutControlGroup();
            this.layoutControlItem1 = new DevExpress.XtraLayout.LayoutControlItem();
            this.emptySpaceItem1 = new DevExpress.XtraLayout.EmptySpaceItem();
            this.lblCancel = new DevExpress.XtraLayout.LayoutControlItem();
            this.lblOk = new DevExpress.XtraLayout.LayoutControlItem();
            ((System.ComponentModel.ISupportInitialize)(this.lcPendingTrans)).BeginInit();
            this.lcPendingTrans.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.lcgPendingTrans)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCancel)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblOk)).BeginInit();
            this.SuspendLayout();
            // 
            // lcPendingTrans
            // 
            this.lcPendingTrans.Controls.Add(this.btnOk);
            this.lcPendingTrans.Controls.Add(this.btnCancel);
            this.lcPendingTrans.Controls.Add(this.ucPendingTransaction);
            resources.ApplyResources(this.lcPendingTrans, "lcPendingTrans");
            this.lcPendingTrans.Name = "lcPendingTrans";
            this.lcPendingTrans.OptionsCustomizationForm.DesignTimeCustomizationFormPositionAndSize = new System.Drawing.Rectangle(432, 82, 250, 350);
            this.lcPendingTrans.Root = this.lcgPendingTrans;
            // 
            // btnOk
            // 
            resources.ApplyResources(this.btnOk, "btnOk");
            this.btnOk.Name = "btnOk";
            this.btnOk.StyleController = this.lcPendingTrans;
            this.btnOk.Click += new System.EventHandler(this.btnOk_Click);
            // 
            // btnCancel
            // 
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            resources.ApplyResources(this.btnCancel, "btnCancel");
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.StyleController = this.lcPendingTrans;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // ucPendingTransaction
            // 
            this.ucPendingTransaction._ProjectId = 0;
            this.ucPendingTransaction._ProjectName = null;
            this.ucPendingTransaction.DeductyTypeId = 0;
            resources.ApplyResources(this.ucPendingTransaction, "ucPendingTransaction");
            this.ucPendingTransaction.Name = "ucPendingTransaction";
            this.ucPendingTransaction.PartyPaymentId = 0;
            this.ucPendingTransaction.ucPendingTrans = null;
            this.ucPendingTransaction.previewEvent += new System.EventHandler(this.ucPendingTransaction1_previewEvent);
            // 
            // lcgPendingTrans
            // 
            resources.ApplyResources(this.lcgPendingTrans, "lcgPendingTrans");
            this.lcgPendingTrans.EnableIndentsWithoutBorders = DevExpress.Utils.DefaultBoolean.True;
            this.lcgPendingTrans.GroupBordersVisible = false;
            this.lcgPendingTrans.Items.AddRange(new DevExpress.XtraLayout.BaseLayoutItem[] {
            this.layoutControlItem1,
            this.emptySpaceItem1,
            this.lblCancel,
            this.lblOk});
            this.lcgPendingTrans.Location = new System.Drawing.Point(0, 0);
            this.lcgPendingTrans.Name = "Root";
            this.lcgPendingTrans.Padding = new DevExpress.XtraLayout.Utils.Padding(0, 0, 0, 0);
            this.lcgPendingTrans.Size = new System.Drawing.Size(744, 320);
            this.lcgPendingTrans.TextVisible = false;
            // 
            // layoutControlItem1
            // 
            this.layoutControlItem1.Control = this.ucPendingTransaction;
            resources.ApplyResources(this.layoutControlItem1, "layoutControlItem1");
            this.layoutControlItem1.Location = new System.Drawing.Point(0, 0);
            this.layoutControlItem1.Name = "layoutControlItem1";
            this.layoutControlItem1.Size = new System.Drawing.Size(744, 294);
            this.layoutControlItem1.TextSize = new System.Drawing.Size(0, 0);
            this.layoutControlItem1.TextToControlDistance = 0;
            this.layoutControlItem1.TextVisible = false;
            // 
            // emptySpaceItem1
            // 
            this.emptySpaceItem1.AllowHotTrack = false;
            resources.ApplyResources(this.emptySpaceItem1, "emptySpaceItem1");
            this.emptySpaceItem1.Location = new System.Drawing.Point(0, 294);
            this.emptySpaceItem1.Name = "emptySpaceItem1";
            this.emptySpaceItem1.Size = new System.Drawing.Size(605, 26);
            this.emptySpaceItem1.TextSize = new System.Drawing.Size(0, 0);
            // 
            // lblCancel
            // 
            this.lblCancel.Control = this.btnCancel;
            resources.ApplyResources(this.lblCancel, "lblCancel");
            this.lblCancel.Location = new System.Drawing.Point(674, 294);
            this.lblCancel.MaxSize = new System.Drawing.Size(70, 26);
            this.lblCancel.MinSize = new System.Drawing.Size(70, 26);
            this.lblCancel.Name = "lblCancel";
            this.lblCancel.Size = new System.Drawing.Size(70, 26);
            this.lblCancel.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblCancel.TextSize = new System.Drawing.Size(0, 0);
            this.lblCancel.TextToControlDistance = 0;
            this.lblCancel.TextVisible = false;
            // 
            // lblOk
            // 
            this.lblOk.Control = this.btnOk;
            resources.ApplyResources(this.lblOk, "lblOk");
            this.lblOk.Location = new System.Drawing.Point(605, 294);
            this.lblOk.MaxSize = new System.Drawing.Size(69, 26);
            this.lblOk.MinSize = new System.Drawing.Size(69, 26);
            this.lblOk.Name = "lblOk";
            this.lblOk.Size = new System.Drawing.Size(69, 26);
            this.lblOk.SizeConstraintsType = DevExpress.XtraLayout.SizeConstraintsType.Custom;
            this.lblOk.TextSize = new System.Drawing.Size(0, 0);
            this.lblOk.TextToControlDistance = 0;
            this.lblOk.TextVisible = false;
            // 
            // frmPendingTransaction
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.CancelButton = this.btnCancel;
            this.Controls.Add(this.lcPendingTrans);
            this.MinimizeBox = false;
            this.Name = "frmPendingTransaction";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.frmPendingTransaction_FormClosing);
            this.Load += new System.EventHandler(this.frmPendingTransaction_Load);
            ((System.ComponentModel.ISupportInitialize)(this.lcPendingTrans)).EndInit();
            this.lcPendingTrans.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.lcgPendingTrans)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.layoutControlItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.emptySpaceItem1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblCancel)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.lblOk)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraLayout.LayoutControl lcPendingTrans;
        private DevExpress.XtraLayout.LayoutControlGroup lcgPendingTrans;
        private UIControls.UcPendingTransaction ucPendingTransaction;
        private DevExpress.XtraLayout.LayoutControlItem layoutControlItem1;
        private DevExpress.XtraEditors.SimpleButton btnOk;
        private DevExpress.XtraEditors.SimpleButton btnCancel;
        private DevExpress.XtraLayout.EmptySpaceItem emptySpaceItem1;
        private DevExpress.XtraLayout.LayoutControlItem lblCancel;
        private DevExpress.XtraLayout.LayoutControlItem lblOk;
    }
}