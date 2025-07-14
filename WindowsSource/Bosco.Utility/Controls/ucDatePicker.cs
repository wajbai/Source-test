using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using Bosco.Utility;

namespace Bosco.Utility.Controls
{
    public partial class ucDatePicker : UserControl
    {
        protected Bosco.Utility.ConfigSetting.SettingProperty AppSetting
        {
            get { return new Bosco.Utility.ConfigSetting.SettingProperty(); }
        }

        CommonMember utilityMember = null;
        protected CommonMember UtilityMember
        {
            get
            {
                if (utilityMember == null) { utilityMember = new CommonMember(); }
                return utilityMember;
            }
        }


        public ucDatePicker()
        {
            InitializeComponent();
            //AppSetting.VoucherDate = UtilityMember.DateSet.ToDate(AppSetting.RecentVoucherDate);
            //datVoucherDate.Text = AppSetting.VoucherDate;
        }

        private void datVoucherDate_MouseEnter(object sender, EventArgs e)
        {
            try
            {
                //AppSetting.VoucherDate = UtilityMember.DateSet.ToDate(datVoucherDate.Text);              
            }
            catch (Exception ex)
            {
                XtraMessageBox.Show(ex.Message);
            }
        }
    }
}
