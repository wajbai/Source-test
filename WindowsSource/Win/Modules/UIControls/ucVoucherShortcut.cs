using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using DevExpress.XtraEditors;
using DevExpress.XtraBars;

namespace ACPP.Modules.UIControls
{
    public partial class ucVoucherShortcut : DevExpress.XtraEditors.XtraUserControl
    {
        public event EventHandler DateClicked;
        public event EventHandler ProjectClicked;
        public event EventHandler ReceiptsClicked;
        public event EventHandler PaymentClicked;
        public event EventHandler ContraClicked;
        public event EventHandler JournalClicked;
        public event EventHandler BankAccountClicked;
        public event EventHandler FixedDepositClicked;
        public event EventHandler CostCentreClicked;
        public event EventHandler DonorClicked;
        public event EventHandler MappingClicked;
        public event EventHandler ConfigureClicked;
        public event EventHandler TransactionVoucherViewClicked;
        public event EventHandler LedgerAddClicked;
        public event EventHandler LedgerOptionsClicked;
        public event EventHandler NextVoucherDateClicked;
        public event EventHandler DeleteVoucherClicked;

        public ucVoucherShortcut()
        {
            InitializeComponent();
        }

        #region Properties
        public BarItemVisibility DisableDate
        {
            get { return bbiDate.Visibility; }
            set { bbiDate.Visibility = value; }
        }

        public bool LockDate
        {
            get { return bbiDate.Enabled; }
            set { bbiDate.Enabled = value; }
        }

        public BarItemVisibility DisableProject
        {
            get { return bbiProject.Visibility; }
            set { bbiProject.Visibility = value; }
        }

        public bool LockProject
        {
            get { return bbiProject.Enabled; }
            set { bbiProject.Enabled = value; }
        }
        
        public BarItemVisibility DisableReceipt
        {
            get { return bbiReceipts.Visibility; }
            set { bbiReceipts.Visibility = value; }
        }

        public bool LockReceipt
        {
            get { return bbiReceipts.Enabled; }
            set { bbiReceipts.Enabled = value; }
        }

        public BarItemVisibility DisablePayment
        {
            get { return bbiPayment.Visibility; }
            set { bbiPayment.Visibility = value; }
        }

        public BarItemVisibility DisableContra
        {
            get { return bbiContra.Visibility; }
            set { bbiContra.Visibility = value; }
        }

        public bool LockPayment
        {
            get { return bbiPayment.Enabled; }
            set { bbiPayment.Enabled = value; }
        }

        public bool LockContra
        {
            get { return bbiContra.Enabled; }
            set { bbiContra.Enabled = value; }
        }

        public BarItemVisibility DisableJournal
        {
            get { return bbiJournal.Visibility; }
            set { bbiJournal.Visibility = value; }
        }

       
        public BarItemVisibility DisableBankAccount
        {
            get { return bbiBankAccount.Visibility; }
            set { bbiBankAccount.Visibility = value; }
        }

        //public BarItemVisibility DisableFixedDeposit
        //{
        //    get { return bbiFixedDeposit.Visibility; }
        //    set { bbiFixedDeposit.Visibility = value; }
        //}

        public BarItemVisibility DisableCostCentre
        {
            get { return bbiCostCentre.Visibility; }
            set { bbiCostCentre.Visibility = value; }
        }

        public BarItemVisibility DisableDonor
        {
            get { return bbiDonor.Visibility; }
            set { bbiDonor.Visibility = value; }
        }

        public BarItemVisibility DisableConfigure
        {
            get { return bbiConfigure.Visibility; }
            set { bbiConfigure.Visibility = value; }
        }

        public BarItemVisibility DisableMapping
        {
            get { return bbiMapping.Visibility; }
            set { bbiMapping.Visibility = value; }
        }

        public BarItemVisibility DisableTransView
        {
            get { return bbiTransactionVoucherView.Visibility; }
            set { bbiTransactionVoucherView.Visibility = value; }
        }
        public BarItemVisibility DisableLedgerAdd
        {
            get { return bbiLedgerAdd.Visibility; }
            set { bbiLedgerAdd.Visibility = value; }

        }
        public BarItemVisibility DisableLedgerOption
        {
            get { return bbiLedgerOptions.Visibility; }
            set { bbiLedgerOptions.Visibility = value; }
        }

        public BarItemVisibility DisableNextVoucherDate
        {
            get { return bbiNextVoucherDate.Visibility; }
            set { bbiNextVoucherDate.Visibility = value; }
        }

        public bool LockNextVoucherDate
        {
            get { return bbiNextVoucherDate.Enabled; }
            set { bbiNextVoucherDate.Enabled = value; }
        }

        public BarItemVisibility DisableDeleteVoucher
        {
            get { return bbiDeleteVoucher.Visibility; }
            set { bbiDeleteVoucher.Visibility = value; }
        }

        public bool LockDeleteVoucher
        {
            get { return bbiDeleteVoucher.Enabled; }
            set { bbiDeleteVoucher.Enabled = value; }
        }
        
        #endregion

        private void bbiDate_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DateClicked != null)
            {
                DateClicked(this, e);
            }
        }

        private void bbiProject_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ProjectClicked != null)
            {
                ProjectClicked(this, e);
            }

        }

        private void bbiReceipts_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ReceiptsClicked != null)
            {
                ReceiptsClicked(this, e);
            }
        }

        private void bbiPayment_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (PaymentClicked != null)
            {
                PaymentClicked(this, e);
            }
        }

        private void bbiContra_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ContraClicked != null)
            {
                ContraClicked(this, e);
            }
        }

        private void bbiJournal_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (JournalClicked != null)
            {
                JournalClicked(this, e);
            }
        }

       

        private void bbiBankAccount_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (BankAccountClicked != null)
            {
                BankAccountClicked(this, e);
            }
        }

        private void bbiFixedDeposit_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //if (FixedDepositClicked != null)
            //{
            //    FixedDepositClicked(this, e);
            //}
        }

        private void bbiCostCentre_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (CostCentreClicked != null)
            {
                CostCentreClicked(this, e);
            }
        }

        private void bbiDonor_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (DonorClicked != null)
            {
                DonorClicked(this, e);
            }
        }

        private void bbiConfigure_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            if (ConfigureClicked != null)
            {
                ConfigureClicked(this, e);
            }
        }

        private void bbiMapping_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (MappingClicked != null)
            {
                MappingClicked(this, e);
            }
        }

        private void bbiTransactionVoucherView_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (TransactionVoucherViewClicked != null)
            {
                TransactionVoucherViewClicked(this, e);
            }
        }

        private void bbiLedgerAdd_ItemClick(object sender, ItemClickEventArgs e)
        {
            if(LedgerAddClicked!=null)
            {
              LedgerAddClicked(this, e);
            }
        }

        private void bbiLedgerOptions_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (LedgerOptionsClicked != null)
            {
                LedgerOptionsClicked(this, e);
            }

        }

        private void bbiNextVoucherDate_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (NextVoucherDateClicked != null)
            {
                NextVoucherDateClicked(this, e);
            }
        }

        private void bbiDeleteVoucher_ItemClick(object sender, ItemClickEventArgs e)
        {
            if (DeleteVoucherClicked != null)
            {
                DeleteVoucherClicked(this, e);
            }
        }
    }
}
