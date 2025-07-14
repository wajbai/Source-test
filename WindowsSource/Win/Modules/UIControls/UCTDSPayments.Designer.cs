namespace ACPP.Modules.UIControls
{
    partial class UCTDSPayments
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.bbmPendingPayments = new DevExpress.XtraBars.BarManager(this.components);
            this.bar1 = new DevExpress.XtraBars.Bar();
            this.bbiTDSPendingPayments = new DevExpress.XtraBars.BarButtonItem();
            this.barDockControlTop = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlBottom = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlLeft = new DevExpress.XtraBars.BarDockControl();
            this.barDockControlRight = new DevExpress.XtraBars.BarDockControl();
            ((System.ComponentModel.ISupportInitialize)(this.bbmPendingPayments)).BeginInit();
            this.SuspendLayout();
            // 
            // bbmPendingPayments
            // 
            this.bbmPendingPayments.AllowCustomization = false;
            this.bbmPendingPayments.Bars.AddRange(new DevExpress.XtraBars.Bar[] {
            this.bar1});
            this.bbmPendingPayments.DockControls.Add(this.barDockControlTop);
            this.bbmPendingPayments.DockControls.Add(this.barDockControlBottom);
            this.bbmPendingPayments.DockControls.Add(this.barDockControlLeft);
            this.bbmPendingPayments.DockControls.Add(this.barDockControlRight);
            this.bbmPendingPayments.Form = this;
            this.bbmPendingPayments.Items.AddRange(new DevExpress.XtraBars.BarItem[] {
            this.bbiTDSPendingPayments});
            this.bbmPendingPayments.MaxItemId = 1;
            // 
            // bar1
            // 
            this.bar1.BarName = "Tools";
            this.bar1.DockCol = 0;
            this.bar1.DockRow = 0;
            this.bar1.DockStyle = DevExpress.XtraBars.BarDockStyle.Top;
            this.bar1.LinksPersistInfo.AddRange(new DevExpress.XtraBars.LinkPersistInfo[] {
            new DevExpress.XtraBars.LinkPersistInfo(this.bbiTDSPendingPayments)});
            this.bar1.OptionsBar.DrawBorder = false;
            this.bar1.Text = "Tools";
            // 
            // bbiTDSPendingPayments
            // 
            this.bbiTDSPendingPayments.AllowHtmlText = DevExpress.Utils.DefaultBoolean.True;
            this.bbiTDSPendingPayments.Caption = "<b>Get Pending <color=blue><u>P</u></color>ayment Details</b>";
            this.bbiTDSPendingPayments.Id = 0;
            this.bbiTDSPendingPayments.ItemAppearance.Normal.Font = new System.Drawing.Font("Tahoma", 9F);
            this.bbiTDSPendingPayments.ItemAppearance.Normal.ForeColor = System.Drawing.Color.DarkCyan;
            this.bbiTDSPendingPayments.ItemAppearance.Normal.Options.UseFont = true;
            this.bbiTDSPendingPayments.ItemAppearance.Normal.Options.UseForeColor = true;
            this.bbiTDSPendingPayments.Name = "bbiTDSPendingPayments";
            this.bbiTDSPendingPayments.ItemClick += new DevExpress.XtraBars.ItemClickEventHandler(this.bbiTDSPendingPayments_ItemClick);
            // 
            // barDockControlTop
            // 
            this.barDockControlTop.CausesValidation = false;
            this.barDockControlTop.Dock = System.Windows.Forms.DockStyle.Top;
            this.barDockControlTop.Location = new System.Drawing.Point(0, 0);
            this.barDockControlTop.Size = new System.Drawing.Size(220, 29);
            // 
            // barDockControlBottom
            // 
            this.barDockControlBottom.CausesValidation = false;
            this.barDockControlBottom.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.barDockControlBottom.Location = new System.Drawing.Point(0, 28);
            this.barDockControlBottom.Size = new System.Drawing.Size(220, 0);
            // 
            // barDockControlLeft
            // 
            this.barDockControlLeft.CausesValidation = false;
            this.barDockControlLeft.Dock = System.Windows.Forms.DockStyle.Left;
            this.barDockControlLeft.Location = new System.Drawing.Point(0, 29);
            this.barDockControlLeft.Size = new System.Drawing.Size(0, 0);
            // 
            // barDockControlRight
            // 
            this.barDockControlRight.CausesValidation = false;
            this.barDockControlRight.Dock = System.Windows.Forms.DockStyle.Right;
            this.barDockControlRight.Location = new System.Drawing.Point(220, 29);
            this.barDockControlRight.Size = new System.Drawing.Size(0, 0);
            // 
            // UCTDSPayments
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.barDockControlLeft);
            this.Controls.Add(this.barDockControlRight);
            this.Controls.Add(this.barDockControlBottom);
            this.Controls.Add(this.barDockControlTop);
            this.Name = "UCTDSPayments";
            this.Size = new System.Drawing.Size(220, 28);
            ((System.ComponentModel.ISupportInitialize)(this.bbmPendingPayments)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private DevExpress.XtraBars.BarManager bbmPendingPayments;
        private DevExpress.XtraBars.Bar bar1;
        private DevExpress.XtraBars.BarButtonItem bbiTDSPendingPayments;
        private DevExpress.XtraBars.BarDockControl barDockControlTop;
        private DevExpress.XtraBars.BarDockControl barDockControlBottom;
        private DevExpress.XtraBars.BarDockControl barDockControlLeft;
        private DevExpress.XtraBars.BarDockControl barDockControlRight;
    }
}
