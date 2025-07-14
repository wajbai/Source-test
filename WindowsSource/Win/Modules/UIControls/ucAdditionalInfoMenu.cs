using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraBars;

namespace ACPP.Modules.UIControls
{
    public partial class ucAdditionalInfoMenu : UserControl
    {
        public event EventHandler DonorClicked;
        public event EventHandler DeleteVoucherClicked;
        public event EventHandler EntryMethodClicked;
        public event EventHandler PrintVoucherClicked;
        public event EventHandler VoucherBillsClicked;

        public ucAdditionalInfoMenu()
        {
            InitializeComponent();

            DiableVoucherBills = BarItemVisibility.Never;
        }

        public BarItemVisibility DiableDonor
        {
            get { return bbiDonorInfo.Visibility; }
            set { bbiDonorInfo.Visibility = value; }
        }

        public BarItemVisibility DiableVoucherBills
        {
            get { return bbiAttachVoucherBills.Visibility; }
            set { //bbiAttachVoucherBills.Visibility = value;
                bbiAttachVoucherBills.Visibility = BarItemVisibility.Never;
            }
        }


        public string DeleteCaption
        {
            set { bbiDeleteVocuher.Caption = value; }
        }

        public BarItemVisibility DisableEntryMethod
        {
            get { return bbiEntryMethod.Visibility; }
            set { bbiEntryMethod.Visibility = value; }
        }
        public BarItemVisibility DisableDeleteVocuher
        {
            get { return bbiDeleteVocuher.Visibility; }
            set { bbiDeleteVocuher.Visibility = value; }
        }

        public bool LockDeleteVocuher
        {
            get { return bbiDeleteVocuher.Enabled; }
            set { bbiDeleteVocuher.Enabled = value; }
        }

        public BarItemVisibility DisablePrintVoucher
        {
            get { return bbiPrintVoucher.Visibility; }
            set { bbiPrintVoucher.Visibility = value; }
        }

        public bool LockPrintVoucher
        {
            get { return bbiPrintVoucher.Enabled; }
            set { bbiPrintVoucher.Enabled = value; }
        }

        public string EntryCaption
        {
            get { return bbiEntryMethod.Caption; }
            set { bbiEntryMethod.Caption = value; }
        }

        public string PrintVoucherCaption
        {
            get { return bbiPrintVoucher.Caption; }
            set { bbiPrintVoucher.Caption = value; }
        }

        private void bbiDonorInfo_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (DonorClicked != null)
            {
                DonorClicked(this, e);
            }
        }

        private void bbiEntryMethod_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (EntryMethodClicked != null)
            {
                EntryMethodClicked(this, e);
            }
        }

        private void bbiDeleteVocuher_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (DeleteVoucherClicked != null)
            {
                DeleteVoucherClicked(this, e);
            }
        }

        private void bbiPrintVoucher_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (PrintVoucherClicked != null)
                PrintVoucherClicked(this, e);
        }

        private void bbiAttachVoucherBills_ItemClick(object sender, ItemClickEventArgs e)
        {

            if (VoucherBillsClicked != null)
                VoucherBillsClicked(this, e);
        }
    }
}
