using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraWaitForm;
using Bosco.Utility.ConfigSetting;

namespace ACPP
{
    public partial class frmWait : WaitForm
    {
        public frmWait()
        {
            InitializeComponent();
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

        //private void ChangeLoadingPicture()
        //{
        //    DevExpress.Skins.Skin commonSkin = DevExpress.Skins.CommonSkins.GetSkin(DevExpress.LookAndFeel.UserLookAndFeel.Default.ActiveLookAndFeel);
        //    DevExpress.Skins.SkinElement loadingBig = commonSkin["LoadingBig"];
        //    loadingBig.Image.SetImage(ACPP.Properties.Resources.bullet1, Color.Empty);
        //}

        #endregion

        public enum WaitFormCommand
        {
        }

        private void frmWait_Load(object sender, EventArgs e)
        {
        }

        private void frmWait_FormClosed(object sender, FormClosedEventArgs e)
        {
        }
    }
}