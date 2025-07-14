using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraWaitForm;
using DevExpress.Skins;
using DevExpress.LookAndFeel;

namespace PAYROLL
{
    public partial class frmPayWait : WaitForm
    {
        public frmPayWait()
        {
            InitializeComponent();
            ChangeLoadingPicture();
        }

        #region Overrides

        public override void SetCaption(string caption)
        {
            base.SetCaption(caption);
            this.progressPanel1.Caption = caption;
        }
        public override void SetDescription(string description)
        {
            base.SetDescription(description);
            this.progressPanel1.Description = description;
        }
        public override void ProcessCommand(Enum cmd, object arg)
        {
            base.ProcessCommand(cmd, arg);
        }
        public void ChangeLoadingPicture()
        {
            Skin commonSkin = CommonSkins.GetSkin(UserLookAndFeel.Default.ActiveLookAndFeel);
            SkinElement loadingBig = commonSkin["LoadingBig"];
            loadingBig.Image.SetImage(PAYROLL.Properties.Resources._1410000099_check_mark, Color.Empty);
        }
        #endregion

        public enum WaitFormCommand
        {
        }
    }
}